using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PNGAnalyzer
{
    public class sPLT : Chunk
    {
        public sPLT(string type, byte[] data, uint crc) : base(type, data, crc)
        {
            if (type != "sPLT")
                throw new ArgumentException("Invalid chunk type passed to sPLT");
            Entries = new List<Entry>();
            ParseData(data);
        }

        public sPLT(Chunk chunk) : base(chunk)
        {
            if (chunk.Type != "sPLT")
                throw new ArgumentException("Invalid chunk type passed to sPLT");
            Entries = new List<Entry>();
            ParseData(chunk.Data);
        }

        public string Name { get; set; }
        public byte SampleDepth { get; set; }
        public List<Entry> Entries { get; }

        private void ParseData(byte[] data)
        {
            Encoding latin1 = Encoding.GetEncoding(28592);
            Name = latin1.GetString(data.TakeWhile(b => b != 0).ToArray());
            SampleDepth = data[Name.Length + 1];
            GetPaletteEntries(data);
        }

        private void GetPaletteEntries(byte[] data)
        {
            switch (SampleDepth)
            {
                case 8:
                    GetByteEntries(data);
                    break;
                case 16:
                    GetShortEntries(data);
                    break;
                default:
                    throw new ArgumentException("Invalid sPLT sample depth");
            }
        }

        private void GetByteEntries(byte[] data)
        {
            for (int i = Name.Length + 2; i < data.Length; i += 6)
                Entries.Add(new ByteEntry(data[i], data[i + 1], data[i + 2], data[i + 3],
                    Converter.ToInt16(data, i + 4)));
        }

        private void GetShortEntries(byte[] data)
        {
            for (int i = Name.Length + 2; i < data.Length; i += 10)
                Entries.Add(new ShortEntry(Converter.ToInt16(data, i), Converter.ToInt16(data, i + 2),
                    Converter.ToInt16(data, i + 4), Converter.ToInt16(data, i + 6),
                    Converter.ToInt16(data, i + 8)));
        }

        public override string ToString()
        {
            return $"{base.ToString()}, " +
                   $"{nameof(Name)}: {Name}, " +
                   $"{nameof(SampleDepth)}: {SampleDepth}, " +
                   $"{nameof(Entries)}: {Entries}";
        }

        public class Entry
        {}

        private class ByteEntry : Entry
        {
            private byte Alpha;
            private byte Blue;
            private short Frequency;
            private byte Green;
            private byte Red;

            public ByteEntry(byte red, byte green, byte blue, byte alpha, short frequency)
            {
                Red = red;
                Green = green;
                Blue = blue;
                Alpha = alpha;
                Frequency = frequency;
            }

            public override string ToString()
            {
                return $"{nameof(Red)}: {Red}\n" +
                       $"{nameof(Green)}: {Green}\n" +
                       $"{nameof(Blue)}: {Blue}\n +" +
                       $"{nameof(Alpha)}: {Alpha}\n +" +
                       $" {nameof(Frequency)}: {Frequency}";
            }
        }

        private class ShortEntry : Entry
        {
            private short Alpha;
            private short Blue;
            private short Frequency;
            private short Green;
            private short Red;

            public ShortEntry(short red, short green, short blue, short alpha, short frequency)
            {
                Red = red;
                Green = green;
                Blue = blue;
                Alpha = alpha;
                Frequency = frequency;
            }

            public override string ToString()
            {
                return $"{nameof(Alpha)}: {Alpha}\n" +
                       $"{nameof(Blue)}: {Blue}\n" +
                       $"{nameof(Frequency)}: {Frequency}\n" +
                       $"{nameof(Green)}: {Green}\n" +
                       $"{nameof(Red)}: {Red}";
            }
        }
    }
}