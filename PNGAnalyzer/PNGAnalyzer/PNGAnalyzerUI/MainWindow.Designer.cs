using System.ComponentModel;

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
            this.button1 = new System.Windows.Forms.Button();
            this.OurEncryption = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FilepathTextBox
            // 
            this.FilepathTextBox.Location = new System.Drawing.Point(13, 15);
            this.FilepathTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FilepathTextBox.Name = "FilepathTextBox";
            this.FilepathTextBox.ReadOnly = true;
            this.FilepathTextBox.Size = new System.Drawing.Size(681, 27);
            this.FilepathTextBox.TabIndex = 0;
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(721, 13);
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
            this.ChunksButton.Size = new System.Drawing.Size(118, 31);
            this.ChunksButton.TabIndex = 2;
            this.ChunksButton.Text = "Chunks";
            this.ChunksButton.UseVisualStyleBackColor = true;
            this.ChunksButton.Click += new System.EventHandler(this.ChunksButton_Click);
            // 
            // AnonymizeButton
            // 
            this.AnonymizeButton.Enabled = false;
            this.AnonymizeButton.Location = new System.Drawing.Point(136, 55);
            this.AnonymizeButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.AnonymizeButton.Name = "AnonymizeButton";
            this.AnonymizeButton.Size = new System.Drawing.Size(132, 31);
            this.AnonymizeButton.TabIndex = 3;
            this.AnonymizeButton.Text = "Anonymize";
            this.AnonymizeButton.UseVisualStyleBackColor = true;
            this.AnonymizeButton.Click += new System.EventHandler(this.AnonymizeButton_Click);
            // 
            // FourierTransformButton
            // 
            this.FourierTransformButton.Enabled = false;
            this.FourierTransformButton.Location = new System.Drawing.Point(276, 55);
            this.FourierTransformButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FourierTransformButton.Name = "FourierTransformButton";
            this.FourierTransformButton.Size = new System.Drawing.Size(151, 31);
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(434, 57);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(173, 31);
            this.button1.TabIndex = 5;
            this.button1.Text = "Microsoft Encryption";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // OurEncryption
            // 
            this.OurEncryption.Location = new System.Drawing.Point(613, 57);
            this.OurEncryption.Name = "OurEncryption";
            this.OurEncryption.Size = new System.Drawing.Size(189, 31);
            this.OurEncryption.TabIndex = 6;
            this.OurEncryption.Text = "PajaceTeam Encryption";
            this.OurEncryption.UseVisualStyleBackColor = true;
            this.OurEncryption.Click += new System.EventHandler(this.OurEncryption_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 99);
            this.Controls.Add(this.OurEncryption);
            this.Controls.Add(this.button1);
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button OurEncryption;
    }
}