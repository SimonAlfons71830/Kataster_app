using QuadTree.Structures;
using QuadTree.test;

namespace QuadTree
{
    public partial class Form1 : Form
    {
        QTreeTest test;
        public Form1()
        {
            InitializeComponent();
            test = new QTreeTest();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            test.SetPocetOperacii((int)numericUpDown1.Value);
            test.SetPocetInsert((int)numericUpDown2.Value);
            test.SetPocetFind((int)numericUpDown3.Value);
            test.SetSeed((int)numericUpDown4.Value);

            test.TestujInsertRemoveFind();

            richTextBox1.Text = "POCET VYKONANYCH OPERACII : " + test.pocetVykonanychOperacii + "\n     " +
                                "pocet operacii insert : " + test.pocetInsert + "\n       " +
                                "pocet operacii find : " + test.pocetFind + "\n\n" +
                                "SUMAR TESTOV : \n\t passed : " + test.passed + "\n\t failed : " + test.failed +
                                "\n\n";

            List<ISpatialObject> list = test.IntervalSearchTest(new QTree.Boundaries((double)numericUpDown8.Value, (double)numericUpDown7.Value, (double)numericUpDown6.Value, (double)numericUpDown5.Value));
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

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}