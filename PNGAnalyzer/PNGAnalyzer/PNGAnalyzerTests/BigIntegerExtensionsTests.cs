using System;
using System.Numerics;
using NUnit.Framework;
using PNGAnalyzer;

namespace PNGAnalyzerTests
{
    [TestFixture]
    public class BigIntegerExtensionsTests
    {
        private static void AssertBigIntegerEqual(int integer, BigInteger bigInteger)
        {
            Assert.AreEqual(new BigInteger(integer), bigInteger);
        }

        private static void AssertBigIntegerEqual(long integer, BigInteger bigInteger)
        {
            Assert.AreEqual(new BigInteger(integer), bigInteger);
        }

        [Test]
        public void ShouldCreateUnsignedBigIntegerFromPositiveByte()
        {
            byte[] bytes = {127};
            BigInteger result = BigIntegerExtensions.UnsignedFromBytes(bytes);
            AssertBigIntegerEqual(127, result);
        }

        [Test]
        public void ShouldCreateUnsignedBigIntegerFromNegativeByte()
        {
            byte[] bytes = {255}; // 255 unsigned, -1 signed
            BigInteger result = BigIntegerExtensions.UnsignedFromBytes(bytes);
            AssertBigIntegerEqual(255, result);
        }

        [Test]
        public void ShouldCreateUnsignedBigIntegerFrom3PositiveBytes()
        {
            byte[] bytes = {0xFF, 0xFF, 0x7F}; // 8388607 (little endian)
            BigInteger result = BigIntegerExtensions.UnsignedFromBytes(bytes);
            AssertBigIntegerEqual(8388607, result);
        }

        [Test]
        public void ShouldCreateUnsignedBigIntegerFrom3NegativeBytes()
        {
            byte[] bytes = {0xFF, 0xFF, 0xFF}; // 16777215 unsigned, -1 signed (little endian)
            BigInteger result = BigIntegerExtensions.UnsignedFromBytes(bytes);
            AssertBigIntegerEqual(16777215, result);
        }

        [Test]
        public void ShouldCreateUnsignedBigIntegerFrom4PositiveBytes()
        {
            byte[] bytes = {0xFF, 0xFF, 0xFF, 0x7F}; // 2147483647 (little endian)
            BigInteger result = BigIntegerExtensions.UnsignedFromBytes(bytes);
            AssertBigIntegerEqual(2147483647, result);
        }

        [Test]
        public void ShouldCreateUnsignedBigIntegerFrom4NegativeBytes()
        {
            byte[] bytes = {0xFF, 0xFF, 0xFF, 0xFF}; // 4294967295 unsigned, -1 signed (little endian)
            BigInteger result = BigIntegerExtensions.UnsignedFromBytes(bytes);
            AssertBigIntegerEqual(4294967295L, result);
        }

        [Test]
        public void ShouldConvertValuesToExtremeMathematics()
        {
            byte[][] testBytes =
            {
                // Little endian
                new byte[] {127},
                new byte[] {255},
                new byte[] {0xFF, 0xFF, 0x7F},
                new byte[] {0xFF, 0xFF, 0xFF},
                new byte[] {0xFF, 0xFF, 0xFF, 0x7F},
                new byte[] {0xFF, 0xFF, 0xFF, 0xFF}
            };
            foreach (var bytes in testBytes)
            {
                BigInteger value = new BigInteger(bytes);
                if (value.Sign < 0)
                    Assert.Throws<ArgumentException>(() => BigIntegerExtensions.ToUnsignedExtremeMathematics(value));
                else
                    Assert.AreEqual(value.ToString(),
                        BigIntegerExtensions.ToUnsignedExtremeMathematics(value).ToString());
            }
        }

        [Test]
        public void ShouldConvertValuesToBigInteger()
        {
            byte[][] testBytes =
            {
                // Little endian
                new byte[] {127},
                new byte[] {255},
                new byte[] {0xFF, 0xFF, 0x7F},
                new byte[] {0xFF, 0xFF, 0xFF},
                new byte[] {0xFF, 0xFF, 0xFF, 0x7F},
                new byte[] {0xFF, 0xFF, 0xFF, 0xFF}
            };
            foreach (var bytes in testBytes)
            {
                Extreme.Mathematics.BigInteger value = new Extreme.Mathematics.BigInteger(bytes);
                if (value.Sign < 0)
                    Assert.Throws<ArgumentException>(() => BigIntegerExtensions.ToUnsignedBigInteger(value));
                else
                    Assert.AreEqual(value.ToString(), BigIntegerExtensions.ToUnsignedBigInteger(value).ToString());
            }
        }

