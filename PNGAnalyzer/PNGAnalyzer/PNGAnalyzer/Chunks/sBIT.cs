namespace PNGAnalyzer
{
    public class sBIT : Chunk
    {
        public byte[] SignificantBytes { get; }

        public sBIT(string type, byte[] data, int crc) : base(type, data, crc)
        {
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