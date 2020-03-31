using System;

namespace PNGAnalyzer
{
    public class IHDR : Chunk
    {
        public IHDR(string type, byte[] data, uint crc) : base(type, data, crc)
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

        public override string ToString()
        {
            return $"{base.ToString()}, " +
                   $"{nameof(Width)}: {Width}, " +
                   $"{nameof(Height)}: {Height}, " +
                   $"{nameof(BitDepth)}: {BitDepth}, " +
                   $"{nameof(ColorType)}: {ColorType}, " +
                   $"{nameof(CompressionMethod)}: {CompressionMethod}, " +
                   $"{nameof(FilterMethod)}: {FilterMethod}, " +
                   $"{nameof(InterlaceMethod)}: {InterlaceMethod}";
        }
    }
}