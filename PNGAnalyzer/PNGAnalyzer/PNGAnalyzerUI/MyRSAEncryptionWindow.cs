using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PNGAnalyzer;
using PNGAnalyzer.BlockCiphers;
using PNGAnalyzer.RSA;

namespace PNGAnalyzerUI
{
    public partial class MyRSAEncryptionWindow : Form
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

        private RSAMethod? rsaMethod;
        private BlockCipherMethod? blockCipherMethod;
        private int? keySize;
        private RSAParameters? rsaParameters;

        public MyRSAEncryptionWindow()
        {
            InitializeComponent();
            // MethodComboBox.Items.AddRange(ToString(RSAMethods));
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
            UpdateRSAParametersUsingTextBoxes();
            if (!rsaParameters.HasValue) return;
            if (SaveFileDialog.ShowDialog() != DialogResult.OK) return;
            IRSA rsa = GetRSA();
            IBlockCipher blockCipher = GetBlockCipher(rsa);
            ImageBlockCipher imageBlockCipher = new ImageBlockCipher(blockCipher);
            List<Chunk> chunks = PNGFile.Read(FilepathTextBox.Text);
            List<Chunk> parsedChunks = ChunkParser.Parse(chunks);
            List<Chunk> cipheredChunks = imageBlockCipher.Cipher(parsedChunks);
            PNGFile.Write(SaveFileDialog.FileName, cipheredChunks);
        }

        private IBlockCipher GetBlockCipher(IRSA rsa)
        {
            BigInteger? initializationVector = null;
            if (blockCipherMethod != BlockCipherMethod.ElectronicCodebook &&
                !string.IsNullOrWhiteSpace(InitializationVectorTextBox.Text))
                initializationVector = BigInteger.Parse(InitializationVectorTextBox.Text);

            return initializationVector.HasValue
                ? GetBlockCipherUsingInitializationVector(rsa, initializationVector.Value)
                : GetBlockCipherWithoutInitializationVector(rsa);
        }

