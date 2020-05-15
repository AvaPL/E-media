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
    public partial class MicrosoftRSAEncryptionWindow : Form
    {
        private static readonly BlockCipherMethod[] BlockCipherMethods =
        {
            BlockCipherMethod.ElectronicCodebook, BlockCipherMethod.CipherBlockChaining
        };

        private static readonly int[] KeySizes = {512, 1024, 2048};

        private BlockCipherMethod? blockCipherMethod;
        private int? keySize;
        private RSAParameters? rsaParameters;

        public MicrosoftRSAEncryptionWindow()
        {
            InitializeComponent();
            BlockCipherComboBox.Items.AddRange(ToString(BlockCipherMethods));
            KeySizeComboBox.Items.AddRange(ToString(KeySizes));
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
            if (!rsaParameters.HasValue) return;
            if (SaveFileDialog.ShowDialog() != DialogResult.OK) return;
            ImageBlockCipher imageBlockCipher = GetImageBlockCipher(rsaParameters.Value);
            EncryptImageAsync(imageBlockCipher);
        }

        private ImageBlockCipher GetImageBlockCipher(RSAParameters parameters)
        {
            MicrosoftRSA rsa = new MicrosoftRSA(parameters);
            IBlockCipher blockCipher = GetBlockCipher(rsa);
            ImageBlockCipher imageBlockCipher = new ImageBlockCipher(blockCipher);
            return imageBlockCipher;
        }

        private IBlockCipher GetBlockCipher(MicrosoftRSA rsa)
        {
            BigInteger? initializationVector = GetInitializationVector();
            return initializationVector.HasValue
                ? new CipherBlockChaining(rsa, initializationVector.Value)
                : GetBlockCipherWithoutInitializationVector(rsa);
        }

        private BigInteger? GetInitializationVector()
        {
            if (blockCipherMethod == BlockCipherMethod.ElectronicCodebook ||
                string.IsNullOrWhiteSpace(InitializationVectorTextBox.Text))
                return null;
            return BigInteger.Parse(InitializationVectorTextBox.Text);
        }

        private IBlockCipher GetBlockCipherWithoutInitializationVector(MicrosoftRSA rsa)
        {
            switch (blockCipherMethod)
            {
                case BlockCipherMethod.ElectronicCodebook:
                    return new ElectronicCodebook(rsa);
                case BlockCipherMethod.CipherBlockChaining:
                    var cbc = new CipherBlockChaining(rsa);
                    InitializationVectorTextBox.Text = cbc.InitializationVector.ToString();
                    return cbc;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void EncryptImageAsync(ImageBlockCipher imageBlockCipher)
        {
            Task.Run(() => EncryptImage(imageBlockCipher));
        }

        private void EncryptImage(ImageBlockCipher imageBlockCipher)
        {
            EncryptButton.Enabled = false;
            Task.Run(AnimateEncryptingText);
            List<Chunk> parsedChunks = ReadAndParseChunks();
            List<Chunk> cipheredChunks = imageBlockCipher.Cipher(parsedChunks);
            PNGFile.Write(SaveFileDialog.FileName, cipheredChunks);
            EncryptButton.Enabled = true;
        }

        private void AnimateEncryptingText()
        {
            string decryptButtonText = EncryptButton.Text;
            for (int i = 0; EncryptButton.Enabled == false; i++)
            {
                EncryptButton.Text = CreateTextWithDots("Encrypting", i);
                Thread.Sleep(500);
            }

            EncryptButton.Text = decryptButtonText;
        }

        private static string CreateTextWithDots(string text, int numberOfDots)
        {
            string dots = string.Concat(Enumerable.Repeat(".", numberOfDots % 4));
            string spaces = string.Concat(Enumerable.Repeat(" ", 4 - dots.Length));
            return text + dots + spaces;
        }

        private List<Chunk> ReadAndParseChunks()
        {
            List<Chunk> chunks = PNGFile.Read(FilepathTextBox.Text);
            List<Chunk> parsedChunks = ChunkParser.Parse(chunks);
            return parsedChunks;
        }

        private void DecryptButton_Click(object sender, EventArgs e)
        {
            if (!rsaParameters.HasValue) return;
            if (SaveFileDialog.ShowDialog() != DialogResult.OK) return;
            ImageBlockCipher imageBlockCipher = GetImageBlockCipher(rsaParameters.Value);
            DecryptImageAsync(imageBlockCipher);
        }

        private void DecryptImageAsync(ImageBlockCipher imageBlockCipher)
        {
            Task.Run(() => DecryptImage(imageBlockCipher));
        }

        private void DecryptImage(ImageBlockCipher imageBlockCipher)
        {
            DecryptButton.Enabled = false;
            Task.Run(AnimateDecryptingText);
            List<Chunk> parsedChunks = ReadAndParseChunks();
            List<Chunk> decipheredChunks = imageBlockCipher.Decipher(parsedChunks);
            PNGFile.Write(SaveFileDialog.FileName, decipheredChunks);
            DecryptButton.Enabled = true;
        }

        private void AnimateDecryptingText()
        {
            string decryptButtonText = DecryptButton.Text;
            for (int i = 0; DecryptButton.Enabled == false; i++)
            {
                DecryptButton.Text = CreateTextWithDots("Decrypting", i);
                Thread.Sleep(500);
            }

            DecryptButton.Text = decryptButtonText;
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

        private void GenerateKeypair(int size)
        {
            GenerateKeysButton.Enabled = false;
            Task.Run(AnimateGeneratingText);
            SetRSAParameters(MicrosoftRSA.GenerateKeyPair(size));
            GenerateKeysButton.Enabled = true;
        }

        private void AnimateGeneratingText()
        {
            string generateKeysButtonText = GenerateKeysButton.Text;
            for (int i = 0; GenerateKeysButton.Enabled == false; i++)
            {
                string generatingText = CreateTextWithDots("Generating", i);
                SetKeypairText(generatingText);
                Thread.Sleep(500);
            }

            GenerateKeysButton.Text = generateKeysButtonText;
        }

        private void SetKeypairText(string generatingText)
        {
            ModulusTextBox.Text = generatingText;
            PublicKeyExponentTextBox.Text = generatingText;
            PrivateKeyExponentTextBox.Text = generatingText;
        }

        private void SetRSAParameters(RSAParameters parameters)
        {
            rsaParameters = parameters;
            ModulusTextBox.Text = BytesToBigIntegerString(parameters.Modulus);
            PublicKeyExponentTextBox.Text = BytesToBigIntegerString(parameters.Exponent);
            PrivateKeyExponentTextBox.Text = BytesToBigIntegerString(parameters.D);
        }

        private string BytesToBigIntegerString(byte[] bytes)
        {
            return BigIntegerExtensions.UnsignedFromBytes(bytes).ToString();
        }

        private void BlockCipherComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BlockCipherComboBox.SelectedItem.ToString() == BlockCipherMethod.ElectronicCodebook.ToMethodString())
                blockCipherMethod = BlockCipherMethod.ElectronicCodebook;
            else if (BlockCipherComboBox.SelectedItem.ToString() ==
                     BlockCipherMethod.CipherBlockChaining.ToMethodString())
                blockCipherMethod = BlockCipherMethod.CipherBlockChaining;
            UpdateInitializationVectorTextBox();
            EncryptButton.Enabled = rsaParameters.HasValue && !string.IsNullOrWhiteSpace(FilepathTextBox.Text);
            DecryptButton.Enabled = rsaParameters.HasValue && !string.IsNullOrWhiteSpace(FilepathTextBox.Text);
        }

        private void UpdateInitializationVectorTextBox()
        {
            if (!blockCipherMethod.HasValue)
            {
                InitializationVectorTextBox.ReadOnly = true;
                InitializationVectorTextBox.Clear();
                return;
            }

            const string NotUsedText = "Not used";
            bool isUsed = blockCipherMethod != BlockCipherMethod.ElectronicCodebook;
            InitializationVectorTextBox.ReadOnly = !isUsed;
            if (!isUsed)
                InitializationVectorTextBox.Text = NotUsedText;
            else if (InitializationVectorTextBox.Text == NotUsedText)
                InitializationVectorTextBox.Clear();
        }

        private void KeySizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            keySize = int.Parse(KeySizeComboBox.SelectedItem.ToString());
            GenerateKeysButton.Enabled = true;
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() != DialogResult.OK) return;
            FilepathTextBox.Text = OpenFileDialog.FileName;
            if (blockCipherMethod.HasValue && rsaParameters.HasValue)
            {
                EncryptButton.Enabled = true;
                DecryptButton.Enabled = true;
            }
        }
    }
}