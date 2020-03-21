using System;

namespace PNGAnalyzer
{
    public class tRNS : Chunk
    {
        public tRNS(string type, byte[] data, int crc) : base(type, data, crc)
        {
            if(type != "tRNS")
                throw new ArgumentException("Invalid chunk type passed to tRNS");
            ParseData(data);
        }

        public tRNS(Chunk chunk) : base(chunk)
        {
            if (chunk.Type != "tRNS")
                throw new ArgumentException("Invalid chunk type passed to tRNS");
            ParseData(chunk.Data);
        }

        public Transparency TransparencyInformation;

        private void ParseData(byte[] data)
        {
            TransparencyInformation = data.Length switch
            {
                2 => new Grayscale(data),
                6 => new Truecolor(data),
                _ =>new IndexedColor(data)
                
            };
        }

        public class Transparency
        {
        }

        public class IndexedColor : Transparency
        {
            public byte[] AlphasForPlte { get; private set; }

            public IndexedColor(byte[] data)
            {
                AlphasForPlte=data;
            }
        }

        public class Grayscale : Transparency
        {
            public short Gray { get; private set; }

            public Grayscale(byte[] data)
            {
                Gray = Converter.ToInt16(data);
            }
        }

        public class Truecolor : Transparency
        {
            public short Red { get; private set; }
            public short Green { get; private set; }
            public short Blue { get; private set; }

            public Truecolor(byte[] data)
            {
                Red = Converter.ToInt16(data, 0);
                Green = Converter.ToInt16(data, 2);
                Blue = Converter.ToInt16(data, 4);
            }
        }
        
    }
}