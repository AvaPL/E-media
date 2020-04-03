using System;
using System.Collections.Generic;
using System.Linq;

namespace PNGAnalyzer
{
    public class ChunkParser
    {
        public static List<Chunk> Parse(List<Chunk> chunks)
        {
            return chunks.Select(ParseChunk).ToList();
        }

        private static Chunk ParseChunk(Chunk chunk)
        {
            return chunk.Type switch
            {
                "bKGD" => new bKGD(chunk),
                "cHRM" => new cHRM(chunk),
                "eXIf" => new eXIf(chunk),
                "gAMA" => new gAMA(chunk),
                "hIST" => new hIST(chunk),
                "iCCP" => new iCCP(chunk),
                "IDAT" => new IDAT(chunk),
                "IEND" => new IEND(chunk),
                "IHDR" => new IHDR(chunk),
                "iTXt" => new iTXt(chunk),
                "pHYs" => new pHYs(chunk),
                "PLTE" => new PLTE(chunk),
                "sBIT" => new sBIT(chunk),
                "sPLT" => new sPLT(chunk),
                "sRGB" => new sRGB(chunk),
                "tEXt" => new tEXt(chunk),
                "tIME" => new tIME(chunk),
                "zTXt" => new zTXt(chunk),
                _ => chunk
            };
        }
    }
}