using System;

namespace PNGAnalyzer
{
    public static class Converter
    {
        public static int ToInt32(byte[] value)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(value, 0, value.Length);
            return BitConverter.ToInt32(value, 0);
        }
        
        public static int ToInt32(byte[] value, int startIndex)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(value, startIndex, 4);
            return BitConverter.ToInt32(value, startIndex);
        }
    }
}