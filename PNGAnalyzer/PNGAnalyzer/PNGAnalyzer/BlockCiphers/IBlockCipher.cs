using System.Collections.Generic;

namespace PNGAnalyzer.BlockCiphers
{
    public interface IBlockCipher
    {
        byte[] Cipher(byte[] data);
        byte[] Decipher(byte[] data);
        int GetResizeRatio();
    }
}