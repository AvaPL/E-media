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

        public Chunk(Chunk chunk)
        {
            Type = chunk.Type;
            Data = chunk.Data;
            CRC = chunk.CRC;
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
            return
                $"{nameof(Type)}: {Type}\n" +
                $"{nameof(Data)}: {Data}\n" +
                $"{nameof(CRC)}: {CRC}";
        }
    }
}