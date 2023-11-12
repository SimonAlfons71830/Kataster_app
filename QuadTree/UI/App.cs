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
    public partial class App : Form
    {
        List<Polygon> list = new List<Polygon>();

        private System.Data.DataTable dataWithRangeSearch = new System.Data.DataTable();

        public bool wasSeeded = false;
        public int max_quad_cap;
        public int max_depth;

        //for editing
        private int originalPROPRegisterNumber;
        private double originalPROPXCoordinateStart;
        private double originalPROPYCoordinateStart;
        private double originalPROPXCoordinateEnd;
        private double originalPROPYCoordinateEnd;
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

        //DRAWING
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
            //initial seeding u can change here
            max_quad_cap = 2;
            max_depth = 10;
            int num_prop = 100;
            int num_plot = 100;

            _app.setOptimalization(checkBoxWantOpt.Checked);
            _app.seedApp(0, 0, 500, 500, num_prop, num_plot, max_quad_cap, max_depth);

            this.HealtPanelUpdate();

            if (this._app.getHealtOfStruct() < 20)
            {
                MessageBox.Show("Consider increasing DEPTH or OBJECT COUNT in quad.");
            }
            if (_app._area.wasOptimalized)
            {
                this.improveLBL.Text = _app.getImprovement().ToString();
            }
            else
            {
                this.improveLBL.Text = "NOT\nYET";
            }

            this.HidePanels();

            //new Grid for interval search
            dataWithRangeSearch.Columns.Add("Reg.Number", typeof(int));
            dataWithRangeSearch.Columns.Add("DESC", typeof(string));
            dataWithRangeSearch.Columns.Add("Type"); //PROP / PLOT
            dataWithRangeSearch.Columns.Add("X0,Y0", typeof((double, double)));
            dataWithRangeSearch.Columns.Add("Xk,Yk", typeof((double, double)));

            //this.redoGrids();

            this.QuadPanel.Invalidate();
        }

        /// <summary>
        /// Hiding panels in GUI
        /// </summary>
        private void HidePanels()
        {
            panelSearchForProp.Hide();
            panelGiveRange.Hide();
            panelAddProp.Hide();
            panelAddPlot.Hide();
            panelPlot.Hide();
            panelProp.Hide();
            panelSeedApp.Hide();
            panelDataEditDel.Hide();
            panelDepth.Hide();
            panelSettings.Hide();
        }

        //DRAWING ==============================================================================================================================================================
        private void QuadPanel_Paint(object sender, PaintEventArgs e)
        {
            this.ClearPanel();
            this.show(this._app._area._root, e);
            this.showIntervalSearch(list, e, coordinatesIS);
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

            e.Graphics.DrawRectangle(blkpen, x0, y0, (xk - x0), (yk - y0));

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
        //============================================================================================================================

        //reclick to testing window
        private void appbutton_Click(object sender, EventArgs e)
        {
            var test = new Test(_test, _app);
            this.Hide();
            test.ShowDialog();
        }

        //standalone search
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
                    string objectDetails = $"PROPERTY: {property._registerNumber}\n" +
                                          $"Coordinates:\n({((Property)property).Coordinates.x.Longitude}{((Property)property).Coordinates.x.LongHem}," +
                                          $" {((Property)property).Coordinates.x.Latitude}{((Property)property).Coordinates.x.LatHem})\n" +
                                          $"Description: {((Property)property).Description}\n";
                    objectDetails += "Plots:\n";

                    foreach (var plot in ((Property)property)._lands)
                    {
                        objectDetails += $"{plot.RegisterNumber} : {plot.Description}\n";
                    }

                    objectDetails += "\n";

                    // Append the formatted text to the RichTextBox.
                    propInfo.AppendText(objectDetails);
                }
                else
                {
                    // Create a string to format the object's details.
                    string objectDetails = $"PLOT\nID: {property._registerNumber}\n" +
                                          $"Coordinates: ({((PlotOfLand)property).Coordinates.startPos._x}, {((PlotOfLand)property).Coordinates.startPos._y})\n" +
                                          $"                     ({((PlotOfLand)property).Coordinates.endPos._x}, {((PlotOfLand)property).Coordinates.endPos._y})\n" +
                                          $"Description: {((PlotOfLand)property).Description}\n";

                    objectDetails += "Properties:\n";

                    foreach (var prop in ((PlotOfLand)property)._properties)
                    {
                        objectDetails += $"{prop.RegisterNumber} : {prop.Description}\n";
                    }
                    objectDetails += "\n";


                    // Append the formatted text to the RichTextBox.
                    propInfo.AppendText(objectDetails);

                }

                this.QuadPanel.Invalidate();
            }

        }

        //NEW RANGE SEARCH MENU BUTTON
        private void giveRangeButton_Click(object sender, EventArgs e)
        {
            this.HidePanels();
            panelGiveRange.Show();
        }

        private void searchRangeButton_Click(object sender, EventArgs e)
        {
            coordinatesIS = (new Coordinates((double)startPosLong.Value, (double)startPosLat.Value, 0), new Coordinates((double)endPosLong.Value, (double)endPosLat.Value, 0));
            list = _app.FindInterval(coordinatesIS);

            fillTheGridWithActualData(list);

            panelDataEditDel.Show();
            this.QuadPanel.Invalidate();
        }

        //NEW GRID WITH DATA FROM RANGE SEARCH
        private void fillTheGridWithActualData(List<Polygon> objects)
        {
            //clear the dataTable binded to grid
            dataWithRangeSearch.Rows.Clear();

            foreach (var item in list)
            {
                DataRow row = dataWithRangeSearch.NewRow();
                row[0] = item._registerNumber;
                if (item is Property)
                {
                    row[1] = ((Property)item)._description;
                    //QuadTree.GeoSystem.Property
                    row[2] = ((Property)item).GetType().ToString().Substring(19, 8);
                    row[3] = (((Property)item).Coordinates.x._x, ((Property)item).Coordinates.x._y);
                    row[4] = (((Property)item).Coordinates.y._x, ((Property)item).Coordinates.y._y);



                }
                else
                {
                    row[1] = ((PlotOfLand)item)._description;
                    row[2] = item.GetType().ToString().Substring(19, 10);
                    row[3] = (((PlotOfLand)item).Coordinates.startPos._x, ((PlotOfLand)item).Coordinates.startPos._y);
                    row[4] = (((PlotOfLand)item).Coordinates.endPos._x, ((PlotOfLand)item).Coordinates.endPos._y);

                }
                dataWithRangeSearch.Rows.Add(row);
            }

            dataGridEditDelete.DataSource = dataWithRangeSearch;
        }

        //NEW SEARCH INDIVIDUALLY MENU BUTTON
        private void searchPropButton_Click(object sender, EventArgs e)
        {
            this.HidePanels();
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

            _app.AddProperty(registerNumber, description.Text, (new Coordinates((double)posLong.Value, (double)posLat.Value, 0), new Coordinates((double)posLongEnd.Value, (double)posLatEnd.Value, 0)));
            this.HealtPanelUpdate();
            //this.redoGrids();
            this.QuadPanel.Invalidate(true);

        }

        //ADD PROP MENU BUTTON
        private void addPropButton_Click(object sender, EventArgs e)
        {
            this.HidePanels();
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
            this.HealtPanelUpdate();
            //this.redoGrids();
            this.QuadPanel.Invalidate(true);
        }

        //ADD PLOT MENU BUTTON
        private void addPlotButton_Click(object sender, EventArgs e)
        {
            this.HidePanels();
            panelAddPlot.Show();
        }

        private void deletePropButton_Click(object sender, EventArgs e)
        {
            this.HidePanels();

        }

        private void showBtn_Click(object sender, EventArgs e)
        {
            var numberInGrid = dataGridEditDelete.SelectedCells.Count;
            // Attach the cell click event handler.
            if (dataGridEditDelete.SelectedCells.Count > 0) //at least 1 cell is selected
            {
                //which row is selected
                //get the obj in that row
                var obj = dataGridEditDelete.SelectedRows[0];

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


        /*//old EDIT does not go here
        private void editBtn_Click(object sender, EventArgs e)
        {
            //this.redoGrids();
            if (dataGridEditDelete.SelectedRows.Count > 0)
            {
                // Get the first selected row (if you allow multiple selections, you may iterate through them).
                DataGridViewRow selectedRow = dataGridEditDelete.SelectedRows[0];
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

                    originalProp = (Property)_app.PickToEdit(new Property((int)regNumb, "", (new Coordinates((double)startPosX, (double)startPosY, 0), new Coordinates((double)endPosX, (double)endPosY, 0)), null));
                    //originalProp = this._app.PickAttrProp(new Property((int)regNumb, "", (new Coordinates((double)startPosX, (double)startPosY, 0), new Coordinates((double)endPosX, (double)endPosY, 0)), null));


                    if (originalProp != null)
                    {
                        rnPropEdit.Text = originalProp._registerNumber.ToString();
                        editPropStartX.Value = (decimal)originalProp.suradnice.x._x;
                        editPropStartY.Value = (decimal)originalProp.suradnice.x._y;
                        editPropEndX.Value = (decimal)originalProp.suradnice.y._x;
                        editPropEndY.Value = (decimal)originalProp.suradnice.y._y;
                        descBoxEditProp.Text = (originalProp)._description.ToString();

                        originalPROPDescription = descBoxEditProp.Text;
                        originalPROPRegisterNumber = originalProp._registerNumber;
                        originalPROPXCoordinateStart = originalProp.suradnice.x.Longitude;
                        originalPROPYCoordinateStart = originalProp.suradnice.x.Latitude;
                        originalPROPXCoordinateEnd = originalProp.suradnice.y.Longitude;
                        originalPROPYCoordinateEnd = originalProp.suradnice.y.Latitude;
                    }

                }
                else
                {
                    //new panel
                    panelProp.Hide();
                    panelPlot.Show();

                    originalPlot = (PlotOfLand)_app.PickToEdit(new PlotOfLand((int)regNumb, "", (new Coordinates((double)startPosX, (double)startPosY, 0), new Coordinates((double)endPosX, (double)endPosY, 0)), null));
                    //originalPlot = this._app.PickAttrPlot(new PlotOfLand((int)regNumb, "", (new Coordinates((double)startPosX, (double)startPosY, 0), new Coordinates((double)endPosX, (double)endPosY, 0)), null));

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
        }*/

        private void editbtnPlot_Click(object sender, EventArgs e)
        {
            if (startPosEditPlotX.Value > endPosEditPlotX.Value || startPosEditPlotY.Value > endPosEditPlotY.Value)
            {
                MessageBox.Show("Wrong key attributes.");
                return;
            }

            bool keyAttrChanged =
                (decimal)originalPLOTXCoordinateStart != startPosEditPlotX.Value ||
                (decimal)originalPLOTYCoordinateStart != startPosEditPlotY.Value ||
                (decimal)originalPLOTXCoordinateEnd != endPosEditPlotX.Value ||
                (decimal)originalPLOTYCoordinateEnd != endPosEditPlotY.Value;

            var boolpom = int.TryParse(rnPlotEdit.Text, out int rn);

            bool attrChanged = originalPLOTRegisterNumber != rn ||
                originalPLOTDescription != descEditPlot.Text;


            /* var changed = this._app.EditObject(originalPlot,
                new PlotOfLand(rn, descEditPlot.Text,
                (new Coordinates((double)startPosEditPlotX.Value, (double)startPosEditPlotY.Value, 0),
                new Coordinates((double)endPosEditPlotX.Value, (double)endPosEditPlotY.Value, 0)), null),
                keyAttrChanged);

             if (changed)
             {
                 MessageBox.Show("Attributes changed.");
             }
             else
             {
                 MessageBox.Show("Failed changing attributes.");
             }*/



            if (!keyAttrChanged && attrChanged)
            {
                originalPlot._description = descEditPlot.Text;
                originalPlot._registerNumber = rn;
                selectedRowProp.Cells[0].Value = rn;
                selectedRowProp.Cells[1].Value = descEditPlot.Text;

                dataGridEditDelete.Refresh();
            }
            else if (keyAttrChanged)
            {
                if (_app.RemoveObj(new PlotOfLand(originalPlot.RegisterNumber, originalPlot.Description, originalPlot.Coordinates, null)))
                {
                    dataGridEditDelete.Rows.Remove(selectedRowProp);
                    _app.AddPlot(rn, descEditPlot.Text, (new Coordinates((double)startPosEditPlotX.Value, (double)startPosEditPlotY.Value, 0), new Coordinates((double)endPosEditPlotX.Value, (double)endPosEditPlotY.Value, 0)));
                    DataRow newRow = dataWithRangeSearch.NewRow();
                    newRow[0] = rn;
                    newRow[1] = descEditPlot.Text;
                    newRow[2] = "PlotOfLand";
                    newRow[3] = ((double, double))(startPosEditPlotX.Value, startPosEditPlotY.Value);
                    newRow[4] = ((double, double))(endPosEditPlotX.Value, endPosEditPlotY.Value);

                    dataWithRangeSearch.Rows.Add(newRow);
                    this.HealtPanelUpdate();
                    dataGridEditDelete.Refresh();
                }
            }

            panelPlot.Hide();
        }

        //NEW EDIT PROP BUTTON
        private void editBTNProp_Click(object sender, EventArgs e)
        {
            if (editPropStartX.Value > editPropEndX.Value || editPropStartY.Value > editPropEndY.Value)
            {
                MessageBox.Show("Wrong key attributes.");
                return;
            }
            //zmena klucoveho atributu
            bool keyAttrChanged = (decimal)originalPROPXCoordinateStart != editPropStartX.Value ||
                (decimal)originalPROPYCoordinateStart != editPropStartY.Value ||
                (decimal)originalPROPXCoordinateEnd != editPropEndX.Value ||
                (decimal)originalPROPYCoordinateEnd != editPropEndY.Value;

            var boolpom = int.TryParse(rnPropEdit.Text, out int rn);

            bool attrChanged = originalPROPRegisterNumber != rn ||
                originalPROPDescription != descBoxEditProp.Text;

            var changed = this._app.EditObject(originalProp,
               new Property(rn, descBoxEditProp.Text,
               (new Coordinates((double)editPropStartX.Value, (double)editPropStartY.Value, 0),
               new Coordinates((double)editPropEndX.Value, (double)editPropEndY.Value, 0)), null),
               keyAttrChanged);

            if (changed)
            {
                MessageBox.Show("Attributes changed.");
                dataGridEditDelete.Rows.Remove(selectedRowProp);
                DataRow newRow = dataWithRangeSearch.NewRow();
                newRow[0] = rn;
                newRow[1] = descBoxEditProp.Text;
                newRow[2] = "Property";
                newRow[3] = ((double, double))(editPropStartX.Value, editPropStartY.Value);
                newRow[4] = ((double, double))(editPropEndX.Value, editPropEndY.Value);

                dataWithRangeSearch.Rows.Add(newRow);
                dataGridEditDelete.Refresh();

            }
            else
            {
                MessageBox.Show("Failed changing attributes.");
            }


            //PICKING THE OBJECT FROM THE STRUCT TO THE FORM
            /*if (!keyAttrChanged && attrChanged)
            {
                originalProp._description = descBoxEditProp.Text;
                originalProp._registerNumber = rn;
                selectedRowProp.Cells[0].Value = rn;
                selectedRowProp.Cells[1].Value = descBoxEditProp.Text;
                dataGridEditDelete.Refresh();
            }
            else if (keyAttrChanged)
            {
                if (_app.RemoveObj(new Property(originalProp.RegisterNumber, originalProp.Description, originalProp.suradnice, null)))
                {
                    dataGridEditDelete.Rows.Remove(selectedRowProp);
                    _app.AddProperty(rn, descBoxEditProp.Text, (new Coordinates((double)editPropStartX.Value, (double)editPropStartY.Value, 0), new Coordinates((double)editPropEndX.Value, (double)editPropEndY.Value, 0)));
                    DataRow newRow = dataWithRangeSearch.NewRow();
                    newRow[0] = rn;
                    newRow[1] = descBoxEditProp.Text;
                    newRow[2] = "Property";
                    newRow[3] = ((double, double))(editPropStartX.Value, editPropStartY.Value);
                    newRow[4] = ((double, double))(editPropEndX.Value, editPropEndY.Value);

                    dataWithRangeSearch.Rows.Add(newRow);

                    healthLBL.Text = this._app._area.TreeHealth.Value.ToString();

                    dataGridEditDelete.Refresh();
                }
            }*/
            panelProp.Hide();
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            this.HidePanels();
            panelSettings.Show();
        }

        //NEW EXPORT MENU BUTTON
        private void exportBtn_Click(object sender, EventArgs e)
        {
            panelDepth.Hide();
            panelSeedApp.Hide();
            this._app.WriteToFiles();
            MessageBox.Show("Export Finished.");
        }

        //NEW IMPORT MENU BUTTON
        private void importBtn_Click(object sender, EventArgs e)
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
                this.HealtPanelUpdate();
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
                this.HealtPanelUpdate();
                MessageBox.Show("Import of Plots is completed.");
            }

            this.QuadPanel.Invalidate();
        }

        //NEW RESET APP MENU BUTTON
        private void resetAppBtn_Click(object sender, EventArgs e)
        {
            panelDepth.Hide();
            panelSeedApp.Hide();
            this._app.Reset();
            this.QuadPanel.Invalidate();
        }

        //NEW SEED APP MENU BUTTON
        private void button1_Click_1(object sender, EventArgs e)
        {
            panelDepth.Hide();
            panelSeedApp.Show();
        }

        //NEW SEEDING
        private void seedBtn2_Click(object sender, EventArgs e)
        {
            wasSeeded = true;

            //get values from GUI
            int max_depth = (int)DepthNo.Value;
            int objects_count = (int)CountNo.Value;
            double startX = (double)startCoordX.Value;
            double startY = (double)startCoordY.Value;
            double endX = (double)endCoordX.Value;
            double endY = (double)endCoordY.Value;
            int propNumber = (int)PropNo.Value;
            int plotNumber = (int)plotNo.Value;

            _app.setOptimalization(checkBoxWantOpt.Checked);

            _app.seedApp(startX, startY, endX, endY, propNumber, plotNumber, objects_count, max_depth);
            this.reinsert_lbl.Text = "";

            //this.redoGrids();

            panelSeedApp.Hide();

            //HEALTH
            this.HealtPanelUpdate();
            if (this._app.getHealtOfStruct() < 40)
            {
                MessageBox.Show("Consider increasing DEPTH or OBJECT COUNT in quad.");
            }
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

        //NEW CHANGE DEPTH MENU BUTTON
        private void changeDepthBtn_Click(object sender, EventArgs e)
        {
            panelSeedApp.Hide();
            panelDepth.Show();
            depthLabel.Text = this._app.getDepthOfStruct().ToString();
        }

        private void chngDepthBtn_Click(object sender, EventArgs e)
        {

            this._app.ChangeDepth((int)newDepthNum.Value);
            //this.redoGrids();
            this.HealtPanelUpdate();
            panelDepth.Hide();
            this.QuadPanel.Invalidate();
        }

        //NEW EDIT
        private void EditBtnRangeSearch_Click(object sender, EventArgs e)
        {
            if (dataGridEditDelete.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridEditDelete.SelectedRows[0];
                selectedRowProp = selectedRow;

                var regNumb = selectedRow.Cells[0].Value;
                var desc = selectedRow.Cells[1].Value;
                var typeOfObj = selectedRow.Cells[2].Value.ToString();
                (double, double) startPos = ((double, double))selectedRow.Cells[3].Value;
                (double, double) endPos = ((double, double))selectedRow.Cells[4].Value;

                if (typeOfObj.Equals("Property"))
                {
                    //new panel
                    panelPlot.Hide();
                    panelProp.Show();

                    //TODO: nepristupovat priamo na GUI k objektu

                    //originalProp = (Property)_app.PickToEdit(new Property((int)regNumb, (string)desc, (new Coordinates(startPos.Item1, startPos.Item2, 0), new Coordinates(endPos.Item1, endPos.Item2, 0)), null));
                    originalProp = this._app.PickAttrProp(new Property((int)regNumb, "", (new Coordinates(startPos.Item1, startPos.Item2, 0), new Coordinates(endPos.Item1, endPos.Item2, 0)), null));

                    if (originalProp != null)
                    {
                        rnPropEdit.Text = originalProp._registerNumber.ToString();
                        editPropStartX.Value = (decimal)((Property)originalProp).suradnice.x._x;
                        editPropStartY.Value = (decimal)((Property)originalProp).suradnice.x._y;
                        editPropEndX.Value = (decimal)((Property)originalProp).suradnice.y._x;
                        editPropEndY.Value = (decimal)((Property)originalProp).suradnice.y._y;
                        descBoxEditProp.Text = (originalProp)._description.ToString();

                        originalPROPDescription = descBoxEditProp.Text;
                        originalPROPRegisterNumber = originalProp._registerNumber;
                        originalPROPXCoordinateStart = originalProp.suradnice.x.Longitude;
                        originalPROPYCoordinateStart = originalProp.suradnice.x.Latitude;
                        originalPROPXCoordinateEnd = originalProp.suradnice.y.Longitude;
                        originalPROPYCoordinateEnd = originalProp.suradnice.y.Latitude;

                    }

                }
                else
                {
                    //new panel
                    panelProp.Hide();
                    panelPlot.Show();

                    originalPlot = (PlotOfLand)_app.PickToEdit(new PlotOfLand((int)regNumb, "", (new Coordinates(startPos.Item1, startPos.Item2, 0), new Coordinates(endPos.Item1, endPos.Item2, 0)), null));
                    //originalPlot = this._app.PickAttrPlot(new PlotOfLand((int)regNumb, "", (new Coordinates(startPos.Item1, startPos.Item2, 0), new Coordinates(endPos.Item1, endPos.Item2, 0)), null));


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
        }

        //NEW DELETE
        private void DeleteBtnRangeSearch_Click(object sender, EventArgs e)
        {
            if (dataGridEditDelete.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridEditDelete.SelectedRows[0];

                // Access data from specific cells within the selected row.
                var regNumb = selectedRow.Cells[0].Value;
                var desc = selectedRow.Cells[1].Value;
                var typeOfObj = selectedRow.Cells[2].Value.ToString();
                (double, double) startPos = ((double, double))selectedRow.Cells[3].Value;
                (double, double) endPos = ((double, double))selectedRow.Cells[4].Value;


                if (typeOfObj.Equals("Property"))
                {
                    if (_app.RemoveObj(new Property((int)regNumb, "", (new Coordinates(startPos.Item1, startPos.Item2, 0), new Coordinates(endPos.Item1, endPos.Item2, 0)), null)))
                    {
                        //remove from grid
                        dataGridEditDelete.Rows.Remove(selectedRow);
                        this.HealtPanelUpdate();
                    }

                }
                else
                {
                    if (_app.RemoveObj(new PlotOfLand((int)regNumb, "", (new Coordinates(startPos.Item1, startPos.Item2, 0), new Coordinates(endPos.Item1, endPos.Item2, 0)), null)))
                    {
                        //removefrom grid
                        dataGridEditDelete.Rows.Remove(selectedRow);
                        this.HealtPanelUpdate();
                    }
                }

                this.QuadPanel.Invalidate();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.HealtPanelUpdate();

            //var objects = this._app.FindInterval((new Coordinates(this._app._area._dimension.X0, this._app._area._dimension.Y0, 0), new Coordinates(this._app._area._dimension.Xk, this._app._area._dimension.Yk, 0))).Count;
            //manually optimizing struct
            this._app.OptimizeStruct();
            //var objects2 = this._app.FindInterval((new Coordinates(this._app._area._dimension.X0, this._app._area._dimension.Y0, 0), new Coordinates(this._app._area._dimension.Xk, this._app._area._dimension.Yk, 0))).Count;

            //withdrawal of the objects, sorting them from the largest and inserting again
            //input is increaing the size of the area to +10 percent of the original size
            // will optimize like this only if new struct has better health than original
            this._app.WithdrawAndOrder(true);

            if (this._app.improvedWithReinsert)
            {
                reinsert_lbl.Text = "YES";
            }
            else
            {
                reinsert_lbl.Text = "NO";
            }

            this.HealtPanelUpdate();
            this.QuadPanel.Invalidate();
        }

        private void HealtPanelUpdate()
        {
            this.healthLBL.Text = this._app.getHealtOfStruct().ToString();
            this.QuadsImprovedLBL.Text = this._app._area.quadsImproved.ToString();
            this.improveLBL.Text = this._app.getImprovement().ToString();
            this.yes_noLBL.Text = this._app.getHealtOfStruct() >= 62 ? "YES" : "NO";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
