using NLog;
using System;
using System.Data;
using System.Windows.Forms;

namespace FruPak.PF.Temp
{
    public partial class Submission_Bins : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static bool bol_write_access;
        private static int int_Current_User_Id = 0;
        private static int int_Submission_Id = 0;
        private static int int_Trader_Id = 0;
        private static decimal dec_cur_num_bins = 0;

        public Submission_Bins(int submission_Id, int Trader_Id, int int_C_User_id, bool bol_w_a, bool bol_Add)
        {
            InitializeComponent();

            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            {
                Console.WriteLine("FruPak.PF.Utils.UserControls.Submission_Bins - UsageMode = Designtime - Skipping populate()");
            }
            else
            {
                Console.WriteLine("FruPak.PF.Utils.UserControls.Submission_Bins - UsageMode = Runtime - Running populate()");
                //populate_comboboxes();
                //}

                int_Current_User_Id = int_C_User_id;
                int_Submission_Id = submission_Id;
                int_Trader_Id = Trader_Id;

                //restrict access
                bol_write_access = bol_w_a;
                btn_Add.Enabled = bol_w_a;

                //check if testing or not

                //if (FruPak.PF.Global.Global.bol_Testing == true)
                //{
                //    this.Text = "FruPak Process Factory - " + this.Text + " - Test Environment";
                //}
                //else
                //{
                //    this.Text = "FruPak Process Factory";
                //}

                //populate submission number
                txt_Submission.Text = Convert.ToString(submission_Id);

                populate_comboboxes();

                if (bol_Add == true)
                {
                    btn_Add.Text = "&Add";
                }
                else
                {
                    btn_Add.Text = "&Update";
                    DataSet ds = FruPak.PF.Data.AccessLayer.CM_Bins.Get_Info_Submission(int_Submission_Id);
                    DataRow dr;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dr = ds.Tables[0].Rows[i];
                        cmb_Location.SelectedValue = Convert.ToInt32(dr["Location_Id"].ToString());
                        cmb_ESP.SelectedValue = Convert.ToInt32(dr["ESP_Id"].ToString());
                        cmb_Storage.SelectedValue = Convert.ToInt32(dr["Storage_Id"].ToString());
                        fruit1.FruitType_Id = Convert.ToInt32(dr["FruitType_Id"].ToString());
                        fruit1.FruitVariety_Id = Convert.ToInt32(dr["Variety_Id"].ToString());
                        materialNumber1.setMaterial(dr["Material"].ToString());
                        nud_total_bins.Value = Convert.ToDecimal(dr["NumBins"]);
                        dec_cur_num_bins = Convert.ToDecimal(dr["NumBins"]);
                    }
                    ds.Dispose();
                }
            }

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

