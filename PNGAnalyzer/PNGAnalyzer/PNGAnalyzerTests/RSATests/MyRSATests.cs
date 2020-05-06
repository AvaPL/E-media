using System;
using System.Linq;
using System.Security.Cryptography;
using NUnit.Framework;
using PNGAnalyzer.RSA;

namespace PNGAnalyzerTests.RSATests
{
    [TestFixture]
    public class MyRSATests
    {
        [Test]
        public void ShouldGenerateKeyOfSpecifiedLength()
        {
            int keyLength = 512;
            for (int i = 0; i < 10; i++) // Repeat 10 times to ensure correct output because of randomness
            {
                RSAParameters rsaParameters = MyRSA.GenerateKeyPair(keyLength);
                Assert.AreEqual(keyLength, rsaParameters.Modulus.Length * 8);
            }
        }

        [Test]
        public void ShouldImportParameters()
        {
            RSAParameters parameters = MyRSA.GenerateKeyPair(512);
            MyRSA myRsa = new MyRSA(parameters);
            Assert.AreEqual(parameters, myRsa.ExportParameters());
        }

        [Test]
        public void ShouldEncryptDataWithoutException()
        {
            MyRSA myRsa = new MyRSA(512);
            byte[] toEncrypt = {1, 2, 3};
            Assert.DoesNotThrow(() => myRsa.Encrypt(toEncrypt));
        }

        [Test]
        public void ShouldEncryptAndDecrypt()
        {
            MyRSA myRsa = new MyRSA(512);
            byte[] toEncrypt = {1, 2, 3};
            byte[] encrypted = myRsa.Encrypt(toEncrypt);
            Assert.AreEqual(64, encrypted.Length);
            byte[] decrypted = myRsa.Decrypt(encrypted);
            Assert.AreEqual(toEncrypt, decrypted);
        }

        [Test]
        public void ShouldEncryptAndDecryptEmptyArray()
        {
            MyRSA myRsa = new MyRSA(512);
            byte[] toEncrypt = { };
            byte[] encrypted = myRsa.Encrypt(toEncrypt);
            Assert.AreEqual(0, encrypted.Length);
            byte[] decrypted = myRsa.Decrypt(encrypted);
            Assert.AreEqual(toEncrypt, decrypted);
        }

        [Test]
        public void ShouldEncryptAndDecrypt0()
        {
            MyRSA myRsa = new MyRSA(512);
            byte[] toEncrypt = {0};
            byte[] encrypted = myRsa.Encrypt(toEncrypt);
            Assert.AreEqual(64, encrypted.Length);
            Assert.AreEqual(0, encrypted[0]);
            byte[] decrypted = myRsa.Decrypt(encrypted);
            Assert.AreEqual(toEncrypt, decrypted);
        }

        [Test]
        public void ShouldEncryptAndDecrypt1()
        {
            MyRSA myRsa = new MyRSA(512);
            byte[] toEncrypt = {1};
            byte[] encrypted = myRsa.Encrypt(toEncrypt);
            Assert.AreEqual(64, encrypted.Length);
            Assert.AreEqual(1, encrypted[0]);
            byte[] decrypted = myRsa.Decrypt(encrypted);
            Assert.AreEqual(toEncrypt, decrypted);
        }

        [Test]
        public void ShouldEncryptAndDecrypt100SetsOfRandomData()
        {
            int keyLength = 1024; // 1024 bit key for max data length of 64 bytes
            MyRSA myRsa = new MyRSA(keyLength);
            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                byte[] toEncrypt = Enumerable.Range(0, random.Next(1, keyLength / (8 * 2) + 1))
                    .Select(i => (byte) i).ToArray();
                byte[] encrypted = myRsa.Encrypt(toEncrypt);
                Assert.AreEqual(keyLength / 8, encrypted.Length);
                byte[] decrypted = myRsa.Decrypt(encrypted);
                Assert.AreEqual(toEncrypt, decrypted);
            }
        }
    }
}