﻿using QuadTree.GeoSystem;
using QuadTree.QTree;
using QuadTree.Structures;
using QuadTree.test;
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
        //List<Property> list = new List<Property>();
        List<Polygon> list = new List<Polygon>();
        Polygon selectedObject;


        (Coordinates start, Coordinates end) coordinatesIS = (new Coordinates(0, 0, 0), new Coordinates(0, 0, 0));

        Pen blkpen = new Pen(Color.FromArgb(255, 0, 155, 0), 1);
        Pen redpen = new Pen(Color.FromArgb(255, 155, 0, 0), 2);
        Pen failedPen = new Pen(Color.FromArgb(255, 155, 0, 0), 5);
        private GeoApp _app;
        QTreeTest _test;

        public App(QTreeTest test, GeoApp app)
        {
            InitializeComponent();
            _app = app;
            _test = test;

        }

        private void App_Load(object sender, EventArgs e)
        {
            //initial seeding
            _app._area._dimension = new QTree.Boundaries(0, 0, 500, 500);
            _app._area._root._boundaries = _app._area._dimension;
            _app._area.MAX_QUAD_CAPACITY = 2;
            _app._area._maxDepth = 10;
            _app._area.Insert(new Property(10000, "New Property", ((new Coordinates(20, 20, 0)), new Coordinates(20, 20, 0)), new List<PlotOfLand>()));

            panelSearchForProp.Hide();
            panelGiveRange.Hide();
            panelAddProp.Hide();
            panelAddPlot.Hide();
            panelDelete.Hide();

            this.QuadPanel.Invalidate();
        }

        /*private void button3_Click(object sender, EventArgs e)
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

        }*/

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
            _app._area.Insert(property);

            //plnenie zoznamu referencii pri pridani nehnutelnosti
            var pomList = _app._area.IntervalSearch(new Boundaries(property.Coordinates.x.Longitude, property.Coordinates.x.Latitude, property.Coordinates.y.Longitude, property.Coordinates.y.Latitude), true);

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

        private void showIntervalSearch(List<Polygon> properties, PaintEventArgs e, (Coordinates start, Coordinates end) rectangleSearch)
        {
            e.Graphics.DrawRectangle(redpen,
                (int)rectangleSearch.start.Longitude,
                (int)rectangleSearch.start.Latitude,
                ((int)(rectangleSearch.end.Longitude - rectangleSearch.start.Longitude)) == 0 ? 1 : ((int)(rectangleSearch.end.Longitude - rectangleSearch.start.Longitude)),
                ((int)(rectangleSearch.end.Latitude - rectangleSearch.start.Latitude)) == 0 ? 1 : (int)(rectangleSearch.end.Latitude - rectangleSearch.start.Latitude));

            foreach (var property in properties)
            {
                if (property is Property)
                {
                    e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, 155, 0, 155), 2), (int)((Property)property).suradnice.x.Longitude, (int)((Property)property).suradnice.x.Latitude, (int)(((Property)property).suradnice.y.Longitude - ((Property)property).suradnice.x.Longitude), (int)(((Property)property).suradnice.y.Latitude - ((Property)property).suradnice.x.Latitude));
                }
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

            this.ClearPanel();
            this.show(this._app._area._root, e);
            this.showIntervalSearch(list, e, coordinatesIS);
            //this.showFailedFind(_test.failedObj, e);

        }

        /*private void button2_Click(object sender, EventArgs e)
        {
            using (SearchForProperty searchForm = new SearchForProperty())
            {
                if (searchForm.ShowDialog() == DialogResult.OK)
                {
                    coordinatesIS = (new Coordinates(searchForm.LongitudeValue, searchForm.LatitudeValue, 0),
                        new Coordinates(searchForm.LongitudeValue, searchForm.LatitudeValue, 0));
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
        }*/

        private void appbutton_Click(object sender, EventArgs e)
        {
            var test = new Test(_test, _app);
            this.Hide();
            test.ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            coordinatesIS = (new Coordinates((double)longitudeNum.Value, (double)latitudeNum.Value, 0), new Coordinates((double)longitudeNum.Value, (double)latitudeNum.Value, 0));
            list = _app.FindOBJInterval(coordinatesIS, true, rbProp.Checked);

            propInfo.Clear();
            foreach (var property in list)
            {
                if (property is Property)
                {
                    // Create a string to format the object's details.
                    string objectDetails = $"PROPERTY\nID: {property._registerNumber}\n" +
                                          $"Coordinates: ({((Property)property).Coordinates.x.Longitude}, {((Property)property).Coordinates.x.Latitude})\n" +
                                          $"Description: {((Property)property).Description}\n\n";

                    // Append the formatted text to the RichTextBox.
                    propInfo.AppendText(objectDetails);
                }
                else
                {
                    // Create a string to format the object's details.
                    string objectDetails = $"PLOT\nID: {property._registerNumber}\n" +
                                          $"Coordinates: ({((PlotOfLand)property).Coordinates.startPos._x}, {((PlotOfLand)property).Coordinates.startPos._y})\n" +
                                          $"                     ({((PlotOfLand)property).Coordinates.endPos._x}, {((PlotOfLand)property).Coordinates.endPos._y})\n" +
                                          $"Description: {((PlotOfLand)property).Description}\n\n";

                    // Append the formatted text to the RichTextBox.
                    propInfo.AppendText(objectDetails);

                }

                this.QuadPanel.Invalidate();
            }

        }



        private void giveRangeButton_Click(object sender, EventArgs e)
        {
            panelSearchForProp.Hide();
            panelAddProp.Hide();
            panelAddPlot.Hide();
            panelDelete.Hide();
            panelGiveRange.Show();
        }

        private void searchRangeButton_Click(object sender, EventArgs e)
        {
            coordinatesIS = (new Coordinates((double)startPosLong.Value, (double)startPosLat.Value, 0), new Coordinates((double)endPosLong.Value, (double)endPosLat.Value, 0));
            list = _app.FindInterval(coordinatesIS);

            objInfo.Clear();
            foreach (var property in list)
            {
                if (property is Property)
                {
                    // Create a string to format the object's details.
                    string objectDetails = $"PROPERTY\nID: {property._registerNumber}\n" +
                                          $"Coordinates: ({((Property)property).Coordinates.x.Longitude}, {((Property)property).Coordinates.x.Latitude})\n" +
                                          $"Description: {((Property)property).Description}\n\n";

                    // Append the formatted text to the RichTextBox.
                    objInfo.AppendText(objectDetails);
                }
                else
                {
                    // Create a string to format the object's details.
                    string objectDetails = $"PLOT\nID: {property._registerNumber}\n" +
                                          $"Coordinates: ({((PlotOfLand)property).Coordinates.startPos._x}, {((PlotOfLand)property).Coordinates.startPos._y})\n" +
                                          $"                     ({((PlotOfLand)property).Coordinates.endPos._x}, {((PlotOfLand)property).Coordinates.endPos._y})\n" +
                                          $"Description: {((PlotOfLand)property).Description}\n\n";

                    // Append the formatted text to the RichTextBox.
                    objInfo.AppendText(objectDetails);

                }

                this.QuadPanel.Invalidate();
            }
        }

        private void searchPropButton_Click(object sender, EventArgs e)
        {
            panelGiveRange.Hide();
            panelAddProp.Hide();
            panelAddPlot.Hide();
            panelDelete.Hide();
            panelSearchForProp.Show();
        }

        private void addBTN_Click(object sender, EventArgs e)
        {
            var registerNMB = int.TryParse(registrationNumber.Text, out int registerNumber);
            if (!registerNMB)
            {
                Random rand = new Random();
                registerNumber = rand.Next(1000, 99999);//random number
            }

            _app.AddProperty(registerNumber, description.Text, (new Coordinates((double)posLong.Value, (double)posLat.Value, 0), new Coordinates((double)posLong.Value, (double)posLat.Value, 0)));
            this.QuadPanel.Invalidate(true);

        }

        private void addPropButton_Click(object sender, EventArgs e)
        {
            panelGiveRange.Hide();
            panelSearchForProp.Hide();
            panelAddPlot.Hide();
            panelDelete.Hide();
            panelAddProp.Show();
        }

        private void PlotAddBtn_Click(object sender, EventArgs e)
        {
            var registerNMB = int.TryParse(regPlotNumber.Text, out int registerNumber);
            if (!registerNMB)
            {
                Random rand = new Random();
                registerNumber = rand.Next(1000, 99999);//random number
            }

            _app.AddPlot(registerNumber, PlotDesc.Text, (new Coordinates((double)startPosPlotLong.Value, (double)startPosPlotLat.Value, 0), new Coordinates((double)endPosPlotLong.Value, (double)endPosPlotLat.Value, 0)));
            this.QuadPanel.Invalidate(true);
        }

        private void addPlotButton_Click(object sender, EventArgs e)
        {
            panelGiveRange.Hide();
            panelSearchForProp.Hide();
            panelAddProp.Hide();
            panelDelete.Hide();
            panelAddPlot.Show();
        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void deletePropButton_Click(object sender, EventArgs e)
        {
            panelSearchForProp.Hide();
            panelGiveRange.Hide();
            panelAddProp.Hide();
            panelAddPlot.Hide();
            //naplnit datagrid objektami
            var list = _app.FindInterval((new Coordinates(_app._area._dimension.X0, _app._area._dimension.Y0, 0),
                new Coordinates(_app._area._dimension.Xk, _app._area._dimension.Yk, 0)));
            datagridOBJ.DataSource = list;

            panelDelete.Show();

        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            // Attach the cell click event handler.
            if (datagridOBJ.SelectedRows.Count > 0)
            {
                // Get the selected row(s) from the DataGridView.
                foreach (DataGridViewRow row in datagridOBJ.SelectedRows)
                {
                    // Access the data from the selected row. Adjust column indexes based on your DataGridView's structure.
                   
                    var id = row.Cells[0].Value.ToString();
                    var x_true = int.TryParse(row.Cells[1].Value.ToString(), out int x);
                    var y_true = int.TryParse(row.Cells[2].Value.ToString(), out int y);

                    
                    // Access other columns as needed.

                    // Now you have the data from the selected row.
                    // You can perform deletion or other actions with this data.
                }
            }

        }

        private void datagridOBJ_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = datagridOBJ.Rows[e.RowIndex];

                // Access the data from the selected row. Adjust column indexes based on your DataGridView's structure.
                var id = selectedRow.Cells[0].Value.ToString();
                var x_true = int.TryParse(selectedRow.Cells[1].Value.ToString(), out int x);
                var y_true = int.TryParse(selectedRow.Cells[2].Value.ToString(), out int y);
                // Access other columns as needed.

                // Now you have access to the data from the clicked row (object).
                // You can perform actions with this data as needed.
                // For example, you can pass it to your quad tree for drawing or other actions.
                //_app.(id, description);
                //DRAW THE CLICKED OBJECT
                list = _app.FindOBJInterval((new Coordinates(x, y, 0), new Coordinates(x, y, 0)), true, true);
                this.QuadPanel.Invalidate();
            }

        }
    }
}
