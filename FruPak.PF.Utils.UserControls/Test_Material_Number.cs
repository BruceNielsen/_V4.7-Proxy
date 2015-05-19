using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FruPak.PF.Utils.UserControls
{
    public partial class Test_Material_Number : Form
    {
        public Test_Material_Number()
        {
            InitializeComponent();
        }

        private void materialNumber1_Load(object sender, EventArgs e)
        {
            materialNumber1.populate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            materialNumber1.setMaterial(textBox1.Text);
        }

        /// <summary>
        /// Close the form with the Esc key (Sel request 11-02-2015 BN)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Test_Material_Number_KeyDown(object sender, KeyEventArgs e)
        {
            // Note that the KeyPreview property must be set to true for this to work
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
