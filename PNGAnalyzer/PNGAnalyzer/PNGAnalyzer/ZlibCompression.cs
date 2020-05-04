using System.IO;
using System.IO.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace PNGAnalyzer
{
    public class ZlibCompression
    {
        public static byte[] Decompress(byte[] bytes)
        {
            using var compressedStream = new MemoryStream(bytes);
            using var decompressionStream = new InflaterInputStream(compressedStream);
            using var resultStream = new MemoryStream();
            decompressionStream.CopyTo(resultStream);
            return resultStream.ToArray();
        }

        public static byte[] Compress(byte[] bytes)
        {
            using var decompressedStream = new MemoryStream(bytes);
            using var resultStream = new MemoryStream();
            using var compressionStream = new DeflaterOutputStream(resultStream);
            decompressedStream.CopyTo(compressionStream);
            compressionStream.Close();
            return resultStream.ToArray();
        }
    }
}