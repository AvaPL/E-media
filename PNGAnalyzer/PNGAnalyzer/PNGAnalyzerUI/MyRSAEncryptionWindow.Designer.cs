using System.ComponentModel;

namespace PNGAnalyzerUI
{
    partial class MyRSAEncryptionWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.EncryptButton = new System.Windows.Forms.Button();
            this.DecryptButton = new System.Windows.Forms.Button();
            this.GenerateKeysButton = new System.Windows.Forms.Button();
            this.PrivateKeyExponentLabel = new System.Windows.Forms.Label();
            this.PublicKeyExponentLabel = new System.Windows.Forms.Label();
            this.ModulusLabel = new System.Windows.Forms.Label();
            this.BlockCipherComboBox = new System.Windows.Forms.ComboBox();
            this.BlockCipherLabel = new System.Windows.Forms.Label();
            this.PrivateKeyExponentTextBox = new System.Windows.Forms.TextBox();
            this.PublicKeyExponentTextBox = new System.Windows.Forms.TextBox();
            this.ModulusTextBox = new System.Windows.Forms.TextBox();
            this.KeySizeLabel = new System.Windows.Forms.Label();
            this.KeySizeComboBox = new System.Windows.Forms.ComboBox();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.InitializationVectorLabel = new System.Windows.Forms.Label();
            this.InitializationVectorTextBox = new System.Windows.Forms.TextBox();
            this.FilepathTextBox = new System.Windows.Forms.TextBox();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.FilteringCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // EncryptButton
            // 
            this.EncryptButton.Enabled = false;
            this.EncryptButton.Location = new System.Drawing.Point(604, 141);
            this.EncryptButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.EncryptButton.Name = "EncryptButton";
            this.EncryptButton.Size = new System.Drawing.Size(108, 23);
            this.EncryptButton.TabIndex = 9;
            this.EncryptButton.Text = "Encrypt";
            this.EncryptButton.UseVisualStyleBackColor = true;
            this.EncryptButton.Click += new System.EventHandler(this.EncryptButton_Click);
            // 
            // DecryptButton
            // 
            this.DecryptButton.Enabled = false;
            this.DecryptButton.Location = new System.Drawing.Point(604, 177);
            this.DecryptButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.DecryptButton.Name = "DecryptButton";
            this.DecryptButton.Size = new System.Drawing.Size(108, 23);
            this.DecryptButton.TabIndex = 10;
            this.DecryptButton.Text = "Decrypt";
            this.DecryptButton.UseVisualStyleBackColor = true;
            this.DecryptButton.Click += new System.EventHandler(this.DecryptButton_Click);
            // 
            // GenerateKeysButton
            // 
            this.GenerateKeysButton.Enabled = false;
            this.GenerateKeysButton.Location = new System.Drawing.Point(604, 108);
            this.GenerateKeysButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.GenerateKeysButton.Name = "GenerateKeysButton";
            this.GenerateKeysButton.Size = new System.Drawing.Size(108, 23);
            this.GenerateKeysButton.TabIndex = 8;
            this.GenerateKeysButton.Text = "Generate keys";
            this.GenerateKeysButton.UseVisualStyleBackColor = true;
            this.GenerateKeysButton.Click += new System.EventHandler(this.GenerateKeysButton_Click);
            // 
            // PrivateKeyExponentLabel
            // 
            this.PrivateKeyExponentLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.PrivateKeyExponentLabel.Location = new System.Drawing.Point(8, 175);
            this.PrivateKeyExponentLabel.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.PrivateKeyExponentLabel.Name = "PrivateKeyExponentLabel";
            this.PrivateKeyExponentLabel.Size = new System.Drawing.Size(199, 24);
            this.PrivateKeyExponentLabel.TabIndex = 106;
            this.PrivateKeyExponentLabel.Text = "Private key exponent:";
            // 
            // PublicKeyExponentLabel
            // 
            this.PublicKeyExponentLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.PublicKeyExponentLabel.Location = new System.Drawing.Point(7, 140);
            this.PublicKeyExponentLabel.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.PublicKeyExponentLabel.Name = "PublicKeyExponentLabel";
            this.PublicKeyExponentLabel.Size = new System.Drawing.Size(199, 24);
            this.PublicKeyExponentLabel.TabIndex = 105;
            this.PublicKeyExponentLabel.Text = "Public key exponent:";
            // 
            // ModulusLabel
            // 
            this.ModulusLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.ModulusLabel.Location = new System.Drawing.Point(7, 107);
            this.ModulusLabel.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ModulusLabel.Name = "ModulusLabel";
            this.ModulusLabel.Size = new System.Drawing.Size(170, 24);
            this.ModulusLabel.TabIndex = 104;
            this.ModulusLabel.Text = "Modulus:";
            // 
            // BlockCipherComboBox
            // 
            this.BlockCipherComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BlockCipherComboBox.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.BlockCipherComboBox.FormattingEnabled = true;
            this.BlockCipherComboBox.Location = new System.Drawing.Point(463, 42);
            this.BlockCipherComboBox.Name = "BlockCipherComboBox";
            this.BlockCipherComboBox.Size = new System.Drawing.Size(249, 28);
            this.BlockCipherComboBox.TabIndex = 3;
            this.BlockCipherComboBox.SelectedIndexChanged +=
                new System.EventHandler(this.BlockCipherComboBox_SelectedIndexChanged);
            // 
            // BlockCipherLabel
            // 
            this.BlockCipherLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.BlockCipherLabel.Location = new System.Drawing.Point(343, 43);
            this.BlockCipherLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.BlockCipherLabel.Name = "BlockCipherLabel";
            this.BlockCipherLabel.Size = new System.Drawing.Size(120, 24);
            this.BlockCipherLabel.TabIndex = 102;
            this.BlockCipherLabel.Text = "Block cipher:";
            // 
            // PrivateKeyExponentTextBox
            // 
            this.PrivateKeyExponentTextBox.Location = new System.Drawing.Point(195, 177);
            this.PrivateKeyExponentTextBox.Name = "PrivateKeyExponentTextBox";
            this.PrivateKeyExponentTextBox.Size = new System.Drawing.Size(404, 23);
            this.PrivateKeyExponentTextBox.TabIndex = 7;
            this.PrivateKeyExponentTextBox.TextChanged +=
                new System.EventHandler(this.PrivateKeyExponentTextBox_TextChanged);
            this.PrivateKeyExponentTextBox.KeyPress +=
                new System.Windows.Forms.KeyPressEventHandler(this.OnlyDigitsTextBox_KeyPress);
            // 
            // PublicKeyExponentTextBox
            // 
            this.PublicKeyExponentTextBox.Location = new System.Drawing.Point(195, 141);
            this.PublicKeyExponentTextBox.Name = "PublicKeyExponentTextBox";
            this.PublicKeyExponentTextBox.Size = new System.Drawing.Size(404, 23);
            this.PublicKeyExponentTextBox.TabIndex = 6;
            this.PublicKeyExponentTextBox.TextChanged +=
                new System.EventHandler(this.PublicKeyExponentTextBox_TextChanged);
            this.PublicKeyExponentTextBox.KeyPress +=
                new System.Windows.Forms.KeyPressEventHandler(this.OnlyDigitsTextBox_KeyPress);
            // 
            // ModulusTextBox
            // 
            this.ModulusTextBox.Location = new System.Drawing.Point(195, 108);
            this.ModulusTextBox.Name = "ModulusTextBox";
            this.ModulusTextBox.Size = new System.Drawing.Size(404, 23);
            this.ModulusTextBox.TabIndex = 5;
            this.ModulusTextBox.TextChanged += new System.EventHandler(this.ModulusTextBox_TextChanged);
            this.ModulusTextBox.KeyPress +=
                new System.Windows.Forms.KeyPressEventHandler(this.OnlyDigitsTextBox_KeyPress);
            // 
            // KeySizeLabel
            // 
            this.KeySizeLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.KeySizeLabel.Location = new System.Drawing.Point(7, 43);
            this.KeySizeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.KeySizeLabel.Name = "KeySizeLabel";
            this.KeySizeLabel.Size = new System.Drawing.Size(120, 24);
            this.KeySizeLabel.TabIndex = 101;
            this.KeySizeLabel.Text = "Key size:";
            // 
            // KeySizeComboBox
            // 
            this.KeySizeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.KeySizeComboBox.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.KeySizeComboBox.FormattingEnabled = true;
            this.KeySizeComboBox.Location = new System.Drawing.Point(133, 42);
            this.KeySizeComboBox.Name = "KeySizeComboBox";
            this.KeySizeComboBox.Size = new System.Drawing.Size(200, 28);
            this.KeySizeComboBox.TabIndex = 2;
            this.KeySizeComboBox.SelectedIndexChanged +=
                new System.EventHandler(this.KeySizeComboBox_SelectedIndexChanged);
            // 
            // SaveFileDialog
            // 
            this.SaveFileDialog.DefaultExt = "png";
            this.SaveFileDialog.Filter = "PNG files (*.png)|*.png|All files (*.*)|*.*";
            // 
            // InitializationVectorLabel
            // 
            this.InitializationVectorLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.InitializationVectorLabel.Location = new System.Drawing.Point(7, 77);
            this.InitializationVectorLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.InitializationVectorLabel.Name = "InitializationVectorLabel";
            this.InitializationVectorLabel.Size = new System.Drawing.Size(125, 24);
            this.InitializationVectorLabel.TabIndex = 103;
            this.InitializationVectorLabel.Text = "IV:";
            // 
            // InitializationVectorTextBox
            // 
            this.InitializationVectorTextBox.Location = new System.Drawing.Point(133, 78);
            this.InitializationVectorTextBox.Name = "InitializationVectorTextBox";
            this.InitializationVectorTextBox.ReadOnly = true;
            this.InitializationVectorTextBox.Size = new System.Drawing.Size(466, 23);
            this.InitializationVectorTextBox.TabIndex = 4;
            this.InitializationVectorTextBox.KeyPress +=
                new System.Windows.Forms.KeyPressEventHandler(this.OnlyDigitsTextBox_KeyPress);
            // 
            // FilepathTextBox
            // 
            this.FilepathTextBox.Location = new System.Drawing.Point(8, 12);
            this.FilepathTextBox.Name = "FilepathTextBox";
            this.FilepathTextBox.ReadOnly = true;
            this.FilepathTextBox.Size = new System.Drawing.Size(592, 23);
            this.FilepathTextBox.TabIndex = 107;
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(605, 12);
            this.BrowseButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(108, 23);
            this.BrowseButton.TabIndex = 108;
            this.BrowseButton.Text = "Browse...";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.DefaultExt = "png";
            this.OpenFileDialog.Filter = "PNG files (*.png)|*.png|All files (*.*)|*.*";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label1.Location = new System.Drawing.Point(604, 78);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 24);
            this.label1.TabIndex = 111;
            this.label1.Text = "Filtering:";
            // 
            // FilteringCheckBox
            // 
            this.FilteringCheckBox.Location = new System.Drawing.Point(692, 83);
            this.FilteringCheckBox.Name = "FilteringCheckBox";
            this.FilteringCheckBox.Size = new System.Drawing.Size(20, 20);
            this.FilteringCheckBox.TabIndex = 112;
            this.FilteringCheckBox.UseVisualStyleBackColor = true;
            // 
            // MyRSAEncryptionWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 215);
            this.Controls.Add(this.FilteringCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BrowseButton);
            this.Controls.Add(this.FilepathTextBox);
            this.Controls.Add(this.InitializationVectorTextBox);
            this.Controls.Add(this.InitializationVectorLabel);
            this.Controls.Add(this.KeySizeComboBox);
            this.Controls.Add(this.KeySizeLabel);
            this.Controls.Add(this.ModulusTextBox);
            this.Controls.Add(this.PublicKeyExponentTextBox);
            this.Controls.Add(this.PrivateKeyExponentTextBox);
            this.Controls.Add(this.BlockCipherLabel);
            this.Controls.Add(this.BlockCipherComboBox);
            this.Controls.Add(this.ModulusLabel);
            this.Controls.Add(this.PublicKeyExponentLabel);
            this.Controls.Add(this.PrivateKeyExponentLabel);
            this.Controls.Add(this.GenerateKeysButton);
            this.Controls.Add(this.DecryptButton);
            this.Controls.Add(this.EncryptButton);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "MyRSAEncryptionWindow";
            this.Text = "MyRSA Encryption";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button GenerateKeysButton;
        private System.Windows.Forms.Button DecryptButton;
        private System.Windows.Forms.Button EncryptButton;
        private System.Windows.Forms.Label ModulusLabel;
        private System.Windows.Forms.Label PublicKeyExponentLabel;
        private System.Windows.Forms.Label PrivateKeyExponentLabel;
        private System.Windows.Forms.ComboBox BlockCipherComboBox;
        private System.Windows.Forms.Label BlockCipherLabel;
        private System.Windows.Forms.TextBox ModulusTextBox;
        private System.Windows.Forms.TextBox PublicKeyExponentTextBox;
        private System.Windows.Forms.TextBox PrivateKeyExponentTextBox;
        private System.Windows.Forms.Label KeySizeLabel;
        private System.Windows.Forms.ComboBox KeySizeComboBox;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.Label InitializationVectorLabel;
        private System.Windows.Forms.TextBox InitializationVectorTextBox;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.TextBox FilepathTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox FilteringCheckBox;
    }
}