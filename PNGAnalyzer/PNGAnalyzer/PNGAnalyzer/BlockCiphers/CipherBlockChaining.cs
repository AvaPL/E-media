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
    }
}