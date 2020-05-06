using System;
using System.Linq;
using System.Security.Cryptography;
using NUnit.Framework;
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
            byte[] data = Enumerable.Range(0, 1030).Select(i => (byte) i).ToArray();
            RSAParameters parameters = MicrosoftRSA.GenerateKeyPair(1024);
            IRSA microsoftRsa = new MicrosoftRSA(parameters);
            ElectronicCodebook ecb = new ElectronicCodebook(data.Length, microsoftRsa);
            byte[] cipheredData = ecb.Cipher(data);
            byte[] decipheredData = ecb.Decipher(cipheredData);
            Assert.AreEqual(data, decipheredData);
        }
    }
}