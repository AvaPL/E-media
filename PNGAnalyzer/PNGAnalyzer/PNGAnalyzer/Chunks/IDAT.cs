using System;

namespace PNGAnalyzer
{
    public class IDAT : Chunk
    {
        public IDAT(string type, byte[] data, int crc) : base(type, data, crc)
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