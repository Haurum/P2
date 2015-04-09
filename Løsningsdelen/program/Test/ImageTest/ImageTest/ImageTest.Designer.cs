namespace ImageTest
{
    partial class ImageTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageTest));
            this.backGround = new System.Windows.Forms.PictureBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.backGround)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // backGround
            // 
            this.backGround.Image = ((System.Drawing.Image)(resources.GetObject("backGround.Image")));
            this.backGround.Location = new System.Drawing.Point(0, 0);
            this.backGround.Name = "backGround";
            this.backGround.Size = new System.Drawing.Size(2875, 3256);
            this.backGround.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.backGround.TabIndex = 0;
            this.backGround.TabStop = false;
            this.backGround.MouseDown += new System.Windows.Forms.MouseEventHandler(this.backGround_MouseDown);
            this.backGround.MouseMove += new System.Windows.Forms.MouseEventHandler(this.backGround_MouseMove);
            // 
            // trackBar1
            // 
            this.trackBar1.LargeChange = 1;
            this.trackBar1.Location = new System.Drawing.Point(13, 13);
            this.trackBar1.Maximum = 12;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(104, 45);
            this.trackBar1.TabIndex = 1;
            this.trackBar1.Value = 10;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // ImageTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 441);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.backGround);
            this.Name = "ImageTest";
            this.Text = "ImageTest";
            ((System.ComponentModel.ISupportInitialize)(this.backGround)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox backGround;
        private System.Windows.Forms.TrackBar trackBar1;

    }
}

