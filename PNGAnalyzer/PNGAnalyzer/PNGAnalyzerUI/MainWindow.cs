using System;
using System.Diagnostics;
using System.Windows.Forms;
using PNGAnalyzer;

namespace PNGAnalyzerUI
{
    public partial class MainWindow : Form
    {
        private const string FourierTransformPath =
            @"..\..\..\..\FourierTransform\dist\FourierTransform\FourierTransform.exe";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() != DialogResult.OK) return;
            FilepathTextBox.Text = OpenFileDialog.FileName;
            ChunksButton.Enabled = true;
            AnonymizeButton.Enabled = true;
            FourierTransformButton.Enabled = true;
        }

        private void ChunksButton_Click(object sender, EventArgs e)
        {
            Form imageInfo = new ImageInfo(FilepathTextBox.Text);
            imageInfo.Show();
        }

        private void AnonymizeButton_Click(object sender, EventArgs e)
        {
            if (SaveFileDialog.ShowDialog() == DialogResult.OK)
                Anonymizer.Anonymize(FilepathTextBox.Text, SaveFileDialog.FileName);
        }

        private void FourierTransformButton_Click(object sender, EventArgs e)
        {
            ExecuteFourierTransformCommand();
        }

        private void ExecuteFourierTransformCommand()
        {
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "cmd.exe",
                    Arguments = "/C " + FourierTransformPath + " " + FilepathTextBox.Text
                }
            };
            process.Start();
        }

        private void MicrosoftRSAButton_Click(object sender, EventArgs e)
        {
            Form encryptionWindow = new MicrosoftRSAEncryptionWindow();
            encryptionWindow.Show();
        }

        private void MyRSAButton_Click(object sender, EventArgs e)
        {
            Form encryptionWindow = new MyRSAEncryptionWindow();
            encryptionWindow.Show();
        }
    }
}