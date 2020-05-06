using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using NUnit.Framework;
using PNGAnalyzer;
using PNGAnalyzer.BlockCiphers;
using PNGAnalyzer.RSA;

namespace PNGAnalyzerTests.BlockCiphersTest
{
    [TestFixture]
    public class ElectronicCodebookTests
    {
        private ElectronicCodebook electronicCodebook;

        [SetUp]
        public void Setup()
        {
            RSAParameters parameters = MicrosoftRSA.GenerateKeyPair(1024);
            IRSA microsoftRsa = new MicrosoftRSA(parameters);
            electronicCodebook = new ElectronicCodebook(microsoftRsa);
        }

        [Test]
        public void ShouldCipherAndDecipherByteArray()
        {
            byte[] data = Enumerable.Range(0, 1024).Select(i => (byte) i).ToArray();
            byte[] cipheredData = electronicCodebook.Cipher(data);
            byte[] decipheredData = electronicCodebook.Decipher(cipheredData);
            Assert.AreEqual(data, decipheredData);
        }

        [Test]
        public void ShouldCipherAndDecipherImage()
        {
            string filePathToRead = @"../../../Data/square_wave.png";
            string filePathToWrite = @"../../../Data/square_wave_encrypted.png";
            List<Chunk> chunks = PNGFile.Read(filePathToRead);
            List<Chunk> parsedChunks = ChunkParser.Parse(chunks);
            List<Chunk> cipheredChunks = electronicCodebook.CipherImage(parsedChunks);
            List<Chunk> decipheredChunks = electronicCodebook.DecipherImage(cipheredChunks);
            PNGFile.Write(filePathToWrite, decipheredChunks);
        }
    }


    [TestFixture]
    public class ElectronicCodebookTestsOnFiles
    {
        private readonly ElectronicCodebook electronicCodebook = GetElectronicCodebook();

        private static ElectronicCodebook GetElectronicCodebook()
        {
            RSAParameters parameters = MicrosoftRSA.GenerateKeyPair(1024);
            IRSA microsoftRsa = new MicrosoftRSA(parameters);
            return new ElectronicCodebook(microsoftRsa);
        }

        [Test]
        public void ShouldCipherImage()
        {
            string filePathToRead = @"../../../Data/square_wave.png";
            string filePathToWrite = @"../../../Data/square_wave_encrypted.png";
            List<Chunk> chunks = PNGFile.Read(filePathToRead);
            List<Chunk> parsedChunks = ChunkParser.Parse(chunks);
            List<Chunk> cipheredChunks = electronicCodebook.CipherImage(parsedChunks);
            PNGFile.Write(filePathToWrite, cipheredChunks);
        }

        [Test]
        public void ShouldDecipherImage()
        {
            string filePathToRead = @"../../../Data/square_wave_encrypted.png";
            string filePathToWrite = @"../../../Data/square_wave_decrypted.png";
            List<Chunk> chunks = PNGFile.Read(filePathToRead);
            List<Chunk> parsedChunks = ChunkParser.Parse(chunks);
            List<Chunk> decipheredChunks = electronicCodebook.DecipherImage(parsedChunks);
            PNGFile.Write(filePathToWrite, decipheredChunks);
        }
    }
}