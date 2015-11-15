using NLog;
using System;
using System.Data;
using System.Windows.Forms;

namespace FruPak.PF.WorkOrder
{
    /*Description
    -----------------
    Maintenance Class.
     * In order to keep things simple and as standard as possible. It was decided where possible to a standard table structure would be used.
     * This structure is: _id, Code, Description, Mod_date, Mod_User_Id
     *
     * This class is a form used to Add, Update, and Delete general Common items, where the tables have followed the standard structure.

    Date        Author     Desc
    -------------------------------------------------------------------------------------------------------------------------------------------------
    01/09/2013  Dave       Creation
    */

    public partial class Work_Order_Maintenance : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static string str_table = "";
        private static int int_Current_User_Id = 0;
        private static int int_DVG_Row_id = 0;
        private static bool bol_write_access;

        public Work_Order_Maintenance(string str_type, int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();
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
            str_table = str_type;
            int_Current_User_Id = int_C_User_id;
            this.Text = str_type + " Maintenance";

            switch (str_table)
            {
                case "PF_Tests":
                    rb_batch.Visible = true;
                    rb_Other.Visible = true;
                    rb_PreOpCheck.Visible = true;
                    lbl_Product.Visible = false;
                    cmb_Product.Visible = false;
                    cmb_Code.Visible = false;
                    txt_code.Visible = true;
                    lbl_Value.Visible = false;
                    txt_Value.Visible = false;
                    fruit1.Visible = false;
                    break;

                case "PF_Product_LabResults":
                    rb_batch.Visible = false;
                    rb_Other.Visible = false;
                    rb_PreOpCheck.Visible = false;
                    populate_comboBox();
                    lbl_Product.Visible = true;
                    cmb_Product.Visible = true;
                    cmb_Code.Visible = true;
                    txt_code.Visible = false;
                    lbl_Value.Visible = true;
                    txt_Value.Visible = true;
                    fruit1.Visible = true;
                    break;

                case "PF_Product_Other":
                    rb_batch.Visible = false;
                    rb_Other.Visible = false;
                    rb_PreOpCheck.Visible = false;
                    populate_comboBox();
                    lbl_Product.Visible = true;
                    cmb_Product.Visible = true;
                    cmb_Code.Visible = true;
                    txt_code.Visible = false;
                    lbl_Value.Visible = true;
                    txt_Value.Visible = true;
                    fruit1.Visible = false;
                    break;

                case "PF_Reports":
                    rb_batch.Visible = false;
                    rb_Other.Visible = false;
                    rb_PreOpCheck.Visible = false;
                    lbl_Product.Visible = false;
                    cmb_Product.Visible = false;
                    cmb_Code.Visible = false;
                    txt_code.Visible = true;
                    lbl_Value.Visible = true;
                    txt_Value.Visible = true;
                    txt_Value.MaxLength = 3000;
                    fruit1.Visible = false;
                    break;

                default:
                    rb_batch.Visible = false;
                    rb_Other.Visible = false;
                    rb_PreOpCheck.Visible = false;
                    lbl_Product.Visible = false;
                    cmb_Product.Visible = false;
                    cmb_Code.Visible = false;
                    txt_code.Visible = true;
                    lbl_Value.Visible = false;
                    txt_Value.Visible = false;
                    fruit1.Visible = false;
                    break;
            }
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

            #endregion Log any interesting events from the UI to the CSV log file
        }

        private void populate_comboBox()
        {
            DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Product.Get_Info();
            cmb_Product.DataSource = ds_Get_Info.Tables[0];
            cmb_Product.DisplayMember = "Code";
            cmb_Product.ValueMember = "Product_Id";
            cmb_Product.Text = null;

            switch (str_table)
            {
                case "PF_Product_LabResults":
                    cmb_Code.Items.AddRange(new object[] { "pH", "Moist", "Brix" });
                    break;

                case "PF_Product_Other":
                    cmb_Code.Items.AddRange(new object[] { "BBD" });
                    cmb_Code.SelectedItem = "BBD";
                    lbl_Value.Text = "# of Months";
                    break;
            }
        }

