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
            this.KeyTextBox = new System.Windows.Forms.TextBox();
            this.EncryptButton = new System.Windows.Forms.Button();
            this.DecryptButton = new System.Windows.Forms.Button();
            this.GenerateKeysButton = new System.Windows.Forms.Button();
            this.PrivateKeyTextBox = new System.Windows.Forms.TextBox();
            this.privateKeyLabel = new System.Windows.Forms.Label();
            this.PublicKeyTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // KeyTextBox
            // 
            this.KeyTextBox.Location = new System.Drawing.Point(22, 25);
            this.KeyTextBox.Multiline = true;
            this.KeyTextBox.Name = "KeyTextBox";
            this.KeyTextBox.Size = new System.Drawing.Size(604, 88);
            this.KeyTextBox.TabIndex = 0;
            // 
            // EncryptButton
            // 
            this.EncryptButton.Location = new System.Drawing.Point(22, 134);
            this.EncryptButton.Name = "EncryptButton";
            this.EncryptButton.Size = new System.Drawing.Size(174, 33);
            this.EncryptButton.TabIndex = 1;
            this.EncryptButton.Text = "Encrypt";
            this.EncryptButton.UseVisualStyleBackColor = true;
            this.EncryptButton.Click += new System.EventHandler(this.EncryptButton_Click);
            // 
            // DecryptButton
            // 
            this.DecryptButton.Location = new System.Drawing.Point(234, 134);
            this.DecryptButton.Name = "DecryptButton";
            this.DecryptButton.Size = new System.Drawing.Size(181, 33);
            this.DecryptButton.TabIndex = 2;
            this.DecryptButton.Text = "Decrypt";
            this.DecryptButton.UseVisualStyleBackColor = true;
            this.DecryptButton.Click += new System.EventHandler(this.DecryptButton_Click);
            // 
            // GenerateKeysButton
            // 
            this.GenerateKeysButton.Location = new System.Drawing.Point(444, 134);
            this.GenerateKeysButton.Name = "GenerateKeysButton";
            this.GenerateKeysButton.Size = new System.Drawing.Size(182, 33);
            this.GenerateKeysButton.TabIndex = 3;
            this.GenerateKeysButton.Text = "Generate keys";
            this.GenerateKeysButton.UseVisualStyleBackColor = true;
            this.GenerateKeysButton.Click += new System.EventHandler(this.GenerateKeysButton_Click);
            // 
            // PrivateKeyTextBox
            // 
            this.PrivateKeyTextBox.Location = new System.Drawing.Point(22, 245);
            this.PrivateKeyTextBox.Multiline = true;
            this.PrivateKeyTextBox.Name = "PrivateKeyTextBox";
            this.PrivateKeyTextBox.ReadOnly = true;
            this.PrivateKeyTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.PrivateKeyTextBox.Size = new System.Drawing.Size(289, 154);
            this.PrivateKeyTextBox.TabIndex = 4;
            // 
            // privateKeyLabel
            // 
            this.privateKeyLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.privateKeyLabel.Location = new System.Drawing.Point(22, 210);
            this.privateKeyLabel.Name = "privateKeyLabel";
            this.privateKeyLabel.Size = new System.Drawing.Size(160, 32);
            this.privateKeyLabel.TabIndex = 5;
            this.privateKeyLabel.Text = "Private key:";
            // 
            // PublicKeyTextbox
            // 
            this.PublicKeyTextbox.Location = new System.Drawing.Point(329, 245);
            this.PublicKeyTextbox.Multiline = true;
            this.PublicKeyTextbox.Name = "PublicKeyTextbox";
            this.PublicKeyTextbox.ReadOnly = true;
            this.PublicKeyTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.PublicKeyTextbox.Size = new System.Drawing.Size(297, 154);
            this.PublicKeyTextbox.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label1.Location = new System.Drawing.Point(329, 210);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 32);
            this.label1.TabIndex = 7;
            this.label1.Text = "Public key:";
            // 
            // EncryptionWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 411);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PublicKeyTextbox);
            this.Controls.Add(this.privateKeyLabel);
            this.Controls.Add(this.PrivateKeyTextBox);
            this.Controls.Add(this.GenerateKeysButton);
            this.Controls.Add(this.DecryptButton);
            this.Controls.Add(this.EncryptButton);
            this.Controls.Add(this.KeyTextBox);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "EncryptionWindow";
            this.Text = "RSA Encryption";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label privateKeyLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PublicKeyTextbox;
        private System.Windows.Forms.TextBox PrivateKeyTextBox;
        private System.Windows.Forms.Button GenerateKeysButton;
        private System.Windows.Forms.Button DecryptButton;
        private System.Windows.Forms.Button EncryptButton;
        private System.Windows.Forms.TextBox KeyTextBox;
    }
}