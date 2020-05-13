﻿using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using PNGAnalyzer.RSA;

namespace PNGAnalyzer.BlockCiphers
{
    public class OutputFeedback
    {
        private const int BlockSize = 32;
        private readonly IRSA rsa;
        private readonly BigInteger initializationVector;

        public OutputFeedback(IRSA rsa)
        {
            this.rsa = rsa;
            initializationVector = BigIntegerExtensions.Random(BlockSize);
        }

        public OutputFeedback(IRSA rsa, BigInteger initializationVector)
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
            byte[] beforeXor = BigIntegerExtensions.UnsignedToBytes(initializationVector);
            for (int i = 0; i < blocks.Count; i++)
            {
                cipheredBlocks.Add(rsa.Encrypt(beforeXor.Take(BlockSize).ToArray()));
                BigInteger beforeXorBlock = BigIntegerExtensions.UnsignedFromBytes(beforeXor);
                BigInteger block = BigIntegerExtensions.UnsignedFromBytes(blocks[i]);
                beforeXor = cipheredBlocks[i];
                cipheredBlocks[i] = XorWithKeySizePadding(beforeXorBlock, block);
            }

            return cipheredBlocks;
        }

        private byte[] XorWithKeySizePadding(BigInteger beforeXorBlock, BigInteger block)
        {
            byte[] xorResult = BigIntegerExtensions.UnsignedToBytes(beforeXorBlock ^ block);
            int keySize = rsa.ExportParameters().Modulus.Length;
            return BlockCipherSupport.PadWithZeroes(xorResult, keySize);
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
            byte[] beforeXor = BigIntegerExtensions.UnsignedToBytes(initializationVector);
            for (int i = 0; i < blocks.Count; i++)
            {
                decipheredBlocks.Add(rsa.Encrypt(beforeXor.Take(BlockSize).ToArray()));
                BigInteger beforeXorBlock = BigIntegerExtensions.UnsignedFromBytes(beforeXor);
                BigInteger block = BigIntegerExtensions.UnsignedFromBytes(blocks[i]);
                beforeXor = decipheredBlocks[i];
                decipheredBlocks[i] = XorWithBlockSizePadding(beforeXorBlock, block);
            }

            return decipheredBlocks;
        }
        
        private byte[] XorWithBlockSizePadding(BigInteger beforeXorBlock, BigInteger block)
        {
            byte[] xorResult = BigIntegerExtensions.UnsignedToBytes(beforeXorBlock ^ block);
            return BlockCipherSupport.PadWithZeroes(xorResult, BlockSize);
        }
    }
}