using System;
using System.Linq;
using System.Text;

namespace PNGAnalyzer
{
    public class iCCP : Chunk
    {
        //TODO: Throw exception on wrong type input.
        public iCCP(string type, byte[] data, int crc) : base(type, data, crc)
        {
            ParseData(data);
        }

        public iCCP(Chunk chunk) : base(chunk)
        {
            ParseData(chunk.Data);
        }

        public string ProfileName { get; private set; }
        public byte CompressionMethod { get; private set; }
        public byte[] CompressedProfile { get; private set; }

        private void ParseData(byte[] data)
        {
            int nullSeparatorIndex = Array.IndexOf(data, 0);
            ProfileName = Encoding.ASCII.GetString(data, 0, nullSeparatorIndex);
            CompressionMethod = data[nullSeparatorIndex + 1];
            CompressedProfile = data.Skip(nullSeparatorIndex + 2).ToArray();
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