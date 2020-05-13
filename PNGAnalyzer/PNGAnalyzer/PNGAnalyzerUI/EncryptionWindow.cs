using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Windows.Forms;
using PNGAnalyzer;
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
        private RSAMethod? rsaMethod;
        private BlockCipherMethod? blockCipherMethod;
        private int? keySize;
        private RSAParameters? rsaParameters;
        private bool haveParametersChangedInTextBoxes;

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
            // TODO: Encrypt in a separate thread.
            if (haveParametersChangedInTextBoxes)
                UpdateRSAParametersUsingTextBoxes();
            throw new System.NotImplementedException();
        }

        private void UpdateRSAParametersUsingTextBoxes()
        {
            BigInteger modulus = BigInteger.Parse(ModulusTextBox.Text);
            BigInteger exponent = BigInteger.Parse(PublicKeyExponentTextBox.Text);
            BigInteger d = BigInteger.Parse(PrivateKeyExponentTextBox.Text);
            rsaParameters = new RSAParameters
            {
                Modulus = BigIntegerExtensions.UnsignedToBytes(modulus),
                Exponent = BigIntegerExtensions.UnsignedToBytes(exponent),
                D = BigIntegerExtensions.UnsignedToBytes(d)
            };
            Debug.WriteLine("Parameters updated");
        }

        private void DecryptButton_Click(object sender, EventArgs e)
        {
            // TODO: Decrypt in a separate thread.
            if (haveParametersChangedInTextBoxes)
                UpdateRSAParametersUsingTextBoxes();
            throw new System.NotImplementedException();
        }

        private void GenerateKeysButton_Click(object sender, EventArgs e)
        {
            // TODO: Generate keypair in a separate thread.
            if (!keySize.HasValue) return;
            if (rsaMethod == RSAMethod.MicrosoftRSA)
                SetRSAParameters(MicrosoftRSA.GenerateKeyPair(keySize.Value));
            else if (rsaMethod == RSAMethod.MyRSA)
                SetRSAParameters(MyRSA.GenerateKeyPair(keySize.Value));
            haveParametersChangedInTextBoxes = false;
            if (blockCipherMethod.HasValue)
            {
                EncryptButton.Enabled = true;
                DecryptButton.Enabled = true;
            }
        }

        private void SetRSAParameters(RSAParameters rsaParameters)
        {
            this.rsaParameters = rsaParameters;
            ModulusTextBox.Text = BytesToBigIntegerString(rsaParameters.Modulus);
            PublicKeyExponentTextBox.Text = BytesToBigIntegerString(rsaParameters.Exponent);
            PrivateKeyExponentTextBox.Text = BytesToBigIntegerString(rsaParameters.D);
        }

        private string BytesToBigIntegerString(byte[] bytes)
        {
            return BigIntegerExtensions.UnsignedFromBytes(bytes).ToString();
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
            rsaMethod = RSAMethod.MicrosoftRSA;
            ClearRSAParameters();
            SetBlockCipherItems(ToString(MicrosoftRSABlockCipherMethods));
            SetKeyTextBoxesReadOnly(true);
            ChangeButtonsEnabled();
        }

        private void SetBlockCipherItems(object[] items)
        {
            blockCipherMethod = null;
            BlockCipherComboBox.Items.Clear();
            BlockCipherComboBox.Items.AddRange(items);
        }

        private void ChangeButtonsEnabled()
        {
            GenerateKeysButton.Enabled = keySize.HasValue;
            EncryptButton.Enabled = !IsAnyKeyTextBoxEmpty();
            DecryptButton.Enabled = !IsAnyKeyTextBoxEmpty();
        }

        private void ClearRSAParameters()
        {
            rsaParameters = null;
            haveParametersChangedInTextBoxes = false;
            ModulusTextBox.Clear();
            PublicKeyExponentTextBox.Clear();
            PrivateKeyExponentTextBox.Clear();
        }

        private void SelectedIndexChangedToMyRSA()
        {
            rsaMethod = RSAMethod.MyRSA;
            SetBlockCipherItems(ToString(MyRSABlockCipherMethods));
            SetKeyTextBoxesReadOnly(false);
            ChangeButtonsEnabled();
        }

        private void SetKeyTextBoxesReadOnly(bool isReadOnly)
        {
            ModulusTextBox.ReadOnly = isReadOnly;
            PublicKeyExponentTextBox.ReadOnly = isReadOnly;
            PrivateKeyExponentTextBox.ReadOnly = isReadOnly;
        }

        private void BlockCipherComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BlockCipherComboBox.SelectedItem.ToString() == BlockCipherMethod.ElectronicCodebook.ToMethodString())
                blockCipherMethod = BlockCipherMethod.ElectronicCodebook;
            else if (BlockCipherComboBox.SelectedItem.ToString() ==
                     BlockCipherMethod.CipherBlockChaining.ToMethodString())
                blockCipherMethod = BlockCipherMethod.CipherBlockChaining;
            else if (BlockCipherComboBox.SelectedItem.ToString() ==
                     BlockCipherMethod.PropagatingCipherBlockChaining.ToMethodString())
                blockCipherMethod = BlockCipherMethod.PropagatingCipherBlockChaining;
            else if (BlockCipherComboBox.SelectedItem.ToString() == BlockCipherMethod.CipherFeedback.ToMethodString())
                blockCipherMethod = BlockCipherMethod.CipherFeedback;
            else if (BlockCipherComboBox.SelectedItem.ToString() == BlockCipherMethod.OutputFeedback.ToMethodString())
                blockCipherMethod = BlockCipherMethod.OutputFeedback;
            else if (BlockCipherComboBox.SelectedItem.ToString() == BlockCipherMethod.Counter.ToMethodString())
                blockCipherMethod = BlockCipherMethod.Counter;
            EncryptButton.Enabled = rsaParameters.HasValue || !IsAnyKeyTextBoxEmpty();
            DecryptButton.Enabled = rsaParameters.HasValue || !IsAnyKeyTextBoxEmpty();
        }

        private void KeySizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            keySize = int.Parse(KeySizeComboBox.SelectedItem.ToString());
            GenerateKeysButton.Enabled = rsaMethod.HasValue;
        }

        private void ModulusTextBox_TextChanged(object sender, EventArgs e)
        {
            KeyTextBoxTextChanged();
        }

        private void KeyTextBoxTextChanged()
        {
            haveParametersChangedInTextBoxes = true;
            if (IsAnyKeyTextBoxEmpty()) return;
            if (rsaMethod.HasValue && blockCipherMethod.HasValue)
            {
                EncryptButton.Enabled = true;
                DecryptButton.Enabled = true;
            }
        }

        private bool IsAnyKeyTextBoxEmpty()
        {
            return string.IsNullOrWhiteSpace(ModulusTextBox.Text) ||
                   string.IsNullOrWhiteSpace(PublicKeyExponentTextBox.Text) ||
                   string.IsNullOrWhiteSpace(PrivateKeyExponentTextBox.Text);
        }

        private void PublicKeyExponentTextBox_TextChanged(object sender, EventArgs e)
        {
            KeyTextBoxTextChanged();
        }

        private void PrivateKeyExponentTextBox_TextChanged(object sender, EventArgs e)
        {
            KeyTextBoxTextChanged();
        }
        
        private void OnlyDigitsTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}