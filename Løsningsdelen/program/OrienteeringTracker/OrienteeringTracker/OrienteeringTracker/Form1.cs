using System;
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
        }

        #region Varibles

        private Bitmap OriginalMap;
        private int MousePosX, MousePosY;
        private float ZoomFactor = 1;

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
    }
}
