using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using PNGAnalyzer.RSA;

namespace PNGAnalyzer.BlockCiphers
{
    public class CipherBlockChaining
    {
        private const int BlockSize = 32;
        private readonly IRSA rsa;
        private BigInteger initializationVector;

        public CipherBlockChaining(IRSA rsa)
        {
            this.rsa = rsa;
            int keyLengthBits = rsa.ExportParameters().Modulus.Length * 8;
            initializationVector = BigIntegerExtensions.Random(keyLengthBits);
        }

        public CipherBlockChaining(IRSA rsa, BigInteger initializationVector)
        {
            this.rsa = rsa;
            this.initializationVector = initializationVector;
        }

        public List<Chunk> CipherImage(List<Chunk> chunks)
        {
            byte[] decompressedBytes = DecompressIDATs(chunks);
            byte[] cipheredBytes = Cipher(decompressedBytes);
            List<Chunk> resultIdats = CompressIDATs(cipheredBytes);
            List<Chunk> resultChunks = SwapIDATs(chunks, resultIdats);
            return resultChunks;
        }

        public byte[] Cipher(byte[] data)
        {
            byte[] dataToDivide = AddPadding(data);
            List<byte[]> blocks = DivideIntoBlocks(dataToDivide, BlockSize);
            CipherBlocks(blocks);
            return ConcatenateBlocks(blocks);
        }

        private void CipherBlocks(List<byte[]> blocks)
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                BigInteger cipheredPreviousBlock =
                    i > 0 ? BigIntegerExtensions.UnsignedFromBytes(blocks[i - 1]) : initializationVector;
                BigInteger block = BigIntegerExtensions.UnsignedFromBytes(blocks[i]);
                block ^= cipheredPreviousBlock;
                blocks[i] = BigIntegerExtensions.UnsignedToBytes(block);
                blocks[i] = rsa.Encrypt(blocks[i]);
            }
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
    }
}