using System;
using System.Collections.Generic;
using NUnit.Framework;
using PNGAnalyzer;

namespace PNGAnalyzerTests
{

    [TestFixture]
    public class PNGFileTests
    {
        [Test]
        public void ShouldReadPNGFile()
        {
            string PNGfilePath = @"../../../Data/Plan.png";
            Assert.DoesNotThrow(() => PNGFile.Read(PNGfilePath));
        }
        
        [Test]
        public void ShouldThrowExceptionGivenNotPNGFile()
        {
            string JPEGfilePath = @"../../../Data/Kleo.jpg";
            Assert.Throws<FormatException>(() => PNGFile.Read(JPEGfilePath));
        }

        [Test]
        public void ShouldReadIHDRChunk()
        {
            string filePath = @"../../../Data/Plan.png";
            Assert.AreEqual("IHDR", PNGFile.Read(filePath)[0].Type);
        }

        [Test]
        public void ShouldWritePNGFile()
        {
            string filePath = @"../../../Data/Plan.png";
            List<Chunk> chunks = PNGFile.Read(filePath);
            string filePathToWrite = @"../../../Data/PlanSaved.png";
            PNGFile.Write(filePathToWrite, chunks);
        }
    }
}