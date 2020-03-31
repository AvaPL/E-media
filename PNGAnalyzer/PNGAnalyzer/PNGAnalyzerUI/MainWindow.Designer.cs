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
            this.SuspendLayout();
            // 
            // FilepathTextBox
            // 
            this.FilepathTextBox.Location = new System.Drawing.Point(10, 10);
            this.FilepathTextBox.Name = "FilepathTextBox";
            this.FilepathTextBox.ReadOnly = true;
            this.FilepathTextBox.Size = new System.Drawing.Size(329, 20);
            this.FilepathTextBox.TabIndex = 0;
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(344, 10);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(61, 20);
            this.BrowseButton.TabIndex = 1;
            this.BrowseButton.Text = "Browse...";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // ChunksButton
            // 
            this.ChunksButton.Enabled = false;
            this.ChunksButton.Location = new System.Drawing.Point(10, 36);
            this.ChunksButton.Name = "ChunksButton";
            this.ChunksButton.Size = new System.Drawing.Size(128, 20);
            this.ChunksButton.TabIndex = 2;
            this.ChunksButton.Text = "Chunks";
            this.ChunksButton.UseVisualStyleBackColor = true;
            this.ChunksButton.Click += new System.EventHandler(this.ChunksButton_Click);
            // 
            // AnonymizeButton
            // 
            this.AnonymizeButton.Enabled = false;
            this.AnonymizeButton.Location = new System.Drawing.Point(143, 36);
            this.AnonymizeButton.Name = "AnonymizeButton";
            this.AnonymizeButton.Size = new System.Drawing.Size(129, 20);
            this.AnonymizeButton.TabIndex = 3;
            this.AnonymizeButton.Text = "Anonymize";
            this.AnonymizeButton.UseVisualStyleBackColor = true;
            this.AnonymizeButton.Click += new System.EventHandler(this.AnonymizeButton_Click);
            // 
            // FourierTransformButton
            // 
            this.FourierTransformButton.Enabled = false;
            this.FourierTransformButton.Location = new System.Drawing.Point(277, 36);
            this.FourierTransformButton.Name = "FourierTransformButton";
            this.FourierTransformButton.Size = new System.Drawing.Size(128, 20);
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
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 66);
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
    }
}