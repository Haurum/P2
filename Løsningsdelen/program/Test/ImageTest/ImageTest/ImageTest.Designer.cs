namespace ImageTest
{
    partial class ImageTestForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageTestForm));
            this.background = new System.Windows.Forms.PictureBox();
            this.LoadGPXButton = new System.Windows.Forms.Button();
            this.PlayButton = new System.Windows.Forms.Button();
            this.lineDrawTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.background)).BeginInit();
            this.SuspendLayout();
            // 
            // background
            // 
            this.background.Image = ((System.Drawing.Image)(resources.GetObject("background.Image")));
            this.background.Location = new System.Drawing.Point(0, 0);
            this.background.Name = "background";
            this.background.Size = new System.Drawing.Size(2875, 3256);
            this.background.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.background.TabIndex = 0;
            this.background.TabStop = false;
            this.background.Paint += new System.Windows.Forms.PaintEventHandler(this.background_Paint);
            this.background.MouseDown += new System.Windows.Forms.MouseEventHandler(this.backGround_MouseDown);
            this.background.MouseEnter += new System.EventHandler(this.backGround_MouseEnter);
            this.background.MouseMove += new System.Windows.Forms.MouseEventHandler(this.backGround_MouseMove);
            this.background.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.backGround_MouseWheel);
            // 
            // LoadGPXButton
            // 
            this.LoadGPXButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadGPXButton.Location = new System.Drawing.Point(776, 406);
            this.LoadGPXButton.Name = "LoadGPXButton";
            this.LoadGPXButton.Size = new System.Drawing.Size(75, 23);
            this.LoadGPXButton.TabIndex = 1;
            this.LoadGPXButton.Text = "Load";
            this.LoadGPXButton.UseVisualStyleBackColor = true;
            this.LoadGPXButton.Click += new System.EventHandler(this.LoadGPXButton_Click);
            // 
            // PlayButton
            // 
            this.PlayButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayButton.Location = new System.Drawing.Point(695, 406);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(75, 23);
            this.PlayButton.TabIndex = 2;
            this.PlayButton.Text = "Play";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // lineDrawTimer
            // 
            this.lineDrawTimer.Interval = 50;
            this.lineDrawTimer.Tick += new System.EventHandler(this.lineDrawTimer_Tick);
            // 
            // ImageTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 441);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.LoadGPXButton);
            this.Controls.Add(this.background);
            this.Name = "ImageTestForm";
            this.Text = "ImageTest";
            ((System.ComponentModel.ISupportInitialize)(this.background)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox background;
        private System.Windows.Forms.Button LoadGPXButton;
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.Timer lineDrawTimer;

    }
}

