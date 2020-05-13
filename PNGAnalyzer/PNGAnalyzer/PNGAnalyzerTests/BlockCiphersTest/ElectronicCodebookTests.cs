using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography;
using NUnit.Framework;
using PNGAnalyzer;
using PNGAnalyzer.BlockCiphers;
using PNGAnalyzer.RSA;

namespace PNGAnalyzerTests.BlockCiphersTest
{
    [TestFixture (typeof(MicrosoftRSA))]
    // [TestFixture (typeof(MyRSA))] 
    public class ElectronicCodebookTests<T> where T:IRSA
    {
        private ElectronicCodebook electronicCodebook;
        private BlockCipherImage blockCipherImage;

        [SetUp]
        public void Setup()
        {
            IRSA rsa = (T) Activator.CreateInstance(typeof(T), 1024);
            electronicCodebook = new ElectronicCodebook(rsa);
            blockCipherImage = new BlockCipherImage(electronicCodebook);
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
    public class ElectronicCodebookTestsOnFiles<T> where T:IRSA
    {
        private readonly BlockCipherImage blockCipherImage;

        public ElectronicCodebookTestsOnFiles()
        {
            IRSA rsa = (T) Activator.CreateInstance(typeof(T), 1024);
            blockCipherImage = new BlockCipherImage(new ElectronicCodebook(rsa));
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
        
        // [Test]
        // public void ShouldCipherLenaImage()
        // {
        //     string filePathToRead = @"../../../Data/Lena.png";
        //     string filePathToWrite = @"../../../Data/Lena_encrypted.png";
        //     List<Chunk> chunks = PNGFile.Read(filePathToRead);
        //     List<Chunk> parsedChunks = ChunkParser.Parse(chunks);
        //     List<Chunk> cipheredChunks = blockCipherImage.Cipher(parsedChunks);
        //     PNGFile.Write(filePathToWrite, cipheredChunks);
        // }
        //
        // [Test]
        // public void ShouldDecipherLenaImage()
        // {
        //     string filePathToRead = @"../../../Data/Lena_encrypted.png";
        //     string filePathToWrite = @"../../../Data/Lena_decrypted.png";
        //     List<Chunk> chunks = PNGFile.Read(filePathToRead);
        //     List<Chunk> parsedChunks = ChunkParser.Parse(chunks);
        //     List<Chunk> decipheredChunks = blockCipherImage.Decipher(parsedChunks);
        //     PNGFile.Write(filePathToWrite, decipheredChunks);
        // }
    }
}