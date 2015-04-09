using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageTest
{
    public partial class ImageTest : Form
    {
        public ImageTest()
        {
            InitializeComponent();
            originalBitmap = backGround.Image as Bitmap;
        }

        private int xPos, yPos;
        private Bitmap originalBitmap;

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
                backGround.Top += (e.Y - yPos);
                backGround.Left += (e.X - xPos);
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            TrackBar tb = sender as TrackBar;
            Size newSize = new Size((int)(originalBitmap.Width * (tb.Value * 0.1)), (int)(originalBitmap.Height * (tb.Value * 0.1)));
            Bitmap bmp = new Bitmap(originalBitmap, newSize);
            backGround.Image = bmp;
            
        }

        
    }
}
