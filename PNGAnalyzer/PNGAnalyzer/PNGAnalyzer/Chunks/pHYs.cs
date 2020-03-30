using System;

namespace PNGAnalyzer
{
    public class pHYs : Chunk
    {
        public pHYs(string type, byte[] data, uint crc) : base(type, data, crc)
        {
            if (type != "pHYs")
                throw new ArgumentException("Invalid chunk type passed to pHYs");
            ParseData(data);
        }

        public pHYs(Chunk chunk) : base(chunk)
        {
            if (chunk.Type != "pHYs")
                throw new ArgumentException("Invalid chunk type passed to pHYs");
            ParseData(chunk.Data);
        }

        public uint PixelsPerUnitX { get; private set; }
        public uint PixelsPerUnitY { get; private set; }
        public byte UnitSpecifier { get; private set; }

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