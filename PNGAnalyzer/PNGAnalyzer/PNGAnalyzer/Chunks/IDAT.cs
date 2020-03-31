using System;
using System.Text;
using Force.Crc32;

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

        public static IDAT operator +(IDAT idat1, IDAT idat2)
        {
            byte[] data = new byte[idat1.Data.Length + idat2.Data.Length];
            idat1.Data.CopyTo(data, 0);
            idat2.Data.CopyTo(data, idat1.Data.Length);
            byte[] crcData = new byte[TypeBytes.Length + data.Length];
            TypeBytes.CopyTo(crcData, 0);
            data.CopyTo(crcData, TypeBytes.Length);
            return new IDAT(idat1.Type, data, Crc32Algorithm.Compute(crcData));
        }
    }
}