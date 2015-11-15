using NLog;
using System;
using System.Data;
using System.Windows.Forms;

namespace FruPak.PF.WorkOrder
{
    public partial class WO_Output : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static bool bol_write_access;
        private static int int_Current_User_Id = 0;
        private static int int_Work_Order_Id = 0;
        private static int int_result = 0;

        private int selectedTrader = 0;
        private int selectedProductGroup = 0;
        private int selectedPackType = 0;
        private int selectedBrand = 0;
        private decimal selectedWeight = 0;
        private int selectedGrade = 0;
        private int selectedTreatment = 0;
        private int selectedMaterial = 0;
        private int selectedVariety = 0;
        private int selectedGrowingMethod = 0;
        private int selectedSize = 0;

        public WO_Output(int int_wo_id, int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();
            int_Current_User_Id = int_C_User_id;
            int_Work_Order_Id = int_wo_id;
            //check if testing or not

            //if (FruPak.PF.Global.Global.bol_Testing == true)
            //{
            //    this.Text = "FruPak Process Factory - " + this.Text + " - Test Environment";
            //}
            //else
            //{
            //    this.Text = "FruPak Process Factory";
            //}
            // setup display of current work order
            woDisplay1.Work_Order_Id = int_Work_Order_Id;
            woDisplay1.populate();

            //restrict access
            bol_write_access = bol_w_a;
            btn_Add.Enabled = bol_w_a;

            AddColumnsProgrammatically();
            get_existing_Work_Order();
            populate_comboboxs();

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
            int int_Product_Id = 0;

            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();

            int_Product_Id = woDisplay1.Product_Id;
            selectedVariety = woDisplay1.Variety_Id;
            DataSet ds_Get_Info;
            DataRow dr_Get_Info;
            ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Work_Order.Get_Info(int_Work_Order_Id);

            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                selectedGrowingMethod = Convert.ToInt32(dr_Get_Info["Growing_Method_Id"].ToString());
            }
            ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Work_Order_Material_Relationship.Get_Info_By_Work_Order(int_Work_Order_Id);

