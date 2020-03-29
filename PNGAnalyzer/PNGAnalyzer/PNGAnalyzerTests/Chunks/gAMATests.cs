using NUnit.Framework;
using PNGAnalyzer;

namespace PNGAnalyzerTests
{
    [TestFixture]
    public class gAMATests
    {
        [Test]
        public void ShouldReadgAMA()
        {
            string filePath = @"../../../Data/kostki.png";
            gAMA gama = new gAMA(PNGReader.Read(filePath)[1]);
            Assert.AreEqual("gAMA", gama.Type);
        }

        [Test]
        public void ShouldContainCorrectData()
        {
            string filePath = @"../../../Data/kostki.png";
            gAMA gama = new gAMA(PNGReader.Read(filePath)[1]);
            Assert.AreEqual(45455 , gama.Gamma);
        }
    }
}