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
            byte[] decompressedBytes = BlockCipherSupport.DecompressIDATs(chunks);
            byte[] cipheredBytes = Cipher(decompressedBytes);
            List<Chunk> resultIdats = BlockCipherSupport.CompressIDATs(cipheredBytes);
            List<Chunk> resultChunks = BlockCipherSupport.SwapIDATs(chunks, resultIdats);
            return resultChunks;
        }
        
        public byte[] Cipher(byte[] data)
        {
            byte[] dataToDivide = BlockCipherSupport.AddPadding(data, BlockSize);
            List<byte[]> blocks = BlockCipherSupport.DivideIntoBlocks(dataToDivide, BlockSize);
            for (int i = 0; i < blocks.Count; i++) 
                blocks[i] = rsa.Encrypt(blocks[i]);

            return BlockCipherSupport.ConcatenateBlocks(blocks);
        }
        
        public List<Chunk> DecipherImage(List<Chunk> chunks)
        {
            byte[] decompressedBytes = BlockCipherSupport.DecompressIDATs(chunks);
            byte[] decipheredBytes = Decipher(decompressedBytes);
            List<Chunk> resultIdats = BlockCipherSupport.CompressIDATs(decipheredBytes);
            List<Chunk> resultChunks = BlockCipherSupport.SwapIDATs(chunks, resultIdats);
            return resultChunks;
        }
        
        public byte[] Decipher(byte[] data)
        {
            RSAParameters parameters = rsa.ExportParameters();
            int keySize = parameters.Modulus.Length;
            List<byte[]> blocks = BlockCipherSupport.DivideIntoBlocks(data, keySize);
            for (int i = 0; i < blocks.Count; i++) 
                blocks[i] = rsa.Decrypt(blocks[i]);

            return RemovePadding(BlockCipherSupport.ConcatenateBlocks(blocks));
        }

        private static byte[] RemovePadding(byte[] imageData)
        {
            int bytesToTake = imageData.Length - Convert.ToInt32(imageData[imageData.Length - 1]);
            return imageData.Take(bytesToTake).ToArray();
        }
    }
}