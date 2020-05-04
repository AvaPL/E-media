using NUnit.Framework;
using PNGAnalyzer;

namespace PNGAnalyzerTests
{
    [TestFixture]
    public class GZipCompressionTests
    {
        private static void CompressAndDecompress(byte[] bytes)
        {
            byte[] compressed = GZipCompression.Compress(bytes);
            byte[] decompressed = GZipCompression.Decompress(compressed);
            Assert.AreEqual(bytes, decompressed);
        }

        [Test]
        public void ShouldCompressAndDecompressEmptyArray()
        {
            byte[] bytes = { };
            CompressAndDecompress(bytes);
        }

        [Test]
        public void ShouldCompressAndDecompressMultipleBytes()
        {
            byte[] bytes = {1, 3, 5, 3, 7};
            CompressAndDecompress(bytes);
        }

        [Test]
        public void ShouldCompressAndDecompressOneByte()
        {
            byte[] bytes = {3};
            CompressAndDecompress(bytes);
        }
    }
}