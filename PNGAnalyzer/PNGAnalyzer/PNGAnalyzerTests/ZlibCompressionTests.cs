using System.Linq;
using NUnit.Framework;
using PNGAnalyzer;

namespace PNGAnalyzerTests
{
    [TestFixture]
    public class ZlibCompressionTests
    {
        private static void CompressAndDecompress(byte[] bytes)
        {
            byte[] compressed = ZlibCompression.Compress(bytes);
            byte[] decompressed = ZlibCompression.Decompress(compressed);
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

        [Test]
        public void ShouldCompressAndDecompressLargeArray()
        {
            byte[] bytes = Enumerable.Range(0, short.MaxValue * 5).Select(i => (byte) i).ToArray();
            CompressAndDecompress(bytes);
        }
    }
}