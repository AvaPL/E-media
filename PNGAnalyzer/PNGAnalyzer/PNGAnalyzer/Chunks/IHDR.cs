using System;

namespace PNGAnalyzer
{
    public class IHDR : Chunk
    {
        public IHDR(string type, byte[] data, int crc) : base(type, data, crc)
        {
            if (type != "IHDR")
                throw new ArgumentException("Invalid chunk type passed to IHDR");
            ParseData(data);
        }

        public IHDR(Chunk chunk) : base(chunk)
        {
            if (chunk.Type != "IHDR")
                throw new ArgumentException("Invalid chunk type passed to IHDR");
            ParseData(chunk.Data);
        }

        public int Width { get; private set; }
        public int Height { get; private set; }
        public byte BitDepth { get; private set; }
        public byte ColorType { get; private set; }
        public byte CompressionMethod { get; private set; }
        public byte FilterMethod { get; private set; }
        public byte InterlaceMethod { get; private set; }

        private void ParseData(byte[] data)
        {
            Width = Converter.ToInt32(data, 0);
            Height = Converter.ToInt32(data, 4);
            BitDepth = data[8];
            ColorType = data[9];
            CompressionMethod = data[10];
            FilterMethod = data[11];
            InterlaceMethod = data[12];
        }

        public override string GetInfo()
        {
            return base.GetInfo() + "\n" + ToString();
        }

        public override string ToString()
        {
            return
                $"{nameof(Width)}: {Width}\n" +
                $"{nameof(Height)}: {Height}\n" +
                $"{nameof(BitDepth)}: {BitDepth}\n" +
                $"{nameof(ColorType)}: {ColorType}\n" +
                $"{nameof(CompressionMethod)}: {CompressionMethod}\n" +
                $"{nameof(FilterMethod)}: {FilterMethod}\n" +
                $"{nameof(InterlaceMethod)}: {InterlaceMethod}";
        }
    }
}