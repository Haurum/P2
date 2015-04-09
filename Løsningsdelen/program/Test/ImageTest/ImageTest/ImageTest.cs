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

        private void backGround_MouseEnter(object sender, EventArgs e)
        {
            if (backGround.Focused == false)
            {
                backGround.Focus();
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
            if (backGround.Image.Width <= originalBitmap.Width*1.5 && backGround.Image.Height <= originalBitmap.Height*1.5)
            {
                Size newSize = new Size((int)(backGround.Image.Width * 1.25), (int)(backGround.Image.Height * 1.25));
                Bitmap bmp = new Bitmap(originalBitmap, newSize);
                backGround.Image = bmp;
            }

        }

        private void ZoomOut()
        {
            if (backGround.Image.Width >= originalBitmap.Width / 5 && backGround.Image.Height >= originalBitmap.Height / 5)
            {
                Size newSize = new Size((int)(backGround.Image.Width / 1.25), (int)(backGround.Image.Height / 1.25));
                Bitmap bmp = new Bitmap(originalBitmap, newSize);
                backGround.Image = bmp;        
            }

        }
        
    }
}
