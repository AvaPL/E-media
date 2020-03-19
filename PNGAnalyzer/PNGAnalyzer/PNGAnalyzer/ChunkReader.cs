using System.Linq;
using System.Text;

namespace PNGAnalyzer
{
    public class ChunkReader
    {
        private readonly byte[] bytes;
        private int index = 8;

        public ChunkReader(byte[] bytes)
        {
            this.bytes = bytes;
        }

        public Chunk ReadChunk()
        {
            return index >= bytes.Length ? null : ParseChunk();
        }

        private Chunk ParseChunk()
        {
            int length = Converter.ToInt32(SkipAndTake(4));
            string type = Encoding.ASCII.GetString(SkipAndTake(4));
            byte[] data = SkipAndTake(length);
            int crc = Converter.ToInt32(SkipAndTake(4));
            return new Chunk(type, data, crc);
        }

        private byte[] SkipAndTake(int length)
        {
            byte[] takenBytes = bytes.Skip(index).Take(length).ToArray();
            index += length;
            return takenBytes;
        }
    }
}