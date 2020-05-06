using System.Security.Cryptography;

namespace PNGAnalyzer.RSA
{
    public interface IRSA
    {
        byte[] Encrypt(byte[] data);
        byte[] Decrypt(byte[] data);
        void ImportParameters(RSAParameters parameters);
        RSAParameters GenerateKeyPair(int numberOfBits);
    }
}