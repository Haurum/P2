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

namespace GPXReader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void LoadGPXButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            Stream GpxStream = null;
            ofd.Filter = "GPX files (*.gpx) |*.gpx";
            DialogResult result = ofd.ShowDialog(); // Show the dialog.
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((GpxStream = ofd.OpenFile()) != null)
                    {
                        GpxReader reader = new GpxReader(GpxStream);
                        MessageBox.Show(reader.Route.RoutePoints.ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
    }
}
