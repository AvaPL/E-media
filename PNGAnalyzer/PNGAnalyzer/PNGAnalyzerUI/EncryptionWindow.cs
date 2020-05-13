using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;
using PNGAnalyzer.BlockCiphers;
using PNGAnalyzer.RSA;

namespace PNGAnalyzerUI
{
    public partial class EncryptionWindow : Form
    {
        private static readonly RSAMethod[] RSAMethods = {RSAMethod.MicrosoftRSA, RSAMethod.MyRSA};

        private static readonly BlockCipherMethod[] MicrosoftRSABlockCipherMethods =
        {
            BlockCipherMethod.ElectronicCodebook, BlockCipherMethod.CipherBlockChaining
        };

        private static readonly BlockCipherMethod[] MyRSABlockCipherMethods =
        {
            BlockCipherMethod.ElectronicCodebook, BlockCipherMethod.CipherBlockChaining,
            BlockCipherMethod.PropagatingCipherBlockChaining, BlockCipherMethod.CipherFeedback,
            BlockCipherMethod.OutputFeedback, BlockCipherMethod.Counter
        };

        private static readonly int[] KeySizes = {512, 1024, 2048};

        private string imagePath;

        public EncryptionWindow(string imagePath)
        {
            InitializeComponent();
            this.imagePath = imagePath;
            MethodComboBox.Items.AddRange(ToString(RSAMethods));
            KeySizeComboBox.Items.AddRange(ToString(KeySizes));
        }

        private object[] ToString(RSAMethod[] rsaMethods)
        {
            return rsaMethods.Select(method => (object) method.ToMethodString()).ToArray();
        }

        private object[] ToString(BlockCipherMethod[] blockCipherMethods)
        {
            return blockCipherMethods.Select(method => (object) method.ToMethodString()).ToArray();
        }

        private object[] ToString(int[] keySizes)
        {
            return keySizes.Select(method => (object) method.ToString()).ToArray();
        }

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

        private void MethodComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            BlockCipherComboBox.Enabled = true;
            if (MethodComboBox.SelectedItem.ToString() == RSAMethod.MicrosoftRSA.ToMethodString())
                SelectedIndexChangedToMicrosoftRSA();
            else if (MethodComboBox.SelectedItem.ToString() == RSAMethod.MyRSA.ToMethodString())
                SelectedIndexChangedToMyRSA();
        }

        private void SelectedIndexChangedToMicrosoftRSA()
        {
            BlockCipherComboBox.Items.Clear();
            ClearKeyTextBoxes();
            BlockCipherComboBox.Items.AddRange(ToString(MicrosoftRSABlockCipherMethods));
            SetKeyTextBoxesReadOnly(true);
            SetKeyButtonsEnabled(false);
        }

        private void SelectedIndexChangedToMyRSA()
        {
            BlockCipherComboBox.Items.Clear();
            BlockCipherComboBox.Items.AddRange(ToString(MyRSABlockCipherMethods));
            SetKeyTextBoxesReadOnly(false);
        }

        private void ClearKeyTextBoxes()
        {
            ModulusTextBox.Clear();
            PublicKeyExponentTextBox.Clear();
            PrivateKeyExponentTextBox.Clear();
        }

        private void SetKeyTextBoxesReadOnly(bool isReadOnly)
        {
            ModulusTextBox.ReadOnly = isReadOnly;
            PublicKeyExponentTextBox.ReadOnly = isReadOnly;
            PrivateKeyExponentTextBox.ReadOnly = isReadOnly;
        }

        private void BlockCipherComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO: Change behaviour in case of Microsoft RSA.
            SetKeyButtonsEnabled(true);
        }

        private void SetKeyButtonsEnabled(bool isEnabled)
        {
            GenerateKeysButton.Enabled = isEnabled;
            EncryptButton.Enabled = isEnabled;
            DecryptButton.Enabled = isEnabled;
        }
    }
}