using System.Collections.Generic;
using System.Text;

namespace PNGAnalyzer
{
    public class ChunkWriter
    {
        
        private readonly List<byte> bytes = new List<byte>();

        public void Write(Chunk chunk)
        {
            bytes.AddRange(Converter.GetBytes(chunk.Data.Length));
            bytes.AddRange(Encoding.ASCII.GetBytes(chunk.Type));
            bytes.AddRange(chunk.Data);
            bytes.AddRange(Converter.GetBytes(chunk.CRC));
        }

        public byte[] GetBytes()
        {
            return bytes.ToArray();
        }
    }
}