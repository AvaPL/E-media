using System;
using System.Text;

namespace PNGAnalyzer
{
    public class IDAT : Chunk
    {
        private static byte[] TypeBytes = Encoding.ASCII.GetBytes("IDAT");

        public IDAT(byte[] data, uint crc) : base("IDAT", data, crc)
        {
        }

        public IDAT(Chunk chunk) : base(chunk)
        {
            if (chunk.Type != "IDAT")
                throw new ArgumentException("Invalid chunk type passed to IDAT");
        }
    }
}