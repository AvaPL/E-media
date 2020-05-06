using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using PNGAnalyzer.RSA;

namespace PNGAnalyzer.BlockCiphers
{
    public class ElectronicCodebook
    {
        private const int BlockSize = 8;
        private readonly int originalDataLength;
        private readonly IRSA rsa;

        public ElectronicCodebook(int dataLength, IRSA rsa)
        {
            originalDataLength = dataLength;
            this.rsa = rsa;
        }

        public byte[] Cipher(byte[] data)
        {
            byte[] dataToDivide = AddPadding(data);
            List<byte[]> blocks = DivideIntoBlocks(dataToDivide, BlockSize);
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i] = rsa.Encrypt(blocks[i]);
            }

            return ConcatenateBlocks(blocks);
        }

        public byte[] Decipher(byte[] data)
        {
            RSAParameters parameters = rsa.ExportParameters();
            int keySize = parameters.Modulus.Length;
            List<byte[]> blocks = DivideIntoBlocks(data, keySize);
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i] = rsa.Decrypt(blocks[i]);
            }

            byte[] imageData = ConcatenateBlocks(blocks);
            int bytesToTake = imageData.Length - Convert.ToInt32(imageData[imageData.Length - 1]); 
            return imageData.Take(bytesToTake).ToArray();
        }

        private static List<byte[]> DivideIntoBlocks(byte[] dataToDivide, int blockSize)
        {
            List<byte[]> result = new List<byte[]>();
            for (int i = 0; i < dataToDivide.Length; i += blockSize)
                result.Add(dataToDivide.Skip(i).Take(blockSize).ToArray());

            return result;
        }

        private static byte[] AddPadding(byte[] data)
        {
            int remainder = data.Length % BlockSize;
            int bytesToAdd;
            if (remainder != 0)
                bytesToAdd = BlockSize - remainder;
            else
                bytesToAdd = BlockSize;
            
            byte[] dataWithPadding = new byte[data.Length + bytesToAdd];
            byte[] dataToAppend = new byte[bytesToAdd];
            dataToAppend[dataToAppend.Length - 1] = Convert.ToByte(bytesToAdd);
            data.CopyTo(dataWithPadding, 0);
            dataToAppend.CopyTo(dataWithPadding, data.Length);
            return dataWithPadding;
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

        // public List<Chunk> CipherImage(List<Chunk> chunks)
        // {
        //     int firstIdatIndex = chunks.TakeWhile(chunk => !IsIDAT(chunk)).Count();
        //     List<IDAT> idats = chunks.Where(IsIDAT).Select(chunk => (IDAT) chunk).ToList();
        //     byte[] bytes = IDATConverter.ConcatToBytes(idats);
        //     byte[] decompressedBytes = ZlibCompression.Decompress(bytes);
        //     byte[] compressedBytes = ZlibCompression.Compress(decompressedBytes);
        //     List<Chunk> resultIdats = IDATConverter.SplitToIDATs(compressedBytes).Select(idat => (Chunk) idat).ToList();
        //     List<Chunk> resultChunks = chunks.Where(chunk => !IsIDAT(chunk)).ToList();
        //     resultChunks.InsertRange(firstIdatIndex, resultIdats);
        // }
    }
}