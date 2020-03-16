namespace PNGAnalyzer
{
    public class PLTE : Chunk
    {
        public byte Red { get; }
        public byte Green { get; }
        public byte Blue { get; }

        public PLTE(string type, byte[] data, int crc) : base(type, data, crc)
        {
        }

        public override string GetInfo()
        {
            return base.GetInfo() + "\n" + ToString();
        }

        public override string ToString()
        {
            return $"{nameof(Red)}: {Red}\n {nameof(Green)}: {Green}\n {nameof(Blue)}: {Blue}";
        }
    }
}