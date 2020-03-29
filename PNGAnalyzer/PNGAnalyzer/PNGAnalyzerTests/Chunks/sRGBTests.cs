using NUnit.Framework;
using PNGAnalyzer;

namespace PNGAnalyzerTests
{
    [TestFixture]
    public class sRGBTests
    {
        [Test]
        public void ShouldReadsRGB()
        {
            string filePath = @"../../../Data/Plan.png";
            sRGB srgb = new sRGB(PNGReader.Read(filePath)[1]);
            Assert.AreEqual("sRGB", srgb.Type);
        }
        
        [Test]
        public void ShouldContainCorrectData()
        {
            string filePath = @"../../../Data/Plan.png";
            sRGB srgb = new sRGB(PNGReader.Read(filePath)[1]);
            Assert.AreEqual(0, srgb.RenderingIntent);
        }
    }
}