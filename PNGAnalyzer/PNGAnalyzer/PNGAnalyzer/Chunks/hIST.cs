namespace PNGAnalyzer
{
    public class hIST : Chunk
    {
        public short[] Histogram { get; }

        public hIST(string type, byte[] data, int crc) : base(type, data, crc)
        {
        }

        public override string GetInfo()
        {
            return base.GetInfo() + "\n" + ToString();
        }

        public override string ToString()
        {
            return $"{nameof(Histogram)}: {Histogram}";
        }
    }
}