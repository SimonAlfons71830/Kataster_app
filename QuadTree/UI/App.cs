using QuadTree.GeoSystem;
using QuadTree.QTree;
using QuadTree.Structures;
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
        List<Property> list = new List<Property>();
        (Coordinates start, Coordinates end) coordinatesIS = (new Coordinates(0, 0, 0), new Coordinates(0, 0, 0));

        Pen blkpen = new Pen(Color.FromArgb(255, 0, 155, 0), 1);
        Pen redpen = new Pen(Color.FromArgb(255, 155, 0, 0), 2);
        Pen failedPen = new Pen(Color.FromArgb(255, 155, 0, 0), 5);
        private GeoApp _app;
        public App(GeoApp app)
        {
            InitializeComponent();
            _app = app;
        }

        private void App_Load(object sender, EventArgs e)
        {
            //initial seeding
            _app._area._dimension = new QTree.Boundaries(0, 0, 500, 500);
            _app._area.GetRoot()._boundaries = _app._area._dimension;
            _app._area.MAX_QUAD_CAPACITY = 2;
            _app._area._maxDepth = 10;
            _app._area.InsertUpdate(new Property(10000, "New Property", ((new Coordinates(20, 20, 0)), new Coordinates(20, 20, 0)), new List<PlotOfLand>()));

            this.QuadPanel.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SpatialSearch spatialForm = new SpatialSearch())
            {
                if (spatialForm.ShowDialog() == DialogResult.OK)
                {
                    coordinatesIS = (new Coordinates(spatialForm.StartLongitudeValue, spatialForm.StartLatitudeValue, 0), 
                        new Coordinates(spatialForm.EndLongitudeValue, spatialForm.EndLatitudeValue, 0));
                    
                }
            }

            gpsLabel.Text = "<" + coordinatesIS.start.Longitude + ", " + coordinatesIS.start.Latitude + ">" + "<" + coordinatesIS.end.Longitude + ", " + coordinatesIS.end.Latitude + ">";
     
            list = _app.FindPropertiesInterval(coordinatesIS, true); //zasahuju -> true

            listView1.Clear();
            foreach (var property in list)
            {
                var listViewItem = new ListViewItem(property.RegisterNumber.ToString());
                listViewItem.SubItems.Add(property.Description);
                listViewItem.SubItems.Add(property.Coordinates.x.ToString());
                listViewItem.SubItems.Add(property.Coordinates.y.ToString());
                listView1.Items.Add(listViewItem);
            }

            this.QuadPanel.Invalidate();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int registerNumber = 0;
            string description = "";
            (Coordinates start, Coordinates end) coordinates = (new Coordinates(0, 0, 0), new Coordinates(0, 0, 0));

            using (AddProperty propertyForm = new AddProperty())
            {
                if (propertyForm.ShowDialog() == DialogResult.OK)
                {
                    registerNumber = propertyForm.RegisterNumber;
                    description = propertyForm.Description;
                    coordinates = propertyForm.Coordinates;
                }
            }

            Property property = new Property(registerNumber, description, coordinates, new List<PlotOfLand>());
            _app._area.InsertUpdate(property);

            //plnenie zoznamu referencii pri pridani nehnutelnosti
            var pomList = _app._area.IntervalSearch(new Boundaries(property.Coordinates.x.Longitude,property.Coordinates.x.Latitude,property.Coordinates.y.Longitude, property.Coordinates.y.Latitude),true);

            foreach (var pl in pomList) 
            {
                if (pl is PlotOfLand)
                {
                    property.Lands.Add((PlotOfLand)pl);
                }
            }
            this.QuadPanel.Invalidate();
        }


        private void ClearPanel()
        {
            using (Graphics g = QuadPanel.CreateGraphics())
            {
                g.Clear(QuadPanel.BackColor);
            }
        }


        private void show(Quad quad, PaintEventArgs e)
        {
            if (quad == null)
            {
                return;
            }
            float x0 = (float)quad._boundaries.X0;
            float y0 = (float)quad._boundaries.Y0;
            float xk = (float)quad._boundaries.Xk;
            float yk = (float)quad._boundaries.Yk;

            e.Graphics.DrawRectangle(blkpen, x0, y0, xk - x0, yk - y0);
            this.showPoints(quad._objects, e);
            if (quad.getNE() != null)
            {
                this.show(quad.getNE(), e);
                this.show(quad.getNW(), e);
                this.show(quad.getSE(), e);
                this.show(quad.getSW(), e);
            }

        }

        private void showPoints(List<ISpatialObject> points, PaintEventArgs e)
        {
            foreach (var _object in points)
            {

                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, 0, 0, 155), 1),
                    (float)((Polygon)_object)._borders.startP._x,
                    (float)((Polygon)_object)._borders.startP._y,
                    ((float)((Polygon)_object)._borders.endP._x - (float)((Polygon)_object)._borders.startP._x) == 0 ? 1 : ((float)((Polygon)_object)._borders.endP._x - (float)((Polygon)_object)._borders.startP._x),
                    (((float)((Polygon)_object)._borders.endP._y - (float)((Polygon)_object)._borders.startP._y) == 0) ? 1 : ((float)((Polygon)_object)._borders.endP._y - (float)((Polygon)_object)._borders.startP._y));
                //e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, 155, 0, 155), 2), (float)((Polygon)_object).suradnice.x.Longitude, (float)((Property)_object).suradnice.x.Latitude, (float)((Property)_object).suradnice.y.Longitude - (float)((Property)_object).suradnice.x.Longitude , (float)((Property)_object).suradnice.y.Latitude - (float)((Property)_object).suradnice.x.Latitude);

            }
        }

        private void showIntervalSearch(List<Property> properties, PaintEventArgs e, (Coordinates start, Coordinates end) rectangleSearch)
        {
            e.Graphics.DrawRectangle(redpen,
                (int)rectangleSearch.start.Longitude,
                (int)rectangleSearch.start.Latitude, 
                ((int)(rectangleSearch.end.Longitude - rectangleSearch.start.Longitude)) == 0 ? 1 : ((int)(rectangleSearch.end.Longitude - rectangleSearch.start.Longitude)), 
                ((int)(rectangleSearch.end.Latitude - rectangleSearch.start.Latitude)) == 0 ? 1 : (int)(rectangleSearch.end.Latitude - rectangleSearch.start.Latitude)) ;

            foreach (var property in properties)
            {

                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, 155, 0, 155), 2), (int)property.suradnice.x.Longitude, (int)property.suradnice.x.Latitude, (int)(property.suradnice.y.Longitude - property.suradnice.x.Longitude), (int)(property.suradnice.y.Latitude - property.suradnice.x.Latitude));

            }
        }

        private void showFailedFind(List<ISpatialObject> failedPoints, PaintEventArgs e)
        {

            foreach (var point in failedPoints)
            {
                e.Graphics.DrawRectangle(failedPen, (float)point._x, (float)point._y, 2, 2);
            }
        }

        private void QuadPanel_Paint(object sender, PaintEventArgs e)
        {
            if (checkBoxDrawing.Checked)
            {
                this.ClearPanel();
                this.show(this._app._area.GetRoot(), e);
                this.showIntervalSearch(list, e, coordinatesIS);
                //this.showFailedFind(_test.failedObj, e);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SearchForProperty searchForm = new SearchForProperty())
            {
                if (searchForm.ShowDialog() == DialogResult.OK)
                {
                    coordinatesIS = (new Coordinates(searchForm.LongitudeValue, searchForm.LatitudeValue, 0),
                        new Coordinates(searchForm.LongitudeValue, searchForm.LatitudeValue,0));
                }
            }

            list = _app.FindPropertiesInterval(coordinatesIS, true); //zasahuju -> true

            listView1.Clear();
            foreach (var property in list)
            {
                var listViewItem = new ListViewItem(property.RegisterNumber.ToString());
                listViewItem.SubItems.Add(property.Description);
                listViewItem.SubItems.Add(property.Coordinates.x.ToString());
                listViewItem.SubItems.Add(property.Coordinates.y.ToString());
                listView1.Items.Add(listViewItem);
            }

            this.QuadPanel.Invalidate();
        }
    }
}
