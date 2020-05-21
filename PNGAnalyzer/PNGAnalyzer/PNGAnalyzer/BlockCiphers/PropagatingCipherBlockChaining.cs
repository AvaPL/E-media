using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using PNGAnalyzer.RSA;

namespace PNGAnalyzer.BlockCiphers
{
    public class PropagatingCipherBlockChaining : IBlockCipher
    {
        private const int BlockSize = 32;
        private readonly IRSA rsa;

        public PropagatingCipherBlockChaining(IRSA rsa)
        {
            this.rsa = rsa;
            InitializationVector = BigIntegerExtensions.Random(BlockSize);
        }

        public PropagatingCipherBlockChaining(IRSA rsa, BigInteger initializationVector)
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
                var previousXor = CalculatePreviousXor(blocks, i, cipheredBlocks);
                var block = BigIntegerExtensions.UnsignedFromBytes(blocks[i]);
                block ^= previousXor;
                cipheredBlocks.Add(rsa.Encrypt(BigIntegerExtensions.UnsignedToBytes(block).Take(BlockSize).ToArray()));
            }

            return cipheredBlocks;
        }

        private BigInteger CalculatePreviousXor(List<byte[]> decipheredBlocks, int i, List<byte[]> cipheredBlocks)
        {
            if (i > 0)
            {
                var previousDecipheredBlock = BigIntegerExtensions.UnsignedFromBytes(decipheredBlocks[i - 1]);
                var previousCipheredBlock = BigIntegerExtensions.UnsignedFromBytes(cipheredBlocks[i - 1]);
                return previousCipheredBlock ^ previousDecipheredBlock;
            }

            return InitializationVector;
        }

        private List<byte[]> DecipherBlocks(List<byte[]> blocks)
        {
            List<byte[]> decipheredBlocks = new List<byte[]>(blocks.Count);
            for (int i = 0; i < blocks.Count; i++)
            {
                var previousXor = CalculatePreviousXor(blocks, i, decipheredBlocks);
                decipheredBlocks.Add(rsa.Decrypt(blocks[i]));
                var decipheredBlock = BigIntegerExtensions.UnsignedFromBytes(decipheredBlocks[i]);
                decipheredBlock ^= previousXor;
                decipheredBlocks[i] = BigIntegerExtensions.UnsignedToBytes(decipheredBlock).Take(BlockSize).ToArray();
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