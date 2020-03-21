using NUnit.Framework;
using PNGAnalyzer;

namespace PNGAnalyzerTests
{
    public class bKGDTests
    {
        [Test]
        public void ShouldReadbKGD()
        {
            string filePath = @"../../../Data/Ruch_wirowy.png";
            bKGD bkgd = new bKGD(PNGReader.Read(filePath)[1]);
            Assert.AreEqual("Red: 255\nGreen: 255\nBlue: 255", bkgd.BackgroundColor.ToString());
        }
    }
}