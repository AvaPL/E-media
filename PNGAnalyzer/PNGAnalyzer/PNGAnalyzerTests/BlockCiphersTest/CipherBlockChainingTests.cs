using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PNGAnalyzer;
using PNGAnalyzer.BlockCiphers;
using PNGAnalyzer.RSA;

namespace PNGAnalyzerTests.BlockCiphersTest
{
    [TestFixture (typeof(MicrosoftRSA))]
    // [TestFixture (typeof(MyRSA))]
    public class CipherBlockChainingTests<T> where T:IRSA
    {
        private CipherBlockChaining cipherBlockChaining;
        private BlockCipherImage blockCipherImage;
        
        [SetUp]
        public void Setup()
        {
            IRSA rsa = (T) Activator.CreateInstance(typeof(T), 1024);
            cipherBlockChaining = new CipherBlockChaining(rsa);
            blockCipherImage = new BlockCipherImage(cipherBlockChaining);
        }
        
        [Test]
        public void ShouldCipherAndDecipherByteArraySmallerThanBlockSize()
        {
            byte[] data = Enumerable.Range(0, 16).Select(i => (byte) i).ToArray();
            byte[] cipheredData = cipherBlockChaining.Cipher(data);
            byte[] decipheredData = cipherBlockChaining.Decipher(cipheredData);
            Assert.AreEqual(data, decipheredData);
        }
        
        [Test]
        public void ShouldCipherAndDecipherByteArrayUsing2Blocks()
        {
            byte[] data = Enumerable.Range(0, 48).Select(i => (byte) i).ToArray();
            byte[] cipheredData = cipherBlockChaining.Cipher(data);
            byte[] decipheredData = cipherBlockChaining.Decipher(cipheredData);
            Assert.AreEqual(data, decipheredData);
        }
        
        [Test]
        public void ShouldCipherAndDecipherByteArray()
        {
            byte[] data = Enumerable.Range(0, 1024).Select(i => (byte) i).ToArray();
            byte[] cipheredData = cipherBlockChaining.Cipher(data);
            byte[] decipheredData = cipherBlockChaining.Decipher(cipheredData);
            Assert.AreEqual(data, decipheredData);
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
    
    // [TestFixture (typeof(MicrosoftRSA))]
    [TestFixture (typeof(MyRSA))]
    public class CipherBlockChainingTestsOnFiles<T> where T:IRSA
    {
        private readonly BlockCipherImage blockCipherImage;

        public CipherBlockChainingTestsOnFiles()
        {
            IRSA rsa = (T) Activator.CreateInstance(typeof(T), 1024);
            blockCipherImage = new BlockCipherImage(new CipherBlockChaining(rsa));
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