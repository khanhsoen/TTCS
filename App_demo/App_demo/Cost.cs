using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace App_demo
{
    public partial class Cost : Form
    {
        public int m_nCost;

        public Cost()
        {
            InitializeComponent();
        }

        private void Cost_Load(object sender, EventArgs e)
        {

        }
        private void OK_Click(object sender, EventArgs e)
        {
            if (value.Text == string.Empty)
                label1.Text = "nhap gia tri";
            else
            {
                m_nCost = int.Parse(value.Text);
                this.Close();
            }
        }
    }
}
