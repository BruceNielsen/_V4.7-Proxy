using System;
using System.Windows.Forms;

namespace FruPak.PF.Utils.UserControls
{
    public partial class Test_Grower : Form
    {
        public Test_Grower()
        {
            InitializeComponent();
            grower2.Orchardist_Id = 5;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = grower2.Orchardist_Id.ToString();
            textBox2.Text = grower2.Grower_Id.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            grower2.Orchardist_Id = 0;
            grower2.Grower_Id = 0;
        }

        /// <summary>
        /// Close the form with the Esc key (Sel request 11-02-2015 BN)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Test_Grower_KeyDown(object sender, KeyEventArgs e)
        {
            // Note that the KeyPreview property must be set to true for this to work
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}