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
            this.MicrosoftRSAButton = new System.Windows.Forms.Button();
            this.MyRSAButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FilepathTextBox
            // 
            this.FilepathTextBox.Location = new System.Drawing.Point(12, 12);
            this.FilepathTextBox.Name = "FilepathTextBox";
            this.FilepathTextBox.ReadOnly = true;
            this.FilepathTextBox.Size = new System.Drawing.Size(331, 23);
            this.FilepathTextBox.TabIndex = 0;
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(349, 12);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(71, 23);
            this.BrowseButton.TabIndex = 1;
            this.BrowseButton.Text = "Browse...";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // ChunksButton
            // 
            this.ChunksButton.Enabled = false;
            this.ChunksButton.Location = new System.Drawing.Point(12, 42);
            this.ChunksButton.Name = "ChunksButton";
            this.ChunksButton.Size = new System.Drawing.Size(132, 23);
            this.ChunksButton.TabIndex = 2;
            this.ChunksButton.Text = "Chunks";
            this.ChunksButton.UseVisualStyleBackColor = true;
            this.ChunksButton.Click += new System.EventHandler(this.ChunksButton_Click);
            // 
            // AnonymizeButton
            // 
            this.AnonymizeButton.Enabled = false;
            this.AnonymizeButton.Location = new System.Drawing.Point(150, 42);
            this.AnonymizeButton.Name = "AnonymizeButton";
            this.AnonymizeButton.Size = new System.Drawing.Size(132, 23);
            this.AnonymizeButton.TabIndex = 3;
            this.AnonymizeButton.Text = "Anonymize";
            this.AnonymizeButton.UseVisualStyleBackColor = true;
            this.AnonymizeButton.Click += new System.EventHandler(this.AnonymizeButton_Click);
            // 
            // FourierTransformButton
            // 
            this.FourierTransformButton.Enabled = false;
            this.FourierTransformButton.Location = new System.Drawing.Point(288, 42);
            this.FourierTransformButton.Name = "FourierTransformButton";
            this.FourierTransformButton.Size = new System.Drawing.Size(132, 23);
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
            // MicrosoftRSAButton
            // 
            this.MicrosoftRSAButton.Location = new System.Drawing.Point(12, 72);
            this.MicrosoftRSAButton.Name = "MicrosoftRSAButton";
            this.MicrosoftRSAButton.Size = new System.Drawing.Size(202, 23);
            this.MicrosoftRSAButton.TabIndex = 5;
            this.MicrosoftRSAButton.Text = "MicrosoftRSA Encryption";
            this.MicrosoftRSAButton.UseVisualStyleBackColor = true;
            this.MicrosoftRSAButton.Click += new System.EventHandler(this.MicrosoftRSAButton_Click);
            // 
            // MyRSAButton
            // 
            this.MyRSAButton.Location = new System.Drawing.Point(218, 72);
            this.MyRSAButton.Name = "MyRSAButton";
            this.MyRSAButton.Size = new System.Drawing.Size(202, 23);
            this.MyRSAButton.TabIndex = 6;
            this.MyRSAButton.Text = "MyRSA Encryption";
            this.MyRSAButton.UseVisualStyleBackColor = true;
            this.MyRSAButton.Click += new System.EventHandler(this.MyRSAButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 106);
            this.Controls.Add(this.MyRSAButton);
            this.Controls.Add(this.MicrosoftRSAButton);
            this.Controls.Add(this.FourierTransformButton);
            this.Controls.Add(this.AnonymizeButton);
            this.Controls.Add(this.ChunksButton);
            this.Controls.Add(this.BrowseButton);
            this.Controls.Add(this.FilepathTextBox);
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
        private System.Windows.Forms.Button MyRSAButton;
        private System.Windows.Forms.Button MicrosoftRSAButton;
    }
}