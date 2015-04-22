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
        int ticks = 0;

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

                Route route = new Route();

                foreach (string file in files)
                {
                    route = Helper.ReadGPXData(new FileStream(file, FileMode.Open));
                    Routes.Add(route);
                }
                for (int Index = 0; Index < Routes.Count; Index++)
                {
                    Routes[Index].RouteColor = Colors[Index];
                    Routes[Index].startingTick.Add(0);
                    Routes[Index].startingTick.Add(422);
                    RunnersCheckBox.Items.Add(Routes[Index].RunnerName);
                    RunnersCheckBox.SetItemChecked(Index, true);
                }
                RunnersCheckBox.ClientSize = new Size(RunnersCheckBox.Width, 
                    RunnersCheckBox.GetItemRectangle(0).Height * RunnersCheckBox.Items.Count);
                RunnersCheckBox.Top -= RunnersCheckBox.GetItemRectangle(0).Height * (RunnersCheckBox.Items.Count - 1);
                PlayBar.Maximum = Routes.Max(r => r.Coords.Count - r.startingTick[0]) + 1;
                LoadButton.Hide();
                ResetButton.Show();
                PlayButton.Show();
                PlayBar.Show();
                TempoUpDown.Show();
                RunnersCheckBox.Show();
                tempoLabel.Show();
                StartpointLabel.Show();
                StartpointUpDown.Show();
            }

            foreach (Route r in Routes)
            {
                foreach (ControlPoint cp in ControlPoints)
                {
                    r.Visited.Add(Helper.ControlPointChecker(cp, r));
                }
            }
            
            // Tjekker bare lige hvordan det ser ud
            //Graphics g = Graphics.FromImage(Map1.Image);
            //Pen p = new Pen(Color.Black,5);
            //foreach (Distance d in distList)
            //{
            //    if (d.CP != null)
            //        g.DrawEllipse(p, Routes[2].Coords.ElementAt(d.Tick).pixelPoint.X, Routes[2].Coords.ElementAt(d.Tick).pixelPoint.Y, 10, 10);
            //}
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

        private void PlayTimer_Tick(object sender, EventArgs e)
        {
            if (ticks >= Routes.Max(r => r.Coords.Count - r.startingTick[(int)StartpointUpDown.Value]))
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
            bool draw = false;
            int tempTailLenght = TailLenght;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Map1.Height = (int)(OriginalMap.Height * ZoomFactor);
            Map1.Width = (int)(OriginalMap.Width * ZoomFactor);
            foreach (Route route in Routes)
            {
                if (!RunnersCheckBox.CheckedItems.Contains(route.RunnerName) || ticks < 2)
                {
                    continue;
                }
                brush = new SolidBrush(route.RouteColor);
                pen = new Pen(brush, 3);
                if (ticks < TailLenght)
                {
                    tempTailLenght = ticks;
                }
                PointF[] RunnerToDraw = new PointF[tempTailLenght];
                for (int Index = 0; Index < tempTailLenght; Index++)
                {
                    if (ticks >= route.Coords.Count() - route.startingTick[(int)StartpointUpDown.Value])
                    {
                        draw = false;
                    }
                    else
                    {
                        RunnerToDraw[Index] = route.Coords[ticks - (tempTailLenght - Index) + route.startingTick[(int)StartpointUpDown.Value]].pixelPoint;
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
                    //Coordsreader.Text = Routes[1].Coords[ticks].UTMPoint.X / ZoomFactor + ", " + Routes[1].Coords[ticks].UTMPoint.Y / ZoomFactor + ", " + Routes[1].Coords[ticks].pixelPoint.X / ZoomFactor + ", " + Routes[1].Coords[ticks].pixelPoint.Y / ZoomFactor;
                }    
            }
        }

        private void PlayBar_Scroll(object sender, EventArgs e)
        {
            ticks = PlayBar.Value;
        }

        private void TempoUpDown_ValueChanged(object sender, EventArgs e)
        {
            PlayTimer.Interval = (int)(1000 / Math.Pow(Convert.ToDouble(TempoUpDown.Value), 2));
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
            TempoUpDown.Hide();
            LoadButton.Show();
            RunnersCheckBox.Items.Clear();
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Put_Data();
            LoadButton.Hide();
        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            //Coordsreader.Text = e.Location.X * ZoomFactor + ", " + e.Location.Y * ZoomFactor;
        }

        private void Map1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            //Coordsreader.Text = me.Location.X / ZoomFactor + ", " + me.Location.Y / ZoomFactor;
        }

        private void Put_Data()
        {
            DataTable.ColumnCount = 4;
        }

        private void loadTrack_Click(object sender, EventArgs e)
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
                Point[] points = {new Point(Convert.ToInt32(cp.Cord.pixelPoint.X + 30), Convert.ToInt32(cp.Cord.pixelPoint.Y)), 
                                         new Point(Convert.ToInt32(cp.Cord.pixelPoint.X - 15), Convert.ToInt32(cp.Cord.pixelPoint.Y-30)), 
                                         new Point(Convert.ToInt32(cp.Cord.pixelPoint.X - 15), Convert.ToInt32(cp.Cord.pixelPoint.Y+30))};
                if (ControlPoints.First() == cp)
                {
                    g.DrawPolygon(p, points);
                }
                else if (ControlPoints.Last() == cp)
                {
                    g.DrawEllipse(p, cp.Cord.pixelPoint.X - 17, cp.Cord.pixelPoint.Y - 17, 34, 34);
                    g.DrawEllipse(p, cp.Cord.pixelPoint.X - 25, cp.Cord.pixelPoint.Y - 25, 50, 50);
                }
                else
                {
                    g.DrawEllipse(p, cp.Cord.pixelPoint.X - 25, cp.Cord.pixelPoint.Y - 25, 50, 50);
                }


                using (Font myFont = new Font("Arial", 24, FontStyle.Bold))
                {
                    g.DrawString(cp.Number.ToString(), myFont, Brushes.Magenta, new Point(Convert.ToInt32(cp.Cord.pixelPoint.X + 30), Convert.ToInt32(cp.Cord.pixelPoint.Y - 25 / 2)));
                }

                if (ControlPoints.First() != cp)
                {
                    float xDiff = ControlPoints[i - 1].Cord.pixelPoint.X - cp.Cord.pixelPoint.X;
                    float yDiff = ControlPoints[i - 1].Cord.pixelPoint.Y - cp.Cord.pixelPoint.Y;

                    float angle = (float)Math.Atan2(yDiff, xDiff) - (float)(Math.PI);

                    float distance = (float)Math.Sqrt(xDiff * xDiff + yDiff * yDiff);

                    float newX = (float)(ControlPoints[i - 1].Cord.pixelPoint.X + (Math.Cos(angle) * (distance - 25)));
                    float newY = (float)(ControlPoints[i - 1].Cord.pixelPoint.Y + (Math.Sin(angle) * (distance - 25)));

                    g.DrawLine(p, new Point(Convert.ToInt32(ControlPoints[i - 1].Cord.pixelPoint.X + Math.Cos(angle) * 25), Convert.ToInt32(ControlPoints[i - 1].Cord.pixelPoint.Y + Math.Sin(angle) * 25)), new Point(Convert.ToInt32(newX), Convert.ToInt32(newY)));
                }
                i++;
            }
            Map1.Refresh();
            LoadButton.Show();
            loadTrack.Hide();
        }

        private void StartpointUpDown_ValueChanged(object sender, EventArgs e)
        {
            ticks = 0;
            PlayBar.Maximum = Routes.Max(r => r.Coords.Count - r.startingTick[(int)StartpointUpDown.Value]) + 1;
        }
    }
}
