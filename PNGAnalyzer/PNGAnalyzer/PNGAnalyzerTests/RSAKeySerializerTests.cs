using System.Security.Cryptography;
using NUnit.Framework;
using PNGAnalyzer.RSA;

namespace PNGAnalyzerTests
{
    [TestFixture]
    public class RSAKeySerializerTests
    {
        [Test]
        public void ShouldDeserializeAndSerializeCorrectly()
        {
            string publicKey = "<RSAKeyValue><Modulus>w67swbp31OFizAMAG/ZsGTrsA8QerxhlfonU04Rkac6Dh3c9ZqVfBnrdkL0tihZ2PnKqWk07zCKd7bt40cSUinL9zLOTiS5UpyzC5VxCsCM2ptDTEzsVTuspSutoiwM4jxmXzJAe3Cp7lkaersFnxaK5+qi8ByeRDHoPu4V7SKs=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            RSAParameters rsaParameters = RSAKeySerializer.DeserializeKey(publicKey);
            Assert.AreEqual(publicKey, RSAKeySerializer.SerializePublicKey(rsaParameters));
        }
    }
}