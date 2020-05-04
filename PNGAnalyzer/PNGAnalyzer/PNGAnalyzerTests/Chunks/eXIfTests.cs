using System.Collections.Generic;
using NUnit.Framework;
using PNGAnalyzer;

namespace PNGAnalyzerTests
{
    [TestFixture]
    public class eXIfTests
    {
        [Test]
        public void ShouldReadeXIf()
        {
            string filePath = @"../../../Data/kostkiExif.png";
            List<Chunk> chunks = PNGFile.Read(filePath);
            eXIf exif = new eXIf(chunks[chunks.Count - 2]);
            Assert.AreEqual("eXIf", exif.Type);
        }

        [Test]
        public void ShouldContainCorrectEndianFlag()
        {
            string filePath = @"../../../Data/kostkiExif.png";
            List<Chunk> chunks = PNGFile.Read(filePath);
            eXIf exif = new eXIf(chunks[chunks.Count - 2]);
            Assert.AreEqual("MM", exif.EndianFlag);
        }

        [Test]
        public void ShouldReadIFD()
        {
            string filePath = @"../../../Data/kostkiExif.png";
            List<Chunk> chunks = PNGFile.Read(filePath);
            eXIf exif = new eXIf(chunks[chunks.Count - 2]);
        }
    }
}