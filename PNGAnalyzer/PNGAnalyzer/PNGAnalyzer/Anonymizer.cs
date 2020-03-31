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
            List<Chunk> anonymized = chunks.Where(c => char.IsUpper(c.Type[0])).ToList();
            PNGFile.Write(filePathToWrite, anonymized);
        }
    }
}