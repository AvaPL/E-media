using System;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Serialization;

namespace PNGAnalyzer.RSA
{
    public class MicrosoftRSA : IRSA
    {
        private readonly RSACryptoServiceProvider cryptoServiceProvider;

        public MicrosoftRSA(RSAParameters parameters)
        {
            cryptoServiceProvider = new RSACryptoServiceProvider();
            cryptoServiceProvider.ImportParameters(parameters);
        }
        
        public MicrosoftRSA(int keyLength)
        {
            cryptoServiceProvider = new RSACryptoServiceProvider(keyLength);
        }

        public byte[] Encrypt(byte[] data)
        {
            byte[] encryptedData = cryptoServiceProvider.Encrypt(data, false);
            return encryptedData;
        }

        public byte[] Decrypt(byte[] data)
        {
            byte[] decryptedData = cryptoServiceProvider.Decrypt(data, false);
            return decryptedData;
        }

        public void ImportParameters(RSAParameters parameters)
        {
            cryptoServiceProvider.ImportParameters(parameters);
        }
        
        public RSAParameters ExportParameters()
        {
            return cryptoServiceProvider.ExportParameters(true);
        }

        RSAParameters IRSA.GenerateKeyPair(int numberOfBits)
        {
            return GenerateKeyPair(numberOfBits);
        }

        public static RSAParameters GenerateKeyPair(int numberOfBits)
        {
            RSACryptoServiceProvider rsaCryptoServiceProvider = new RSACryptoServiceProvider(numberOfBits);
            return rsaCryptoServiceProvider.ExportParameters(true);
        }
        
    }
}