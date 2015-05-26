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
            DataTable.SortCompare += TableCustomSort;
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
                string[] CPPath = Directory.GetFiles(fbd.SelectedPath, "*utm");
                string[] RunnersPath = Directory.GetFiles(fbd.SelectedPath, "*.gpx");
                if (CPPath.Count() > 0 && RunnersPath.Count() > 0)
                {
                    LoadControlPoints(CPPath.First());
                    LoadRunners(RunnersPath);
                    LoadLegs();
                    Put_Data(MainLeg);
                    LoadButton.Hide();
                    ResetButton.Show();
                    player.StartUp();
                }
                else if (CPPath.Count() == 0)
                {
                    MessageBox.Show("Controlpoints not found in selected folder");

                }
                else if (RunnersPath.Count() == 0)
                {
                    MessageBox.Show("GPX Files not found in selected folder");
                }
                
            }  
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

        private void TableCustomSort(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column == DataTable.Columns[0])
            {
                int a = int.Parse(e.CellValue1.ToString()), b = int.Parse(e.CellValue2.ToString());
                e.SortResult = a.CompareTo(b);
            }
            else if (e.Column == DataTable.Columns[4] || e.Column == DataTable.Columns[5])
            {
                Double a = Double.Parse(e.CellValue1.ToString()), b = Double.Parse(e.CellValue2.ToString());
                e.SortResult = a.CompareTo(b);
            }
            else if (e.Column == DataTable.Columns[6] && isLeg)
            {
                int a = int.Parse(e.CellValue1.ToString()), b = int.Parse(e.CellValue2.ToString());
                e.SortResult = a.CompareTo(b);
            }
            else
            {
                e.SortResult = e.CellValue1.ToString().CompareTo(e.CellValue2.ToString());
            }
            

            // If the cell value is already an integer, just cast it instead of parsing

            

            e.Handled = true;
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
            string time = "";
            string pos = "";
            foreach(RunnerData rd in leg.Runners)
            {
                if (rd.time <= new TimeSpan(0))
                {
                    time = "X";
                }
                else
                {
                    time = rd.time.ToString();
                }
                if (rd.pos == -1)
                    pos = "X";
                else
                    pos = rd.pos.ToString();
                if (isLeg)
                {
                    row = new List<string> { pos, rd.name, time, rd.diff.ToString(), Math.Round(rd.distance, 2).ToString(), Math.Round(rd.speed, 2).ToString() };
                    foreach (RunnerData mainRd in MainLeg.Runners)
                    {
                        if (mainRd.name == rd.name)
                        {
                            if (mainRd.pos == -1)
                                row.Add("X");
                            else
                                row.Add(mainRd.pos.ToString());
                        }
                    } 
                }
                else
                {
                    row = new List<string> { pos, rd.name, time, rd.diff.ToString(), Math.Round(rd.distance, 2).ToString(), Math.Round(rd.speed, 2).ToString() };
                    foreach (Leg l in Legs)
                    {
                        for (int runnerIndex = 0; runnerIndex < l.Runners.Count; runnerIndex++)
                        { 
                            if (rd.name == l.Runners[runnerIndex].name)
                            {
                                if (l.Runners[runnerIndex].time <= new TimeSpan(0))
                                    time = "X";
                                else
                                    time = l.Runners[runnerIndex].time.ToString();
                                row.Add(time);
                            }
                        }
                    }
                    
                }
                DataTable.Rows.Add(row.ToArray<string>());
            }
            DataTable.Sort(DataTable.Columns[0], ListSortDirection.Ascending);
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

        private void LoadRunners(string[] GPXFiles)
        {
            int ColorCount = 0;
            Runner runner;
            MainLeg.Name = string.Format("0 - {0}", ControlPoints.Count - 1);
            ControlPointTime cpt = new ControlPointTime();
            bool reached;

            foreach (string file in GPXFiles)
            {
                runner = new Runner();
                runner.ReadGPXData(new FileStream(file, FileMode.Open));
                runner.reachedAll = true;
                runner.RouteColor = Colors[ColorCount % 6];
                ColorCount++;
                reached = true;


                foreach (ControlPoint cp in ControlPoints)
                {
                    cpt = new ControlPointTime();
                    cpt.ControlPointChecker(cp, runner);
                    runner.Visited.Add(cpt);
                    if (cpt.Cord == null)
                    {
                        reached = false;
                    }
                }

                RunnerData runnerdata = new RunnerData();
                runnerdata.name = runner.RunnerName;
                if (reached)
                {
                    runnerdata.distance = Helper.CalcTotalLength(runner, runner.Visited[0].Second,
                        runner.Visited[runner.Visited.Count - 1].Second);
                    runnerdata.time = TimeSpan.FromSeconds(runner.Visited[runner.Visited.Count - 1].Second -
                        runner.Visited[0].Second);
                    runnerdata.speed = Helper.CalcSpeedMinsPrKm(runnerdata.distance, (int)(runnerdata.time.TotalSeconds));
                }
                else
                {
                    runnerdata.distance = 0;
                    runnerdata.time = new TimeSpan(0);
                    runnerdata.speed = 0;
                }

                runnerdata.reached = reached;
                MainLeg.Runners.Add(runnerdata);
                Runners.Add(runner);
            }
            MainLeg.Runners = Helper.GetPosAndDiff(MainLeg.Runners);
        }

        private void LoadControlPoints(string RouteFile)
        {
            ControlPoint newControlPoint;
            int cpNr = 0;
            foreach (var line in File.ReadLines(RouteFile))
            {
                newControlPoint = new ControlPoint();
                newControlPoint.ReadControlPoint(line, cpNr);
                ControlPoints.Add(newControlPoint);
                cpNr++;
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
                    g.DrawString(cp.Number.ToString(), myFont, Brushes.Magenta, new Point(Convert.ToInt32(cp.Cord.pixelPoint.X + 30),
                        Convert.ToInt32(cp.Cord.pixelPoint.Y - 25 / 2)));
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
        }

        private void LoadLegs()
        {
            for (int i = 1; i < ControlPoints.Count; i++)
            {
                Leg leg = new Leg();
                leg.Name = string.Format("{0} - {1}", i - 1, i);
                foreach (Runner r in Runners)
                {
                    RunnerData runnerdata = new RunnerData();
                    if (r.Visited[i].Cord != null)
                    {
                        runnerdata.reached = true;
                        runnerdata.distance = Helper.CalcTotalLength(r, r.Visited[i - 1].Second, r.Visited[i].Second);
                        runnerdata.time = TimeSpan.FromSeconds(r.Visited[i].Second - r.Visited[i - 1].Second);
                        runnerdata.speed = Helper.CalcSpeedMinsPrKm(runnerdata.distance, (int)(runnerdata.time.TotalSeconds));
                    }
                    else
                    {
                        runnerdata.reached = false;
                        runnerdata.distance = 0;
                        runnerdata.time = new TimeSpan(0);
                        runnerdata.speed = 0;
                    }

                    runnerdata.name = r.RunnerName;
                    leg.Runners.Add(runnerdata);
                }
                leg.Runners = Helper.GetPosAndDiff(leg.Runners);
                Legs.Add(leg);
            }
        }
    }
}
