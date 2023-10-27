using QuadTree.QTree;
using QuadTree.Structures;
using QuadTree.test;

namespace QuadTree
{
    public partial class Form1 : Form
    {
        Pen blkpen = new Pen(Color.FromArgb(255, 0, 155, 0), 1);
        Pen redpen = new Pen(Color.FromArgb(255, 155, 0, 0), 2);
        Pen failedPen = new Pen(Color.FromArgb(255, 155, 0, 0), 5);
        QTreeTest _test;
        List<ISpatialObject> list = new List<ISpatialObject>();
        List<ISpatialObject> list2 = new List<ISpatialObject>();
        Random rand = new Random();


        int x0;
        int y0;
        int xk;
        int yk;

        public Form1(QTreeTest test)
        {
            InitializeComponent();
            _test = test;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButtonPoints.Checked || radioButtonPolygons.Checked || radioButtonBoth.Checked)
            {
                if (radioButtonPoints.Checked)
                {
                    _test.setGeneratingObjects(1);
                }
                else if (radioButtonPolygons.Checked)
                {
                    _test.setGeneratingObjects(2);
                }
                else
                {
                    _test.setGeneratingObjects(3);
                }

                QuadPanel.Controls.Clear();
                this._test.ResetTest();
                list.Clear();
                list2.Clear();

                this.ClearPanel();

                _test.SetPocetOperacii((int)numericUpDown2.Value + (int)numericUpDown3.Value + (int)removeCount.Value);

                _test.SetPocetInsert((int)numericUpDown2.Value);
                _test.SetPocetFind((int)numericUpDown3.Value);
                _test.SetPocetRemove((int)removeCount.Value);

                //_test.SetSeed((int)numericUpDown4.Value);
                _test.setSizeOfTree((double)width_tree.Value, (double)heigth_tree.Value);
                _test.setMaxDepth((int)maxDepth.Value);
                _test.setMaxObjects((int)numberOfObjects.Value);

                _test.TestInsertRemoveFind();

                richTextBox1.Text = "POCET VYKONANYCH OPERACII : " + _test.pocetVykonanychOperacii + "\n" +
                                    "   pocet operacii insert : \n\t\tpassed: " + _test.passedInsert + "\n\t\tfailed: " + _test.failedInsert + "\n" +
                                    "   pocet operacii find : \n\t\tpassed: " + _test.passedFind + "\n\t\tfailed: " + _test.failedFind + "\n" +
                                    "   pocet operacii remove : \n\t\tpassed:" + _test.passedRemove + "\n\t\tfailed: " + _test.failedRemove + "\n";// +
                                                                                                                                                   //"\nSUMAR TESTOV : \n\t passed : " + _test.passed + "\n\t failed : " + _test.failed + "\n";

                x0 = rand.Next(0, (int)width_tree.Value);
                y0 = rand.Next(0, (int)heigth_tree.Value);
                xk = rand.Next(x0, (int)width_tree.Value);
                yk = rand.Next(y0, (int)heigth_tree.Value);
                list = _test.IntervalSearchTest(new QTree.Boundaries(x0, y0, xk, yk));
                list2 = _test.quadTree.IntervalSearchN(new QTree.Boundaries(x0, y0, xk, yk));
                richTextBox1.Text += "\nINTERVAL SEARACH : " + _test.TestIntervalSearch(list, list2);
                richTextBox1.Text += "\n\tPoints in interval X < " + x0 + "; " + y0 + "> and Y <" + xk + "; " + yk + ">\n";
                var pomPassed = 0;
                var pomFailed = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    //richTextBox1.Text += "[" + list[i]._x + " ; " + list[i]._y + "]";

                    if (list[i]._x <= xk && list[i]._x >= x0 && list[i]._y <= yk && list[i]._y >= y0)
                    {
                        pomPassed++;
                    }
                    else
                    {
                        pomFailed++;
                    }
                }
                richTextBox1.Text += "\t\tpassed : " + pomPassed + " \n\t\tfailed : " + pomFailed;
                richTextBox1.Text += "\n\tTOTAL POINTS: " + _test.quadTree._objectsCount;
                richTextBox1.Text += "\n\tPOINTS SEARCHED: " + _test.quadTree._objectsSearched;

                richTextBox1.Text += "\n\nFAILED OBJ";

                for (int i = 0; i < _test.failedObj.Count; i++)
                {
                    richTextBox1.Text += "\n" + _test.failedObj.ElementAt(i)._x + " , " + _test.failedObj.ElementAt(i)._y;
                }


                //redraws the panel
                this.QuadPanel.Invalidate();
            }
        }

        private void ClearPanel()
        {
            using (Graphics g = QuadPanel.CreateGraphics())
            {
                g.Clear(QuadPanel.BackColor);
            }
        }

        private void QuadPanel_Paint(object sender, PaintEventArgs e)
        {
            if (checkBox1.Checked)
            {
                this.ClearPanel();
                this.show(this._test.quadTree.GetRoot(), e);
                this.showIntervalSearch(list, e);
                this.showFailedFind(_test.failedObj, e);
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
                if (_object is MyPoint)
                {
                    e.Graphics.DrawRectangle(blkpen, (float)_object._x, (float)_object._y, 1, 1);
                }
                else
                {
                    e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, 0, 0, 155), 1), (float)((Polygon)_object).GetTops().ElementAt(0)._x, (float)((Polygon)_object).GetTops().ElementAt(0)._y, (float)((Polygon)_object).GetTops().ElementAt(1)._x - (float)((Polygon)_object).GetTops().ElementAt(0)._x, (float)((Polygon)_object).GetTops().ElementAt(1)._y - (float)((Polygon)_object).GetTops().ElementAt(0)._y);
                }
            }
        }

        private void showIntervalSearch(List<ISpatialObject> selectedPoints, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(redpen, x0, y0, xk - x0, yk - y0);
            foreach (var point in selectedPoints)
            {
                if (point is MyPoint)
                {
                    e.Graphics.DrawRectangle(redpen, (float)point._x, (float)point._y, 1, 1);
                }
                else
                {
                    e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, 155, 0, 155), 2), (float)((Polygon)point).GetTops().ElementAt(0)._x, (float)((Polygon)point).GetTops().ElementAt(0)._y, (float)((Polygon)point).GetTops().ElementAt(1)._x - (float)((Polygon)point).GetTops().ElementAt(0)._x, (float)((Polygon)point).GetTops().ElementAt(1)._y - (float)((Polygon)point).GetTops().ElementAt(0)._y);
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
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            QuadPanel.Controls.Clear();
            _test.TestRemoveSeparatelly();
            list = _test.IntervalSearchTest(new QTree.Boundaries(x0, y0, xk, yk));
            this.QuadPanel.Invalidate();
        }

    }
}