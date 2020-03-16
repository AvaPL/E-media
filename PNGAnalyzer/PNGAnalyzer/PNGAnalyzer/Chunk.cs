namespace PNGAnalyzer
{
    public class Chunk
    {
        public string Type { get; }
        public byte[] Data { get; }
        public int CRC { get; }

        public Chunk(string type, byte[] data, int crc)
        {
            Type = type;
            Data = data;
            CRC = crc;
        }
        
        
    }
}