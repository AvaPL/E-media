using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using PNGAnalyzer.RSA;

namespace PNGAnalyzer.BlockCiphers
{
    public class ElectronicCodebook
    {
        private const int BlockSize = 32;
        private readonly IRSA rsa;

        public ElectronicCodebook(IRSA rsa)
        {
            this.rsa = rsa;
        }

        public List<Chunk> CipherImage(List<Chunk> chunks)
        {
            byte[] decompressedBytes = DecompressIDATs(chunks);
            byte[] cipheredBytes = Cipher(decompressedBytes);
            List<Chunk> resultIdats = CompressIDATs(cipheredBytes);
            List<Chunk> resultChunks = SwapIDATs(chunks, resultIdats);
            return resultChunks;
        }
        
        private static byte[] DecompressIDATs(List<Chunk> chunks)
        {
            List<IDAT> idats = chunks.Where(IsIDAT).Select(chunk => (IDAT) chunk).ToList();
            byte[] bytes = IDATConverter.ConcatToBytes(idats);
            byte[] decompressedBytes = ZlibCompression.Decompress(bytes);
            return decompressedBytes;
        }
        
        private static bool IsIDAT(Chunk chunk)
        {
            return chunk.Type == "IDAT";
        }
        
        public byte[] Cipher(byte[] data)
        {
            byte[] dataToDivide = AddPadding(data);
            List<byte[]> blocks = DivideIntoBlocks(dataToDivide, BlockSize);
            for (int i = 0; i < blocks.Count; i++) 
                blocks[i] = rsa.Encrypt(blocks[i]);

            return ConcatenateBlocks(blocks);
        }
        
        private static byte[] AddPadding(byte[] data)
        {
            int remainder = data.Length % BlockSize;
            int bytesToAdd = remainder != 0 ? BlockSize - remainder : BlockSize;
            byte[] dataWithPadding = AppendPadding(data, bytesToAdd);
            return dataWithPadding;
        }

        private static byte[] AppendPadding(byte[] data, int bytesToAdd)
        {
            byte[] dataWithPadding = new byte[data.Length + bytesToAdd];
            byte[] dataToAppend = new byte[bytesToAdd];
            dataToAppend[dataToAppend.Length - 1] = Convert.ToByte(bytesToAdd);
            data.CopyTo(dataWithPadding, 0);
            dataToAppend.CopyTo(dataWithPadding, data.Length);
            return dataWithPadding;
        }
        
        private static List<byte[]> DivideIntoBlocks(byte[] dataToDivide, int blockSize)
        {
            List<byte[]> result = new List<byte[]>();
            for (int i = 0; i < dataToDivide.Length; i += blockSize)
                result.Add(dataToDivide.Skip(i).Take(blockSize).ToArray());

            return result;
        }
        
        private byte[] ConcatenateBlocks(List<byte[]> blocks)
        {
            int dataLength = blocks.Sum(block => block.Length);
            byte[] data = new byte[dataLength];
            int index = 0;
            foreach (byte[] block in blocks)
            {
                block.CopyTo(data, index);
                index += block.Length;
            }

            return data;
        }
        
        private static List<Chunk> CompressIDATs(byte[] cipheredBytes)
        {
            byte[] compressedBytes = ZlibCompression.Compress(cipheredBytes);
            List<Chunk> resultIdats = IDATConverter.SplitToIDATs(compressedBytes).Select(idat => (Chunk) idat).ToList();
            return resultIdats;
        }
        
        private static List<Chunk> SwapIDATs(List<Chunk> chunks, List<Chunk> resultIdats)
        {
            int firstIdatIndex = chunks.TakeWhile(chunk => !IsIDAT(chunk)).Count();
            List<Chunk> resultChunks = chunks.Where(chunk => !IsIDAT(chunk)).ToList();
            resultChunks.InsertRange(firstIdatIndex, resultIdats);
            return resultChunks;
        }
        
        public List<Chunk> DecipherImage(List<Chunk> chunks)
        {
            byte[] decompressedBytes = DecompressIDATs(chunks);
            byte[] decipheredBytes = Decipher(decompressedBytes);
            List<Chunk> resultIdats = CompressIDATs(decipheredBytes);
            List<Chunk> resultChunks = SwapIDATs(chunks, resultIdats);
            return resultChunks;
        }
        
        public byte[] Decipher(byte[] data)
        {
            RSAParameters parameters = rsa.ExportParameters();
            int keySize = parameters.Modulus.Length;
            List<byte[]> blocks = DivideIntoBlocks(data, keySize);
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i] = rsa.Decrypt(blocks[i]);
                blocks[i] = PadWithZeroes(blocks[i]);
            }

            return RemovePadding(ConcatenateBlocks(blocks));
        }
        
        private byte[] PadWithZeroes(byte[] bytes)
        {
            if (bytes.Length == BlockSize) return bytes;
            byte[] result = new byte[BlockSize];
            bytes.CopyTo(result, 0);
            new byte[BlockSize - bytes.Length].CopyTo(result, bytes.Length);
            return result;
        }

        private static byte[] RemovePadding(byte[] imageData)
        {
            int bytesToTake = imageData.Length - Convert.ToInt32(imageData[imageData.Length - 1]);
            return imageData.Take(bytesToTake).ToArray();
        }
    }
}