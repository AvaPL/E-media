namespace PNGAnalyzer
{
    public class pHYs : Chunk
    {
        public uint PixelsPerUnitX { get; private set; }
        public uint PixelsPerUnitY { get; private set; }
        public byte UnitSpecifier { get; private set; }

        public pHYs(string type, byte[] data, int crc) : base(type, data, crc)
        {
            ParseData(data);
        }

        public pHYs(Chunk chunk) : base(chunk)
        {
            ParseData(chunk.Data);
        }

        private void ParseData(byte[] data)
        {
            PixelsPerUnitX = Converter.ToUInt32(data, 0);
            PixelsPerUnitY = Converter.ToUInt32(data, 4);
            UnitSpecifier = data[8];
        }

        public override string GetInfo()
        {
            return base.GetInfo() + ToString();
        }

        public override string ToString()
        {
            return
                $"{nameof(PixelsPerUnitX)}: {PixelsPerUnitX}\n" +
                $"{nameof(PixelsPerUnitY)}: {PixelsPerUnitY}\n" +
                $"{nameof(UnitSpecifier)}: {UnitSpecifier}";
        }
    }
}