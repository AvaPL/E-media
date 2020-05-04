using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNGAnalyzer
{
    public class sPLT : Chunk
    {
        public sPLT(byte[] data, uint crc) : base("sPLT", data, crc)
        {
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

        public string Name { get; private set; }
        public byte SampleDepth { get; private set; }
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
            if (SampleDepth == 8)
                GetByteEntries(data);
            else if (SampleDepth == 16)
                GetShortEntries(data);
            else
                throw new ArgumentException("Invalid sPLT sample depth");
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
                   $"{nameof(Entries)}: {Entries.Count} entries";
        }

        public class Entry
        {}

        private class ByteEntry : Entry
        {
            private byte alpha;
            private byte blue;
            private short frequency;
            private byte green;
            private byte red;

            public ByteEntry(byte red, byte green, byte blue, byte alpha, short frequency)
            {
                this.red = red;
                this.green = green;
                this.blue = blue;
                this.alpha = alpha;
                this.frequency = frequency;
            }

            public override string ToString()
            {
                return $"{nameof(alpha)}: {alpha}, " +
                       $"{nameof(blue)}: {blue}, " +
                       $"{nameof(frequency)}: {frequency}, " +
                       $"{nameof(green)}: {green}, " +
                       $"{nameof(red)}: {red}";
            }
        }

        private class ShortEntry : Entry
        {
            private short alpha;
            private short blue;
            private short frequency;
            private short green;
            private short red;

            public ShortEntry(short red, short green, short blue, short alpha, short frequency)
            {
                this.red = red;
                this.green = green;
                this.blue = blue;
                this.alpha = alpha;
                this.frequency = frequency;
            }

            public override string ToString()
            {
                return $"{nameof(alpha)}: {alpha}, " +
                       $"{nameof(blue)}: {blue}, " +
                       $"{nameof(frequency)}: {frequency}, " +
                       $"{nameof(green)}: {green}, " +
                       $"{nameof(red)}: {red}";
            }
        }
    }
}