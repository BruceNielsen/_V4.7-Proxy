using NLog;
using System;
using System.Data;
using System.Windows.Forms;

namespace FruPak.PF.Utils.Scanning
{
    public partial class Bin_To_Order : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static int int_Current_User_Id = 0;

        public Bin_To_Order(int int_C_User_id)
        {
            InitializeComponent();

            int_Current_User_Id = int_C_User_id;

            //check if testing or not
            //if (FruPak.PF.Global.Global.bol_Testing == true)
            //{
            //    this.Text = "FruPak Process Factory - " + this.Text + " - Test Environment";
            //}
            //else
            //{
            //    this.Text = "FruPak Process Factory";
            //}

            DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Orders.Get_Info_Desc();
            cmb_Orders.DataSource = ds_Get_Info.Tables[0];
            cmb_Orders.DisplayMember = "Combined";
            cmb_Orders.ValueMember = "Order_Id";
            cmb_Orders.Text = null;
            ds_Get_Info.Dispose();

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
                else if (c.GetType() == typeof(FruPak.PF.Utils.UserControls.Customer))
                {
                    FruPak.PF.Utils.UserControls.Customer cust = (FruPak.PF.Utils.UserControls.Customer)c;
                    cust.CustomerChanged += new EventHandler(this.CustomerControl_CustomerChanged);
                }
            }

            #endregion Log any interesting events from the UI to the CSV log file
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            string str_msg = "";

            if (txt_bin_num.TextLength == 0)
            {
                str_msg = str_msg + "Invalid Bin Number. Please enter a valid Bin Number." + Environment.NewLine;
            }
            else
            {
                try
                {
                    Convert.ToInt32(txt_bin_num.Text);
                }
                catch
                {
                    str_msg = str_msg + "Invalid Bin Number. Please enter a valid Bin Number." + Environment.NewLine;
                }
            }
            if (cmb_Orders.SelectedIndex == -1)
            {
                str_msg = str_msg + "Invalid Order Number. Please select a valid Order Number from dropdown list." + Environment.NewLine;
            }

            DialogResult dlr = new DialogResult();
            if (str_msg.Length > 0)
            {
                dlr = MessageBox.Show(str_msg, "Process Factory - Order Number Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (dlr != System.Windows.Forms.DialogResult.OK)
            {
                DataSet ds = null;
                DataRow dr;
                ds = FruPak.PF.Data.AccessLayer.PF_Work_Order.Get_Current();
                int int_current_WO = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dr = ds.Tables[0].Rows[i];
                    int_current_WO = Convert.ToInt32(dr["Work_Order_Id"].ToString());
                }
                ds.Dispose();

                ds = FruPak.PF.Data.AccessLayer.PF_Pallet.Get_Pallet_barocde(int_current_WO, Convert.ToInt32(txt_bin_num.Text));
                int int_Pallet_Id = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dr = ds.Tables[0].Rows[i];
                    int_Pallet_Id = Convert.ToInt32(dr["Pallet_Id"].ToString());
                }
                ds.Dispose();
                int int_result = -1;
                int_result = FruPak.PF.Data.AccessLayer.PF_Pallet.Update_Order_Id(int_Pallet_Id, Convert.ToInt32(cmb_Orders.SelectedValue.ToString()), int_Current_User_Id);

                if (int_result >= 0)
                {
                    lbl_msg.Text = "Bin/Order has been updated";
                    lbl_msg.ForeColor = System.Drawing.Color.Blue;
                }
                else
                {
                    lbl_msg.Text = "Bin/Order failed to updated";
                    lbl_msg.ForeColor = System.Drawing.Color.Red;
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
            FruPak.PF.Utils.UserControls.Customer cust = (FruPak.PF.Utils.UserControls.Customer)sender;
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
        private void Bin_To_Order_KeyDown(object sender, KeyEventArgs e)
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