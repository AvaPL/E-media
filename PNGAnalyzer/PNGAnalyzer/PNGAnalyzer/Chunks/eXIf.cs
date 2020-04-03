using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNGAnalyzer
{
    public class eXIf : Chunk
    {
        public eXIf(string type, byte[] data, uint crc) : base(type, data, crc)
        {
            if (type != "eXIf")
                throw new ArgumentException("Invalid chunk type passed to eXIf");
            ParseData(data);
        }

        public eXIf(Chunk chunk) : base(chunk)
        {
            if (chunk.Type != "eXIf")
                throw new ArgumentException("Invalid chunk type passed to eXIf");
            ParseData(chunk.Data);
        }

        public string EndianFlag { get; private set; }


        private void ParseData(byte[] data)
        {
            EndianFlag = Encoding.GetEncoding("ISO-8859-1").GetString(data.Take(2).ToArray());
            int index = 2;
            if (Converter.ToInt16(data, index)!=42)
            {
                throw new FormatException("eXIf data is not in TIFF format");
            }
            index += 2;
        }


        public class IFD
        {
            public class Tag
            {
                public short TagID { get; private set; }
                public short TagType { get; private set; }
                public uint Count { get; private set; }
                public uint Offset { get; private set; }
            }
            
            public List<Tag> Tags { get; private set; }
            public short Length { get; private set; }
        }
    }
}