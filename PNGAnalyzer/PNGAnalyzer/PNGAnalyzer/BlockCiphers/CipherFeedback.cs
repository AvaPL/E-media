using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using PNGAnalyzer.RSA;

namespace PNGAnalyzer.BlockCiphers
{
    public class CipherFeedback : IBlockCipher
    {
        private const int BlockSize = 32;
        private readonly IRSA rsa;

        public CipherFeedback(IRSA rsa)
        {
            this.rsa = rsa;
            InitializationVector = BigIntegerExtensions.Random(BlockSize);
        }

        public CipherFeedback(IRSA rsa, BigInteger initializationVector)
        {
            this.rsa = rsa;
            this.InitializationVector = initializationVector;
        }

        public BigInteger InitializationVector { get; }

        public byte[] Cipher(byte[] data)
        {
            byte[] dataToDivide = BlockCipherSupport.AddPadding(data, BlockSize);
            List<byte[]> blocks = BlockCipherSupport.DivideIntoBlocks(dataToDivide, BlockSize);
            List<byte[]> cipheredBlocks = CipherBlocks(blocks);
            return BlockCipherSupport.ConcatenateBlocks(cipheredBlocks);
        }

        public byte[] Decipher(byte[] data)
        {
            RSAParameters parameters = rsa.ExportParameters();
            int keySize = parameters.Modulus.Length;
            List<byte[]> blocks = BlockCipherSupport.DivideIntoBlocks(data, keySize);
            List<byte[]> decipheredBlocks = DecipherBlocks(blocks);
            return BlockCipherSupport.RemovePadding(BlockCipherSupport.ConcatenateBlocks(decipheredBlocks));
        }

        private List<byte[]> CipherBlocks(List<byte[]> blocks)
        {
            List<byte[]> cipheredBlocks = new List<byte[]>(blocks.Count);
            for (int i = 0; i < blocks.Count; i++)
            {
                byte[] cipheredPreviousBlock = GetCipheredPreviousBlock(cipheredBlocks, i);
                BigInteger cipheredAgainPreviousBlock = BigIntegerExtensions.UnsignedFromBytes(rsa.Encrypt(cipheredPreviousBlock));
                BigInteger block = BigIntegerExtensions.UnsignedFromBytes(blocks[i]);
                byte[] xorResult = XorKeySizePadding(cipheredAgainPreviousBlock, block);
                cipheredBlocks.Add(xorResult);
            }

            return cipheredBlocks;
        }

        private byte[] XorKeySizePadding(BigInteger cipheredAgainPreviousBlock, BigInteger block)
        {
            byte[] xorResult = BigIntegerExtensions.UnsignedToBytes(cipheredAgainPreviousBlock ^ block);
            int keySize = rsa.ExportParameters().Modulus.Length;
            return BlockCipherSupport.PadWithZeros(xorResult, keySize);
        }

        private byte[] GetCipheredPreviousBlock(List<byte[]> cipheredBlocks, int index)
        {
            return index > 0
                ? cipheredBlocks[index - 1].Take(BlockSize).ToArray()
                : BigIntegerExtensions.UnsignedToBytes(InitializationVector);
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
                decipheredBlocks[i] = BlockCipherSupport.PadWithZeros(decipheredBlocks[i], BlockSize);
            }
        
            return decipheredBlocks;
        }
        
        public int GetResizeRatio()
        {
            return rsa.ExportParameters().Modulus.Length / BlockSize;
        }
    }
}