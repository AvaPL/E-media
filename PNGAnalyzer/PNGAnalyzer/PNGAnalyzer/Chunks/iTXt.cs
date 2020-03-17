namespace PNGAnalyzer
{
    public class iTXt : Chunk
    {
        public iTXt(string type, byte[] data, int crc) : base(type, data, crc)
        {
            ParseData(data);
        }

        public iTXt(Chunk chunk) : base(chunk)
        {
            ParseData(chunk.Data);
        }

        private void ParseData(byte[] data)
        {
            //TODO: Implement.
        }
    }
}