        private IBlockCipher GetBlockCipherWithoutInitializationVector(IRSA rsa)
        {
            switch (blockCipherMethod)
            {
                case BlockCipherMethod.ElectronicCodebook:
                    return new ElectronicCodebook(rsa);
                case BlockCipherMethod.CipherBlockChaining:
                    var cbc = new CipherBlockChaining(rsa);
                    InitializationVectorTextBox.Text = cbc.InitializationVector.ToString();
                    return cbc;
                case BlockCipherMethod.PropagatingCipherBlockChaining:
                    var pcbc = new CipherBlockChaining(rsa);
                    InitializationVectorTextBox.Text = pcbc.InitializationVector.ToString();
                    return pcbc;
                case BlockCipherMethod.CipherFeedback:
                    var cf = new CipherBlockChaining(rsa);
                    InitializationVectorTextBox.Text = cf.InitializationVector.ToString();
                    return cf;
                case BlockCipherMethod.OutputFeedback:
                    var of = new CipherBlockChaining(rsa);
                    InitializationVectorTextBox.Text = of.InitializationVector.ToString();
                    return of;
                case BlockCipherMethod.Counter:
                    var c = new CipherBlockChaining(rsa);
                    InitializationVectorTextBox.Text = c.InitializationVector.ToString();
                    return c;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private IBlockCipher GetBlockCipherUsingInitializationVector(IRSA rsa, BigInteger initializationVector)
        {
            switch (blockCipherMethod)
            {
                case BlockCipherMethod.CipherBlockChaining:
                    return new CipherBlockChaining(rsa, initializationVector);
                case BlockCipherMethod.PropagatingCipherBlockChaining:
                    return new PropagatingCipherBlockChaining(rsa, initializationVector);
                case BlockCipherMethod.CipherFeedback:
                    return new CipherFeedback(rsa, initializationVector);
                case BlockCipherMethod.OutputFeedback:
                    return new OutputFeedback(rsa, initializationVector);
                case BlockCipherMethod.Counter:
                    return new Counter(rsa, initializationVector);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private IRSA GetRSA()
        {
            switch (rsaMethod)
            {
                case RSAMethod.MicrosoftRSA:
                    return new MicrosoftRSA(rsaParameters.Value);
                case RSAMethod.MyRSA:
                    return new MyRSA(rsaParameters.Value);
                default:
                    throw new ArgumentOutOfRangeException();
            }
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
            UpdateRSAParametersUsingTextBoxes();
            if (!rsaParameters.HasValue) return;
            if (SaveFileDialog.ShowDialog() != DialogResult.OK) return;
            IRSA rsa = GetRSA();
            IBlockCipher blockCipher = GetBlockCipher(rsa);
            ImageBlockCipher imageBlockCipher = new ImageBlockCipher(blockCipher);
            List<Chunk> chunks = PNGFile.Read(FilepathTextBox.Text);
            List<Chunk> parsedChunks = ChunkParser.Parse(chunks);
            List<Chunk> decipheredChunks = imageBlockCipher.Decipher(parsedChunks);
            PNGFile.Write(SaveFileDialog.FileName, decipheredChunks);
        }

        private void GenerateKeysButton_Click(object sender, EventArgs e)
        {
            GenerateKeypairAsync();
            if (blockCipherMethod.HasValue)
            {
                EncryptButton.Enabled = !string.IsNullOrWhiteSpace(FilepathTextBox.Text);
                DecryptButton.Enabled = !string.IsNullOrWhiteSpace(FilepathTextBox.Text);
            }
        }

        private void GenerateKeypairAsync()
        {
            if (!keySize.HasValue) return;
            Task.Run(() => GenerateKeypair(keySize.Value));
        }

        private void GenerateKeypair(int keySize)
        {
            SetKeypairInputActive(false);
            Task.Run(AnimateGeneratingText);
            if (rsaMethod == RSAMethod.MicrosoftRSA)
                SetRSAParameters(MicrosoftRSA.GenerateKeyPair(keySize));
            else if (rsaMethod == RSAMethod.MyRSA)
                SetRSAParameters(MyRSA.GenerateKeyPair(keySize));
            SetKeypairInputActive(true);
        }

        private void SetKeypairInputActive(bool isActive)
        {
            GenerateKeysButton.Enabled = isActive;
            if (rsaMethod == RSAMethod.MicrosoftRSA) return;
            ModulusTextBox.ReadOnly = !isActive;
            PublicKeyExponentTextBox.ReadOnly = !isActive;
            PrivateKeyExponentTextBox.ReadOnly = !isActive;
        }

        private void AnimateGeneratingText()
        {
            string generateKeysButtonText = GenerateKeysButton.Text;
            for (int i = 0; GenerateKeysButton.Enabled == false; i++)
            {
                string generatingText = CreateGeneratingText(i);
                SetKeypairInputText(generatingText);
                Thread.Sleep(500);
            }

            GenerateKeysButton.Text = generateKeysButtonText;
        }

        private void SetKeypairInputText(string generatingText)
        {
            ModulusTextBox.Text = generatingText;
            PublicKeyExponentTextBox.Text = generatingText;
            PrivateKeyExponentTextBox.Text = generatingText;
        }

        private static string CreateGeneratingText(int i)
        {
            string generating = "Generating";
            string dots = string.Concat(Enumerable.Repeat(".", i % 4));
            return generating + dots;
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

        // private void MethodComboBox_SelectedIndexChanged(object sender, EventArgs e)
        // {
        //     BlockCipherComboBox.Enabled = true;
        //     if (MethodComboBox.SelectedItem.ToString() == RSAMethod.MicrosoftRSA.ToMethodString())
        //         SelectedIndexChangedToMicrosoftRSA();
        //     else if (MethodComboBox.SelectedItem.ToString() == RSAMethod.MyRSA.ToMethodString())
        //         SelectedIndexChangedToMyRSA();
        // }

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
            UpdateInitializationVectorTextBox();
        }

        private void ChangeButtonsEnabled()
        {
            GenerateKeysButton.Enabled = keySize.HasValue;
            EncryptButton.Enabled = !IsAnyKeyTextBoxEmpty() && !string.IsNullOrWhiteSpace(FilepathTextBox.Text);
            DecryptButton.Enabled = !IsAnyKeyTextBoxEmpty() && !string.IsNullOrWhiteSpace(FilepathTextBox.Text); 
        }

        private void ClearRSAParameters()
        {
            rsaParameters = null;
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
            UpdateInitializationVectorTextBox();
            EncryptButton.Enabled = (rsaParameters.HasValue || !IsAnyKeyTextBoxEmpty()) && !string.IsNullOrWhiteSpace(FilepathTextBox.Text);
            DecryptButton.Enabled = (rsaParameters.HasValue || !IsAnyKeyTextBoxEmpty()) && !string.IsNullOrWhiteSpace(FilepathTextBox.Text);
        }

        private void UpdateInitializationVectorTextBox()
        {
            if (!blockCipherMethod.HasValue)
            {
                InitializationVectorTextBox.ReadOnly = true;
                InitializationVectorTextBox.Clear();
                return;
            }

            string notUsedText = "Not used";
            bool isUsed = blockCipherMethod != BlockCipherMethod.ElectronicCodebook;
            InitializationVectorTextBox.ReadOnly = !isUsed;
            if (!isUsed)
                InitializationVectorTextBox.Text = notUsedText;
            else if (InitializationVectorTextBox.Text == notUsedText)
                InitializationVectorTextBox.Clear();
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
            if (IsAnyKeyTextBoxEmpty()) return;
            if (rsaMethod.HasValue && blockCipherMethod.HasValue)
            {
                EncryptButton.Enabled = !string.IsNullOrWhiteSpace(FilepathTextBox.Text);
                DecryptButton.Enabled = !string.IsNullOrWhiteSpace(FilepathTextBox.Text);
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

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() != DialogResult.OK) return;
            FilepathTextBox.Text = OpenFileDialog.FileName;
        }
    }
}