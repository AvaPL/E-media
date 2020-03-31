using System;

namespace PNGAnalyzer
{
    public class sBIT : Chunk
    {
        public sBIT(string type, byte[] data, uint crc) : base(type, data, crc)
        {
            if (type != "sBIT")
                throw new ArgumentException("Invalid chunk type passed to sBIT");
            SignificantBytes = new byte[data.Length];
            ParseData(data);
        }

        public sBIT(Chunk chunk) : base(chunk)
        {
            if (chunk.Type != "sBIT")
                throw new ArgumentException("Invalid chunk type passed to sBIT");
            SignificantBytes = new byte[chunk.Data.Length];
            ParseData(chunk.Data);
        }

        public byte[] SignificantBytes { get; private set; }

        private void ParseData(byte[] data)
        {
            SignificantBytes = data;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, " +
                   $"{nameof(SignificantBytes)}: {SignificantBytes}";
        }
    }
}