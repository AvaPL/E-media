using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using PNGAnalyzer.RSA;

namespace PNGAnalyzer.BlockCiphers
{
    public class CipherFeedback
    {
        private const int BlockSize = 32;
        private readonly IRSA rsa;
        private BigInteger initializationVector;

        public CipherFeedback(IRSA rsa)
        {
            this.rsa = rsa;
            initializationVector = BigIntegerExtensions.Random(BlockSize);
        }

        public CipherFeedback(IRSA rsa, BigInteger initializationVector)
        {
            this.rsa = rsa;
            this.initializationVector = initializationVector;
        }

        public List<Chunk> CipherImage(List<Chunk> chunks)
        {
            // TODO: Move to a separate class with common interface
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
            List<byte[]> cipheredBlocks = CipherBlocks(blocks);
            return BlockCipherSupport.ConcatenateBlocks(cipheredBlocks);
        }

        private List<byte[]> CipherBlocks(List<byte[]> blocks)
        {
            List<byte[]> cipheredBlocks = new List<byte[]>(blocks.Count);
            for (int i = 0; i < blocks.Count; i++)
            {
                byte[] cipheredPreviousBlock = GetCipheredPreviousBlock(cipheredBlocks, i);
                BigInteger cipheredAgainPreviousBlock = BigIntegerExtensions.UnsignedFromBytes(rsa.Encrypt(cipheredPreviousBlock));
                BigInteger block = BigIntegerExtensions.UnsignedFromBytes(blocks[i]);
                byte[] xorResult = Xor(cipheredAgainPreviousBlock, block);
                cipheredBlocks.Add(xorResult);
            }

            return cipheredBlocks;
        }

        private byte[] Xor(BigInteger cipheredAgainPreviousBlock, BigInteger block)
        {
            byte[] xorResult = BigIntegerExtensions.UnsignedToBytes(cipheredAgainPreviousBlock ^ block);
            int keySize = rsa.ExportParameters().Modulus.Length;
            return BlockCipherSupport.PadWithZeroes(xorResult, keySize);
        }

        private byte[] GetCipheredPreviousBlock(List<byte[]> cipheredBlocks, int index)
        {
            return index > 0
                ? cipheredBlocks[index - 1].Take(BlockSize).ToArray()
                : BigIntegerExtensions.UnsignedToBytes(initializationVector);
        }

        public List<Chunk> DecipherImage(List<Chunk> chunks)
        {
            // TODO: Move to a separate class with common interface
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
                byte[] cipheredPreviousBlock = GetCipheredPreviousBlock(blocks, i);
                BigInteger cipheredAgainPreviousBlock = BigIntegerExtensions.UnsignedFromBytes(rsa.Encrypt(cipheredPreviousBlock)); // Cipher intended
                BigInteger block = BigIntegerExtensions.UnsignedFromBytes(blocks[i]);
                decipheredBlocks.Add(BigIntegerExtensions.UnsignedToBytes(cipheredAgainPreviousBlock ^ block));
                decipheredBlocks[i] = BlockCipherSupport.PadWithZeroes(decipheredBlocks[i], BlockSize);
            }
        
            return decipheredBlocks;
        }
    }
}