using System;

namespace PNGAnalyzer
{
    public class IEND : Chunk
    {
        public IEND(string type, byte[] data, uint crc) : base(type, data, crc)
                 {
                     if(type != "IEND")
                         throw new ArgumentException("Invalid chunk type passed to IEND");
                 }
         
                 public IEND(Chunk chunk) : base(chunk)
                 {
                     if (chunk.Type != "IEND")
                         throw new ArgumentException("Invalid chunk type passed to IEND");
                 }
    }
}