using System;
using System.Collections.Generic;
using System.Linq;

namespace PNGAnalyzer
{
    public class PLTE : Chunk
    {
        public PLTE(string type, byte[] data, uint crc) : base(type, data, crc)
        {
            if (type != "PLTE")
                throw new ArgumentException("Invalid chunk type passed to PLTE");
            Entries = new List<Entry>(data.Length / 3);
            ParseData(data);
        }

        public PLTE(Chunk chunk) : base(chunk)
        {
            if (chunk.Type != "PLTE")
                throw new ArgumentException("Invalid chunk type passed to PLTE");
            Entries = new List<Entry>(chunk.Data.Length / 3);
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

        public override string ToString()
        {
            return $"{base.ToString()}, " +
                   $"{nameof(Entries)}: {Entries}";
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

            protected bool Equals(Entry other)
            {
                return Red == other.Red && Green == other.Green && Blue == other.Blue;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((Entry) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = Red.GetHashCode();
                    hashCode = (hashCode * 397) ^ Green.GetHashCode();
                    hashCode = (hashCode * 397) ^ Blue.GetHashCode();
                    return hashCode;
                }
            }
        }
    }
}