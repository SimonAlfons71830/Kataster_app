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
    public partial class App : Form
    {
        private GeoApp _app;
        public App(GeoApp app)
        {
            InitializeComponent();
            _app = app;
        }

        private void App_Load(object sender, EventArgs e)
        {
            _app._area._dimension = new QTree.Boundaries(0, 0, 500, 500);
            _app._area.GetRoot()._boundaries = _app._area._dimension;
            _app._area.MAX_QUAD_CAPACITY = 2;
            _app._area._maxDepth = 10;
            _app._area.InsertUpdate(new Property(10000, "New Property", ((new Coordinates(20, 20)), new Coordinates(20, 20)), new List<PlotOfLand>()));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double startLongitude = 0;
            double startLatitude = 0;
            double endLongitude = 0;
            double endLatitude = 0;
            using (SpatialSearch spatialForm = new SpatialSearch())
            {
                if (spatialForm.ShowDialog() == DialogResult.OK)
                {
                    // Access the coordinates from the SpatialSearch form
                    startLongitude = spatialForm.StartLongitudeValue;
                    startLatitude = spatialForm.StartLatitudeValue;
                    endLongitude = spatialForm.EndLongitudeValue;
                    endLatitude = spatialForm.EndLatitudeValue;

                    // Do something with the coordinates in the App form
                }
            }

            gpsLabel.Text = "<" + startLongitude + ", " + startLatitude + ">" + "<" + endLongitude + ", " + endLatitude + ">";
            //(Coordinates start, Coordinates end) intervalSearch = (new Coordinates(startLongitude, startLatitude), (new Coordinates(endLongitude, endLatitude)));
            var list = _app.FindPropertiesInterval((new Coordinates(startLongitude, startLatitude), (new Coordinates(endLongitude, endLatitude))), false);

            foreach (var property in list)
            {
                var listViewItem = new ListViewItem(property.RegisterNumber.ToString());
                listViewItem.SubItems.Add(property.Description);
                listViewItem.SubItems.Add(property.Coordinates.x.ToString());
                listViewItem.SubItems.Add(property.Coordinates.y.ToString());
                listView1.Items.Add(listViewItem);
            }

        }
    }
}
