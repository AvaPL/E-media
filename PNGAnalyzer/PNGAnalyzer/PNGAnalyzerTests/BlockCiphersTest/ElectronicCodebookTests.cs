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
        [Test]
        public void CipherAndDecipherByteArray()
        {
            byte[] data = Enumerable.Range(0, 1024).Select(i => (byte) i).ToArray();
            RSAParameters parameters = MicrosoftRSA.GenerateKeyPair(1024);
            IRSA microsoftRsa = new MicrosoftRSA(parameters);
            ElectronicCodebook ecb = new ElectronicCodebook(data.Length, microsoftRsa);
            byte[] cipheredData = ecb.Cipher(data);
            byte[] decipheredData = ecb.Decipher(cipheredData);
            Assert.AreEqual(data, decipheredData);
        }

        [Test]
        public void CipherAndDecipherImage()
        {
            RSAParameters parameters = MicrosoftRSA.GenerateKeyPair(1024);
            IRSA microsoftRsa = new MicrosoftRSA(parameters);
            
            string filePathToRead = @"../../../Data/Plan.png";
            string filePathToWrite = @"../../../Data/PlanEncrypted.png";
            List<Chunk> chunks = PNGFile.Read(filePathToRead);
            List<Chunk> parsedChunks = ChunkParser.Parse(chunks);
            byte[] bytes=IDATConverter.ConcatToBytes(IDATConverter.GetAllIDats(parsedChunks));
            byte[] decompressedBytes = ZlibCompression.Decompress(bytes);
            ElectronicCodebook ecb = new ElectronicCodebook(decompressedBytes.Length, microsoftRsa);
            byte[] cipheredData = ecb.Cipher(decompressedBytes);
            byte[] decipheredData = ecb.Decipher(cipheredData);
            List<IDAT> idats = IDATConverter.SplitToIDATs(decipheredData);
        }
    }
    
}
