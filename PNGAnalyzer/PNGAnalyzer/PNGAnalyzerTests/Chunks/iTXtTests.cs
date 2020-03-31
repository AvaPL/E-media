using NUnit.Framework;
using PNGAnalyzer;

namespace PNGAnalyzerTests
{
    [TestFixture]
    public class iTXtTests
    {
        [Test]
        public void ShouldReadiTXt()
        {
            string filePath = @"../../../Data/itxt.png";
            iTXt itxt = new iTXt(PNGFile.Read(filePath)[8]);
            Assert.AreEqual("Author", itxt.Keyword);
            Assert.AreEqual(0, itxt.CompressionFlag);
            Assert.AreEqual(0, itxt.CompressionMethod);
            Assert.AreEqual("fr", itxt.LanguageTag);
            Assert.AreEqual("Auteur", itxt.TranslatedKeyword);
            Assert.AreEqual("La plume de ma tante", itxt.Text);
        }

        [Test]
        public void ShouldDecompressText()
        {
            string filePath = @"../../../Data/itxt.png";
            iTXt itxt = new iTXt(PNGFile.Read(filePath)[PNGFile.Read(filePath).Count - 2]);
            Assert.AreEqual("Warning", itxt.Keyword);
            Assert.AreEqual(1, itxt.CompressionFlag);
            Assert.AreEqual(
                "Es is verboten, um diese Datei in das GIF-Bildformat\numzuwandeln.  Sie sind gevarnt worden.",
                itxt.Text);
        }
    }
}