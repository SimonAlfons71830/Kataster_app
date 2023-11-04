using QuadTree.GeoSystem;
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
    public partial class resetBtn : Form
    {
        //List<Property> list = new List<Property>();
        List<Polygon> list = new List<Polygon>();
        Polygon selectedObject;

        private System.Data.DataTable dataObjToRemove = new System.Data.DataTable();

        public bool wasSeeded = false;
        public int max_quad_cap;
        public int max_depth;


        //for editing
        private int originalPROPRegisterNumber;
        private double originalPROPXCoordinate;
        private double originalPROPYCoordinate;
        private string originalPROPDescription;
        private Property originalProp;
        DataGridViewRow selectedRowProp;

        private int originalPLOTRegisterNumber;
        private double originalPLOTXCoordinateStart;
        private double originalPLOTYCoordinateStart;
        private double originalPLOTXCoordinateEnd;
        private double originalPLOTYCoordinateEnd;
        private string originalPLOTDescription;
        private PlotOfLand originalPlot;
        DataGridViewRow selectedRowPlot;


        (Coordinates start, Coordinates end) coordinatesIS = (new Coordinates(0, 0, 0), new Coordinates(0, 0, 0));

        Pen blkpen = new Pen(Color.FromArgb(255, 0, 155, 0), 1);
        Pen redpen = new Pen(Color.FromArgb(255, 155, 0, 0), 2);
        Pen failedPen = new Pen(Color.FromArgb(255, 155, 0, 0), 5);
        private GeoApp _app;
        QTreeTest _test;

        public resetBtn(QTreeTest test, GeoApp app)
        {
            InitializeComponent();
            _app = app;
            _test = test;

        }

        private void App_Load(object sender, EventArgs e)
        {
            //initial seeding
            max_quad_cap = 2;
            max_depth = 10;

            _app.seedApp(500, 500, 20, 10, max_quad_cap, max_depth);
            if (_app._area.wasOptimalized)
            {
                this.improveLBL.Text = _app._area.improvement.ToString();
            }
            else
            {
                this.improveLBL.Text = "NOT\nYET";
            }

            /* _app._area._dimension = new QTree.Boundaries(0, 0, 500, 500);
             _app._area._root._boundaries = _app._area._dimension;
             _app._area.MAX_QUAD_CAPACITY = 2;
             _app._area.Insert(new Property(10000, "New Property", ((new Coordinates(20, 20, 0)), new Coordinates(20, 20, 0)), new List<PlotOfLand>()));
 */
            panelSearchForProp.Hide();
            panelGiveRange.Hide();
            panelAddProp.Hide();
            panelAddPlot.Hide();
            panelEdit.Hide();
            panelDelete.Hide();
            panelPlot.Hide();
            panelDepth.Hide();
            panelProp.Hide();
            panelSeedApp.Hide();
            panelSettings.Hide();

            dataObjToRemove.Columns.Add("Reg.Number", typeof(int));
            dataObjToRemove.Columns.Add("S_x", typeof(int));
            dataObjToRemove.Columns.Add("S_y", typeof(int));
            dataObjToRemove.Columns.Add("E_x", typeof(int));
            dataObjToRemove.Columns.Add("E_y", typeof(int));
            dataObjToRemove.Columns.Add("Type");



            //naplnit datagrid objektami
            var list = _app.FindInterval((new Coordinates(_app._area._dimension.X0, _app._area._dimension.Y0, 0),
                new Coordinates(_app._area._dimension.Xk, _app._area._dimension.Yk, 0)));

            this.redoGrids(list);

            healthLBL.Text = this._app._area.TreeHealth.Value.ToString();

            this.QuadPanel.Invalidate();
        }

        private void redoGrids(List<Polygon> list)
        {
            dataObjToRemove.Rows.Clear();
            foreach (var item in list)
            {
                DataRow row = dataObjToRemove.NewRow();
                row[0] = item._registerNumber;
                if (item is Property)
                {
                    row[1] = ((Property)item).Coordinates.x._x;
                    row[2] = ((Property)item).Coordinates.x._y;
                    row[3] = ((Property)item).Coordinates.y._x;
                    row[4] = ((Property)item).Coordinates.y._y;
                    //QuadTree.GeoSystem.Property
                    row[5] = ((Property)item).GetType().ToString().Substring(19, 8);
                }
                else
                {
                    row[1] = ((PlotOfLand)item).Coordinates.startPos._x;
                    row[2] = ((PlotOfLand)item).Coordinates.startPos._y;
                    row[3] = ((PlotOfLand)item).Coordinates.endPos._x;
                    row[4] = ((PlotOfLand)item).Coordinates.endPos._y;
                    row[5] = item.GetType().ToString().Substring(19, 10);
                }
                dataObjToRemove.Rows.Add(row);
            }

            dataGridObj.DataSource = dataObjToRemove;
            dataGridObj2.DataSource = dataObjToRemove;
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
            panelEdit.Hide();
            panelPlot.Hide();
            panelProp.Hide();
            panelSeedApp.Hide();
            panelDepth.Hide();
            panelSettings.Hide();
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
            panelEdit.Hide();
            panelPlot.Hide();
            panelProp.Hide();
            panelSettings.Hide();
            panelDepth.Hide();
            panelSeedApp.Hide();
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
            healthLBL.Text = this._app._area.TreeHealth.Value.ToString();
            this.QuadPanel.Invalidate(true);

        }

        private void addPropButton_Click(object sender, EventArgs e)
        {
            panelGiveRange.Hide();
            panelSearchForProp.Hide();
            panelAddPlot.Hide();
            panelDelete.Hide();
            panelEdit.Hide();
            panelPlot.Hide();
            panelProp.Hide();
            panelSeedApp.Hide();
            panelSettings.Hide();
            panelDepth.Hide();
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
            healthLBL.Text = this._app._area.TreeHealth.Value.ToString();
            this.QuadPanel.Invalidate(true);
        }

        private void addPlotButton_Click(object sender, EventArgs e)
        {
            panelGiveRange.Hide();
            panelSearchForProp.Hide();
            panelAddProp.Hide();
            panelDelete.Hide();
            panelEdit.Hide();
            panelPlot.Hide();
            panelProp.Hide();
            panelSeedApp.Hide();
            panelSettings.Hide();
            panelDepth.Hide();
            panelAddPlot.Show();
        }

        private void deletePropButton_Click(object sender, EventArgs e)
        {
            panelSearchForProp.Hide();
            panelGiveRange.Hide();
            panelAddProp.Hide();
            panelAddPlot.Hide();
            panelEdit.Hide();
            panelPlot.Hide();
            panelSeedApp.Hide();
            panelProp.Hide();
            panelSettings.Hide();
            panelDepth.Hide();
            panelDelete.Show();

        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            // Get the first selected row (if you allow multiple selections, you may iterate through them).
            DataGridViewRow selectedRow = dataGridObj.SelectedRows[0];

            // Access data from specific cells within the selected row.
            var regNumb = selectedRow.Cells[0].Value;
            var startPosX = selectedRow.Cells[1].Value;
            var startPosY = selectedRow.Cells[2].Value;
            var endPosX = selectedRow.Cells[3].Value;
            var endPosY = selectedRow.Cells[4].Value;
            var typeOfObj = selectedRow.Cells[5].Value.ToString();


            if (typeOfObj.Equals("Property"))
            {
                if (_app.RemoveObj(new Property((int)regNumb, "", (new Coordinates((int)startPosX, (int)startPosY, 0), new Coordinates((int)endPosX, (int)endPosY, 0)), null)))
                {
                    //remove from grid
                    dataGridObj.Rows.Remove(selectedRow);
                    healthLBL.Text = this._app._area.TreeHealth.Value.ToString();
                    //dataGridObj2.Rows.Remove(selectedRow);
                }

            }
            else
            {
                if (_app.RemoveObj(new PlotOfLand((int)regNumb, "", (new Coordinates((int)startPosX, (int)startPosY, 0), new Coordinates((int)endPosX, (int)endPosY, 0)), null)))
                {
                    //removefrom grid
                    dataGridObj.Rows.Remove(selectedRow);
                    healthLBL.Text = this._app._area.TreeHealth.Value.ToString();
                    //dataGridObj2.Rows.Remove(selectedRow);
                }
            }

            this.QuadPanel.Invalidate();

        }

        private void showBtn_Click(object sender, EventArgs e)
        {
            var numberInGrid = dataGridObj.SelectedCells.Count;
            // Attach the cell click event handler.
            if (dataGridObj.SelectedCells.Count > 0) //at least 1 cell is selected
            {
                //which row is selected
                //get the obj in that row
                var obj = dataGridObj.SelectedRows[0];

                int regNum = (int)obj.Cells[0].Value; //id
                int startX = (int)obj.Cells[1].Value; //startPos x
                int startY = (int)obj.Cells[2].Value;
                int endX = (int)obj.Cells[3].Value;
                int endY = (int)obj.Cells[4].Value;

                if (obj.Cells[5].Value.Equals("Property"))
                {
                    //showToRemove(sender, (PaintEventArgs)e, startX, startY, endX, endY);
                }
            }
        }

        private void showToRemove(object sender, PaintEventArgs e, int x0, int y0, int xk, int yk)
        {
            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, 155, 0, 0), 2), x0, y0, xk - x0, yk - y0);
        }

        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {

        }

        private void seedBtn_Click(object sender, EventArgs e)
        {
            wasSeeded = true;
            _app.seedApp((int)widthOfTree.Value, (int)LengthOfTree.Value, (int)PropNo.Value, (int)plotNo.Value, max_quad_cap, max_depth);
            healthLBL.Text = this._app._area.TreeHealth.Value.ToString();
            if (_app._area.wasOptimalized)
            {
                this.improveLBL.Text = _app._area.improvement.ToString();
            }
            else
            {
                this.improveLBL.Text = "NOT\nYET";
            }
            this.QuadPanel.Invalidate();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            panelSearchForProp.Hide();
            panelGiveRange.Hide();
            panelAddProp.Hide();
            panelAddPlot.Hide();
            panelDelete.Hide();
            panelPlot.Hide();
            panelProp.Hide();
            panelSettings.Hide();
            panelSeedApp.Hide();
            panelDepth.Hide();
            panelEdit.Show();

        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            // Get the first selected row (if you allow multiple selections, you may iterate through them).
            DataGridViewRow selectedRow = dataGridObj2.SelectedRows[0];
            selectedRowProp = selectedRow;

            // Access data from specific cells within the selected row.
            var regNumb = selectedRow.Cells[0].Value;
            var startPosX = selectedRow.Cells[1].Value;
            var startPosY = selectedRow.Cells[2].Value;
            var endPosX = selectedRow.Cells[3].Value;
            var endPosY = selectedRow.Cells[4].Value;
            var typeOfObj = selectedRow.Cells[5].Value.ToString();



            if (typeOfObj.Equals("Property"))
            {
                //new panel
                panelPlot.Hide();
                panelProp.Show();

                originalProp = (Property)_app.PickToEdit(new Property((int)regNumb, "", (new Coordinates((int)startPosX, (int)startPosY, 0), new Coordinates((int)endPosX, (int)endPosY, 0)), null));

                if (originalProp != null)
                {
                    rnPropEdit.Text = originalProp._registerNumber.ToString();
                    XcoordProp.Value = (decimal)originalProp._x;
                    YCoordProp.Value = (decimal)originalProp._y;
                    descBoxEditProp.Text = (originalProp)._description.ToString();

                    originalPROPDescription = descBoxEditProp.Text;
                    originalPROPRegisterNumber = originalProp._registerNumber;
                    originalPROPXCoordinate = originalProp._x;
                    originalPROPYCoordinate = originalProp._y;

                }

            }
            else
            {
                //new panel
                panelProp.Hide();
                panelPlot.Show();

                originalPlot = (PlotOfLand)_app.PickToEdit(new PlotOfLand((int)regNumb, "", (new Coordinates((int)startPosX, (int)startPosY, 0), new Coordinates((int)endPosX, (int)endPosY, 0)), null));

                if (originalPlot != null)
                {
                    rnPlotEdit.Text = originalPlot._registerNumber.ToString();
                    startPosEditPlotX.Value = (decimal)originalPlot._coordinates.startPos._x;
                    startPosEditPlotY.Value = (decimal)originalPlot._coordinates.startPos._y;
                    endPosEditPlotX.Value = (decimal)originalPlot._coordinates.endPos._x;
                    endPosEditPlotY.Value = (decimal)originalPlot._coordinates.endPos._y;
                    descEditPlot.Text = originalPlot._description.ToString();

                    originalPLOTRegisterNumber = originalPlot._registerNumber;
                    originalPLOTXCoordinateStart = (double)startPosEditPlotX.Value;
                    originalPLOTYCoordinateStart = (double)startPosEditPlotY.Value;
                    originalPLOTXCoordinateEnd = (double)endPosEditPlotX.Value;
                    originalPLOTYCoordinateEnd = (double)endPosEditPlotY.Value;
                    originalPLOTDescription = descEditPlot.Text;


                }
            }
        }

        private void editbtnPlot_Click(object sender, EventArgs e)
        {
            bool keyAttrChanged =
                (decimal)originalPLOTXCoordinateStart != startPosEditPlotX.Value ||
                (decimal)originalPLOTYCoordinateStart != startPosEditPlotY.Value ||
                (decimal)originalPLOTXCoordinateEnd != endPosEditPlotX.Value ||
                (decimal)originalPLOTYCoordinateEnd != endPosEditPlotY.Value;

            var boolpom = int.TryParse(rnPlotEdit.Text, out int rn);

            bool attrChanged = originalPLOTRegisterNumber != rn ||
                originalPLOTDescription != descEditPlot.Text;

            if (!keyAttrChanged && attrChanged)
            {
                originalPlot._description = descEditPlot.Text;
                originalPlot._registerNumber = rn;
                selectedRowProp.Cells[0].Value = rn;

                dataGridObj.Refresh();
            }
            else if (keyAttrChanged)
            {
                if (_app.RemoveObj(new PlotOfLand(originalPlot.RegisterNumber, originalPlot.Description, originalPlot.Coordinates, null)))
                {
                    dataGridObj2.Rows.Remove(selectedRowProp);
                    _app.AddPlot(rn, descEditPlot.Text, (new Coordinates((double)startPosEditPlotX.Value, (double)startPosEditPlotY.Value, 0), new Coordinates((double)endPosEditPlotX.Value, (double)endPosEditPlotY.Value, 0)));
                    DataRow newRow = dataObjToRemove.NewRow();
                    newRow[0] = rn;
                    newRow[1] = startPosEditPlotX.Value;
                    newRow[2] = startPosEditPlotY.Value;
                    newRow[3] = endPosEditPlotX.Value;
                    newRow[4] = endPosEditPlotY.Value;
                    newRow[5] = "PlotOfLand";
                    dataObjToRemove.Rows.Add(newRow);
                    healthLBL.Text = this._app._area.TreeHealth.Value.ToString();
                    dataGridObj2.Refresh();
                }
            }
        }

        private void editBTNProp_Click(object sender, EventArgs e)
        {
            bool keyAttrChanged = (decimal)originalPROPXCoordinate != XcoordProp.Value ||
                (decimal)originalPROPYCoordinate != YCoordProp.Value;


            var boolpom = int.TryParse(rnPropEdit.Text, out int rn);

            bool attrChanged = originalPROPRegisterNumber != rn ||
                originalPROPDescription != descBoxEditProp.Text;

            if (!keyAttrChanged && attrChanged)
            {
                originalProp._description = descBoxEditProp.Text;
                originalProp._registerNumber = rn;
                selectedRowProp.Cells[0].Value = rn;
                dataGridObj.Refresh();
            }
            else if (keyAttrChanged)
            {
                if (_app.RemoveObj(new Property(originalProp.RegisterNumber, originalProp.Description, originalProp.suradnice, null)))
                {
                    dataGridObj2.Rows.Remove(selectedRowProp);
                    _app.AddProperty(rn, descBoxEditProp.Text, (new Coordinates((double)XcoordProp.Value, (double)YCoordProp.Value, 0), new Coordinates((double)XcoordProp.Value, (double)YCoordProp.Value, 0)));
                    DataRow newRow = dataObjToRemove.NewRow();
                    newRow[0] = rn;
                    newRow[1] = XcoordProp.Value;
                    newRow[2] = YCoordProp.Value;
                    newRow[3] = XcoordProp.Value;
                    newRow[4] = YCoordProp.Value;
                    newRow[5] = "Property";
                    dataObjToRemove.Rows.Add(newRow);
                    healthLBL.Text = this._app._area.TreeHealth.Value.ToString();

                    dataGridObj2.Refresh();
                }
            }
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            panelSearchForProp.Hide();
            panelGiveRange.Hide();
            panelAddProp.Hide();
            panelAddPlot.Hide();
            panelDelete.Hide();
            panelPlot.Hide();
            panelProp.Hide();
            panelSeedApp.Hide();
            panelEdit.Hide();
            panelDepth.Hide();
            panelSettings.Show();
        }

        private void exportBtn_Click(object sender, EventArgs e)
        {
            panelDepth.Hide();
            panelSeedApp.Hide();
            this._app.WriteToFiles();
            MessageBox.Show("Export Finished.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelDepth.Hide();
            panelSeedApp.Hide();

            MessageBox.Show("Choose a Properties.txt");
            String filepath = String.Empty;
            String fileExt = string.Empty;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                filepath = openFileDialog.FileName;
                fileExt = Path.GetExtension(filepath);

                this._app.ReadProperties(filepath);
                healthLBL.Text = this._app._area.TreeHealth.Value.ToString();
                MessageBox.Show("Import of Properties is completed.");

            }
            MessageBox.Show("Choose a Plots.txt");
            String filepathPoi = String.Empty;
            String fileExtPoi = string.Empty;
            OpenFileDialog openFileD = new OpenFileDialog();
            if (openFileD.ShowDialog() == DialogResult.OK)
            {
                filepathPoi = openFileD.FileName;
                fileExt = Path.GetExtension(filepathPoi);

                this._app.ReadPlots(filepathPoi);
                healthLBL.Text = this._app._area.TreeHealth.Value.ToString();
                MessageBox.Show("Import of Plots is completed.");
            }

            this.QuadPanel.Invalidate();
        }

        private void resetAppBtn_Click(object sender, EventArgs e)
        {
            panelDepth.Hide();
            panelSeedApp.Hide();
            this._app._area.ResetTree(this._app._area._root);
            this.QuadPanel.Invalidate();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            panelDepth.Hide();
            panelSeedApp.Show();
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void seedBtn2_Click(object sender, EventArgs e)
        {
            wasSeeded = true;
            _app.seedApp((int)widthOfTree.Value, (int)LengthOfTree.Value, (int)PropNo.Value, (int)plotNo.Value, (int)CountNo.Value, (int)DepthNo.Value);
            this.redoGrids(_app.FindInterval((new Coordinates(_app._area._dimension.X0, _app._area._dimension.Y0, 0),
                new Coordinates(_app._area._dimension.Xk, _app._area._dimension.Yk, 0))));
            panelSeedApp.Hide();
            healthLBL.Text = this._app._area.TreeHealth.Value.ToString();
            if (_app._area.wasOptimalized)
            {
                this.improveLBL.Text = _app._area.improvement.ToString();
            }
            else
            {
                this.improveLBL.Text = "NOT\nYET";
            }



            this.QuadPanel.Invalidate();
        }

        private void changeDepthBtn_Click(object sender, EventArgs e)
        {

            panelSeedApp.Hide();
            panelDepth.Show();
            depthLabel.Text = this._app._area.maxDepth.ToString();
        }

        private void chngDepthBtn_Click(object sender, EventArgs e)
        {

            this._app.ChangeDepth((int)newDepthNum.Value);
            this.redoGrids(_app.FindInterval((new Coordinates(_app._area._dimension.X0, _app._area._dimension.Y0, 0),
                new Coordinates(_app._area._dimension.Xk, _app._area._dimension.Yk, 0))));
            panelDepth.Hide();
            this.QuadPanel.Invalidate();
        }
    }
}
