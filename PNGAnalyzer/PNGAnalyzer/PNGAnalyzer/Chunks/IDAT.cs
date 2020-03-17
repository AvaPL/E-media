namespace PNGAnalyzer
{
    public class IDAT : Chunk
    {
        //TODO: Throw exception on wrong type input.
        public IDAT(string type, byte[] data, int crc) : base(type, data, crc)
        {
        }

        public IDAT(Chunk chunk) : base(chunk)
        {
        }
    }
}