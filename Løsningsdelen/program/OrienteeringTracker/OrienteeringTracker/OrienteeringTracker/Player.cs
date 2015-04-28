using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrienteeringTracker
{
    class Player
    {
        public Player(MainForm mf)
        {
            mainForm = mf;
            TailLenght = 30;
            Second = 0;
        }

        MainForm mainForm { get; set; }
        public int Second { get; set; }
        public int TailLenght { get; set; }

        public void Draw(Graphics g)
        {
            SolidBrush brush;
            Pen pen;
            bool draw = false;
            int tempTailLenght = TailLenght;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            foreach (Runner runner in mainForm.Runners)
            {
                if (!mainForm.RunnersCheckBox.CheckedItems.Contains(runner.RunnerName) || Second < 2)
                {
                    continue;
                }
                brush = new SolidBrush(runner.RouteColor);
                pen = new Pen(brush, 3);
                if (Second < TailLenght)
                {
                    tempTailLenght = Second;
                }
                PointF[] RunnerToDraw = new PointF[tempTailLenght];
                for (int Index = 0; Index < tempTailLenght; Index++)
                {
                    if (Second >= runner.Coords.Count() - runner.Visited[(int)mainForm.StartpointUpDown.Value].Second)
                    {
                        draw = false;
                    }
                    else
                    {
                        RunnerToDraw[Index] = runner.Coords[Second - (tempTailLenght - Index) +
                            runner.Visited[(int)mainForm.StartpointUpDown.Value].Second].pixelPoint;
                        RunnerToDraw[Index].X *= mainForm.ZoomFactor;
                        RunnerToDraw[Index].Y *= mainForm.ZoomFactor;
                        draw = true;
                    }

                }
                if (draw)
                {
                    g.DrawLines(pen, RunnerToDraw);
                    g.FillEllipse(brush, RunnerToDraw[RunnerToDraw.Length - 1].X - 4,
                        RunnerToDraw[RunnerToDraw.Length - 1].Y - 4, 8, 8);
                    g.DrawString(runner.RunnerName, new Font("Arial", 14, FontStyle.Bold), new SolidBrush(Color.Black),
                        RunnerToDraw[RunnerToDraw.Length - 1].X + 5, RunnerToDraw[RunnerToDraw.Length - 1].Y + 5);
                }
            }
        }

        public void Tick()
        {
            if (Second >= mainForm.Runners.Max(r => r.Coords.Count - r.Visited[(int)mainForm.StartpointUpDown.Value].Second))
            {
                mainForm.PlayTimer.Stop();
            }
            mainForm.PlayBar.Value = Second;
            mainForm.Map1.Refresh();
            Second++;
        }

        public void Play_Pause()
        {
            if (mainForm.PlayTimer.Enabled)
            {
                mainForm.PlayTimer.Stop();
            }
            else
            {
                mainForm.PlayTimer.Start();
            }
        }

        public void Update()
        {
            mainForm.PlayBar.Maximum = mainForm.Runners.Max(r => r.Coords.Count - 
                r.Visited[(int)mainForm.StartpointUpDown.Value].Second);
        }

        public void AddRunnerCheckBox(string name, bool isChecked)
        {
            mainForm.RunnersCheckBox.Items.Add(name);
            mainForm.RunnersCheckBox.SetItemChecked(mainForm.RunnersCheckBox.Items.Count - 1, isChecked);
        }

        public void StartUp()
        {
            mainForm.PlayButton.Show();
            mainForm.PlayBar.Show();
            mainForm.TempoUpDown.Show();
            mainForm.RunnersCheckBox.Show();
            mainForm.tempoLabel.Show();
            mainForm.StartpointLabel.Show();
            mainForm.StartpointUpDown.Show();

            mainForm.RunnersCheckBox.ClientSize = new Size(mainForm.RunnersCheckBox.ClientSize.Width,
                    mainForm.RunnersCheckBox.GetItemRectangle(0).Height * mainForm.RunnersCheckBox.Items.Count);
            mainForm.RunnersCheckBox.Top = mainForm.PlayButton.Top - 10 - mainForm.RunnersCheckBox.GetItemRectangle(0).Height *
                (mainForm.RunnersCheckBox.Items.Count);

            mainForm.StartpointUpDown.Maximum = mainForm.ControlPoints.Count - 1;

            this.Update();
        }

        public void ShutDown()
        {
            mainForm.PlayButton.Hide();
            mainForm.PlayBar.Hide();
            mainForm.TempoUpDown.Hide();
            mainForm.RunnersCheckBox.Hide();
            mainForm.tempoLabel.Hide();
            mainForm.StartpointLabel.Hide();
            mainForm.StartpointUpDown.Hide();
        }
    }
}
