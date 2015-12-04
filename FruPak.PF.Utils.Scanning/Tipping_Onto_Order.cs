using NLog;
using System;
using System.Data;
using System.Windows.Forms;

namespace PF.Utils.Scanning
{
    public partial class Tipping_Onto_Order : Form
    {
        // Phantom 11/12/2014
        private static Logger logger = LogManager.GetCurrentClassLogger();
        DataTable dt_Custom;    // For Multi-Column ComboBox and also TreeView - BN 29/11/2015

        private static bool bool_write_access;
        private static int int_Current_User_Id = 0;

        public Tipping_Onto_Order(int int_C_User_id, bool bol_w_a)
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

            //restrict access
            bool_write_access = bol_w_a;
            btn_Add.Enabled = bol_w_a;

            AddColumnsProgrammatically();
            populate_combobox();

            //for testing
            barcode1.Set_Label = "From :";
            barcode1.BarcodeValue = "00394210096400000011";

            populate_dataGridView1();

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

        private void populate_combobox()
        {
            DataSet ds_Get_Info = PF.Data.AccessLayer.PF_Orders.Get_Info_Desc();

            //multiColumnComboBoxOrders.DataSource = ds_Get_Info.Tables[0];
            //multiColumnComboBoxOrders.DisplayMember = "Order_Id";
            //multiColumnComboBoxOrders.ValueMember = "Order_Id";
            multiColumnComboBoxOrders.Text = null;

            // MultiColumnComboBox - BN 29/11/2015

            // Make a copy of the data
            dt_Custom = ds_Get_Info.Tables[0];

            // Remove the columns we don't want to see
            dt_Custom.Columns.Remove("Truck_Id");
            dt_Custom.Columns.Remove("City_Id");
            dt_Custom.Columns.Remove("Load_Date");
            dt_Custom.Columns.Remove("PF_Active_Ind");
            dt_Custom.Columns.Remove("Mod_User_Id");
            dt_Custom.Columns.Remove("Invoice_Id");
            dt_Custom.Columns.Remove("Customer_Id");
            dt_Custom.Columns.Remove("Hold_For_Payment");

            // The Comments field can be a bit too long but there's no easy way to override it
            // Easiest thing right now is to adjust the forms start position until I can find
            // either a better way of displaying the data or another ComboBox control entirely

            //dt_Custom.Columns("Comments");

            multiColumnComboBoxOrders.DataSource = dt_Custom;
            multiColumnComboBoxOrders.DisplayMember = "Order_Id";
            multiColumnComboBoxOrders.ValueMember = "Order_Id";

            ds_Get_Info.Dispose();
        }

        private void populate_barcode_combo()
        {
            DataSet ds_barcode = PF.Data.AccessLayer.PF_Pallet.Get_Barcode_For_Order(Convert.ToInt32(multiColumnComboBoxOrders.SelectedValue.ToString()));
            cmb_barcode2.DataSource = ds_barcode.Tables[0];
            cmb_barcode2.DisplayMember = "Barcode";
            cmb_barcode2.ValueMember = "Barcode";
            cmb_barcode2.Text = null;
            ds_barcode.Dispose();
        }

