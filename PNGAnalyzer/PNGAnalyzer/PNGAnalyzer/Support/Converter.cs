using System;

namespace PNGAnalyzer
{
    public static class Converter
    {
        public static int ToInt32(byte[] value)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(value, 0, 4);
            return BitConverter.ToInt32(value, 0);
        }

        public static int ToInt32(byte[] value, int startIndex)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(value, startIndex, 4);
            return BitConverter.ToInt32(value, startIndex);
        }

        public static short ToInt16(byte[] value)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(value, 0, 2);
            return BitConverter.ToInt16(value, 0);
        }

        public static short ToInt16(byte[] value, int startIndex)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(value, startIndex, 2);
            return BitConverter.ToInt16(value, startIndex);
        }

        public static uint ToUInt32(byte[] value, int startIndex)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(value, startIndex, 4);
            return BitConverter.ToUInt32(value, startIndex);
        }
    }
}