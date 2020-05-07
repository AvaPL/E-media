using System.Security.Cryptography;

namespace PNGAnalyzer.RSA
{
    public static class RSAKeySerializer
    {
        public static RSAParameters DeserializeKey(string xmlString)
        {   var rsa = System.Security.Cryptography.RSA.Create();
            rsa.FromXmlString(xmlString);
            return rsa.ExportParameters(xmlString.Contains("<Q>"));
        }
        
        public static string SerializePrivateKey(RSAParameters rsaParameters)
        {
            var rsa = System.Security.Cryptography.RSA.Create(rsaParameters);
            return rsa.ToXmlString(true);
        }
        
        public static string SerializePublicKey(RSAParameters rsaParameters)
        {
            var rsa = System.Security.Cryptography.RSA.Create(rsaParameters);
            return rsa.ToXmlString(false);
        }
    }
}