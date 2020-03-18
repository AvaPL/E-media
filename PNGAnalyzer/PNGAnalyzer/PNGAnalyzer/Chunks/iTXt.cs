using System;
using System.Linq;
using System.Text;

namespace PNGAnalyzer
{
    public class iTXt : Chunk
    {
        public iTXt(string type, byte[] data, int crc) : base(type, data, crc)
        {
            ParseData(data);
        }

        public iTXt(Chunk chunk) : base(chunk)
        {
            ParseData(chunk.Data);
        }

        public string Keyword { get; private set; }
        public byte CompressionFlag { get; private set; }
        public byte CompressionMethod { get; private set; }
        public string LanguageTag { get; private set; }
        public string TranslatedKeyword { get; private set; }
        public Text InternationalText { get; private set; }

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
            InternationalText = ParseInternationalText(data, startIndex);
        }

        private string GetNullSeparatedString(byte[] data, Encoding encoding, int startIndex)
        {
            int nullSeparatorIndex = Array.IndexOf(data, 0, startIndex);
            return encoding.GetString(data, startIndex, nullSeparatorIndex - startIndex);
        }

        private Text ParseInternationalText(byte[] data, int startIndex)
        {
            if (CompressionFlag == 0)
            {
                string text = Encoding.UTF8.GetString(data, startIndex, data.Length - startIndex);
                return new Uncompressed(text);
            }
            else
            {
                byte[] text = data.Skip(startIndex).ToArray();
                return new Compressed(text);
            }
        }

        public override string GetInfo()
        {
            return base.GetInfo() + "\n" + ToString();
        }

        public override string ToString()
        {
            return
                $"{base.ToString()}\n" +
                $"{nameof(Keyword)}: {Keyword}\n" +
                $"{nameof(CompressionFlag)}: {CompressionFlag}\n" +
                $"{nameof(CompressionMethod)}: {CompressionMethod}\n" +
                $"{nameof(LanguageTag)}: {LanguageTag}\n" +
                $"{nameof(TranslatedKeyword)}: {TranslatedKeyword}\n" +
                $"{nameof(InternationalText)}: {InternationalText}";
        }

        public class Text
        {
        }

        private class Uncompressed : Text
        {
            public Uncompressed(string uncompressedText)
            {
                UncompressedText = uncompressedText;
            }

            public string UncompressedText { get; }

            public override string ToString()
            {
                return $"{nameof(UncompressedText)}: {UncompressedText}";
            }
        }

        private class Compressed : Text
        {
            public Compressed(byte[] compressedText)
            {
                CompressedText = compressedText;
            }

            public byte[] CompressedText { get; }

            public override string ToString()
            {
                return $"{nameof(CompressedText)}: {CompressedText}";
            }
        }
    }
}