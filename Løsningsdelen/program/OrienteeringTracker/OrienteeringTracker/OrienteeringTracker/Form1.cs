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
            player = new Player(this);
            OriginalMap = Map1.Image as Bitmap;
        }

        #region Varibles

        Color[] Colors = { Color.Blue, Color.Red, Color.Black, Color.Purple, Color.Turquoise, Color.Lime };
        public List<Runner> Runners = new List<Runner>();
        public List<ControlPoint> ControlPoints = new List<ControlPoint>();
        List<Leg> Legs = new List<Leg>();
        Leg MainLeg = new Leg();
        Player player;
        private Bitmap OriginalMap;
        private int MousePosX, MousePosY;
        public float ZoomFactor = 1;
        int headers = 6;
        bool isLeg = false;

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

                Runner runner = new Runner();

                foreach (string file in files)
                {
                    runner = Helper.ReadGPXData(new FileStream(file, FileMode.Open));
                    Runners.Add(runner);
                }
                MainLeg.Name = string.Format("0 - {0}", ControlPoints.Count - 1);
                for (int Index = 0; Index < Runners.Count; Index++)
                {
                    Runners[Index].RouteColor = Colors[Index];
                    player.AddRunnerCheckBox(Runners[Index].RunnerName, true);
                    foreach (ControlPoint cp in ControlPoints)
                    {
                        Runners[Index].Visited.Add(Helper.ControlPointChecker(cp, Runners[Index]));
                    }
                    
                    RunnerData runnerdata = new RunnerData();
                    runnerdata.name = Runners[Index].RunnerName;
                    runnerdata.distance = Helper.CalcTotalLength(Runners[Index], Runners[Index].Visited[0].Second, 
                        Runners[Index].Visited[Runners[Index].Visited.Count-1].Second);
                    runnerdata.time = TimeSpan.FromSeconds(Runners[Index].Visited[Runners[Index].Visited.Count - 1].Second - 
                        Runners[Index].Visited[0].Second);
                    runnerdata.speed = Helper.CalcSpeedMinsPrKm(runnerdata.distance, (int)(runnerdata.time.TotalSeconds));
                    MainLeg.Runners.Add(runnerdata);

                }
                MainLeg.Runners = Helper.GetPosAndDiff(MainLeg.Runners);

                for (int i = 1; i < ControlPoints.Count; i++)
                {
                    Leg leg = new Leg();
                    leg.Name = string.Format("{0} - {1}", i - 1, i);
                    foreach (Runner r in Runners)
                    {
                        RunnerData runnerdata = new RunnerData();
                        runnerdata.name = r.RunnerName;
                        runnerdata.distance = Helper.CalcTotalLength(r, r.Visited[i - 1].Second, r.Visited[i].Second);
                        runnerdata.time = TimeSpan.FromSeconds(r.Visited[i].Second - r.Visited[i - 1].Second);
                        runnerdata.speed = Helper.CalcSpeedMinsPrKm(runnerdata.distance, (int)(runnerdata.time.TotalSeconds));
                        leg.Runners.Add(runnerdata);
                    }
                    leg.Runners = Helper.GetPosAndDiff(leg.Runners);
                    Legs.Add(leg);                    
                }

                LoadButton.Hide();
                ResetButton.Show();
                player.StartUp();
            }
            Put_Data(MainLeg);
            
            // Tjekker bare lige hvordan det ser ud
            //Graphics g = Graphics.FromImage(Map1.Image);
            //Pen p = new Pen(Color.Black, 5);
            //foreach (ControlPointTime d in Runners[1].Visited)
            //{
            //    if (d != null)
            //        g.DrawEllipse(p, Runners[1].Coords.ElementAt(d.Tick).pixelPoint.X, Runners[1].Coords.ElementAt(d.Tick).pixelPoint.Y, 10, 10);
            //}
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            player.Play_Pause();
        }

        private void PlayTimer_Tick(object sender, EventArgs e)
        {
            player.Tick();
        }

        private void Map1_Paint(object sender, PaintEventArgs e)
        {
            Map1.Height = (int)(OriginalMap.Height * ZoomFactor);
            Map1.Width = (int)(OriginalMap.Width * ZoomFactor);
            player.Draw(e.Graphics);
        }

        private void PlayBar_Scroll(object sender, EventArgs e)
        {
            player.Second = PlayBar.Value;
        }

        private void TempoUpDown_ValueChanged(object sender, EventArgs e)
        {
            PlayTimer.Interval = (int)(1000 / Math.Pow(Convert.ToDouble(TempoUpDown.Value), 2));
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            Runners.Clear();
            player.Second = 0;
            PlayTimer.Stop();
            Map1.Refresh();
            ResetButton.Hide();
            LoadButton.Show();
            player.ShutDown();           
            RunnersCheckBox.Items.Clear();
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadButton.Hide();
            BackButton.Hide();
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

        private void Setup_Table()
        {
            DataTable.Columns.Clear();
            
            DataTable.ColumnCount = headers + ControlPoints.Count - 1;
            DataTable.Columns[0].HeaderText = "Position";
            DataTable.Columns[1].HeaderText = "Name";
            DataTable.Columns[2].HeaderText = "Time";
            DataTable.Columns[3].HeaderText = "Difference";
            DataTable.Columns[4].HeaderText = "Distance";
            DataTable.Columns[5].HeaderText = "Speed";

            if (isLeg)
            {
                headers++;
                DataTable.ColumnCount = headers;
                DataTable.Columns[6].HeaderText = "Final Position";
            }
            else if (!isLeg)
            {
                for (int legIndex = 0; legIndex < ControlPoints.Count - 1; legIndex++)
                {
                    DataTable.Columns[legIndex + headers].HeaderText = Legs[legIndex].Name;
                }
            }
        }

        private void Put_Data(Leg leg)
        {
            DataTitle.Text = leg.Name;
            Setup_Table();
            DataTable.Rows.Clear();
            List<string> row;
            foreach(RunnerData rd in leg.Runners)
            {
                if (isLeg)
                {
                    row = new List<string> { rd.pos.ToString(), rd.name, rd.time.ToString(), rd.diff.ToString(), rd.distance.ToString(), rd.speed.ToString() };
                    foreach (RunnerData mainRd in MainLeg.Runners)
                    {
                        if (mainRd.name == rd.name)
                        {
                            row.Add(mainRd.pos.ToString());
                        }
                    } 
                }
                else
                {
                    row = new List<string> { rd.pos.ToString(), rd.name, rd.time.ToString(), rd.diff.ToString(), rd.distance.ToString(), rd.speed.ToString() };
                    foreach (Leg l in Legs)
                    {
                        for (int runnerIndex = 0; runnerIndex < l.Runners.Count; runnerIndex++)
                        { 
                            if (rd.name == l.Runners[runnerIndex].name)
                            {
                                row.Add(l.Runners[runnerIndex].time.ToString());
                            }
                        }
                    }
                    
                }
                DataTable.Rows.Add(row.ToArray<string>());
            }
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

                    g.DrawLine(p, new Point(Convert.ToInt32(ControlPoints[i - 1].Cord.pixelPoint.X + Math.Cos(angle) * 25), 
                        Convert.ToInt32(ControlPoints[i - 1].Cord.pixelPoint.Y + Math.Sin(angle) * 25)), 
                        new Point(Convert.ToInt32(newX), Convert.ToInt32(newY)));
                }
                i++;
            }
            Map1.Refresh();
            LoadButton.Show();
            loadTrack.Hide();
        }

        private void StartpointUpDown_ValueChanged(object sender, EventArgs e)
        {
            player.Second = 0;
            player.Update();
        }

        private void DataTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!isLeg)
            {
                for (int i = 0; i < Legs.Count; i++)
                {
                    if (i == e.ColumnIndex - headers)
                    {
                        DataTable.ColumnCount = headers;
                        isLeg = true;
                        Put_Data(Legs[i]);
                        BackButton.Show();
                    }
                }
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            headers--;
            isLeg = false;
            Put_Data(MainLeg);
            BackButton.Hide();
        }
    }
}
