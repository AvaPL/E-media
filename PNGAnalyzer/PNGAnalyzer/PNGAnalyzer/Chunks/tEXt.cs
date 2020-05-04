using System;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Security.Permissions;
using System.Text;

namespace PNGAnalyzer
{
    public class tEXt : Chunk
    {
        public tEXt(byte[] data, uint crc) : base("tEXt", data, crc)
        {
            ParseData(data);
        }

        public tEXt(Chunk chunk) : base(chunk)
        {
            if (chunk.Type != "tEXt")
                throw new ArgumentException("Invalid chunk type passed to tEXt");
            ParseData(chunk.Data);
        }

        public string Keyword { get; set; }
        public string Text { get; set; }

        private void ParseData(byte[] data)
        {
            Encoding latin1 = Encoding.GetEncoding(28592);
            Keyword = latin1.GetString(data.TakeWhile(b => b != 0).ToArray());
            Text = latin1.GetString(data.Skip(Keyword.Length + 1).ToArray());
        }

        public override string ToString()
        {
            return $"{base.ToString()}, " +
                   $"{nameof(Keyword)}: {Keyword}, " +
                   $"{nameof(Text)}: {Text}";
        }
    }
}