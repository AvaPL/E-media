using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using PNGAnalyzer.RSA;

namespace PNGAnalyzer.BlockCiphers
{
    public class Counter : IBlockCipher
    {
        private const int BlockSize = 32;
        private readonly IRSA rsa;

        public Counter(IRSA rsa)
        {
            this.rsa = rsa;
            InitializationVector = BigIntegerExtensions.Random(BlockSize);
        }

        public Counter(IRSA rsa, BigInteger initializationVector)
        {
            this.rsa = rsa;
            this.InitializationVector = initializationVector;
        }

        public BigInteger InitializationVector { get; }

        public byte[] Cipher(byte[] data)
        {
            byte[] dataToDivide = BlockCipherSupport.AddPadding(data, BlockSize);
            List<byte[]> blocks = BlockCipherSupport.DivideIntoBlocks(dataToDivide, BlockSize);
            CipherBlocks(blocks);
            return BlockCipherSupport.ConcatenateBlocks(blocks);
        }

        public byte[] Decipher(byte[] data)
        {
            RSAParameters parameters = rsa.ExportParameters();
            int keySize = parameters.Modulus.Length;
            List<byte[]> blocks = BlockCipherSupport.DivideIntoBlocks(data, keySize);
            DecipherBlocks(blocks);
            return BlockCipherSupport.RemovePadding(BlockCipherSupport.ConcatenateBlocks(blocks));
        }

        private void CipherBlocks(List<byte[]> blocks)
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                BigInteger nonceCounterXor = InitializationVector ^ new BigInteger(i);
                byte[] nonceCounterXorBytes = rsa.Encrypt(BigIntegerExtensions.UnsignedToBytes(nonceCounterXor));
                nonceCounterXor = BigIntegerExtensions.UnsignedFromBytes(nonceCounterXorBytes);
                BigInteger block = BigIntegerExtensions.UnsignedFromBytes(blocks[i]);
                block ^= nonceCounterXor;
                RSAParameters parameters = rsa.ExportParameters();
                int keySize = parameters.Modulus.Length;
                blocks[i] = BlockCipherSupport.PadWithZeroes(BigIntegerExtensions.UnsignedToBytes(block), keySize);
            }
        }

        private void DecipherBlocks(List<byte[]> blocks)
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                BigInteger nonceCounterXor = InitializationVector ^ new BigInteger(i);
                byte[] nonceCounterXorBytes = rsa.Encrypt(BigIntegerExtensions.UnsignedToBytes(nonceCounterXor));
                nonceCounterXor = BigIntegerExtensions.UnsignedFromBytes(nonceCounterXorBytes);
                BigInteger block = BigIntegerExtensions.UnsignedFromBytes(blocks[i]);
                block ^= nonceCounterXor;
                blocks[i] = BlockCipherSupport.PadWithZeroes(BigIntegerExtensions.UnsignedToBytes(block), BlockSize);
            }
        }
    }
}