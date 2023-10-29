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
    public partial class AddProperty : Form
    {
        public int RegisterNumber { get; set; }
        public string Description { get; set; }
        public (Coordinates start, Coordinates end) Coordinates { get; set; }

        public AddProperty()
        {
            InitializeComponent();
        }

        private void AddProperty_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegisterNumber = (int)registerNumberNum.Value;
            Description = descriptionText.Text;
            Coordinates = (new Coordinates((double)startX.Value, (double)startY.Value, 0), new Coordinates((double)endX.Value, (double)endY.Value, 0));

            this.DialogResult = DialogResult.OK; // Indicate success
            this.Close(); // Close the form
        }
    }
}
