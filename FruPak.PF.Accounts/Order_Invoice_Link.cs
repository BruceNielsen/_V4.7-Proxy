using NLog;
using System;
using System.Data;
using System.Windows.Forms;

namespace PF.Accounts
{
    public partial class Order_Invoice_Link : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static bool bol_write_access;
        private static int int_Current_User_Id = 0;

        public Order_Invoice_Link(int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();
            int_Current_User_Id = int_C_User_id;

            //restrict access
            bol_write_access = bol_w_a;
            btn_Add.Enabled = bol_w_a;
            //check if testing or not

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

        private void Order_Invoice_Link_Load(object sender, EventArgs e)
        {
            DataSet ds_orders = PF.Data.AccessLayer.PF_Orders.Get_UnInvoiced();
            cmb_Order.DataSource = ds_orders.Tables[0];
            cmb_Order.DisplayMember = "Order_Id";
            cmb_Order.ValueMember = "Order_Id";
            cmb_Order.Text = null;

            DataSet ds_Invoice = PF.Data.AccessLayer.PF_A_Invoice.Get_Info();
            cmb_Invoice.DataSource = ds_Invoice.Tables[0];
            cmb_Invoice.DisplayMember = "Invoice_Id";
            cmb_Invoice.ValueMember = "Invoice_Id";
            cmb_Invoice.Text = null;
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            cmb_Invoice.Text = null;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            string str_msg = "";
            int int_result = 0;
            int int_order_id = 0;

            DialogResult DLR_Message = new DialogResult();
            if (cmb_Order.SelectedIndex == -1)
            {
                str_msg = str_msg + "Invalid Order Number. Please select a valid Order from the drop down list." + Environment.NewLine;
            }
            else
            {
                int_order_id = Convert.ToInt32(cmb_Order.SelectedValue.ToString());
            }
            if (cmb_Invoice.SelectedIndex == -1)
            {
                str_msg = str_msg + "Invalid Invoice Number. Please select a valid Invoice from the drop down list." + Environment.NewLine;
            }

            if (str_msg.Length > 0)
            {
                DLR_Message = MessageBox.Show(str_msg, "Process Factory - Dispatch(Orders)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (DLR_Message != System.Windows.Forms.DialogResult.OK)
            {
                DataSet ds = PF.Data.AccessLayer.PF_Pallet.Get_Info_for_Order("(" + int_order_id + ")");
                DataRow dr;

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    int int_Invoice_Details_id = PF.Common.Code.General.int_max_user_id("PF_A_Invoice_Details");

                    dr = ds.Tables[0].Rows[i];

                    int_result = int_result + PF.Data.AccessLayer.PF_A_Invoice_Details.Insert_Product(int_Invoice_Details_id, Convert.ToInt32(cmb_Invoice.SelectedValue.ToString()), Convert.ToInt32(dr["Material_Id"].ToString()), 0, int_Current_User_Id, Convert.ToString(int_order_id), Convert.ToDecimal(dr["Quantity"].ToString()));
                    int_result = int_result + PF.Data.AccessLayer.PF_Orders.Update_Invoiced(int_order_id, Convert.ToInt32(cmb_Invoice.SelectedValue.ToString()), int_Current_User_Id);
                }
            }
            if (int_result > 0)
            {
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                lbl_message.Text = "Order has been saved";
                Reset();
            }
            else
            {
                lbl_message.ForeColor = System.Drawing.Color.Red;
                lbl_message.Text = "Order has NOT been saved";
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
        private void Order_Invoice_Link_KeyDown(object sender, KeyEventArgs e)
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