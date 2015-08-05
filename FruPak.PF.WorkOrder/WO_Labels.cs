using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using NLog;
using FruPak.PF.CustomSettings;

namespace FruPak.PF.WorkOrder
{
    public partial class WO_Labels : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        // Phantom 17/12/2014 Declare application settings object
        private static PhantomCustomSettings Settings;

        private static bool bol_write_access;
        private static int int_Current_User_Id = 0;
        private static int int_Work_Order_Id = 0;
        private static int int_Trader_Id = 0;
        private static int int_W_Trader_Id = 0;
        private static int int_Pallet_Id = 0;
        private static int int_DVG_Row_id = 0;
        private static int int_DVG_Pallet_Id = 0;
        private static string str_Variety_desc = "";
        private static string str_product_code = "";
        public static bool bol_bypass = false;

        /// <summary>
        /// Parameterless constructor to avoid designer error - Phantom 12/12/2014
        /// refers to Tablet_WO_Labels, which inherits from this class
        /// </summary>
        public WO_Labels()
        {
            #region Load CustomSettings
            #region Get "My Documents" folder
            string path = Application.StartupPath;
            // string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //path = Path.Combine(path, "FruPak_Settings"); // Creates a subfolder

            // Create folder if it doesn't already exist
            //if (!Directory.Exists(path))
            //    Directory.CreateDirectory(path);
            #endregion

            // Initialize settings
            Settings = new PhantomCustomSettings();
            Settings.SettingsPath = Path.Combine(path, "FruPak.Phantom.config");
            Settings.EncryptionKey = "phantomKey";

            if (!File.Exists(Settings.SettingsPath))
            {
                Settings.Save();
                logger.Log(LogLevel.Info, LogCode("Default Phantom Settings file created"));
            }
            // Load settings - Normally in Form_Load
            Settings.Load();

            logger.Log(LogLevel.Info, LogCode("Settings file opened."));

            //local_path = Settings.Path_Local_Path;
            //remote_path = Settings.Path_Remote_Path;
            //path_to_settings = Settings.Path_To_Settings;

            // Save settings - Normally in Form_Closing
            //Settings.Save();

            #endregion

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

        public WO_Labels(int int_wo_Id, int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();

            int_Current_User_Id = int_C_User_id;
            int_Work_Order_Id = int_wo_Id;
            //check if testing or not
            //if (FruPak.PF.Global.Global.bol_Testing == true)
            //{
            //    this.Text = "FruPak Process Factory - " + this.Text + " - Test Environment";
            //}
            //else
            //{
            //    this.Text = "FruPak Process Factory";
            //}

            //populate Work Order Display
            woDisplay1.Work_Order_Id = int_wo_Id;
            woDisplay1.populate();
            DataSet ds_Get_Info;
            DataRow dr_Get_Info;

            ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Fruit_Variety.Get_Info_std(Convert.ToInt32(woDisplay1.Variety_Id.ToString()));
            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                str_Variety_desc = dr_Get_Info["Description"].ToString();
            }
            ds_Get_Info.Dispose();

            //restrict access
            bol_write_access = bol_w_a;
            btn_Add.Enabled = bol_w_a;

            ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Bins.Get_Info_for_Work_Order(int_Work_Order_Id);

            if (bol_bypass == false)
            {
                if (ds_Get_Info.Tables[0].Rows == null || Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()) <= 0)
                {
                    btn_Add.Enabled = false;
                    marqueeLabel1.Text = "NO Bins have been tipped for this Work Order Yet.";
                    marqueeLabel1.ForeColor = System.Drawing.Color.Red;
                }
            }

            Get_Product_code();
            populate_Combobox();

