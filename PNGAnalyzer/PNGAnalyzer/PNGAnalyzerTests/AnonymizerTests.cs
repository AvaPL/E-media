using System.Collections.Generic;
using NUnit.Framework;
using PNGAnalyzer;

namespace PNGAnalyzerTests
{
    [TestFixture]
    public class AnonymizerTests
    {
        [Test]
        public void ShouldAnonymizePNGFile()
        {
            string filePathToRead = @"../../../Data/Plan.png";
            string filePathToWrite = @"../../../Data/PlanAnonymized.png";
            Anonymizer.Anonymize(filePathToRead, filePathToWrite);
        }
    }
}