using System.Collections.Generic;
using System.Linq;

namespace PNGAnalyzer
{
    public class IDATConverter
    {
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
    }
}