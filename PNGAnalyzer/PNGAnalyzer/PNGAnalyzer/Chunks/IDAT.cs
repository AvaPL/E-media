using System;
using System.Text;

namespace PNGAnalyzer
{
    public class IDAT : Chunk
    {
        private static byte[] TypeBytes = Encoding.ASCII.GetBytes("IDAT");

        public IDAT(string type, byte[] data, uint crc) : base(type, data, crc)
        {
            if (type != "IDAT")
                throw new ArgumentException("Invalid chunk type passed to IDAT");
        }

        public IDAT(Chunk chunk) : base(chunk)
        {
            if (chunk.Type != "IDAT")
                throw new ArgumentException("Invalid chunk type passed to IDAT");
        }
    }
}