using System.Collections.Generic;
using NUnit.Framework;
using PNGAnalyzer;

namespace PNGAnalyzerTests
{
    [TestFixture]
    public class AnonymizerTests
    {
        // [Test]
        // public void ShouldAnonymizeFile()
        // {
        //     string filePath = @"../../../Data/kostki.png";
        //     List<Chunk> anonymized = Anonymizer.Anonymize(filePath);
        //     Assert.AreEqual("IHDR", anonymized[0].Type);
        //     Assert.AreEqual("IDAT", anonymized[1].Type);
        //     Assert.AreEqual("IDAT", anonymized[2].Type);
        //     Assert.AreEqual("IEND", anonymized[3].Type);
        // }
        
        [Test]
        public void ShouldAnonymizePNGFile()
        {
            string filePathToRead = @"../../../Data/Plan.png";
            string filePathToWrite = @"../../../Data/PlanAnonymized.png";
            Anonymizer.Anonymize(filePathToRead, filePathToWrite);
        }
    }
}