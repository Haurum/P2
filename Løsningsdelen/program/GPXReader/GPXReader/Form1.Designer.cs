namespace GPXReader
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.LoadGPXButton = new System.Windows.Forms.Button();
            this.CoordinatesLabel = new System.Windows.Forms.Label();
            this.CoordinatesConvertedLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LoadGPXButton
            // 
            this.LoadGPXButton.Location = new System.Drawing.Point(994, 518);
            this.LoadGPXButton.Name = "LoadGPXButton";
            this.LoadGPXButton.Size = new System.Drawing.Size(75, 23);
            this.LoadGPXButton.TabIndex = 0;
            this.LoadGPXButton.Text = "Load";
            this.LoadGPXButton.UseVisualStyleBackColor = true;
            this.LoadGPXButton.Click += new System.EventHandler(this.LoadGPXButton_Click);
            // 
            // CoordinatesLabel
            // 
            this.CoordinatesLabel.AutoSize = true;
            this.CoordinatesLabel.Location = new System.Drawing.Point(13, 13);
            this.CoordinatesLabel.Name = "CoordinatesLabel";
            this.CoordinatesLabel.Size = new System.Drawing.Size(0, 13);
            this.CoordinatesLabel.TabIndex = 1;
            // 
            // CoordinatesConvertedLabel
            // 
            this.CoordinatesConvertedLabel.AutoSize = true;
            this.CoordinatesConvertedLabel.Location = new System.Drawing.Point(567, 24);
            this.CoordinatesConvertedLabel.Name = "CoordinatesConvertedLabel";
            this.CoordinatesConvertedLabel.Size = new System.Drawing.Size(0, 13);
            this.CoordinatesConvertedLabel.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 553);
            this.Controls.Add(this.CoordinatesConvertedLabel);
            this.Controls.Add(this.CoordinatesLabel);
            this.Controls.Add(this.LoadGPXButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoadGPXButton;
        private System.Windows.Forms.Label CoordinatesLabel;
        private System.Windows.Forms.Label CoordinatesConvertedLabel;
    }
}

