using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using Extreme.Mathematics;
using BigInteger = System.Numerics.BigInteger;

namespace PNGAnalyzer
{
    public static class BigIntegerExtensions
    {
        // Random generator (thread safe)
        private static readonly ThreadLocal<RNGCryptoServiceProvider> ThreadGenerator =
            new ThreadLocal<RNGCryptoServiceProvider>(
                () => new RNGCryptoServiceProvider());

        // Random generator (thread safe)
        private static RNGCryptoServiceProvider Rng => ThreadGenerator.Value;

        public static BigInteger UnsignedFromBytes(byte[] bytes)
        {
            return new BigInteger(bytes.Append((byte) 0x00).ToArray());
        }

        public static byte[] UnsignedToBytes(BigInteger value)
        {
            if (value.Sign < 0)
                throw new ArgumentException("Only non-negative values allowed");
            byte[] bytes = value.ToByteArray();
            return ShouldTrimSign(bytes) ? bytes.Take(bytes.Length - 1).ToArray() : bytes;
        }

        private static bool ShouldTrimSign(byte[] bytes)
        {
            if (bytes.Length <= 1)
                return false;
            return bytes[bytes.Length - 1] == 0;
        }

        public static Extreme.Mathematics.BigInteger ToUnsignedExtremeMathematics(BigInteger value)
        {
            if (value.Sign < 0)
                throw new ArgumentException("Only non-negative values allowed");
            return new Extreme.Mathematics.BigInteger(value.ToByteArray());
        }

        public static BigInteger ToUnsignedBigInteger(Extreme.Mathematics.BigInteger value)
        {
            if (value.Sign < 0)
                throw new ArgumentException("Only non-negative values allowed");
            return new BigInteger(value.ToByteArray());
        }

        public static BigInteger ModularInverse(this BigInteger value, BigInteger modulus)
        {
            Extreme.Mathematics.BigInteger emValue = ToUnsignedExtremeMathematics(value);
            Extreme.Mathematics.BigInteger emModulus = ToUnsignedExtremeMathematics(modulus);
            Extreme.Mathematics.BigInteger result = Extreme.Mathematics.BigInteger.ModularInverse(emValue, emModulus);
            return ToUnsignedBigInteger(result);
        }

        public static BigInteger LeastCommonMultiple(this BigInteger a, BigInteger b)
        {
            Extreme.Mathematics.BigInteger emA = ToUnsignedExtremeMathematics(a);
            Extreme.Mathematics.BigInteger emB = ToUnsignedExtremeMathematics(b);
            Extreme.Mathematics.BigInteger result = IntegerMath.LeastCommonMultiple(emA, emB);
            return ToUnsignedBigInteger(result);
        }

        public static bool IsProbablyPrime(this BigInteger value, int witnesses = 10)
        {
            if (value <= 1 || value.IsEven)
                return false;

            if (witnesses <= 0)
                witnesses = 10;

            BigInteger d = value - 1;
            int s = 0;

            while (d % 2 == 0)
            {
                d /= 2;
                s += 1;
            }

            Byte[] bytes = new Byte[value.ToByteArray().LongLength];
            BigInteger a;

            for (int i = 0; i < witnesses; i++)
            {
                do
                {
                    Rng.GetBytes(bytes);

                    a = new BigInteger(bytes);
                } while (a < 2 || a >= value - 2);

                BigInteger x = BigInteger.ModPow(a, d, value);
                if (x == 1 || x == value - 1)
                    continue;

                for (int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, value);

                    if (x == 1)
                        return false;
                    if (x == value - 1)
                        break;
                }

                if (x != value - 1)
                    return false;
            }

            return true;
        }

        public static BigInteger GreatestCommonDivisor(this BigInteger a, BigInteger b)
        {
            Extreme.Mathematics.BigInteger emA = ToUnsignedExtremeMathematics(a);
            Extreme.Mathematics.BigInteger emB = ToUnsignedExtremeMathematics(b);
            Extreme.Mathematics.BigInteger result = IntegerMath.GreatestCommonDivisor(emA, emB);
            return ToUnsignedBigInteger(result);
        }

        public static BigInteger Random(int numberOfBits)
        {
            byte[] bytes = new byte[numberOfBits / 8];
            Rng.GetBytes(bytes);
            bytes[bytes.Length - 1] |= 0b10000000; // Ensures numberOfBits length
            return UnsignedFromBytes(bytes);
        }
    }
}