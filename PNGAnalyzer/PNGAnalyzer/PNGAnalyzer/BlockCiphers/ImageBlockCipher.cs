using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace PNGAnalyzer.BlockCiphers
{
    public class ImageBlockCipher
    {
        private readonly IBlockCipher blockCipher;

        public ImageBlockCipher(IBlockCipher blockCipher)
        {
            this.blockCipher = blockCipher;
        }

        public void CipherWithFiltering(string imagePath, string pathToSave)
        {
            Bitmap bmp = new Bitmap(imagePath);

            Rectangle rectangle = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData =
                bmp.LockBits(rectangle, ImageLockMode.ReadWrite,
                    bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;

            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgbValues = new byte[bytes];
            Marshal.Copy(ptr, rgbValues, 0, bytes);
            byte[] cipheredBytes = blockCipher.Cipher(rgbValues);
            Bitmap bitmapToSave = new Bitmap(blockCipher.GetResizeRatio() * bmp.Width, bmp.Height,
                blockCipher.GetResizeRatio() * bmpData.Stride, bmp.PixelFormat,
                Marshal.UnsafeAddrOfPinnedArrayElement(cipheredBytes, 0));
            bitmapToSave.Save(pathToSave, ImageFormat.Png);
        }

        public void DecipherWithFiltering(string imagePath, string pathToSave)
        {
            Bitmap bmp = new Bitmap(imagePath);

            Rectangle rectangle = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData =
                bmp.LockBits(rectangle, ImageLockMode.ReadWrite,
                    bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;

            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgbValues = new byte[bytes];
            Marshal.Copy(ptr, rgbValues, 0, bytes);
            byte[] decipheredBytes = blockCipher.Decipher(rgbValues);
            Bitmap bitmapToSave = new Bitmap(bmp.Width / blockCipher.GetResizeRatio(), bmp.Height,
                bmpData.Stride / blockCipher.GetResizeRatio(), bmp.PixelFormat,
                Marshal.UnsafeAddrOfPinnedArrayElement(decipheredBytes, 0));
            bitmapToSave.Save(pathToSave, ImageFormat.Png);
        }

        public List<Chunk> CipherWithoutFiltering(List<Chunk> chunks)
        {
            byte[] decompressedBytes = BlockCipherSupport.DecompressIDATs(chunks);
            byte[] cipheredBytes = blockCipher.Cipher(decompressedBytes);
            List<Chunk> resultIdats = BlockCipherSupport.CompressIDATs(cipheredBytes);
            List<Chunk> resultChunks = BlockCipherSupport.SwapIDATs(chunks, resultIdats);
            return resultChunks;
        }

        public List<Chunk> DecipherWithoutFiltering(List<Chunk> chunks)
        {
            byte[] decompressedBytes = BlockCipherSupport.DecompressIDATs(chunks);
            byte[] decipheredBytes = blockCipher.Decipher(decompressedBytes);
            List<Chunk> resultIdats = BlockCipherSupport.CompressIDATs(decipheredBytes);
            List<Chunk> resultChunks = BlockCipherSupport.SwapIDATs(chunks, resultIdats);
            return resultChunks;
        }
    }
}