using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Extreme.DataAnalysis;

namespace PNGAnalyzer
{
    public class PNGFiltering
    {
        public static byte[] DecodeImage(byte[] data, int imageWidth, int bpp)
        {
            List<byte[]> imageLines = new List<byte[]>();
            for (int i = 0; i < data.Length; i += imageWidth + 1)
                imageLines.Add(data.Skip(i).Take(imageWidth + 1).ToArray());

            for (int i = 0; i < imageLines.Count; i++)
            {
                byte[] upperLine = i > 0 ? imageLines[i - 1] : new byte[imageWidth + 1];
                switch (imageLines[i][0])
                {
                    case 0:
                        break;
                    case 1:
                        imageLines[i] = RevertSub(imageLines[i], bpp);
                        break;
                    case 2:
                        imageLines[i] = RevertUp(imageLines[i], upperLine);
                        break;
                    case 3:
                        imageLines[i] = RevertAverage(imageLines[i], upperLine, bpp);
                        break;
                    case 4:
                        imageLines[i] = RevertPaeth(imageLines[i], upperLine, bpp);
                        break;
                }
            }

            return ConcatenateLines(imageLines);
        }

        public static byte[] EncodeImage(byte[] data, int imageWidth, int bpp)
        {
            List<byte[]> imageLines = new List<byte[]>();
            byte[] encodedData = new byte[data.Length];
            for (int i = 0; i < data.Length; i += imageWidth + 1)
                imageLines.Add(data.Skip(i).Take(imageWidth + 1).ToArray());

            for (int i = 0; i < imageLines.Count; i++)
            {
                byte[] upperLine = i > 0 ? imageLines[i - 1] : new byte[imageWidth + 1];
                int index = i * (imageWidth + 1);
                switch (imageLines[i][0])
                {
                    case 0:
                        imageLines[i].CopyTo(encodedData, index);
                        break;
                    case 1:
                        Sub(imageLines[i], bpp).CopyTo(encodedData, index);
                        break;
                    case 2:
                        Up(imageLines[i], upperLine).CopyTo(encodedData, index);
                        break;
                    case 3:
                        Average(imageLines[i], upperLine, bpp).CopyTo(encodedData, index);
                        break;
                    case 4:
                        Paeth(imageLines[i], upperLine, bpp).CopyTo(encodedData, index);
                        break;
                }
            }

            return encodedData;
        }

        private static byte[] Sub(byte[] line, int bpp)
        {
            byte[] encodedLine = new byte[line.Length];
            encodedLine[0] = line[0];
            for (int i = 1; i < line.Length; i++)
            {
                var predictedValue = i > bpp ? line[i - bpp] : (byte) 0;
                encodedLine[i] = (byte) (line[i] - predictedValue);
            }

            return encodedLine;
        }

        private static byte[] RevertSub(byte[] encodedLine, int bpp)
        {
            byte[] decodedLine = new byte[encodedLine.Length];
            decodedLine[0] = encodedLine[0];
            for (int i = 1; i < encodedLine.Length; i++)
            {
                var predictedValue = i > bpp ? decodedLine[i - bpp] : (byte) 0;
                decodedLine[i] = (byte) (encodedLine[i] + predictedValue);
            }

            return decodedLine;
        }

        private static byte[] Up(byte[] line, byte[] upperLine)
        {
            byte[] encodedLine = new byte[line.Length];
            encodedLine[0] = line[0];
            for (int i = 1; i < line.Length; i++)
            {
                encodedLine[i] = (byte) (line[i] - upperLine[i]);
            }

            return encodedLine;
        }

        private static byte[] RevertUp(byte[] encodedLine, byte[] upperLine)
        {
            byte[] decodedLine = new byte[encodedLine.Length];
            decodedLine[0] = encodedLine[0];
            for (int i = 1; i < encodedLine.Length; i++)
            {
                decodedLine[i] = (byte) (encodedLine[i] + upperLine[i]);
            }

            return decodedLine;
        }

        private static byte[] Average(byte[] line, byte[] upperLine, int bpp)
        {
            byte[] encodedLine = new byte[line.Length];
            encodedLine[0] = line[0];
            for (int i = 1; i < line.Length; i++)
            {
                var leftPixel = i > bpp ? line[i - bpp] : (byte) 0;
                int mean = (leftPixel + upperLine[i]) / 2;
                encodedLine[i] = (byte) (line[i] - mean);
            }

            return encodedLine;
        }

        private static byte[] RevertAverage(byte[] encodedLine, byte[] upperLine, int bpp)
        {
            byte[] decodedLine = new byte[encodedLine.Length];
            decodedLine[0] = encodedLine[0];
            for (int i = 1; i < encodedLine.Length; i++)
            {
                var leftPixel = i > bpp ? decodedLine[i - bpp] : (byte) 0;
                int mean = (leftPixel + upperLine[i]) / 2;
                decodedLine[i] = (byte) (encodedLine[i] + mean);
            }

            return decodedLine;
        }

        private static byte[] Paeth(byte[] line, byte[] upperLine, int bpp)
        {
            byte[] encodedLine = new byte[line.Length];
            encodedLine[0] = line[0];
            for (int i = 1; i < line.Length; i++)
            {
                var leftPixel = i > bpp ? line[i - bpp] : (byte) 0;
                var upperLeftPixel = i > bpp ? upperLine[i - bpp] : (byte) 0;
                encodedLine[i] = (byte) (line[i] - PaethPredictor(leftPixel, upperLine[i], upperLeftPixel));
            }

            return encodedLine;
        }

        private static byte[] RevertPaeth(byte[] encodedLine, byte[] upperLine, int bpp)
        {
            byte[] decodedLine = new byte[encodedLine.Length];
            decodedLine[0] = encodedLine[0];
            for (int i = 1; i < encodedLine.Length; i++)
            {
                var leftPixel = i > bpp ? decodedLine[i - bpp] : (byte) 0;
                var upperLeftPixel = i > bpp ? upperLine[i - bpp] : (byte) 0;
                decodedLine[i] = (byte) (encodedLine[i] + PaethPredictor(leftPixel, upperLine[i], upperLeftPixel));
            }

            return decodedLine;
        }

        private static byte PaethPredictor(byte a, byte b, byte c)
        {
            int p = a + b + c;
            int pa = Math.Abs(p - a);
            int pb = Math.Abs(p - b);
            int pc = Math.Abs(p - c);
            if (pa <= pb && pa <= pc)
                return a;
            if (pb <= pc)
                return b;
            return c;
        }

        private static byte[] ConcatenateLines(List<byte[]> lines)
        {
            int dataLength = lines.Sum(line => line.Length);
            byte[] data = new byte[dataLength];
            int index = 0;
            foreach (byte[] line in lines)
            {
                line.CopyTo(data, index);
                index += line.Length;
            }

            return data;
        }
    }
}