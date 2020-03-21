﻿using System;

namespace PNGAnalyzer
{
    public class cHRM : Chunk
    {
        public cHRM(string type, byte[] data, int crc) : base(type, data, crc)
        {
            if (type != "cHRM")
                throw new ArgumentException("Invalid chunk type passed to cHRM");
            ParseData(data);
        }

        public cHRM(Chunk chunk) : base(chunk)
        {
            if (chunk.Type != "cHRM")
                throw new ArgumentException("Invalid chunk type passed to cHRM");
            ParseData(chunk.Data);
        }

        public int  WhitePointX { get; private set; }
        public int WhitePointY { get; private set; }
        public int RedX { get; private set; }
        public int RedY { get; private set; }
        public int GreenX { get; private set; }
        public int GreenY { get; private set; }
        public int BlueX { get; private set; }
        public int BlueY { get; private set; }

        private void ParseData(byte[] data)
        {
            WhitePointX = Converter.ToInt32(data, 0);
            WhitePointY = Converter.ToInt32(data, 4);
            RedX = Converter.ToInt32(data, 8);
            RedY = Converter.ToInt32(data, 12);
            GreenX = Converter.ToInt32(data, 16);
            GreenY = Converter.ToInt32(data, 20);
            BlueX = Converter.ToInt32(data, 24);
            BlueY = Converter.ToInt32(data, 28);
        }

        public override string GetInfo()
        {
            return base.GetInfo() + "\n" + ToString();
        }

        public override string ToString()
        {
            return $"{nameof(WhitePointX)}: {WhitePointX}\n" + 
            $"{nameof(WhitePointY)}: {WhitePointY}\n" +
            $"{nameof(RedX)}: {RedX}\n" +
            $"{nameof(RedY)}: {RedY}\n" +
            $"{nameof(GreenX)}: {GreenX}\n" +
            $"{nameof(GreenY)}: {GreenY}\n" +
            $"{nameof(BlueX)}: {BlueX}\n +" +
            $"{nameof(BlueY)}: {BlueY}";
        }
    }
}