using System.Collections.Generic;

namespace PNGAnalyzer.BlockCiphers
{
    public class BlockCipherImage
    {
        private readonly IBlockCipher blockCipher;

        public BlockCipherImage(IBlockCipher blockCipher)
        {
            this.blockCipher = blockCipher;
        }
        
        public List<Chunk> Cipher(List<Chunk> chunks)
        {
            byte[] decompressedBytes = BlockCipherSupport.DecompressIDATs(chunks);
            byte[] cipheredBytes = blockCipher.Cipher(decompressedBytes);
            List<Chunk> resultIdats = BlockCipherSupport.CompressIDATs(cipheredBytes);
            List<Chunk> resultChunks = BlockCipherSupport.SwapIDATs(chunks, resultIdats);
            return resultChunks;
        }
        
        public List<Chunk> Decipher(List<Chunk> chunks)
        {
            byte[] decompressedBytes = BlockCipherSupport.DecompressIDATs(chunks);
            byte[] decipheredBytes = blockCipher.Decipher(decompressedBytes);
            List<Chunk> resultIdats = BlockCipherSupport.CompressIDATs(decipheredBytes);
            List<Chunk> resultChunks = BlockCipherSupport.SwapIDATs(chunks, resultIdats);
            return resultChunks;
        }
    }
}