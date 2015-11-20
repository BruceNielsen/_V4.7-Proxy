using NLog;
using System;
using System.Data;
using System.Windows.Forms;

namespace PF.Utils.Common
{
    public partial class Material : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static bool bol_write_access;
        private static int int_Current_User_Id = 0;

        public Material(int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();

            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            {
                Console.WriteLine("PF.Utils.Common.Material - UsageMode = Designtime - Skipping populate()");
            }
            else
            {
                Console.WriteLine("PF.Utils.Common.Material - UsageMode = Runtime - Running populate()");
                //populate_comboboxs();
                //}

                int_Current_User_Id = int_C_User_id;
                //check if testing or not

                //if (PF.Global.Global.bol_Testing == true)
                //{
                //    this.Text = "FP Process Factory - " + this.Text + " - Test Environment";
                //}
                //else
                //{
                //    this.Text = "FP Process Factory";
                //}
                //restrict access
                bol_write_access = bol_w_a;
                btn_Add.Enabled = bol_w_a;

                populate_comboboxs();
                txt_Material_No.Text = "";
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
                else if (c.GetType() == typeof(PF.Utils.UserControls.Customer))
                {
                    PF.Utils.UserControls.Customer cust = (PF.Utils.UserControls.Customer)c;
                    cust.CustomerChanged += new EventHandler(this.CustomerControl_CustomerChanged);
                }
            }
            #endregion Log any interesting events from the UI to the CSV log file
        }

        public void populate_comboboxs()
        {
            fruit1.FruitType_Id = 0;
            fruit1.FruitVariety_Id = 0;

            DataSet ds_Get_Info;
            //Brand
            ds_Get_Info = PF.Data.AccessLayer.CM_Brand.Get_Info();
            cmb_Brand.DataSource = ds_Get_Info.Tables[0];
            cmb_Brand.DisplayMember = "Combined";
            cmb_Brand.ValueMember = "Brand_Id";
            cmb_Brand.Text = null;
            ds_Get_Info.Dispose();

            //Count
            ds_Get_Info = PF.Data.AccessLayer.CM_Count.Get_Info();
            cmb_Count.DataSource = ds_Get_Info.Tables[0];
            cmb_Count.DisplayMember = "Combined";
            cmb_Count.ValueMember = "Count_Id";
            cmb_Count.Text = null;
            ds_Get_Info.Dispose();

            //Grade
            ds_Get_Info = PF.Data.AccessLayer.CM_Grade.Get_Info();
            cmb_Grade.DataSource = ds_Get_Info.Tables[0];
            cmb_Grade.DisplayMember = "Combined";
            cmb_Grade.ValueMember = "Grade_Id";
            cmb_Grade.Text = null;
            ds_Get_Info.Dispose();

            //Growing Method
            ds_Get_Info = PF.Data.AccessLayer.CM_Growing_Method.Get_Info();
            cmb_GrowingMethod.DataSource = ds_Get_Info.Tables[0];
            cmb_GrowingMethod.DisplayMember = "Combined";
            cmb_GrowingMethod.ValueMember = "Growing_Method_Id";
            cmb_GrowingMethod.Text = null;
            ds_Get_Info.Dispose();

            //MArke Attr
            ds_Get_Info = PF.Data.AccessLayer.CM_Market_Attribute.Get_Info();
            cmb_MarketAttrib.DataSource = ds_Get_Info.Tables[0];
            cmb_MarketAttrib.DisplayMember = "Combined";
            cmb_MarketAttrib.ValueMember = "Market_Attribute_Id";
            cmb_MarketAttrib.Text = null;
            ds_Get_Info.Dispose();

            //Pack Type
            ds_Get_Info = PF.Data.AccessLayer.CM_Pack_Type.Get_Info();
            cmb_PackType.DataSource = ds_Get_Info.Tables[0];
            cmb_PackType.DisplayMember = "Combined";
            cmb_PackType.ValueMember = "PackType_Id";
            cmb_PackType.Text = null;
            ds_Get_Info.Dispose();

            //Pallet Type
            ds_Get_Info = PF.Data.AccessLayer.CM_Pallet_Type.Get_Info();
            cmb_Pallet_Type.DataSource = ds_Get_Info.Tables[0];
            cmb_Pallet_Type.DisplayMember = "Combined";
            cmb_Pallet_Type.ValueMember = "PalletType_Id";
            cmb_Pallet_Type.Text = null;
            ds_Get_Info.Dispose();

            //Product Group
            ds_Get_Info = PF.Data.AccessLayer.CM_Product_Group.Get_Info();
            cmb_Product_Group.DataSource = ds_Get_Info.Tables[0];
            cmb_Product_Group.DisplayMember = "Combined";
            cmb_Product_Group.ValueMember = "ProductGroup_Id";
            cmb_Product_Group.Text = null;
            ds_Get_Info.Dispose();

            //Size
            ds_Get_Info = PF.Data.AccessLayer.CM_Size.Get_Info();
            cmb_Size.DataSource = ds_Get_Info.Tables[0];
            cmb_Size.DisplayMember = "Combined";
            cmb_Size.ValueMember = "Size_Id";
            cmb_Size.Text = null;
            ds_Get_Info.Dispose();

            //Trader
            ds_Get_Info = PF.Data.AccessLayer.CM_Trader.Get_Info();
            cmb_Trader.DataSource = ds_Get_Info.Tables[0];
            cmb_Trader.DisplayMember = "Combined";
            cmb_Trader.ValueMember = "Trader_Id";
            cmb_Trader.Text = null;
            ds_Get_Info.Dispose();

            //Treatment
            ds_Get_Info = PF.Data.AccessLayer.CM_Treatment.Get_Info();
            cmb_Treatment.DataSource = ds_Get_Info.Tables[0];
            cmb_Treatment.DisplayMember = "Combined";
            cmb_Treatment.ValueMember = "Treatment_Id";
            cmb_Treatment.Text = null;
            ds_Get_Info.Dispose();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (Add_btn() > 0)
            {
                btn_Relationships();
            }
        }

