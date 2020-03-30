using System;
using System.Collections.Generic;
using System.Linq;

namespace PNGAnalyzer
{
    public class Anonymizer
    {
        public static void Anonymize(string filePathToRead, string filePathToWrite)
        {
            List<Chunk> chunks = ChunkParser.Parse(PNGFile.Read(filePathToRead));
            List<Chunk> anonymized = Anonymize(chunks);
            PNGFile.Write(filePathToWrite, anonymized);
        }

        private static List<Chunk> Anonymize(List<Chunk> chunks)
        {
            return chunks.Where(c => char.IsUpper(c.Type[0]))
                .GroupBy(chunk => chunk.Type, chunk => chunk)
                .Select(grouping => grouping.Key == "IDAT" ? AggregateIDAT(grouping) : grouping)
                .SelectMany(chunk => chunk).ToList();
        }

        private static IEnumerable<Chunk> AggregateIDAT(IEnumerable<Chunk> idats)
        {
            return new[] {idats.Select(chunk => (IDAT) chunk).Aggregate((idat1, idat2) => idat1 + idat2)};
        }
    }
}