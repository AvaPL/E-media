using System.Linq;
using NUnit.Framework;
using PNGAnalyzer;

namespace PNGAnalyzerTests
{
    [TestFixture]
    public class IENDTests
    {
        [Test]
        public void ShouldReadIEND()
        {
            string filePath = @"../../../Data/przekladnia.png";
            IEND iend = new IEND(PNGReader.Read(filePath).Last());
            Assert.AreEqual("IEND", iend.Type);
        }
    }
}