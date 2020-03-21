using NUnit.Framework;
using PNGAnalyzer;

namespace PNGAnalyzerTests
{
    [TestFixture]
    public class tRNSTests
    {
        [Test]
        public void ShouldReadstRNS()
        {
            string filePath = @"../../../Data/Plan.png";
            tRNS trns = new tRNS(PNGReader.Read(filePath)[1]);
            Assert.AreEqual("tRNS", trns.Type);
        }
    }
}