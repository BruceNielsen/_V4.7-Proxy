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
    public partial class Reject_Bins_Weights : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();     

        private static int int_Current_User_Id = 0;
        public Reject_Bins_Weights(int int_C_User_id)
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
        private bool Validate_Barcode()
        {
            bool bol_return_code = false;
            if (barcode1.BarcodeValue.Length == 0)
            {
                lbl_message.Text = "Invalid Barcode.";
                bol_return_code = false;
            }
            else
            {
                bol_return_code = true;
            }
            return bol_return_code;
        }
        private bool Validate_Gross()
        {
            bool bol_return_code = false;
            if (nud_Gross.Value == 0)
            {
                lbl_message.Text = "Invalid Weight";
                bol_return_code = false;
            }
            else
            {
                bol_return_code = true;
            }
            return bol_return_code;
        }
        private int Get_Bin_Id()
        {
            int return_code = 0;
            //get the bin_id from the Barcode
            DataSet ds = FruPak.PF.Data.AccessLayer.CM_Bins.Get_Info_For_Barcode(barcode1.BarcodeValue.ToString());
            DataRow dr;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dr = ds.Tables[0].Rows[i];
                return_code = Convert.ToInt32(dr["Bins_Id"].ToString());
            }
            ds.Dispose();
            return return_code;
        }
        private int Get_Material_Id(string str_PG_Code, int int_Fruit_Type_Id, int int_Fruit_Variety_Id)
        {
            int return_code = 0;
            DataSet ds = FruPak.PF.Data.AccessLayer.CM_Material.Get_Material_for_Prod_Grp(str_PG_Code, int_Fruit_Type_Id, int_Fruit_Variety_Id);
            DataRow dr;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dr = ds.Tables[0].Rows[i];
                return_code = Convert.ToInt32(dr["Material_Id"].ToString());
            }
            ds.Dispose();

            return return_code;
        }
        private int Update_Tare(int int_Bin_Id, decimal dec_Tare)
        {
           return FruPak.PF.Data.AccessLayer.CM_Bins.Update_Tare_Weight(int_Bin_Id, dec_Tare, int_Current_User_Id);
        }
        private int Update_Gross(int int_Bin_Id, decimal dec_Gross)
        {
            return FruPak.PF.Data.AccessLayer.CM_Bins.Update_Weight_Gross(int_Bin_Id, dec_Gross, int_Current_User_Id);
        }
        private int Update_Material(int int_Bin_Id, int int_Material_Id)
        {
            return FruPak.PF.Data.AccessLayer.CM_Bins.Update_Material_Id(int_Bin_Id, int_Material_Id, int_Current_User_Id);
        }        
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private int Common_Bin_Detail(string str_bin_type)
        {
            int int_result = 0;
            int int_Bin_Id = Get_Bin_Id();
            int_result = int_result + Update_Tare(int_Bin_Id, Convert.ToDecimal("65.00"));
            int_result = int_result + Update_Gross(int_Bin_Id, nud_Gross.Value);
            DataSet ds = FruPak.PF.Data.AccessLayer.CM_Bins.Get_fruit_from_bin(int_Bin_Id);
            DataRow dr;
            int int_Fruit_Type_Id = 0;
            int int_Fruit_Variety_Id = 0;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dr = ds.Tables[0].Rows[i];
                int_Fruit_Type_Id = Convert.ToInt32(dr["Fruit_Type_Id"].ToString());
                int_Fruit_Variety_Id = Convert.ToInt32(dr["Fruit_Variety_Id"].ToString());
            }

            int_result = int_result + Update_Material(int_Bin_Id, Get_Material_Id(str_bin_type, int_Fruit_Type_Id, int_Fruit_Variety_Id));

            return int_result;
        }
        private void btn_Small_Click(object sender, EventArgs e)
        {
            int int_result = 0;

            if (Validate_Barcode() == true && Validate_Gross() == true)
            {
               int_result = Common_Bin_Detail("PFS");
            }
            if (int_result >= 0)
            {
                lbl_message.Text = "Smalls Bin has been Updated";
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                Reset();
            }
            else
            {
                lbl_message.Text = "Bin was NOT updated";
                lbl_message.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void btn_Peeler_Click(object sender, EventArgs e)
        {
            int int_result = 0;

            if (Validate_Barcode() == true && Validate_Gross() == true)
            {
                int_result = Common_Bin_Detail("PFR");
            }
            if (int_result >= 0)
            {
                lbl_message.Text = "Peeler Bin has been Updated";
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                Reset();
            }
            else
            {
                lbl_message.Text = "Bin was NOT updated";
                lbl_message.ForeColor = System.Drawing.Color.Red;
            }
        }
        private void Reset()
        {
            barcode1.BarcodeValue = "";
            nud_Gross.Value = 0;
        }

        private void btn_Grower_Click(object sender, EventArgs e)
        {
            int int_result = 0;

            if (Validate_Barcode() == true && Validate_Gross() == true)
            {
                int_result = Common_Bin_Detail("PFG");
            }
            if (int_result >= 0)
            {
                lbl_message.Text = "Grower Bin has been Updated";
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                Reset();
            }
            else
            {
                lbl_message.Text = "Bin was NOT updated";
                lbl_message.ForeColor = System.Drawing.Color.Red;
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
        #endregion

        /// <summary>
        /// Close the form with the Esc key (Sel request 11-02-2015 BN)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reject_Bins_Weights_KeyDown(object sender, KeyEventArgs e)
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
