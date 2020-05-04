using System.Collections.Generic;
using NUnit.Framework;
using PNGAnalyzer;

namespace PNGAnalyzerTests
{
    [TestFixture]
    public class IDATConverterTests
    {
        [Test]
        public void ShouldConcatEmptyList()
        {
            List<IDAT> idats = new List<IDAT>();
            byte[] result = IDATConverter.ConcatToBytes(idats);
            Assert.AreEqual(new byte[] {}, result);
        }
        
        [Test]
        public void ShouldConcatOneIDAT()
        {
            IDAT idat = new IDAT("IDAT", new byte[] {1, 5, 7}, 0);
            List<IDAT> idats = new List<IDAT> {idat};
            byte[] result = IDATConverter.ConcatToBytes(idats);
            Assert.AreEqual(new byte[] {1, 5, 7}, result);
        }

        [Test]
        public void ShouldConcatFourIDATs()
        {
            List<IDAT> idats = new List<IDAT>
            {
                new IDAT("IDAT", new byte[] {1, 5, 7}, 0),
                new IDAT("IDAT", new byte[] {4}, 0),
                new IDAT("IDAT", new byte[] {}, 0),
                new IDAT("IDAT", new byte[] {2, 5}, 0)
            };
            byte[] result = IDATConverter.ConcatToBytes(idats);
            Assert.AreEqual(new byte[] {1, 5, 7, 4, 2, 5}, result);
        }
    }
}