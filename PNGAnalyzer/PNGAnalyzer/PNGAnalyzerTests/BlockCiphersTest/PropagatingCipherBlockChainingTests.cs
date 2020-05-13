using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PNGAnalyzer;
using PNGAnalyzer.BlockCiphers;
using PNGAnalyzer.RSA;

namespace PNGAnalyzerTests.BlockCiphersTest
{
    //[TestFixture (typeof(MicrosoftRSA))]
    [TestFixture (typeof(MyRSA))]
    public class PropagatingCipherBlockChainingTests<T> where T:IRSA
    {
        private PropagatingCipherBlockChaining propagatingCipherBlockChaining;
             
             [SetUp]
             public void Setup()
             {
                 IRSA rsa = (T) Activator.CreateInstance(typeof(T), 1024);
                 propagatingCipherBlockChaining = new PropagatingCipherBlockChaining(rsa);
             }
             
             [Test]
             public void ShouldCipherAndDecipherByteArraySmallerThanBlockSize()
             {
                 byte[] data = Enumerable.Range(0, 16).Select(i => (byte) i).ToArray();
                 byte[] cipheredData = propagatingCipherBlockChaining.Cipher(data);
                 byte[] decipheredData = propagatingCipherBlockChaining.Decipher(cipheredData);
                 Assert.AreEqual(data, decipheredData);
             }
             
             [Test]
             public void ShouldCipherAndDecipherByteArrayUsing2Blocks()
             {
                 byte[] data = Enumerable.Range(0, 48).Select(i => (byte) i).ToArray();
                 byte[] cipheredData = propagatingCipherBlockChaining.Cipher(data);
                 byte[] decipheredData = propagatingCipherBlockChaining.Decipher(cipheredData);
                 Assert.AreEqual(data, decipheredData);
             }
             
             [Test]
             public void ShouldCipherAndDecipherByteArray()
             {
                 byte[] data = Enumerable.Range(0, 1024).Select(i => (byte) i).ToArray();
                 byte[] cipheredData = propagatingCipherBlockChaining.Cipher(data);
                 byte[] decipheredData = propagatingCipherBlockChaining.Decipher(cipheredData);
                 Assert.AreEqual(data, decipheredData);
             }
             
             [Test]
             public void ShouldCipherAndDecipherImage()
             {
                 string filePathToRead = @"../../../Data/square_wave.png";
                 string filePathToWrite = @"../../../Data/square_wave_encrypted_and_decrypted.png";
                 List<Chunk> chunks = PNGFile.Read(filePathToRead);
                 List<Chunk> parsedChunks = ChunkParser.Parse(chunks);
                 List<Chunk> cipheredChunks = propagatingCipherBlockChaining.CipherImage(parsedChunks);
                 List<Chunk> decipheredChunks = propagatingCipherBlockChaining.DecipherImage(cipheredChunks);
                 PNGFile.Write(filePathToWrite, decipheredChunks);
             }
    }

    [TestFixture (typeof(MicrosoftRSA))]
    // [TestFixture(typeof(MyRSA))]
    public class PropagatingCipherBlockChainingTestsOnFiles<T> where T : IRSA
    {
        private readonly PropagatingCipherBlockChaining propagatingCipherBlockChaining;

        public PropagatingCipherBlockChainingTestsOnFiles()
        {
            IRSA rsa = (T) Activator.CreateInstance(typeof(T), 1024);
            propagatingCipherBlockChaining = new PropagatingCipherBlockChaining(rsa);
        }

        [Test]
        public void ShouldCipherImage()
        {
            string filePathToRead = @"../../../Data/square_wave.png";
            string filePathToWrite = @"../../../Data/square_wave_encrypted.png";
            List<Chunk> chunks = PNGFile.Read(filePathToRead);
            List<Chunk> parsedChunks = ChunkParser.Parse(chunks);
            List<Chunk> cipheredChunks = propagatingCipherBlockChaining.CipherImage(parsedChunks);
            PNGFile.Write(filePathToWrite, cipheredChunks);
        }
        
        [Test]
        public void ShouldDecipherImage()
        {
             string filePathToRead = @"../../../Data/square_wave_encrypted.png";
             string filePathToWrite = @"../../../Data/square_wave_decrypted.png";
             List<Chunk> chunks = PNGFile.Read(filePathToRead);
             List<Chunk> parsedChunks = ChunkParser.Parse(chunks);
            List<Chunk> decipheredChunks = propagatingCipherBlockChaining.DecipherImage(parsedChunks);
             PNGFile.Write(filePathToWrite, decipheredChunks);
         }
    }
}