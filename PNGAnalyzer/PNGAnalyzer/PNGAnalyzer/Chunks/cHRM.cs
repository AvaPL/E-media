namespace PNGAnalyzer
{
    public class cHRM : Chunk
    {
        public int  WhitePointX { get; }
        public int WhitePointY { get; }
        public int RedX { get; }
        public int RedY { get; }
        public int GreenX { get; }
        public int GreenY { get; }
        public int BlueX { get; }
        public int BlueY { get; }

        public cHRM(string type, byte[] data, int crc) : base(type, data, crc)
        {
        }

        public override string GetInfo()
        {
            return base.GetInfo() + "\n" + ToString();
        }

        public override string ToString()
        {
            return $"{nameof(WhitePointX)}: {WhitePointX}\n {nameof(WhitePointY)}: {WhitePointY}\n {nameof(RedX)}: {RedX}\n {nameof(RedY)}: {RedY}\n {nameof(GreenX)}: {GreenX}\n {nameof(GreenY)}: {GreenY}\n {nameof(BlueX)}: {BlueX}\n {nameof(BlueY)}: {BlueY}";
        }
    }
}