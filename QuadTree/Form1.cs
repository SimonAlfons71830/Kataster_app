using QuadTree.QTree;
using QuadTree.Structures;
using QuadTree.test;

namespace QuadTree
{
    public partial class Form1 : Form
    {
        Pen blkpen = new Pen(Color.FromArgb(255, 0, 155, 0), 1);
        QTreeTest _test;
        Boolean buttonClicked = false;
        public Form1(QTreeTest test)
        {
            InitializeComponent();
            _test = test;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            _test.SetPocetOperacii((int)numericUpDown1.Value);
            _test.SetPocetInsert((int)numericUpDown2.Value);
            _test.SetPocetFind((int)numericUpDown3.Value);
            _test.SetSeed((int)numericUpDown4.Value);
            _test.setSizeOfTree((double)width_tree.Value, (double)heigth_tree.Value);

            _test.TestujInsertRemoveFind();

            richTextBox1.Text = "POCET VYKONANYCH OPERACII : " + _test.pocetVykonanychOperacii + "\n     " +
                                "pocet operacii insert : " + _test.pocetInsert + "\n       " +
                                "pocet operacii find : " + _test.pocetFind + "\n\n" +
                                "SUMAR TESTOV : \n\t passed : " + _test.passed + "\n\t failed : " + _test.failed +
                                "\n\n";

            List<ISpatialObject> list = _test.IntervalSearchTest(new QTree.Boundaries((double)numericUpDown8.Value, (double)numericUpDown7.Value, (double)numericUpDown6.Value, (double)numericUpDown5.Value));
            richTextBox1.Text += "Points in interval X < " + numericUpDown8.Value + "; " + numericUpDown7.Value + "> and Y <" + numericUpDown6.Value + "; " + numericUpDown5.Value + ">\n";
            var pomPassed = 0;
            var pomFailed = 0;
            for (int i = 0; i < list.Count; i++)
            {
                //richTextBox1.Text += "[" + list[i]._x + " ; " + list[i]._y + "]";

                if (list[i]._x <= (double)numericUpDown6.Value && list[i]._x >= (double)numericUpDown8.Value && list[i]._y <= (double)numericUpDown5.Value && list[i]._y >= (double)numericUpDown7.Value)
                {
                    pomPassed++;
                }
                else
                {
                    pomFailed++;
                }
            }
            richTextBox1.Text += "\nPASSED : " + pomPassed + " FAILED : " + pomFailed;

            buttonClicked = true;

            //redraws the panel
            this.QuadPanel.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

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
            this.ClearPanel();
            this.show(this._test.quadTree.GetRoot(), e);
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
                e.Graphics.DrawRectangle(blkpen, (float)_object._x, (float)_object._y, 1, 1);
            }
        }

    }
}