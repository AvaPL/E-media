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

        public static IDAT operator +(IDAT idat1, IDAT idat2)
        {
            byte[] data = new byte[idat1.Data.Length + idat2.Data.Length];
            idat1.Data.CopyTo(data, 0);
            idat2.Data.CopyTo(data, idat1.Data.Length);
            return new IDAT(idat1.Type, data, idat1.CRC + idat2.CRC);
        }
    }
}