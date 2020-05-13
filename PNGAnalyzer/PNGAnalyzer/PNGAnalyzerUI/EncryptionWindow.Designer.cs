using System.ComponentModel;

namespace PNGAnalyzerUI
{
    partial class EncryptionWindow
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
            this.MethodComboBox = new System.Windows.Forms.ComboBox();
            this.BlockCipherComboBox = new System.Windows.Forms.ComboBox();
            this.MethodLabel = new System.Windows.Forms.Label();
            this.BlockCipherLabel = new System.Windows.Forms.Label();
            this.PrivateKeyExponentTextBox = new System.Windows.Forms.TextBox();
            this.PublicKeyExponentTextBox = new System.Windows.Forms.TextBox();
            this.ModulusTextBox = new System.Windows.Forms.TextBox();
            this.KeySizeLabel = new System.Windows.Forms.Label();
            this.KeySizeComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // EncryptButton
            // 
            this.EncryptButton.Enabled = false;
            this.EncryptButton.Location = new System.Drawing.Point(607, 124);
            this.EncryptButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.EncryptButton.Name = "EncryptButton";
            this.EncryptButton.Size = new System.Drawing.Size(108, 23);
            this.EncryptButton.TabIndex = 1;
            this.EncryptButton.Text = "Encrypt";
            this.EncryptButton.UseVisualStyleBackColor = true;
            this.EncryptButton.Click += new System.EventHandler(this.EncryptButton_Click);
            // 
            // DecryptButton
            // 
            this.DecryptButton.Enabled = false;
            this.DecryptButton.Location = new System.Drawing.Point(607, 158);
            this.DecryptButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.DecryptButton.Name = "DecryptButton";
            this.DecryptButton.Size = new System.Drawing.Size(108, 23);
            this.DecryptButton.TabIndex = 2;
            this.DecryptButton.Text = "Decrypt";
            this.DecryptButton.UseVisualStyleBackColor = true;
            this.DecryptButton.Click += new System.EventHandler(this.DecryptButton_Click);
            // 
            // GenerateKeysButton
            // 
            this.GenerateKeysButton.Enabled = false;
            this.GenerateKeysButton.Location = new System.Drawing.Point(607, 90);
            this.GenerateKeysButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.GenerateKeysButton.Name = "GenerateKeysButton";
            this.GenerateKeysButton.Size = new System.Drawing.Size(108, 23);
            this.GenerateKeysButton.TabIndex = 3;
            this.GenerateKeysButton.Text = "Generate keys";
            this.GenerateKeysButton.UseVisualStyleBackColor = true;
            this.GenerateKeysButton.Click += new System.EventHandler(this.GenerateKeysButton_Click);
            // 
            // PrivateKeyExponentLabel
            // 
            this.PrivateKeyExponentLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.PrivateKeyExponentLabel.Location = new System.Drawing.Point(8, 156);
            this.PrivateKeyExponentLabel.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.PrivateKeyExponentLabel.Name = "PrivateKeyExponentLabel";
            this.PrivateKeyExponentLabel.Size = new System.Drawing.Size(170, 24);
            this.PrivateKeyExponentLabel.TabIndex = 5;
            this.PrivateKeyExponentLabel.Text = "Private key exponent:";
            // 
            // PublicKeyExponentLabel
            // 
            this.PublicKeyExponentLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.PublicKeyExponentLabel.Location = new System.Drawing.Point(8, 122);
            this.PublicKeyExponentLabel.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.PublicKeyExponentLabel.Name = "PublicKeyExponentLabel";
            this.PublicKeyExponentLabel.Size = new System.Drawing.Size(170, 24);
            this.PublicKeyExponentLabel.TabIndex = 7;
            this.PublicKeyExponentLabel.Text = "Public key exponent:";
            // 
            // ModulusLabel
            // 
            this.ModulusLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.ModulusLabel.Location = new System.Drawing.Point(8, 88);
            this.ModulusLabel.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ModulusLabel.Name = "ModulusLabel";
            this.ModulusLabel.Size = new System.Drawing.Size(170, 24);
            this.ModulusLabel.TabIndex = 8;
            this.ModulusLabel.Text = "Modulus:";
            // 
            // MethodComboBox
            // 
            this.MethodComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MethodComboBox.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.MethodComboBox.FormattingEnabled = true;
            this.MethodComboBox.Location = new System.Drawing.Point(138, 12);
            this.MethodComboBox.Name = "MethodComboBox";
            this.MethodComboBox.Size = new System.Drawing.Size(200, 28);
            this.MethodComboBox.TabIndex = 9;
            this.MethodComboBox.SelectedIndexChanged +=
                new System.EventHandler(this.MethodComboBox_SelectedIndexChanged);
            // 
            // BlockCipherComboBox
            // 
            this.BlockCipherComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BlockCipherComboBox.Enabled = false;
            this.BlockCipherComboBox.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.BlockCipherComboBox.FormattingEnabled = true;
            this.BlockCipherComboBox.Location = new System.Drawing.Point(138, 52);
            this.BlockCipherComboBox.Name = "BlockCipherComboBox";
            this.BlockCipherComboBox.Size = new System.Drawing.Size(576, 28);
            this.BlockCipherComboBox.TabIndex = 10;
            this.BlockCipherComboBox.SelectedIndexChanged +=
                new System.EventHandler(this.BlockCipherComboBox_SelectedIndexChanged);
            // 
            // MethodLabel
            // 
            this.MethodLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.MethodLabel.Location = new System.Drawing.Point(8, 14);
            this.MethodLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MethodLabel.Name = "MethodLabel";
            this.MethodLabel.Size = new System.Drawing.Size(125, 24);
            this.MethodLabel.TabIndex = 11;
            this.MethodLabel.Text = "Method:";
            // 
            // BlockCipherLabel
            // 
            this.BlockCipherLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.BlockCipherLabel.Location = new System.Drawing.Point(8, 54);
            this.BlockCipherLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.BlockCipherLabel.Name = "BlockCipherLabel";
            this.BlockCipherLabel.Size = new System.Drawing.Size(125, 24);
            this.BlockCipherLabel.TabIndex = 12;
            this.BlockCipherLabel.Text = "Block cipher:";
            // 
            // PrivateKeyExponentTextBox
            // 
            this.PrivateKeyExponentTextBox.Location = new System.Drawing.Point(186, 158);
            this.PrivateKeyExponentTextBox.Name = "PrivateKeyExponentTextBox";
            this.PrivateKeyExponentTextBox.ReadOnly = true;
            this.PrivateKeyExponentTextBox.Size = new System.Drawing.Size(416, 23);
            this.PrivateKeyExponentTextBox.TabIndex = 13;
            // 
            // PublicKeyExponentTextBox
            // 
            this.PublicKeyExponentTextBox.Location = new System.Drawing.Point(186, 124);
            this.PublicKeyExponentTextBox.Name = "PublicKeyExponentTextBox";
            this.PublicKeyExponentTextBox.ReadOnly = true;
            this.PublicKeyExponentTextBox.Size = new System.Drawing.Size(416, 23);
            this.PublicKeyExponentTextBox.TabIndex = 14;
            // 
            // ModulusTextBox
            // 
            this.ModulusTextBox.Location = new System.Drawing.Point(186, 90);
            this.ModulusTextBox.Name = "ModulusTextBox";
            this.ModulusTextBox.ReadOnly = true;
            this.ModulusTextBox.Size = new System.Drawing.Size(416, 23);
            this.ModulusTextBox.TabIndex = 15;
            // 
            // KeySizeLabel
            // 
            this.KeySizeLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.KeySizeLabel.Location = new System.Drawing.Point(384, 14);
            this.KeySizeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.KeySizeLabel.Name = "KeySizeLabel";
            this.KeySizeLabel.Size = new System.Drawing.Size(125, 24);
            this.KeySizeLabel.TabIndex = 16;
            this.KeySizeLabel.Text = "Key size:";
            // 
            // KeySizeComboBox
            // 
            this.KeySizeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.KeySizeComboBox.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.KeySizeComboBox.FormattingEnabled = true;
            this.KeySizeComboBox.Location = new System.Drawing.Point(514, 12);
            this.KeySizeComboBox.Name = "KeySizeComboBox";
            this.KeySizeComboBox.Size = new System.Drawing.Size(200, 28);
            this.KeySizeComboBox.TabIndex = 17;
            // 
            // EncryptionWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 192);
            this.Controls.Add(this.KeySizeComboBox);
            this.Controls.Add(this.KeySizeLabel);
            this.Controls.Add(this.ModulusTextBox);
            this.Controls.Add(this.PublicKeyExponentTextBox);
            this.Controls.Add(this.PrivateKeyExponentTextBox);
            this.Controls.Add(this.BlockCipherLabel);
            this.Controls.Add(this.MethodLabel);
            this.Controls.Add(this.BlockCipherComboBox);
            this.Controls.Add(this.MethodComboBox);
            this.Controls.Add(this.ModulusLabel);
            this.Controls.Add(this.PublicKeyExponentLabel);
            this.Controls.Add(this.PrivateKeyExponentLabel);
            this.Controls.Add(this.GenerateKeysButton);
            this.Controls.Add(this.DecryptButton);
            this.Controls.Add(this.EncryptButton);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "EncryptionWindow";
            this.Text = "RSA Encryption";
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
        private System.Windows.Forms.Label MethodLabel;
        private System.Windows.Forms.ComboBox BlockCipherComboBox;
        private System.Windows.Forms.ComboBox MethodComboBox;
        private System.Windows.Forms.Label BlockCipherLabel;
        private System.Windows.Forms.TextBox ModulusTextBox;
        private System.Windows.Forms.TextBox PublicKeyExponentTextBox;
        private System.Windows.Forms.TextBox PrivateKeyExponentTextBox;
        private System.Windows.Forms.Label KeySizeLabel;
        private System.Windows.Forms.ComboBox KeySizeComboBox;
    }
}