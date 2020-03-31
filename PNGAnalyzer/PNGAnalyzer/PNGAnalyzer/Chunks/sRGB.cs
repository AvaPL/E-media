using System;

namespace PNGAnalyzer
{
    public class sRGB : Chunk
    {
        public sRGB(string type, byte[] data, uint crc) : base(type, data, crc)
        {
            if (type != "sRGB")
                throw new ArgumentException("Invalid chunk type passed to sRGB");
            ParseData(data);
        }

        public sRGB(Chunk chunk) : base(chunk)
        {
            if (chunk.Type != "sRGB")
                throw new ArgumentException("Invalid chunk type passed to sRGB");
            ParseData(chunk.Data);
        }

        public byte RenderingIntent { get; private set; }

        private void ParseData(byte[] data)
        {
            RenderingIntent = data[0];
        }

        public override string ToString()
        {
            return $"{base.ToString()}, " +
                   $"{nameof(RenderingIntent)}: {RenderingIntent}";
        }
    }
}