using NUnit.Framework;
using PNGAnalyzer;

namespace PNGAnalyzerTests
{
    [TestFixture]
    public class IDATTests
    {
        [Test]
        public void ShouldReadIDAT()
        {
            string filePath = @"../../Data/Plan.png";
            IDAT idat = new IDAT(PNGReader.Read(filePath)[4]);
            Assert.AreEqual("IDAT", idat.Type);
        }
    }
}