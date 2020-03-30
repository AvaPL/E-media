using System;
using System.Collections.Generic;
using System.Linq;

namespace PNGAnalyzer
{
    public class Anonymizer
    {
        public static List<Chunk> Anonymize(string filePathToRead)
        {
            List<Chunk> chunks = PNGFile.Read(filePathToRead);
            List<Chunk> anonymized = chunks.Where(c => Char.IsUpper(c.Type.ToCharArray()[0])).ToList();
            
            return anonymized;
        }
    }
}