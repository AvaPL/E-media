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
            List<byte[]> blocks = DivideIntoBlocks(data, BlockSize);
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

            return ConcatenateBlocks(blocks).Take(originalDataLength).ToArray();
        }

        private static List<byte[]> DivideIntoBlocks(byte[] data, int blockSize)
        {
            byte[] dataToDivide = AddPadding(data);
            List<byte[]> result = new List<byte[]>();
            for (int i = 0; i < dataToDivide.Length; i += blockSize)
                result.Add(dataToDivide.Skip(i).Take(blockSize).ToArray());

            return result;
        }

        private static byte[] AddPadding(byte[] data)
        {
            int remainder = data.Length % BlockSize;
            if (remainder != 0)
            {
                byte[] dataWithPadding = new byte[data.Length + BlockSize - remainder];
                byte[] dataToAppend = new byte[BlockSize - remainder];
                data.CopyTo(dataWithPadding, 0);
                dataToAppend.CopyTo(dataWithPadding, data.Length);
                return dataWithPadding;
            }

            return data;
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
    }
}