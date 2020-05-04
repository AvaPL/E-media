﻿using System.ComponentModel;

namespace PNGAnalyzerUI
{
    partial class MainWindow
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
            this.FilepathTextBox = new System.Windows.Forms.TextBox();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.ChunksButton = new System.Windows.Forms.Button();
            this.AnonymizeButton = new System.Windows.Forms.Button();
            this.FourierTransformButton = new System.Windows.Forms.Button();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.EncryptionButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FilepathTextBox
            // 
            this.FilepathTextBox.Location = new System.Drawing.Point(13, 15);
            this.FilepathTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FilepathTextBox.Name = "FilepathTextBox";
            this.FilepathTextBox.ReadOnly = true;
            this.FilepathTextBox.Size = new System.Drawing.Size(579, 27);
            this.FilepathTextBox.TabIndex = 0;
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(600, 13);
            this.BrowseButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(81, 31);
            this.BrowseButton.TabIndex = 1;
            this.BrowseButton.Text = "Browse...";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // ChunksButton
            // 
            this.ChunksButton.Enabled = false;
            this.ChunksButton.Location = new System.Drawing.Point(10, 55);
            this.ChunksButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ChunksButton.Name = "ChunksButton";
            this.ChunksButton.Size = new System.Drawing.Size(171, 31);
            this.ChunksButton.TabIndex = 2;
            this.ChunksButton.Text = "Chunks";
            this.ChunksButton.UseVisualStyleBackColor = true;
            this.ChunksButton.Click += new System.EventHandler(this.ChunksButton_Click);
            // 
            // AnonymizeButton
            // 
            this.AnonymizeButton.Enabled = false;
            this.AnonymizeButton.Location = new System.Drawing.Point(189, 55);
            this.AnonymizeButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.AnonymizeButton.Name = "AnonymizeButton";
            this.AnonymizeButton.Size = new System.Drawing.Size(172, 31);
            this.AnonymizeButton.TabIndex = 3;
            this.AnonymizeButton.Text = "Anonymize";
            this.AnonymizeButton.UseVisualStyleBackColor = true;
            this.AnonymizeButton.Click += new System.EventHandler(this.AnonymizeButton_Click);
            // 
            // FourierTransformButton
            // 
            this.FourierTransformButton.Enabled = false;
            this.FourierTransformButton.Location = new System.Drawing.Point(369, 55);
            this.FourierTransformButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FourierTransformButton.Name = "FourierTransformButton";
            this.FourierTransformButton.Size = new System.Drawing.Size(171, 31);
            this.FourierTransformButton.TabIndex = 4;
            this.FourierTransformButton.Text = "Fourier transform";
            this.FourierTransformButton.UseVisualStyleBackColor = true;
            this.FourierTransformButton.Click += new System.EventHandler(this.FourierTransformButton_Click);
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.DefaultExt = "png";
            this.OpenFileDialog.Filter = "PNG files (*.png)|*.png|All files (*.*)|*.*";
            // 
            // SaveFileDialog
            // 
            this.SaveFileDialog.DefaultExt = "png";
            this.SaveFileDialog.Filter = "PNG files (*.png)|*.png|All files (*.*)|*.*";
            // 
            // EncryptionButton
            // 
            this.EncryptionButton.Location = new System.Drawing.Point(547, 55);
            this.EncryptionButton.Name = "EncryptionButton";
            this.EncryptionButton.Size = new System.Drawing.Size(134, 31);
            this.EncryptionButton.TabIndex = 5;
            this.EncryptionButton.Text = "Encryption";
            this.EncryptionButton.UseVisualStyleBackColor = true;
            this.EncryptionButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 101);
            this.Controls.Add(this.EncryptionButton);
            this.Controls.Add(this.FourierTransformButton);
            this.Controls.Add(this.AnonymizeButton);
            this.Controls.Add(this.ChunksButton);
            this.Controls.Add(this.BrowseButton);
            this.Controls.Add(this.FilepathTextBox);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainWindow";
            this.Text = "PNG Analyzer";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button FourierTransformButton;
        private System.Windows.Forms.Button AnonymizeButton;
        private System.Windows.Forms.Button ChunksButton;
        private System.Windows.Forms.TextBox FilepathTextBox;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.Button EncryptionButton;
    }
}