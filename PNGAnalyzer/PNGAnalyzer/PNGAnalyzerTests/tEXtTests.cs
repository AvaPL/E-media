using NUnit.Framework;
using PNGAnalyzer;

namespace PNGAnalyzerTests
{
    [TestFixture]
    public class tEXtTests
    {
        [Test]
        public void ShouldReadtEXt()
        {
            string filePath = @"../../../Data/lena2.png";
            tEXt text = new tEXt(PNGReader.Read(filePath)[5]);
            Assert.AreEqual("tEXt", text.Type);
            Assert.AreEqual("date:create", text.Keyword);
        }
    }
}