        private void AddColumnsProgrammatically()
        {
            var col1 = new DataGridViewTextBoxColumn();
            var col2 = new DataGridViewTextBoxColumn();
            var col3 = new DataGridViewTextBoxColumn();
            var col4 = new DataGridViewTextBoxColumn();
            var col5 = new DataGridViewTextBoxColumn();
            var col6 = new DataGridViewTextBoxColumn();
            var col7 = new DataGridViewTextBoxColumn();
            var col8 = new DataGridViewTextBoxColumn();
            var col9 = new DataGridViewTextBoxColumn();
            var col10 = new DataGridViewTextBoxColumn();
            var col11 = new DataGridViewTextBoxColumn();

            col1.HeaderText = "Id";
            col1.Name = "Id";
            col1.ReadOnly = true;
            col1.Visible = false;

            col2.HeaderText = "Code";
            col2.Name = "Code";
            col2.ReadOnly = true;

            col3.HeaderText = "Description";
            col3.Name = "Description";
            col3.Width = 200;
            col3.ReadOnly = true;

            col4.HeaderText = "Active";
            col4.Name = "Active_Ind";
            col4.ReadOnly = true;

            col5.HeaderText = "Mod_Date";
            col5.Name = "Mod_Date";
            col5.ReadOnly = true;
            col5.Visible = false;

            col6.HeaderText = "Mod_User_id";
            col6.Name = "Mod_User_id";
            col6.ReadOnly = true;
            col6.Visible = false;

            switch (str_table)
            {
                case "PF_Tests":
                    col7.HeaderText = "Type";
                    col7.Name = "Type";
                    col7.ReadOnly = true;
                    col7.Visible = true;
                    break;

                case "PF_Product_LabResults":
                    col7.HeaderText = "Value";
                    col7.Name = "Value";
                    col7.ReadOnly = true;
                    col7.Visible = true;

                    col8.HeaderText = "Product_Id";
                    col8.Name = "Product_Id";
                    col8.ReadOnly = true;
                    col8.Visible = false;

                    col9.HeaderText = "Product";
                    col9.Name = "Product";
                    col9.ReadOnly = true;
                    col9.Visible = true;

                    col10.HeaderText = "FruitType_Id";
                    col10.Name = "FruitType_Id";
                    col10.ReadOnly = true;
                    col10.Visible = false;

                    col11.HeaderText = "Variety_Id";
                    col11.Name = "Variety_Id";
                    col11.ReadOnly = true;
                    col11.Visible = false;
                    break;

                case "PF_Product_Other":
                    col7.HeaderText = "Value";
                    col7.Name = "Value";
                    col7.ReadOnly = true;
                    col7.Visible = true;

                    col8.HeaderText = "Product_Id";
                    col8.Name = "Product_Id";
                    col8.ReadOnly = true;
                    col8.Visible = false;

                    col9.HeaderText = "Product";
                    col9.Name = "Product";
                    col9.ReadOnly = true;
                    col9.Visible = true;
                    break;

                case "PF_Reports":
                    col7.HeaderText = "Value";
                    col7.Name = "Value";
                    col7.ReadOnly = true;
                    col7.Visible = true;
                    col8.Visible = false;
                    col9.Visible = false;
                    col10.Visible = false;
                    col11.Visible = false;
                    break;

                default:
                    col7.Visible = false;
                    col8.Visible = false;
                    col9.Visible = false;
                    col10.Visible = false;
                    col11.Visible = false;
                    break;
            }

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col1, col2, col3, col4, col5, col6, col7, col8, col9, col10, col11 });

            DataGridViewImageColumn img_delete = new DataGridViewImageColumn();
            dataGridView1.Columns.Add(img_delete);
            img_delete.HeaderText = "Delete";
            img_delete.Name = "Delete";
            img_delete.Image = FruPak.PF.Global.Properties.Resources.delete;
            img_delete.ReadOnly = true;
            img_delete.Visible = bol_write_access;

            DataGridViewImageColumn img_edit = new DataGridViewImageColumn();
            dataGridView1.Columns.Add(img_edit);
            img_edit.HeaderText = "Edit";
            img_edit.Name = "Edit";
            img_edit.Image = FruPak.PF.Global.Properties.Resources.edit;
            img_edit.ReadOnly = true;
        }

        private void populate_datagridview()
        {
            DataSet ds_Get_Info = null;
            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();

            switch (str_table)
            {
                case "PF_Chemicals":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Chemicals.Get_Info();
                    break;

                case "PF_Cleaning_Area":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Cleaning_Area.Get_Info();
                    break;

                case "PF_Cleaning_Area_Parts":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Cleaning_Area_Parts.Get_Info();
                    break;

                case "PF_Product":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Product.Get_Info();
                    break;

                case "PF_Product_LabResults":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Product_LabResults.Get_Info();
                    break;

                case "PF_Product_Other":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Product_Other.Get_Info();
                    break;

                case "PF_Tests":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Tests.Get_Info();
                    break;

                case "PF_Reports":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Reports.Get_Info();
                    break;
            }

            DataRow dr_Get_Info;
            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                dataGridView1.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_User_Id = new DataGridViewTextBoxCell();
                switch (str_table)
                {
                    case "PF_Chemicals":
                        DGVC_User_Id.Value = dr_Get_Info["Chemical_Id"].ToString();
                        break;

                    case "PF_Cleaning_Area":
                        DGVC_User_Id.Value = dr_Get_Info["CleanArea_Id"].ToString();
                        break;

                    case "PF_Cleaning_Area_Parts":
                        DGVC_User_Id.Value = dr_Get_Info["CleanAreaParts_Id"].ToString();
                        break;

                    case "PF_Product":
                        DGVC_User_Id.Value = dr_Get_Info["Product_Id"].ToString();
                        break;

                    case "PF_Product_LabResults":
                        DGVC_User_Id.Value = dr_Get_Info["ProdLab_Id"].ToString();
                        break;

                    case "PF_Product_Other":
                        DGVC_User_Id.Value = dr_Get_Info["Prod_Other_Id"].ToString();
                        break;

                    case "PF_Tests":
                        DGVC_User_Id.Value = dr_Get_Info["Test_Id"].ToString();
                        break;

                    case "PF_Reports":
                        DGVC_User_Id.Value = dr_Get_Info["Report_Id"].ToString();
                        break;
                }

                dataGridView1.Rows[i].Cells["Id"] = DGVC_User_Id;

                DataGridViewCell DGVC_Name = new DataGridViewTextBoxCell();
                DGVC_Name.Value = dr_Get_Info["Code"].ToString();
                dataGridView1.Rows[i].Cells["Code"] = DGVC_Name;

                DataGridViewCell DGVC_Description = new DataGridViewTextBoxCell();
                DGVC_Description.Value = dr_Get_Info["Description"].ToString();
                dataGridView1.Rows[i].Cells["Description"] = DGVC_Description;

                DataGridViewCell DGVC_Active = new DataGridViewTextBoxCell();
                DGVC_Active.Value = dr_Get_Info["PF_Active_Ind"].ToString();
                dataGridView1.Rows[i].Cells["Active_Ind"] = DGVC_Active;

                DataGridViewCell DGVC_Mod_date = new DataGridViewTextBoxCell();
                DGVC_Mod_date.Value = dr_Get_Info["Mod_date"].ToString();
                dataGridView1.Rows[i].Cells["Mod_Date"] = DGVC_Mod_date;

                DataGridViewCell DGVC_Mod_User_Id = new DataGridViewTextBoxCell();
                DGVC_Mod_User_Id.Value = dr_Get_Info["Mod_User_Id"].ToString();
                dataGridView1.Rows[i].Cells["Mod_User_id"] = DGVC_Mod_User_Id;

                DataGridViewCell DGVC_Type = new DataGridViewTextBoxCell();
                DataGridViewCell DGVC_prod_id = new DataGridViewTextBoxCell();
                DataSet ds_Get_Info2 = null;
                DataRow dr_Get_Info2;
                switch (str_table)
                {
                    case "PF_Tests":
                        DGVC_Type.Value = dr_Get_Info["Type"].ToString();
                        dataGridView1.Rows[i].Cells[6] = DGVC_Type;
                        break;

                    case "PF_Product_LabResults":
                        DGVC_Type.Value = dr_Get_Info["Value"].ToString();
                        dataGridView1.Rows[i].Cells[6] = DGVC_Type;

                        DGVC_prod_id.Value = dr_Get_Info["Product_Id"].ToString();
                        dataGridView1.Rows[i].Cells[7] = DGVC_prod_id;

                        ds_Get_Info2 = FruPak.PF.Data.AccessLayer.PF_Product.Get_Info(Convert.ToInt32(dr_Get_Info["Product_Id"].ToString()));
                        for (int j = 0; j < Convert.ToInt32(ds_Get_Info2.Tables[0].Rows.Count.ToString()); j++)
                        {
                            dr_Get_Info2 = ds_Get_Info2.Tables[0].Rows[j];
                            DataGridViewCell DGVC_prod = new DataGridViewTextBoxCell();
                            DGVC_prod.Value = dr_Get_Info2["Code"].ToString();
                            dataGridView1.Rows[i].Cells[8] = DGVC_prod;
                        }
                        ds_Get_Info2.Dispose();

                        DataGridViewCell DGVC_Cell10 = new DataGridViewTextBoxCell();
                        DGVC_Cell10.Value = dr_Get_Info["FruitType_Id"].ToString();
                        dataGridView1.Rows[i].Cells["FruitType_Id"] = DGVC_Cell10;

                        DataGridViewCell DGVC_Cell11 = new DataGridViewTextBoxCell();
                        DGVC_Cell11.Value = dr_Get_Info["Variety_Id"].ToString();
                        dataGridView1.Rows[i].Cells["Variety_Id"] = DGVC_Cell11;

                        break;

                    case "PF_Product_Other":
                        DGVC_Type.Value = dr_Get_Info["Value"].ToString();
                        dataGridView1.Rows[i].Cells[6] = DGVC_Type;

                        DGVC_prod_id.Value = dr_Get_Info["Product_Id"].ToString();
                        dataGridView1.Rows[i].Cells[7] = DGVC_prod_id;

                        ds_Get_Info2 = FruPak.PF.Data.AccessLayer.PF_Product.Get_Info(Convert.ToInt32(dr_Get_Info["Product_Id"].ToString()));
                        for (int j = 0; j < Convert.ToInt32(ds_Get_Info2.Tables[0].Rows.Count.ToString()); j++)
                        {
                            dr_Get_Info2 = ds_Get_Info2.Tables[0].Rows[j];
                            DataGridViewCell DGVC_prod = new DataGridViewTextBoxCell();
                            DGVC_prod.Value = dr_Get_Info2["Code"].ToString();
                            dataGridView1.Rows[i].Cells[8] = DGVC_prod;
                        }
                        ds_Get_Info2.Dispose();
                        break;

                    case "PF_Reports":
                        DGVC_Type.Value = dr_Get_Info["Value"].ToString();
                        dataGridView1.Rows[i].Cells[6] = DGVC_Type;
                        break;
                }
                Column_Size();
                ds_Get_Info.Dispose();
            }
        }

        private void SizeAllColumns(Object sender, EventArgs e)
        {
            Column_Size();
        }

        private void Column_Size()
        {
            dataGridView1.AutoResizeColumn(0, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(1, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(3, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(4, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(5, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(6, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(7, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(8, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(9, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(10, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(11, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(12, DataGridViewAutoSizeColumnMode.AllCells);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult DLR_Message = new DialogResult();
            int int_result = 0;

            //Delete
            if (e.ColumnIndex == 11)
            {
                string str_msg;

                str_msg = "You are about to delete " + Environment.NewLine;
                str_msg = str_msg + dataGridView1.Rows[e.RowIndex].Cells["Code"].Value.ToString() + Environment.NewLine;
                str_msg = str_msg + "Do you want to continue?";
                DLR_Message = MessageBox.Show(str_msg, "Process Factory - Maintenance(Delete)", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (DLR_Message == DialogResult.Yes)
                {
                    switch (str_table)
                    {
                        case "PF_Chemicals":
                            int_result = FruPak.PF.Data.AccessLayer.PF_Product_Chemicals_Relationship.Delete_Chemical_From_Product(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString()));
                            int_result = FruPak.PF.Data.AccessLayer.PF_Chemicals.Delete(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString()));
                            break;

                        case "PF_Cleaning_Area":
                            int_result = FruPak.PF.Data.AccessLayer.PF_Clean_Area_Area_Parts_Relationship.Delete_Cleaning_Area(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString()));
                            int_result = FruPak.PF.Data.AccessLayer.PF_Cleaning_Area.Delete(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString()));
                            break;

                        case "PF_Cleaning_Area_Parts":
                            int_result = FruPak.PF.Data.AccessLayer.PF_Clean_Area_Area_Parts_Relationship.Delete_Cleaning_Part(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString()));
                            int_result = FruPak.PF.Data.AccessLayer.PF_Cleaning_Area_Parts.Delete(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString()));
                            break;

                        case "PF_Product":
                            int_result = FruPak.PF.Data.AccessLayer.PF_Product.Delete(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString()));
                            break;

                        case "PF_Product_LabResults":
                            int_result = FruPak.PF.Data.AccessLayer.PF_Product_LabResults.Delete(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString()));
                            break;

                        case "PF_Product_Other":
                            int_result = FruPak.PF.Data.AccessLayer.PF_Product_Other.Delete(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString()));
                            break;

                        case "PF_Tests":
                            int_result = FruPak.PF.Data.AccessLayer.PF_Product_Tests_Relationship.Delete_Test_From_Product(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString()));
                            int_result = FruPak.PF.Data.AccessLayer.PF_Tests.Delete(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString()));
                            break;

                        case "PF_Reports":
                            int_result = FruPak.PF.Data.AccessLayer.PF_Reports.Delete(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString()));
                            break;
                    }
                }

                if (int_result > 0)
                {
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    lbl_message.Text = dataGridView1.Rows[e.RowIndex].Cells["Code"].Value.ToString() + " has been deleted";
                }
                else
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = dataGridView1.Rows[e.RowIndex].Cells["Code"].Value.ToString() + " failed to delete";
                }
                populate_datagridview();
            }
            //Edit
            else if (e.ColumnIndex == 12)
            {
                int_DVG_Row_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString());
                txt_code.Text = dataGridView1.Rows[e.RowIndex].Cells["Code"].Value.ToString();
                ckb_Active.Checked = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                if (str_table == "PF_Tests")
                {
                    switch (dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString())
                    {
                        case "P":
                            rb_PreOpCheck.Checked = true;
                            rb_batch.Checked = false;
                            rb_Other.Checked = false;
                            break;

                        case "B":
                            rb_PreOpCheck.Checked = false;
                            rb_batch.Checked = true;
                            rb_Other.Checked = false;
                            break;

                        case "O":
                            rb_PreOpCheck.Checked = false;
                            rb_batch.Checked = false;
                            rb_Other.Checked = true;
                            break;

                        default:
                            rb_PreOpCheck.Checked = false;
                            rb_batch.Checked = false;
                            rb_Other.Checked = false;
                            break;
                    }
                }
                txt_Description.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                btn_Add.Text = "&Update";
                switch (str_table)
                {
                    case "PF_Chemicals":
                        populate_check_boxList();
                        tabControl1.Visible = true;
                        tabControl1.TabPages[0].Text = "Products";
                        tabControl1.TabPages.RemoveAt(1);
                        checkedListBox1.Visible = true;
                        btn_Update_Relationship.Visible = true;
                        btn_Update_Relationship.Enabled = bol_write_access;
                        break;

                    case "PF_Cleaning_Area":
                        populate_check_boxList();
                        populate_check_boxList2();
                        tabControl1.Visible = true;
                        tabControl1.TabPages[0].Text = "Parts";
                        tabControl1.TabPages[1].Text = "Products";
                        checkedListBox1.Visible = true;
                        checkedListBox2.Visible = true;
                        btn_Update_Relationship.Visible = true;
                        btn_Update_Relationship.Enabled = bol_write_access;
                        break;

                    case "PF_Cleaning_Area_Parts":
                        populate_check_boxList();
                        tabControl1.Visible = true;
                        tabControl1.TabPages[0].Text = "Area";
                        tabControl1.TabPages.RemoveAt(1);
                        checkedListBox1.Visible = true;
                        btn_Update_Relationship.Visible = true;
                        btn_Update_Relationship.Enabled = bol_write_access;
                        break;

                    case "PF_Product":
                        populate_check_boxList();
                        populate_check_boxList2();
                        tabControl1.Visible = true;
                        tabControl1.TabPages[0].Text = "Product Groups";
                        tabControl1.TabPages[1].Text = "Area";
                        checkedListBox1.Visible = true;
                        checkedListBox2.Visible = true;
                        btn_Update_Relationship.Visible = true;
                        btn_Update_Relationship.Enabled = bol_write_access;
                        break;

                    case "PF_Tests":
                        populate_check_boxList();
                        tabControl1.Visible = true;
                        tabControl1.TabPages[0].Text = "Products";
                        tabControl1.TabPages.RemoveAt(1);
                        checkedListBox1.Visible = true;
                        btn_Update_Relationship.Visible = true;
                        btn_Update_Relationship.Enabled = bol_write_access;
                        break;

                    case "PF_Product_LabResults":
                        txt_Value.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                        cmb_Product.SelectedValue = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString());
                        fruit1.FruitType_Id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["FruitType_Id"].Value.ToString());
                        fruit1.FruitVariety_Id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Variety_Id"].Value.ToString());
                        cmb_Code.SelectedItem = dataGridView1.Rows[e.RowIndex].Cells["Code"].Value.ToString();
                        break;

                    case "PF_Product_Other":
                        txt_Value.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                        cmb_Product.SelectedValue = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString());
                        cmb_Code.SelectedItem = dataGridView1.Rows[e.RowIndex].Cells["Code"].Value.ToString();
                        break;

                    case "PF_Reports":
                        txt_Value.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                        break;

                    default:
                        tabControl1.Visible = false;
                        checkedListBox1.Visible = false;
                        btn_Update_Relationship.Visible = false;
                        btn_Update_Relationship.Enabled = bol_write_access;
                        break;
                }
            }
        }

        private void populate_check_boxList()
        {
            DataSet ds_Get_Info = null;
            DataRow dr_Get_Info;
            checkedListBox1.Items.Clear();

            //get list of products
            switch (str_table)
            {
                case "PF_Chemicals":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Product.Get_Info();
                    break;

                case "PF_Cleaning_Area":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Cleaning_Area_Parts.Get_Info();
                    break;

                case "PF_Cleaning_Area_Parts":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Cleaning_Area.Get_Info();
                    break;

                case "PF_Product":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Product_Group.Get_Info();
                    break;

                case "PF_Tests":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Product.Get_Info();
                    break;
            }

            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                switch (str_table)
                {
                    case "PF_Chemicals":
                        checkedListBox1.Items.Add(dr_Get_Info["Product_Id"].ToString() + " " + dr_Get_Info["Code"].ToString());
                        break;

                    case "PF_Cleaning_Area":
                        checkedListBox1.Items.Add(dr_Get_Info["CleanAreaParts_Id"].ToString() + " " + dr_Get_Info["Combined"].ToString());
                        break;

                    case "PF_Cleaning_Area_Parts":
                        checkedListBox1.Items.Add(dr_Get_Info["CleanArea_Id"].ToString() + " " + dr_Get_Info["Combined"].ToString());
                        break;

                    case "PF_Product":
                        checkedListBox1.Items.Add(dr_Get_Info["ProductGroup_Id"].ToString() + " " + dr_Get_Info["Code"].ToString() + " " + dr_Get_Info["Description"].ToString());
                        break;

                    case "PF_Tests":
                        checkedListBox1.Items.Add(dr_Get_Info["Product_Id"].ToString() + " " + dr_Get_Info["Code"].ToString());
                        break;
                }
            }
            ds_Get_Info.Dispose();

            //turn checks on or off according to current relationship data
            switch (str_table)
            {
                case "PF_Chemicals":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Product_Chemicals_Relationship.Get_Info_By_Chemical(int_DVG_Row_id);
                    break;

                case "PF_Cleaning_Area":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Clean_Area_Area_Parts_Relationship.Get_Info_Area(int_DVG_Row_id);
                    break;

                case "PF_Cleaning_Area_Parts":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Clean_Area_Area_Parts_Relationship.Get_Info_Parts(int_DVG_Row_id);
                    break;

                case "PF_Product":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Product_Product_Group_Relationship.Get_Info_By_Product(int_DVG_Row_id);
                    break;

                case "PF_Tests":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Product_Tests_Relationship.Get_Info_By_Test(int_DVG_Row_id);
                    break;
            }

            for (int i = 0; i < Convert.ToInt32(checkedListBox1.Items.Count.ToString()); i++)
            {
                string item = checkedListBox1.Items[i].ToString();
                int int_User_Id = Convert.ToInt32(item.Substring(0, item.IndexOf(' ')));
                checkedListBox1.SetItemCheckState(i, CheckState.Unchecked);
                for (int j = 0; j < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); j++)
                {
                    dr_Get_Info = ds_Get_Info.Tables[0].Rows[j];

                    int int_returned_Id = 0;
                    switch (str_table)
                    {
                        case "PF_Chemicals":
                            int_returned_Id = Convert.ToInt32(dr_Get_Info["Product_Id"].ToString());
                            break;

                        case "PF_Cleaning_Area":
                            int_returned_Id = Convert.ToInt32(dr_Get_Info["CleanAreaParts_Id"].ToString());
                            break;

                        case "PF_Cleaning_Area_Parts":
                            int_returned_Id = Convert.ToInt32(dr_Get_Info["CleanArea_Id"].ToString());
                            break;

                        case "PF_Product":
                            int_returned_Id = Convert.ToInt32(dr_Get_Info["ProductGroup_Id"].ToString());
                            break;

                        case "PF_Tests":
                            int_returned_Id = Convert.ToInt32(dr_Get_Info["Product_Id"].ToString());
                            break;
                    }

                    if (Convert.ToInt32(item.Substring(0, item.IndexOf(' '))) == int_returned_Id)
                    {
                        checkedListBox1.SetItemCheckState(i, CheckState.Checked);
                    }
                }
            }
            ds_Get_Info.Dispose();
        }

        private void populate_check_boxList2()
        {
            DataSet ds_Get_Info = null;
            DataRow dr_Get_Info;
            checkedListBox2.Items.Clear();

            //get list of products
            switch (str_table)
            {
                case "PF_Cleaning_Area":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Product.Get_Info();
                    break;

                case "PF_Product":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Cleaning_Area.Get_Info();
                    break;
            }

            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                switch (str_table)
                {
                    case "PF_Cleaning_Area":
                        checkedListBox2.Items.Add(dr_Get_Info["Product_Id"].ToString() + " " + dr_Get_Info["Code"].ToString());
                        break;

                    case "PF_Product":
                        checkedListBox2.Items.Add(dr_Get_Info["CleanArea_Id"].ToString() + " " + dr_Get_Info["Combined"].ToString());
                        break;
                }
            }
            ds_Get_Info.Dispose();

            //turn checks on or off according to current relationship data
            switch (str_table)
            {
                case "PF_Cleaning_Area":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Product_Cleaning_Area_Relationship.Get_Info_By_Area(int_DVG_Row_id);
                    break;

                case "PF_Product":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Product_Cleaning_Area_Relationship.Get_Info_By_Product(int_DVG_Row_id);
                    break;
            }

            for (int i = 0; i < Convert.ToInt32(checkedListBox2.Items.Count.ToString()); i++)
            {
                string item = checkedListBox2.Items[i].ToString();
                int int_User_Id = Convert.ToInt32(item.Substring(0, item.IndexOf(' ')));
                checkedListBox2.SetItemCheckState(i, CheckState.Unchecked);
                for (int j = 0; j < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); j++)
                {
                    dr_Get_Info = ds_Get_Info.Tables[0].Rows[j];

                    int int_returned_Id = 0;
                    switch (str_table)
                    {
                        case "PF_Cleaning_Area":
                            int_returned_Id = Convert.ToInt32(dr_Get_Info["Product_Id"].ToString());
                            break;

                        case "PF_Product":
                            int_returned_Id = Convert.ToInt32(dr_Get_Info["CleanArea_Id"].ToString());
                            break;
                    }

                    if (Convert.ToInt32(item.Substring(0, item.IndexOf(' '))) == int_returned_Id)
                    {
                        checkedListBox2.SetItemCheckState(i, CheckState.Checked);
                    }
                }
            }
            ds_Get_Info.Dispose();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Add_btn();
        }

        private void Add_btn()
        {
            DialogResult DLR_MessageBox = new DialogResult();
            string str_msg = "";
            string str_Test_Type = "";
            int int_result = 0;

            str_msg = validate(str_table, btn_Add.Text);

            if (rb_PreOpCheck.Checked == true)
            {
                str_Test_Type = "P";
            }
            else if (rb_batch.Checked == true)
            {
                str_Test_Type = "B";
            }
            else if (rb_Other.Checked == true)
            {
                str_Test_Type = "O";
            }

            if (str_msg.Length > 0)
            {
                DLR_MessageBox = MessageBox.Show(str_msg, "Process Factory - Maintenance", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (DLR_MessageBox != DialogResult.OK)
            {
                switch (btn_Add.Text)
                {
                    case "&Add":
                        switch (str_table)
                        {
                            case "PF_Chemicals":
                                int_result = FruPak.PF.Data.AccessLayer.PF_Chemicals.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Chemicals"), txt_code.Text, txt_Description.Text, Convert.ToBoolean(ckb_Active.Checked.ToString()), int_Current_User_Id);
                                break;

                            case "PF_Cleaning_Area":
                                int_result = FruPak.PF.Data.AccessLayer.PF_Cleaning_Area.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Cleaning_Area"), txt_code.Text, txt_Description.Text, Convert.ToBoolean(ckb_Active.Checked.ToString()), int_Current_User_Id);
                                break;

                            case "PF_Cleaning_Area_Parts":
                                int_result = FruPak.PF.Data.AccessLayer.PF_Cleaning_Area_Parts.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Cleaning_Area_Parts"), txt_code.Text, txt_Description.Text, Convert.ToBoolean(ckb_Active.Checked.ToString()), int_Current_User_Id);
                                break;

                            case "PF_Product":
                                int_result = FruPak.PF.Data.AccessLayer.PF_Product.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Product"), txt_code.Text, txt_Description.Text, Convert.ToBoolean(ckb_Active.Checked.ToString()), int_Current_User_Id);
                                break;

                            case "PF_Product_LabResults":
                                int_result = FruPak.PF.Data.AccessLayer.PF_Product_LabResults.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Product_LabResults"), Convert.ToInt32(cmb_Product.SelectedValue.ToString()), fruit1.FruitType_Id, fruit1.FruitVariety_Id, txt_code.Text, txt_Description.Text, txt_Value.Text, Convert.ToBoolean(ckb_Active.Checked.ToString()), int_Current_User_Id);
                                break;

                            case "PF_Product_Other":
                                int_result = FruPak.PF.Data.AccessLayer.PF_Product_Other.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Product_Other"), Convert.ToInt32(cmb_Product.SelectedValue.ToString()), txt_code.Text, txt_Description.Text, txt_Value.Text, Convert.ToBoolean(ckb_Active.Checked.ToString()), int_Current_User_Id);
                                break;

                            case "PF_Tests":
                                int_result = FruPak.PF.Data.AccessLayer.PF_Tests.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Tests"), txt_code.Text, txt_Description.Text, str_Test_Type, Convert.ToBoolean(ckb_Active.Checked.ToString()), int_Current_User_Id);
                                break;

                            case "PF_Reports":
                                int_result = FruPak.PF.Data.AccessLayer.PF_Reports.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Reports"), txt_code.Text, txt_Description.Text, txt_Value.Text, Convert.ToBoolean(ckb_Active.Checked.ToString()), int_Current_User_Id);
                                break;
                        }
                        break;

                    case "&Update":
                        switch (str_table)
                        {
                            case "PF_Chemicals":
                                int_result = FruPak.PF.Data.AccessLayer.PF_Chemicals.Update(int_DVG_Row_id, txt_code.Text, txt_Description.Text, Convert.ToBoolean(ckb_Active.Checked.ToString()), int_Current_User_Id);
                                break;

                            case "PF_Cleaning_Area":
                                int_result = FruPak.PF.Data.AccessLayer.PF_Cleaning_Area.Update(int_DVG_Row_id, txt_code.Text, txt_Description.Text, Convert.ToBoolean(ckb_Active.Checked.ToString()), int_Current_User_Id);
                                break;

                            case "PF_Cleaning_Area_Parts":
                                int_result = FruPak.PF.Data.AccessLayer.PF_Cleaning_Area_Parts.Update(int_DVG_Row_id, txt_code.Text, txt_Description.Text, Convert.ToBoolean(ckb_Active.Checked.ToString()), int_Current_User_Id);
                                break;

                            case "PF_Product":
                                int_result = FruPak.PF.Data.AccessLayer.PF_Product.Update(int_DVG_Row_id, txt_code.Text, txt_Description.Text, Convert.ToBoolean(ckb_Active.Checked.ToString()), int_Current_User_Id);
                                break;

                            case "PF_Product_LabResults":
                                int_result = FruPak.PF.Data.AccessLayer.PF_Product_LabResults.Update(int_DVG_Row_id, Convert.ToInt32(cmb_Product.SelectedValue.ToString()), fruit1.FruitType_Id, fruit1.FruitVariety_Id, txt_code.Text, txt_Description.Text, txt_Value.Text, Convert.ToBoolean(ckb_Active.Checked.ToString()), int_Current_User_Id);
                                break;

                            case "PF_Product_Other":
                                int_result = FruPak.PF.Data.AccessLayer.PF_Product_Other.Update(int_DVG_Row_id, Convert.ToInt32(cmb_Product.SelectedValue.ToString()), txt_code.Text, txt_Description.Text, txt_Value.Text, Convert.ToBoolean(ckb_Active.Checked.ToString()), int_Current_User_Id);
                                break;

                            case "PF_Tests":
                                int_result = FruPak.PF.Data.AccessLayer.PF_Tests.Update(int_DVG_Row_id, txt_code.Text, txt_Description.Text, str_Test_Type, Convert.ToBoolean(ckb_Active.Checked.ToString()), int_Current_User_Id);
                                break;

                            case "PF_Reports":
                                int_result = FruPak.PF.Data.AccessLayer.PF_Reports.Update(FruPak.PF.Common.Code.General.int_max_user_id("PF_Reports"), txt_code.Text, txt_Description.Text, txt_Value.Text, Convert.ToBoolean(ckb_Active.Checked.ToString()), int_Current_User_Id);
                                break;
                        }
                        break;
                }
            }
            if (int_result > 0)
            {
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                lbl_message.Text = txt_code.Text + " has been saved";
                Reset();
            }
            else
            {
                lbl_message.ForeColor = System.Drawing.Color.Red;
                lbl_message.Text = txt_code.Text + " has NOT been saved";
            }
            populate_datagridview();
        }

        /// <summary>
        /// validates the text fields on length
        /// </summary>
        /// <returns></returns>
        ///
        private DataSet ds_validate;

        private string validate(string str_table, string str_btnText)
        {
            string str_msg = "";

            switch (str_table)
            {
                case "PF_Product_LabResults":
                case "PF_Product_Other":
                    try
                    {
                        txt_code.Text = cmb_Code.SelectedItem.ToString();
                    }
                    catch
                    {
                        str_msg = str_msg + "Invalid Code: Please Enter a valid Code" + Environment.NewLine;
                    }
                    try
                    {
                        Convert.ToInt32(cmb_Product.SelectedValue.ToString());
                    }
                    catch
                    {
                        str_msg = str_msg + "Invalid Product: Please Select a valid Product from the drop down list" + Environment.NewLine;
                    }
                    break;
            }

            if (str_msg.Length == 0)
            {
                if (str_btnText == "Add")
                {
                    switch (str_table)
                    {
                        case "PF_Chemicals":
                            ds_validate = FruPak.PF.Data.AccessLayer.PF_Chemicals.Get_Info(txt_code.Text);
                            break;

                        case "PF_Cleaning_Area":
                            ds_validate = FruPak.PF.Data.AccessLayer.PF_Cleaning_Area.Get_Info(txt_code.Text);
                            break;

                        case "PF_Cleaning_Area_Parts":
                            ds_validate = FruPak.PF.Data.AccessLayer.PF_Cleaning_Area_Parts.Get_Info(txt_code.Text);
                            break;

                        case "PF_Product":
                            ds_validate = FruPak.PF.Data.AccessLayer.PF_Product.Get_Info(txt_code.Text);
                            break;

                        case "PF_Product_LabResults":
                            ds_validate = FruPak.PF.Data.AccessLayer.PF_Product_LabResults.Get_Info(Convert.ToInt32(cmb_Product.SelectedValue.ToString()), fruit1.FruitType_Id, fruit1.FruitVariety_Id, txt_code.Text);
                            break;

                        case "PF_Product_Other":
                            ds_validate = FruPak.PF.Data.AccessLayer.PF_Product_Other.Get_Info(Convert.ToInt32(cmb_Product.SelectedValue.ToString()), txt_code.Text);
                            break;

                        case "PF_Tests":
                            ds_validate = FruPak.PF.Data.AccessLayer.PF_Tests.Get_Info(txt_code.Text);
                            break;

                        case "PF_Reports":
                            ds_validate = FruPak.PF.Data.AccessLayer.PF_Reports.Get_Info(txt_code.Text);
                            break;
                    }
                    if (ds_validate != null)
                    {
                        if (Convert.ToInt32(ds_validate.Tables[0].Rows.Count.ToString()) > 0)
                        {
                            str_msg = str_msg + "Invalid Code: This Code already exists. Please choose a different Code or deactivate the old code first" + Environment.NewLine;
                        }
                        ds_validate.Dispose();
                    }
                }
            }
            if (txt_Description.TextLength == 0)
            {
                str_msg = str_msg + "Invalid Description: Please Enter a Description" + Environment.NewLine;
            }
            switch (str_table)
            {
                case "PF_Tests":
                    if (rb_PreOpCheck.Checked == false && rb_batch.Checked == false && rb_Other.Checked == false)
                    {
                        str_msg = str_msg + "Invalid Test Type: Please select one of the Radio buttons" + Environment.NewLine;
                    }
                    break;

                case "PF_Product_LabResults":
                    if (cmb_Product.SelectedValue == null)
                    {
                        str_msg = str_msg + "Invalid Product: Please select a valid product from the drop down list" + Environment.NewLine;
                    }
                    if (txt_Value.TextLength == 0)
                    {
                        str_msg = str_msg + "Invalid Value: Please enter a valid value." + Environment.NewLine;
                    }

                    if (fruit1.FruitType_Id <= 0)
                    {
                        str_msg = str_msg + "Invalid Fruit Type: Please select a valid Fruit Type from the drop down list." + Environment.NewLine;
                    }
                    if (fruit1.FruitVariety_Id <= 0)
                    {
                        str_msg = str_msg + "Invalid Fruit Variety: Please select a valid Fruit Variety from the drop down list." + Environment.NewLine;
                    }
                    break;

                case "PF_Product_Other":
                    if (cmb_Product.SelectedValue == null)
                    {
                        str_msg = str_msg + "Invalid Product: Please select a valid product from the drop down list" + Environment.NewLine;
                    }
                    if (txt_Value.TextLength == 0)
                    {
                        str_msg = str_msg + "Invalid Value: Please enter a valid value." + Environment.NewLine;
                    }
                    break;
            }
            return str_msg;
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            txt_code.ResetText();
            txt_Description.ResetText();
            rb_batch.Checked = false;
            rb_Other.Checked = false;
            rb_PreOpCheck.Checked = false;
            tabControl1.Visible = false;
            checkedListBox1.Visible = false;
            txt_Value.ResetText();
            cmb_Product.Text = null;
            fruit1.FruitType_Id = 0;
            fruit1.FruitVariety_Id = 0;

            btn_Update_Relationship.Visible = false;
            btn_Add.Text = "&Add";

            switch (str_table)
            {
                case "PF_Product_Other":
                    cmb_Code.SelectedItem = "BBD";
                    break;

                default:
                    cmb_Code.Text = null;
                    break;
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Update_Relationship_Click(object sender, EventArgs e)
        {
            switch (str_table)
            {
                case "PF_Chemicals":
                    FruPak.PF.Data.AccessLayer.PF_Product_Chemicals_Relationship.Delete_Chemical_From_Product(int_DVG_Row_id);
                    break;

                case "PF_Cleaning_Area":
                    FruPak.PF.Data.AccessLayer.PF_Clean_Area_Area_Parts_Relationship.Delete_Cleaning_Area(int_DVG_Row_id);
                    break;

                case "PF_Cleaning_Area_Parts":
                    FruPak.PF.Data.AccessLayer.PF_Clean_Area_Area_Parts_Relationship.Delete_Cleaning_Part(int_DVG_Row_id);
                    break;

                case "PF_Product":
                    FruPak.PF.Data.AccessLayer.CM_Product_Product_Group_Relationship.Delete_Group_From_Product(int_DVG_Row_id);
                    break;

                case "PF_Tests":
                    FruPak.PF.Data.AccessLayer.PF_Product_Tests_Relationship.Delete_Test_From_Product(int_DVG_Row_id);
                    break;
            }
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                int int_CLB_Id = Convert.ToInt32(checkedListBox1.Items[i].ToString().Substring(0, checkedListBox1.Items[i].ToString().IndexOf(' ')));
                if (checkedListBox1.GetItemCheckState(i) == CheckState.Checked)
                {
                    switch (str_table)
                    {
                        case "PF_Chemicals":
                            FruPak.PF.Data.AccessLayer.PF_Product_Chemicals_Relationship.Delete_Product_From_Chemical(int_CLB_Id, int_DVG_Row_id);
                            FruPak.PF.Data.AccessLayer.PF_Product_Chemicals_Relationship.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Product_Chemicals_Relationship"), int_CLB_Id, int_DVG_Row_id, int_Current_User_Id);
                            break;

                        case "PF_Cleaning_Area":
                            FruPak.PF.Data.AccessLayer.PF_Clean_Area_Area_Parts_Relationship.Delete_Cleaning_Area_Part(int_CLB_Id, int_DVG_Row_id);
                            FruPak.PF.Data.AccessLayer.PF_Clean_Area_Area_Parts_Relationship.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Clean_Area_Area_Parts_Relationship"), int_DVG_Row_id, int_CLB_Id, int_Current_User_Id);
                            break;

                        case "PF_Cleaning_Area_Parts":
                            FruPak.PF.Data.AccessLayer.PF_Product_Chemicals_Relationship.Delete_Product_From_Chemical(int_CLB_Id, int_DVG_Row_id);
                            FruPak.PF.Data.AccessLayer.PF_Clean_Area_Area_Parts_Relationship.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Clean_Area_Area_Parts_Relationship"), int_CLB_Id, int_DVG_Row_id, int_Current_User_Id);
                            break;

                        case "PF_Product":
                            FruPak.PF.Data.AccessLayer.CM_Product_Product_Group_Relationship.Delete_Product_From_Group(int_DVG_Row_id, int_CLB_Id);
                            FruPak.PF.Data.AccessLayer.CM_Product_Product_Group_Relationship.Insert(FruPak.PF.Common.Code.General.int_max_user_id("CM_Product_Product_Group_Relationship"), int_DVG_Row_id, int_CLB_Id, int_Current_User_Id);
                            break;

                        case "PF_Tests":
                            FruPak.PF.Data.AccessLayer.PF_Product_Tests_Relationship.Delete_Product_From_Test(int_CLB_Id, int_DVG_Row_id);
                            FruPak.PF.Data.AccessLayer.PF_Product_Tests_Relationship.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Product_Tests_Relationship"), int_CLB_Id, int_DVG_Row_id, int_Current_User_Id);
                            break;
                    }
                }
                else
                {
                    switch (str_table)
                    {
                        case "PF_Chemicals":
                            FruPak.PF.Data.AccessLayer.PF_Product_Chemicals_Relationship.Delete_Product_From_Chemical(int_CLB_Id, int_DVG_Row_id);
                            break;

                        case "PF_Cleaning_Area":
                            FruPak.PF.Data.AccessLayer.PF_Clean_Area_Area_Parts_Relationship.Delete_Cleaning_Area_Part(int_CLB_Id, int_DVG_Row_id);
                            break;

                        case "PF_Cleaning_Area_Parts":
                            FruPak.PF.Data.AccessLayer.PF_Product_Chemicals_Relationship.Delete_Product_From_Chemical(int_CLB_Id, int_DVG_Row_id);
                            break;

                        case "PF_Product":
                            FruPak.PF.Data.AccessLayer.CM_Product_Product_Group_Relationship.Delete_Product_From_Group(int_DVG_Row_id, int_CLB_Id);
                            break;

                        case "PF_Tests":
                            FruPak.PF.Data.AccessLayer.PF_Product_Tests_Relationship.Delete_Product_From_Test(int_CLB_Id, int_DVG_Row_id);
                            break;
                    }
                }
            }
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                int int_CLB_Id = Convert.ToInt32(checkedListBox2.Items[i].ToString().Substring(0, checkedListBox2.Items[i].ToString().IndexOf(' ')));
                if (checkedListBox2.GetItemCheckState(i) == CheckState.Checked)
                {
                    switch (str_table)
                    {
                        case "PF_Cleaning_Area":
                            FruPak.PF.Data.AccessLayer.PF_Product_Cleaning_Area_Relationship.Delete(int_CLB_Id, int_DVG_Row_id);
                            FruPak.PF.Data.AccessLayer.PF_Product_Cleaning_Area_Relationship.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Product_Cleaning_Area_Relationship"), int_CLB_Id, int_DVG_Row_id, int_Current_User_Id);
                            break;

                        case "PF_Product":
                            FruPak.PF.Data.AccessLayer.PF_Product_Cleaning_Area_Relationship.Delete(int_DVG_Row_id, int_CLB_Id);
                            FruPak.PF.Data.AccessLayer.PF_Product_Cleaning_Area_Relationship.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Product_Cleaning_Area_Relationship"), int_DVG_Row_id, int_CLB_Id, int_Current_User_Id);
                            break;
                    }
                }
                else
                {
                    switch (str_table)
                    {
                        case "PF_Cleaning_Area":
                            FruPak.PF.Data.AccessLayer.PF_Product_Cleaning_Area_Relationship.Delete(int_CLB_Id, int_DVG_Row_id);
                            break;

                        case "PF_Product":
                            FruPak.PF.Data.AccessLayer.PF_Product_Cleaning_Area_Relationship.Delete(int_DVG_Row_id, int_CLB_Id);
                            break;
                    }
                }
            }
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

        private void cmb_Code_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmb_Code.SelectedItem.ToString() == "BBD")
            {
                lbl_Value.Text = "# of Months";
            }
            else
            {
                lbl_Value.Text = "Value:";
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
        private void Work_Order_Maintenance_KeyDown(object sender, KeyEventArgs e)
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