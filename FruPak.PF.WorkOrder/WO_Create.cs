using NLog;
using System;
using System.Data;
using System.Windows.Forms;

namespace FruPak.PF.WorkOrder
{
    public partial class WO_Create : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static bool bol_write_access;
        private static int int_Current_User_Id = 0;
        private static int int_Work_Order_Id = 0;
        private static int int_result = 0;

        public WO_Create(int int_wo_id, int int_C_User_id, bool bol_w_a)
        {
            //must reset the bol_test on the grower user control before the form is Initialized
            bool bol_temp = FruPak.PF.Global.Global.bol_Testing;

            InitializeComponent();

            grower1.bol_test = bol_temp;

            FruPak.PF.Global.Global.bol_Testing = bol_temp;

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
            //restrict access
            bol_write_access = bol_w_a;
            btn_Add.Enabled = bol_w_a;
            btn_Update.Enabled = bol_w_a;

            set_btn_Current();
            populate_combobox();

            if (int_wo_id > 0)
            {
                int_Work_Order_Id = int_wo_id;
                txt_WorkOrder.Text = Convert.ToString(int_wo_id);
                btn_Add.Text = "&Copy";
                get_existing_Work_Order();
            }
            else
            {
                btn_Update.Visible = false;
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

        private void get_existing_Work_Order()
        {
            DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Work_Order.Get_Info(int_Work_Order_Id);
            DataRow dr_Get_Info;
            for (int i = 0; i < ds_Get_Info.Tables[0].Rows.Count; i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                dtp_date.Text = dr_Get_Info["Process_Date"].ToString();
                dtp_start.Text = dr_Get_Info["Start_Time"].ToString();
                dtp_finish.Text = dr_Get_Info["Finish_Time"].ToString();
                cmb_Trader.SelectedValue = Convert.ToInt32(dr_Get_Info["Trader_Id"].ToString());
                grower1.Orchardist_Id = Convert.ToInt32(dr_Get_Info["Orchardist_Id"].ToString());
                grower1.Grower_Id = Convert.ToInt32(dr_Get_Info["Grower_Id"].ToString());

                cmb_Product.SelectedValue = Convert.ToInt32(dr_Get_Info["Product_Id"].ToString());
                txt_batches.Text = dr_Get_Info["Num_Batches"].ToString();

                fruit1.FruitType_Id = Convert.ToInt32(dr_Get_Info["Fruit_Type_Id"]);
                fruit1.FruitVariety_Id = Convert.ToInt32(dr_Get_Info["Fruit_Variety_Id"]);

                cmb_Growing_Method.SelectedValue = Convert.ToInt32(dr_Get_Info["Growing_Method_Id"].ToString());
                txt_comments.Text = dr_Get_Info["Comments"].ToString();
            }

            ds_Get_Info.Dispose();
            //total bins tipped
            ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Bins.Count_Bins_for_WorkOrder(int_Work_Order_Id);
            for (int i = 0; i < ds_Get_Info.Tables[0].Rows.Count; i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                txt_bins_tipped.Text = dr_Get_Info["Total"].ToString();
            }
            ds_Get_Info.Dispose();
            //Current Bacth
            ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Pallet_Details.Current_Batch_for_WorkOrder(int_Work_Order_Id);
            for (int i = 0; i < ds_Get_Info.Tables[0].Rows.Count; i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                txt_current_batch.Text = dr_Get_Info["Current_Batch"].ToString();
            }
            //Product in Bacth
            ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Pallet_Details.Count_for_Current_Batch_for_WorkOrder(int_Work_Order_Id);
            for (int i = 0; i < ds_Get_Info.Tables[0].Rows.Count; i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                txt_ctn_batch.Text = dr_Get_Info["Current_Total"].ToString();
            }
            ds_Get_Info.Dispose();
            //total output
            ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Pallet_Details.Count_for_WorkOrder(int_Work_Order_Id);
            for (int i = 0; i < ds_Get_Info.Tables[0].Rows.Count; i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                txt_total_ctn.Text = dr_Get_Info["Total"].ToString();
            }
            ds_Get_Info.Dispose();
        }

        private void set_btn_Current()
        {
            btn_Current.Enabled = bol_write_access;
            if (int_Work_Order_Id == 0)
            {
                btn_Current.Visible = false;
            }
            else
            {
                btn_Current.Visible = true;
            }
        }

        private void populate_combobox()
        {
            DataSet ds_Get_Info;
            //Trader
            ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Trader.Get_Info();
            cmb_Trader.DataSource = ds_Get_Info.Tables[0];
            cmb_Trader.DisplayMember = "Combined";
            cmb_Trader.ValueMember = "Trader_Id";
            cmb_Trader.Text = null;
            ds_Get_Info.Dispose();

            //Product
            ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Product.Get_Info();
            cmb_Product.DataSource = ds_Get_Info.Tables[0];
            cmb_Product.DisplayMember = "Code";
            cmb_Product.ValueMember = "Product_Id";
            cmb_Product.Text = null;
            ds_Get_Info.Dispose();

            //Growing_Method
            ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Growing_Method.Get_Info();

            cmb_Growing_Method.DataSource = ds_Get_Info.Tables[0];
            cmb_Growing_Method.DisplayMember = "Combined";
            cmb_Growing_Method.ValueMember = "Growing_Method_Id";
            cmb_Growing_Method.Text = null;
            ds_Get_Info.Dispose();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            DialogResult DLR_MessageBox = new DialogResult();
            string str_msg = "";

            if (cmb_Product.SelectedValue == null)
            {
                str_msg = str_msg + "Invalid Product: Please select a valid product from the Drop Down List" + Environment.NewLine;
            }

            if (cmb_Trader.SelectedValue == null)
            {
                str_msg = str_msg + "Invalid Trader: Please select a valid Trader from the Drop Down List" + Environment.NewLine;
            }
            if (grower1.Orchardist_Id == 0)
            {
                str_msg = str_msg + "Invalid Orchardist: Please select a valid Orchardist from the Drop Down List" + Environment.NewLine;
            }
            if (grower1.Grower_Id == 0)
            {
                str_msg = str_msg + "Invalid Grower: Please select a valid Grower from the Drop Down List" + Environment.NewLine;
            }

            if (txt_batches.TextLength == 0)
            {
                str_msg = str_msg + "Invalid Num of Batches: Please enter a valid number of batches" + Environment.NewLine;
            }
            else
            {
                try
                {
                    decimal dec_NUm_bacth = Convert.ToDecimal(txt_batches.Text);
                }
                catch
                {
                    str_msg = str_msg + "Invalid Num of Batches: Please enter a valid number of batches" + Environment.NewLine;
                }
            }

            int int_FruitType_Id = fruit1.FruitType_Id;

            if (int_FruitType_Id == 0)
            {
                str_msg = str_msg + "Invalid Fruit Type: Please Select a valid Fruit Type" + Environment.NewLine;
            }

            int int_FruitVariety_Id = fruit1.FruitVariety_Id;
            if (int_FruitVariety_Id == 0)
            {
                str_msg = str_msg + "Invalid Fruit Variety: Please Select a valid Fruit Variety" + Environment.NewLine;
            }

            if (cmb_Growing_Method == null)
            {
                str_msg = str_msg + "Invalid Growing Method: Please select a valid Growing Method from the Drop Down List" + Environment.NewLine;
            }

            if (str_msg.Length != 0)
            {
                DLR_MessageBox = MessageBox.Show(str_msg, "Process Factory - Work Order", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (DLR_MessageBox != DialogResult.OK)
            {
                if ((sender as Button).Text == "&Add" || (sender as Button).Text == "&Copy")
                {
                    int_Work_Order_Id = FruPak.PF.Common.Code.General.int_max_user_id("PF_Work_Order");
                    txt_WorkOrder.Text = Convert.ToString(int_Work_Order_Id);

                    int_result = FruPak.PF.Data.AccessLayer.PF_Work_Order.Insert(int_Work_Order_Id, Convert.ToDecimal(DateTime.Now.ToString("yyyy")),
                                                                             dtp_date.Value.ToString("yyyy/MM/dd"), dtp_start.Value.ToString("HH:mm:ss"), dtp_finish.Value.ToString("HH:mm:ss"),
                                                                             Convert.ToInt32(cmb_Trader.SelectedValue.ToString()), Convert.ToInt32(cmb_Product.SelectedValue.ToString()),
                                                                             grower1.Orchardist_Id, grower1.Grower_Id,
                                                                             Convert.ToInt32(cmb_Growing_Method.SelectedValue.ToString()), int_FruitType_Id, int_FruitVariety_Id,
                                                                             Convert.ToDecimal(txt_batches.Text), txt_comments.Text, int_Current_User_Id);
                    if (int_result > 0)
                    {
                        lbl_message.ForeColor = System.Drawing.Color.Blue;
                        lbl_message.Text = "Work Order: " + Convert.ToString(int_Work_Order_Id) + " has been created";
                        set_btn_Current();
                    }
                    else
                    {
                        lbl_message.ForeColor = System.Drawing.Color.Red;
                        lbl_message.Text = "Work Order: " + Convert.ToString(int_Work_Order_Id) + " failed to be Created";
                    }
                }
                else if ((sender as Button).Text == "&Update")
                {
                    int_result = FruPak.PF.Data.AccessLayer.PF_Work_Order.Update(int_Work_Order_Id, Convert.ToDecimal(DateTime.Now.ToString("yyyy")),
                                                                             dtp_date.Value.ToString("yyyy/MM/dd"), dtp_start.Value.ToString("HH:mm:ss"), dtp_finish.Value.ToString("HH:mm:ss"),
                                                                             Convert.ToInt32(cmb_Trader.SelectedValue.ToString()), Convert.ToInt32(cmb_Product.SelectedValue.ToString()),
                                                                             grower1.Orchardist_Id, grower1.Grower_Id,
                                                                             Convert.ToInt32(cmb_Growing_Method.SelectedValue.ToString()), int_FruitType_Id, int_FruitVariety_Id,
                                                                             Convert.ToDecimal(txt_batches.Text), txt_comments.Text, int_Current_User_Id);
                    if (int_result > 0)
                    {
                        lbl_message.ForeColor = System.Drawing.Color.Blue;
                        lbl_message.Text = "Work Order: " + Convert.ToString(int_Work_Order_Id) + " has been Updated";
                    }
                    else
                    {
                        lbl_message.ForeColor = System.Drawing.Color.Red;
                        lbl_message.Text = "Work Order: " + Convert.ToString(int_Work_Order_Id) + " failed to be Updated";
                    }
                }
            }
        }

        private void btn_Current_Click(object sender, EventArgs e)
        {
            DataSet ds_Current = FruPak.PF.Data.AccessLayer.PF_Work_Order.Get_Current();
            DataRow dr_Current;

            for (int i = 0; i < Convert.ToInt32(ds_Current.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Current = ds_Current.Tables[0].Rows[i];
                int_result = FruPak.PF.Data.AccessLayer.PF_Work_Order.Update_Current_Ind(Convert.ToInt32(dr_Current["Work_Order_Id"].ToString()), false, int_Current_User_Id);
            }

            int_result = FruPak.PF.Data.AccessLayer.PF_Work_Order.Update_Current_Ind(int_Work_Order_Id, true, int_Current_User_Id);
            if (int_result > 0)
            {
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                lbl_message.Text = "Work Order: " + Convert.ToString(int_Work_Order_Id) + " has been Set to the Current Work Order.";
            }
            else
            {
                lbl_message.ForeColor = System.Drawing.Color.Red;
                lbl_message.Text = "Work Order: " + Convert.ToString(int_Work_Order_Id) + " FAILED to update the Current Status.";
            }
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            txt_WorkOrder.ResetText();
            dtp_date.Text = DateTime.Now.ToString();
            dtp_start.Text = DateTime.Now.ToString();
            dtp_finish.Text = DateTime.Now.ToString();
            cmb_Trader.Text = null;
            grower1.Orchardist_Id = 0;
            grower1.Grower_Id = 0;
            cmb_Product.Text = null;
            cmb_Growing_Method.Text = null;
            fruit1.FruitType_Id = 0;
            fruit1.FruitVariety_Id = 0;
            txt_batches.ResetText();
            txt_bins_tipped.ResetText();
            txt_comments.ResetText();
            txt_ctn_batch.ResetText();
            txt_current_batch.ResetText();
            txt_total_ctn.ResetText();
            btn_Add.Text = "&Add";
            btn_Update.Visible = false;
            int_Work_Order_Id = 0;
            set_btn_Current();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int int_FruitType_Id = 0;

        private void fruit1_FruitTypeChanged(object sender, EventArgs e)
        {
            int_FruitType_Id = (sender as FruPak.PF.Utils.UserControls.Fruit).FruitType_Id;
        }

        private int int_FruitVariety_Id = 0;

        private void fruit1_FruitVarietyChanged(object sender, EventArgs e)
        {
            int_FruitVariety_Id = (sender as FruPak.PF.Utils.UserControls.Fruit).FruitVariety_Id;
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            DataSet ds_system = FruPak.PF.Data.AccessLayer.CM_System.Get_Info_Like("PF%");
            DataRow dr_system;
            for (int i = 0; i < ds_system.Tables[0].Rows.Count; i++)
            {
                dr_system = ds_system.Tables[0].Rows[i];
                switch (dr_system["Code"].ToString())
                {
                    case "PF-TPath":
                        FruPak.PF.PrintLayer.Word.TemplatePath = dr_system["Value"].ToString();
                        break;

                    case "PF-TWorkO":
                        FruPak.PF.PrintLayer.Word.TemplateName = dr_system["Value"].ToString();
                        break;
                }
            }
            ds_system.Dispose();
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
        private void WO_Create_KeyDown(object sender, KeyEventArgs e)
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