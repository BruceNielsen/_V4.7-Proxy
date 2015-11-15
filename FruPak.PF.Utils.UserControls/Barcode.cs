using System;
using System.Windows.Forms;

namespace FruPak.PF.Utils.UserControls
{
    public partial class Barcode : UserControl
    {
        public event EventHandler txtChanged;

        public Barcode()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets/Sets Barcode (Renamed from Get_Barcode) BN 30/04/2015
        /// </summary>
        public string BarcodeValue
        {
            get
            {
                return txt_Barcode.Text;
            }
            set
            {
                txt_Barcode.Text = value;
                txt_Barcode.SelectAll();
                txt_Barcode.Focus();
            }
        }

        public string Set_Label
        {
            get
            {
                return lbl_barcode.Text;
            }
            set
            {
                lbl_barcode.Text = value;
            }
        }

        private void txt_Barcode_TextChanged(object sender, EventArgs e)
        {
            EventHandler handler = txtChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}