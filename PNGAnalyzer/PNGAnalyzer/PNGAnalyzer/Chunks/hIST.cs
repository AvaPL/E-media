using System;

namespace PNGAnalyzer
{
    public class hIST : Chunk
    {
        public hIST(string type, byte[] data, uint crc) : base(type, data, crc)
        {
            if (type != "hIST")
                throw new ArgumentException("Invalid chunk type passed to hIST");
            Histogram = new short[data.Length / 2];
            ParseData(data);
        }

        public hIST(Chunk chunk) : base(chunk)
        {
            if (chunk.Type != "hIST")
                throw new ArgumentException("Invalid chunk type passed to hIST");
            Histogram = new short[chunk.Data.Length / 2];
            ParseData(chunk.Data);
        }

        public short[] Histogram { get; }

        private void ParseData(byte[] data)
        {
            for (int i = 0; i < data.Length; i += 2)
            {
                Histogram[i / 2] = Converter.ToInt16(data, i);
            }
        }

        public override string GetInfo()
        {
            return base.GetInfo() + "\n" + ToString();
        }

        public override string ToString()
        {
            return $"{nameof(Histogram)}: {Histogram}";
        }
    }
}