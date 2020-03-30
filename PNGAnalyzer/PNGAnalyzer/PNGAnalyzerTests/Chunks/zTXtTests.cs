using NUnit.Framework;
using PNGAnalyzer;

namespace PNGAnalyzerTests
{
    [TestFixture]
    public class zTXtTests
    {
        [Test]
        public void ShouldReadszTXT()
        {
            string filePath = @"../../../Data/czerwony.png";
            zTXt ztxt = new zTXt(PNGFile.Read(filePath)[2]);
            Assert.AreEqual("zTXt", ztxt.Type);
        }
        
        [Test]
        public void ShouldContainCorrectData()
        {
            string filePath = @"../../../Data/czerwony.png";
            zTXt ztxt = new zTXt(PNGFile.Read(filePath)[2]);
            Assert.AreEqual("Raw profile type APP1", ztxt.Keyword);
        }
    }
}