using System.Linq;
using NUnit.Framework;
using PNGAnalyzer;

namespace PNGAnalyzerTests
{
    [TestFixture]
    public class PNGFilteringTests
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void ShouldEncodeAndDecodeCorrectlyGrayscale(int type)
        {
            byte[] data = {(byte)type, 12, 32, 12, 32, (byte)type, 34, 14, 34, 0 };
            byte[] encodedData = PNGFiltering.EncodeImage(data, 4,1);
            byte[] decodedData = PNGFiltering.DecodeImage(encodedData, 4, 1);
            Assert.AreEqual(data,decodedData);
        }
        
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void ShouldEncodeAndDecodeCorrectlyTrueColor(int type)
        {
            byte[] data = {(byte)type, 12, 32, 12, 32, 19, 9, (byte)type, 34, 14, 34, 12, 12, 19 };
            byte[] encodedData = PNGFiltering.EncodeImage(data, 6,3);
            byte[] decodedData = PNGFiltering.DecodeImage(encodedData, 6, 3);
            Assert.AreEqual(data,decodedData);
        }
    }
}