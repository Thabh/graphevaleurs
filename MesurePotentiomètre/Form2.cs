using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MesurePotentiomètre
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form grapheDirect = new Form3();
            grapheDirect.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form grapheDonnees = new Form1();
            grapheDonnees.Show();
        }
    }
}
