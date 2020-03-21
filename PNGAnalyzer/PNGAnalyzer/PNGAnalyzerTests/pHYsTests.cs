using NUnit.Framework;
using PNGAnalyzer;

namespace PNGAnalyzerTests
{
    [TestFixture]
    public class pHYsTests
    {
        [Test]
        public void ShouldReadpHYs()
        {
            string filePath = @"../../../Data/IccP.png";
            pHYs phys = new pHYs(PNGReader.Read(filePath)[2]);
            Assert.AreEqual(phys.PixelsPerUnitX, 3543);
            Assert.AreEqual(phys.PixelsPerUnitY, 3543);
            Assert.AreEqual(phys.UnitSpecifier, 1);
        }
    }
}