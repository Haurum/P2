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
            this.components = new System.ComponentModel.Container();
            this.Map1 = new System.Windows.Forms.PictureBox();
            this.LoadButton = new System.Windows.Forms.Button();
            this.PlayButton = new System.Windows.Forms.Button();
            this.PlayTimer = new System.Windows.Forms.Timer(this.components);
            this.PlayBar = new System.Windows.Forms.TrackBar();
            this.TempoUpDown = new System.Windows.Forms.NumericUpDown();
            this.StartingpointUpDown = new System.Windows.Forms.NumericUpDown();
            this.ResetButton = new System.Windows.Forms.Button();
            this.RunnersCheckBox = new System.Windows.Forms.CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)(this.Map1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TempoUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartingpointUpDown)).BeginInit();
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
            this.Map1.Paint += new System.Windows.Forms.PaintEventHandler(this.Map1_Paint);
            this.Map1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Map1_MouseDown);
            this.Map1.MouseEnter += new System.EventHandler(this.Map1_MouseEnter);
            this.Map1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Map1_MouseMove);
            this.Map1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Map1_MouseWheel);
            // 
            // LoadButton
            // 
            this.LoadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadButton.Location = new System.Drawing.Point(911, 305);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(75, 23);
            this.LoadButton.TabIndex = 1;
            this.LoadButton.Text = "Load";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // PlayButton
            // 
            this.PlayButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayButton.Location = new System.Drawing.Point(830, 305);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(75, 23);
            this.PlayButton.TabIndex = 2;
            this.PlayButton.Text = "Play/Pause";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // PlayTimer
            // 
            this.PlayTimer.Interval = 200;
            this.PlayTimer.Tick += new System.EventHandler(this.PlayTimer_Tick);
            // 
            // PlayBar
            // 
            this.PlayBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PlayBar.Location = new System.Drawing.Point(0, 334);
            this.PlayBar.Name = "PlayBar";
            this.PlayBar.Size = new System.Drawing.Size(998, 45);
            this.PlayBar.TabIndex = 3;
            this.PlayBar.Scroll += new System.EventHandler(this.PlayBar_Scroll);
            // 
            // TempoUpDown
            // 
            this.TempoUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TempoUpDown.Location = new System.Drawing.Point(12, 308);
            this.TempoUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.TempoUpDown.Name = "TempoUpDown";
            this.TempoUpDown.Size = new System.Drawing.Size(66, 20);
            this.TempoUpDown.TabIndex = 4;
            this.TempoUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.TempoUpDown.ValueChanged += new System.EventHandler(this.TempoUpDown_ValueChanged);
            // 
            // StartingpointUpDown
            // 
            this.StartingpointUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.StartingpointUpDown.Location = new System.Drawing.Point(84, 308);
            this.StartingpointUpDown.Name = "StartingpointUpDown";
            this.StartingpointUpDown.Size = new System.Drawing.Size(66, 20);
            this.StartingpointUpDown.TabIndex = 5;
            // 
            // ResetButton
            // 
            this.ResetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ResetButton.Location = new System.Drawing.Point(911, 305);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(75, 23);
            this.ResetButton.TabIndex = 6;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Visible = false;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // RunnersCheckBox
            // 
            this.RunnersCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RunnersCheckBox.FormattingEnabled = true;
            this.RunnersCheckBox.Location = new System.Drawing.Point(866, 205);
            this.RunnersCheckBox.Name = "RunnersCheckBox";
            this.RunnersCheckBox.Size = new System.Drawing.Size(120, 94);
            this.RunnersCheckBox.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 379);
            this.Controls.Add(this.RunnersCheckBox);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.StartingpointUpDown);
            this.Controls.Add(this.TempoUpDown);
            this.Controls.Add(this.PlayBar);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.Map1);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.Map1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TempoUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartingpointUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Map1;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.Timer PlayTimer;
        private System.Windows.Forms.TrackBar PlayBar;
        private System.Windows.Forms.NumericUpDown TempoUpDown;
        private System.Windows.Forms.NumericUpDown StartingpointUpDown;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.CheckedListBox RunnersCheckBox;
    }
}

