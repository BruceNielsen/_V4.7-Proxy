using NLog;
using System;
using System.Data;
using System.Windows.Forms;

namespace PF.Utils.Scanning
{
    public partial class Wet_Dry : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static int int_Current_User_Id = 0;

        public Wet_Dry(int int_C_User_id)
        {
            InitializeComponent();
            int_Current_User_Id = int_C_User_id;

            //if (PF.Global.Global.bol_Testing == true)
            //{
            //    this.Text = "FP Process Factory - " + this.Text + " - Test Environment";
            //}
            //else
            //{
            //    this.Text = "FP Process Factory";
            //}

            #region Log any interesting events from the UI to the CSV log file

            foreach (Control c in this.Controls)
            {
                if (c.GetType() == typeof(Button))
                {
                    c.Click += new EventHandler(this.Control_Click);
                }
                else if (c.GetType() == typeof(TextBox))
                {
                    c.Validated += new EventHandler(this.Control_Validated);
                }
                else if (c.GetType() == typeof(ComboBox))
                {
                    ComboBox cb = (ComboBox)c;
                    cb.SelectedValueChanged += new EventHandler(this.Control_SelectedValueChanged);
                }
                else if (c.GetType() == typeof(DateTimePicker))
                {
                    DateTimePicker dtp = (DateTimePicker)c;
                    dtp.ValueChanged += new EventHandler(this.Control_ValueChanged);
                }
                else if (c.GetType() == typeof(NumericUpDown))
                {
                    NumericUpDown nud = (NumericUpDown)c;
                    nud.ValueChanged += new EventHandler(this.Control_NudValueChanged);
                }
                else if (c.GetType() == typeof(CheckBox))
                {
                    CheckBox cb = (CheckBox)c;
                    cb.CheckedChanged += new EventHandler(this.Control_CheckedChanged);
                }
                else if (c.GetType() == typeof(PF.Utils.UserControls.Customer))
                {
                    PF.Utils.UserControls.Customer cust = (PF.Utils.UserControls.Customer)c;
                    cust.CustomerChanged += new EventHandler(this.CustomerControl_CustomerChanged);
                }
            }

            #endregion Log any interesting events from the UI to the CSV log file
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbn_btn_Click(object sender, EventArgs e)
        {
            if (barcode1.BarcodeValue.Length == 0)
            {
                lbl_message.Text = "Invalid Barcode";
                lbl_message.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                int int_Bins_Id = 0;
                DataSet ds = null;
                if (rbn_PF.Checked == true)
                {
                    ds = PF.Data.AccessLayer.CM_Bins.Get_All_For_Barcode(barcode1.BarcodeValue.ToString());
                }
                else if (rbn_GDI.Checked == true)
                {
                    ds = PF.Data.AccessLayer.CM_Bins.Get_All_For_GDIBarcode(barcode1.BarcodeValue.ToString());
                }
                DataRow dr;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dr = ds.Tables[0].Rows[i];
                    int_Bins_Id = Convert.ToInt32(dr["Bins_Id"].ToString());
                }
                ds.Dispose();
                if (rbn_Wet.Checked == true)
                {
                    PF.Data.AccessLayer.CM_Bins.Update_Dump_Ind(int_Bins_Id, "W", int_Current_User_Id);
                    lbl_message.Text = "Updated to WET";
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                }
                else if (rbn_Dry.Checked == true)
                {
                    PF.Data.AccessLayer.CM_Bins.Update_Dump_Ind(int_Bins_Id, "D", int_Current_User_Id);
                    lbl_message.Text = "Updated to Dry";
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                }
            }
        }

        #region Methods to log UI events to the CSV file. BN 29/01/2015

        /// <summary>
        /// Method to log the identity of controls we are interested in into the CSV log file.
        /// BN 29/01/2015
        /// </summary>
        /// <param name="sender">Control</param>
        /// <param name="e">EventArgs</param>
        private void Control_Click(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(Button))
            {
                Button b = (Button)sender;
                logger.Log(LogLevel.Info, DecorateString(b.Name, b.Text, "Click"));
            }
        }

        private void Control_Validated(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            logger.Log(LogLevel.Info, DecorateString(t.Name, t.Text, "Validated"));
        }

        private void Control_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            logger.Log(LogLevel.Info, DecorateString(cb.Name, cb.Text, "SelectedValueChanged"));
        }

        private void Control_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;
            logger.Log(LogLevel.Info, DecorateString(dtp.Name, dtp.Text, "ValueChanged"));
        }

        private void Control_NudValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            logger.Log(LogLevel.Info, DecorateString(nud.Name, nud.Text, "NudValueChanged"));
        }

        private void Control_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            logger.Log(LogLevel.Info, DecorateString(cb.Name, cb.Checked.ToString(), "CheckedChanged"));
        }

        private void CustomerControl_CustomerChanged(object sender, EventArgs e)
        {
            PF.Utils.UserControls.Customer cust = (PF.Utils.UserControls.Customer)sender;
            logger.Log(LogLevel.Info, DecorateString(cust.Name, cust.Customer_Name, "TextChanged"));
        }

        #region Decorate String

        // DecorateString
        private string openPad = " --- [ ";

        private string closePad = " ] --- ";
        private string intro = "--->   { ";
        private string outro = " }   <---";

        /// <summary>
        /// Formats the output to the debug log so that the relevant bits stand out amongst the machine-generated stuff.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="input"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private string DecorateString(string name, string input, string action)
        {
            // This code is intentionally duplicated from Main_Menu.cs, as it's faster
            // to find the faulty code - This Class/Form is a known major problem area.

            string output = string.Empty;

            output = intro + name + openPad + input + closePad + action + outro;
            return output;
        }

        #endregion Decorate String

        /// <summary>
        /// Close the form with the Esc key (Sel request 11-02-2015 BN)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Wet_Dry_KeyDown(object sender, KeyEventArgs e)
        {
            // Note that the KeyPreview property must be set to true for this to work
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        #endregion Methods to log UI events to the CSV file. BN 29/01/2015
    }
}