namespace PNGAnalyzer
{
    public class hIST : Chunk
    {
        public short[] Histogram { get; }

        public hIST(string type, byte[] data, int crc) : base(type, data, crc)
        {
            ParseData(data);
        }

        public hIST(Chunk chunk) : base(chunk)
        {
            ParseData(chunk.Data);
        }

        private void ParseData(byte[] data)
        {
            for (int i = 0; i < data.Length; i+=2)
            {
                Histogram[i / 2] = Converter.ToInt16(data, i);
            }
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