            if (Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()) > 0)
            {
                for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
                {
                    dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                    DataSet ds_Get_Info2 = FruPak.PF.Data.AccessLayer.CM_Material.Get_Info_By_Translated(Convert.ToInt32(dr_Get_Info["Material_Id"].ToString()));
                    DataRow dr_Get_Info2;
                    for (int j = 0; j < Convert.ToInt32(ds_Get_Info2.Tables[0].Rows.Count.ToString()); j++)
                    {
                        int i_rows = Convert.ToInt32(dataGridView1.Rows.Count.ToString());
                        dr_Get_Info2 = ds_Get_Info2.Tables[0].Rows[j];
                        dataGridView1.Rows.Add(new DataGridViewRow());

                        DataGridViewCell DGVC_Id = new DataGridViewTextBoxCell();
                        DGVC_Id.Value = dr_Get_Info2["Material_Id"].ToString();
                        dataGridView1.Rows[i_rows].Cells[0] = DGVC_Id;

                        DataGridViewCell DGVC_Trader = new DataGridViewTextBoxCell();
                        DGVC_Trader.Value = dr_Get_Info2["Trader"].ToString();
                        dataGridView1.Rows[i_rows].Cells[1] = DGVC_Trader;

                        DataGridViewCell DGVC_ProductGroup = new DataGridViewTextBoxCell();
                        DGVC_ProductGroup.Value = dr_Get_Info2["PG_Combined"].ToString();
                        dataGridView1.Rows[i_rows].Cells[2] = DGVC_ProductGroup;

                        DataGridViewCell DGVC_PackType = new DataGridViewTextBoxCell();
                        DGVC_PackType.Value = dr_Get_Info2["PT_Combined"].ToString();
                        dataGridView1.Rows[i_rows].Cells[3] = DGVC_PackType;

                        DataGridViewCell DGVC_Weight = new DataGridViewTextBoxCell();
                        DGVC_Weight.Value = dr_Get_Info2["Weight"].ToString();
                        dataGridView1.Rows[i_rows].Cells[4] = DGVC_Weight;

                        DataGridViewCell DGVC_Brand = new DataGridViewTextBoxCell();
                        DGVC_Brand.Value = dr_Get_Info2["Brand"].ToString();
                        dataGridView1.Rows[i_rows].Cells[5] = DGVC_Brand;

                        DataGridViewCell DGVC_Material = new DataGridViewTextBoxCell();
                        DGVC_Material.Value = dr_Get_Info2["Material_Num"].ToString();
                        dataGridView1.Rows[i_rows].Cells[6] = DGVC_Material;
                    }
                    ds_Get_Info2.Dispose();
                }
            }
            else
            {
                DataSet ds_Get_prod_reltn = FruPak.PF.Data.AccessLayer.CM_Product_Product_Group_Relationship.Get_Info_By_Product(int_Product_Id);
                DataRow dr_Get_prod_reltn;
                for (int i = 0; i < Convert.ToInt32(ds_Get_prod_reltn.Tables[0].Rows.Count.ToString()); i++)
                {
                    dr_Get_prod_reltn = ds_Get_prod_reltn.Tables[0].Rows[i];
                    populate_datagrid(int_Product_Id, Convert.ToInt32(dr_Get_prod_reltn["ProductGroup_Id"].ToString()));
                }
                ds_Get_prod_reltn.Dispose();
            }
            ds_Get_Info.Dispose();
        }

        private DataView dv;

        private void populate_comboboxs()
        {
            DataSet ds_Get_Info;
            ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Material.Get_Info_By_Translated();
            dv = new DataView(ds_Get_Info.Tables[0]);
            string filterString = "";
            if (selectedVariety != 0)
                filterString = string.Format("Variety_Id = {0}", selectedVariety);
            if (selectedGrowingMethod != 0)
            {
                if (filterString != "")
                    filterString = filterString + " AND ";
                filterString = filterString + string.Format("Growing_Method_Id = {0}", selectedGrowingMethod);
            }
            if (selectedProductGroup != 0)
            {
                if (filterString != "")
                    filterString = filterString + " AND ";
                filterString = filterString + string.Format("ProductGroup_Id = {0}", selectedProductGroup);
            }
            if (selectedTrader != 0)
            {
                if (filterString != "")
                    filterString = filterString + " AND ";
                filterString = filterString + string.Format("Trader_Id = {0}", selectedTrader);
            }
            if (selectedBrand != 0)
            {
                if (filterString != "")
                    filterString = filterString + " AND ";
                filterString = filterString + string.Format("Brand_Id = {0}", selectedBrand);
            }
            if (selectedMaterial != 0)
            {
                if (filterString != "")
                    filterString = filterString + " AND ";
                filterString = filterString + string.Format("Material_Id = {0}", selectedMaterial);
            }
            if (selectedPackType != 0)
            {
                if (filterString != "")
                    filterString = filterString + " AND ";
                filterString = filterString + string.Format("PackType_Id = {0}", selectedPackType);
            }
            if (selectedWeight != 0)
            {
                if (filterString != "")
                    filterString = filterString + " AND ";
                filterString = filterString + string.Format("Weight = {0}", selectedWeight);
            }
            if (selectedGrade != 0)
            {
                if (filterString != "")
                    filterString = filterString + " AND ";
                filterString = filterString + string.Format("Grade_Id = {0}", selectedGrade);
            }
            if (selectedTreatment != 0)
            {
                if (filterString != "")
                    filterString = filterString + " AND ";
                filterString = filterString + string.Format("Treatment_Id = {0}", selectedTreatment);
            }
            if (selectedSize != 0)
            {
                if (filterString != "")
                    filterString = filterString + " AND ";
                filterString = filterString + string.Format("Size_Id = {0}", selectedSize);
            }
            dv.RowFilter = filterString;

            //Trader
            cmb_Trader.DataSource = dv.ToTable(true, "T_Combined", "Trader_Id");
            cmb_Trader.DisplayMember = "T_Combined";
            cmb_Trader.ValueMember = "Trader_Id";
            cmb_Trader.SelectedValue = selectedTrader;
            if (cmb_Trader.Items.Count == 1)
                cmb_Trader.SelectedIndex = 0;

            //Product Group

            cmb_Product_Group.DataSource = dv.ToTable(true, "PG_Combined", "ProductGroup_Id");
            cmb_Product_Group.DisplayMember = "PG_Combined";
            cmb_Product_Group.ValueMember = "ProductGroup_Id";
            cmb_Product_Group.SelectedValue = selectedProductGroup;
            if (cmb_Product_Group.Items.Count == 1)
                cmb_Product_Group.SelectedIndex = 0;

            //pack Type
            cmb_PackType.DataSource = dv.ToTable(true, "PT_Combined", "PackType_Id");
            cmb_PackType.DisplayMember = "PT_Combined";
            cmb_PackType.ValueMember = "PackType_Id";
            cmb_PackType.SelectedValue = selectedPackType;
            if (cmb_PackType.Items.Count == 1)
                cmb_PackType.SelectedIndex = 0;

            //Brand
            cmb_Brand.DataSource = dv.ToTable(true, "B_Combined", "Brand_Id");
            cmb_Brand.DisplayMember = "B_Combined";
            cmb_Brand.ValueMember = "Brand_Id";
            cmb_Brand.SelectedValue = selectedBrand;
            if (cmb_Brand.Items.Count == 1)
                cmb_Brand.SelectedIndex = 0;

            //Weight
            cmb_Weight.DataSource = dv.ToTable(true, "Weight");
            cmb_Weight.DisplayMember = "Weight";
            cmb_Weight.ValueMember = "Weight";
            cmb_Weight.SelectedValue = selectedWeight;
            if (cmb_Weight.Items.Count == 1)
                cmb_Weight.SelectedIndex = 0;

            //Grade
            cmb_Grade.DataSource = dv.ToTable(true, "G_Combined", "Grade_Id");
            cmb_Grade.DisplayMember = "G_Combined";
            cmb_Grade.ValueMember = "Grade_Id";
            cmb_Grade.SelectedValue = selectedGrade;
            if (cmb_Grade.Items.Count == 1)
                cmb_Grade.SelectedIndex = 0;

            //Treatment
            cmb_Treatment.DataSource = dv.ToTable(true, "TM_Combined", "Treatment_Id");
            cmb_Treatment.DisplayMember = "TM_Combined";
            cmb_Treatment.ValueMember = "Treatment_Id";
            cmb_Treatment.SelectedValue = selectedTreatment;
            if (cmb_Treatment.Items.Count == 1)
                cmb_Treatment.SelectedIndex = 0;

            //Size
            cmb_Size.DataSource = dv.ToTable(true, "S_Combined", "Size_Id");
            cmb_Size.DisplayMember = "S_Combined";
            cmb_Size.ValueMember = "Size_Id";
            cmb_Size.SelectedValue = selectedSize;
            if (cmb_Size.Items.Count == 1)
                cmb_Size.SelectedIndex = 0;

            //Material
            cmb_Material.DataSource = dv;
            cmb_Material.DisplayMember = "Material_Num";
            cmb_Material.ValueMember = "Material_Id";
            cmb_Material.SelectedValue = selectedMaterial;
            if (cmb_Material.Items.Count == 1)
                cmb_Material.SelectedIndex = 0;
            ds_Get_Info.Dispose();
        }

        private void AddColumnsProgrammatically()
        {
            var col0 = new DataGridViewTextBoxColumn();
            var col1 = new DataGridViewTextBoxColumn();
            var col2 = new DataGridViewTextBoxColumn();
            var col3 = new DataGridViewTextBoxColumn();
            var col4 = new DataGridViewTextBoxColumn();
            var col5 = new DataGridViewTextBoxColumn();
            var col6 = new DataGridViewTextBoxColumn();

            col0.HeaderText = "Material_Id";
            col0.Name = "Material_Id";
            col0.ReadOnly = false;
            col0.Visible = false;

            col1.HeaderText = "Trader";
            col1.Name = "Trader";
            col1.ReadOnly = false;
            col1.Visible = true;

            col2.HeaderText = "Product Group";
            col2.Name = "Product";
            col2.ReadOnly = true;

            col3.HeaderText = "Pack Type";
            col3.Name = "Packtype";
            col3.ReadOnly = true;

            col4.HeaderText = "Weight";
            col4.Name = "Weight";
            col4.ReadOnly = true;

            col5.HeaderText = "Brand";
            col5.Name = "Brand";
            col5.ReadOnly = false;
            col5.Visible = true;

            col6.HeaderText = "Material";
            col6.Name = "Material";
            col6.ReadOnly = false;
            col6.Visible = true;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2, col3, col4, col5, col6 });

            DataGridViewImageColumn img_delete = new DataGridViewImageColumn();
            dataGridView1.Columns.Add(img_delete);
            img_delete.HeaderText = "Delete";
            img_delete.Name = "Delete";
            img_delete.Image = FruPak.PF.Global.Properties.Resources.delete;
            img_delete.ReadOnly = true;
        }

        private void populate_datagrid(int int_Product_Id, int int_ProductGroup_Id)
        {
            //dataGridView1.Refresh();
            //dataGridView1.Rows.Clear();

            DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Product_Product_Group_Relationship.Get_Info_By_Product_Material(int_Product_Id, int_ProductGroup_Id);
            DataRow dr_Get_Info;
            if (Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()) == 1)
            {
                for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
                {
                    int i_rows = Convert.ToInt32(dataGridView1.Rows.Count.ToString());
                    dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                    dataGridView1.Rows.Add(new DataGridViewRow());

                    DataGridViewCell DGVC_Id = new DataGridViewTextBoxCell();
                    DGVC_Id.Value = dr_Get_Info["Material_Id"].ToString();
                    dataGridView1.Rows[i_rows].Cells[0] = DGVC_Id;

                    int_result = FruPak.PF.Data.AccessLayer.PF_Work_Order_Material_Relationship.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Work_Order_Material_Relationship"), int_Work_Order_Id, Convert.ToInt32(dr_Get_Info["Material_Id"].ToString()), int_Current_User_Id);

                    DataGridViewCell DGVC_Trader = new DataGridViewTextBoxCell();
                    DGVC_Trader.Value = dr_Get_Info["Trader"].ToString();
                    dataGridView1.Rows[i_rows].Cells[1] = DGVC_Trader;

                    DataGridViewCell DGVC_ProductGroup = new DataGridViewTextBoxCell();
                    DGVC_ProductGroup.Value = dr_Get_Info["ProductGroup"].ToString() + " - " + dr_Get_Info["PG_Description"].ToString();
                    dataGridView1.Rows[i_rows].Cells[2] = DGVC_ProductGroup;

                    DataGridViewCell DGVC_PackType = new DataGridViewTextBoxCell();
                    DGVC_PackType.Value = dr_Get_Info["PackType"].ToString() + " - " + dr_Get_Info["PT_Description"].ToString();
                    dataGridView1.Rows[i_rows].Cells["Packtype"] = DGVC_PackType;

                    DataGridViewCell DGVC_Weight = new DataGridViewTextBoxCell();
                    DGVC_Weight.Value = dr_Get_Info["Weight"].ToString();
                    dataGridView1.Rows[i_rows].Cells[4] = DGVC_Weight;

                    DataGridViewCell DGVC_Brand = new DataGridViewTextBoxCell();
                    DGVC_Brand.Value = dr_Get_Info["Brand"].ToString();
                    dataGridView1.Rows[i_rows].Cells[5] = DGVC_Brand;

                    DataGridViewCell DGVC_Material = new DataGridViewTextBoxCell();
                    DGVC_Material.Value = dr_Get_Info["Material_Num"].ToString();
                    dataGridView1.Rows[i_rows].Cells[6] = DGVC_Material;
                }
            }
            ds_Get_Info.Dispose();
        }

        private void SizeAllColumns(Object sender, EventArgs e)
        {
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void cmb_Trader_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedValue != null)
            {
                selectedTrader = Convert.ToInt32((sender as ComboBox).SelectedValue.ToString());
                dv.RowFilter = string.Format("Trader_Id = {0}", (sender as ComboBox).SelectedValue);
                populate_comboboxs();
            }
        }

        private void cmb_Product_Group_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedValue != null)
            {
                selectedProductGroup = Convert.ToInt32((sender as ComboBox).SelectedValue.ToString());
                dv.RowFilter = string.Format("ProductGroup_Id = {0}", (sender as ComboBox).SelectedValue);
                populate_comboboxs();
            }
        }

        private void cmb_PackType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedValue != null)
            {
                selectedPackType = Convert.ToInt32((sender as ComboBox).SelectedValue.ToString());
                dv.RowFilter = string.Format("PackType_Id = {0}", (sender as ComboBox).SelectedValue);
                populate_comboboxs();
            }
        }

        private void cmb_Brand_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedValue != null)
            {
                selectedBrand = Convert.ToInt32((sender as ComboBox).SelectedValue.ToString());
                dv.RowFilter = string.Format("Brand_Id = {0}", (sender as ComboBox).SelectedValue);
                populate_comboboxs();
            }
        }

        private void cmb_Weight_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedValue != null)
            {
                selectedWeight = Convert.ToDecimal((sender as ComboBox).SelectedValue.ToString());
                dv.RowFilter = string.Format("Weight = {0}", (sender as ComboBox).SelectedValue);
                populate_comboboxs();
            }
        }

        private void cmb_Grade_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedValue != null)
            {
                selectedGrade = Convert.ToInt32((sender as ComboBox).SelectedValue.ToString());
                dv.RowFilter = string.Format("Grade_Id = {0}", (sender as ComboBox).SelectedValue);
                populate_comboboxs();
            }
        }

        private void cmb_Treatment_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedValue != null)
            {
                selectedTreatment = Convert.ToInt32((sender as ComboBox).SelectedValue.ToString());
                dv.RowFilter = string.Format("Treatment_Id = {0}", (sender as ComboBox).SelectedValue);
                populate_comboboxs();
            }
        }

        private void cmb_Size_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedValue != null)
            {
                selectedSize = Convert.ToInt32((sender as ComboBox).SelectedValue.ToString());
                dv.RowFilter = string.Format("Size_Id = {0}", (sender as ComboBox).SelectedValue);
                populate_comboboxs();
            }
        }

        private void cmb_Material_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedValue != null)
            {
                selectedMaterial = Convert.ToInt32((sender as ComboBox).SelectedValue.ToString());
                dv.RowFilter = string.Format("Material_Id = {0}", (sender as ComboBox).SelectedValue);
                populate_comboboxs();
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Add_btn();
        }

        private void Add_btn()
        {
            DialogResult DLR_MessageBox = new DialogResult();
            string str_msg = "";
            int int_result = 0;

            if (cmb_Material.SelectedValue == null)
            {
                str_msg = str_msg + "Invalid Material Number, Please select a valid Material Number from the DropDown list." + Environment.NewLine;
            }
            if (str_msg.Length != 0)
            {
                DLR_MessageBox = MessageBox.Show(str_msg, "Process Factory - Work Order", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (DLR_MessageBox != DialogResult.OK)
            {
                int_result = FruPak.PF.Data.AccessLayer.PF_Work_Order_Material_Relationship.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Work_Order_Material_Relationship"), int_Work_Order_Id, Convert.ToInt32(cmb_Material.SelectedValue.ToString()), int_Current_User_Id);
            }
            if (int_result > 0)
            {
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                lbl_message.Text = cmb_Material.Text + " has been Added to the Work Order";
                Reset();
            }
            else
            {
                lbl_message.ForeColor = System.Drawing.Color.Red;
                lbl_message.Text = cmb_Material.Text + " has NOT been saved";
            }
            get_existing_Work_Order();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult DLR_Message = new DialogResult();
            int int_result = 0;

            //Delete
            if (e.ColumnIndex == 7)
            {
                string str_msg;

                str_msg = "You are about to remove Material Number  ";
                str_msg = str_msg + dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                str_msg = str_msg + " from this Work Order." + Environment.NewLine;
                str_msg = str_msg + "Do you want to continue?";
                DLR_Message = MessageBox.Show(str_msg, "Process Factory - Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (DLR_Message == DialogResult.Yes)
                {
                    int_result = FruPak.PF.Data.AccessLayer.PF_Work_Order_Material_Relationship.Delete(int_Work_Order_Id, Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                }

                if (int_result > 0)
                {
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    lbl_message.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString() + " has been Removed from the Work Order";
                }
                else
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + " failed to be removed";
                }
                get_existing_Work_Order();
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            selectedProductGroup = 0;
            selectedMaterial = 0;
            selectedPackType = 0;
            selectedTrader = 0;
            selectedWeight = 0;
            selectedBrand = 0;
            selectedGrade = 0;
            selectedTreatment = 0;
            selectedSize = 0;
            populate_comboboxs();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
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
        private void WO_Output_KeyDown(object sender, KeyEventArgs e)
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