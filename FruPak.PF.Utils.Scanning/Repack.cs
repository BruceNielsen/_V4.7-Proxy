using NLog;
using System;
using System.Data;
using System.Windows.Forms;

namespace FruPak.PF.Utils.Scanning
{
    public partial class Repack : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        #region AutoResize Class Variable Declaration

        // Lives immediately after the public partial class declaration
        //private FruPak.PF.Common.Code.AutoResize _form_resize;

        #endregion AutoResize Class Variable Declaration

        private static bool bol_write_access;
        private static int int_Current_User_Id = 0;
        private static int int_Season = 0;

        public Repack(int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();

            #region AutoResize EventHandlers - Lives immediately after InitializeComponent(); in the public Form declaration

            //_form_resize = new FruPak.PF.Common.Code.AutoResize(this);

            //this.Load += _Load;
            //this.Resize += _Resize;

            #endregion AutoResize EventHandlers - Lives immediately after InitializeComponent(); in the public Form declaration

            int_Current_User_Id = int_C_User_id;

            //restrict access
            bol_write_access = bol_w_a;
            btn_Update.Enabled = bol_w_a;
            populate_comboBoxes();
            int_Season = FruPak.PF.Common.Code.General.Get_Season();

            txt_barcode_from.Text = "00394210096400000000"; // Sel has asked for this to be left alone 10/02/2015

            AddColumnsProgrammatically();

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

        private void populate_comboBoxes()
        {
            DataSet ds_get_info = FruPak.PF.Data.AccessLayer.CM_Pallet_Type.Get_Info();
            cmb_Pallet_Type.DataSource = ds_get_info.Tables[0];
            cmb_Pallet_Type.DisplayMember = "Description";
            cmb_Pallet_Type.ValueMember = "PalletType_Id";
            cmb_Pallet_Type.Text = null;
            ds_get_info.Dispose();
        }

        private void populate_combobox2()
        {
            try
            {
                DataSet ds_get_info = FruPak.PF.Data.AccessLayer.CM_Material.Get_Repack_List(txt_barcode_from.Text);
                cmb_Material.DataSource = ds_get_info.Tables[0];
                cmb_Material.DisplayMember = "Combined";
                cmb_Material.ValueMember = "Material_id";
                ds_get_info.Dispose();
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message);
            }
        }

        private void txt_barcode_Leave(object sender, EventArgs e)
        {
            populate_combobox2();
            populate_dataGridView1();
            SizeColumns();
        }

