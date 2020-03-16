using System;
using System.Linq;
using System.Text;

namespace PNGAnalyzer
{
    public class ChunkReader
    {
        Encoding ascii = Encoding.ASCII;
        private byte[] bytes;
        private int index = 8;

        public ChunkReader(byte[] bytes)
        {
            this.bytes = bytes;
        }

        public Chunk ReadChunk()
        {
            if (index >= bytes.Length)
                return null;

            int length = ToInt32(SkipAndTake(4));
            string type = ascii.GetString(SkipAndTake(4));
            byte[] data = SkipAndTake(length);
            int crc = ToInt32(SkipAndTake(4));

            return new Chunk(type, data, crc);
        }

        private int ToInt32(byte[] value)
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