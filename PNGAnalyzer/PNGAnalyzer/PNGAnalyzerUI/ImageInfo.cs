using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PNGAnalyzer;

namespace PNGAnalyzerUI
{
    public partial class ImageInfo : Form
    {
        public ImageInfo(string imagePath)
        {
            InitializeComponent();
            PictureBox.ImageLocation = imagePath;
            FillChunkInfo(imagePath);
        }

        private void FillChunkInfo(string imagePath)
        {
            List<Chunk> chunks = ChunkParser.Parse(PNGFile.Read(imagePath));
            string chunkInfo = GetChunksInfo(chunks);
            InfoText.Text = chunkInfo;
        }

        private static string GetChunksInfo(List<Chunk> chunks)
        {
            return string.Join(Environment.NewLine, chunks.Select(GetChunkInfo));
        }

        private static string GetChunkInfo(Chunk chunk)
        {
            return chunk.GetInfo().Replace("\n", Environment.NewLine);
        }
    }
}