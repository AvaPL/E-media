using NUnit.Framework;
using PNGAnalyzer;

namespace PNGAnalyzerTests
{
    [TestFixture]
    public class iTXtTests
    {
        [Test]
        public void ShoulReadiTXt()
        {
            string filePath = @"../../../Data/itxt.png";
            iTXt itxt = new iTXt(PNGFile.Read(filePath)[8]);
            Assert.AreEqual("Author", itxt.Keyword);
            Assert.AreEqual(0, itxt.CompressionFlag);
            Assert.AreEqual(0, itxt.CompressionMethod);
            Assert.AreEqual("fr", itxt.LanguageTag);
            Assert.AreEqual("Auteur", itxt.TranslatedKeyword);
            Assert.AreEqual("UncompressedText: La plume de ma tante", itxt.InternationalText.ToString());
        }
    }
}