using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PNGAnalyzer
{
    public class PNGReader
    {
        public static Dictionary<string, byte[]> Read(string filePath)
        {
            byte[] bytes = File.ReadAllBytes(filePath);
            if (!IsPNG(bytes))
                throw new FormatException("The file is not a PNG file!");
            
            Dictionary<string, byte[]> dictionary = new Dictionary<string, byte[]>();

            return dictionary;
        }

        private static bool IsPNG(byte[] bytes)
        {
            return bytes.Take(8).SequenceEqual(new byte[]{137, 80, 78, 71, 13, 10, 26, 10});
        }
    }
}