            AddColumnsProgrammatically();
            populate_datagridview();
            hide_when_hotfill();


        }

        private void Get_Product_code()
        {
            DataSet ds = FruPak.PF.Data.AccessLayer.PF_Product.Get_Info(woDisplay1.Product_Id);
            DataRow dr;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dr = ds.Tables[0].Rows[i];
                str_product_code = dr["Code"].ToString();
            }
        }
        private void hide_when_hotfill()
        {
            switch (str_product_code)
            {
                case "WPC":
                case "FDA":
                    cmb_Batch2.Visible = false;
                    cmb_Batch3.Visible = false;
                    nud_Quantity2.Visible = false;
                    nud_Quantity3.Visible = false;
                    rb_EOR1.Visible = true;
                    rb_EOR2.Visible = false;
                    rb_EOR3.Visible = false;
                    nud_Quantity1.Value = 1;
                    break;
                case "HotFill":
                    cmb_Batch2.Visible = false;
                    cmb_Batch3.Visible = false;
                    nud_Quantity2.Visible = false;
                    nud_Quantity3.Visible = false;
                    rb_EOR1.Visible = false;
                    rb_EOR2.Visible = false;
                    rb_EOR3.Visible = false;
                    break;
                case "Juice":
                    cmb_Batch2.Visible = false;
                    cmb_Batch3.Visible = false;
                    nud_Quantity2.Visible = false;
                    nud_Quantity3.Visible = false;
                    rb_EOR1.Visible = true;
                    rb_EOR2.Visible = false;
                    rb_EOR3.Visible = false;
                    break;
                default:
                    cmb_Batch2.Visible = true;
                    cmb_Batch3.Visible = true;
                    nud_Quantity2.Visible = true;
                    nud_Quantity3.Visible = true;
                    rb_EOR1.Visible = true;
                    rb_EOR2.Visible = true;
                    rb_EOR3.Visible = true;
                    break;
            }
        }
        private static string str_Trader_Desc = "";
        public void populate_Combobox()
        {

            DataSet ds_Get_Info;
            //batch numbers
            ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Work_Order.Get_Info(int_Work_Order_Id);
            DataRow dr_Get_Info;

            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                int_W_Trader_Id = Convert.ToInt32(dr_Get_Info["Trader_Id"].ToString());
                str_Trader_Desc = dr_Get_Info["T_Description"].ToString();

                DataSet ds = FruPak.PF.Data.AccessLayer.PF_Pallet.Get_Max_Pallet_Numberd(int_Work_Order_Id);
                DataRow dr;

                int int_j = 0;

                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    dr = ds.Tables[0].Rows[j];
                    try
                    {
                        int_j = Convert.ToInt32(dr["Pallet_Num"].ToString());
                    }
                    catch
                    {
                        int_j = 0;
                    }
                }
                ds.Dispose();


                // the number of batches already created = the total number allowed, then reset so all are displayed
                if (int_j == Convert.ToInt32(dr_Get_Info["Num_Batches"].ToString()))
                {
                    int_j = 0;
                }
                //piemix must show all batch numbers
                switch (str_product_code)
                {
                    case "Piemix":
                        int_j = 0;
                        break;
                }
                for (int j = int_j; j < Convert.ToInt32(dr_Get_Info["Num_Batches"].ToString()); j++)
                {
                    cmb_Batch1.Items.Add(j + 1);
                    cmb_Batch2.Items.Add(j + 1);
                    cmb_Batch3.Items.Add(j + 1);
                }
            }
            cmb_Batch1.Text = null;
            cmb_Batch2.Text = null;
            cmb_Batch3.Text = null;
            ds_Get_Info.Dispose();

            //Pallet Type
            ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Pallet_Type.Get_Info();
            cmb_Pallet_Type.DataSource = ds_Get_Info.Tables[0];
            cmb_Pallet_Type.DisplayMember = "Combined";
            cmb_Pallet_Type.ValueMember = "PalletType_Id";
            cmb_Pallet_Type.Text = null;

            ds_Get_Info.Dispose();

            //Location
            ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Location.Get_Info();
            cmb_Location.DataSource = ds_Get_Info.Tables[0];
            cmb_Location.DisplayMember = "Code";
            cmb_Location.ValueMember = "Location_Id";
            cmb_Location.Text = "YPCP";
            ds_Get_Info.Dispose();

            //output products
            ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Work_Order_Material_Relationship.Get_Info_By_WO_Translated(int_Work_Order_Id);
            cmb_Material.DataSource = ds_Get_Info.Tables[0];
            cmb_Material.DisplayMember = "Combined";
            cmb_Material.ValueMember = "Material_Id";
            cmb_Material.Text = null;
            ds_Get_Info.Dispose();

            ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Orders.Get_Info_Desc();
            cmb_Orders.DataSource = ds_Get_Info.Tables[0];
            cmb_Orders.DisplayMember = "Combined";
            cmb_Orders.ValueMember = "Order_Id";
            cmb_Orders.Text = null;
            ds_Get_Info.Dispose();
        }
        private void AddColumnsProgrammatically()
        {
            //DataGridView 1
            #region ------------- DataGridView 1 -------------
            var col10 = new DataGridViewTextBoxColumn();
            var col11 = new DataGridViewTextBoxColumn();
            var col12 = new DataGridViewTextBoxColumn();
            var col13 = new DataGridViewTextBoxColumn();
            var col14 = new DataGridViewTextBoxColumn();
            var col15 = new DataGridViewTextBoxColumn();
            var col16 = new DataGridViewTextBoxColumn();
            var col17 = new DataGridViewTextBoxColumn();

            col10.HeaderText = "Pallet_Id";
            col10.Name = "Pallet_Id";
            col10.ReadOnly = true;
            col10.Visible = true;

            col11.HeaderText = "PalletType_Id";
            col11.Name = "PalletType_Id";
            col11.ReadOnly = true;
            col11.Visible = false;

            col12.HeaderText = "PalletType";
            col12.Name = "PalletType";
            col12.ReadOnly = true;
            col12.Visible = true;

            col13.HeaderText = "Barcode";
            col13.Name = "Barcode";
            col13.ReadOnly = true;
            col13.Visible = true;

            col14.HeaderText = "Location_Id";
            col14.Name = "Location_Id";
            col14.ReadOnly = true;
            col14.Visible = false;

            col15.HeaderText = "Location";
            col15.Name = "Location";
            col15.ReadOnly = true;

            col16.HeaderText = "Trader_Id";
            col16.Name = "Trader_Id";
            col16.ReadOnly = true;
            col16.Visible = false;

            col17.HeaderText = "Trader";
            col17.Name = "Trader";
            col17.ReadOnly = true;
            col17.Visible = true;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col10, col11, col12, col13, col14, col15, col16, col17 });

            DataGridViewButtonColumn btn_Reprint = new DataGridViewButtonColumn();
            btn_Reprint.Text = "Print";
            btn_Reprint.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(btn_Reprint);

            DataGridViewImageColumn img_delete1 = new DataGridViewImageColumn();
            dataGridView1.Columns.Add(img_delete1);
            img_delete1.HeaderText = "Delete";
            img_delete1.Name = "Delete";
            img_delete1.Image = FruPak.PF.Global.Properties.Resources.delete;
            img_delete1.ReadOnly = true;

            DataGridViewImageColumn img_edit1 = new DataGridViewImageColumn();
            dataGridView1.Columns.Add(img_edit1);
            img_edit1.HeaderText = "Edit";
            img_edit1.Name = "Edit";
            img_edit1.Image = FruPak.PF.Global.Properties.Resources.edit;
            img_edit1.ReadOnly = true;
            #endregion
            //DataGridView 2
            #region ------------- DataGridView 2 -------------
            var col20 = new DataGridViewTextBoxColumn();
            var col21 = new DataGridViewTextBoxColumn();
            var col22 = new DataGridViewTextBoxColumn();
            var col23 = new DataGridViewTextBoxColumn();
            var col24 = new DataGridViewTextBoxColumn();
            var col25 = new DataGridViewTextBoxColumn();

            col20.HeaderText = "PalletDetails_Id";
            col20.Name = "PalletDetails_Id";
            col20.ReadOnly = true;
            col20.Visible = false;

            col21.HeaderText = "Pallet_Id";
            col21.Name = "Pallet_Id";
            col21.ReadOnly = true;
            col21.Visible = true;

            col22.HeaderText = "Batch_Num";
            col22.Name = "Batch_Num";
            col22.ReadOnly = true;

            col23.HeaderText = "Material_Id";
            col23.Name = "Material_Id";
            col23.ReadOnly = true;
            col23.Visible = false;

            col24.HeaderText = "Product";
            col24.Name = "Product";
            col24.ReadOnly = true;

            col25.HeaderText = "Quantity";
            col25.Name = "Quantity";
            col25.ReadOnly = true;

            dataGridView2.Columns.AddRange(new DataGridViewColumn[] {col20, col21, col22, col23, col24, col25 });

            DataGridViewImageColumn img_delete = new DataGridViewImageColumn();
            dataGridView2.Columns.Add(img_delete);
            img_delete.HeaderText = "Delete";
            img_delete.Name = "Delete";
            img_delete.Image = FruPak.PF.Global.Properties.Resources.delete;
            img_delete.ReadOnly = true;

            DataGridViewImageColumn img_edit = new DataGridViewImageColumn();
            dataGridView2.Columns.Add(img_edit);
            img_edit.HeaderText = "Edit";
            img_edit.Name = "Edit";
            img_edit.Image = FruPak.PF.Global.Properties.Resources.edit;
            img_edit.ReadOnly = true;
            #endregion
        }
        private void SizeAllColumns(Object sender, EventArgs e)
        {
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataGridView2.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }
        private void populate_datagridview()
        {
            DataSet ds_Get_Info;
            DataRow dr_Get_Info;

            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();
            ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Pallet.Get_Info_Translated(int_Work_Order_Id);
            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                int i_rows = Convert.ToInt32(dataGridView1.Rows.Count.ToString());
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                dataGridView1.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_Cell10 = new DataGridViewTextBoxCell();
                DGVC_Cell10.Value = dr_Get_Info["Pallet_Id"].ToString();
                dataGridView1.Rows[i_rows].Cells["Pallet_Id"] = DGVC_Cell10;

                if (i == 0)
                {
                    int_Pallet_Id = Convert.ToInt32(dr_Get_Info["Pallet_Id"].ToString());
                }

                DataGridViewCell DGVC_Cell11 = new DataGridViewTextBoxCell();
                DGVC_Cell11.Value = dr_Get_Info["PalletType_Id"].ToString();
                dataGridView1.Rows[i_rows].Cells["PalletType_Id"] = DGVC_Cell11;

                DataGridViewCell DGVC_Cell12 = new DataGridViewTextBoxCell();
                DGVC_Cell12.Value = dr_Get_Info["PT_Combined"].ToString();
                dataGridView1.Rows[i_rows].Cells["PalletType"] = DGVC_Cell12;

                DataGridViewCell DGVC_Cell13 = new DataGridViewTextBoxCell();
                DGVC_Cell13.Value = dr_Get_Info["Barcode"].ToString();
                dataGridView1.Rows[i_rows].Cells["Barcode"] = DGVC_Cell13;

                DataGridViewCell DGVC_Cell14 = new DataGridViewTextBoxCell();
                DGVC_Cell14.Value = dr_Get_Info["Location_Id"].ToString();
                dataGridView1.Rows[i_rows].Cells["Location_Id"] = DGVC_Cell14;

                DataGridViewCell DGVC_Cell15 = new DataGridViewTextBoxCell();
                DGVC_Cell15.Value = dr_Get_Info["L_Code"].ToString();
                dataGridView1.Rows[i_rows].Cells["Location"] = DGVC_Cell15;

                DataGridViewCell DGVC_Cell16 = new DataGridViewTextBoxCell();
                DGVC_Cell16.Value = dr_Get_Info["Trader_Id"].ToString();
                dataGridView1.Rows[i_rows].Cells["Trader_Id"] = DGVC_Cell16;

                DataGridViewCell DGVC_Cell17 = new DataGridViewTextBoxCell();
                DGVC_Cell17.Value = dr_Get_Info["T_Code"].ToString();
                dataGridView1.Rows[i_rows].Cells["Trader"] = DGVC_Cell17;
            }
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            ds_Get_Info.Dispose();
        }
        private void populate_datagridview2()
        {
            DataSet ds_Get_Info;
            DataRow dr_Get_Info;

            dataGridView2.Refresh();
            dataGridView2.Rows.Clear();

            ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Pallet_Details.Get_Info(int_Work_Order_Id, int_DVG_Pallet_Id);

            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                int i_rows = Convert.ToInt32(dataGridView2.Rows.Count.ToString());
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                dataGridView2.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_Cell20 = new DataGridViewTextBoxCell();
                DGVC_Cell20.Value = dr_Get_Info["PalletDetails_Id"].ToString();
                dataGridView2.Rows[i_rows].Cells["PalletDetails_Id"] = DGVC_Cell20;

                DataGridViewCell DGVC_Cell21 = new DataGridViewTextBoxCell();
                DGVC_Cell21.Value = dr_Get_Info["Pallet_Id"].ToString();
                dataGridView2.Rows[i_rows].Cells["Pallet_Id"] = DGVC_Cell21;

                DataGridViewCell DGVC_Cell22 = new DataGridViewTextBoxCell();
                DGVC_Cell22.Value = dr_Get_Info["Batch_Num"].ToString();
                dataGridView2.Rows[i_rows].Cells["Batch_Num"] = DGVC_Cell22;

                DataGridViewCell DGVC_Cell23 = new DataGridViewTextBoxCell();
                DGVC_Cell23.Value = dr_Get_Info["Material_Id"].ToString();
                dataGridView2.Rows[i_rows].Cells["Material_Id"] = DGVC_Cell23;

                DataSet ds_Get_Info2 = FruPak.PF.Data.AccessLayer.PF_Work_Order_Material_Relationship.Get_Info_By_WO_Translated_M(Convert.ToInt32(dr_Get_Info["Material_Id"].ToString()));
                DataRow dr_Get_Info2;
                for (int j = 0; j < Convert.ToInt32(ds_Get_Info2.Tables[0].Rows.Count.ToString()); j++)
                {
                    dr_Get_Info2 = ds_Get_Info2.Tables[0].Rows[j];

                    DataGridViewCell DGVC_Cell24 = new DataGridViewTextBoxCell();
                    DGVC_Cell24.Value = dr_Get_Info2["Combined"].ToString();
                    dataGridView2.Rows[i_rows].Cells["Product"] = DGVC_Cell24;
                }
                DataGridViewCell DGVC_Cell25 = new DataGridViewTextBoxCell();
                DGVC_Cell25.Value = dr_Get_Info["Quantity"].ToString();
                dataGridView2.Rows[i_rows].Cells["Quantity"] = DGVC_Cell25;
            }
            ds_Get_Info.Dispose();
        }

        private string Create_New_Pallet()
        {
            string str_msg = "";

            if (cmb_Pallet_Type.SelectedValue == null)
            {
                str_msg = str_msg + "Invaild Pallet Type. PLease select a vaild Pallet type from the dropdown list." + Environment.NewLine;
            }
            if (cmb_Location.SelectedValue == null)
            {
                str_msg = str_msg + "Invaild Location. PLease select a vaild Location from the dropdown list." + Environment.NewLine;
            }
            return str_msg;
        }
        private string Create_New_Pallet_Details()
        {
            string str_msg = "";

            if (cmb_Material.SelectedValue == null)
            {
                str_msg = str_msg + "Invalid Output Product. Please select an Output Product from the dropdown list." + Environment.NewLine;
            }
            if (cmb_Batch1.SelectedItem == null && cmb_Batch2.SelectedItem == null && cmb_Batch3.SelectedItem == null)
            {
                str_msg = str_msg + "Invalid Batch Number. Please select a Batch Number from the dropdwon list." + Environment.NewLine;
            }
            if (cmb_Batch1.SelectedItem != null && nud_Quantity1.Value == 0)
            {
                str_msg = str_msg + "Invalid Batch Number/Quantity. You have not entered a Quantity for the first Batch." + Environment.NewLine;
            }
            if (cmb_Batch2.SelectedItem != null && nud_Quantity2.Value == 0)
            {
                str_msg = str_msg + "Invalid Batch Number/Quantity. You have not entered a Quantity for the Second Batch." + Environment.NewLine;
            }
            if (cmb_Batch3.SelectedItem != null && nud_Quantity3.Value == 0)
            {
                str_msg = str_msg + "Invalid Batch Number/Quantity. You have not entered a Quantity for the Third Batch." + Environment.NewLine;
            }
            if (rb_EOR1.Checked == true ||
                rb_EOR2.Checked == true ||
                rb_EOR3.Checked == true)
            {
                if (nud_EOR_Weight.Value == 0 )
                {
                    str_msg = str_msg + "Invalid End of Run Weight. You need to enter an End of Run Weight." + Environment.NewLine;
                }
            }
            if (nud_Quantity1.Value > 0)
            {
                str_msg = str_msg + check_quantity(nud_Quantity1.Value);
            }
            if (nud_Quantity2.Value > 0)
            {
                str_msg = str_msg + check_quantity(nud_Quantity2.Value);
            }
            if (nud_Quantity3.Value > 0)
            {
                str_msg = str_msg + check_quantity(nud_Quantity3.Value);
            }
            return str_msg;
        }

        private string check_quantity(decimal quantity)
        {
            string str_msg = "";
            if (quantity > 99)
            {
                str_msg = "Invalid Quantity. Quantity can NOT exceed 99, Please re-enter." + Environment.NewLine;
            }
            return str_msg;
        }
        private static string str_barcode = "";
        private static int int_Season = 0;
        private void btn_Add_Click(object sender, EventArgs e)
        {
            int return_code = 0;
            return_code = Submit();

            //if (return_code >= 0)
            //{

                if (ckb_plc_on_order.Checked == true)
                {
                    Place_On_Order();
                }
                if (ckb_Auto_Reset.Checked == true)
                {
                    Reset_Part();
                    hide_when_hotfill();
                }
                else
                {
                    Reset_Full();
                    hide_when_hotfill();
                }
            //}
        }
        /// <summary>
        /// This method will place the newly created pallet directly on to an a valid order
        /// This is used for some product that is being produced for a customer
        /// </summary>
        private void Place_On_Order()
        {
            string str_msg = "";
            DialogResult DLR_MSG = new DialogResult();
            int int_result = 0;
            //check an oder number has been selected.
            if (cmb_Orders.SelectedValue == null)
            {
                str_msg = str_msg + "Invalid Order. Please select a valid order from the drop down list." + Environment.NewLine;
            }
            if (int_Pallet_Id == 0)
            {
                str_msg = str_msg + "Invalid Pallet Identifer. The creation of the pallet may have failed. Contact support." + Environment.NewLine;
            }

            if (str_msg.Length > 0)
            {
                DLR_MSG = MessageBox.Show(str_msg, "Process Factory - Work Order(Labels)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (DLR_MSG != System.Windows.Forms.DialogResult.OK)
            {
                int_result = FruPak.PF.Data.AccessLayer.PF_Pallet.Update_Order_Id(int_Pallet_Id, Convert.ToInt32(cmb_Orders.SelectedValue.ToString()), int_Current_User_Id);
            }
            marqueeLabel1.Text = marqueeLabel1.Text + " Pallet has been place on Order " + cmb_Orders.SelectedValue.ToString();
        }
        private int Submit()
        {
            DialogResult DLR_MessageBox = new DialogResult();
            string str_msg = "";

            int int_result = -1;

            switch (btn_Add.Text)
            {
                case "&Add":
                    str_msg = str_msg + Create_New_Pallet();
                    str_msg = str_msg + Create_New_Pallet_Details();
                    break;
                case "&Update":
                    str_msg = str_msg + Create_New_Pallet();
                    if (cmb_Batch1.SelectedItem != null || cmb_Batch2.SelectedItem != null || cmb_Batch3.SelectedItem != null)
                    {
                        str_msg = str_msg + Create_New_Pallet_Details();
                    }
                    break;
                case "Update &Batch":
                    str_msg = str_msg + Create_New_Pallet_Details();
                    break;
            }

            if (str_msg.Length != 0)
            {
                DLR_MessageBox = MessageBox.Show(str_msg, "Process Factory - Work Order(Labels)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (DLR_MessageBox != DialogResult.OK)
            {
                int_Season = FruPak.PF.Common.Code.General.Get_Season();

                switch (btn_Add.Text)
                {
                    case "&Add":
                        int_Pallet_Id = FruPak.PF.Common.Code.General.int_max_user_id("PF_Pallet");

                        //Calculate Weight_Gross, and Weight_Tare

                        //1. get weight from material number
                        DataSet ds_weight = FruPak.PF.Data.AccessLayer.CM_Material.Get_Info(Convert.ToInt32(cmb_Material.SelectedValue.ToString()));
                        DataRow dr_weight;
                        decimal dec_material_weight = 0;
                        int int_material_packtype_id = 0;

                        for (int i = 0; i < ds_weight.Tables[0].Rows.Count; i++)
                        {
                            dr_weight = ds_weight.Tables[0].Rows[i];
                            dec_material_weight = Convert.ToDecimal(dr_weight["Weight"].ToString());
                            int_material_packtype_id = Convert.ToInt32(dr_weight["PackType_Id"]);
                        }
                        ds_weight.Dispose();

                        //2. Get tare Weight
                        DataSet ds_weight_tare = FruPak.PF.Data.AccessLayer.CM_Pack_Type_Weight.Get_Info(int_material_packtype_id);
                        DataRow dr_weight_tare;
                        decimal dec_pt_weight_tare = 0;

                        for (int i = 0; i < ds_weight_tare.Tables[0].Rows.Count; i++)
                        {
                            dr_weight_tare = ds_weight_tare.Tables[0].Rows[i];
                            dec_pt_weight_tare = Convert.ToDecimal(dr_weight_tare["Weight_Tare"].ToString());
                        }
                        ds_weight_tare.Dispose();

                        //3. Calculate gross weight.
                        decimal dec_weight_gross = 0;

                        dec_weight_gross = dec_weight_gross + (nud_Quantity1.Value * dec_material_weight);
                        dec_weight_gross = dec_weight_gross + (nud_Quantity2.Value * dec_material_weight);
                        dec_weight_gross = dec_weight_gross + (nud_Quantity3.Value * dec_material_weight);
                        dec_weight_gross = dec_weight_gross + dec_pt_weight_tare;
                        //

                        //setting barcode_Trader = 0, is a reset that is requireed
                        FruPak.PF.Common.Code.Barcode.Barcode_Trader = 0;

                        FruPak.PF.Common.Code.Barcode.Barcode_Create(int_Pallet_Id);
                        str_barcode = FruPak.PF.Common.Code.Barcode.Barcode_Num.ToString();

                        //count of pallets in a work order
                        DataSet ds = FruPak.PF.Data.AccessLayer.PF_Pallet_Details.Count_of_Pallets(int_Work_Order_Id);
                        DataRow dr;
                        int int_pallet_tot = 0;
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            dr = ds.Tables[0].Rows[i];
                            int_pallet_tot = Convert.ToInt32(dr["Total"].ToString()) + 1;
                        }
                        ds.Dispose();

                        ds = FruPak.PF.Data.AccessLayer.CM_Material.Get_Info(Convert.ToInt32(cmb_Material.SelectedValue.ToString()));
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            dr = ds.Tables[0].Rows[i];
                            int_Trader_Id = Convert.ToInt32(dr["Trader_Id"].ToString());
                        }
                        ds.Dispose();

                        int_result = FruPak.PF.Data.AccessLayer.PF_Pallet.Insert(int_Pallet_Id, Convert.ToInt32(cmb_Pallet_Type.SelectedValue.ToString()), str_barcode, Convert.ToInt32(cmb_Location.SelectedValue.ToString()), int_Trader_Id, true, int_Season, int_Current_User_Id, int_pallet_tot, dec_weight_gross, dec_pt_weight_tare);
                        int_result = Process_Pallet_Details(int_Pallet_Id, nud_Quantity1.Value);

                        break;
                        case "Add &SML/Rej":
                        for (int i = 0; i< Convert.ToInt32(nud_Quantity1.Value);i++)
                            {
                                int_Pallet_Id = FruPak.PF.Common.Code.General.int_max_user_id("CM_Bins");

                                FruPak.PF.Common.Code.Barcode.Barcode_Trader = int_W_Trader_Id;
                                FruPak.PF.Common.Code.Barcode.Barcode_Create(int_Pallet_Id);
                                str_barcode = FruPak.PF.Common.Code.Barcode.Barcode_Num.ToString();

                                int_result = FruPak.PF.Data.AccessLayer.CM_Bins.InsertWorkOrder(int_Pallet_Id,woDisplay1.Work_Order_Id,str_barcode,Convert.ToInt32(cmb_Location.SelectedValue.ToString()),Convert.ToInt32(cmb_Material.SelectedValue.ToString()),1,4,int_Current_User_Id);
                                Print_Bin_Cards();
                            }
                        break;
                    case "&Update":
                        int_result = FruPak.PF.Data.AccessLayer.PF_Pallet.Update(int_DVG_Row_id, Convert.ToInt32(cmb_Pallet_Type.SelectedValue.ToString()), Convert.ToInt32(cmb_Location.SelectedValue.ToString()), int_Current_User_Id);
                        int_result = Process_Pallet_Details(int_DVG_Row_id, nud_Quantity1.Value);
                        break;
                    case "Update &Batch":
                        string str_batch = "";
                        str_batch = woDisplay1.Process_Date.Substring(0, 4) + woDisplay1.Process_Date.Substring(5, 2) + woDisplay1.Process_Date.Substring(8, 2);
                        int_result = FruPak.PF.Data.AccessLayer.PF_Pallet_Details.Update(int_DVG_Row_id,Convert.ToInt32(str_batch + cmb_Batch1.SelectedItem.ToString()), Convert.ToInt32(cmb_Material.SelectedValue.ToString()),
                                                                                        nud_Quantity1.Value, int_Current_User_Id);
                        break;
                }
            }
            if (int_result >= 0)
            {
                marqueeLabel1.Text = "Pallet has been added/updated";
                marqueeLabel1.ForeColor = System.Drawing.Color.Blue;
                populate_datagridview();
                //auto print
                if (ckb_AutoPrint.Checked == true )
                {
                    print_Palletcard(0);
                    if (ckb_dup.Checked == true)
                    {
                        print_Palletcard(0);
                    }
                }
            }
            else
            {
                marqueeLabel1.Text = "Pallet failed to be added/Updated";
                marqueeLabel1.ForeColor = System.Drawing.Color.Red;
            }
            return int_result;
        }
        private int Process_Pallet_Details(int int_Pallet_Id, decimal dec_quantity1)
        {
            int int_result = 0;
            string str_batch = "";
            int int_batch_Id = 0;

            str_batch = woDisplay1.Process_Date.Substring(0, 4) + woDisplay1.Process_Date.Substring(5, 2) + woDisplay1.Process_Date.Substring(8, 2);

            if (cmb_Batch1.SelectedItem != null)
            {
                int int_pallet_details_Id = FruPak.PF.Common.Code.General.int_max_user_id("PF_Pallet_Details");

                int_result = FruPak.PF.Data.AccessLayer.PF_Pallet_Details.Insert(int_pallet_details_Id, int_Pallet_Id, Convert.ToInt32(str_batch + cmb_Batch1.SelectedItem.ToString()),
                                                                                 Convert.ToInt32(cmb_Material.SelectedValue.ToString()), int_Work_Order_Id, dec_quantity1, int_Current_User_Id);

                if (int_result >= 0)
                {
                    switch (str_product_code)
                    {
                        case "WPC":
                        case "FDA":
                        case "HotFill":
                        case "Juice":
                            //increase batch number
                            try
                            {
                                int_batch_Id = Convert.ToInt32(cmb_Batch1.SelectedItem.ToString()) + 1;
                            }
                            catch (Exception ex)
                            {
                                logger.Log(LogLevel.Debug, ex.Message);
                            }
                            cmb_Batch3.Items.Remove(Convert.ToInt32(cmb_Batch1.SelectedItem.ToString()));
                            cmb_Batch2.Items.Remove(Convert.ToInt32(cmb_Batch1.SelectedItem.ToString()));
                            cmb_Batch1.Items.Remove(Convert.ToInt32(cmb_Batch1.SelectedItem.ToString()));
                            break;
                    }
                }
                if (rb_EOR1.Checked == true)
                {
                    int_result = int_result + FruPak.PF.Data.AccessLayer.PF_Pallet_Details.Update_EOR_Weight(int_pallet_details_Id, nud_EOR_Weight.Value, int_Current_User_Id);
                }
            }
            if (cmb_Batch2.SelectedItem != null)
            {
                int int_pallet_details_Id = FruPak.PF.Common.Code.General.int_max_user_id("PF_Pallet_Details");

                int_result = FruPak.PF.Data.AccessLayer.PF_Pallet_Details.Insert(int_pallet_details_Id, int_Pallet_Id, Convert.ToInt32(str_batch + cmb_Batch2.SelectedItem.ToString()),
                                                                                 Convert.ToInt32(cmb_Material.SelectedValue.ToString()), int_Work_Order_Id, nud_Quantity2.Value, int_Current_User_Id);
                if (int_result >= 0)
                {
                    switch (str_product_code)
                    {
                        case "WPC":
                        case "FDA":
                        case "HotFill":
                        case "Juice":
                            //increase batch number
                            try
                            {
                                int_batch_Id = Convert.ToInt32(cmb_Batch2.SelectedItem.ToString()) + 1;
                            }
                            catch (Exception ex)
                            {
                                logger.Log(LogLevel.Debug, ex.Message);
                            }
                            cmb_Batch3.Items.Remove(Convert.ToInt32(cmb_Batch1.SelectedItem.ToString()));
                            cmb_Batch2.Items.Remove(Convert.ToInt32(cmb_Batch1.SelectedItem.ToString()));
                            cmb_Batch1.Items.Remove(Convert.ToInt32(cmb_Batch1.SelectedItem.ToString()));
                            break;
                    }
                }
                if (rb_EOR2.Checked == true)
                {
                    int_result = int_result + FruPak.PF.Data.AccessLayer.PF_Pallet_Details.Update_EOR_Weight(int_pallet_details_Id, nud_EOR_Weight.Value, int_Current_User_Id);
                }
            }
            if (cmb_Batch3.SelectedItem != null)
            {
                int int_pallet_details_Id = FruPak.PF.Common.Code.General.int_max_user_id("PF_Pallet_Details");

                int_result = FruPak.PF.Data.AccessLayer.PF_Pallet_Details.Insert(int_pallet_details_Id, int_Pallet_Id, Convert.ToInt32(str_batch + cmb_Batch3.SelectedItem.ToString()),
                                                                                 Convert.ToInt32(cmb_Material.SelectedValue.ToString()), int_Work_Order_Id, nud_Quantity3.Value, int_Current_User_Id);
                if (int_result >= 0)
                {
                    switch (str_product_code)
                    {
                        case "WPC":
                        case "FDA":
                        case "HotFill":
                        case "Juice":
                            //increase batch number
                            try
                            {
                                int_batch_Id = Convert.ToInt32(cmb_Batch3.SelectedItem.ToString()) + 1;
                            }
                            catch (Exception ex)
                            {
                                logger.Log(LogLevel.Debug, ex.Message);
                            }
                            cmb_Batch3.Items.Remove(Convert.ToInt32(cmb_Batch1.SelectedItem.ToString()));
                            cmb_Batch2.Items.Remove(Convert.ToInt32(cmb_Batch1.SelectedItem.ToString()));
                            cmb_Batch1.Items.Remove(Convert.ToInt32(cmb_Batch1.SelectedItem.ToString()));
                            break;
                    }
                }
                if (rb_EOR3.Checked == true)
                {
                    int_result = int_result + FruPak.PF.Data.AccessLayer.PF_Pallet_Details.Update_EOR_Weight(int_pallet_details_Id, nud_EOR_Weight.Value, int_Current_User_Id);
                }
            }

            if (int_result >= 0)
            {
                int_result = int_result + Stock_Materials_Adjustment(dec_quantity1 + nud_Quantity2.Value + nud_Quantity3.Value);

                //reset batch combo boxs
                try
                {
                    cmb_Batch1.SelectedItem = int_batch_Id;
                    cmb_Batch2.SelectedItem = int_batch_Id;
                    cmb_Batch3.SelectedItem = int_batch_Id;
                }
                catch (Exception ex)
                {
                    logger.Log(LogLevel.Debug, ex.Message);
                }
            }
            return int_result;
        }

        //stock adjustment not finished yest
        private int Stock_Materials_Adjustment(decimal dec_quantity1)
        {
            decimal dec_total = 0;
            int int_result = 0;

            DataSet ds = FruPak.PF.Data.AccessLayer.PF_Stock_Item.Get_Info_for_material(Convert.ToInt32(cmb_Material.SelectedValue.ToString()));
            DataRow dr;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dr = ds.Tables[0].Rows[i];
                dec_total = dec_quantity1  * Convert.ToDecimal(dr["Multiplier"].ToString());
                int_result = int_result + FruPak.PF.Data.AccessLayer.PF_Stock_Used.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Stock_Used"), Convert.ToInt32(dr["Stock_Item_Id"].ToString()), int_Season, Convert.ToInt32(dec_total), int_Current_User_Id);
            }

            //check stock holding/used levels
            FruPak.PF.Common.Code.General.Consumable_Check(int_Current_User_Id);

            return int_result;
        }
        private void btn_reset_Click(object sender, EventArgs e)
        {
            if (ckb_Auto_Reset.Checked == true)
            {
                Reset_Part();
            }
            else
            {
                Reset_Full();
            }
        }
        private void Reset_Full()
        {
            Reset_Part();
            cmb_Material.Text = null;
            cmb_Location.Text = null;
            cmb_Pallet_Type.Text = null;
            lbl_Location.Visible = true;
            lbl_Pallet_Type.Visible = true;
            cmb_Location.Visible = true;
            cmb_Pallet_Type.Visible = true;
            cmb_Batch1.ResetText();
            cmb_Batch1.SelectedItem = null;
            ckb_Auto_Reset.Checked = false;
            ckb_AutoPrint.Checked = false;
            ckb_plc_on_order.Checked = false;
            ckb_dup.Checked = false;
        }
        private void Reset_Part()
        {
            cmb_Batch2.ResetText();
            cmb_Batch3.ResetText();
            cmb_Batch2.SelectedItem = null;
            cmb_Batch3.SelectedItem = null;
            nud_Quantity1.Value = 0;
            nud_Quantity1.Enabled = true;
            nud_Quantity2.Value = 0;
            nud_Quantity2.Visible = true;
            nud_Quantity2.Enabled = true;
            nud_Quantity3.Value = 0;
            nud_Quantity3.Visible = true;
            nud_Quantity3.Enabled = true;
            cmb_Batch2.Visible = true;
            cmb_Batch3.Visible = true;
            rb_EOR1.Checked = false;
            rb_EOR2.Checked = false;
            rb_EOR3.Checked = false;
            btn_Add.Text = "&Add";

            switch (str_product_code)
            {
                case "HotFill":
                    nud_Quantity1.Value = 0;
                    nud_Quantity1.Enabled = true;
                    nud_Quantity2.Value = 0;
                    nud_Quantity2.Visible = false;
                    nud_Quantity2.Enabled = false;
                    nud_Quantity3.Value = 0;
                    nud_Quantity3.Visible = false;
                    nud_Quantity3.Enabled = false;
                    cmb_Batch2.SelectedItem = null;
                    cmb_Batch2.Visible = false;
                    cmb_Batch3.SelectedItem = null;
                    cmb_Batch3.Visible = false;
                    rb_EOR1.Visible = false;
                    break;
                case "Juice":
                case "WPC":
                case "FDA":
                    break;
                default:
                    cmb_Batch1.ResetText();
                    cmb_Batch1.SelectedItem = null;
                    break;
            }
        }
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if ((sender as DataGridView).Rows[Convert.ToInt32((sender as DataGridView).CurrentCell.RowIndex.ToString())].Cells[0].Value != null)
            {
                int_DVG_Pallet_Id = Convert.ToInt32((sender as DataGridView).Rows[Convert.ToInt32((sender as DataGridView).CurrentCell.RowIndex.ToString())].Cells[0].Value.ToString());
            }
            populate_datagridview2();
        }
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int_DVG_Pallet_Id = FruPak.PF.Common.Code.General.int_max_user_id("PF_Pallet") - 1;
        }
        private List<string> lst_filenames = new List<string>();
        private void print_Palletcard(int rowindex)
        {
            Cursor = Cursors.WaitCursor;
            FruPak.PF.Common.Code.General.Get_Printer("A4");

            switch (str_product_code)
            {
                case "WPC":
                case "FDA":
                    DialogResult DLR_Weights = new System.Windows.Forms.DialogResult();
                    Form frm_check_weight = new FruPak.PF.Utils.Scanning.Pallet_Weight(dataGridView1.Rows[rowindex].Cells["Barcode"].Value.ToString(), int_Current_User_Id, bol_write_access);
                    DLR_Weights = frm_check_weight.ShowDialog();
                    frm_check_weight.Dispose();

                    if (FruPak.PF.PrintLayer.Word.Test_for_Word() == true)
                    {
                        try
                        {
                            FruPak.PF.PrintLayer.Word.CloseWord();
                        }
                        catch (Exception ex)
                        {
                            logger.Log(LogLevel.Debug, ex.Message);
                        }
                        FruPak.PF.PrintLayer.WPC_Card.Print(dataGridView1.Rows[rowindex].Cells["Barcode"].Value.ToString(), true, int_Current_User_Id);
                    }
                    else
                    {
                        string Data = "";
                        Data = Data + dataGridView1.Rows[rowindex].Cells["Barcode"].Value.ToString();
                        Data = Data + ":True";

                        //FruPak.PF.PrintLayer.Word.Printer = "Brother HL-2040 series";
                        FruPak.PF.PrintLayer.Word.Printer = Settings.Printer_Name;  // 16/06/2015 Fixed - Jim worked out there was some hardcoded strings

                        // Phantom 18/12/2014
                        //FruPak.PF.PrintLayer.Word.Printer = Settings.Printer_Name;   // Reverted 06-03-2015

                        DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_System.Get_Info_Like("PF%");
                        DataRow dr_Get_Info;
                        for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
                        {
                            dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                            switch (dr_Get_Info["Code"].ToString())
                            {
                                case "PF-TPath":
                                    FruPak.PF.PrintLayer.Word.TemplatePath = dr_Get_Info["Value"].ToString();
                                    break;
                                case "PF-TWPC":
                                    FruPak.PF.PrintLayer.Word.TemplateName = dr_Get_Info["Value"].ToString();
                                    break;
                            }
                        }
                        ds_Get_Info.Dispose();
                        FruPak.PF.PrintLayer.Word.Server_Print(Data, int_Current_User_Id);
                    }
                    break;
                default:
                    if (FruPak.PF.PrintLayer.Word.Test_for_Word() == true)
                    {
                        try
                        {
                            FruPak.PF.PrintLayer.Word.CloseWord();
                        }
                        catch (Exception ex)
                        {
                            logger.Log(LogLevel.Debug, ex.Message);
                        }
                        FruPak.PF.PrintLayer.Pallet_Card.Print(dataGridView1.Rows[rowindex].Cells["Barcode"].Value.ToString(), true, int_Current_User_Id);
                    }
                    else
                    {
                        string Data = "";
                        Data = Data + dataGridView1.Rows[rowindex].Cells["Barcode"].Value.ToString();
                        Data = Data + ":True";

                        //FruPak.PF.PrintLayer.Word.Printer = "Brother HL-2040 series";

                        // Phantom 18/12/2014
                        FruPak.PF.PrintLayer.Word.Printer = Settings.Printer_Name;

                        DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_System.Get_Info_Like("PF%");
                        DataRow dr_Get_Info;
                        for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
                        {
                            dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                            switch (dr_Get_Info["Code"].ToString())
                            {
                                case "PF-TPath":
                                    FruPak.PF.PrintLayer.Word.TemplatePath = dr_Get_Info["Value"].ToString();
                                    break;
                                case "PF-TPallet":
                                    FruPak.PF.PrintLayer.Word.TemplateName = dr_Get_Info["Value"].ToString();
                                    break;
                            }
                        }
                        ds_Get_Info.Dispose();
                        FruPak.PF.PrintLayer.Word.Server_Print(Data, int_Current_User_Id);
                    }
                    break;
            }
            try
            {
                FruPak.PF.PrintLayer.Word.CloseWord();
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message);
            }
            string str_path = "";

            DataSet ds_Get_Info1 = FruPak.PF.Data.AccessLayer.CM_System.Get_Info_Like("PF%");
            DataRow dr_Get_Info1;
            for (int i = 0; i < Convert.ToInt32(ds_Get_Info1.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info1 = ds_Get_Info1.Tables[0].Rows[i];
                switch (dr_Get_Info1["Code"].ToString())
                {
                    case "PF-TPPath":
                        str_path = dr_Get_Info1["Value"].ToString();
                        break;
                }
            }
            ds_Get_Info1.Dispose();

            Cursor = Cursors.Default;
            lst_filenames.AddRange(System.IO.Directory.GetFiles(str_path, "*" + dataGridView1.Rows[rowindex].Cells["Barcode"].Value.ToString() + "*", System.IO.SearchOption.TopDirectoryOnly));

            foreach (string filename in lst_filenames)
            {
                File.Delete(filename);
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult DLR_Message = new DialogResult();
            int int_result = 0;

            //Print
            #region ------------- Print -------------
            if (e.ColumnIndex == 8)
            {
                print_Palletcard(e.RowIndex);
                if (ckb_dup.Checked == true)
                {
                    print_Palletcard(e.RowIndex);
                }
            }
            #endregion
            //Delete
            #region ------------- Delete -------------
            else if (e.ColumnIndex == 9)
            {
                string str_msg;

                str_msg = "You are about to Delete Pallet Number ";
                str_msg = str_msg + dataGridView1.Rows[e.RowIndex].Cells["Pallet_Id"].Value.ToString();
                str_msg = str_msg + " and its contents " + Environment.NewLine;
                str_msg = str_msg + "Do you want to continue?";
                DLR_Message = MessageBox.Show(str_msg, "Process Factory - Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (DLR_Message == DialogResult.Yes)
                {
                    int_result = FruPak.PF.Data.AccessLayer.PF_Pallet_Details.Delete_Pallet(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Pallet_Id"].Value.ToString()));

                    try
                    {
                        for (int i = 0; i < dataGridView2.Rows.Count; i++)
                        {
                            Stock_Materials_Adjustment(-1 * Convert.ToDecimal(dataGridView2.Rows[i].Cells["Quantity"].Value.ToString()));
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Log(LogLevel.Debug, ex.Message);
                    }

                    int_result = FruPak.PF.Data.AccessLayer.PF_Pallet.Delete(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Pallet_Id"].Value.ToString()));

                    //adjust stock.
                }
                if (int_result > 0)
                {
                    marqueeLabel1.ForeColor = System.Drawing.Color.Blue;
                    marqueeLabel1.Text = "Pallet " + dataGridView1.Rows[e.RowIndex].Cells["Pallet_Id"].Value.ToString() + " has been Deleted";
                }
                else
                {
                    marqueeLabel1.ForeColor = System.Drawing.Color.Red;
                    marqueeLabel1.Text = "Pallet " + dataGridView1.Rows[e.RowIndex].Cells["Pallet_Id"].Value.ToString() + " failed to be Deleted";
                }
                populate_datagridview();
            }
            #endregion
            //Edit
            #region ------------- Edit -------------
            else if (e.ColumnIndex == 10)
            {
                int_DVG_Row_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Pallet_Id"].Value.ToString());
                cmb_Pallet_Type.SelectedValue = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["PalletType_Id"].Value.ToString());
                cmb_Location.SelectedValue = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Location_Id"].Value.ToString());
                btn_Add.Text = "&Update";
            }
            #endregion
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult DLR_Message = new DialogResult();
            int int_result = 0;
            //Delete
            #region ------------- Delete -------------
            if (e.ColumnIndex == 6)
            {
                string str_msg;

                str_msg = "You are about to Delete Pallet Number ";
                str_msg = str_msg + dataGridView1.Rows[e.RowIndex].Cells["PalletDetails_Id"].Value.ToString();
                str_msg = str_msg + " and its contents " + Environment.NewLine;
                str_msg = str_msg + "Do you want to continue?";
                DLR_Message = MessageBox.Show(str_msg, "Process Factory - Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (DLR_Message == DialogResult.Yes)
                {
                    int_result = FruPak.PF.Data.AccessLayer.PF_Pallet_Details.Delete(Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["PalletDetails_Id"].Value.ToString()));
                    int_result = int_result + Stock_Materials_Adjustment(-1 * Convert.ToDecimal(dataGridView2.Rows[e.RowIndex].Cells["Quantity"].Value.ToString()));
                }
                if (int_result > 0)
                {
                    marqueeLabel1.ForeColor = System.Drawing.Color.Blue;
                    marqueeLabel1.Text = "Pallet Details" + dataGridView2.Rows[e.RowIndex].Cells["PalletDetails_Id"].Value.ToString() + " has been Deleted";
                }
                else
                {
                    marqueeLabel1.ForeColor = System.Drawing.Color.Red;
                    marqueeLabel1.Text = "Pallet Details" + dataGridView2.Rows[e.RowIndex].Cells["PalletDetails_Id"].Value.ToString() + " failed to be Deleted";
                }
                populate_datagridview2();
            }
            #endregion
            //Edit
            #region ------------- Edit -------------
            else if (e.ColumnIndex == 7)
            {
                int_DVG_Row_id = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["PalletDetails_Id"].Value.ToString());
                cmb_Material.SelectedValue = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["Material_Id"].Value.ToString());
                cmb_Batch1.SelectedItem = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["Batch_Num"].Value.ToString().Substring(8));
                nud_Quantity1.Value = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["Quantity"].Value.ToString());

                lbl_Location.Visible = false;
                cmb_Location.Visible = false;
                lbl_Pallet_Type.Visible = false;
                cmb_Pallet_Type.Visible = false;

                cmb_Batch2.Visible = false;
                nud_Quantity2.Visible = false;
                cmb_Batch3.Visible = false;
                nud_Quantity3.Visible = false;

                btn_Add.Text = "&Update Batch";
            }
            #endregion
        }
        private void cmb_Batch1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Convert.ToInt32((sender as ComboBox).SelectedItem.ToString()) == 0)
            {
                btn_Add.Text = "Add &SML/Rej";
                cmb_Batch2.Visible = false;
                cmb_Batch3.Visible = false;
                nud_Quantity2.Visible = false;
                nud_Quantity3.Visible = false;
            }
            else
            {
                btn_Add.Text = "&Add";
                cmb_Batch2.Visible = true;
                cmb_Batch3.Visible = true;
                nud_Quantity2.Visible = true;
                nud_Quantity3.Visible = true;
                hide_when_hotfill();
            }
        }
        private void Print_Bin_Cards()
        {
            DataSet ds_Get_Info;
            DataRow dr_Get_Info;
            //Create Barcode
            string str_image_location = "";
            FruPak.PF.PrintLayer.Barcode.str_Barcode = str_barcode;
            FruPak.PF.PrintLayer.Barcode.Width = 200;
            FruPak.PF.PrintLayer.Barcode.Height = 50;
            str_image_location = FruPak.PF.PrintLayer.Barcode.Generate_Barcode();

            ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_System.Get_Info_Like("PF%");
            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                switch (dr_Get_Info["Code"].ToString())
                {
                    case "PF-TPath":
                        FruPak.PF.PrintLayer.Labels.TemplatePath = dr_Get_Info["Value"].ToString();
                        break;
                    case "PF-TBin":
                        FruPak.PF.PrintLayer.Labels.TemplateName = dr_Get_Info["Value"].ToString();
                        break;
                }
            }
            ds_Get_Info.Dispose();

            FruPak.PF.PrintLayer.Labels.Bin_Labels(str_barcode, cmb_Material.Text.Substring(0, cmb_Material.Text.IndexOf('-') - 1), str_Trader_Desc, str_Variety_desc, "", "", woDisplay1.Process_Date.ToString().Substring(8, 2) + "/" + woDisplay1.Process_Date.ToString().Substring(5, 2) + "/" + woDisplay1.Process_Date.ToString().Substring(0, 4), false);
        }
        private void cmb_Material_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DataSet ds_Get_info = FruPak.PF.Data.AccessLayer.CM_Material.Get_Info(Convert.ToInt32((sender as ComboBox).SelectedValue.ToString()));
            DataRow dr_Get_info;

            for (int i = 0; i < Convert.ToInt32(ds_Get_info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_info = ds_Get_info.Tables[0].Rows[i];
                try
                {
                    cmb_Pallet_Type.SelectedValue = Convert.ToInt32(dr_Get_info["PalletType_Id"]);
                }
                catch
                {
                    cmb_Pallet_Type.SelectedValue = 0;
                    cmb_Pallet_Type.Text = null;
                }
            }
        }
        private void Enter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Submit();
            }
        }
        private void rb_EORCheckedChanged(object sender, EventArgs e)
        {
            nud_EOR_Weight.Visible = (sender as RadioButton).Checked;

            if (rb_EOR1.Checked == true)
            {
                nud_Quantity1.Value = 1;
                nud_Quantity1.Enabled = false;
                nud_Quantity2.Value = 0;
                nud_Quantity2.Enabled = true;
                nud_Quantity3.Value = 0;
                nud_Quantity3.Enabled = true;
            }
            else if (rb_EOR2.Checked == true)
            {
                nud_Quantity1.Value = 0;
                nud_Quantity1.Enabled = true;
                nud_Quantity2.Value = 1;
                nud_Quantity2.Enabled = false;
                nud_Quantity3.Value = 0;
                nud_Quantity3.Enabled = true;
            }
            else if (rb_EOR3.Checked == true)
            {
                nud_Quantity1.Value = 0;
                nud_Quantity1.Enabled = true;
                nud_Quantity2.Value = 0;
                nud_Quantity2.Enabled = true;
                nud_Quantity3.Value = 1;
                nud_Quantity3.Enabled = false;
            }
        }
        private void cmb_Batch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex >= 0)
            {
                switch((sender as ComboBox).Name)
                {
                    case "cmb_Batch1":
                        rb_EOR1.Visible = true;
                        rb_EOR2.Visible = false;
                        rb_EOR3.Visible = false;
                        break;
                    case "cmb_Batch2":
                        rb_EOR1.Visible = false;
                        rb_EOR2.Visible = true;
                        rb_EOR3.Visible = false;
                        break;
                    case "cmb_Batch3":
                        rb_EOR1.Visible = false;
                        rb_EOR2.Visible = false;
                        rb_EOR3.Visible = true;
                        break;
                }
            }
        }
        private void ckb_plc_on_order_CheckedChanged(object sender, EventArgs e)
        {
            lbl_order.Visible = ckb_plc_on_order.Checked;
            cmb_Orders.Visible = ckb_plc_on_order.Checked;
        }
        private void nud_Quantity_KeyUp(object sender, KeyEventArgs e)
        {
            if (IsKeyADigit(e.KeyCode) == true)
            {
                add_totals();
            }
        }
        private void nud_Quantity_ValueChanged(object sender, EventArgs e)
        {
            add_totals();
        }
        private void add_totals()
        {
            txt_Pallet_Total.Text = Convert.ToString(nud_Quantity1.Value + nud_Quantity2.Value + nud_Quantity3.Value);
        }
        public static bool IsKeyADigit(Keys key)
        {
            return (key >= Keys.D0 && key <= Keys.D9) || (key >= Keys.NumPad0 && key <= Keys.NumPad9);
        }

        #region Log Code
        /// <summary>
        /// Formats the output to the debug log so that the actual log contents stand out amongst the machine-generated stuff.
        /// Refers to machine-generated content, not calling specifics.
        /// </summary>
        /// <param name="input">The code function that we want to log.</param>
        /// <returns>A formatted (Bracketed) string</returns>
        public string LogCode(string input)
        {
            string Output = "___[ " + input + " ]___";
            return Output;
        }
        #endregion

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
        private void WO_Labels_KeyDown(object sender, KeyEventArgs e)
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
