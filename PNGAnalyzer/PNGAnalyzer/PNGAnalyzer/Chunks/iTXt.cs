using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace PNGAnalyzer
{
    public class iTXt : Chunk
    {
        public iTXt(byte[] data, uint crc) : base("iTXt", data, crc)
        {
            ParseData(data);
        }

        public iTXt(Chunk chunk) : base(chunk)
        {
            if (chunk.Type != "iTXt")
                throw new ArgumentException("Invalid chunk type passed to iTXt");
            ParseData(chunk.Data);
        }

        public string Keyword { get; private set; }
        public byte CompressionFlag { get; private set; }
        public byte CompressionMethod { get; private set; }
        public string LanguageTag { get; private set; }
        public string TranslatedKeyword { get; private set; }
        public string Text { get; private set; }

        private void ParseData(byte[] data)
        {
            Keyword = GetNullSeparatedString(data, Encoding.UTF8, 0);
            CompressionFlag = data[Keyword.Length + 1];
            CompressionMethod = data[Keyword.Length + 2];
            int startIndex = Keyword.Length + 3;
            LanguageTag = GetNullSeparatedString(data, Encoding.ASCII, startIndex);
            startIndex += LanguageTag.Length + 1;
            TranslatedKeyword = GetNullSeparatedString(data, Encoding.UTF8, startIndex);
            startIndex += TranslatedKeyword.Length + 1;
            Text = ParseInternationalText(data, startIndex);
        }

        private string GetNullSeparatedString(byte[] data, Encoding encoding, int startIndex)
        {
            int nullSeparatorIndex = Array.IndexOf(data, (byte) 0, startIndex);
            return encoding.GetString(data, startIndex, nullSeparatorIndex - startIndex);
        }

        private string ParseInternationalText(byte[] data, int startIndex)
        {
            return CompressionFlag == 0
                ? Encoding.UTF8.GetString(data, startIndex, data.Length - startIndex)
                : Encoding.UTF8.GetString(ZlibCompression.Decompress(data.Skip(startIndex).ToArray()));
        }

        public override string ToString()
        {
            return
                $"{base.ToString()}, " +
                $"{nameof(Keyword)}: {Keyword}, " +
                $"{nameof(CompressionFlag)}: {CompressionFlag}, " +
                $"{nameof(CompressionMethod)}: {CompressionMethod}, " +
                $"{nameof(LanguageTag)}: {LanguageTag}, " +
                $"{nameof(TranslatedKeyword)}: {TranslatedKeyword}, " +
                $"{nameof(Text)}: {Text}";
        }
    }
}