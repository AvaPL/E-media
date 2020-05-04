using System.Security.Cryptography;
using Extreme.Mathematics;

namespace PNGAnalyzer.RSA
{
    public class MyRSA : IRSA
    {
        public byte[] Encrypt(byte[] data)
        {
            throw new System.NotImplementedException();
        }

        public byte[] Decrypt(byte[] data)
        {
            throw new System.NotImplementedException();
        }

        public void ImportParameters(RSAParameters parameters)
        {
            throw new System.NotImplementedException();
        }

        RSAParameters IRSA.GenerateKeyPair(int numberOfBits)
        {
            return GenerateKeyPair(numberOfBits);
        }

        public static RSAParameters GenerateKeyPair(int numberOfBits)
        {
            BigInteger
                primeMin = new BigInteger(6074001000) <<
                           (numberOfBits / 2 - 33); // primeMin ≈ √2 × 2^(bits - 1), assures key length
            BigInteger p = RandomPrimeAbove(primeMin, numberOfBits / 2);
            BigInteger q = RandomPrimeAbove(primeMin, numberOfBits / 2);
            BigInteger n = p * q; // Modulus
            BigInteger lcm = IntegerMath.LeastCommonMultiple(p - 1, q - 1);
            BigInteger e = RandomCoprimeBelow(lcm); // Exponent
            BigInteger d = BigInteger.ModularInverse(e, lcm);
            // Remaining in RSAParameters
            BigInteger dp = d % (p - 1);
            BigInteger dq = d % (q - 1);
            BigInteger inverseQ = BigInteger.ModularInverse(q, p);
            return new RSAParameters
            {
                D = d.ToByteArray(),
                DP = dp.ToByteArray(),
                DQ = dq.ToByteArray(),
                Exponent = e.ToByteArray(),
                InverseQ = inverseQ.ToByteArray(),
                Modulus = n.ToByteArray(),
                P = p.ToByteArray(),
                Q = q.ToByteArray()
            };
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
                result = RandomBigInteger(numberOfBits);
            } while (!result.IsPseudoPrime(256));

            return result;
        }

        private static BigInteger RandomBigInteger(int numberOfBits)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] bytes = new byte[numberOfBits / 8 + 1]; // Include sign bit
            rng.GetBytes(bytes);
            bytes[bytes.Length - 1] &= 0x7F; // Force sign bit to positive
            return new BigInteger(bytes);
        }

        private static BigInteger RandomCoprimeBelow(BigInteger bound)
        {
            BigInteger result;
            do
            {
                result = RandomPrimeBelow(bound);
            } while (bound % result == 0);

            return result;
        }

        private static BigInteger RandomPrimeBelow(BigInteger bound)
        {
            BigInteger result;
            do
            {
                result = RandomPrime(bound.BitCount);
            } while (result >= bound);

            return result;
        }
    }
}