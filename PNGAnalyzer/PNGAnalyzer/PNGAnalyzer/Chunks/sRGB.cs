using System;

namespace PNGAnalyzer
{
    public class sRGB : Chunk
    {
        public sRGB(string type, byte[] data, int crc) : base(type, data, crc)
        {
            if(type != "sRGB")
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
        
        public override string GetInfo()
        {
            return base.GetInfo() + "\n" + ToString();
        }

        public override string ToString()
        {
            return $"{nameof(RenderingIntent)}: {RenderingIntent}\n";
        }
    }
}