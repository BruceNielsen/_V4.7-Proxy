using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;

namespace FruPak.PF.Utils.Scanning
{
    public partial class Bin_Tare_Weights : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();     

        private static bool bol_write_access;
        private static int int_Current_User_Id = 0;
        private static int int_Bin_Id = 0;
        public Bin_Tare_Weights(int int_C_User_id, bool bol_w_a)
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
            #endregion

        }

        private void btn_Tipped_Click(object sender, EventArgs e)
        {
            bool bol_valid_wo = false;
            if (nud_weight.Value == 0)
            {
                lbl_message.Text = "Invalid Weight";
                lbl_message.ForeColor = System.Drawing.Color.Red;
                bol_valid_wo = false;
            }
            else
            {
                bol_valid_wo = true;
            }
            if (validate_barcode() == true && bol_valid_wo == true)
            {
                int int_result = 0;
                int_result = FruPak.PF.Data.AccessLayer.CM_Bins.Update_Tare_Weight(int_Bin_Id, nud_weight.Value, int_Current_User_Id);
                if (int_result > 0)
                {
                    lbl_message.Text = "Tare Weight Added to Bin";
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    Reset();
                }
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
                DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Bins.Get_Weights(barcode1.BarcodeValue.ToString());
                if (ds_Get_Info.Tables[0].Rows.Count > 0)
                {
                    DataRow dr_Get_Info;
                    for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
                    {
                        dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                        int_Bin_Id = Convert.ToInt32(dr_Get_Info["Bins_Id"].ToString());
                        try
                        {
                            Convert.ToDecimal(dr_Get_Info["Weight_Gross"].ToString());

                            if (nud_weight.Value == 0)
                            {
                                nud_weight.Value = Convert.ToDecimal(dr_Get_Info["Weight_Gross"].ToString());
                            }
                        }
                        catch
                        {
                            nud_weight.Value = 0;
                        }
                        dec_weight = nud_weight.Value;
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

        private void txt_barcode_Validating(object sender, CancelEventArgs e)
        {
            validate_barcode();        }

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
            nud_weight.Value = 0;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
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
        #endregion

        /// <summary>
        /// Close the form with the Esc key (Sel request 11-02-2015 BN)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bin_Tare_Weights_KeyDown(object sender, KeyEventArgs e)
        {
            // Note that the KeyPreview property must be set to true for this to work
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        #endregion

        private void nud_weight_Enter(object sender, EventArgs e)
        {
            nud_weight.Focus();
            nud_weight.Select(0, nud_weight.Value.ToString("000000").Length);

        }

        private void buttonZeroTareWeight_Click(object sender, EventArgs e)
        {
            nud_weight.Value = 0;
        }

    }
}
