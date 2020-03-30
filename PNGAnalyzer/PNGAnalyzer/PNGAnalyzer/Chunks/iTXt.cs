using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace PNGAnalyzer
{
    public class iTXt : Chunk
    {
        public iTXt(string type, byte[] data, uint crc) : base(type, data, crc)
        {
            if (type != "iTXt")
                throw new ArgumentException("Invalid chunk type passed to iTXt");
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
                : DecompressData(data.Skip(startIndex).ToArray());
        }

        private string DecompressData(byte[] data)
        {
            using var compressedStream = new MemoryStream(data);
            using var decompressionStream = new GZipStream(compressedStream, CompressionMode.Decompress);
            using var resultStream = new MemoryStream();
            decompressionStream.CopyTo(resultStream);
            return Encoding.UTF8.GetString(resultStream.ToArray());
        }

        public override string GetInfo()
        {
            return base.GetInfo() + "\n" + ToString();
        }

        public override string ToString()
        {
            return
                $"{nameof(Keyword)}: {Keyword}\n" +
                $"{nameof(CompressionFlag)}: {CompressionFlag}\n" +
                $"{nameof(CompressionMethod)}: {CompressionMethod}\n" +
                $"{nameof(LanguageTag)}: {LanguageTag}\n" +
                $"{nameof(TranslatedKeyword)}: {TranslatedKeyword}\n" +
                $"{nameof(Text)}: {Text}";
        }
    }
}