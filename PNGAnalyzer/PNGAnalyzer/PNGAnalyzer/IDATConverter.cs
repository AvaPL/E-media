using System;
using System.Collections.Generic;
using System.Linq;
using Force.Crc32;

namespace PNGAnalyzer
{
    public class IDATConverter
    {
        private static bool IsIDAT(Chunk chunk)
        {
            return chunk.Type == "IDAT";
        }

        public static byte[] ConcatToBytes(List<IDAT> idats)
        {
            long bytesCount = idats.Select(idat => idat.Data.Length).Sum();
            byte[] result = new byte[bytesCount];
            long resultIndex = 0;
            foreach (var idat in idats)
            {
                idat.Data.CopyTo(result, resultIndex);
                resultIndex += idat.Data.Length;
            }

            return result;
        }

        public static List<IDAT> SplitToIDATs(byte[] bytes)
        {
            if (bytes.Length == 0)
                return new List<IDAT> {CreateIDAT(bytes, 0)};

            List<IDAT> result = new List<IDAT>();
            for (int i = 0; i < bytes.Length; i += short.MaxValue)
            {
                IDAT idat = CreateIDAT(bytes, i);
                result.Add(idat);
            }

            return result;
        }

        private static IDAT CreateIDAT(byte[] bytes, int startIndex)
        {
            byte[] data = bytes.Skip(startIndex).Take(short.MaxValue).ToArray();
            uint crc = Crc32Algorithm.Compute(data);
            IDAT idat = new IDAT(data, crc);
            return idat;
        }
    }
}