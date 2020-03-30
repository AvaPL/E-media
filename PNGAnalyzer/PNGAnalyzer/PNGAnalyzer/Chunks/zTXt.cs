using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace PNGAnalyzer
{
    public class zTXt : Chunk
    {
        public zTXt(string type, byte[] data, uint crc) : base(type, data, crc)
        {
            if(type != "zTXt")
                throw new ArgumentException("Invalid chunk type passed to zTXt");
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
        //public TextData DecompressedData { get; private set; }
        public string Text { get; private set; }

        private void ParseData(byte[] data)
        {
            Keyword = GetKeyword(data);
            int index = Keyword.Length + 1;
            CompressionMethod = data[index];
            index += 1;
            Text = DecompressData(data.Skip(index).ToArray());
            //if (Keyword == "Raw profile type APP1")
            //TODO:ADD EXIF PARSING  
            //else
            //    DecompressedData = new Description(DecompressData(data.Skip(index).ToArray()));
        }

        private string DecompressData(byte[] data)
        {
            using var compressedStream=new MemoryStream(data);
            using var decompressionStream = new GZipStream(compressedStream, CompressionMode.Decompress);
            using var resultStream = new MemoryStream();
            decompressionStream.CopyTo(resultStream);
            return Encoding.GetEncoding("ISO-8859-1").GetString(resultStream.ToArray());
        }
        
        private string GetKeyword(byte[] data)
        {
            int nullSeparatorIndex =Array.IndexOf(data, (byte)0);
            return Encoding.GetEncoding("ISO-8859-1").GetString(data, 0, nullSeparatorIndex);
        }
        
        public override string GetInfo()
        {
            return base.GetInfo() + "\n" + ToString();
        }
        
        public override string ToString()
        {
            return $"{nameof(Keyword)}: {Keyword}";
        }
        
        public class  TextData
        {
        }

        
        private class Description : TextData
        {
            public string DecompressedText { get; private set; }

            public Description(string decompressedText)
            {
                DecompressedText = decompressedText;
            }
        }

        
    }
}