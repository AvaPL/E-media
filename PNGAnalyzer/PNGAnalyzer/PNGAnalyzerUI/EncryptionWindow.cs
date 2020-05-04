using System;
using System.Windows.Forms;

namespace PNGAnalyzerUI
{
    public partial class EncryptionWindow : Form
    {
        public EncryptionWindow(string imageFilePath)
        {
            InitializeComponent();
            imagePath = imageFilePath;
        }

        private string imagePath;

        private void EncryptButton_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void DecryptButton_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void GenerateKeysButton_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}