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

namespace QuadTree
{
    public partial class Menu : Form
    {
        private QTreeTest _test;
        public Menu(QTreeTest test)
        {
            InitializeComponent();
            _test = test;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var testForm = new Test(_test);
            testForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
