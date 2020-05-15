using System;
using System.Collections.Generic;
using System.Linq;

namespace PNGAnalyzer
{
    public class ImageStructureInfo
    {
        public static int CalculateBytesPerPixel(IHDR header)
        {
            return header.ColorType switch
            {
                0 => (int) Math.Ceiling((decimal) header.BitDepth / 8),
                2 => 3 * header.BitDepth / 8,
                3 => (int) Math.Ceiling((decimal) header.BitDepth / 8),
                4 => 2 * header.BitDepth / 8,
                6 => 4 * header.BitDepth / 8,
                _ => throw new ArgumentException("Invalid color type")
            };
        }

        public static int CalculateImageWidth(IHDR header)
        {
            return header.ColorType switch
            {
                0 => header.Width * header.BitDepth / 8,
                2 => 3 * header.Width * header.BitDepth / 8,
                3 => header.Width * header.BitDepth / 8,
                4 => 2 * header.Width * header.BitDepth / 8,
                6 => 4 * header.Width * header.BitDepth / 8,
                _ => throw new ArgumentException("Invalid color type")
            };
        }
    }
}