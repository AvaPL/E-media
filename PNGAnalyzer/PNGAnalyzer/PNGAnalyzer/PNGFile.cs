using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PNGAnalyzer
{
    public class PNGFile
    {
        public static List<Chunk> Read(string filePath)
        {
            byte[] bytes = File.ReadAllBytes(filePath);
            if (!IsPNG(bytes))
                throw new FormatException("The file is not a PNG file");
            return ReadChunks(bytes);
        }

        private static bool IsPNG(byte[] bytes)
        {
            return bytes.Take(8).SequenceEqual(new byte[] {137, 80, 78, 71, 13, 10, 26, 10});
        }

        private static List<Chunk> ReadChunks(byte[] bytes)
        {
            ChunkReader chunkReader = new ChunkReader(bytes);
            List<Chunk> chunks = new List<Chunk>();
            Chunk chunk;
            while ((chunk = chunkReader.ReadChunk()) != null)
                chunks.Add(chunk);
            return chunks;
        }

        public static void Write(string filePath, List<Chunk> chunks)
        {
            byte[] sequence = {137, 80, 78, 71, 13, 10, 26, 10};
            ChunkWriter chunkWriter = new ChunkWriter();
            foreach (var chunk in chunks)
                chunkWriter.Write(chunk);
            byte[] chunkBytes = chunkWriter.GetBytes();
            byte[] result = new byte[sequence.Length + chunkBytes.Length];
            sequence.CopyTo(result, 0);
            chunkBytes.CopyTo(result, sequence.Length);
            File.WriteAllBytes(filePath, result);
        }
    }
}