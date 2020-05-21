using System.Linq;
using System.Numerics;
using System.Security.Cryptography;

namespace PNGAnalyzer.RSA
{
    public class MyRSA : IRSA
    {
        public MyRSA(int numberOfBits)
        {
            parameters = GenerateKeyPair(numberOfBits);
        }

        public MyRSA(RSAParameters parameters)
        {
            this.parameters = parameters;
        }

        private RSAParameters parameters;

        public byte[] Encrypt(byte[] data)
        {
            if (data.Length == 0)
                return data;

            BigInteger exponent = BigIntegerExtensions.UnsignedFromBytes(parameters.Exponent);
            return PadWithZeroes(ModPow(data, exponent)); // Pad in case result is shorter than key
        }

        private byte[] PadWithZeroes(byte[] bytes)
        {
            int targetLength = parameters.Modulus.Length;
            if (bytes.Length == targetLength) return bytes;
            byte[] result = new byte[targetLength];
            bytes.CopyTo(result, 0);
            new byte[targetLength - bytes.Length].CopyTo(result, bytes.Length);
            return result;
        }

        public byte[] Decrypt(byte[] data)
        {
            if (data.Length == 0)
                return data;

            BigInteger d = BigIntegerExtensions.UnsignedFromBytes(parameters.D);
            return ModPow(data, d);
        }

        public void ImportParameters(RSAParameters parameters)
        {
            this.parameters = parameters;
        }

        public RSAParameters ExportParameters()
        {
            return parameters;
        }

        RSAParameters IRSA.GenerateKeyPair(int numberOfBits)
        {
            return GenerateKeyPair(numberOfBits);
        }

        private byte[] ModPow(byte[] data, BigInteger exponent)
        {
            BigInteger modulus = BigIntegerExtensions.UnsignedFromBytes(parameters.Modulus);
            BigInteger dataBigInteger = BigIntegerExtensions.UnsignedFromBytes(data);
            BigInteger result = BigInteger.ModPow(dataBigInteger, exponent, modulus);
            return BigIntegerExtensions.UnsignedToBytes(result);
        }

        public static RSAParameters GenerateKeyPair(int numberOfBits)
        {
            BigInteger primeMin = new BigInteger(6074001000)
                                  << (numberOfBits / 2 - 33); // primeMin ≈ √2 × 2^(bits - 1), insures key length
            BigInteger p = RandomPrimeAbove(primeMin, numberOfBits / 2);
            BigInteger q = RandomPrimeAbove(primeMin, numberOfBits / 2);
            BigInteger n = p * q; // Modulus
            BigInteger lcm = (p - 1).LeastCommonMultiple(q - 1);
            BigInteger e = RandomCoprimeBelow(lcm, numberOfBits / 2); // Exponent
            BigInteger d = e.ModularInverse(lcm);

            return new RSAParameters
            {
                D = ToByteArrayWithoutSign(d, numberOfBits),
                Exponent = ToByteArrayWithoutSign(e, numberOfBits),
                Modulus = ToByteArrayWithoutSign(n, numberOfBits),
                P = ToByteArrayWithoutSign(p, numberOfBits),
                Q = ToByteArrayWithoutSign(q, numberOfBits)
            };
        }

        private static byte[] ToByteArrayWithoutSign(BigInteger d, int numberOfBits)
        {
            byte[] bytes = d.ToByteArray();
            return bytes.Length > numberOfBits / 8 ? bytes.Take(bytes.Length - 1).ToArray() : bytes;
        }

        private static BigInteger RandomPrimeAbove(BigInteger bound, int numberOfBits)
        {
            BigInteger result;
            do
            {
                result = RandomPrime(numberOfBits);
            } while (result <= bound);

            return result;
        }

        private static BigInteger RandomPrime(int numberOfBits)
        {
            BigInteger result;
            do
            {
                result = BigIntegerExtensions.Random(numberOfBits);
            } while (!result.IsProbablyPrime(256));

            return result;
        }

        private static BigInteger RandomCoprimeBelow(BigInteger bound, int numberOfBits)
        {
            BigInteger result;
            do
            {
                result = RandomPrimeBelow(bound, numberOfBits);
            } while (result.GreatestCommonDivisor(bound) != 1);

            return result;
        }

        private static BigInteger RandomPrimeBelow(BigInteger bound, int numberOfBits)
        {
            BigInteger result;
            do
            {
                result = RandomPrime(numberOfBits);
            } while (result >= bound);

            return result;
        }
    }
}