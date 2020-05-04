using System;

namespace PNGAnalyzer
{
    public class hIST : Chunk
    {
        public hIST(byte[] data, uint crc) : base("hIST", data, crc)
        {
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

        public override string ToString()
        {
            return $"{base.ToString()}, " +
                   $"{nameof(Histogram)}: {Histogram.Length} bars";
        }
    }
}