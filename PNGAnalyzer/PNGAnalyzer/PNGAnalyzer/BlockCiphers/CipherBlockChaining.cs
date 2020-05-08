using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
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
            CipherBlocks(blocks);
            return BlockCipherSupport.ConcatenateBlocks(blocks);
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
            List<byte[]> decipheredBlocks = DecipherBlocks(blocks);
            return BlockCipherSupport.RemovePadding(BlockCipherSupport.ConcatenateBlocks(decipheredBlocks));
        }

        private List<byte[]> DecipherBlocks(List<byte[]> blocks)
        {
            List<byte[]> decipheredBlocks = new List<byte[]>(blocks.Count);
            for (int i = 0; i < blocks.Count; i++)
            {
                BigInteger cipheredPreviousBlock =
                    i > 0 ? BigIntegerExtensions.UnsignedFromBytes(blocks[i - 1]) : initializationVector;
                decipheredBlocks.Add(rsa.Decrypt(blocks[i]));
                BigInteger decipheredBlock = BigIntegerExtensions.UnsignedFromBytes(decipheredBlocks[i]);
                decipheredBlock ^= cipheredPreviousBlock;
                decipheredBlocks[i] = BigIntegerExtensions.UnsignedToBytes(decipheredBlock);
                decipheredBlocks[i] = BlockCipherSupport.PadWithZeroes(decipheredBlocks[i], BlockSize);
            }

            return decipheredBlocks;
        }
    }
}