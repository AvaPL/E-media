using System;
using NUnit.Framework;
using PNGAnalyzer;

namespace PNGAnalyzerTests
{

    [TestFixture]
    public class PNGReaderTests
    {
        [Test]
        public void ShouldReadPNGFile()
        {
            string PNGfilePath = @"../../Data/Plan.png";
            Assert.DoesNotThrow(() => PNGReader.Read(PNGfilePath));
        }
        
        [Test]
        public void ShouldThrowExceptionGivenNotPNGFile()
        {
            string JPEGfilePath = @"../../Data/Kleo.jpg";
            Assert.Throws<FormatException>(() => PNGReader.Read(JPEGfilePath));
        }

        [Test]
        public void ShouldReadIHDRChunk()
        {
            string filePath = @"../../Data/Plan.png";
            Assert.AreEqual("IHDR", PNGReader.Read(filePath)[0].Type);
        }
    }
}