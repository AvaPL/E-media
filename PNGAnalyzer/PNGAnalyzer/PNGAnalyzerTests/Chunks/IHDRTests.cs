using NUnit.Framework;
using PNGAnalyzer;

namespace PNGAnalyzerTests
{
    [TestFixture]
    public class IHDRTests
    {
        [Test]
        public void ShouldReadIHDR()
        {
            string filePath = @"../../../Data/Plan.png";
            IHDR ihdr = new IHDR(PNGFile.Read(filePath)[0]);
            Assert.AreEqual("IHDR", ihdr.Type);
            Assert.AreEqual(1438, ihdr.Width);
            Assert.AreEqual(640, ihdr.Height);
            Assert.AreEqual(8, ihdr.BitDepth);
            Assert.AreEqual(6, ihdr.ColorType);
            Assert.AreEqual(0, ihdr.CompressionMethod);
            Assert.AreEqual(0, ihdr.FilterMethod);
            Assert.AreEqual(0, ihdr.InterlaceMethod);
        }
    }
}