using System;

namespace PNGAnalyzer
{
    public class gAMA : Chunk
    {
        public gAMA(string type, byte[] data, uint crc) : base(type, data, crc)
        {
            if (type != "gAMA")
                throw new ArgumentException("Invalid chunk type passed to gAMA");
            ParseData(data);
        }

        public gAMA(Chunk chunk) : base(chunk)
        {
            if (chunk.Type != "gAMA")
                throw new ArgumentException("Invalid chunk type passed to gAMA");
            ParseData(chunk.Data);
        }

        public int Gamma { get; private set; }


        private void ParseData(byte[] data)
        {
            Gamma = Converter.ToInt32(data, 0);
        }

        public override string ToString()
        {
            return $"{base.ToString()}, " +
                   $"{nameof(Gamma)}: {Gamma}";
        }
    }
}