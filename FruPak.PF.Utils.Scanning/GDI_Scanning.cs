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
    public partial class GDI_Scanning : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();     

        private static bool bol_write_access;
        private static int int_Current_User_Id = 0;
        private static int int_Bin_Id = 0;
        private static int int_GDI_Bin_Id = 0;
        private static int int_Trader_Id = 0;
        private static int int_Work_Order_Id = 0;
        public GDI_Scanning(int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();
            int_Current_User_Id = int_C_User_id;

            //restrict access
            bol_write_access = bol_w_a;
            btn_Weight.Enabled = bol_w_a;
            btn_Dry.Enabled = bol_w_a;          
            
            populate_ComboBox();
                        
            int_Trader_Id = 7;

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
            DataSet ds;
            ds = FruPak.PF.Data.AccessLayer.PF_Work_Order.Get_Current();
            DataRow dr;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dr = ds.Tables[0].Rows[i];
                lbl_WO_Num.Text = dr["Work_Order_Id"].ToString();
                int_Work_Order_Id = Convert.ToInt32(dr["Work_Order_Id"].ToString());
            }

            ds.Dispose();

            
            ds = FruPak.PF.Data.AccessLayer.GH_Submission.Get_for_Combo();
            cmb_sub_Id.DataSource = ds.Tables[0];
            cmb_sub_Id.DisplayMember = "Combined";
            cmb_sub_Id.ValueMember = "Submission_Id";
            cmb_sub_Id.Text = null;
            ds.Dispose();


        }
        private void btn_Tipped_Click(object sender, EventArgs e)
        {
            int int_result = 0;

            if (rbn_Overrun.Checked == true)
            {
                DataSet dsp = FruPak.PF.Data.AccessLayer.EX_GDI_Overrun.Get_GDIBarcode(txt_barcode.Text);
                if (dsp.Tables[0].Rows.Count < 1)
                {
                    lbl_message.Text = "Invalid Barcode";
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    int int_new_Bin_Id = FruPak.PF.Common.Code.General.int_max_user_id("CM_Bins");

                    //Setup for Generating Barcode
                    FruPak.PF.Common.Code.Barcode.Barcode_Trader = Convert.ToInt32(int_Trader_Id);
                    FruPak.PF.Common.Code.Barcode.Barcode_Create(int_new_Bin_Id);

                    int_result = int_result + FruPak.PF.Data.AccessLayer.CM_Bins.InsertGDIOverrun(int_new_Bin_Id, FruPak.PF.Common.Code.Barcode.Barcode_Num.ToString(),txt_barcode.Text, int_Current_User_Id);

                    int_result = int_result + FruPak.PF.Data.AccessLayer.CM_Bins.Update_Weight_Gross(int_new_Bin_Id, nud_weight.Value, int_Current_User_Id);
                    int_result = int_result + FruPak.PF.Data.AccessLayer.CM_Bins.Update_Tare_Weight(int_new_Bin_Id,Convert.ToDecimal("65.00"), int_Current_User_Id);
                    int_result = int_result + FruPak.PF.Data.AccessLayer.CM_Bins.Update_Tipped_Date(int_new_Bin_Id, int_Work_Order_Id, int_Current_User_Id);
                    if ((sender as Button).Name == "btn_Wet")
                    {
                        int_result = int_result + FruPak.PF.Data.AccessLayer.CM_Bins.Update_Dump_Ind(int_new_Bin_Id, "W", int_Current_User_Id);
                    }
                    else if ((sender as Button).Name == "btn_Dry")
                    {
                        int_result = int_result + FruPak.PF.Data.AccessLayer.CM_Bins.Update_Dump_Ind(int_new_Bin_Id, "D", int_Current_User_Id);
                    }

                    int_result = int_result + FruPak.PF.Data.AccessLayer.EX_GDI_Overrun.Update_Active(txt_barcode.Text, false);
  
                }
                dsp.Dispose();
            }
            else
            {

                bool bol_valid_sub = false;
                if (cmb_sub_Id.SelectedIndex == -1)
                {
                    lbl_message.Text = "Invalid Submission";
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    bol_valid_sub = false;
                }
                else
                {
                    bol_valid_sub = true;
                }
                DataSet ds = FruPak.PF.Data.AccessLayer.CM_Bins.Get_Info_For_GDIBarcode(txt_barcode.Text);
                
                if (ds.Tables[0].Rows.Count > 0)
                {
                    int_result = int_result + FruPak.PF.Data.AccessLayer.CM_Bins.Update_Weight_Gross(int_Bin_Id, nud_weight.Value, int_Current_User_Id);
                    int_result = int_result + FruPak.PF.Data.AccessLayer.CM_Bins.Update_Tipped_Date(int_Bin_Id, int_Work_Order_Id, int_Current_User_Id);
                    if ((sender as Button).Name == "btn_Wet")
                    {
                        int_result = int_result + FruPak.PF.Data.AccessLayer.CM_Bins.Update_Dump_Ind(int_Bin_Id, "W", int_Current_User_Id);
                    }
                    else if ((sender as Button).Name == "btn_Dry")
                    {
                        int_result = int_result + FruPak.PF.Data.AccessLayer.CM_Bins.Update_Dump_Ind(int_Bin_Id, "D", int_Current_User_Id);
                    }
                    int_result = int_result + FruPak.PF.Data.AccessLayer.EX_GDI_Bins.Update_Active(int_GDI_Bin_Id, false, int_Current_User_Id);
                }
                else
                {
                    if (validate_barcode() == true && bol_valid_sub == true)
                    {
                        int_result = int_result + FruPak.PF.Data.AccessLayer.CM_Bins.Update_Weight_Gross(int_Bin_Id, nud_weight.Value, int_Current_User_Id);
                        int_result = int_result + FruPak.PF.Data.AccessLayer.CM_Bins.Update_Tipped_Date(int_Bin_Id, int_Work_Order_Id, int_Current_User_Id);
                        if ((sender as Button).Name == "btn_Wet")
                        {
                            int_result = int_result + FruPak.PF.Data.AccessLayer.CM_Bins.Update_Dump_Ind(int_Bin_Id, "W", int_Current_User_Id);
                        }
                        else if ((sender as Button).Name == "btn_Dry")
                        {
                            int_result = int_result + FruPak.PF.Data.AccessLayer.CM_Bins.Update_Dump_Ind(int_Bin_Id, "D", int_Current_User_Id);
                        }
                        int_result = int_result + FruPak.PF.Data.AccessLayer.EX_GDI_Bins.Update_Active(int_GDI_Bin_Id, false, int_Current_User_Id);
                    }
                }
            }
            if (int_result > 0)
            {
                lbl_message.Text = "Bin has been Tipped";
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                Reset();
            }
        }


        private void btn_Weight_Click(object sender, EventArgs e)
        {
            if (rbn_Overrun.Checked == true)
            {
                DataSet dsp = FruPak.PF.Data.AccessLayer.EX_GDI_Overrun.Get_GDIBarcode(txt_barcode.Text);
                if (dsp.Tables[0].Rows.Count < 1)
                {
                    lbl_message.Text = "Invalid Barcode";
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    FruPak.PF.Data.AccessLayer.EX_GDI_Overrun.Update_Weight(txt_barcode.Text, nud_weight.Value);
                    lbl_message.Text = "Weight has been updated";
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                }
                dsp.Dispose();
            }
            else
            {
                if (validate_barcode() == true)
                {
                    int int_result = 0;
                    int_result = FruPak.PF.Data.AccessLayer.CM_Bins.Update_Weight_Gross(int_Bin_Id, nud_weight.Value, int_Current_User_Id);
                    if (int_result > 0)
                    {
                        lbl_message.Text = "Weight has been Loaded";
                        lbl_message.ForeColor = System.Drawing.Color.Blue;
                        Reset();
                    }
                }
            }
        }
        private static decimal dec_weight = 0;
        private bool validate_barcode()
        {
            bool bol_valid = false;
            lbl_message.Text = "";

            if (txt_barcode.TextLength == 0)
            {
                lbl_message.Text = "Invalid barcode";
                lbl_message.ForeColor = System.Drawing.Color.Red;
                bol_valid = false;
            }
            else
            {
                DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.EX_GDI_Bins.Get_Info(txt_barcode.Text);
                if (ds_Get_Info.Tables[0].Rows.Count > 0)
                {
                    DataRow dr_Get_Info;
                    for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
                    {
                        dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                        int_GDI_Bin_Id = Convert.ToInt32(dr_Get_Info["Bins_Id"].ToString());
                        int_Bin_Id = FruPak.PF.Common.Code.General.int_max_user_id("CM_Bins");

                        //Setup for Generating Barcode

                       
                        FruPak.PF.Common.Code.Barcode.Barcode_Trader = Convert.ToInt32(int_Trader_Id);
                        FruPak.PF.Common.Code.Barcode.Barcode_Create(int_Bin_Id);


                        int Loc_id = 456;
                        int Mat_id = Convert.ToInt32(dr_Get_Info["Material_Id"].ToString());
                        int Stor_id = Convert.ToInt32(dr_Get_Info["Storage_Id"].ToString());
                        int ESP_id = Convert.ToInt32(dr_Get_Info["ESP_Id"].ToString());

                        FruPak.PF.Data.AccessLayer.CM_Bins.InsertSubmission(int_Bin_Id, Convert.ToInt32(cmb_sub_Id.SelectedValue), FruPak.PF.Common.Code.Barcode.Barcode_Num.ToString(),
                            Loc_id, Mat_id, Stor_id, ESP_id, int_Current_User_Id, 65);

                        FruPak.PF.Data.AccessLayer.Temp_submission.Update_GDIBarcode(int_Bin_Id, txt_barcode.Text, int_Current_User_Id);
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
                           // nud_weight.Value = 0;
                            logger.Log(LogLevel.Debug, ex.Message);
                        }
                        dec_weight = nud_weight.Value;
                    }

                    bol_valid = true;
                }
                else
                {
                    DataSet ds_bin_id = FruPak.PF.Data.AccessLayer.CM_Bins.Get_Info_For_GDIBarcode(txt_barcode.Text);
                    if (ds_bin_id.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr_bin_id;
                        for (int i = 0; i < Convert.ToInt32(ds_bin_id.Tables[0].Rows.Count.ToString()); i++)
                        {
                            dr_bin_id = ds_bin_id.Tables[0].Rows[i];
                            int_Bin_Id = Convert.ToInt32(dr_bin_id["Bins_Id"].ToString());                      
                        }
                        bol_valid = true;
                    }
                    else
                    {
                        lbl_message.Text = "Invalid barcode";
                        lbl_message.ForeColor = System.Drawing.Color.Red;
                        bol_valid = false;
                    }
                    ds_bin_id.Dispose();
                }
                
                ds_Get_Info.Dispose();
            }
            return bol_valid;
        }
        private void nud_weight_Leave(object sender, EventArgs e)
        {
            if (dec_weight > 0)
            {
                btn_Dry.Focus();
            }
            else
            {
                btn_Weight.Focus();
            }
        }
        private void txt_barcode_Validating(object sender, CancelEventArgs e)
        {
            if (rbn_Overrun.Checked == true)
            {
                DataSet dsp = FruPak.PF.Data.AccessLayer.EX_GDI_Overrun.Get_GDIBarcode(txt_barcode.Text);
                if (dsp.Tables[0].Rows.Count < 1)
                {
                    lbl_message.Text = "Invalid Barcode";
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lbl_message.Text = "Valid Barcode";
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    DataRow dr;
                    for (int i = 0; i < dsp.Tables[0].Rows.Count; i++)
                    {
                        dr = dsp.Tables[0].Rows[i];
                        try
                        {
                            nud_weight.Value = Convert.ToDecimal(dr["Weight_Gross"].ToString());
                        }
                        catch (Exception ex)
                        {
                            logger.Log(LogLevel.Debug, ex.Message);
                        }
                    }
                }
            }
            else
            {
                validate_barcode();
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
           // txt_barcode.Text = "100";            
            txt_barcode.ResetText();
            nud_weight.Value = 0;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_barcode_TextChanged(object sender, EventArgs e)
        {
            if (rbn_Overrun.Checked == true)
            {

            }
            else
            {
                DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Bins.Get_Info_For_GDIBarcode(txt_barcode.Text);
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

                            nud_weight.Value = Convert.ToDecimal(dr_Get_Info["Weight_Gross"].ToString());

                        }
                        catch (Exception ex)
                        {
                            logger.Log(LogLevel.Debug, ex.Message);
                        }
                        dec_weight = nud_weight.Value;
                    }
                }
            }
        }

        private void rbn_CheckedChanged(object sender, EventArgs e)
        {
            cmb_sub_Id.Visible = rbn_Bin.Checked;
            lbl_sub_Id.Visible = rbn_Bin.Checked;
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
        private void GDI_Scanning_KeyDown(object sender, KeyEventArgs e)
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
