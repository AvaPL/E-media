namespace PNGAnalyzer
{
    public class IHDR : Chunk
    {
        public int Width { get; }
        public int Height { get; }
        public byte BitDepth { get; }
        public byte ColorType { get; }
        public byte CompressionMethod { get; }
        public byte FilterMethod { get; }
        public byte InterlaceMethod { get; }

        public IHDR(string type, byte[] data, int crc) : base(type, data, crc)
        {
        }

        public override string GetInfo()
        {
            return base.GetInfo() + "\n" + ToString();
        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}\n {nameof(Height)}: {Height}\n {nameof(BitDepth)}: {BitDepth}\n {nameof(ColorType)}: {ColorType}\n {nameof(CompressionMethod)}: {CompressionMethod}\n {nameof(FilterMethod)}: {FilterMethod}\n {nameof(InterlaceMethod)}: {InterlaceMethod}";
        }
    }
}