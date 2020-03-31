﻿using System.ComponentModel;

namespace PNGAnalyzerUI
{
    partial class ImageInfo
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
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.InfoText = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBox
            // 
            this.PictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureBox.Location = new System.Drawing.Point(12, 12);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(769, 737);
            this.PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBox.TabIndex = 0;
            this.PictureBox.TabStop = false;
            // 
            // ChunkInfo
            // 
            this.InfoText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InfoText.BackColor = System.Drawing.SystemColors.Window;
            this.InfoText.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.InfoText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.InfoText.Location = new System.Drawing.Point(787, 12);
            this.InfoText.MaxLength = 0;
            this.InfoText.Multiline = true;
            this.InfoText.Name = "InfoText";
            this.InfoText.ReadOnly = true;
            this.InfoText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.InfoText.Size = new System.Drawing.Size(385, 737);
            this.InfoText.TabIndex = 1;
            // 
            // ImageInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.InfoText);
            this.Controls.Add(this.PictureBox);
            this.MaximizeBox = false;
            this.Name = "ImageInfo";
            this.Text = "Chunks";
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureBox;
        private System.Windows.Forms.TextBox InfoText;
    }
}