using NUnit.Framework;
using PNGAnalyzer;

namespace PNGAnalyzerTests
{
    [TestFixture]
    public class iCCPTests
    {
        [Test]
        public void ShouldReadiCCP()
        {
            string filePath = @"../../../Data/lena2.png";
            iCCP iccp = new iCCP(PNGFile.Read(filePath)[1]);
            Assert.AreEqual("icc", iccp.ProfileName);
            Assert.AreEqual(0, iccp.CompressionMethod);
        }
    }
}