using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using PNGAnalyzer.RSA;

namespace PNGAnalyzer.BlockCiphers
{
    public class CipherBlockChaining : IBlockCipher
    {
        private const int BlockSize = 32;
        private readonly IRSA rsa;
        private readonly BigInteger initializationVector;

        public CipherBlockChaining(IRSA rsa)
        {
            this.rsa = rsa;
            initializationVector = BigIntegerExtensions.Random(BlockSize);
        }

        public CipherBlockChaining(IRSA rsa, BigInteger initializationVector)
        {
            this.rsa = rsa;
            this.initializationVector = initializationVector;
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
                    i > 0
                        ? BigIntegerExtensions.UnsignedFromBytes(blocks[i - 1].Take(BlockSize).ToArray())
                        : initializationVector;
                BigInteger block = BigIntegerExtensions.UnsignedFromBytes(blocks[i]);
                block ^= cipheredPreviousBlock;
                blocks[i] = BigIntegerExtensions.UnsignedToBytes(block);
                blocks[i] = rsa.Encrypt(blocks[i]);
            }
        }

        public byte[] Decipher(byte[] data)
        {
            RSAParameters parameters = rsa.ExportParameters();
            int keySize = parameters.Modulus.Length;
            List<byte[]> blocks = BlockCipherSupport.DivideIntoBlocks(data, keySize);
            List<byte[]> decipheredBlocks = DecipherBlocks(blocks);
            return BlockCipherSupport.RemovePadding(BlockCipherSupport.ConcatenateBlocks(decipheredBlocks));
        }

        public int GetResizeRatio()
        {
            return rsa.ExportParameters().Modulus.Length / BlockSize;
        }

        private List<byte[]> DecipherBlocks(List<byte[]> blocks)
        {
            List<byte[]> decipheredBlocks = new List<byte[]>(blocks.Count);
            for (int i = 0; i < blocks.Count; i++)
            {
                BigInteger cipheredPreviousBlock = GetCipheredPreviousBlock(blocks, i);
                decipheredBlocks.Add(rsa.Decrypt(blocks[i]));
                BigInteger decipheredBlock = BigIntegerExtensions.UnsignedFromBytes(decipheredBlocks[i]);
                decipheredBlock ^= cipheredPreviousBlock;
                decipheredBlocks[i] = BigIntegerExtensions.UnsignedToBytes(decipheredBlock);
                decipheredBlocks[i] = BlockCipherSupport.PadWithZeroes(decipheredBlocks[i], BlockSize);
            }

            return decipheredBlocks;
        }

        private BigInteger GetCipheredPreviousBlock(List<byte[]> blocks, int index)
        {
            return index > 0
                ? BigIntegerExtensions.UnsignedFromBytes(blocks[index - 1].Take(BlockSize).ToArray())
                : initializationVector;
        }
    }
}