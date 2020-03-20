using System;
using System.Linq;
using System.Text;

namespace PNGAnalyzer
{
    public class zTXt : Chunk
    {
        public zTXt(string type, byte[] data, int crc) : base(type, data, crc)
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
        public byte[] CompressedText { get; private set; }

        private void ParseData(byte[] data)
        {
            Keyword = GetKeyword(data);
            int index = Keyword.Length + 1;
            CompressionMethod = data[index];
            index += 1;
            CompressedText = data.Skip(index).ToArray();
        }
        
        private string GetKeyword(byte[] data)
        {
            int nullSeparatorIndex = Array.IndexOf(data, 0, 0);
            return Encoding.GetEncoding("ISO-8859-1").GetString(data, 0, nullSeparatorIndex);
        }
        
        public override string GetInfo()
        {
            return base.GetInfo() + "\n" + ToString();
        }
        
        public override string ToString()
        {
            return $"{nameof(Keyword)}: {Keyword}\n";
        }
    }
}