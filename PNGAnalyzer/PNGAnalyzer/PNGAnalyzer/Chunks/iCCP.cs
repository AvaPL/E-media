using System;
using System.Linq;
using System.Text;

namespace PNGAnalyzer
{
    public class iCCP : Chunk
    {
        public string ProfileName { get; set; }
        public byte CompressionMethod { get; set; }
        public byte[] CompressedProfile { get; set; }

        public iCCP(string type, byte[] data, int crc) : base(type, data, crc)
        {
            ParseData(data);
        }

        private void ParseData(byte[] data)
        {
            int nullSeparatorIndex = Array.IndexOf(data, 0);
            ProfileName = Encoding.ASCII.GetString(data, 0, nullSeparatorIndex);
            CompressionMethod = data[nullSeparatorIndex + 1];
            CompressedProfile = data.Skip(nullSeparatorIndex + 2).ToArray();
        }

        public iCCP(Chunk chunk) : base(chunk)
        {
            ParseData(chunk.Data);
        }

        public override string GetInfo()
        {
            return base.GetInfo() + "\n" + ToString();
        }

        public override string ToString()
        {
            return
                $"{nameof(ProfileName)}: {ProfileName}\n" +
                $"{nameof(CompressionMethod)}: {CompressionMethod}\n" +
                $"{nameof(CompressedProfile)}: {CompressedProfile}";
        }
    }
}