        [Test]
        public void ShouldConvertPreviouslyNegativeValueToBigInteger()
        {
            Extreme.Mathematics.BigInteger value = new Extreme.Mathematics.BigInteger(-12345);
            value += 12345 * 2;
            Assert.AreEqual(new BigInteger(12345), BigIntegerExtensions.ToUnsignedBigInteger(value));
        }

        [Test]
        public void ShouldConvertBigIntegerWithSignByteToUnsignedBytes()
        {
            BigInteger[] testValues =
            {
                128,
                170,
                255,
                32768,
                33940,
                65535,
                4294967295
            };
            byte[][] expectedValues =
            {
                // Little endian
                new byte[] {128},
                new byte[] {170},
                new byte[] {255},
                new byte[] {0x00, 0x80},
                new byte[] {0x94, 0x84},
                new byte[] {0xFF, 0xFF},
                new byte[] {0xFF, 0xFF, 0xFF, 0xFF}
            };
            for (int i = 0; i < testValues.Length; i++)
                Assert.AreEqual(expectedValues[i], BigIntegerExtensions.UnsignedToBytes(testValues[i]));
        }

        [Test]
        public void ShouldCalculateModularInverse()
        {
            AssertBigIntegerEqual(21, new BigInteger(7311).ModularInverse(26));
            AssertBigIntegerEqual(111, new BigInteger(5445).ModularInverse(322));
            AssertBigIntegerEqual(112181, new BigInteger(455345).ModularInverse(235453));
            AssertBigIntegerEqual(6170, new BigInteger(456482).ModularInverse(12357));
            AssertBigIntegerEqual(37, new BigInteger(97531).ModularInverse(213));
        }

        [Test]
        public void ShouldThrowForModularInverseOnNonCoprimeValues()
        {
            Assert.Throws<ArithmeticException>(() => new BigInteger(51).ModularInverse(17));
        }

        [Test]
        public void ShouldCalculateLeastCommonMultiple()
        {
            AssertBigIntegerEqual(48, new BigInteger(48).LeastCommonMultiple(12));
            AssertBigIntegerEqual(48, new BigInteger(12).LeastCommonMultiple(48));
            AssertBigIntegerEqual(1813260, new BigInteger(564).LeastCommonMultiple(3215));
            AssertBigIntegerEqual(120498, new BigInteger(453).LeastCommonMultiple(798));
            AssertBigIntegerEqual(21115152, new BigInteger(47664).LeastCommonMultiple(886));
            AssertBigIntegerEqual(20501736, new BigInteger(37896).LeastCommonMultiple(541));
        }

        [Test]
        public void ShouldDetectPrimeNumbers()
        {
            int baseValue = 256;
            Assert.IsTrue(new BigInteger(15737).IsProbablyPrime(baseValue));
            Assert.IsTrue(new BigInteger(546684643).IsProbablyPrime(baseValue));
            Assert.IsTrue(new BigInteger(154161135846359).IsProbablyPrime(baseValue));
            Assert.IsFalse(new BigInteger(143654677).IsProbablyPrime(baseValue));
            Assert.IsFalse(new BigInteger(3468673).IsProbablyPrime(baseValue));
            Assert.IsFalse(new BigInteger(1347899).IsProbablyPrime(baseValue));
        }

        [Test]
        public void ShouldGiveCorrectGreatestCommonDivisors()
        {
            AssertBigIntegerEqual(67, new BigInteger(134).GreatestCommonDivisor(67));
            AssertBigIntegerEqual(67, new BigInteger(67).GreatestCommonDivisor(134));
            AssertBigIntegerEqual(342, new BigInteger(4176846).GreatestCommonDivisor(46854684));
            AssertBigIntegerEqual(4, new BigInteger(534645664).GreatestCommonDivisor(46436));
            AssertBigIntegerEqual(67, new BigInteger(413486748).GreatestCommonDivisor(13315513));
            AssertBigIntegerEqual(1, new BigInteger(13584686).GreatestCommonDivisor(5165617));
        }

        [Test]
        public void ShouldGenerateUnsignedRandomBigIntegers()
        {
            int numberOfBits = 512;
            for (int i = 0; i < 1000; i++)
            {
                BigInteger random = BigIntegerExtensions.Random(numberOfBits);
                int arrayLength = random.ToByteArray().Length * 8;
                Assert.AreEqual(numberOfBits + 8, arrayLength); // Include sign bit for 512 bit values
                Assert.AreEqual(1, random.Sign);
            }
        }
    }
}