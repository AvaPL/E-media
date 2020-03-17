using System.Collections.Generic;
using System.Linq;

namespace PNGAnalyzer
{
    public class PLTE : Chunk
    {
        public PLTE(string type, byte[] data, int crc) : base(type, data, crc)
        {
            Entries = new List<Entry>(data.Length/3);
            ParseData(data);
        }

        public PLTE(Chunk chunk) : base(chunk)
        {
            Entries = new List<Entry>(chunk.Data.Length/3);
            ParseData(chunk.Data);
        }

        public List<Entry> Entries { get; }


        private void ParseData(byte[] data)
        {
            for (int i = 0; i < data.Length; i += 3)
            {
                Entries.Add(new Entry(data[i], data[i + 1], data[i + 2]));
            }
        }

        public override string GetInfo()
        {
            return base.GetInfo() + "\n" + ToString();
        }

        public override string ToString()
        {
            return $"{nameof(Entries)}: {Entries}";
        }

        public class Entry
        {
            public Entry(byte red, byte green, byte blue)
            {
                Red = red;
                Green = green;
                Blue = blue;
            }

            public byte Red { get; }
            public byte Green { get; }
            public byte Blue { get; }
        }
    }
}