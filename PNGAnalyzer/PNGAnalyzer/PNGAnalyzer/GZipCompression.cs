﻿using System.IO;
using System.IO.Compression;

namespace PNGAnalyzer
{
    public class GZipCompression
    {
        public static byte[] Decompress(byte[] bytes)
        {
            using var compressedStream = new MemoryStream(bytes);
            using var decompressionStream = new GZipStream(compressedStream, CompressionMode.Decompress);
            using var resultStream = new MemoryStream();
            decompressionStream.CopyTo(resultStream);
            return resultStream.ToArray();
        }

        public static byte[] Compress(byte[] bytes)
        {
            //TODO: Change names.
            using var originalFileStream = new MemoryStream(bytes);
            using var compressedFileStream = new MemoryStream();
            using var compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress);
            originalFileStream.CopyTo(compressionStream);
            return compressedFileStream.ToArray();
        }
    }
}