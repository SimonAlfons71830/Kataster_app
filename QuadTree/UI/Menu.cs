using QuadTree.GeoSystem;
using QuadTree.test;
using QuadTree.UI;
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
        private GeoApp _app;
        public Menu(QTreeTest test, GeoApp app)
        {
            InitializeComponent();
            _test = test;
            _app = app;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var testForm = new Test(_test);
            testForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var appForm = new App(_app);
            appForm.ShowDialog();
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }
    }
}
