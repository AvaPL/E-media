using System;

namespace PNGAnalyzer
{
    public class sBIT : Chunk
    {
        public sBIT(byte[] data, uint crc) : base("sBIT", data, crc)
        {
            SignificantBits = new byte[data.Length];
            ParseData(data);
        }

        public sBIT(Chunk chunk) : base(chunk)
        {
            if (chunk.Type != "sBIT")
                throw new ArgumentException("Invalid chunk type passed to sBIT");
            SignificantBits = new byte[chunk.Data.Length];
            ParseData(chunk.Data);
        }

        public byte[] SignificantBits { get; private set; }

        private void ParseData(byte[] data)
        {
            SignificantBits = data;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, " +
                   $"{nameof(SignificantBits)}: {string.Join(", ", SignificantBits)}";
        }
    }
}