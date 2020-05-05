using System;
using System.Diagnostics;
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
            try
            {
                return SafeGenerateKeyPair(numberOfBits);
            }
            catch (ArithmeticException e)
            {
                // Should occur only in really rare cases when p and q aren't coprime,
                // which means they aren't prime anyway.
                Debug.WriteLine("ArithmeticException: probably p and q were not coprime");
                Debug.WriteLine(e);
                return GenerateKeyPair(numberOfBits);
            }
        }

        private static RSAParameters SafeGenerateKeyPair(int numberOfBits)
        {
            BigInteger primeMin = new BigInteger(6074001000)
                                  << (numberOfBits / 2 - 33); // primeMin ≈ √2 × 2^(bits - 1), assures key length
            BigInteger p = RandomPrimeAbove(primeMin, numberOfBits / 2);
            BigInteger q = RandomPrimeAbove(primeMin, numberOfBits / 2);
            BigInteger inverseQ = BigInteger.ModularInverse(q, p);
            BigInteger n = p * q; // Modulus
            BigInteger lcm = IntegerMath.LeastCommonMultiple(p - 1, q - 1);
            BigInteger e = RandomCoprimeBelow(lcm, numberOfBits / 2); // Exponent
            BigInteger d = BigInteger.ModularInverse(e, lcm);
            // Remaining in RSAParameters
            BigInteger dp = d % (p - 1);
            BigInteger dq = d % (q - 1);

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
            try
            {
                return SafeRandomBigInteger(numberOfBits);
            }
            catch (IndexOutOfRangeException e)
            {
                // Should occur only in really rare cases on internal library
                Debug.WriteLine("IndexOutOfRangeException: internal library error");
                Debug.WriteLine(e);
                return RandomBigInteger(numberOfBits);
            }
        }

        private static BigInteger SafeRandomBigInteger(int numberOfBits)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] bytes = new byte[numberOfBits / 8];
            rng.GetBytes(bytes);
            return new BigInteger(1, bytes);
        }

        private static BigInteger RandomCoprimeBelow(BigInteger bound, int numberOfBits)
        {
            BigInteger result;
            do
            {
                result = RandomPrimeBelow(bound, numberOfBits);
            } while (IntegerMath.GreatestCommonDivisor(result, bound) != 1);

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