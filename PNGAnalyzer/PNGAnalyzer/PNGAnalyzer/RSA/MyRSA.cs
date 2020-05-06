using System;
using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography;

namespace PNGAnalyzer.RSA
{
    public class MyRSA : IRSA
    {
        public RSAParameters Parameters { get; private set; }

        public MyRSA(int numberOfBits)
        {
            Parameters = GenerateKeyPair(numberOfBits);
        }

        public MyRSA(RSAParameters parameters)
        {
            Parameters = parameters;
        }

        public byte[] Encrypt(byte[] data)
        {
            if (data.Length == 0)
                return data;

            BigInteger exponent = new BigInteger(Parameters.Exponent);
            BigInteger modulus = new BigInteger(Parameters.Modulus);
            return BigInteger.ModPow(new BigInteger(data), exponent, modulus).ToByteArray();
        }

        public byte[] Decrypt(byte[] data)
        {
            if (data.Length == 0)
                return data;

            BigInteger d = new BigInteger(Parameters.D);
            BigInteger modulus = new BigInteger(Parameters.Modulus);
            return BigInteger.ModPow(new BigInteger(data), d, modulus).ToByteArray();
        }

        public void ImportParameters(RSAParameters parameters)
        {
            Parameters = parameters;
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
            BigInteger inverseQ = q.ModularInverse(p);
            BigInteger n = p * q; // Modulus
            BigInteger lcm = (p - 1).LeastCommonMultiple(q - 1);
            BigInteger e = RandomCoprimeBelow(lcm, numberOfBits / 2); // Exponent
            BigInteger d = e.ModularInverse(lcm);
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
                result = BigIntegerExtensions.Random(numberOfBits);
            } while (!result.IsProbablyPrime(256));

            return result;
        }

        // private static BigInteger RandomBigInteger(int numberOfBits)
        // {
        //     try
        //     {
        //         return SafeRandomBigInteger(numberOfBits);
        //     }
        //     catch (IndexOutOfRangeException e)
        //     {
        //         // Should occur only in really rare cases on internal library
        //         Debug.WriteLine("IndexOutOfRangeException: internal library error");
        //         Debug.WriteLine(e);
        //         return RandomBigInteger(numberOfBits);
        //     }
        // }
        //
        // private static BigInteger SafeRandomBigInteger(int numberOfBits)
        // {
        //     RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        //     byte[] bytes = new byte[numberOfBits / 8];
        //     rng.GetBytes(bytes);
        //     return new BigInteger(1, bytes);
        // }

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