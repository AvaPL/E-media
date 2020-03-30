using System;
using System.Collections.Generic;

namespace PNGAnalyzer
{
    public class ChunkParser
    {
        public static List<Chunk> Parse(List<Chunk> chunks)
        {
            List<Chunk> result = new List<Chunk>();
            foreach (var chunk in chunks)
            {
                switch (chunk.Type)
                {
                    case "bKGD": result.Add(new bKGD(chunk));
                        break;
                    case "cHRM": result.Add(new cHRM(chunk));
                        break;
                    case "gAMA": result.Add(new gAMA(chunk));
                        break;
                    case "hIST": result.Add(new hIST(chunk));
                        break;
                    case "iCCP": result.Add(new iCCP(chunk));
                        break;
                    case "IDAT": result.Add(new IDAT(chunk));
                        break;
                    case "IEND": result.Add(new IEND(chunk));
                        break;
                    case "IHDR": result.Add(new IHDR(chunk));
                        break;
                    case "iTXt": result.Add(new iTXt(chunk));
                        break;
                    case "pHYs": result.Add(new pHYs(chunk));
                        break;
                    case "PLTE": result.Add(new PLTE(chunk));
                        break;
                    case "sBIT": result.Add(new sBIT(chunk));
                        break;
                    case "sPLT": result.Add(new sPLT(chunk));
                        break;
                    case "sRGB": result.Add(new sRGB(chunk));
                        break;
                    case "tEXt": result.Add(new tEXt(chunk));
                        break;
                    case "tIME": result.Add(new tIME(chunk));
                        break;
                    case "zTXt": result.Add(new zTXt(chunk));
                        break;
                    default: throw new FormatException("Invalid chunk type.");
                }
            }

            return result;
        }
    }
}