        private void populate_comboboxes()
        {
            DataSet ds_Get_Info;
            //Location
            ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Location.Get_Info();
            cmb_Location.DataSource = ds_Get_Info.Tables[0];
            cmb_Location.DisplayMember = "Combined";
            cmb_Location.ValueMember = "Location_Id";
            cmb_Location.Text = "YPCP - Yard Process Factory Canopy";
            ds_Get_Info.Dispose();

            //Storage
            ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Storage.Get_Info();
            cmb_Storage.DataSource = ds_Get_Info.Tables[0];
            cmb_Storage.DisplayMember = "Combined";
            cmb_Storage.ValueMember = "Storage_Id";
            cmb_Storage.Text = null;
            ds_Get_Info.Dispose();

            //ESP
            ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_ESP.Get_Info();
            cmb_ESP.DataSource = ds_Get_Info.Tables[0];
            cmb_ESP.DisplayMember = "Combined";
            cmb_ESP.ValueMember = "ESP_Id";
            cmb_ESP.Text = null;
            ds_Get_Info.Dispose();

            materialNumber1.selectedTrader = int_Trader_Id;
            materialNumber1.populate();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            DialogResult DLR_Message = new DialogResult();
            string str_msg = "";
            int int_result = 0;

            if (cmb_Location.SelectedIndex == -1)
            {
                str_msg = str_msg + "Invalid Location. Please select a valid Location from the drop down list." + Environment.NewLine;
            }
            if (fruit1.FruitType_Id == 0)
            {
                str_msg = str_msg + "Invalid Fruit Type. Please select a valid Fruit Type from the drop down list." + Environment.NewLine;
            }
            if (fruit1.FruitVariety_Id == 0)
            {
                str_msg = str_msg + "Invalid Fruit Variety. Please select a valid Fruit Varity from the drop down list." + Environment.NewLine;
            }
            if (materialNumber1.Material_Number == 0)
            {
                str_msg = str_msg + "Invalid Material Number. Please select a valid Material Number from the drop down list." + Environment.NewLine;
            }
            if (cmb_Storage.SelectedIndex == -1)
            {
                str_msg = str_msg + "Invalid Stroage. Please select a valid Storeage from the drop down list." + Environment.NewLine;
            }
            if (cmb_ESP.SelectedIndex == -1)
            {
                str_msg = str_msg + "Invalid ESP. Please select a valid ESP from the drop down list." + Environment.NewLine;
            }
            if (nud_total_bins.Value == 0)
            {
                str_msg = str_msg + "Invalid Number of Bins. Please enter a valid number of bins for this submission." + Environment.NewLine;
            }
            if (str_msg.Length > 0)
            {
                DLR_Message = MessageBox.Show(str_msg, "Process Factory - Submission", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (DLR_Message != System.Windows.Forms.DialogResult.OK)
            {
                switch (btn_Add.Text)
                {
                    case "&Add":
                        for (int i = 0; i < nud_total_bins.Value; i++)
                        {
                            int_result = Add_New_Bins();
                        }
                        break;

                    case "&Update":
                        // add new bins

                        if (dec_cur_num_bins < nud_total_bins.Value)
                        {
                            decimal dec_current = dec_cur_num_bins;
                            while (dec_current < nud_total_bins.Value)
                            {
                                int_result = Add_New_Bins();
                                dec_current++;
                            }
                        }
                        //number of bins has decreased, so a bins need to be deleted
                        else if (dec_cur_num_bins > nud_total_bins.Value)
                        {
                            int int_def = Convert.ToInt32(dec_cur_num_bins - nud_total_bins.Value);

                            for (int i = 0; i < int_def; i++)
                            {
                                int_result = FruPak.PF.Data.AccessLayer.CM_Bins.Delete(FruPak.PF.Common.Code.General.int_max_user_id("CM_Bins") - 1);
                            }
                        }
                        //update details only
                        else
                        {
                            // int_result = FruPak.PF.Data.AccessLayer.CM_Bins.Update(int_Submission_Id, Convert.ToInt32(cmb_Location.SelectedValue.ToString()),Convert.
                            MessageBox.Show("Not Coded Yet", "Process Factory - Temp(Submisssion - Update)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                }
            }
            if (int_result > 0)
            {
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                lbl_message.Text = Convert.ToString(int_result) + " Bins have been added to Submission";

                //information to return to main form;
                Created_Bins = int_result;
                btn_Add.DialogResult = System.Windows.Forms.DialogResult.OK;
                btn_Close.DialogResult = btn_Add.DialogResult;
                this.Close();
            }
            else
            {
                lbl_message.ForeColor = System.Drawing.Color.Red;
                lbl_message.Text = "Bins were NOT Saved to Submission";
            }
        }

        private int int_result = 0;

        private int Add_New_Bins()
        {
            int int_new_Bin_Id = FruPak.PF.Common.Code.General.int_max_user_id("CM_Bins");

            //Setup for Generating Barcode
            FruPak.PF.Common.Code.Barcode.Barcode_Trader = Convert.ToInt32(int_Trader_Id);
            FruPak.PF.Common.Code.Barcode.Barcode_Create(int_new_Bin_Id);

            int_result = int_result + FruPak.PF.Data.AccessLayer.CM_Bins.InsertSubmission(int_new_Bin_Id, int_Submission_Id, FruPak.PF.Common.Code.Barcode.Barcode_Num.ToString(),
                Convert.ToInt32(cmb_Location.SelectedValue.ToString()), materialNumber1.Material_Number, Convert.ToInt32(cmb_Storage.SelectedValue.ToString()),
                Convert.ToInt32(cmb_ESP.SelectedValue.ToString()), int_Current_User_Id);

            return int_result;
        }

        public static int Created_Bins
        {
            get;
            set;
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            cmb_ESP.Text = null;
            cmb_Location.Text = null;
            cmb_Storage.Text = null;
            nud_total_bins.Value = 0;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fruit1_FruitVarietyChanged(object sender, EventArgs e)
        {
            materialNumber1.selectedVariety = fruit1.FruitVariety_Id;
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
        private void Submission_Bins_KeyDown(object sender, KeyEventArgs e)
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