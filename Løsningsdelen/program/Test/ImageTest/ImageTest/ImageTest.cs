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

namespace ImageTest
{
    public partial class ImageTestForm : Form
    {
        public ImageTestForm()
        {
            InitializeComponent();
            originalBitmap = background.Image as Bitmap;
            pen = new Pen(brush, 3);
        }
        List<OPoint> pointFs = new List<OPoint>();
        List<PointF> rout = new List<PointF>();
        Pen pen;
        SolidBrush brush = new SolidBrush(Color.Blue);
        float zoomFactor = 1;

        private void LoadGPXButton_Click(object sender, EventArgs e)
        {

            double UTM_easting, UTM_northing;
            float pixelX, pixelY;

            OpenFileDialog ofd = new OpenFileDialog();
            Stream GpxStream = null;
            ofd.Filter = "GPX files (*.gpx) |*.gpx";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    GpxStream = ofd.OpenFile();
                    if (GpxStream != null)
                    {
                        GpxReader reader = new GpxReader(GpxStream);
                        reader.Read();
                        foreach (GpxTrackPoint gt in reader.Track.Segments[0].TrackPoints)
                        {
                            //CoordinatesLabel.Text += String.Format("{0} - {1}\n", gt.Longitude, gt.Latitude);

                            Program.ConvertLatLongToUTM(gt.Longitude, gt.Latitude, out UTM_northing, out UTM_easting);

                            Program.ConvertUTMToPixel(UTM_northing, UTM_easting, out pixelX, out pixelY);

                            pointFs.Add(new OPoint(pixelX, pixelY, gt.Time));
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
            int i = 0;
            while (i+1 < pointFs.Count)
            {
                DateTime temp = pointFs[i].time;
                temp = temp.AddSeconds(1);
                if (pointFs[i].time.Equals(pointFs[i+1].time))
                {
                    pointFs.RemoveAt(i + 1);
                    i--;
                }
                else if (!temp.Equals(pointFs[i+1].time))
                {
                    pointFs.Insert(i + 1, new OPoint(pointFs[i].point, temp));
                }
                i++;
            }
        }

        private int xPos, yPos;
        public Bitmap originalBitmap;

        private void backGround_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                xPos = e.X;
                yPos = e.Y;
            }
        }

        private void backGround_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                background.Top += (e.Y - yPos);
                background.Left += (e.X - xPos);
            }
        }

        private void backGround_MouseEnter(object sender, EventArgs e)
        {
            if (background.Focused == false)
            {
                background.Focus();
            }
        }

        private void backGround_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                ZoomIn();
            }
            else
            {
                ZoomOut();
            }
        }

        private void ZoomIn()
        {
            if (background.Width <= originalBitmap.Width * 1.5 && background.Height <= originalBitmap.Height * 1.5)
            {
                zoomFactor *= 1.25f;
                background.Height = (int)(originalBitmap.Height * zoomFactor);
                background.Width = (int)(originalBitmap.Width * zoomFactor);
                background.Refresh();
            }
        }

        private void ZoomOut()
        {
            if (background.Width >= originalBitmap.Width / 5 && background.Height >= originalBitmap.Height / 5)
            {
                zoomFactor /= 1.25f;
                background.Height = (int)(originalBitmap.Height * zoomFactor);
                background.Width = (int)(originalBitmap.Width * zoomFactor);
                background.Refresh();
            }
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            if (lineDrawTimer.Enabled)
            {
                lineDrawTimer.Stop();
            }
            else
            {
                lineDrawTimer.Start();
            }
            
        }
        int j = 0;
        private void lineDrawTimer_Tick(object sender, EventArgs e)
        {
            if (j < pointFs.Count)
	        {
                rout.Add(pointFs[j].point);
                if (j >= 30)
                {

                    rout.RemoveAt(0);
                }
            }
            else
            {
                lineDrawTimer.Stop();
            }

            
            j++;
            background.Refresh();
        }

        private void background_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            PointF[] points = new PointF[30];
            int i = 0;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (rout.Count >= 30)
            {
                foreach (PointF routPoint in rout)
                {
                    points[i].X = routPoint.X * zoomFactor;
                    points[i].Y = routPoint.Y * zoomFactor;
                    i++;
                }
                g.DrawLines(pen, points);
                g.FillEllipse(brush, points[points.Length-1].X - 4, points[points.Length-1].Y - 4, 8, 8);
            }
        }
    }
}
