using System;
using System.Collections.Generic;
using System.Linq;

namespace PNGAnalyzer.BlockCiphers
{
    public class BlockCipherSupport
    {
        public static byte[] AddPadding(byte[] data, int blockSize)
        {
            int remainder = data.Length % blockSize;
            int bytesToAdd = remainder != 0 ? blockSize - remainder : blockSize;
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

        public static List<byte[]> DivideIntoBlocks(byte[] dataToDivide, int blockSize)
        {
            List<byte[]> result = new List<byte[]>();
            for (int i = 0; i < dataToDivide.Length; i += blockSize)
                result.Add(dataToDivide.Skip(i).Take(blockSize).ToArray());

            return result;
        }

        public static byte[] ConcatenateBlocks(List<byte[]> blocks)
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

        public static byte[] DecompressIDATs(List<Chunk> chunks)
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

        public static List<Chunk> CompressIDATs(byte[] cipheredBytes)
        {
            byte[] compressedBytes = ZlibCompression.Compress(cipheredBytes);
            List<Chunk> resultIdats = IDATConverter.SplitToIDATs(compressedBytes).Select(idat => (Chunk) idat).ToList();
            return resultIdats;
        }

        public static List<Chunk> SwapIDATs(List<Chunk> chunks, List<Chunk> resultIdats)
        {
            int firstIdatIndex = chunks.TakeWhile(chunk => !IsIDAT(chunk)).Count();
            List<Chunk> resultChunks = chunks.Where(chunk => !IsIDAT(chunk)).ToList();
            resultChunks.InsertRange(firstIdatIndex, resultIdats);
            return resultChunks;
        }

        public static byte[] PadWithZeroes(byte[] bytes, int blockSize)
        {
            if (bytes.Length == blockSize) return bytes;
            byte[] result = new byte[blockSize];
            bytes.CopyTo(result, 0);
            new byte[blockSize - bytes.Length].CopyTo(result, bytes.Length);
            return result;
        }

        public static byte[] RemovePadding(byte[] imageData)
        {
            int bytesToTake = imageData.Length - Convert.ToInt32(imageData[imageData.Length - 1]);
            return imageData.Take(bytesToTake).ToArray();
        }
    }
}