using System;
using System.Security.Cryptography;
using System.Windows.Forms;
using PNGAnalyzer.RSA;

namespace PNGAnalyzerUI
{
    public partial class EncryptionWindow : Form
    {
        public EncryptionWindow(string imageFilePath, EncryptionType encryptionType)
        {
            InitializeComponent();
            ImagePath = imageFilePath;
            EncryptingObjectType = encryptionType;
        }

        private string ImagePath;
        private EncryptionType EncryptingObjectType;

        private IRSA CreateEncryptionObject(RSAParameters parameters, EncryptionType encryptionType)
        {
            switch (encryptionType)
            {
                case EncryptionType.MicrosoftEncryption:
                    return new MicrosoftRSA(new RSAParameters());
                case EncryptionType.PajaceTeamEncryption:
                    throw new System.NotImplementedException();
                default:
                    throw new System.NotImplementedException("Unknown encryption type");
            }
        }

        private void EncryptButton_Click(object sender, EventArgs e)
        {
            
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