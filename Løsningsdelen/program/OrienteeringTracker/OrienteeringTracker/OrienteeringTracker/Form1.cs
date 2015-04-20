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

        Color[] Colors = { Color.Blue, Color.Red, Color.Black, Color.Purple, Color.Turquoise, Color.Lime };
        List<Route> Routes = new List<Route>();
        List<ControlPoint> ControlPoints = new List<ControlPoint>();
        private Bitmap OriginalMap;
        private int MousePosX, MousePosY;
        private float ZoomFactor = 1;
        private int TailLenght = 30;

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
            if (dr == DialogResult.OK)
            {
                string[] files = Directory.GetFiles(fbd.SelectedPath);

                foreach (string file in files)
                {
                    Routes.Add(Helper.ReadGPXData(new FileStream(file, FileMode.Open)));
                }
                for (int Index = 0; Index < Routes.Count; Index++)
                {
                    Routes[Index].RouteColor = Colors[Index];
                    RunnersCheckBox.Items.Add(Routes[Index].RunnerName);
                    RunnersCheckBox.SetItemChecked(Index, true);
                }
                RunnersCheckBox.ClientSize = new Size(RunnersCheckBox.Width, 
                    RunnersCheckBox.GetItemRectangle(0).Height * RunnersCheckBox.Items.Count);
                RunnersCheckBox.Top -= RunnersCheckBox.GetItemRectangle(0).Height * (RunnersCheckBox.Items.Count - 1);
                PlayBar.Maximum = Routes.Max(r => r.Coords.Count) + 1;
                PlayBar.Minimum = TailLenght;
                LoadButton.Hide();
                ResetButton.Show();
                PlayButton.Show();
                PlayBar.Show();
                TempoUpDown.Show();
                RunnersCheckBox.Show();
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
            if (ticks < TailLenght)
            {
                ticks = TailLenght + 1;
            }
            if (ticks >= Routes.Max(r => r.Coords.Count))
            {
                PlayTimer.Stop();
            }
            PlayBar.Value = ticks;
            Map1.Refresh();
            ticks++;
        }

        private void Map1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush brush;
            Pen pen;
            bool draw = true;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            foreach (Route route in Routes)
            {
                if (!RunnersCheckBox.CheckedItems.Contains(route.RunnerName))
                {
                    continue;
                }
                brush = new SolidBrush(route.RouteColor);
                pen = new Pen(brush, 3);
                PointF[] RunnerToDraw = new PointF[TailLenght];
                for (int Index = 0; Index < TailLenght; Index++)
                {
                    if (ticks >= route.Coords.Count())
                    {
                        draw = false;
                    }
                    else if(ticks > TailLenght)
                    {
                        RunnerToDraw[Index] = route.Coords[ticks - (TailLenght - Index)].pixelPoint;
                        RunnerToDraw[Index].X *= ZoomFactor;
                        RunnerToDraw[Index].Y *= ZoomFactor;
                        draw = true;
                    }

                }
                if (draw)
                {
                    g.DrawLines(pen, RunnerToDraw);
                    g.FillEllipse(brush, RunnerToDraw[RunnerToDraw.Length - 1].X - 4, 
                        RunnerToDraw[RunnerToDraw.Length - 1].Y - 4, 8, 8);
                    g.DrawString(route.RunnerName, new Font("Arial", 14, FontStyle.Bold), new SolidBrush(Color.Black),
                        RunnerToDraw[RunnerToDraw.Length - 1].X + 5, RunnerToDraw[RunnerToDraw.Length - 1].Y + 5);
                }    
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
            ticks = 0;
            PlayTimer.Stop();
            Map1.Refresh();
            ResetButton.Hide();
            PlayButton.Hide();
            PlayBar.Hide();
            TempoUpDown.Hide();
            RunnersCheckBox.Hide();
            LoadButton.Show();
            RunnersCheckBox.Items.Clear();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                ControlPoints = Helper.ReadControlPoints(ofd.FileName);
            }

            Graphics g = Graphics.FromImage(Map1.Image);
            Pen p = new Pen(Color.Magenta);
            p.Width = 5;
            int i = 0;

            foreach (ControlPoint cp in ControlPoints)
            {
                Point[] points = {new Point(Convert.ToInt32(cp.Cord.p.X + 30), Convert.ToInt32(cp.Cord.p.Y)), 
                                         new Point(Convert.ToInt32(cp.Cord.p.X - 15), Convert.ToInt32(cp.Cord.p.Y-30)), 
                                         new Point(Convert.ToInt32(cp.Cord.p.X - 15), Convert.ToInt32(cp.Cord.p.Y+30))};
                if (ControlPoints.First() == cp)
                {
                    g.DrawPolygon(p, points);
                }
                else if (ControlPoints.Last() == cp)
                {
                    g.DrawEllipse(p, cp.Cord.p.X - 17, cp.Cord.p.Y - 17, 34, 34);
                    g.DrawEllipse(p, cp.Cord.p.X - 25, cp.Cord.p.Y - 25, 50, 50);
                }
                else
                {
                    g.DrawEllipse(p, cp.Cord.p.X - 25, cp.Cord.p.Y - 25, 50, 50);
                }


                using (Font myFont = new Font("Arial", 24, FontStyle.Bold))
                {
                    g.DrawString(cp.Number.ToString(), myFont, Brushes.Magenta, new Point(Convert.ToInt32(cp.Cord.p.X + 30), Convert.ToInt32(cp.Cord.p.Y - 25 / 2)));
                }

                if (ControlPoints.First() != cp)
                {
                    float xDiff = ControlPoints[i - 1].Cord.p.X - cp.Cord.p.X;
                    float yDiff = ControlPoints[i - 1].Cord.p.Y - cp.Cord.p.Y;

                    float angle = (float)Math.Atan2(yDiff, xDiff) - (float)(Math.PI);

                    float distance = (float)Math.Sqrt(xDiff * xDiff + yDiff * yDiff);

                    float newX = (float)(ControlPoints[i - 1].Cord.p.X + (Math.Cos(angle) * (distance - 25)));
                    float newY = (float)(ControlPoints[i - 1].Cord.p.Y + (Math.Sin(angle) * (distance - 25)));

                    g.DrawLine(p, new Point(Convert.ToInt32(ControlPoints[i - 1].Cord.p.X + Math.Cos(angle) * 25), Convert.ToInt32(ControlPoints[i - 1].Cord.p.Y + Math.Sin(angle) * 25)), new Point(Convert.ToInt32(newX), Convert.ToInt32(newY)));
                }
                i++;
            }
        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            Coordsreader.Text = e.Location.X * ZoomFactor + ", " + e.Location.Y * ZoomFactor;
        }

        private void Map1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Coordsreader.Text = me.Location.X / ZoomFactor + ", " + me.Location.Y / ZoomFactor;
        }


    }
}
