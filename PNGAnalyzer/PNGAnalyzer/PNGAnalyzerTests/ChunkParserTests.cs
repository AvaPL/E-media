using System.Collections.Generic;
using NUnit.Framework;
using PNGAnalyzer;

namespace PNGAnalyzerTests
{
    [TestFixture]
    public class ChunkParserTests
    {
        [Test]
        public void ShouldParseChunks()
        {
            string filePath = @"../../../Data/kostki.png";
            List<Chunk> chunks = PNGReader.Read(filePath);
            List<Chunk> result = ChunkParser.Parse(chunks);
            cHRM expected = new cHRM(PNGReader.Read(filePath)[2]);
            cHRM chrm = (cHRM) result[2];
            Assert.AreEqual(expected.WhitePointX, chrm.WhitePointX);
        }
    }
}