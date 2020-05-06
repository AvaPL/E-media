using System;
using System.Linq;
using System.Security.Cryptography;
using NUnit.Framework;
using PNGAnalyzer.RSA;

namespace PNGAnalyzerTests.RSATests
{
    [TestFixture]
    public class MicrosoftRSATests
    {
        [SetUp]
        public void Setup()
        {
            RSAParameters parameters = MicrosoftRSA.GenerateKeyPair(512);
            MicrosoftRsa = new MicrosoftRSA(parameters);
        }

        private MicrosoftRSA MicrosoftRsa { get; set; }

        private void EncryptAndDecrypt(byte[] data)
        {
            byte[] encryptedData = MicrosoftRsa.Encrypt(data);
            byte[] decryptedData = MicrosoftRsa.Decrypt(encryptedData);
            Assert.AreEqual(data, decryptedData);
        }

        [Test]
        public void ShouldEncryptAndDecryptMultipleBytes()
        {
            byte[] data = {1, 2, 3, 4, 5};
            EncryptAndDecrypt(data);
        }

        [Test]
        public void ShouldEncryptAndDecryptMultipleBytesTwiceWithDifferentKeys()
        {
            byte[] data = {1, 2, 3, 4, 5};
            EncryptAndDecrypt(data);
            RSAParameters parameters = MicrosoftRSA.GenerateKeyPair(1048);
            MicrosoftRsa.ImportParameters(parameters);
            EncryptAndDecrypt(data);
        }
        
        [Test]
        public void ShouldEncryptAndDecryptEmptyArray()
        {
            byte[] data = {};
            EncryptAndDecrypt(data);
        }
        
        [Test]
        public void ShouldEncryptAndDecryptOneByte()
        {
            byte[] data = {3};
            EncryptAndDecrypt(data);
        }
        
        [Test]
        public void ShouldCompressAndDecompressMaxSizeArray()
        {
            RSAParameters parameters = MicrosoftRsa.ExportParameters();
            int maxValue = parameters.Modulus.Length - 11;
            byte[] data = Enumerable.Range(0, maxValue).Select(i => (byte) i).ToArray();
            EncryptAndDecrypt(data);
        }
        
        [Test]
        public void ShouldThrowExceptionForArrayLongerThanModulusSize()
        {
            RSAParameters parameters = MicrosoftRsa.ExportParameters();
            int maxValue = parameters.Modulus.Length - 10;
            byte[] data = Enumerable.Range(0, maxValue).Select(i => (byte) i).ToArray();
            Assert.Throws<CryptographicException>(()=> {MicrosoftRsa.Encrypt(data);});
            Assert.Throws<CryptographicException>(()=> {MicrosoftRsa.Decrypt(data);});
        }
    }
}