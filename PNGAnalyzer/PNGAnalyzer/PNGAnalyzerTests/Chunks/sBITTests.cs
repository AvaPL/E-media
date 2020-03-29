using NUnit.Framework;
using PNGAnalyzer;

namespace PNGAnalyzerTests
{
    [TestFixture]
    public class sBITTests
    {
        [Test]
        public void ShouldReadsBIT()
        {
            string filePath = @"../../../Data/lena.png";
            sBIT sbit = new sBIT(PNGReader.Read(filePath)[1]);
            Assert.AreEqual("sBIT", sbit.Type);
            Assert.AreEqual(8, sbit.SignificantBytes[0]);
            Assert.AreEqual(3, sbit.SignificantBytes.Length);
        }
    }
}