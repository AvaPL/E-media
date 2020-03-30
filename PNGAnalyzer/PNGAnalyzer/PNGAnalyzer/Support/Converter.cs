using System;
using System.Linq;

namespace PNGAnalyzer
{
    public static class Converter
    {
        public static int ToInt32(byte[] value)
        {
            return BitConverter.ToInt32(BitConverter.IsLittleEndian ? value.Take(4).Reverse().ToArray() : value, 0);
        }

        public static uint ToUInt32(byte[] value)
        {
            return BitConverter.ToUInt32(BitConverter.IsLittleEndian ? value.Take(4).Reverse().ToArray() : value, 0);
        }

        public static int ToInt32(byte[] value, int startIndex)
        {
            return BitConverter.IsLittleEndian
                ? BitConverter.ToInt32(value.Skip(startIndex).Take(4).Reverse().ToArray(), 0)
                : BitConverter.ToInt32(value, startIndex);
        }

        public static short ToInt16(byte[] value)
        {
            return BitConverter.ToInt16(BitConverter.IsLittleEndian ? value.Take(2).Reverse().ToArray() : value, 0);
        }

        public static short ToInt16(byte[] value, int startIndex)
        {
            return BitConverter.IsLittleEndian
                ? BitConverter.ToInt16(value.Skip(startIndex).Take(2).Reverse().ToArray(), 0)
                : BitConverter.ToInt16(value, startIndex);
        }

        public static uint ToUInt32(byte[] value, int startIndex)
        {
            return BitConverter.IsLittleEndian
                ? BitConverter.ToUInt32(value.Skip(startIndex).Take(4).Reverse().ToArray(), 0)
                : BitConverter.ToUInt32(value, startIndex);
        }

        public static byte[] GetBytes(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return bytes;
        }

        public static byte[] GetBytes(uint value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return bytes;
        }
    }
}