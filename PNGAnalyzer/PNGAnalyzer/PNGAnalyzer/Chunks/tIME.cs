using System;

namespace PNGAnalyzer
{
    public class tIME : Chunk
    {
        public tIME(string type, byte[] data, uint crc) : base(type, data, crc)
        {
            if (type != "tIME")
                throw new ArgumentException("Invalid chunk type passed to tIME");
            ParseData(data);
        }

        public tIME(Chunk chunk) : base(chunk)
        {
            if (chunk.Type != "tIME")
                throw new ArgumentException("Invalid chunk type passed to tIME");
            ParseData(chunk.Data);
        }

        public DateTime LatestModificationDate { get; private set; }

        private void ParseData(byte[] data)
        {
            LatestModificationDate =
                new DateTime(Converter.ToInt16(data), data[2], data[3], data[4], data[5], data[6]);
        }

        public override string ToString()
        {
            return $"{base.ToString()}, " +
                   $"{nameof(LatestModificationDate)}: {LatestModificationDate}";
        }
    }
}