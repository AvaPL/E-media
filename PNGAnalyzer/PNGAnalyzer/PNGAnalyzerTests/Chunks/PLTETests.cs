using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PNGAnalyzer;

namespace PNGAnalyzerTests
{
    [TestFixture]
    public class PLTETests
    {
        [Test]
        public void ShouldReadcHRM()
        {
            string filePath = @"../../../Data/papugaxd.png";
            PLTE plte = new PLTE(PNGFile.Read(filePath)[4]);
            Assert.AreEqual("PLTE", plte.Type);
            Assert.AreEqual(new PLTE.Entry(0,0,0), plte.Entries.First());
            Assert.AreEqual(new PLTE.Entry(8, 255, 255), plte.Entries.Last());
        }
    }
}