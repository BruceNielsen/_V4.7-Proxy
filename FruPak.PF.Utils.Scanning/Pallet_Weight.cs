using NLog;
using System;
using System.Data;
using System.Windows.Forms;

namespace FruPak.PF.Utils.Scanning
{
    public partial class Pallet_Weight : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static bool bol_write_access;
        private static int int_Current_User_Id = 0;
        private static int int_Bin_Id = 0;
        private static bool bol_close = false;

        public Pallet_Weight(int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();
            int_Current_User_Id = int_C_User_id;

            //restrict access
            bol_write_access = bol_w_a;
            btn_Tipped.Enabled = bol_w_a;

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

        public Pallet_Weight(string barcode, int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();
            int_Current_User_Id = int_C_User_id;
            barcode1.BarcodeValue = barcode;

            //restrict access
            bol_write_access = bol_w_a;
            btn_Tipped.Enabled = bol_w_a;
            bol_close = true;

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

        private void btn_Tipped_Click(object sender, EventArgs e)
        {
            if (rbn_PalletCard.Checked == true)
            {
                if (validate_barcode() == true)
                {
                    if (nud_Gross_Weight.Value == 0 && nud_Tare_Weight.Value == 0)
                    {
                        lbl_message.Text = "Invalid Weights";
                        lbl_message.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        update_weights();
                    }
                }
            }
            else
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

                try
                {
                    ds = FruPak.PF.Data.AccessLayer.PF_Pallet.Get_Pallet_barocde(int_current_WO, Convert.ToInt32(barcode1.BarcodeValue.ToString()));
                    string str_Barcode = "";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dr = ds.Tables[0].Rows[i];
                        str_Barcode = dr["Barcode"].ToString();
                        int_Bin_Id = Convert.ToInt32(dr["Pallet_Id"].ToString());
                    }
                    ds.Dispose();
                    if (int_Bin_Id > 0)
                    {
                        update_weights();
                    }
                }
                catch
                {
                    lbl_message.Text = "Invalid Bin number or barcode";
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private void update_weights()
        {
            int int_result_G = -1;
            int int_result_T = -1;

            if (nud_Gross_Weight.Value > 0)
            {
                int_result_G = FruPak.PF.Data.AccessLayer.PF_Pallet.Update_Gross_Weight(int_Bin_Id, nud_Gross_Weight.Value, int_Current_User_Id);
            }
            if (nud_Tare_Weight.Value > 0)
            {
                int_result_T = FruPak.PF.Data.AccessLayer.PF_Pallet.Update_Tare_Weight(int_Bin_Id, nud_Tare_Weight.Value, int_Current_User_Id);
            }
            if (int_result_G > 0 && int_result_T == 0)
            {
                lbl_message.Text = "Gross Weight Added to Pallet";
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                Reset();
            }
            else if (int_result_G == 0 && int_result_T > 0)
            {
                lbl_message.Text = "Tare Weight Added to Pallet";
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                Reset();
            }
            else if (int_result_G > 0 && int_result_T > 0)
            {
                lbl_message.Text = "Gross and Tare Weights Added to Pallet";
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                Reset();
            }
            else
            {
                lbl_message.Text = "Weights failed to Added to Pallet";
                lbl_message.ForeColor = System.Drawing.Color.Red;
            }
            if (bol_close == true)
            {
                this.Close();
            }
        }

        private static decimal dec_weight = 0;

        private bool validate_barcode()
        {
            bool bol_valid = false;
            lbl_message.Text = "";

            if (barcode1.BarcodeValue.Length == 0)
            {
                lbl_message.Text = "Invalid barcode";
                lbl_message.ForeColor = System.Drawing.Color.Red;
                bol_valid = false;
            }
            else
            {
                DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Pallet.Get_Pallet_Id_From_Baracode(barcode1.BarcodeValue.ToString());
                if (ds_Get_Info.Tables[0].Rows.Count > 0)
                {
                    DataRow dr_Get_Info;
                    for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
                    {
                        dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                        int_Bin_Id = Convert.ToInt32(dr_Get_Info["Pallet_Id"].ToString());

                        try
                        {
                            if (nud_Gross_Weight.Value == 0)
                            {
                                nud_Gross_Weight.Value = Convert.ToDecimal(dr_Get_Info["Weight_Gross"].ToString());
                            }
                            if (nud_Tare_Weight.Value == 0)
                            {
                                nud_Tare_Weight.Value = Convert.ToDecimal(dr_Get_Info["Weight_Tare"].ToString());
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.Log(LogLevel.Debug, ex.Message);
                        }
                    }

                    bol_valid = true;
                }
                else
                {
                    lbl_message.Text = "Invalid barcode";
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    bol_valid = false;
                }
                ds_Get_Info.Dispose();
            }
            return bol_valid;
        }

        private void nud_weight_Leave(object sender, EventArgs e)
        {
            if (dec_weight > 0)
            {
                btn_Tipped.Focus();
            }
        }

        private void txt_barcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void Reset()
        {
            barcode1.BarcodeValue = "";
            nud_Gross_Weight.Value = 0;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void barcode1_txtChanged(object sender, EventArgs e)
        {
            if (rbn_PalletCard.Checked == true)
            {
                validate_barcode();
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
        private void Pallet_Weight_KeyDown(object sender, KeyEventArgs e)
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