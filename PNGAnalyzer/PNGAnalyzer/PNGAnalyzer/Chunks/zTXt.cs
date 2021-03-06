﻿using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace PNGAnalyzer
{
    public class zTXt : Chunk
    {
        public zTXt(byte[] data, uint crc) : base("IDAT", data, crc)
        {
            ParseData(data);
        }

        public zTXt(Chunk chunk) : base(chunk)
        {
            if (chunk.Type != "zTXt")
                throw new ArgumentException("Invalid chunk type passed to zTXt");
            ParseData(chunk.Data);
        }

        public string Keyword { get; private set; }
        public byte CompressionMethod { get; private set; }
        public string Text { get; private set; }

        private void ParseData(byte[] data)
        {
            Keyword = GetKeyword(data);
            int index = Keyword.Length + 1;
            CompressionMethod = data[index];
            index += 1;
            Text = Encoding.GetEncoding("ISO-8859-1").GetString(ZlibCompression.Decompress(data.Skip(index).ToArray()));
        }

        private string GetKeyword(byte[] data)
        {
            int nullSeparatorIndex = Array.IndexOf(data, (byte) 0);
            return Encoding.GetEncoding("ISO-8859-1").GetString(data, 0, nullSeparatorIndex);
        }

        public override string ToString()
        {
            return $"{base.ToString()}, " +
                   $"{nameof(Keyword)}: {Keyword}, " +
                   $"{nameof(CompressionMethod)}: {CompressionMethod}, " +
                   $"{nameof(Text)}: {Text}";
        }
    }
}