using NUnit.Framework;
using PNGAnalyzer;

namespace PNGAnalyzerTests
{
    [TestFixture]
    public class tIMETests
    {
        [Test]
        public void ShouldReadtIME()
        {
            string filePath = @"../../../Data/przekladnia.png";
            tIME time = new tIME(PNGReader.Read(filePath)[2]);
            Assert.AreEqual("tIME", time.Type);
        }

        [Test]
        public void ShouldContainCorrectDate()
        {
            string filePath = @"../../../Data/przekladnia.png";
            tIME time = new tIME(PNGReader.Read(filePath)[2]);
            Assert.AreEqual(2009, time.LatestModificationDate.Year);
            Assert.AreEqual(7, time.LatestModificationDate.Month);
            Assert.AreEqual(24, time.LatestModificationDate.Day);
        }
    }
}