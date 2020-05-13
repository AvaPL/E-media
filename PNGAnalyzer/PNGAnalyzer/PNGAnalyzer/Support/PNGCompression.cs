using System;
using System.Collections.Generic;
using System.Linq;

// namespace PNGAnalyzer
// {
//     public class PNGCompression
//     {
//         public static List<Chunk> DecompressImage(List<Chunk> chunks)
//         {
//             IHDR header = (IHDR) chunks[0];
//             int bpp = CalculateBpp(header.ColorType, header.BitDepth);
//             int imageWidth = CalculateImageWidth(header.ColorType, header.BitDepth, header.Width);
//             List<IDAT> idats = chunks.Where(IsIDAT).Select(chunk => (IDAT) chunk).ToList();
//             byte[] bytes = IDATConverter.ConcatToBytes(idats);
//             byte[] decompressedBytes = ZlibCompression.Decompress(bytes);
//             byte[] decoded
//         }
//
//         public static List<Chunk> CompressImage(List<Chunk> chunks)
//         {
//         }
//
//         private static int CalculateBpp(int colorType, int bitDepth)
//         {
//             return colorType switch
//             {
//                 0 => (int) Math.Ceiling((decimal) bitDepth / 8),
//                 2 => 3 * bitDepth / 8,
//                 3 => (int) Math.Ceiling((decimal) bitDepth / 8),
//                 4 => 2 * bitDepth / 8,
//                 6 => 4 * bitDepth / 8,
//                 _ => throw new ArgumentException("Invalid color type")
//             };
//         }
//
//         private static int CalculateImageWidth(int colorType, int bitDepth, int imageWidth)
//         {
//             return colorType switch
//             {
//                 0 => imageWidth * bitDepth / 8,
//                 2 => 3 * imageWidth * bitDepth / 8,
//                 3 => imageWidth * bitDepth / 8,
//                 4 => 2 * imageWidth * bitDepth / 8,
//                 6 => 4 * imageWidth * bitDepth / 8,
//                 _ => throw new ArgumentException("Invalid color type")
//             };
//         }
//         
//         
//         private static bool IsIDAT(Chunk chunk)
//         {
//             return chunk.Type == "IDAT";
//         }
//     }
// }