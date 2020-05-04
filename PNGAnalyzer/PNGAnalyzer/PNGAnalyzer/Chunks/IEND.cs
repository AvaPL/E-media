using System;

namespace PNGAnalyzer
{
    public class IEND : Chunk
    {
        public IEND(byte[] data, uint crc) : base("IEND", data, crc)
        {
        }

        public IEND(Chunk chunk) : base(chunk)
        {
            if (chunk.Type != "IEND")
                throw new ArgumentException("Invalid chunk type passed to IEND");
        }
    }
}