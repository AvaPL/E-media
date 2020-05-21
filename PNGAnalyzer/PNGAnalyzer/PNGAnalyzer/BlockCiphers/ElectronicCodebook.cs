using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using PNGAnalyzer.RSA;

namespace PNGAnalyzer.BlockCiphers
{
    public class ElectronicCodebook : IBlockCipher
    {
        private const int BlockSize = 32;
        private readonly IRSA rsa;

        public ElectronicCodebook(IRSA rsa)
        {
            this.rsa = rsa;
        }
        
        public byte[] Cipher(byte[] data)
        {
            byte[] dataToDivide = BlockCipherSupport.AddPadding(data, BlockSize);
            List<byte[]> blocks = BlockCipherSupport.DivideIntoBlocks(dataToDivide, BlockSize);
            for (int i = 0; i < blocks.Count; i++) 
                blocks[i] = rsa.Encrypt(blocks[i]);

            return BlockCipherSupport.ConcatenateBlocks(blocks);
        }
        
        public byte[] Decipher(byte[] data)
        {
            RSAParameters parameters = rsa.ExportParameters();
            int keySize = parameters.Modulus.Length;
            List<byte[]> blocks = BlockCipherSupport.DivideIntoBlocks(data, keySize);
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i] = rsa.Decrypt(blocks[i]);
                blocks[i] = BlockCipherSupport.PadWithZeros(blocks[i], BlockSize);
            }

            return BlockCipherSupport.RemovePadding(BlockCipherSupport.ConcatenateBlocks(blocks));
        }
        
        public int GetResizeRatio()
        {
            return rsa.ExportParameters().Modulus.Length / BlockSize;
        }
    }
}