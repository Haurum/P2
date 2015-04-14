namespace OrienteeringTracker
{
    partial class MainForm
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
            this.Map1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Map1)).BeginInit();
            this.SuspendLayout();
            // 
            // Map1
            // 
            this.Map1.Image = global::OrienteeringTracker.Properties.Resources.Hjermind_Egekrat_ref_ref;
            this.Map1.Location = new System.Drawing.Point(0, 0);
            this.Map1.Name = "Map1";
            this.Map1.Size = new System.Drawing.Size(2875, 3256);
            this.Map1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Map1.TabIndex = 0;
            this.Map1.TabStop = false;
            this.Map1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Map1_MouseDown);
            this.Map1.MouseEnter += new System.EventHandler(this.Map1_MouseEnter);
            this.Map1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Map1_MouseMove);
            this.Map1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Map1_MouseWheel);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.Map1);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.Map1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox Map1;
    }
}