        private void AddColumnsProgrammatically()
        {
            var col0 = new DataGridViewTextBoxColumn();
            var col1 = new DataGridViewTextBoxColumn();
            var col2 = new DataGridViewTextBoxColumn();
            var col3 = new DataGridViewTextBoxColumn();
            var col4 = new DataGridViewTextBoxColumn();

            col0.HeaderText = "Pallet_Id";
            col0.Name = "Pallet_Id";
            col0.ReadOnly = false;
            col0.Visible = false;

            col1.HeaderText = "Date";
            col1.Name = "Date";
            col1.ReadOnly = false;
            col1.Visible = true;

            col2.HeaderText = "Batch";
            col2.Name = "Batch";
            col2.ReadOnly = true;

            col3.HeaderText = "Qty";
            col3.Name = "Qty";
            col3.ReadOnly = true;

            col4.HeaderText = "# Reqd";
            col4.Name = "Required";
            col4.ReadOnly = false;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2, col3, col4 });
        }

        private void populate_dataGridView1()
        {
            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();

            DataSet ds_Get_Info;
            DataRow dr_Get_Info;
            if (barcode1.BarcodeValue.Length > 0)
            {
                ds_Get_Info = PF.Data.AccessLayer.PF_Pallet.Get_Info_for_Barcode(barcode1.BarcodeValue.ToString());
                for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
                {
                    dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                    dataGridView1.Rows.Add(new DataGridViewRow());

                    DataGridViewCell DGVC_Id = new DataGridViewTextBoxCell();
                    DGVC_Id.Value = dr_Get_Info["Pallet_Id"].ToString();
                    dataGridView1.Rows[i].Cells[0] = DGVC_Id;

                    DataGridViewCell DGVC_date = new DataGridViewTextBoxCell();
                    DGVC_date.Value = dr_Get_Info["Batch_Num"].ToString().Substring(6, 2) + "/" + dr_Get_Info["Batch_Num"].ToString().Substring(4, 2) + "/" + dr_Get_Info["Batch_Num"].ToString().Substring(0, 4);
                    dataGridView1.Rows[i].Cells[1] = DGVC_date;

                    DataGridViewCell DGVC_Batch = new DataGridViewTextBoxCell();
                    DGVC_Batch.Value = dr_Get_Info["Batch_Num"].ToString().Substring(8);
                    dataGridView1.Rows[i].Cells[2] = DGVC_Batch;

                    DataGridViewCell DGVC_Quantity = new DataGridViewTextBoxCell();
                    DGVC_Quantity.Value = dr_Get_Info["Quantity"].ToString();
                    dataGridView1.Rows[i].Cells[3] = DGVC_Quantity;
                }
                ColumnSize();
            }
        }

        private void SizeAllColumns(Object sender, EventArgs e)
        {
            // Move the form away from centre screen to allow for the ridiculously long comments
            // which hide the new ComboBox dropdown info off screen - BN 29/11/2015
            //this.Left = 100;
            //this.Top = 100;

            ColumnSize();

            // multiColumnComboBoxOrders gets populated accidentally at startup - bit confusing - BN 29/11/2015
            multiColumnComboBoxOrders.Text = "";

            // Treeview gets populated accidentally at startup - bit confusing - BN 29/11/2015
            treeViewTipOrder.Nodes.Clear();
        }

        private void ColumnSize()
        {
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = 40;
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int int_new_value = 0;
            if (e.ColumnIndex == 4)
            {
                try
                {
                    int_new_value = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue.ToString());
                }
                catch (Exception ex)
                {
                    logger.Log(LogLevel.Debug, ex.Message);
                }

                if (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString()) < int_new_value)
                {
                    lbl_message.Text = "Not Enough left in this Batch on this Pallet";
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lbl_message.Text = "";
                }
            }
            int_new_value = 0;
            for (int i = 0; i < Convert.ToInt32(dataGridView1.Rows.Count.ToString()); i++)
            {
                try
                {
                    int_new_value = int_new_value + Convert.ToInt32(dataGridView1.Rows[i].Cells["Required"].Value.ToString());
                }
                catch
                {
                    int_new_value = int_new_value + 0;
                }
            }
            txt_total.Text = Convert.ToString(int_new_value);
        }

        public void btn_Add_Click(object sender, EventArgs e)
        {
            string str_msg = "";
            int int_req_Amount = 0;
            int int_pallet_total = 0;
            int int_result = 0;
            DialogResult DLR_Message = new DialogResult();

            if (multiColumnComboBoxOrders.SelectedValue == null)
            {
                str_msg = str_msg + "Invalid Order. Please Select an Order Number from the dropdown list.";
                lbl_message.Text = str_msg;
                lbl_message.ForeColor = System.Drawing.Color.Red;
                logger.Log(LogLevel.Debug, "Invalid Order. Please Select an Order Number from the dropdown list.");
            }
            if (barcode1.BarcodeValue.Length == 0)
            {
                str_msg = str_msg + "Invalid Pallet. Please scan a barcode from a valid pallet.";
                lbl_message.Text = str_msg;
                lbl_message.ForeColor = System.Drawing.Color.Red;
                logger.Log(LogLevel.Debug, "Invalid Pallet. Please scan a barcode from a valid pallet.");
            }
            else
            {
                DataSet ds = PF.Data.AccessLayer.PF_Pallet_Details.Check_Material(barcode1.BarcodeValue.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DLR_Message = MessageBox.Show("Do you really want to place unboxed product on this Order?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
            }
            if (str_msg.Length == 0 && DLR_Message != DialogResult.No)
            {
                for (int i = 0; i < Convert.ToInt32(dataGridView1.Rows.Count.ToString()); i++)
                {
                    try
                    {
                        int_req_Amount = int_req_Amount + Convert.ToInt32(dataGridView1.Rows[i].Cells["Required"].Value.ToString());
                    }
                    catch
                    {
                        int_req_Amount = int_req_Amount + 0;
                    }
                    try
                    {
                        int_pallet_total = int_pallet_total + Convert.ToInt32(dataGridView1.Rows[i].Cells["Qty"].Value.ToString());
                    }
                    catch
                    {
                        int_pallet_total = int_pallet_total + 0;
                    }
                }

                //find out if the order is waiting on payment
                DataSet ds = PF.Data.AccessLayer.PF_Orders.Get_Info(Convert.ToInt32(multiColumnComboBoxOrders.SelectedValue.ToString()));
                DataRow dr;
                bool bol_hold_for_payment = false;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dr = ds.Tables[0].Rows[i];
                    bol_hold_for_payment = Convert.ToBoolean(dr["Hold_For_Payment"].ToString());
                }
                ds.Dispose();

                // if the required amount is zero then we are dispatching the complete pallet
                // if the required amount is the same as the total on the pallet then we are dispatching the complete pallet.
                // This is done by updating the Pallet record to have an Order_Id, and to active_ind to be set to false.
                if (int_req_Amount == 0 || int_pallet_total == int_req_Amount)
                {
                    if (bol_hold_for_payment == false)
                    {
                        // BN 21/01/2015 - Will crash here if rowcount = 0.
                        // Ideally, the Tip to Order button would be greyed out until there was actually at least one order ready
                        if (dataGridView1.Rows.Count > 0)
                        {
                            int_result = PF.Data.AccessLayer.PF_Pallet.Update_Order_Id(Convert.ToInt32(dataGridView1.Rows[0].Cells["Pallet_Id"].Value.ToString()), Convert.ToInt32(multiColumnComboBoxOrders.SelectedValue.ToString()), int_Current_User_Id);
                        }
                        else
                        {
                            MessageBox.Show("DataGrid is empty", "Problem", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                    }
                    else
                    {
                        int_result = PF.Data.AccessLayer.PF_Pallet.Update__Hold_Order_Id(Convert.ToInt32(dataGridView1.Rows[0].Cells["Pallet_Id"].Value.ToString()), Convert.ToInt32(multiColumnComboBoxOrders.SelectedValue.ToString()), int_Current_User_Id);
                    }
                }

                #region --------- create a new pallet and shift the require quantities to it, and adjust the old pallet quantities -------------------------

                else
                {
                    DataSet ds_Get_info;
                    DataRow dr_Get_info;

                    int int_Pallet_Id = 0;

                    #region Create a new pallet if a second barcode is not scanned

                    if (cmb_barcode2.SelectedIndex == -1)
                    {
                        //create a new pallet if a second barcode is not scanned
                        int_Pallet_Id = PF.Common.Code.General.int_max_user_id("PF_Pallet");

                        PF.Common.Code.Barcode.Barcode_Create(int_Pallet_Id);
                        string str_barcode = PF.Common.Code.Barcode.Barcode_Num.ToString();

                        //get old Pallet Details
                        ds_Get_info = PF.Data.AccessLayer.PF_Pallet.Get_Info_For_Pallet_Id(Convert.ToInt32(dataGridView1.Rows[0].Cells["Pallet_Id"].Value.ToString()));

                        for (int i = 0; i < Convert.ToInt32(ds_Get_info.Tables[0].Rows.Count.ToString()); i++)
                        {
                            dr_Get_info = ds_Get_info.Tables[0].Rows[i];

                            int int_loc_id = 0;
                            try
                            {
                                int_loc_id = Convert.ToInt32(dr_Get_info["Location_Id"].ToString());
                            }
                            catch
                            {
                                int_loc_id = 456;
                            }

                            if (bol_hold_for_payment == false)
                            {
                                int_result = PF.Data.AccessLayer.PF_Pallet.Insert(int_Pallet_Id,
                                    Convert.ToInt32(dr_Get_info["PalletType_Id"].ToString()),
                                    str_barcode,
                                    int_loc_id,
                                    Convert.ToInt32(dr_Get_info["Trader_Id"].ToString()),
                                    Convert.ToInt32(multiColumnComboBoxOrders.SelectedValue.ToString()),
                                    false,
                                    PF.Common.Code.General.Get_Season(),
                                    int_Current_User_Id);
                            }
                            else
                            {
                                int_result = PF.Data.AccessLayer.PF_Pallet.Insert_On_Hold(int_Pallet_Id,
                                    Convert.ToInt32(dr_Get_info["PalletType_Id"].ToString()),
                                    str_barcode,
                                    int_loc_id,
                                    Convert.ToInt32(dr_Get_info["Trader_Id"].ToString()),
                                    Convert.ToInt32(multiColumnComboBoxOrders.SelectedValue.ToString()),
                                    false,
                                    PF.Common.Code.General.Get_Season(),
                                    int_Current_User_Id);
                            }
                            //store old pallet_id on the new pallet_id so it can be traced.
                            int_result = int_result + PF.Data.AccessLayer.PF_Pallet.Update_Old_Pallet_Id(int_Pallet_Id, Convert.ToInt32(dataGridView1.Rows[0].Cells["Pallet_Id"].Value.ToString()), int_Current_User_Id);
                        }
                        ds_Get_info.Dispose();
                    }

                    #endregion Create a new pallet if a second barcode is not scanned

                    #region Use the pallet linked to the second barcode that has been scanned

                    else
                    {
                        //use the pallet linked to the second barcode that has been scanned
                        DataSet ds_Get_Info2 = PF.Data.AccessLayer.PF_Pallet.Get_Info_for_Barcode(cmb_barcode2.SelectedValue.ToString());
                        DataRow dr_Get_Info2;
                        for (int i = 0; i < ds_Get_Info2.Tables[0].Rows.Count; i++)
                        {
                            dr_Get_Info2 = ds_Get_Info2.Tables[0].Rows[i];
                            int_Pallet_Id = Convert.ToInt32(dr_Get_Info2["Pallet_Id"].ToString());
                        }
                        ds_Get_Info2.Dispose();
                    }

                    #endregion Use the pallet linked to the second barcode that has been scanned

                    ds_Get_info = PF.Data.AccessLayer.PF_Pallet_Details.Get_Info_For_Pallet(Convert.ToInt32(dataGridView1.Rows[0].Cells["Pallet_Id"].Value.ToString()));
                    for (int i = 0; i < Convert.ToInt32(ds_Get_info.Tables[0].Rows.Count.ToString()); i++)
                    {
                        dr_Get_info = ds_Get_info.Tables[0].Rows[i];

                        for (int j = 0; j < Convert.ToInt32(dataGridView1.Rows.Count.ToString()); j++)
                        {
                            string dgv_Batch_Num = "";
                            dgv_Batch_Num = Convert.ToString(dataGridView1.Rows[j].Cells["Date"].Value.ToString().Substring(6, 4) + dataGridView1.Rows[j].Cells["Date"].Value.ToString().Substring(3, 2) + dataGridView1.Rows[j].Cells["Date"].Value.ToString().Substring(0, 2) + dataGridView1.Rows[j].Cells["Batch"].Value.ToString());

                            if (dgv_Batch_Num == dr_Get_info["Batch_Num"].ToString() && dataGridView1.Rows[j].Cells["Required"].Value != null)
                            {
                                int_result = PF.Data.AccessLayer.PF_Pallet_Details.Insert(PF.Common.Code.General.int_max_user_id("PF_Pallet_Details"), int_Pallet_Id, Convert.ToInt32(dr_Get_info["Batch_Num"].ToString()),
                                                                                                     Convert.ToInt32(dr_Get_info["Material_Id"].ToString()), Convert.ToInt32(dr_Get_info["Work_Order_Id"].ToString()), Convert.ToDecimal(dataGridView1.Rows[j].Cells["Required"].Value.ToString()), int_Current_User_Id);
                                //if (int_result > 0)
                                //{
                                int_result = PF.Data.AccessLayer.PF_Pallet_Details.Update(Convert.ToInt32(dr_Get_info["PalletDetails_Id"].ToString()), Convert.ToInt32(dr_Get_info["Batch_Num"].ToString()), Convert.ToInt32(dr_Get_info["Material_Id"].ToString()), Convert.ToDecimal(dr_Get_info["Quantity"].ToString()) - Convert.ToDecimal(dataGridView1.Rows[j].Cells["Required"].Value.ToString()), int_Current_User_Id);
                                //}

                                // Calculate Gross weights.

                                //1. get weight from material number
                                DataSet ds_weight = PF.Data.AccessLayer.CM_Material.Get_Info(Convert.ToInt32(dr_Get_info["Material_Id"].ToString()));
                                DataRow dr_weight;
                                decimal dec_material_weight = 0;

                                for (int k = 0; k < ds_weight.Tables[0].Rows.Count; k++)
                                {
                                    dr_weight = ds_weight.Tables[0].Rows[k];
                                    dec_material_weight = Convert.ToDecimal(dr_weight["Weight"].ToString());
                                }
                                ds_weight.Dispose();

                                //2 calculate the weight to adjust by
                                decimal dec_adj_weight = Convert.ToDecimal(dataGridView1.Rows[j].Cells["Required"].Value.ToString()) * dec_material_weight;

                                //3.adjust gross weight of old pallet.
                                DataSet ds_old_weight = null;
                                DataRow dr_old_weight;
                                decimal dec_old_weight = 0;

                                ds_old_weight = PF.Data.AccessLayer.PF_Pallet.Get_Info_For_Pallet_Id(Convert.ToInt32(dataGridView1.Rows[0].Cells["Pallet_Id"].Value.ToString()));

                                for (int k = 0; k < ds_old_weight.Tables[0].Rows.Count; k++)
                                {
                                    dr_old_weight = ds_old_weight.Tables[0].Rows[k];
                                    dec_old_weight = Convert.ToDecimal(dr_old_weight["Weight_Gross"].ToString());
                                }
                                PF.Data.AccessLayer.PF_Pallet.Update_Gross_Weight(Convert.ToInt32(dataGridView1.Rows[0].Cells["Pallet_Id"].Value.ToString()), dec_old_weight - dec_adj_weight, int_Current_User_Id);

                                //4.adjust gross weight of new pallet.
                                ds_old_weight = PF.Data.AccessLayer.PF_Pallet.Get_Info_For_Pallet_Id(int_Pallet_Id);
                                dec_old_weight = 0;

                                for (int k = 0; k < ds_old_weight.Tables[0].Rows.Count; k++)
                                {
                                    dr_old_weight = ds_old_weight.Tables[0].Rows[k];
                                    try
                                    {
                                        dec_old_weight = Convert.ToDecimal(dr_old_weight["Weight_Gross"].ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        logger.Log(LogLevel.Debug, ex.Message);
                                    }
                                }
                                PF.Data.AccessLayer.PF_Pallet.Update_Gross_Weight(int_Pallet_Id, dec_old_weight + dec_adj_weight, int_Current_User_Id);
                            }
                        }
                    }
                }

                #endregion --------- create a new pallet and shift the require quantities to it, and adjust the old pallet quantities -------------------------
            }
            if (int_result >= 0)
            {
                lbl_message.Text = "Pallet has been added to the Order";
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                logger.Log(LogLevel.Info, "Pallet has been added to the Order.");
            }
            else
            {
                lbl_message.Text = "Pallet has NOT been added to the Order";
                lbl_message.ForeColor = System.Drawing.Color.Red;
                logger.Log(LogLevel.Debug, "Pallet has NOT been added to the Order.");
            }

            try
            {
                populate_barcode_combo();
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //this is to change the package from a plastic bag to a box example only
            if (multiColumnComboBoxOrders.SelectedValue == null)
            {
                lbl_message.Text = "Invalid Order. Please Select an Order Number from the dropdown list.";
                lbl_message.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void txt_Barcode_TextChanged(object sender, EventArgs e)
        {
            lbl_message.Text = "";
            if (barcode1.BarcodeValue != null || barcode1.BarcodeValue != "")
            {
                populate_dataGridView1();
            }
        }

        private void txt_Barcode2_Leave(object sender, EventArgs e)
        {
            if (cmb_barcode2.SelectedIndex > -1)
            {
                btn_Add.Text = "Transfer From/To";
            }
            else
            {
                btn_Add.Text = "Tip to Order";
            }
        }

        public void btn_Close_Click(object sender, EventArgs e)
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
        private void Tipping_Onto_Order_KeyDown(object sender, KeyEventArgs e)
        {
            // Note that the KeyPreview property must be set to true for this to work
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        #endregion Methods to log UI events to the CSV file. BN 29/01/2015

        private void multiColumnComboBoxOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            int currentIndex = multiColumnComboBoxOrders.SelectedIndex;

            treeViewTipOrder.Nodes.Clear();
            TreeNode tnRoot = new TreeNode();

            tnRoot.Text = "Order";    // 

            //for (int i = 0; i < dt_Custom.Columns.Count; i++)
            //{
                for (int iCell = 0; iCell < dt_Custom.Rows[currentIndex].ItemArray.Length; iCell++)
                {
                    TreeNode tn = new TreeNode(dt_Custom.Columns[iCell].ColumnName + ": " + dt_Custom.Rows[currentIndex].ItemArray[iCell].ToString());
                    tnRoot.Nodes.Add(tn);
                }
            //}
            treeViewTipOrder.Nodes.Add(tnRoot);    // order number as root text
            treeViewTipOrder.ExpandAll();
        }

        private void multiColumnComboBoxOrders_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt32(multiColumnComboBoxOrders.SelectedValue.ToString());
                populate_barcode_combo();
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message);
            }
        }

        private void buttonPasteThePalletBarcode_Click(object sender, EventArgs e)
        {
            string barcode = Clipboard.GetText();
            barcode1.BarcodeValue = barcode;

        }

        private void buttonClearTheBarcodeText_Click(object sender, EventArgs e)
        {
            barcode1.BarcodeValue = string.Empty;
        }
    }
}