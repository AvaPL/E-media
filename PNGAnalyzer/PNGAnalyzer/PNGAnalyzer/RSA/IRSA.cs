namespace PNGAnalyzer.RSA
{
    public interface IRSA
    {
        byte[] Encrypt(byte[] data);
        byte[] Decrypt(byte[] data);
        void ImportPublicKey(string publicKey);
        void ImportPrivateKey(string privateKey);
        void ImportKeyPair(KeyPair keyPair);
        KeyPair GenerateKeyPair(int numberOfBits);
    }
}