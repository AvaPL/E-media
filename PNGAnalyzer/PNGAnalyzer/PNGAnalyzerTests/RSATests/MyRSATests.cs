using System.Security.Cryptography;
using NUnit.Framework;
using PNGAnalyzer.RSA;

namespace PNGAnalyzerTests
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
                Assert.AreEqual(keyLength + 8, rsaParameters.Modulus.Length * 8); // Include sign bit
            }
        }

        [Test]
        public void ShouldImportParameters()
        {
            RSAParameters parameters = MyRSA.GenerateKeyPair(512);
            MyRSA myRsa = new MyRSA(parameters);
            Assert.AreEqual(parameters, myRsa.Parameters);
        }

        [Test]
        public void ShouldEncryptDataWithoutException()
        {
            MyRSA myRsa = new MyRSA(512);
            byte[] toEncrypt = {1, 2, 3};
            Assert.DoesNotThrow(() => myRsa.Encrypt(toEncrypt));
        }
    }
}