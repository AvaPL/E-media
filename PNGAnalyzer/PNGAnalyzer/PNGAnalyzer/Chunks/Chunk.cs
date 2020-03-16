namespace PNGAnalyzer
{
    public class Chunk
    {
        public Chunk(string type, byte[] data, int crc)
        {
            Type = type;
            Data = data;
            CRC = crc;
        }

        public string Type { get; }
        public byte[] Data { get; }
        public int CRC { get; }

        public virtual string GetInfo()
        {
            return ToString();
        }

        public override string ToString()
        {
            return $"{nameof(Type)}: {Type}\n {nameof(Data)}: {Data}\n {nameof(CRC)}: {CRC}";
        }
    }
}