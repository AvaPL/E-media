﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PNGAnalyzer;
using PNGAnalyzer.BlockCiphers;
using PNGAnalyzer.RSA;

namespace PNGAnalyzerTests.BlockCiphersTest
{
    [TestFixture] // Microsoft RSA doesn't work here
    public class CipherFeedbackTests
    {
        private CipherFeedback cipherFeedback;
        private ImageBlockCipher imageBlockCipher;
        private IRSA rsa;

        [SetUp]
        public void Setup()
        {
            rsa = new MyRSA(1024);
            cipherFeedback = new CipherFeedback(rsa);
            imageBlockCipher = new ImageBlockCipher(cipherFeedback);
        }

        [Test]
        public void ShouldCipherAndDecipherByteArraySmallerThanBlockSize()
        {
            byte[] data = Enumerable.Range(0, 16).Select(i => (byte) i).ToArray();
            byte[] cipheredData = cipherFeedback.Cipher(data);
            byte[] decipheredData = cipherFeedback.Decipher(cipheredData);
            Assert.AreEqual(data, decipheredData);
        }

        [Test]
        public void ShouldCipherAndDecipherByteArrayUsing2Blocks()
        {
            byte[] data = Enumerable.Range(0, 48).Select(i => (byte) i).ToArray();
            byte[] cipheredData = cipherFeedback.Cipher(data);
            byte[] decipheredData = cipherFeedback.Decipher(cipheredData);
            Assert.AreEqual(data, decipheredData);
        }

        [Test]
        public void ShouldCipherAndDecipherByteArrayUsingRandomBlocks()
        {
            Random random = new Random();
            cipherFeedback = new CipherFeedback(rsa, 0);
            for (int i = 0; i < 1000; i++)
            {
                byte[] data = new byte[random.Next(64, 96)];
                random.NextBytes(data);
                byte[] cipheredData = cipherFeedback.Cipher(data);
                byte[] decipheredData = cipherFeedback.Decipher(cipheredData);
                Assert.AreEqual(data, decipheredData);
            }
        }

        [Test]
        public void ShouldCipherAndDecipherWith0InitializationVector()
        {
            // 0 initialization vector may cause smaller encrypted data size than key length
            cipherFeedback = new CipherFeedback(rsa, 0);
            byte[] data =
            {
                208, 59, 152, 15, 20, 5, 233, 119, 22, 58, 9, 128, 253, 33, 212, 58, 184, 80, 242, 239, 193, 150, 177,
                195, 179, 68, 23, 13, 14, 162, 131, 226
            };
            byte[] cipheredData = cipherFeedback.Cipher(data);
            byte[] decipheredData = cipherFeedback.Decipher(cipheredData);
            Assert.AreEqual(data, decipheredData);
        }

        [Test]
        public void ShouldCipherAndDecipherByteArray()
        {
            byte[] data = Enumerable.Range(0, 1024).Select(i => (byte) i).ToArray();
            byte[] cipheredData = cipherFeedback.Cipher(data);
            byte[] decipheredData = cipherFeedback.Decipher(cipheredData);
            Assert.AreEqual(data, decipheredData);
        }

        [Test]
        public void ShouldCipherAndDecipherImage()
        {
            string filePathToRead = @"../../../Data/square_wave.png";
            string filePathToWrite = @"../../../Data/square_wave_encrypted_and_decrypted.png";
            List<Chunk> chunks = PNGFile.Read(filePathToRead);
            List<Chunk> parsedChunks = ChunkParser.Parse(chunks);
            List<Chunk> cipheredChunks = imageBlockCipher.CipherWithoutFiltering(parsedChunks);
            List<Chunk> decipheredChunks = imageBlockCipher.DecipherWithoutFiltering(cipheredChunks);
            PNGFile.Write(filePathToWrite, decipheredChunks);
        }
    }

    [TestFixture]
    public class CipherFeedbackTestsOnFiles
    {
        private readonly ImageBlockCipher imageBlockCipher;

        public CipherFeedbackTestsOnFiles()
        {
            IRSA rsa = new MyRSA(1024);
            imageBlockCipher = new ImageBlockCipher(new CipherFeedback(rsa));
        }

        [Test]
        public void ShouldCipherImageWithoutFiltering()
        {
            string filePathToRead = @"../../../Data/square_wave.png";
            string filePathToWrite = @"../../../Data/square_wave_encrypted.png";
            List<Chunk> chunks = PNGFile.Read(filePathToRead);
            List<Chunk> parsedChunks = ChunkParser.Parse(chunks);
            List<Chunk> cipheredChunks = imageBlockCipher.CipherWithoutFiltering(parsedChunks);
            PNGFile.Write(filePathToWrite, cipheredChunks);
        }
        
        [Test]
        public void ShouldDecipherImageWithoutFiltering()
        {
            string filePathToRead = @"../../../Data/square_wave_encrypted.png";
            string filePathToWrite = @"../../../Data/square_wave_decrypted.png";
            List<Chunk> chunks = PNGFile.Read(filePathToRead);
            List<Chunk> parsedChunks = ChunkParser.Parse(chunks);
            List<Chunk> decipheredChunks = imageBlockCipher.DecipherWithoutFiltering(parsedChunks);
            PNGFile.Write(filePathToWrite, decipheredChunks);
        }
        
        [Test]
        public void ShouldCipherImageWithFiltering()
        {
            string filePathToRead = @"../../../Data/square_wave.png";
            string filePathToWrite = @"../../../Data/square_wave_encrypted.png";
            imageBlockCipher.CipherWithFiltering(filePathToRead, filePathToWrite);
        }
        
        [Test]
        public void ShouldDecipherImageWithFiltering()
        {
            string filePathToRead = @"../../../Data/square_wave_encrypted.png";
            string filePathToWrite = @"../../../Data/square_wave_decrypted.png";
            imageBlockCipher.DecipherWithFiltering(filePathToRead, filePathToWrite);
        }
    }
}