        private int Add_btn()
        {
            DialogResult DLR_MessageBox = new DialogResult();
            string str_msg = "";
            int int_result = 0;

            if (txt_Material_No.TextLength == 0)
            {
                str_msg = str_msg + "Invalid Material Number: Please enter a valid Material Number" + Environment.NewLine;
            }
            else
            {
                try
                {
                    DataSet ds_Get_Info = PF.Data.AccessLayer.CM_Material.Check_Mat_num_Trader(Convert.ToDecimal(txt_Material_No.Text), Convert.ToInt32(cmb_Trader.SelectedValue.ToString()));

                    if (Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()) > 0)
                    {
                        str_msg = str_msg + "Invalid Material Number: The Materail number entered has already been used. Please enter a valid Material Number" + Environment.NewLine;
                    }
                }
                catch
                {
                    str_msg = str_msg + "Invalid Material Number: The Materail number must be numeric only. Please enter a valid Material Number" + Environment.NewLine;
                }
            }

            if (cmb_Brand.SelectedValue == null)
            {
                str_msg = str_msg + "Invalid Brand: Please select a valid Brand from the dropdown list" + Environment.NewLine;
            }
            if (cmb_Count.SelectedValue == null)
            {
                str_msg = str_msg + "Invalid Count: Please select a valid Count from the dropdown list" + Environment.NewLine;
            }
            if (cmb_Grade.SelectedValue == null)
            {
                str_msg = str_msg + "Invalid Grade: Please select a valid Grade from the dropdown list" + Environment.NewLine;
            }
            if (cmb_GrowingMethod.SelectedValue == null)
            {
                str_msg = str_msg + "Invalid Growing Method: Please select a valid Growing Method from the dropdown list" + Environment.NewLine;
            }
            if (cmb_MarketAttrib.SelectedValue == null)
            {
                str_msg = str_msg + "Invalid Market Attribute: Please select a valid Market Attribute from the dropdown list" + Environment.NewLine;
            }
            if (cmb_PackType.SelectedValue == null)
            {
                str_msg = str_msg + "Invalid Pack Type: Please select a valid Pack Type from the dropdown list" + Environment.NewLine;
            }
            if (cmb_Pallet_Type.SelectedValue == null)
            {
                str_msg = str_msg + "Invalid Pallet Type: Please select a valid Pack Type from the dropdown list" + Environment.NewLine;
            }
            if (cmb_Product_Group.SelectedValue == null)
            {
                str_msg = str_msg + "Invalid Product Group: Please select a valid Product Group from the dropdown list" + Environment.NewLine;
            }
            if (cmb_Size.SelectedValue == null)
            {
                str_msg = str_msg + "Invalid Size: Please select a valid Size from the dropdown list" + Environment.NewLine;
            }
            if (cmb_Trader.SelectedValue == null)
            {
                str_msg = str_msg + "Invalid Trader: Please select a valid Trader from the dropdown list" + Environment.NewLine;
            }
            if (cmb_Treatment.SelectedValue == null)
            {
                str_msg = str_msg + "Invalid Treatment: Please select a valid Treatment from the dropdown list" + Environment.NewLine;
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
            if (str_msg.Length > 0)
            {
                DLR_MessageBox = MessageBox.Show(str_msg, "Common - Material Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (DLR_MessageBox != DialogResult.OK)
            {
                DataSet ds = PF.Data.AccessLayer.CM_Material.Does_It_Exist(Convert.ToInt32(cmb_Brand.SelectedValue.ToString()), Convert.ToInt32(cmb_Count.SelectedValue.ToString()), int_FruitType_Id, Convert.ToInt32(cmb_Grade.SelectedValue.ToString()),
                    Convert.ToInt32(cmb_GrowingMethod.SelectedValue.ToString()), Convert.ToInt32(cmb_MarketAttrib.SelectedValue.ToString()), Convert.ToInt32(cmb_PackType.SelectedValue.ToString()), Convert.ToInt32(cmb_Pallet_Type.SelectedValue.ToString()),
                    Convert.ToInt32(cmb_Product_Group.SelectedValue.ToString()), Convert.ToInt32(cmb_Size.SelectedValue.ToString()), Convert.ToInt32(cmb_Treatment.SelectedValue.ToString()), int_FruitVariety_Id, Convert.ToInt32(cmb_Trader.SelectedValue.ToString()));
                DataRow dr;
                str_msg = "";
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dr = ds.Tables[0].Rows[i];
                    str_msg = str_msg + dr["Material_Num"].ToString() + " - " + dr["M_Description"].ToString() + " Trader = " + dr["T_Description"].ToString() + Environment.NewLine;
                }
                ds.Dispose();

                if (str_msg.Length > 0)
                {
                    DLR_MessageBox = MessageBox.Show("The Following Material Number already for the combination you supplied." + Environment.NewLine + str_msg, "Process Factory - Common (Material Number)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (DLR_MessageBox != System.Windows.Forms.DialogResult.OK)
                {
                    int_result = PF.Data.AccessLayer.CM_Material.insert(PF.Common.Code.General.int_max_user_id("CM_Material"), Convert.ToDecimal(txt_Material_No.Text), Convert.ToInt32(cmb_Trader.SelectedValue.ToString()), int_FruitType_Id, int_FruitVariety_Id,
                                                                            Convert.ToInt32(cmb_PackType.SelectedValue.ToString()), Convert.ToInt32(cmb_Size.SelectedValue.ToString()), Convert.ToInt32(cmb_Count.SelectedValue.ToString()),
                                                                            Convert.ToInt32(cmb_Brand.SelectedValue.ToString()), Convert.ToInt32(cmb_Grade.SelectedValue.ToString()), Convert.ToInt32(cmb_Treatment.SelectedValue.ToString()),
                                                                            Convert.ToInt32(cmb_MarketAttrib.SelectedValue.ToString()), Convert.ToInt32(cmb_GrowingMethod.SelectedValue.ToString()), Convert.ToInt32(cmb_Product_Group.SelectedValue.ToString()),
                                                                            Convert.ToDecimal(nud_weight.Value), txt_Description.Text, Convert.ToBoolean(ckb_Active.Checked), int_Current_User_Id, Convert.ToInt32(cmb_Pallet_Type.SelectedValue.ToString()));
                }
            }
            if (int_result > 0)
            {
                lbl_message.Text = "Material Number " + txt_Material_No.Text + " has been added";
                lbl_message.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                lbl_message.Text = "Material Number " + txt_Material_No.Text + " failed to be added";
                lbl_message.ForeColor = System.Drawing.Color.Red;
            }
            return int_result;
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            txt_Material_No.ResetText();
            txt_Description.ResetText();
            cmb_Brand.Text = null;
            cmb_Count.Text = null;
            cmb_Grade.Text = null;
            cmb_GrowingMethod.Text = null;
            cmb_MarketAttrib.Text = null;
            cmb_PackType.Text = null;
            cmb_Pallet_Type.Text = null;
            cmb_Product_Group.Text = null;
            cmb_Size.Text = null;
            cmb_Trader.Text = null;
            cmb_Treatment.Text = null;
            nud_weight.Value = 0;
            fruit1.FruitType_Id = 0;
            fruit1.FruitVariety_Id = 0;
            btn_Add.Text = "&Add";
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            int int_cmb_Brand = 0;
            if (cmb_Count.SelectedValue != null)
            {
                int_cmb_Brand = Convert.ToInt32(cmb_Brand.SelectedValue.ToString());
            }

            int int_cmb_Count = 0;
            if (cmb_Count.SelectedValue != null)
            {
                int_cmb_Count = Convert.ToInt32(cmb_Count.SelectedValue.ToString());
            }

            int int_cmb_Grade = 0;
            if (cmb_Grade.SelectedValue != null)
            {
                int_cmb_Grade = Convert.ToInt32(cmb_Grade.SelectedValue.ToString());
            }

            int int_cmb_GrowingMethod = 0;
            if (cmb_GrowingMethod.SelectedValue != null)
            {
                int_cmb_GrowingMethod = Convert.ToInt32(cmb_GrowingMethod.SelectedValue.ToString());
            }

            int int_cmb_MarketAttrib = 0;
            if (cmb_MarketAttrib.SelectedValue != null)
            {
                int_cmb_MarketAttrib = Convert.ToInt32(cmb_MarketAttrib.SelectedValue.ToString());
            }

            int int_cmb_PackType = 0;
            if (cmb_PackType.SelectedValue != null)
            {
                int_cmb_PackType = Convert.ToInt32(cmb_PackType.SelectedValue.ToString());
            }

            int int_cmb_Pallet_Type = 0;
            if (cmb_Pallet_Type.SelectedValue != null)
            {
                int_cmb_Pallet_Type = Convert.ToInt32(cmb_Pallet_Type.SelectedValue.ToString());
            }

            int int_cmb_Product_Group = 0;
            if (cmb_Product_Group.SelectedValue != null)
            {
                int_cmb_Product_Group = Convert.ToInt32(cmb_Product_Group.SelectedValue.ToString());
            }

            int int_cmb_Size = 0;
            if (cmb_Size.SelectedValue != null)
            {
                int_cmb_Size = Convert.ToInt32(cmb_Size.SelectedValue.ToString());
            }

            int int_cmb_Trader = 0;
            if (cmb_Trader.SelectedValue != null)
            {
                int_cmb_Trader = Convert.ToInt32(cmb_Trader.SelectedValue.ToString());
            }

            int int_cmb_Treatment = 0;
            if (cmb_Treatment.SelectedValue != null)
            {
                int_cmb_Treatment = Convert.ToInt32(cmb_Treatment.SelectedValue.ToString());
            }
            populate_comboboxs();

            cmb_Brand.SelectedValue = int_cmb_Brand;
            cmb_Count.SelectedValue = int_cmb_Count;
            cmb_Grade.SelectedValue = int_cmb_Grade;
            cmb_GrowingMethod.SelectedValue = int_cmb_GrowingMethod;
            cmb_MarketAttrib.SelectedValue = int_cmb_MarketAttrib;
            cmb_PackType.SelectedValue = int_cmb_PackType;
            cmb_Pallet_Type.SelectedValue = int_cmb_Pallet_Type;
            cmb_Product_Group.SelectedValue = int_cmb_Product_Group;
            cmb_Size.SelectedValue = int_cmb_Size;
            cmb_Trader.SelectedValue = int_cmb_Trader;
            cmb_Treatment.SelectedValue = int_cmb_Treatment;
        }

        private void btn_reltn_Click(object sender, EventArgs e)
        {
            btn_Relationships();
        }

        private void btn_Relationships()
        {
            Form frm = new Material_Relationship_Maint(int_Current_User_Id, bol_write_access);
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
        }

        private void KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void Enter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Add_btn();
            }
        }

        private void combo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            combo_Selection((sender as ComboBox).Name);
        }

