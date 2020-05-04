using System;
using System.Linq;
using System.Text;

namespace PNGAnalyzer
{
    public class iCCP : Chunk
    {
        public iCCP(byte[] data, uint crc) : base("iCCP", data, crc)
        {
            ParseData(data);
        }

        public iCCP(Chunk chunk) : base(chunk)
        {
            if (chunk.Type != "iCCP")
                throw new ArgumentException("Invalid chunk type passed to iCCP");
            ParseData(chunk.Data);
        }

        public string ProfileName { get; private set; }
        public byte CompressionMethod { get; private set; }
        public byte[] CompressedProfile { get; private set; }

        private void ParseData(byte[] data)
        {
            int nullSeparatorIndex = Array.IndexOf(data, (byte) 0);
            ProfileName = Encoding.GetEncoding("iso-8859-1").GetString(data, 0, nullSeparatorIndex);
            CompressionMethod = data[nullSeparatorIndex + 1];
            CompressedProfile = data.Skip(nullSeparatorIndex + 2).ToArray();
        }

        public override string ToString()
        {
            return $"{base.ToString()}, " +
                   $"{nameof(ProfileName)}: {ProfileName}, " +
                   $"{nameof(CompressionMethod)}: {CompressionMethod}, " +
                   $"{nameof(CompressedProfile)}: {CompressedProfile.Length} bytes";
        }
    }
}