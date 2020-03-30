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
            string filePath = @"../../../Data/Plan.png";
            IDAT idat = new IDAT(PNGFile.Read(filePath)[4]);
            Assert.AreEqual("IDAT", idat.Type);
        }
        
        [Test]
        public void ShouldAddIDATs()
        {
            byte[] data1 = {1,2,3,4};
            byte[] data2 = {5,6,7,8};
            IDAT idat1 = new IDAT("IDAT", data1, 1 );
            IDAT idat2 = new IDAT("IDAT", data2, 2 );
            IDAT idat = idat1 + idat2;
            byte[] expectedResult = {1,2,3,4,5,6,7,8};
            Assert.AreEqual(expectedResult, idat.Data);
        }
    }
}