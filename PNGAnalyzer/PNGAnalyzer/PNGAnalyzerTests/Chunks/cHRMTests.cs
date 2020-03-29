using NUnit.Framework;
using PNGAnalyzer;

namespace PNGAnalyzerTests
{
    [TestFixture]
    public class cHRMTests
    {
        [Test]
        public void ShouldReadcHRM()
        {
            string filePath = @"../../../Data/kostki.png";
            cHRM chrm = new cHRM(PNGReader.Read(filePath)[2]);
            Assert.AreEqual("cHRM", chrm.Type);
            Assert.AreEqual(31270, chrm.WhitePointX);
            Assert.AreEqual(32900, chrm.WhitePointY);
            Assert.AreEqual(64000, chrm.RedX);
            Assert.AreEqual(33000, chrm.RedY);
            Assert.AreEqual(30000, chrm.GreenX);
            Assert.AreEqual(60000, chrm.GreenY);
            Assert.AreEqual(15000, chrm.BlueX);
            Assert.AreEqual(6000, chrm.BlueY);
        }
    }
}