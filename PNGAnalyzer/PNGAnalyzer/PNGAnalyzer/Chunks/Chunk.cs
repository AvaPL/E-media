namespace PNGAnalyzer
{
    public class Chunk
    {
        public Chunk(string type, byte[] data, uint crc)
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
        public uint CRC { get; }

        public virtual string GetInfo()
        {
            return base.ToString();
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