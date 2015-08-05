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

namespace FruPak.PF.BaseLoad
{
    // ------------------------------------------------------------------------------
    // BN Note - 12-02-2015 - Noticed that some of the controls on this form had the
    // Tag property set to the same value as the TabIndex value...
    // I don't know if this is significant or what.
    // ------------------------------------------------------------------------------
    public partial class BLWO_Labels : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        // Declare application settings object - 12-01-2015 BN - BEGIN
        private static PhantomCustomSettings Settings;
        // Declare application settings object - 12-01-2015 BN - END

        private string pathToTempFolder = string.Empty; // BN

        private static bool bol_write_access;
        private static int int_Current_User_Id = 0;
        private static int int_Work_Order_Id = 0;
        private static int int_Trader_Id = 0;
        private static int int_Pallet_Id = 0;
        private static int int_DVG_Row_id = 0;
        private static int int_DVG_Pallet_Id = 0;
        private static string str_Variety_desc = "";

        public BLWO_Labels(int int_wo_Id, int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();
            logger.Log(LogLevel.Info, LogCode("BLWO_Labels - InitializeComponent"));

            // All this needs to be refactored into a single static class. Will do, but for now...

            #region Load CustomSettings
            #region Get "My Documents" folder
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            path = Path.Combine(path, "FruPak_Settings");

            // Create folder if it doesn't already exist
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            #endregion

            // Initialize settings
            Settings = new PhantomCustomSettings();
            Settings.SettingsPath = Path.Combine(path, "FruPak.Phantom.config");
            Settings.EncryptionKey = "phantomKey";

            if (!File.Exists(Settings.SettingsPath))
            {
                Settings.Save();
                // This should NEVER happen
                logger.Log(LogLevel.Info, LogCode("BLWO_Labels.cs: Default settings file created"));
            }
            // Load settings - Normally in Form_Load
            Settings.Load();

            logger.Log(LogLevel.Info, LogCode("BLWO_Labels.cs: Settings file opened."));

            #endregion

            int_Current_User_Id = int_C_User_id;
            int_Work_Order_Id = int_wo_Id;

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


            //if (FruPak.PF.Global.Global.bol_Testing == true)
            //{
            //    this.Text = "FruPak Process Factory - " + this.Text + " - Test Environment";
            //}
            //else
            //{
            //    this.Text = "FruPak Process Factory";
            //}

            populate_Combobox();

            AddColumnsProgrammatically();
            populate_datagridview();

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
        private static string str_Trader_Desc = "";
        public void populate_Combobox()
        {
            DataSet ds_Get_Info;
            //batch numbers
            ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Work_Order.Get_Info(int_Work_Order_Id);
            DataRow dr_Get_Info;

            cmb_Batch1.Items.Add(0);

            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                int_Trader_Id = Convert.ToInt32(dr_Get_Info["Trader_Id"].ToString());
                str_Trader_Desc = dr_Get_Info["T_Description"].ToString();

                for (int j = 0; j < Convert.ToInt32(dr_Get_Info["Num_Batches"].ToString()); j++)
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

            if (ds_Get_Info.Tables[0].Rows.Count > 1)
            {
                cmb_Material.Text = null;
            }
            ds_Get_Info.Dispose();
        }
        private void AddColumnsProgrammatically()
        {
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
            col15.Visible = true;

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

                DataGridViewCell DGVC_Id = new DataGridViewTextBoxCell();
                DGVC_Id.Value = dr_Get_Info["Pallet_Id"].ToString();
                dataGridView1.Rows[i_rows].Cells[0] = DGVC_Id;

                if (i == 0)
                {
                    int_Pallet_Id = Convert.ToInt32(dr_Get_Info["Pallet_Id"].ToString());
                }

                DataGridViewCell DGVC_PalletType_Id = new DataGridViewTextBoxCell();
                DGVC_PalletType_Id.Value = dr_Get_Info["PalletType_Id"].ToString();
                dataGridView1.Rows[i_rows].Cells[1] = DGVC_PalletType_Id;

                DataGridViewCell DGVC_PalletType = new DataGridViewTextBoxCell();
                DGVC_PalletType.Value = dr_Get_Info["PT_Combined"].ToString();
                dataGridView1.Rows[i_rows].Cells[2] = DGVC_PalletType;

                DataGridViewCell DGVC_Barcode = new DataGridViewTextBoxCell();
                DGVC_Barcode.Value = dr_Get_Info["Barcode"].ToString();
                dataGridView1.Rows[i_rows].Cells[3] = DGVC_Barcode;

                DataGridViewCell DGVC_Location_Id = new DataGridViewTextBoxCell();
                DGVC_Location_Id.Value = dr_Get_Info["Location_Id"].ToString();
                dataGridView1.Rows[i_rows].Cells[4] = DGVC_Location_Id;

                DataGridViewCell DGVC_Location = new DataGridViewTextBoxCell();
                DGVC_Location.Value = dr_Get_Info["L_Code"].ToString();
                dataGridView1.Rows[i_rows].Cells[5] = DGVC_Location;

                DataGridViewCell DGVC_Trader_Id = new DataGridViewTextBoxCell();
                DGVC_Trader_Id.Value = dr_Get_Info["Trader_Id"].ToString();
                dataGridView1.Rows[i_rows].Cells[6] = DGVC_Trader_Id;

                DataGridViewCell DGVC_Trader = new DataGridViewTextBoxCell();
                DGVC_Trader.Value = dr_Get_Info["T_Code"].ToString();
                dataGridView1.Rows[i_rows].Cells[7] = DGVC_Trader;
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

                DataGridViewCell DGVC_Id = new DataGridViewTextBoxCell();
                DGVC_Id.Value = dr_Get_Info["PalletDetails_Id"].ToString();
                dataGridView2.Rows[i_rows].Cells[0] = DGVC_Id;

                DataGridViewCell DGVC_Pallet_Id = new DataGridViewTextBoxCell();
                DGVC_Pallet_Id.Value = dr_Get_Info["Pallet_Id"].ToString();
                dataGridView2.Rows[i_rows].Cells[1] = DGVC_Pallet_Id;

                DataGridViewCell DGVC_Batch_Num = new DataGridViewTextBoxCell();
                DGVC_Batch_Num.Value = dr_Get_Info["Batch_Num"].ToString();
                dataGridView2.Rows[i_rows].Cells[2] = DGVC_Batch_Num;

                DataGridViewCell DGVC_Material_Id = new DataGridViewTextBoxCell();
                DGVC_Material_Id.Value = dr_Get_Info["Material_Id"].ToString();
                dataGridView2.Rows[i_rows].Cells[3] = DGVC_Material_Id;

                DataSet ds_Get_Info2 = FruPak.PF.Data.AccessLayer.PF_Work_Order_Material_Relationship.Get_Info_By_WO_Translated_M(Convert.ToInt32(dr_Get_Info["Material_Id"].ToString()));
                DataRow dr_Get_Info2;
                for (int j = 0; j < Convert.ToInt32(ds_Get_Info2.Tables[0].Rows.Count.ToString()); j++)
                {
                    dr_Get_Info2 = ds_Get_Info2.Tables[0].Rows[j];

                    DataGridViewCell DGVC_Material = new DataGridViewTextBoxCell();
                    DGVC_Material.Value = dr_Get_Info2["Combined"].ToString();
                    dataGridView2.Rows[i_rows].Cells[4] = DGVC_Material;

                }

                DataGridViewCell DGVC_Quantity = new DataGridViewTextBoxCell();
                DGVC_Quantity.Value = dr_Get_Info["Quantity"].ToString();
                dataGridView2.Rows[i_rows].Cells[5] = DGVC_Quantity;

            }
            dataGridView2.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
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
                if (cmb_Batch1.Text == "" && cmb_Batch2.Text == "" && cmb_Batch3.Text == "")
                {
                    str_msg = str_msg + "Invalid Batch Number. Please select a Batch Number from the dropdwon list." + Environment.NewLine;
                }
            }
            if (cmb_Batch1.SelectedItem != null && nud_Quantity1.Value == 0 || cmb_Batch1.Text != "" && nud_Quantity1.Value == 0)
            {
                str_msg = str_msg + "Invalid Batch Number/Quantity. You have not entered a Quantity for the first Batch." + Environment.NewLine;
            }
            if (cmb_Batch2.SelectedItem != null && nud_Quantity2.Value == 0 || cmb_Batch2.Text != "" && nud_Quantity2.Value == 0)
            {
                str_msg = str_msg + "Invalid Batch Number/Quantity. You have not entered a Quantity for the Second Batch." + Environment.NewLine;
            }
            if (cmb_Batch3.SelectedItem != null && nud_Quantity3.Value == 0 || cmb_Batch3.Text != "" && nud_Quantity3.Value == 0)
            {
                str_msg = str_msg + "Invalid Batch Number/Quantity. You have not entered a Quantity for the Third Batch." + Environment.NewLine;
            }
            return str_msg;
        }
        private static string str_barcode = "";
        private void btn_Add_Click(object sender, EventArgs e)
        {
            DialogResult DLR_MessageBox = new DialogResult();
            string str_msg = "";


            int int_result = 0;


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
                int int_Season = FruPak.PF.Common.Code.General.Get_Season();
                #region ----------- switch --------------
                switch (btn_Add.Text)
                {
                    case "&Add":
                        int_Pallet_Id = FruPak.PF.Common.Code.General.int_max_user_id("PF_Pallet");

                        //setting barcode_Trader = 0, is a reset that is requireed
                        FruPak.PF.Common.Code.Barcode.Barcode_Trader = 0;

                        FruPak.PF.Common.Code.Barcode.Barcode_Create(int_Pallet_Id);
                        str_barcode = FruPak.PF.Common.Code.Barcode.Barcode_Num.ToString();

                        int_result = FruPak.PF.Data.AccessLayer.PF_Pallet.Insert(int_Pallet_Id, Convert.ToInt32(cmb_Pallet_Type.SelectedValue.ToString()), str_barcode, Convert.ToInt32(cmb_Location.SelectedValue.ToString()), int_Trader_Id,Convert.ToBoolean(1),int_Season, int_Current_User_Id);
                        int_result = Process_Pallet_Details(int_Pallet_Id, nud_Quantity1.Value);

                        break;
                        case "Add &SML/Rej":
                        for (int i = 0; i< Convert.ToInt32(nud_Quantity1.Value);i++)
                            {
                                int_Pallet_Id = FruPak.PF.Common.Code.General.int_max_user_id("PF_Pallet");

                                FruPak.PF.Common.Code.Barcode.Barcode_Create(int_Pallet_Id);
                                str_barcode = FruPak.PF.Common.Code.Barcode.Barcode_Num.ToString();

                                int_result = FruPak.PF.Data.AccessLayer.PF_Pallet.Insert(int_Pallet_Id, Convert.ToInt32(cmb_Pallet_Type.SelectedValue.ToString()), str_barcode, Convert.ToInt32(cmb_Location.SelectedValue.ToString()), int_Trader_Id, Convert.ToBoolean(1),int_Season, int_Current_User_Id);
                                int_result = Process_Pallet_Details(int_Pallet_Id, 1);
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
                #endregion
            }
            if (int_result >= 0)
            {
                // update Weights
                decimal dec_Nett_Weight = 0;
                dec_Nett_Weight = dec_Nett_Weight + FruPak.PF.Common.Code.General.Calculate_Nett_Weight(Convert.ToInt32(cmb_Material.SelectedValue.ToString()), nud_Quantity1.Value);
                dec_Nett_Weight = dec_Nett_Weight + FruPak.PF.Common.Code.General.Calculate_Nett_Weight(Convert.ToInt32(cmb_Material.SelectedValue.ToString()), nud_Quantity2.Value);
                dec_Nett_Weight = dec_Nett_Weight + FruPak.PF.Common.Code.General.Calculate_Nett_Weight(Convert.ToInt32(cmb_Material.SelectedValue.ToString()), nud_Quantity3.Value);

                FruPak.PF.Data.AccessLayer.PF_Pallet.Update_Gross_Weight(int_Pallet_Id, dec_Nett_Weight, int_Current_User_Id);
                FruPak.PF.Data.AccessLayer.PF_Pallet.Update_Tare_Weight(int_Pallet_Id, Convert.ToDecimal("0.00"), int_Current_User_Id);

                lbl_message.Text = "Pallet has been added/updated";
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                populate_datagridview();
                Reset();
            }
            else
            {
                lbl_message.Text = "Pallet failed to be added/Updated";
                lbl_message.ForeColor = System.Drawing.Color.Red;
            }

        }
        private int Process_Pallet_Details(int int_Pallet_Id, decimal dec_quantity1)
        {
            int int_result = 0;
            string str_batch = "";

            if (cmb_Batch1.SelectedItem != null || cmb_Batch1.Text != "")
            {
                string str_day = "";
                string str_mth = "";
                if (dtp_Batch_Date1.Value.Day < 10)
                {
                    str_day = "0" + dtp_Batch_Date1.Value.Day.ToString();
                }
                else
                {
                    str_day = dtp_Batch_Date1.Value.Day.ToString();
                }
                if (dtp_Batch_Date1.Value.Month < 10)
                {
                    str_mth = "0" + dtp_Batch_Date1.Value.Month.ToString();
                }
                else
                {
                    str_mth = dtp_Batch_Date1.Value.Month.ToString();
                }
                str_batch = dtp_Batch_Date1.Value.Year.ToString() + str_mth + str_day;

                if (cmb_Batch1.SelectedItem == null)
                {
                    str_batch = str_batch + cmb_Batch1.Text.ToString();
                }
                else
                {
                    str_batch = str_batch + cmb_Batch1.SelectedItem.ToString();
                }

                int_result = FruPak.PF.Data.AccessLayer.PF_Pallet_Details.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Pallet_Details"), int_Pallet_Id, Convert.ToInt32(str_batch),
                                                                                 Convert.ToInt32(cmb_Material.SelectedValue.ToString()), int_Work_Order_Id, dec_quantity1, int_Current_User_Id);
                update_preSystemStock(Convert.ToInt32(cmb_Material.SelectedValue.ToString()), dec_quantity1);
            }
            if (cmb_Batch2.SelectedItem != null || cmb_Batch2.Text != "")
            {
                string str_day = "";
                string str_mth = "";
                if (dtp_Batch_Date2.Value.Day < 10)
                {
                    str_day = "0" + dtp_Batch_Date2.Value.Day.ToString();
                }
                else
                {
                    str_day = dtp_Batch_Date2.Value.Day.ToString();
                }
                if (dtp_Batch_Date2.Value.Month < 10)
                {
                    str_mth = "0" + dtp_Batch_Date2.Value.Month.ToString();
                }
                else
                {
                    str_mth = dtp_Batch_Date2.Value.Month.ToString();
                }
                str_batch = dtp_Batch_Date2.Value.Year.ToString() + str_mth + str_day;
                if (cmb_Batch2.SelectedItem == null)
                {
                    str_batch = str_batch + cmb_Batch2.Text.ToString();
                }
                else
                {
                    str_batch = str_batch + cmb_Batch2.SelectedItem.ToString();
                }
                int_result = FruPak.PF.Data.AccessLayer.PF_Pallet_Details.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Pallet_Details"), int_Pallet_Id, Convert.ToInt32(str_batch),
                                                                                 Convert.ToInt32(cmb_Material.SelectedValue.ToString()), int_Work_Order_Id, nud_Quantity2.Value, int_Current_User_Id);
                update_preSystemStock(Convert.ToInt32(cmb_Material.SelectedValue.ToString()), nud_Quantity2.Value);
            }
            if (cmb_Batch3.SelectedItem != null || cmb_Batch3.Text != "")
            {
                string str_day = "";
                string str_mth = "";
                if (dtp_Batch_Date3.Value.Day < 10)
                {
                    str_day = "0" + dtp_Batch_Date3.Value.Day.ToString();
                }
                else
                {
                    str_day = dtp_Batch_Date3.Value.Day.ToString();
                }
                if (dtp_Batch_Date3.Value.Month < 10)
                {
                    str_mth = "0" + dtp_Batch_Date3.Value.Month.ToString();
                }
                else
                {
                    str_mth = dtp_Batch_Date3.Value.Month.ToString();
                }
                str_batch = dtp_Batch_Date3.Value.Year.ToString() + str_mth + str_day;
                if (cmb_Batch3.SelectedItem == null)
                {
                    str_batch = str_batch + cmb_Batch3.Text.ToString();
                }
                else
                {
                    str_batch = str_batch + cmb_Batch3.SelectedItem.ToString();
                }
                int_result = FruPak.PF.Data.AccessLayer.PF_Pallet_Details.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Pallet_Details"), int_Pallet_Id, Convert.ToInt32(str_batch),
                                                                                 Convert.ToInt32(cmb_Material.SelectedValue.ToString()), int_Work_Order_Id, nud_Quantity3.Value, int_Current_User_Id);
                update_preSystemStock(Convert.ToInt32(cmb_Material.SelectedValue.ToString()), nud_Quantity3.Value);
            }
            return int_result;
        }
        private void update_preSystemStock(int Material_id, decimal Quantity)
        {
            DataSet ds = FruPak.PF.Data.AccessLayer.PF_PreSystem_Pallet_Stock.Get_Info(Material_id);
            DataRow dr;
            decimal dec_current_total = 0;
            int int_PrePalletStock_ID = 0;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dr = ds.Tables[0].Rows[i];
                int_PrePalletStock_ID = Convert.ToInt32(dr["PrePalletStock_ID"].ToString());
                dec_current_total = Convert.ToDecimal(dr["Quantity"].ToString());
            }

            dec_current_total = dec_current_total - Quantity;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (dec_current_total > 0)
                {
                    FruPak.PF.Data.AccessLayer.PF_PreSystem_Pallet_Stock.Update(int_PrePalletStock_ID,dec_current_total,int_Current_User_Id);
                }
                else
                {
                    FruPak.PF.Data.AccessLayer.PF_PreSystem_Pallet_Stock.Update(int_PrePalletStock_ID,0,int_Current_User_Id);
                }
            }
        }
        private void btn_reset_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void Reset()
        {
            populate_Combobox();

            nud_Quantity1.Value = 0;
            nud_Quantity2.Value = 0;
            nud_Quantity3.Value = 0;

            lbl_Location.Visible = true;
            lbl_Pallet_Type.Visible = true;
            cmb_Location.Visible = true;
            cmb_Pallet_Type.Visible = true;
            cmb_Batch2.Visible = true;
            nud_Quantity2.Visible = true;
            cmb_Batch3.Visible = true;
            nud_Quantity3.Visible = true;
            dtp_Batch_Date1.Value = DateTime.Now;
            btn_Add.Text = "&Add";
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
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int int_result = 0;
            #region ------------- Print -------------
            if (e.ColumnIndex == 8)
            {

                Cursor = Cursors.WaitCursor;
                FruPak.PF.Common.Code.General.Get_Printer("A4");
                FruPak.PF.PrintLayer.Pallet_Card.Print(dataGridView1.Rows[e.RowIndex].Cells["Barcode"].Value.ToString(), true, int_Current_User_Id);
                FruPak.PF.Data.AccessLayer.PF_Pallet.Update_For_PCard(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Pallet_Id"].Value.ToString()), true, int_Current_User_Id);
                Cursor = Cursors.Default;
                #region ------------- print and cleanup -------------
                //Clean Up

                // Modified for alterable path setting via config file. We do not approve of hardcoded strings. 12-01-2015 BN
                //lst_filenames.AddRange(System.IO.Directory.GetFiles(@"\\FRUPAK-SBS\PublicDocs\FruPak\Server\Temp", "*" + dataGridView1.Rows[e.RowIndex].Cells["Barcode"].Value.ToString() + "*", System.IO.SearchOption.TopDirectoryOnly));

                string tempPath = Settings.Path_Printer_Temp;
                lst_filenames.AddRange(System.IO.Directory.GetFiles(tempPath, "*" + dataGridView1.Rows[e.RowIndex].Cells["Barcode"].Value.ToString() + "*", System.IO.SearchOption.TopDirectoryOnly));

                foreach (string filename in lst_filenames)
                {
                    File.Delete(filename);
                }
                #endregion
            }
            #endregion
            #region ------------- Delete -------------
            else if (e.ColumnIndex == 9)
            {
                string str_msg;
                DialogResult DLR_Message = new System.Windows.Forms.DialogResult();
                str_msg = "You are about to Delete Pallet Number ";
                str_msg = str_msg + dataGridView1.Rows[e.RowIndex].Cells["Pallet_Id"].Value.ToString();
                str_msg = str_msg + " and its contents " + Environment.NewLine;
                str_msg = str_msg + "Do you want to continue?";
                DLR_Message = MessageBox.Show(str_msg, "Process Factory - Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (DLR_Message == DialogResult.Yes)
                {
                    int_result = FruPak.PF.Data.AccessLayer.PF_Pallet_Details.Delete_Pallet(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Pallet_Id"].Value.ToString()));
                    int_result = FruPak.PF.Data.AccessLayer.PF_Pallet.Delete(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Pallet_Id"].Value.ToString()));

                }
                if (int_result > 0)
                {
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    lbl_message.Text = "Pallet " + dataGridView1.Rows[e.RowIndex].Cells["Pallet_Id"].Value.ToString() + " has been Deleted";
                }
                else
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = "Pallet " + dataGridView1.Rows[e.RowIndex].Cells["Pallet_Id"].Value.ToString() + " failed to be Deleted";
                }
                populate_datagridview();
            }
            #endregion
            #region ------------- Edit -------------
            else if (e.ColumnIndex == 10)
            {
                int_DVG_Row_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                cmb_Pallet_Type.SelectedValue = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                cmb_Location.SelectedValue = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                btn_Add.Text = "&Update";
            }
            #endregion
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            #region ------------- Delete -------------
            if (e.ColumnIndex == 6)
            {
                string str_msg;
                int int_result = 0;
                DialogResult DLR_Message = new System.Windows.Forms.DialogResult();

                str_msg = "You are about to Delete Pallet Number ";
                str_msg = str_msg + dataGridView1.Rows[e.RowIndex].Cells["PalletDetails_Id"].Value.ToString();
                str_msg = str_msg + " and its contents " + Environment.NewLine;
                str_msg = str_msg + "Do you want to continue?";
                DLR_Message = MessageBox.Show(str_msg, "Process Factory - Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (DLR_Message == DialogResult.Yes)
                {
                    int_result = FruPak.PF.Data.AccessLayer.PF_Pallet_Details.Delete(Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["PalletDetails_Id"].Value.ToString()));

                }
                if (int_result > 0)
                {
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    lbl_message.Text = "Pallet Details" + dataGridView2.Rows[e.RowIndex].Cells["PalletDetails_Id"].Value.ToString() + " has been Deleted";
                }
                else
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = "Pallet Details" + dataGridView2.Rows[e.RowIndex].Cells["PalletDetails_Id"].Value.ToString() + " failed to be Deleted";
                }
                populate_datagridview2();


            }
            #endregion
            #region ------------- Edit -------------
            else if (e.ColumnIndex == 7)
            {
                int_DVG_Row_id = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString());
                cmb_Material.SelectedValue = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString());
                cmb_Batch1.SelectedItem = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString().Substring(8));
                nud_Quantity1.Value = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[5].Value.ToString());

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
        private void dtp_Batch_Date1_ValueChanged(object sender, EventArgs e)
        {
            dtp_Batch_Date2.Value = dtp_Batch_Date1.Value;
            dtp_Batch_Date3.Value = dtp_Batch_Date1.Value;
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
        private void BLWO_Labels_KeyDown(object sender, KeyEventArgs e)
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
