using NUnit.Framework;
using PNGAnalyzer;

namespace PNGAnalyzerTests
{
    [TestFixture]
    public class tEXtTests
    {
        [Test]
        public void ShouldReadtEXtFromLena()
        {
            string filePath = @"../../../Data/lena2.png";
            tEXt text = new tEXt(PNGReader.Read(filePath)[5]);
            Assert.AreEqual("tEXt", text.Type);
            Assert.AreEqual("date:create", text.Keyword);
        }
        
        [Test]
        public void ShouldReadtEXtFromItxt()
        {
            string filePath = @"../../../Data/itxt.png";
            tEXt text = new tEXt(PNGReader.Read(filePath)[7]);
            Assert.AreEqual("tEXt", text.Type);
            Assert.AreEqual("Title", text.Keyword);
            Assert.AreEqual("PNG", text.Text);
        }
    }
}