        private void AddColumnsProgrammatically()
        {
            var col0 = new DataGridViewTextBoxColumn();
            var col1 = new DataGridViewTextBoxColumn();
            var col2 = new DataGridViewTextBoxColumn();
            var col3 = new DataGridViewTextBoxColumn();
            var col4 = new DataGridViewTextBoxColumn();
            var col5 = new DataGridViewTextBoxColumn();

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

            col5.HeaderText = "PalletDetails_Id";
            col5.Name = "PalletDetails_Id";
            col5.ReadOnly = false;
            col5.Visible = false;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2, col3, col4, col5 });
        }

        private void populate_dataGridView1()
        {
            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();

            DataSet ds_Get_Info;
            DataRow dr_Get_Info;
            if (txt_barcode_from.TextLength > 0)
            {
                ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Pallet.Get_Info_for_Barcode(txt_barcode_from.Text);
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

                    DataGridViewCell DGVC_col5 = new DataGridViewTextBoxCell();
                    DGVC_col5.Value = dr_Get_Info["PalletDetails_Id"].ToString();
                    dataGridView1.Rows[i].Cells[5] = DGVC_col5;
                }
                SizeColumns();
            }
        }

        private void SizeAllColumns(Object sender, EventArgs e)
        {
            SizeColumns();

            #region AutoResize - Appears in Load and Resize methods

            //_form_resize._get_initial_size();

            #endregion AutoResize - Appears in Load and Resize methods
        }

        private void SizeColumns()
        {
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = 40;
            }
        }

        /// <summary>
        /// Validates the barcode.
        /// </summary>
        /// <returns>True if barcode is found</returns>
        private bool Validate_from_Barcode()
        {
            bool return_code;
            DataSet ds_From_barcode = FruPak.PF.Data.AccessLayer.PF_Pallet.Get_Pallet_Id_From_Baracode(txt_barcode_from.Text);
            if (ds_From_barcode.Tables[0].Rows.Count > 0)
            {
                return_code = true;
            }
            else
            {
                return_code = false;
            }
            return return_code;
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            int int_result = -1;
            int int_req_Amount = 0;
            if (txt_barcode_from.TextLength == 0)
            {
                lbl_message.Text = "Invalid Barcode - Length is zero.";
                lbl_message.ForeColor = System.Drawing.Color.Red;
                logger.Log(LogLevel.Debug, "Invalid Barcode - Length is zero.");
            }
            else if (Validate_from_Barcode() == false)
            {
                lbl_message.Text = "Invalid Barcode - Validation failed.";
                lbl_message.ForeColor = System.Drawing.Color.Red;
                logger.Log(LogLevel.Debug, "Invalid Barcode - Validation failed.");
            }
            else if (cmb_Pallet_Type.SelectedIndex == -1)
            {
                lbl_message.Text = "Invalid Pallet Type.";
                lbl_message.ForeColor = System.Drawing.Color.Red;
                logger.Log(LogLevel.Debug, "Invalid Pallet Type.");
            }
            else if (cmb_Material.SelectedIndex == -1)
            {
                lbl_message.Text = "Invalid Material Number.";
                lbl_message.ForeColor = System.Drawing.Color.Red;
                logger.Log(LogLevel.Debug, "Invalid Material Number.");
            }
            else
            {
                #region Get total number being repacked - This is where I made the change for Sel 11-02-2015

                for (int i = 0; i < Convert.ToInt32(dataGridView1.Rows.Count.ToString()); i++)
                {
                    // This is where it was failing, corrupting the database - BN
                    try
                    {
                        int_req_Amount = int_req_Amount + Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value.ToString());
                    }
                    catch
                    {
                        int_req_Amount = int_req_Amount + 0;

                        // BN 10-02-2015 - Am trying this as an experiment to see if it makes any difference.
                        // The system was designed to allow a Required value of zero, signifying that
                        // we are repacking the complete amount, but for some reason it's failing.
                        // My thought is that an empty or null value is being read from the datagrid, and is not actually what was intended.

                        dataGridView1.Rows[i].Cells[4].Value = 0;   // Didn't work. Sel just wants it to stop and show the red error message

                        // Bypass the rest of the logic and jump straight to the end - BN 11-02-2015
                        lbl_message.Text = "Pallet Details failed to update - Req'd amount is zero";
                        lbl_message.ForeColor = System.Drawing.Color.Red;
                        logger.Log(LogLevel.Debug, "Pallet Details failed to update - Required amount is zero.");

                        // Will jump to the end of the method without making any changes
                        return;
                    }
                }

                #endregion Get total number being repacked - This is where I made the change for Sel 11-02-2015

                #region Create a new pallet

                if (txt_Barcode_To.TextLength == 0)
                {
                    #region ------- Create New Pallet -----------------

                    //create barcode

                    // BN Notes:
                    // Go get 4-digit number to be used as part of the barcode
                    int int_Pallet_Id = FruPak.PF.Common.Code.General.int_max_user_id("PF_Pallet");

                    // Create a barcode using the previously obtained number - looks like it's tacking on a check digit as well
                    FruPak.PF.Common.Code.Barcode.Barcode_Create(int_Pallet_Id);

                    // Convert the number into a string, preserving the leading zeroes
                    string str_barcode = FruPak.PF.Common.Code.Barcode.Barcode_Num.ToString();

                    // Finally, assign the 'Barcode To' text to the barcode string.
                    txt_Barcode_To.Text = str_barcode;

                    // BN 11-02-2015 Make a copy of the last entered barcode to for Sel
                    this.labelBarcodeToBeCopied.Text = str_barcode;
                    this.labelBarcodeForSel.Text = str_barcode;

                    //get old Pallet Details
                    DataSet ds_Get_info;
                    DataRow dr_Get_info;

                    try
                    {
                        // This call will fail if the barcode does not exist.
                        ds_Get_info = FruPak.PF.Data.AccessLayer.PF_Pallet.Get_Info_For_Pallet_Id(Convert.ToInt32(dataGridView1.Rows[0].Cells["Pallet_Id"].Value.ToString()));

                        //create new pallet
                        for (int i = 0; i < Convert.ToInt32(ds_Get_info.Tables[0].Rows.Count.ToString()); i++)
                        {
                            dr_Get_info = ds_Get_info.Tables[0].Rows[i];

                            int_result = FruPak.PF.Data.AccessLayer.PF_Pallet.Insert(
                                int_Pallet_Id,
                                Convert.ToInt32(cmb_Pallet_Type.SelectedValue.ToString()),
                                str_barcode,
                                Convert.ToInt32(dr_Get_info["Location_Id"].ToString()),
                                Convert.ToInt32(dr_Get_info["Trader_Id"].ToString()),
                                true,
                                Convert.ToInt32(dr_Get_info["Season"].ToString()),
                                int_Current_User_Id);

                            //store old pallet_id on the new pallet_id so it can be traced.
                            int_result = int_result + FruPak.PF.Data.AccessLayer.PF_Pallet.Update_Old_Pallet_Id(
                                int_Pallet_Id,
                                Convert.ToInt32(dataGridView1.Rows[0].Cells["Pallet_Id"].Value.ToString()),
                                int_Current_User_Id);

                            // Debugging  3-03-2015
                            foreach (var item in dr_Get_info.ItemArray)
                            {
                                Console.WriteLine("Repack - DR: " + item);
                            }
                        }
                        ds_Get_info.Dispose();

                        for (int i = -0; i < Convert.ToInt32(dataGridView1.Rows.Count.ToString()); i++)
                        {
                            DataSet ds_old = FruPak.PF.Data.AccessLayer.PF_Pallet_Details.Get_Info(
                                Convert.ToInt32(dataGridView1.Rows[i].Cells["PalletDetails_Id"].Value.ToString()));
                            DataRow dr_old;
                            for (int j = 0; j < ds_old.Tables[0].Rows.Count; j++)
                            {
                                dr_old = ds_old.Tables[0].Rows[j];
                                //if the required amount is zero then we are repacking the complete amount
                                if (int_req_Amount == 0)
                                {
                                    int_result = FruPak.PF.Data.AccessLayer.PF_Pallet_Details.Insert(
                                        FruPak.PF.Common.Code.General.int_max_user_id("PF_Pallet_Details"),
                                        int_Pallet_Id,
                                        Convert.ToInt32(dr_old["Batch_Num"].ToString()),
                                        Convert.ToInt32(cmb_Material.SelectedValue.ToString()),
                                        Convert.ToInt32(dr_old["Work_Order_Id"].ToString()),
                                        Convert.ToDecimal(dr_old["Quantity"].ToString()),
                                        int_Current_User_Id);

                                    #region Make old pallet inactive

                                    try
                                    {
                                        FruPak.PF.Data.AccessLayer.PF_Pallet.Update_Active_status(
                                            Convert.ToInt32(dataGridView1.Rows[i].Cells["Pallet_Id"].Value.ToString()),
                                            false,
                                            int_Current_User_Id);
                                    }
                                    catch (Exception ex)
                                    {
                                        logger.Log(LogLevel.Debug, ex.Message);
                                    }

                                    #endregion Make old pallet inactive

                                    #region Copy gross weight from old pallet to new pallet and set the tare weight to zero

                                    //2. get the current gross weight that has been loaded
                                    decimal dec_current_gross = FruPak.PF.Common.Code.General.Current_Gross_Weight(
                                        int_Pallet_Id);

                                    decimal dec_current_tare = FruPak.PF.Common.Code.General.Current_Tare_Weight(
                                        Convert.ToInt32(dataGridView1.Rows[i].Cells["Pallet_Id"].Value.ToString()));

                                    decimal dec_old_gross = FruPak.PF.Common.Code.General.Current_Gross_Weight(
                                        Convert.ToInt32(dataGridView1.Rows[i].Cells["Pallet_Id"].Value.ToString()));

                                    try
                                    {
                                        FruPak.PF.Data.AccessLayer.PF_Pallet.Update_Gross_Weight(int_Pallet_Id, dec_current_gross + (dec_old_gross - dec_current_tare), int_Current_User_Id);
                                        FruPak.PF.Data.AccessLayer.PF_Pallet.Update_Tare_Weight(int_Pallet_Id, Convert.ToDecimal("0.00"), int_Current_User_Id);
                                    }
                                    catch (Exception ex)
                                    {
                                        logger.Log(LogLevel.Debug, ex.Message);
                                    }

                                    #endregion Copy gross weight from old pallet to new pallet and set the tare weight to zero
                                }
                                else
                                {
                                    #region Calculate Weight_Gross

                                    //1. get weight from material number
                                    decimal dec_material_weight = FruPak.PF.Common.Code.General.Material_Weight(Convert.ToInt32(cmb_Material.SelectedValue.ToString()));

                                    //2. get the current gross weight that has been loaded
                                    decimal dec_current_gross = FruPak.PF.Common.Code.General.Current_Gross_Weight(int_Pallet_Id);
                                    try
                                    {
                                        FruPak.PF.Data.AccessLayer.PF_Pallet.Update_Gross_Weight(int_Pallet_Id, dec_current_gross + (dec_material_weight * Convert.ToDecimal(dataGridView1.Rows[i].Cells["Required"].Value.ToString())), int_Current_User_Id);
                                        FruPak.PF.Data.AccessLayer.PF_Pallet.Update_Tare_Weight(int_Pallet_Id, Convert.ToDecimal("0.00"), int_Current_User_Id);
                                    }
                                    catch (Exception ex)
                                    {
                                        logger.Log(LogLevel.Debug, ex.Message);
                                    }

                                    int_result = FruPak.PF.Data.AccessLayer.PF_Pallet_Details.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Pallet_Details"), int_Pallet_Id, Convert.ToInt32(dr_old["Batch_Num"].ToString()),
                                        Convert.ToInt32(cmb_Material.SelectedValue.ToString()), Convert.ToInt32(dr_old["Work_Order_Id"].ToString()), Convert.ToDecimal(dataGridView1.Rows[i].Cells["Required"].Value.ToString()), int_Current_User_Id);
                                    //update number transferred from old pallet.
                                    int_result = FruPak.PF.Data.AccessLayer.PF_Pallet_Details.Update(Convert.ToInt32(dr_old["PalletDetails_Id"].ToString()), Convert.ToInt32(dr_old["Batch_Num"].ToString()),
                                        Convert.ToInt32(dr_old["Material_Id"].ToString()), Convert.ToDecimal(dr_old["Quantity"].ToString()) - Convert.ToDecimal(dataGridView1.Rows[i].Cells["Required"].Value.ToString()), int_Current_User_Id);

                                    #endregion Calculate Weight_Gross
                                }

                                //adjust stock numbers
                                Stock_Adjust(Convert.ToInt32(dr_old["Material_Id"].ToString()), int_req_Amount);
                            }
                            ds_old.Dispose();
                        }

                        #endregion ------- Create New Pallet -----------------
                    }
                    catch (Exception ex)
                    {
                        {
                            logger.Log(LogLevel.Debug, ex.Message);
                            MessageBox.Show(ex.Message, "Repack: Barcode does not exist.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                #endregion Create a new pallet

                #region Use existing pallet

                else
                {
                    #region ------- Use current Pallet -----------------

                    //get pallet ID
                    DataSet ds_to_Pallet = FruPak.PF.Data.AccessLayer.PF_Pallet.Get_Pallet_Id_From_Baracode(txt_Barcode_To.Text);
                    DataRow dr_to_Pallet;
                    int int_to_Pallet_Id = 0;
                    for (int i = 0; i < ds_to_Pallet.Tables[0].Rows.Count; i++)
                    {
                        dr_to_Pallet = ds_to_Pallet.Tables[0].Rows[i];
                        int_to_Pallet_Id = Convert.ToInt32(dr_to_Pallet["Pallet_id"].ToString());
                    }

                    for (int i = 0; i < Convert.ToInt32(dataGridView1.Rows.Count.ToString()); i++)
                    {
                        DataSet ds_old = FruPak.PF.Data.AccessLayer.PF_Pallet_Details.Get_Info(Convert.ToInt32(dataGridView1.Rows[i].Cells["PalletDetails_Id"].Value.ToString()));
                        DataRow dr_old;
                        for (int j = 0; j < ds_old.Tables[0].Rows.Count; j++)
                        {
                            dr_old = ds_old.Tables[0].Rows[j];
                            //if the require amount is zero then we are repacking the complete amount
                            if (int_req_Amount == 0)
                            {
                                int_result = FruPak.PF.Data.AccessLayer.PF_Pallet_Details.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Pallet_Details"), int_to_Pallet_Id, Convert.ToInt32(dr_old["Batch_Num"].ToString()),
                                                                                                    Convert.ToInt32(cmb_Material.SelectedValue.ToString()), Convert.ToInt32(dr_old["Work_Order_Id"].ToString()), Convert.ToDecimal(dr_old["Quantity"].ToString()), int_Current_User_Id);
                                //make old pallet inactive
                                FruPak.PF.Data.AccessLayer.PF_Pallet.Update_Active_status(Convert.ToInt32(dataGridView1.Rows[i].Cells["Pallet_Id"].Value.ToString()), false, int_Current_User_Id);

                                //copy gross weight from old pallet to new pallet and set the tate weight to zero

                                //2. get the current gross weight that has been loaded
                                decimal dec_current_gross = FruPak.PF.Common.Code.General.Current_Gross_Weight(int_to_Pallet_Id);
                                decimal dec_current_tare = FruPak.PF.Common.Code.General.Current_Tare_Weight(Convert.ToInt32(dataGridView1.Rows[i].Cells["Pallet_Id"].Value.ToString()));
                                decimal dec_old_gross = FruPak.PF.Common.Code.General.Current_Gross_Weight(Convert.ToInt32(dataGridView1.Rows[i].Cells["Pallet_Id"].Value.ToString()));

                                try
                                {
                                    FruPak.PF.Data.AccessLayer.PF_Pallet.Update_Gross_Weight(int_to_Pallet_Id, dec_current_gross + (dec_old_gross - dec_current_tare), int_Current_User_Id);
                                    FruPak.PF.Data.AccessLayer.PF_Pallet.Update_Tare_Weight(int_to_Pallet_Id, Convert.ToDecimal("0.00"), int_Current_User_Id);
                                }
                                catch (Exception ex)
                                {
                                    logger.Log(LogLevel.Debug, ex.Message);
                                }
                            }
                            else
                            {
                                int_result = FruPak.PF.Data.AccessLayer.PF_Pallet_Details.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Pallet_Details"), int_to_Pallet_Id, Convert.ToInt32(dr_old["Batch_Num"].ToString()),
                                    Convert.ToInt32(cmb_Material.SelectedValue.ToString()), Convert.ToInt32(dr_old["Work_Order_Id"].ToString()), Convert.ToDecimal(dataGridView1.Rows[i].Cells["Required"].Value.ToString()), int_Current_User_Id);

                                //Calculate Weight_Gross

                                //1. get weight from material number
                                decimal dec_material_weight = FruPak.PF.Common.Code.General.Material_Weight(Convert.ToInt32(cmb_Material.SelectedValue.ToString()));

                                //2. get the current gross weight that has been loaded
                                decimal dec_current_gross = FruPak.PF.Common.Code.General.Current_Gross_Weight(int_to_Pallet_Id);

                                try
                                {
                                    FruPak.PF.Data.AccessLayer.PF_Pallet.Update_Gross_Weight(int_to_Pallet_Id, dec_current_gross + (dec_material_weight * Convert.ToDecimal(dataGridView1.Rows[i].Cells["Required"].Value.ToString())), int_Current_User_Id);
                                    FruPak.PF.Data.AccessLayer.PF_Pallet.Update_Tare_Weight(int_to_Pallet_Id, Convert.ToDecimal("0.00"), int_Current_User_Id);
                                }
                                catch (Exception ex)
                                {
                                    logger.Log(LogLevel.Debug, ex.Message);
                                }

                                //update number transferred from old pallet.
                                int_result = FruPak.PF.Data.AccessLayer.PF_Pallet_Details.Update(Convert.ToInt32(dr_old["PalletDetails_Id"].ToString()), Convert.ToInt32(dr_old["Batch_Num"].ToString()),
                                    Convert.ToInt32(dr_old["Material_Id"].ToString()), Convert.ToDecimal(dr_old["Quantity"].ToString()) - Convert.ToDecimal(dataGridView1.Rows[i].Cells["Required"].Value.ToString()), int_Current_User_Id);

                                //Calculate Weight_Gross

                                //1. get weight from material number
                                dec_material_weight = FruPak.PF.Common.Code.General.Material_Weight(Convert.ToInt32(dr_old["Material_Id"].ToString()));

                                //2. get the current gross weight that has been loaded
                                dec_current_gross = FruPak.PF.Common.Code.General.Current_Gross_Weight(Convert.ToInt32(dataGridView1.Rows[i].Cells["Pallet_Id"].Value.ToString()));
                                decimal dec_current_tare = FruPak.PF.Common.Code.General.Current_Tare_Weight(Convert.ToInt32(dataGridView1.Rows[i].Cells["Pallet_Id"].Value.ToString()));

                                try
                                {
                                    FruPak.PF.Data.AccessLayer.PF_Pallet.Update_Gross_Weight(Convert.ToInt32(dataGridView1.Rows[i].Cells["Pallet_Id"].Value.ToString()), dec_current_tare + (dec_material_weight * (Convert.ToDecimal(dr_old["Quantity"].ToString()) - Convert.ToDecimal(dataGridView1.Rows[i].Cells["Required"].Value.ToString()))), int_Current_User_Id);
                                }
                                catch (Exception ex)
                                {
                                    logger.Log(LogLevel.Debug, ex.Message);
                                }
                            }
                            //adjust stock numbers
                            Stock_Adjust(Convert.ToInt32(dr_old["Material_Id"].ToString()), int_req_Amount);
                        }
                        ds_old.Dispose();
                    }

                    #endregion ------- Use current Pallet -----------------
                }

                #endregion Use existing pallet

                if (int_result >= 0)
                {
                    lbl_message.Text = "Pallet Details have been updated";
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    logger.Log(LogLevel.Info, "Pallet Details have been updated.");

                    Reset();
                }
                else
                {
                    lbl_message.Text = "Pallet Details failed to update";
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    logger.Log(LogLevel.Debug, "Pallet Details failed to update.");
                }
                txt_Barcode_To.Text = string.Empty;

                cmb_Material.DataSource = null;
            }
        }   // return will jump to here

        //stock adjustment not finished yest
        private void Stock_Adjust(int int_old_Mat_Id, int int_req_Amount)
        {
            //adjust stock levels
            DataSet ds_stock = FruPak.PF.Data.AccessLayer.CM_Material_ST_Product_Relationship.Get_Update_Info(int_old_Mat_Id, Convert.ToInt32(cmb_Material.SelectedValue.ToString()));
            DataRow dr_stock;
            for (int j = 0; j < ds_stock.Tables[0].Rows.Count; j++)
            {
                dr_stock = ds_stock.Tables[0].Rows[j];

                DataSet ds_stock1 = FruPak.PF.Data.AccessLayer.PF_Stock_Item.Get_Info(Convert.ToInt32(dr_stock["Stock_Item_Id"].ToString()));
                DataRow dr_stock1;
                for (int k = 0; k < ds_stock1.Tables[0].Rows.Count; k++)
                {
                    decimal dec_total = 0;
                    dr_stock1 = ds_stock1.Tables[0].Rows[k];
                    dec_total = Convert.ToDecimal(int_req_Amount) * Convert.ToDecimal(dr_stock1["Multiplier"].ToString());

                    FruPak.PF.Data.AccessLayer.PF_Stock_Used.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Stock_Used"), Convert.ToInt32(dr_stock["Stock_Item_Id"].ToString()), int_Season, Convert.ToInt32(dec_total), int_Current_User_Id);
                }
                ds_stock1.Dispose();
            }
            ds_stock.Dispose();
        }

        private void Reset()
        {
            txt_barcode_from.ResetText();
            txt_total.ResetText();
            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();
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

        #endregion Decorate String

        private void buttonCopyBarcodeToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.labelBarcodeForSel.Text);
            this.toolTip1.SetToolTip(this.buttonCopyBarcodeToClipboard, "Copied to Clipboard: " + Environment.NewLine + this.labelBarcodeForSel.Text);
        }

        #endregion Methods to log UI events to the CSV file. BN 29/01/2015

        /// <summary>
        /// Auto-fill datagrid (Sel's request 11-02-2015 BN)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_barcode_from_TextChanged(object sender, EventArgs e)
        {
            lbl_message.Text = "";
            if (txt_barcode_from.Text != null || txt_barcode_from.Text != "")
            {
                populate_dataGridView1();
            }
        }

        /// <summary>
        /// Close the form with the Esc key (Sel request 11-02-2015 BN)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Repack_KeyDown(object sender, KeyEventArgs e)
        {
            // Note that the KeyPreview property must be set to true for this to work
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Copied from Pallet_Transfer.cs BN 11-02-2015
            int int_new_value = 0;

            // Only copied this bit, so does no other validation - BN
            int_new_value = 0;
            for (int i = 0; i < Convert.ToInt32(dataGridView1.Rows.Count.ToString()); i++)
            {
                try
                {
                    int_new_value = int_new_value + Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value.ToString());
                }
                catch
                {
                    int_new_value = int_new_value + 0;
                }
            }
            txt_total.Text = Convert.ToString(int_new_value);
        }

        private void txt_Barcode_To_TextChanged(object sender, EventArgs e)
        {
            this.labelBarcodeToBeCopied.Text = this.txt_Barcode_To.Text;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            this.labelBarcodeToBeCopied.Text = "";
        }

        private void Repack_Resize(object sender, EventArgs e)
        {
            #region AutoResize - Appears in Load and Resize methods

            //_form_resize._resize();

            #endregion AutoResize - Appears in Load and Resize methods
        }
    }
}