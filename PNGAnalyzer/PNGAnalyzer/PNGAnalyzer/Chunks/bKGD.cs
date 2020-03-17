using System;

namespace PNGAnalyzer
{
    public class bKGD : Chunk
    {
        //TODO: Throw exception on wrong type input.
        public bKGD(string type, byte[] data, int crc) : base(type, data, crc)
        {
            ParseData(data);
        }

        public bKGD(Chunk chunk) : base(chunk)
        {
            ParseData(chunk.Data);
        }

        public Color BackgroundColor { get; private set; }

        private void ParseData(byte[] data)
        {
            BackgroundColor = data.Length switch
            {
                1 => new Indexed(data[0]), //Type 3
                2 => new Grayscale(Converter.ToInt16(data)), //Types 0 and 4
                6 => new RGB(Converter.ToInt16(data, 0), Converter.ToInt16(data, 2),
                    Converter.ToInt16(data, 4)), //Types 2 and 6
                _ => throw new FormatException("Background color does not match any available format")
            };
        }

        public override string GetInfo()
        {
            return base.GetInfo() + "\n" + ToString();
        }

        public override string ToString()
        {
            return $"{base.ToString()}, {nameof(BackgroundColor)}: {BackgroundColor}";
        }

        public class Color
        {
        }

        private class Indexed : Color
        {
            public Indexed(byte paletteIndex)
            {
                PaletteIndex = paletteIndex;
            }

            public byte PaletteIndex { get; }

            public override string ToString()
            {
                return $"{nameof(PaletteIndex)}: {PaletteIndex}";
            }
        }

        private class Grayscale : Color
        {
            public Grayscale(short gray)
            {
                Gray = gray;
            }

            public short Gray { get; }

            public override string ToString()
            {
                return $"{nameof(Gray)}: {Gray}";
            }
        }

        private class RGB : Color
        {
            public RGB(short red, short green, short blue)
            {
                Red = red;
                Green = green;
                Blue = blue;
            }

            public short Red { get; }
            public short Green { get; }
            public short Blue { get; }

            public override string ToString()
            {
                return
                    $"{nameof(Red)}: {Red}\n" +
                    $"{nameof(Green)}: {Green}\n" +
                    $"{nameof(Blue)}: {Blue}";
            }
        }
    }
}