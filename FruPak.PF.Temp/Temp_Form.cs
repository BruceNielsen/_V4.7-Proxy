using System.Windows.Forms;

namespace FruPak.PF.Temp
{
    public partial class Temp_Form : Form
    {
        public Temp_Form()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Close the form with the Esc key (Sel request 11-02-2015 BN)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Temp_Form_KeyDown(object sender, KeyEventArgs e)
        {
            // Note that the KeyPreview property must be set to true for this to work
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}