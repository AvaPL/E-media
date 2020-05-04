using System.Collections.Generic;
using System.Linq;
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
            Assert.AreEqual(new byte[] { }, result);
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
                new IDAT("IDAT", new byte[] { }, 0),
                new IDAT("IDAT", new byte[] {2, 5}, 0)
            };
            byte[] result = IDATConverter.ConcatToBytes(idats);
            Assert.AreEqual(new byte[] {1, 5, 7, 4, 2, 5}, result);
        }

        [Test]
        public void ShouldSplitToOneEmptyIDAT()
        {
            byte[] bytes = { };
            List<IDAT> result = IDATConverter.SplitToIDATs(bytes);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(bytes, result[0].Data);
        }

        [Test]
        public void ShouldSplitToOneIDAT()
        {
            byte[] bytes = {1, 3, 5};
            List<IDAT> result = IDATConverter.SplitToIDATs(bytes);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(bytes, result[0].Data);
        }

        [Test]
        public void ShouldSplitToThreeIDATs()
        {
            byte[] bytes = CreateByteRange(0, short.MaxValue * 3);
            List<IDAT> result = IDATConverter.SplitToIDATs(bytes);
            Assert.AreEqual(3, result.Count);
            byte[] expectedFirstChunkData = CreateByteRange(0, short.MaxValue);
            byte[] expectedSecondChunkData = CreateByteRange(short.MaxValue, short.MaxValue);
            byte[] expectedThirdChunkData = CreateByteRange(short.MaxValue * 2, short.MaxValue);
            Assert.AreEqual(expectedFirstChunkData, result[0].Data);
            Assert.AreEqual(expectedSecondChunkData, result[1].Data);
            Assert.AreEqual(expectedThirdChunkData, result[2].Data);
        }

        private static byte[] CreateByteRange(int start, int count)
        {
            return Enumerable.Range(start, count).Select(i => (byte) i).ToArray();
        }

        [Test]
        public void ShouldSplitToThreeIDATsWithOneShorter()
        {
            byte[] bytes = CreateByteRange(0, short.MaxValue * 2 + 1);
            List<IDAT> result = IDATConverter.SplitToIDATs(bytes);
            Assert.AreEqual(3, result.Count);
            byte[] expectedFirstChunkData = CreateByteRange(0, short.MaxValue);
            byte[] expectedSecondChunkData = CreateByteRange(short.MaxValue, short.MaxValue);
            byte[] expectedThirdChunkData = CreateByteRange(short.MaxValue * 2, 1);
            Assert.AreEqual(expectedFirstChunkData, result[0].Data);
            Assert.AreEqual(expectedSecondChunkData, result[1].Data);
            Assert.AreEqual(expectedThirdChunkData, result[2].Data);
        }

        [Test]
        public void ShouldConcatAndThenSplitImage()
        {
            string filePath = @"../../../Data/lena.png";
            List<Chunk> chunks = PNGFile.Read(filePath);
            List<Chunk> parsedChunks = ChunkParser.Parse(chunks);
            int firstIdatIndex = parsedChunks.TakeWhile(chunk => !IsIDAT(chunk)).Count();
            List<IDAT> idats = parsedChunks.Where(IsIDAT).Select(chunk => (IDAT) chunk).ToList();
            byte[] bytes = IDATConverter.ConcatToBytes(idats);
            List<Chunk> resultIdats = IDATConverter.SplitToIDATs(bytes).Select(idat => (Chunk) idat).ToList();
            List<Chunk> resultChunks = parsedChunks.Where(chunk => !IsIDAT(chunk)).ToList();
            resultChunks.InsertRange(firstIdatIndex, resultIdats);
            PNGFile.Write(@"../../../Data/lenaConverted.png", resultChunks);
        }
        
        [Test]
        public void ShouldConcatDecompressCompressAndThenSplitImage()
        {
            string filePath = @"../../../Data/lena.png";
            List<Chunk> chunks = PNGFile.Read(filePath);
            List<Chunk> parsedChunks = ChunkParser.Parse(chunks);
            int firstIdatIndex = parsedChunks.TakeWhile(chunk => !IsIDAT(chunk)).Count();
            List<IDAT> idats = parsedChunks.Where(IsIDAT).Select(chunk => (IDAT) chunk).ToList();
            byte[] bytes = IDATConverter.ConcatToBytes(idats);
            byte[] decompressedBytes = ZlibCompression.Decompress(bytes);
            byte[] compressedBytes = ZlibCompression.Compress(decompressedBytes);
            List<Chunk> resultIdats = IDATConverter.SplitToIDATs(compressedBytes).Select(idat => (Chunk) idat).ToList();
            List<Chunk> resultChunks = parsedChunks.Where(chunk => !IsIDAT(chunk)).ToList();
            resultChunks.InsertRange(firstIdatIndex, resultIdats);
            PNGFile.Write(@"../../../Data/lenaCompressed.png", resultChunks);
        }

        private static bool IsIDAT(Chunk chunk)
        {
            return chunk.Type == "IDAT";
        }
    }
}