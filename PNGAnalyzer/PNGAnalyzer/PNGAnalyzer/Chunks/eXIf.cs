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
        public int Offset { get; private set; }


        private void ParseData(byte[] data)
        {
            EndianFlag = Encoding.GetEncoding("ISO-8859-1").GetString(data.Take(2).ToArray());
            int index = 2;
            if (Converter.ToInt16(data, index) != 42)
            {
                throw new FormatException("eXIf data is not in TIFF format");
            }

            index += 2;
            Offset = Converter.ToInt32(data, index);
            index = Offset;
            IFD ifd0 = new IFD(data.Skip(index).ToArray());
        }


        private class IFD
        {
            public IFD(byte[] data)
            {
                int index = 0;
                short length = Converter.ToInt16(data, index);
                index += 2;
                Tags = new List<Tag>(length);
                for (int i = 0; i < length; i++)
                {
                    Tags.Add(new Tag(data.Skip(index).Take(12).ToArray()));
                    index += 12;
                }

                Data = data.Skip(index).ToArray();
            }

            public List<Tag> Tags { get; }
            public byte[] Data { get; }

            public class Tag
            {
                public Tag(byte[] data)
                {
                    int index = 0;
                    short id = Converter.ToInt16(data, index);
                    index += 2;
                    short type = Converter.ToInt16(data, index);
                    index += 2;
                    uint count = Converter.ToUInt32(data, index);
                    index += 4;
                    uint offset = Converter.ToUInt32(data, index);
                    TagID = id;
                    TagType = type;
                    Count = count;
                    Offset = offset;
                }

                public short TagID { get; }
                public short TagType { get; }
                public uint Count { get; }
                public uint Offset { get; }
            }
        }
    }
}