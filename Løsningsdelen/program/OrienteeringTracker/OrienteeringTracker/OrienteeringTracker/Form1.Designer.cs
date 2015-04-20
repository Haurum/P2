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
            this.ResetButton = new System.Windows.Forms.Button();
            this.RunnersCheckBox = new System.Windows.Forms.CheckedListBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.mapTab = new System.Windows.Forms.TabPage();
            this.tempoLabel = new System.Windows.Forms.Label();
            this.dataTab = new System.Windows.Forms.TabPage();
            this.DataTitle = new System.Windows.Forms.Label();
            this.Coordsreader = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Map1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TempoUpDown)).BeginInit();
            this.tabControl.SuspendLayout();
            this.mapTab.SuspendLayout();
            this.dataTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // Map1
            // 
            this.Map1.Image = global::OrienteeringTracker.Properties.Resources.Hjermind_Egekrat_ref_ref;
            this.Map1.Location = new System.Drawing.Point(0, 0);
            this.Map1.Margin = new System.Windows.Forms.Padding(4);
            this.Map1.Name = "Map1";
            this.Map1.Size = new System.Drawing.Size(3833, 4007);
            this.Map1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Map1.TabIndex = 0;
            this.Map1.TabStop = false;
            this.Map1.Click += new System.EventHandler(this.Map1_Click);
            this.Map1.Paint += new System.Windows.Forms.PaintEventHandler(this.Map1_Paint);
            this.Map1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Map1_MouseDown);
            this.Map1.MouseEnter += new System.EventHandler(this.Map1_MouseEnter);
            this.Map1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Map1_MouseMove);
            this.Map1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Map1_MouseWheel);
            // 
            // LoadButton
            // 
            this.LoadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadButton.Location = new System.Drawing.Point(1214, 342);
            this.LoadButton.Margin = new System.Windows.Forms.Padding(4);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(100, 28);
            this.LoadButton.TabIndex = 1;
            this.LoadButton.Text = "Load";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // PlayButton
            // 
            this.PlayButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayButton.Location = new System.Drawing.Point(1106, 342);
            this.PlayButton.Margin = new System.Windows.Forms.Padding(4);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(100, 28);
            this.PlayButton.TabIndex = 2;
            this.PlayButton.Text = "Play/Pause";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Visible = false;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // PlayTimer
            // 
            this.PlayTimer.Tick += new System.EventHandler(this.PlayTimer_Tick);
            // 
            // PlayBar
            // 
            this.PlayBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PlayBar.Location = new System.Drawing.Point(3, 378);
            this.PlayBar.Margin = new System.Windows.Forms.Padding(4);
            this.PlayBar.Name = "PlayBar";
            this.PlayBar.Size = new System.Drawing.Size(1317, 56);
            this.PlayBar.TabIndex = 3;
            this.PlayBar.Visible = false;
            this.PlayBar.Scroll += new System.EventHandler(this.PlayBar_Scroll);
            // 
            // TempoUpDown
            // 
            this.TempoUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TempoUpDown.Location = new System.Drawing.Point(9, 348);
            this.TempoUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.TempoUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.TempoUpDown.Name = "TempoUpDown";
            this.TempoUpDown.Size = new System.Drawing.Size(69, 22);
            this.TempoUpDown.TabIndex = 4;
            this.TempoUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.TempoUpDown.Visible = false;
            this.TempoUpDown.ValueChanged += new System.EventHandler(this.TempoUpDown_ValueChanged);
            // 
            // ResetButton
            // 
            this.ResetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ResetButton.Location = new System.Drawing.Point(1214, 342);
            this.ResetButton.Margin = new System.Windows.Forms.Padding(4);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(100, 28);
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
            this.RunnersCheckBox.Location = new System.Drawing.Point(1162, 313);
            this.RunnersCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.RunnersCheckBox.Name = "RunnersCheckBox";
            this.RunnersCheckBox.Size = new System.Drawing.Size(152, 21);
            this.RunnersCheckBox.TabIndex = 7;
            this.RunnersCheckBox.Visible = false;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.mapTab);
            this.tabControl.Controls.Add(this.dataTab);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1331, 466);
            this.tabControl.TabIndex = 8;
            // 
            // mapTab
            // 
            this.mapTab.Controls.Add(this.Coordsreader);
            this.mapTab.Controls.Add(this.tempoLabel);
            this.mapTab.Controls.Add(this.RunnersCheckBox);
            this.mapTab.Controls.Add(this.ResetButton);
            this.mapTab.Controls.Add(this.TempoUpDown);
            this.mapTab.Controls.Add(this.PlayBar);
            this.mapTab.Controls.Add(this.PlayButton);
            this.mapTab.Controls.Add(this.LoadButton);
            this.mapTab.Controls.Add(this.Map1);
            this.mapTab.Location = new System.Drawing.Point(4, 25);
            this.mapTab.Name = "mapTab";
            this.mapTab.Padding = new System.Windows.Forms.Padding(3);
            this.mapTab.Size = new System.Drawing.Size(1323, 437);
            this.mapTab.TabIndex = 0;
            this.mapTab.Text = "Map View";
            this.mapTab.UseVisualStyleBackColor = true;
            // 
            // tempoLabel
            // 
            this.tempoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tempoLabel.AutoSize = true;
            this.tempoLabel.Location = new System.Drawing.Point(9, 327);
            this.tempoLabel.Name = "tempoLabel";
            this.tempoLabel.Size = new System.Drawing.Size(52, 17);
            this.tempoLabel.TabIndex = 8;
            this.tempoLabel.Text = "Tempo";
            this.tempoLabel.Visible = false;
            // 
            // dataTab
            // 
            this.dataTab.Controls.Add(this.DataTitle);
            this.dataTab.Location = new System.Drawing.Point(4, 25);
            this.dataTab.Name = "dataTab";
            this.dataTab.Padding = new System.Windows.Forms.Padding(3);
            this.dataTab.Size = new System.Drawing.Size(1323, 437);
            this.dataTab.TabIndex = 1;
            this.dataTab.Text = "Data View";
            this.dataTab.UseVisualStyleBackColor = true;
            // 
            // DataTitle
            // 
            this.DataTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.DataTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataTitle.Location = new System.Drawing.Point(3, 3);
            this.DataTitle.Name = "DataTitle";
            this.DataTitle.Size = new System.Drawing.Size(1317, 27);
            this.DataTitle.TabIndex = 0;
            this.DataTitle.Text = "S - F";
            this.DataTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // Coordsreader
            // 
            this.Coordsreader.AutoSize = true;
            this.Coordsreader.Location = new System.Drawing.Point(438, 9);
            this.Coordsreader.Name = "Coordsreader";
            this.Coordsreader.Size = new System.Drawing.Size(22, 13);
            this.Coordsreader.TabIndex = 8;
            this.Coordsreader.Text = "0,0";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1331, 466);
            this.Controls.Add(this.tabControl);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.Map1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TempoUpDown)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.mapTab.ResumeLayout(false);
            this.mapTab.PerformLayout();
            this.dataTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Map1;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.Timer PlayTimer;
        private System.Windows.Forms.TrackBar PlayBar;
        private System.Windows.Forms.NumericUpDown TempoUpDown;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.CheckedListBox RunnersCheckBox;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage mapTab;
        private System.Windows.Forms.TabPage dataTab;
        private System.Windows.Forms.Label tempoLabel;
        private System.Windows.Forms.Label DataTitle;
        private System.Windows.Forms.Label Coordsreader;
    }
}

