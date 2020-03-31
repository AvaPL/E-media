using NUnit.Framework;
using PNGAnalyzer;

namespace PNGAnalyzerTests
{
    [TestFixture]
    public class zTXtTests
    {
        [Test]
        public void ShouldReadzTXT()
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

        [Test]
        public void ShouldContainDecompressedText()
        {
            string filePath = @"../../../Data/czerwony.png";
            zTXt ztxt = new zTXt(PNGFile.Read(filePath)[2]);
            Assert.AreEqual(
                "\ngeneric profile\n      34\n49492a0008000000010031010200070000001a00000000000000476f6f676c650000\n",
                ztxt.Text);
        }
    }
}