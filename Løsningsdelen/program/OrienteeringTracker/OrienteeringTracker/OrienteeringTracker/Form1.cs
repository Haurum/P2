﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            PlayControl PC = new PlayControl();
        }

        #region Varibles

        public static List<Route> Routes = new List<Route>();
        public static List<PointF[]> RoutesToDraw = new List<PointF[]>();
        private Bitmap OriginalMap;
        private int MousePosX, MousePosY;
        private float ZoomFactor = 1;
        private int TailLenght = 30;
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

        private void PlayButton_Click(object sender, EventArgs e)
        {
            foreach (Route route in Routes)
            {
                RoutesToDraw.Add(new PointF[TailLenght]);
            }
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
            if (ticks < Routes.Max(r => r.Coords.Count))
            {
                for (int routeNum = 0; routeNum < RoutesToDraw.Count; routeNum++ )
                {
                    for (int pointNum = 0; pointNum < TailLenght; pointNum++)
                    {
                        RoutesToDraw[routeNum][pointNum] = Routes[routeNum].Coords[ticks - (TailLenght - pointNum)].p;
                    }
                }
            }
            else
            {
                PlayTimer.Stop();
            }
            ticks++;
            Map1.Refresh();
        }

        private void Map1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush brush;
            Pen pen;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            foreach (PointF[] route in RoutesToDraw)
            {
                brush = new SolidBrush(Color.Blue);
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


    }
}
