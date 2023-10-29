using QuadTree.GeoSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuadTree.UI
{
    public partial class SpatialSearch : Form
    {
        public double StartLongitudeValue { get; private set; }
        public double StartLatitudeValue { get; private set; }
        public double EndLongitudeValue { get; private set; }
        public double EndLatitudeValue { get; private set; }

        public SpatialSearch()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void SpatialSearch_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartLongitudeValue = (double)StartLongitude.Value;
            StartLatitudeValue = (double)StartLatitude.Value;
            EndLongitudeValue = (double)EndLongitude.Value;
            EndLatitudeValue = (double)EndLatitude.Value;

            this.DialogResult = DialogResult.OK; // Indicate success
            this.Close(); // Close the form
        }
    }
}
