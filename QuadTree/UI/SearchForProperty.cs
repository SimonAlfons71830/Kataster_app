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
    public partial class SearchForProperty : Form
    {
        public double LongitudeValue { get; private set; }
        public double LatitudeValue { get; private set; }
        public SearchForProperty()
        {
            InitializeComponent();
        }

        private void SearchForProperty_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LongitudeValue = (double)numericUpDown1.Value;
            LatitudeValue = (double)numericUpDown2.Value;

            this.DialogResult = DialogResult.OK; // Indicate success
            this.Close(); // Close the form
        }
    }
}
