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
                                "SUMAR TESTOV : \n\t passed : " + test.passed + "\n\t failed : " + test.failed;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}