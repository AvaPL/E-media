using System;
using System.Linq;
using System.Text;

namespace PNGAnalyzer
{
    public class ChunkReader
    {
        private readonly Encoding ascii = Encoding.ASCII;
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
            int length = ToInt32(SkipAndTake(4));
            string type = ascii.GetString(SkipAndTake(4));
            byte[] data = SkipAndTake(length);
            int crc = ToInt32(SkipAndTake(4));
            return new Chunk(type, data, crc);
        }

        private static int ToInt32(byte[] value)
        {
            Array.Reverse(value, 0, value.Length);
            return BitConverter.ToInt32(value, 0);
        }

        private byte[] SkipAndTake(int toTake)
        {
            byte[] takenBytes = bytes.Skip(index).Take(toTake).ToArray();
            index += toTake;
            return takenBytes;
        }
    }
}