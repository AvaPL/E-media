using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PNGAnalyzer;
using PNGAnalyzer.BlockCiphers;
using PNGAnalyzer.RSA;

namespace PNGAnalyzerTests.BlockCiphersTest
{
    [TestFixture]
    public class CounterTest
    {
        private Counter counter;
        private BlockCipherImage blockCipherImage;
        
        [SetUp]
        public void Setup()
        {
            IRSA rsa = new MyRSA(1024);
            counter = new Counter(rsa);
            blockCipherImage = new BlockCipherImage(counter);
        }
        
        [Test]
        public void ShouldCipherAndDecipherByteArraySmallerThanBlockSize()
        {
            byte[] data = Enumerable.Range(0, 16).Select(i => (byte) i).ToArray();
            byte[] cipheredData = counter.Cipher(data);
            byte[] decipheredData = counter.Decipher(cipheredData);
            Assert.AreEqual(data, decipheredData);
        }
        
        [Test]
        public void ShouldCipherAndDecipherByteArrayUsing2Blocks()
        {
            byte[] data = Enumerable.Range(0, 48).Select(i => (byte) i).ToArray();
            byte[] cipheredData = counter.Cipher(data);
            byte[] decipheredData = counter.Decipher(cipheredData);
            Assert.AreEqual(data, decipheredData);
        }
        
        [Test]
        public void ShouldCipherAndDecipherByteArray()
        {
            byte[] data = Enumerable.Range(0, 1024).Select(i => (byte) i).ToArray();
            byte[] cipheredData = counter.Cipher(data);
            byte[] decipheredData = counter.Decipher(cipheredData);
            Assert.AreEqual(data, decipheredData);
        }
        
        [Test]
        public void ShouldCipherAndDecipherHugeByteArray()
        {
            byte[] data = Enumerable.Range(0, byte.MaxValue).Select(i => (byte) i).ToArray();
            byte[] cipheredData = counter.Cipher(data);
            byte[] decipheredData = counter.Decipher(cipheredData);
            Assert.AreEqual(data, decipheredData);
        }
        
        [Test]
        public void ShouldCipherAndDecipherProblematicArray()
        {
            byte[] data =
            {
                97, 235, 230, 60, 12, 166, 152, 206, 95, 224, 159, 165,
                206, 189, 81, 240, 44, 145, 234, 25, 69, 211, 135, 152, 108, 233, 65, 58, 161, 25, 118, 0, 96
            };
            byte[] cipheredData = counter.Cipher(data);
            byte[] decipheredData = counter.Decipher(cipheredData);
            Assert.AreEqual(data, decipheredData);
        }
        
        [Test]
        public void ShouldCipherAndDecipherRandomByteArray()
        {
            Random randNum = new Random();
            for (int i = 0; i < 100; i++)
            {
                byte[] data = Enumerable.Repeat(0, 90).Select(i => (byte) randNum.Next(0, byte.MaxValue)).ToArray();
                byte[] cipheredData = counter.Cipher(data);
                byte[] decipheredData = counter.Decipher(cipheredData);
                Assert.AreEqual(data, decipheredData, string.Join(", ", data));
            }
        }
        
        [Test]
        public void ShouldCipherAndDecipherImage()
        {
            string filePathToRead = @"../../../Data/square_wave.png";
            string filePathToWrite = @"../../../Data/square_wave_encrypted_and_decrypted.png";
            List<Chunk> chunks = PNGFile.Read(filePathToRead);
            List<Chunk> parsedChunks = ChunkParser.Parse(chunks);
            List<Chunk> cipheredChunks = blockCipherImage.Cipher(parsedChunks);
            List<Chunk> decipheredChunks = blockCipherImage.Decipher(cipheredChunks);
            PNGFile.Write(filePathToWrite, decipheredChunks);
        }
    }
    
    [TestFixture]
    public class CounterTestsOnFiles
    {
        private readonly BlockCipherImage blockCipherImage;

        public CounterTestsOnFiles()
        {
            IRSA rsa = new MyRSA(1024);
            blockCipherImage = new BlockCipherImage(new Counter(rsa));
        }

        [Test]
        public void ShouldCipherImage()
        {
            string filePathToRead = @"../../../Data/square_wave.png";
            string filePathToWrite = @"../../../Data/square_wave_encrypted.png";
            List<Chunk> chunks = PNGFile.Read(filePathToRead);
            List<Chunk> parsedChunks = ChunkParser.Parse(chunks);
            List<Chunk> cipheredChunks = blockCipherImage.Cipher(parsedChunks);
            PNGFile.Write(filePathToWrite, cipheredChunks);
        }

        [Test]
        public void ShouldDecipherImage()
        {
            string filePathToRead = @"../../../Data/square_wave_encrypted.png";
            string filePathToWrite = @"../../../Data/square_wave_decrypted.png";
            List<Chunk> chunks = PNGFile.Read(filePathToRead);
            List<Chunk> parsedChunks = ChunkParser.Parse(chunks);
            List<Chunk> decipheredChunks = blockCipherImage.Decipher(parsedChunks);
            PNGFile.Write(filePathToWrite, decipheredChunks);
        }
    }
}