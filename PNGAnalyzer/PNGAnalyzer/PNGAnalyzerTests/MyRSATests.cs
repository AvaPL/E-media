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
            int[] keyLengths = {512 /*, 1024, 2048, 4096*/};
            foreach (var keyLength in keyLengths)
            {
                RSAParameters rsaParameters = MyRSA.GenerateKeyPair(keyLength);
                Assert.AreEqual(keyLength + 2 * 8 /* additional sign bytes from p and q */,
                    rsaParameters.Modulus.Length * 8);
            }
        }
    }
}