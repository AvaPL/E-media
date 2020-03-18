namespace PNGAnalyzer
{
    public class sBIT : Chunk
    {
        public sBIT(string type, byte[] data, int crc) : base(type, data, crc)
        {
            SignificantBytes = new byte[data.Length];
            ParseData(data);
        }

        public sBIT(Chunk chunk) : base(chunk)
        {
            SignificantBytes = new byte[chunk.Data.Length];
            ParseData(chunk.Data);
        }

        public byte[] SignificantBytes { get; private set; }

        private void ParseData(byte[] data)
        {
            SignificantBytes = data;
        }

        public override string GetInfo()
        {
            return base.GetInfo() + "\n" + ToString();
        }

        public override string ToString()
        {
            return $"{nameof(SignificantBytes)}: {SignificantBytes}";
        }
    }
}