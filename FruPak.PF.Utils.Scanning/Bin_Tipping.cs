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
    public partial class Bin_Tipping : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();     

        private static bool bol_write_access;
        private static int int_Current_User_Id = 0;
        private static int int_Bin_Id = 0;
        private static int int_Work_Order_Id = 0;
        private static decimal dec_weight = 0;
        private static string str_dump_ind = "";
        public Bin_Tipping(int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();
            int_Current_User_Id = int_C_User_id;

            //if (FruPak.PF.Global.Global.bol_Testing == true)
            //{
            //    this.Text = "FruPak Process Factory - " + this.Text + " - Test Environment";
            //}
            //else
            //{
            //    this.Text = "FruPak Process Factory";
            //}
            //restrict access
            bol_write_access = bol_w_a;
            btn_Weight.Enabled = bol_w_a;
            btn_Wet.Enabled = bol_w_a;
            btn_Dry.Enabled = bol_w_a;
            populate_ComboBox();

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
        private void populate_ComboBox()
        {
            DataSet ds = FruPak.PF.Data.AccessLayer.PF_Work_Order.Get_Current();
            DataRow dr;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dr = ds.Tables[0].Rows[i];
                lbl_WO_Num.Text = dr["Work_Order_Id"].ToString();
                int_Work_Order_Id = Convert.ToInt32(dr["Work_Order_Id"].ToString());
            }

            ds.Dispose();
        }
        private bool bol_msg_reset = true;

        private void btn_Weight_Click(object sender, EventArgs e)
        {
            int int_result = -1;
            int_result = FruPak.PF.Data.AccessLayer.CM_Bins.Update_Weight_Gross(int_Bin_Id, nud_weight.Value, int_Current_User_Id);
            if (int_result >= 0)
            {
                lbl_message.Text = "Weight has been Loaded";
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                bol_msg_reset = false;
                Reset();
            }
        }
        private bool validate_barcode()
        {
            bool bol_valid = false;
            if (bol_msg_reset == true)
            {
                lbl_message.Text = "";
            }

            if (barcode1.BarcodeValue.Length == 0 && bol_msg_reset == true)
            {
                lbl_message.Text = "Invalid barcode";
                lbl_message.ForeColor = System.Drawing.Color.Red;
                bol_valid = false;
            }
            else
            {
                DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Bins.Get_Info_For_Barcode(barcode1.BarcodeValue.ToString());
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
                        catch (Exception ex)
                        {
                            //nud_weight.Value = 0;
                            logger.Log(LogLevel.Debug, ex.Message);
                        }
                        dec_weight = nud_weight.Value;
                    }

                    bol_valid = true;
                }
                else
                {
                    if (bol_msg_reset == true)
                    {
                        lbl_message.Text = "Invalid barcode";
                        lbl_message.ForeColor = System.Drawing.Color.Red;
                        bol_valid = false;
                    }
                }
                ds_Get_Info.Dispose();
            }
            return bol_valid;
        }
        private void nud_weight_Leave(object sender, EventArgs e)
        {
            if (dec_weight > 0)
            {
                btn_Wet.Focus();
            }
            else
            {
                btn_Weight.Focus();
            }
        }
        private void txt_barcode_Validating(object sender, CancelEventArgs e)
        {
            validate_barcode();
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
            nud_weight.Value = 0;
            btn_Dry.UseVisualStyleBackColor = true;
            btn_Dry.FlatStyle = FlatStyle.Standard;

            btn_Wet.UseVisualStyleBackColor = true;
            btn_Wet.FlatStyle = FlatStyle.Standard;
            str_dump_ind = "";
        }
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Wet_Dry_Click(object sender, EventArgs e)
        {
            if ((sender as Button).Name == "btn_Wet")
            {
                btn_Wet.BackColor = System.Drawing.Color.LightBlue;
                btn_Wet.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
                btn_Wet.FlatAppearance.BorderSize = 4;
                btn_Wet.FlatStyle = FlatStyle.Flat;
                btn_Dry.UseVisualStyleBackColor = true;
                btn_Dry.FlatStyle = FlatStyle.Standard;
                str_dump_ind = "W";
            }
            else if ((sender as Button).Name == "btn_Dry")
            {
                btn_Dry.BackColor = System.Drawing.Color.LightBlue;
                btn_Dry.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
                btn_Dry.FlatAppearance.BorderSize = 4;
                btn_Dry.FlatStyle = FlatStyle.Flat;
                btn_Wet.UseVisualStyleBackColor = true;
                btn_Wet.FlatStyle = FlatStyle.Standard;
                str_dump_ind = "D";
            }
            else
            {
                str_dump_ind = "";
            }

            if (str_dump_ind == "")
            {
                lbl_message.Text = "Invalid Dump option";
                lbl_message.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                if (validate_barcode() == true)
                {
                    int int_result = 0;
                    int_result = FruPak.PF.Data.AccessLayer.CM_Bins.Update_Weight_Gross(int_Bin_Id, nud_weight.Value, int_Current_User_Id);
                    int_result = int_result + FruPak.PF.Data.AccessLayer.CM_Bins.Update_Tipped_Date(int_Bin_Id, int_Work_Order_Id, int_Current_User_Id);
                    int_result = int_result + FruPak.PF.Data.AccessLayer.CM_Bins.Update_Dump_Ind(int_Bin_Id, str_dump_ind, int_Current_User_Id);

                    if (int_result >= 0)
                    {
                        lbl_message.Text = "Bin has been Tipped";
                        lbl_message.ForeColor = System.Drawing.Color.Blue;
                        bol_msg_reset = false;
                        Reset();
                    }
                }               
            }            
        }

        private void barcode1_txtChanged(object sender, EventArgs e)
        {
            validate_barcode();
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
        private void Bin_Tipping_KeyDown(object sender, KeyEventArgs e)
        {
            // Note that the KeyPreview property must be set to true for this to work
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        #endregion


    }
}