        private void combo_Selection(string str_combo_name)
        {
            DataSet ds = null;
            switch (str_combo_name)
            {
                #region cmb_Brand
                case "cmb_Brand":
                    if (cmb_Brand.SelectedText != string.Empty)
                    {
                        if (cmb_Brand.SelectedValue != null)
                        {
                            try
                            {
                                ds = PF.Data.AccessLayer.CM_Brand.Get_Info(Convert.ToInt32(cmb_Brand.SelectedValue.ToString()));
                            }
                            catch (Exception ex)
                            {
                                logger.Log(LogLevel.Debug, ex.Message);
                            }
                        }
                        else
                        {
                            // Phantom 15/12/2014
                            logger.Log(LogLevel.Warn, "Avoided error due to cmb_Brand SelectedValue being null");
                        }
                    }
                    else
                    {
                        // Phantom 15/12/2014
                        logger.Log(LogLevel.Warn, "Avoided error due to cmb_Brand SelectedText being null");
                    }
                    break;
                #endregion cmb_Brand

                #region cmb_Grade
                case "cmb_Grade":
                    if (cmb_Grade.SelectedText != string.Empty)
                    {
                        if (cmb_Grade.SelectedValue != null)
                        {
                            try { ds = PF.Data.AccessLayer.CM_Grade.Get_Info(Convert.ToInt32(cmb_Grade.SelectedValue.ToString())); }
                            catch (Exception ex)
                            {
                                logger.Log(LogLevel.Debug, ex.Message);
                            }
                        }
                        else
                        {
                            // Phantom 15/12/2014
                            logger.Log(LogLevel.Warn, "Avoided error due to cmb_Grade SelectedValue being null");
                        }
                    }
                    else
                    {
                        // Phantom 15/12/2014
                        logger.Log(LogLevel.Warn, "Avoided error due to cmb_Grade SelectedText being null");
                    }
                    break;

                #endregion cmb_Grade

                #region cmb_GrowingMethod
                case "cmb_GrowingMethod":
                    if (cmb_GrowingMethod.SelectedText != string.Empty)
                    {
                        if (cmb_GrowingMethod.SelectedValue != null)
                        {
                            try { ds = PF.Data.AccessLayer.CM_Growing_Method.Get_Info(Convert.ToInt32(cmb_GrowingMethod.SelectedValue.ToString())); }
                            catch (Exception ex)
                            {
                                logger.Log(LogLevel.Debug, ex.Message);
                            }
                        }
                        else
                        {
                            // Phantom 15/12/2014
                            logger.Log(LogLevel.Warn, "Avoided error due to cmb_GrowingMethod SelectedValue being null");
                        }
                    }
                    else
                    {
                        // Phantom 15/12/2014
                        logger.Log(LogLevel.Warn, "Avoided error due to cmb_GrowingMethod SelectedText being null");
                    }
                    break;

                #endregion cmb_GrowingMethod

                #region cmb_MarketAttrib
                case "cmb_MarketAttrib":
                    if (cmb_MarketAttrib.SelectedText != string.Empty)
                    {
                        if (cmb_MarketAttrib.SelectedValue != null)
                        {
                            try { ds = PF.Data.AccessLayer.CM_Market_Attribute.Get_Info(Convert.ToInt32(cmb_MarketAttrib.SelectedValue.ToString())); }
                            catch (Exception ex)
                            {
                                logger.Log(LogLevel.Debug, ex.Message);
                            }
                        }
                        else
                        {
                            // Phantom 15/12/2014
                            logger.Log(LogLevel.Warn, "Avoided error due to cmb_MarketAttrib SelectedValue being null");
                        }
                    }
                    else
                    {
                        // Phantom 15/12/2014
                        logger.Log(LogLevel.Warn, "Avoided error due to cmb_MarketAttrib SelectedText being null");
                    }
                    break;

                #endregion cmb_MarketAttrib

                #region cmb_PackType
                case "cmb_PackType":
                    if (cmb_PackType.SelectedText != string.Empty)
                    {
                        if (cmb_PackType.SelectedValue != null)
                        {
                            try { ds = PF.Data.AccessLayer.CM_Pack_Type.Get_Info(Convert.ToInt32(cmb_PackType.SelectedValue.ToString())); }
                            catch (Exception ex)
                            {
                                logger.Log(LogLevel.Debug, ex.Message);
                            }
                        }
                        else
                        {
                            // Phantom 15/12/2014
                            logger.Log(LogLevel.Warn, "Avoided error due to cmb_PackType SelectedValue being null");
                        }
                    }
                    else
                    {
                        // Phantom 15/12/2014
                        logger.Log(LogLevel.Warn, "Avoided error due to cmb_PackType SelectedText being null");
                    }
                    break;

                #endregion cmb_PackType

                #region cmb_Product_Group
                case "cmb_Product_Group":
                    if (cmb_Product_Group.SelectedText != string.Empty)
                    {
                        if (cmb_Product_Group.SelectedValue != null)
                        {
                            try { ds = PF.Data.AccessLayer.CM_Product_Group.Get_Info(Convert.ToInt32(cmb_Product_Group.SelectedValue.ToString())); }
                            catch (Exception ex)
                            {
                                logger.Log(LogLevel.Debug, ex.Message);
                            }
                        }
                        else
                        {
                            // Phantom 15/12/2014
                            logger.Log(LogLevel.Warn, "Avoided error due to cmb_Product_Group SelectedValue being null");
                        }
                    }
                    else
                    {
                        // Phantom 15/12/2014
                        logger.Log(LogLevel.Warn, "Avoided error due to cmb_Product_Group SelectedText being null");
                    }
                    break;

                #endregion cmb_Product_Group

                #region cmb_Size
                case "cmb_Size":
                    if (cmb_Size.SelectedText != string.Empty)
                    {
                        if (cmb_Size.SelectedValue != null)
                        {
                            try { ds = PF.Data.AccessLayer.CM_Size.Get_Info(Convert.ToInt32(cmb_Size.SelectedValue.ToString())); }
                            catch (Exception ex)
                            {
                                logger.Log(LogLevel.Debug, ex.Message);
                            }
                        }
                        else
                        {
                            // Phantom 15/12/2014
                            logger.Log(LogLevel.Warn, "Avoided error due to cmb_Size SelectedValue being null");
                        }
                    }
                    else
                    {
                        // Phantom 15/12/2014
                        logger.Log(LogLevel.Warn, "Avoided error due to cmb_Size SelectedText being null");
                    }
                    break;

                #endregion cmb_Size

                #region cmb_Trader
                case "cmb_Trader":
                    if (cmb_Trader.SelectedText != string.Empty)
                    {
                        if (cmb_Trader.SelectedValue != null)
                        {
                            try { ds = PF.Data.AccessLayer.CM_Trader.Get_Info(Convert.ToInt32(cmb_Trader.SelectedValue.ToString())); }
                            catch (Exception ex)
                            {
                                logger.Log(LogLevel.Debug, ex.Message);
                            }
                        }
                        else
                        {
                            // Phantom 15/12/2014
                            logger.Log(LogLevel.Warn, "Avoided error due to cmb_Trader SelectedValue being null");
                        }
                    }
                    else
                    {
                        // Phantom 15/12/2014
                        logger.Log(LogLevel.Warn, "Avoided error due to cmb_Trader SelectedText being null");
                    }
                    break;

                #endregion cmb_Trader

                #region cmb_Treatment
                case "cmb_Treatment":
                    if (cmb_Treatment.SelectedText != string.Empty)
                    {
                        if (cmb_Treatment.SelectedValue != null)
                        {
                            try { ds = PF.Data.AccessLayer.CM_Treatment.Get_Info(Convert.ToInt32(cmb_Treatment.SelectedValue.ToString())); }
                            catch (Exception ex)
                            {
                                logger.Log(LogLevel.Debug, ex.Message);
                            }
                        }
                        else
                        {
                            // Phantom 15/12/2014
                            logger.Log(LogLevel.Warn, "Avoided error due to cmb_Treatment SelectedValue being null");
                        }
                    }
                    else
                    {
                        // Phantom 15/12/2014
                        logger.Log(LogLevel.Warn, "Avoided error due to cmb_Treatment SelectedText being null");
                    }
                    break;

                    #endregion cmb_Treatment
            }
            try
            {
                DataRow dr;
                // Phantom 15/12/2014
                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dr = ds.Tables[0].Rows[i];
                        switch (str_combo_name)
                        {
                            case "cmb_Brand":
                                description.brand = dr["Code"].ToString();
                                switch (dr["Code"].ToString())
                                {
                                    case "GN":
                                        number.pos1 = 7;
                                        break;

                                    case "BA":
                                        number.pos1 = 6;
                                        break;
                                }
                                break;

                            case "cmb_Grade":
                                description.grade_code = dr["Code"].ToString();
                                number.grade_id = dr["Grade_Id"].ToString().PadLeft(2, '0');
                                break;

                            case "cmb_GrowingMethod":
                                description.growwing_method = dr["Code"].ToString();
                                break;

                            case "cmb_MarketAttrib":
                                description.market_attrib = dr["Code"].ToString();
                                break;
                            #region-------- Pack Type ------------
                            case "cmb_PackType":
                                description.pack_type = dr["Code"].ToString();
                                description.grade_full = dr["description"].ToString();
                                DataSet ds_pg = null;
                                DataRow dr_pg;
                                switch (dr["Code"].ToString().ToUpper())
                                {
                                    #region ---------- RND -------------
                                    case "RND":
                                        //defaults
                                        cmb_Brand.SelectedValue = 0;
                                        cmb_Pallet_Type.SelectedValue = 0;
                                        cmb_Product_Group.SelectedValue = 0;

                                        ds_pg = PF.Data.AccessLayer.CM_Product_Group.Get_Info("PB");
                                        for (int i_pg = 0; i_pg < ds_pg.Tables[0].Rows.Count; i_pg++)
                                        {
                                            dr_pg = ds_pg.Tables[0].Rows[i_pg];
                                            cmb_Product_Group.SelectedValue = dr_pg["ProductGroup_Id"].ToString();
                                        }
                                        ds_pg.Dispose();
                                        ds_pg = PF.Data.AccessLayer.CM_Brand.Get_Info("PB");
                                        for (int i_pg = 0; i_pg < ds_pg.Tables[0].Rows.Count; i_pg++)
                                        {
                                            dr_pg = ds_pg.Tables[0].Rows[i_pg];
                                            cmb_Brand.SelectedValue = dr_pg["Brand_Id"].ToString();
                                        }
                                        ds_pg.Dispose();
                                        ds_pg = PF.Data.AccessLayer.CM_Pallet_Type.Get_Info("FPB");
                                        for (int i_pg = 0; i_pg < ds_pg.Tables[0].Rows.Count; i_pg++)
                                        {
                                            dr_pg = ds_pg.Tables[0].Rows[i_pg];
                                            cmb_Pallet_Type.SelectedValue = dr_pg["PalletType_Id"].ToString();
                                        }
                                        ds_pg.Dispose();
                                        ds_pg = PF.Data.AccessLayer.CM_Size.Get_Info("MIX");
                                        for (int i_pg = 0; i_pg < ds_pg.Tables[0].Rows.Count; i_pg++)
                                        {
                                            dr_pg = ds_pg.Tables[0].Rows[i_pg];
                                            cmb_Size.SelectedValue = dr_pg["Size_Id"].ToString();
                                        }
                                        ds_pg.Dispose();
                                        ds_pg = PF.Data.AccessLayer.CM_Grade.Get_Info("UG");
                                        for (int i_pg = 0; i_pg < ds_pg.Tables[0].Rows.Count; i_pg++)
                                        {
                                            dr_pg = ds_pg.Tables[0].Rows[i_pg];
                                            cmb_Grade.SelectedValue = dr_pg["Grade_Id"].ToString();
                                        }
                                        ds_pg.Dispose();
                                        ds_pg = PF.Data.AccessLayer.CM_Market_Attribute.Get_Info("N");
                                        for (int i_pg = 0; i_pg < ds_pg.Tables[0].Rows.Count; i_pg++)
                                        {
                                            dr_pg = ds_pg.Tables[0].Rows[i_pg];
                                            cmb_MarketAttrib.SelectedValue = dr_pg["Market_Attribute_Id"].ToString();
                                        }
                                        ds_pg.Dispose();
                                        break;
                                    #endregion ---------- RND -------------
                                    #region ---------- PHB -----------------
                                    case "PHB":
                                        ds_pg = PF.Data.AccessLayer.CM_Product_Group.Get_Info("PFP");
                                        for (int i_pg = 0; i_pg < ds_pg.Tables[0].Rows.Count; i_pg++)
                                        {
                                            dr_pg = ds_pg.Tables[0].Rows[i_pg];
                                            cmb_Product_Group.SelectedValue = dr_pg["ProductGroup_Id"].ToString();
                                        }
                                        ds_pg.Dispose();
                                        ds_pg = PF.Data.AccessLayer.CM_Brand.Get_Info("BA");
                                        for (int i_pg = 0; i_pg < ds_pg.Tables[0].Rows.Count; i_pg++)
                                        {
                                            dr_pg = ds_pg.Tables[0].Rows[i_pg];
                                            cmb_Brand.SelectedValue = dr_pg["Brand_Id"].ToString();
                                        }
                                        ds_pg.Dispose();
                                        ds_pg = PF.Data.AccessLayer.CM_Pallet_Type.Get_Info("FPB");
                                        for (int i_pg = 0; i_pg < ds_pg.Tables[0].Rows.Count; i_pg++)
                                        {
                                            dr_pg = ds_pg.Tables[0].Rows[i_pg];
                                            cmb_Pallet_Type.SelectedValue = dr_pg["PalletType_Id"].ToString();
                                        }
                                        ds_pg.Dispose();
                                        ds_pg = PF.Data.AccessLayer.CM_Grade.Get_Info("HF");
                                        for (int i_pg = 0; i_pg < ds_pg.Tables[0].Rows.Count; i_pg++)
                                        {
                                            dr_pg = ds_pg.Tables[0].Rows[i_pg];
                                            cmb_Grade.SelectedValue = dr_pg["Grade_Id"].ToString();
                                        }
                                        ds_pg.Dispose();
                                        ds_pg = PF.Data.AccessLayer.CM_Market_Attribute.Get_Info("HB");
                                        for (int i_pg = 0; i_pg < ds_pg.Tables[0].Rows.Count; i_pg++)
                                        {
                                            dr_pg = ds_pg.Tables[0].Rows[i_pg];
                                            cmb_MarketAttrib.SelectedValue = dr_pg["Market_Attribute_Id"].ToString();
                                        }
                                        ds_pg.Dispose();
                                        break;
                                    #endregion ---------- PHB -----------------
                                    #region ----------- PHC -------------
                                    case "PHC":
                                        ds_pg = PF.Data.AccessLayer.CM_Product_Group.Get_Info("PFP");
                                        for (int i_pg = 0; i_pg < ds_pg.Tables[0].Rows.Count; i_pg++)
                                        {
                                            dr_pg = ds_pg.Tables[0].Rows[i_pg];
                                            cmb_Product_Group.SelectedValue = dr_pg["ProductGroup_Id"].ToString();
                                        }
                                        ds_pg.Dispose();
                                        ds_pg = PF.Data.AccessLayer.CM_Brand.Get_Info("GN");
                                        for (int i_pg = 0; i_pg < ds_pg.Tables[0].Rows.Count; i_pg++)
                                        {
                                            dr_pg = ds_pg.Tables[0].Rows[i_pg];
                                            cmb_Brand.SelectedValue = dr_pg["Brand_Id"].ToString();
                                        }
                                        ds_pg.Dispose();
                                        ds_pg = PF.Data.AccessLayer.CM_Pallet_Type.Get_Info("BCHEP");
                                        for (int i_pg = 0; i_pg < ds_pg.Tables[0].Rows.Count; i_pg++)
                                        {
                                            dr_pg = ds_pg.Tables[0].Rows[i_pg];
                                            cmb_Pallet_Type.SelectedValue = dr_pg["PalletType_Id"].ToString();
                                        }
                                        ds_pg.Dispose();
                                        ds_pg = PF.Data.AccessLayer.CM_Grade.Get_Info("HF");
                                        for (int i_pg = 0; i_pg < ds_pg.Tables[0].Rows.Count; i_pg++)
                                        {
                                            dr_pg = ds_pg.Tables[0].Rows[i_pg];
                                            cmb_Grade.SelectedValue = dr_pg["Grade_Id"].ToString();
                                        }
                                        ds_pg.Dispose();
                                        ds_pg = PF.Data.AccessLayer.CM_Market_Attribute.Get_Info("HB");
                                        for (int i_pg = 0; i_pg < ds_pg.Tables[0].Rows.Count; i_pg++)
                                        {
                                            dr_pg = ds_pg.Tables[0].Rows[i_pg];
                                            cmb_MarketAttrib.SelectedValue = dr_pg["Market_Attribute_Id"].ToString();
                                        }
                                        ds_pg.Dispose();
                                        break;
                                        #endregion ----------- PHC -------------
                                }
                                break;
                            #endregion
                            case "cmb_Product_Group":
                                switch (dr["Code"].ToString())
                                {
                                    default:
                                        number.pos1 = 5;
                                        number.product_id = dr["ProductGroup_Id"].ToString().PadLeft(2, '0');
                                        break;

                                    case "PFP":
                                        number.pos1 = 6;
                                        number.product_id = "00";
                                        break;
                                }
                                break;

                            case "cmb_Size":
                                description.size = dr["Code"].ToString();
                                break;

                            case "cmb_Trader":
                                description.trader = dr["Code"].ToString();
                                break;

                            case "cmb_Treatment":
                                description.treatment = dr["Code"].ToString();
                                break;
                        }
                    }
                    ds.Dispose();
                }
                else
                {
                    // Phantom 15/12/2014
                    logger.Log(LogLevel.Warn, "Avoided error due to the DataSet being null");
                }
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message);
            }

            txt_Description.Text = create_text();
            txt_Material_No.Text = create_number();
        }

        private void fruit1_FruitTypeChanged(object sender, EventArgs e)
        {
            string str_ft = "";
            str_ft = Convert.ToString(fruit1.FruitType_Id);
            number.fruittype = str_ft.ToString().PadLeft(2, '0');

            txt_Description.Text = create_text();
            txt_Material_No.Text = create_number();
        }

        private void fruit1_FruitVarietyChanged(object sender, EventArgs e)
        {
            DataSet ds = PF.Data.AccessLayer.CM_Fruit_Variety.Get_Info_std(Convert.ToInt32(fruit1.FruitVariety_Id));
            DataRow dr;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dr = ds.Tables[0].Rows[i];
                description.variety = dr["Code"].ToString() + dr["Description"].ToString();
            }
            ds.Dispose();
            txt_Description.Text = create_text();
            txt_Material_No.Text = create_number();
        }

        private string create_text()
        {
            string str_text = "";
            str_text = description.grade_full + " - " + description.trader + " " + description.brand + " " + description.variety + " " + description.size + " " + description.pack_type + " " +
                       description.growwing_method + " " + description.grade_code + " " + description.market_attrib + description.treatment;
            return str_text;
        }

        private string create_number()
        {
            string str_text = "";
            if (number.pos1 == 6 || number.pos1 == 7)
            {
                number.product_id = number.grade_id;
            }

            DataSet ds = PF.Data.AccessLayer.CM_Material.Get_Max_Material_Num(Convert.ToInt32(number.pos1.ToString() + number.product_id + number.fruittype + "000"), Convert.ToInt32(number.pos1.ToString() + number.product_id + number.fruittype + "999"));
            DataRow dr;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dr = ds.Tables[0].Rows[i];
                try
                {
                    str_text = Convert.ToString(Convert.ToInt32(dr["Max"].ToString()) + 1);
                }
                catch
                {
                    str_text = number.pos1.ToString() + number.product_id + number.fruittype + "001";
                }
            }
            return str_text;
        }

        private void txt_Material_No_Leave(object sender, EventArgs e)
        {
            string str_temp_material_Num = "";
            if (txt_Material_No.TextLength > 0)
            {
                str_temp_material_Num = txt_Material_No.Text;
                DataSet ds = PF.Data.AccessLayer.CM_Material.Get_Info(Convert.ToDecimal(txt_Material_No.Text));
                DataRow dr;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dr = ds.Tables[0].Rows[i];
                    txt_Description.Text = dr["Description"].ToString();
                    fruit1.FruitType_Id = Convert.ToInt32(dr["FruitType_Id"].ToString());
                    fruit1.FruitVariety_Id = Convert.ToInt32(dr["Variety_Id"].ToString());
                    cmb_Brand.SelectedValue = Convert.ToInt32(dr["Brand_Id"].ToString());
                    cmb_Count.SelectedValue = Convert.ToInt32(dr["Count_Id"].ToString());
                    cmb_Grade.SelectedValue = Convert.ToInt32(dr["Grade_Id"].ToString());
                    cmb_GrowingMethod.SelectedValue = Convert.ToInt32(dr["Growing_Method_Id"].ToString());
                    cmb_MarketAttrib.SelectedValue = Convert.ToInt32(dr["Market_Attribute_Id"].ToString());
                    cmb_PackType.SelectedValue = Convert.ToInt32(dr["PackType_Id"].ToString());
                    cmb_Pallet_Type.SelectedValue = Convert.ToInt32(dr["PalletType_Id"].ToString());
                    cmb_Product_Group.SelectedValue = Convert.ToInt32(dr["ProductGroup_Id"].ToString());
                    cmb_Size.SelectedValue = Convert.ToInt32(dr["Size_Id"].ToString());
                    cmb_Treatment.SelectedValue = Convert.ToInt32(dr["Treatment_Id"].ToString());
                    nud_weight.Value = Convert.ToDecimal(dr["Weight"].ToString());
                }
                ds.Dispose();

                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is ComboBox)
                    {
                        if (ctrl.Name != "cmb_Trader")
                        {
                            combo_Selection(ctrl.Name);
                        }
                    }
                }

                //reset Material Number back to the one entered in the first place
                txt_Material_No.Text = str_temp_material_Num;
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

        #endregion

        /// <summary>
        /// Close the form with the Esc key (Sel request 11-02-2015 BN)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Material_KeyDown(object sender, KeyEventArgs e)
        {
            // Note that the KeyPreview property must be set to true for this to work
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        #endregion
    }

    public class description
    {
        public static string brand { get; set; }
        public static string grade_full { get; set; }
        public static string grade_code { get; set; }
        public static string growwing_method { get; set; }
        public static string market_attrib { get; set; }
        public static string pack_type { get; set; }
        public static string size { get; set; }
        public static string trader { get; set; }
        public static string treatment { get; set; }
        public static string variety { get; set; }
    }

    public class number
    {
        public static int pos1 { get; set; }
        public static string product_id { get; set; }
        public static string grade_id { get; set; }
        public static string fruittype { get; set; }
    }
}