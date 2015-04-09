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
        }
        List<OPoint> pointFs = new List<OPoint>();

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
                if (!temp.Equals(pointFs[i+1].time))
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
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
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
                background.Height = (int)(background.Height + (background.Height * 0.05));
                background.Width = (int)(background.Width + (background.Width * 0.05));
                background.Refresh();
            }
        }

        private void ZoomOut()
        {
            if (background.Width >= originalBitmap.Width / 5 && background.Height >= originalBitmap.Height / 5)
            {
                background.Height = (int)(background.Height - (background.Height * 0.05));
                background.Width = (int)(background.Width - (background.Width * 0.05));
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
            Graphics g = Graphics.FromImage(background.Image);
            Pen pen = new Pen(Color.Blue, 3);
            if (j+1 < pointFs.Count)
	        {
                g.DrawLine(pen, pointFs[j].point, pointFs[j + 1].point);
            }
            else
            {
                lineDrawTimer.Stop();
            }
            j++;
            background.Refresh();
        }
    }
}
