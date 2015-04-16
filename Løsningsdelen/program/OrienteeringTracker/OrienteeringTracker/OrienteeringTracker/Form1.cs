using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrienteeringTracker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {      
            InitializeComponent();
            OriginalMap = Map1.Image as Bitmap;
        }

        #region Varibles

        Color[] Colors = { Color.Blue, Color.Red, Color.Black, Color.Purple, Color.Turquoise, Color.Orange };
        List<Route> Routes = new List<Route>();
        List<PointF[]> RoutesToDraw = new List<PointF[]>();
        private Bitmap OriginalMap;
        private int MousePosX, MousePosY;
        private float ZoomFactor = 1;
        private int TailLenght = 30;
        private int MaxLenght = 0;
        private int Tempo;

        #endregion

        private void Map1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Map1.Top += (e.Y - MousePosY);
                Map1.Left += (e.X - MousePosX);
            }

        }

        private void Map1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MousePosX = e.X;
                MousePosY = e.Y;
            }

        }
        private void Map1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                ZoomFactor *= 1.25f;
            }
            else
            {
                ZoomFactor /= 1.25f;
            }
            Map1.Height = (int)(OriginalMap.Height * ZoomFactor);
            Map1.Width = (int)(OriginalMap.Width * ZoomFactor);
            Map1.Refresh();
        }

        private void Map1_MouseEnter(object sender, EventArgs e)
        {
            if (Map1.Focused == false)
            {
                Map1.Focus();
            }
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult dr = fbd.ShowDialog();
            if(dr == DialogResult.OK)
            {
                string[] files = Directory.GetFiles(fbd.SelectedPath);

                foreach (string file in files)
                {
                    Routes.Add(Helper.ReadGPXData(new FileStream(file, FileMode.Open)));
                }
                PlayBar.Maximum = Routes.Max(r => r.Coords.Count) + 1;
                foreach (Route route in Routes)
                {
                    RoutesToDraw.Add(new PointF[TailLenght]);
                    RunnersCheckBox.Items.Add(route.RunnerName);
                }
                LoadButton.Hide();
                ResetButton.Show();
            }
            
	    }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            
            if (PlayTimer.Enabled)
            {
                PlayTimer.Stop();
            }
            else
            {
                PlayTimer.Start();
            }
        }

        int ticks = 0;
        private void PlayTimer_Tick(object sender, EventArgs e)
        {
            UpdateState();
            ticks++;
        }

        private void UpdateState()
        {
            if (ticks > TailLenght)
            {
                for (int routeNum = 0; routeNum < RoutesToDraw.Count; routeNum++)
                {
                    for (int pointNum = 0; pointNum < TailLenght; pointNum++)
                    {
                        if (ticks < Routes[routeNum].Coords.Count)
                        {
                            RoutesToDraw[routeNum][pointNum] = Routes[routeNum].Coords[ticks - (TailLenght - pointNum)].p;
                        }
                        else
                        {
                            RoutesToDraw[routeNum][pointNum] = new PointF();
                        }
                    }
                }
                PlayBar.Value = ticks;
                Map1.Refresh();
            }
            if (ticks >= Routes.Max(r => r.Coords.Count))
            {
                PlayTimer.Stop();
            }
        }

        private void Map1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush brush;
            Pen pen;
            int colorIndex = 0;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            foreach (PointF[] route in RoutesToDraw)
            {
                brush = new SolidBrush(Colors[colorIndex]);
                colorIndex++;
                pen = new Pen(brush, 3);
                for (int i = 0; i < TailLenght; i++ )
                {
                    route[i].X *= ZoomFactor;
                    route[i].Y *= ZoomFactor;
                }
                g.DrawLines(pen, route);
                g.FillEllipse(brush, route[route.Length - 1].X - 4, route[route.Length - 1].Y - 4, 8, 8);
            }
        }

        private void PlayBar_Scroll(object sender, EventArgs e)
        {
            ticks = PlayBar.Value;
        }

        private void TempoUpDown_ValueChanged(object sender, EventArgs e)
        {
            PlayTimer.Interval = (int)(1000 / TempoUpDown.Value);
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            Routes.Clear();
            RoutesToDraw.Clear();
            ticks = 0;
            PlayTimer.Stop();
            Map1.Refresh();
            ResetButton.Hide();
            LoadButton.Show();  
        }


    }
}
