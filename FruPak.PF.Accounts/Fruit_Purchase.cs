using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;

namespace FruPak.PF.Accounts
{
    public partial class Fruit_Purchase : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();     

        private static int int_Current_User_Id = 0;
        private static bool bol_write_access;
        private static string str_save_filename = "";
        public Fruit_Purchase(int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();
            int_Current_User_Id = int_C_User_id;
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

            AddColumnsProgrammatically();
            AddColumnsProgrammatically2();
            populate_datagridview2();

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

        private void populate_datagridview1()
        {
            DataSet ds_Get_Info;
            DataRow dr_Get_Info;
            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();

            //get list of products
            switch (btn_Add.Text)
            {
                case "&Update":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.GH_Submission.Get_ALL_Fruit_For_Customer(customer1.Customer_Id);                    
                    break;
                default:
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.GH_Submission.Get_Fruit_NOT_Purchased(customer1.Customer_Id);
                    break;
            }
            

            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                //datagridview
                dataGridView1.Rows.Add(new DataGridViewRow());

                if (dr_Get_Info["FruitPurchase_Id"].ToString() != "")
                {
                    dataGridView1.Rows[i].Cells["Select"].Value = true;
                }

                DataGridViewCell DGVC_Cell0 = new DataGridViewTextBoxCell();
                DGVC_Cell0.Value = dr_Get_Info["Submission_Id"].ToString();
                dataGridView1.Rows[i].Cells["Submission_Id"] = DGVC_Cell0;

                DataGridViewCell DGVC_Cell1 = new DataGridViewTextBoxCell();
                DGVC_Cell1.Value = dr_Get_Info["Sub_date"].ToString();
                dataGridView1.Rows[i].Cells["Submission_date"] = DGVC_Cell1;

                DataGridViewCell DGVC_Cell2 = new DataGridViewTextBoxCell();
                DGVC_Cell2.Value = dr_Get_Info["FT_Description"].ToString();
                dataGridView1.Rows[i].Cells["Fruit_type"] = DGVC_Cell2;

                DataGridViewCell DGVC_Cell3 = new DataGridViewTextBoxCell();
                DGVC_Cell3.Value = dr_Get_Info["FV_Description"].ToString();
                dataGridView1.Rows[i].Cells["Fruit_variety"] = DGVC_Cell3;

                DataGridViewCell DGVC_Cell4 = new DataGridViewTextBoxCell();
                DGVC_Cell4.Value = dr_Get_Info["Bin_Count"].ToString();
                dataGridView1.Rows[i].Cells["Bin_Count"] = DGVC_Cell4;

                DataGridViewCell DGVC_Cell5 = new DataGridViewTextBoxCell();
                DGVC_Cell5.Value = dr_Get_Info["Gross_Weight"].ToString();
                dataGridView1.Rows[i].Cells["Gross_Weight"] = DGVC_Cell5;

                DataGridViewCell DGVC_Cell6 = new DataGridViewTextBoxCell();
                DGVC_Cell6.Value = dr_Get_Info["Tare_Weight"].ToString();
                dataGridView1.Rows[i].Cells["Tare_Weight"] = DGVC_Cell6;

                DataGridViewCell DGVC_Cell7 = new DataGridViewTextBoxCell();
                DGVC_Cell7.Value = dr_Get_Info["Material_Id"].ToString();
                dataGridView1.Rows[i].Cells["Material_Id"] = DGVC_Cell7;

                DataGridViewCell DGVC_Cell8 = new DataGridViewTextBoxCell();
                DGVC_Cell8.Value = dr_Get_Info["Material_Num"].ToString();
                dataGridView1.Rows[i].Cells["Material_Num"] = DGVC_Cell8;

                DataGridViewCell DGVC_Cell9 = new DataGridViewTextBoxCell();
                DGVC_Cell9.Value = dr_Get_Info["Value"].ToString();
                dataGridView1.Rows[i].Cells["Cost"] = DGVC_Cell9;

                decimal gross = 0;
                decimal tare = 0;
                decimal cost = 0;
                try
                {
                    gross = Convert.ToDecimal(dr_Get_Info["Gross_Weight"].ToString());
                }
                catch
                {
                    gross = 0;
                }

                try
                {
                    tare = Convert.ToDecimal(dr_Get_Info["Tare_Weight"].ToString());
                }
                catch
                {
                    tare = 0;
                }
                try
                {
                    cost = Convert.ToDecimal(dr_Get_Info["Value"].ToString());
                }
                catch
                {
                    cost = 0;
                }
                dataGridView1.Rows[i].Cells["Total"].Value = (gross - tare) * cost;

            }
            ds_Get_Info.Dispose();
            Column_resize();
        }
        private void cmb_Customer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (customer1.CFocused == true)
            {               
                populate_datagridview1();
            }
        }
        private void AddColumnsProgrammatically()
        {
            DataGridViewCheckBoxColumn ckb_select = new DataGridViewCheckBoxColumn();
            dataGridView1.Columns.Add(ckb_select);
            ckb_select.HeaderText = "Select";
            ckb_select.Name = "Select";

            var col0 = new DataGridViewTextBoxColumn();
            var col1 = new DataGridViewTextBoxColumn();
            var col2 = new DataGridViewTextBoxColumn();
            var col3 = new DataGridViewTextBoxColumn();
            var col4 = new DataGridViewTextBoxColumn();
            var col5 = new DataGridViewTextBoxColumn();
            var col6 = new DataGridViewTextBoxColumn();
            var col7 = new DataGridViewTextBoxColumn();
            var col8 = new DataGridViewTextBoxColumn();
            var col9 = new DataGridViewTextBoxColumn();

            col0.HeaderText = "Sub ID";
            col0.Name = "Submission_Id";
            col0.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col0.ReadOnly = true;

            col1.HeaderText = "Sub Date";
            col1.Name = "Submission_date";
            col1.ReadOnly = true;

            col2.HeaderText = "Fruit Type";
            col2.Name = "Fruit_type";
            col2.ReadOnly = true;

            col3.HeaderText = "Fruit Variety";
            col3.Name = "Fruit_variety";
            col3.ReadOnly = true;

            col4.HeaderText = "Bins";
            col4.Name = "Bin_Count";
            col4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col4.ReadOnly = true;

            col5.HeaderText = "Gross";
            col5.Name = "Gross_Weight";
            col5.ReadOnly = true;
            col5.Visible = true;

            col6.HeaderText = "Tare";
            col6.Name = "Tare_Weight";
            col6.ReadOnly = true;
            col6.Visible = true;

            col7.HeaderText = "Material_Id";
            col7.Name = "Material_Id";
            col7.ReadOnly = true;
            col7.Visible = false;

            col8.HeaderText = "Material_Num";
            col8.Name = "Material_Num";
            col8.ReadOnly = true;
            col8.Visible = false;

            col9.HeaderText = "Cost";
            col9.Name = "Cost";
            col9.ReadOnly = true;
            col9.Visible = true;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2, col3, col4, col5, col6, col7, col8, col9 });

            DataGridViewTextBoxColumn txt_total = new DataGridViewTextBoxColumn();
            dataGridView1.Columns.Add(txt_total);
            txt_total.HeaderText = "Total";
            txt_total.Name = "Total";
        }
        private void AddColumnsProgrammatically2()
        {
            var col0 = new DataGridViewTextBoxColumn();
            var col1 = new DataGridViewTextBoxColumn();
            var col2 = new DataGridViewTextBoxColumn();
            var col3 = new DataGridViewTextBoxColumn();
            var col4 = new DataGridViewTextBoxColumn();
            var col5 = new DataGridViewTextBoxColumn();

            col0.HeaderText = "Purchase ID";
            col0.Name = "FruitPurchase_Id";
            col0.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col0.ReadOnly = true;

            col1.HeaderText = "Customer Id";
            col1.Name = "Customer_Id";
            col1.ReadOnly = true;
            col1.Visible = false;

            col2.HeaderText = "Customer";
            col2.Name = "Customer";
            col2.ReadOnly = true;

            col3.HeaderText = "Date to Office";
            col3.Name = "Date_to_Office";
            col3.ReadOnly = true;

            col4.HeaderText = "Comments";
            col4.Name = "Comments";
            col4.Width = 200;
            col4.ReadOnly = true;

            col5.HeaderText = "Cost";
            col5.Name = "Cost";
            col5.ReadOnly = true;

            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2, col3, col4, col5 });

            DataGridViewImageColumn img_delete = new DataGridViewImageColumn();
            dataGridView2.Columns.Add(img_delete);
            img_delete.HeaderText = "Delete";
            img_delete.Name = "Delete";
            img_delete.Image = FruPak.PF.Global.Properties.Resources.delete;
            img_delete.ReadOnly = true;
            img_delete.Visible = bol_write_access;

            DataGridViewImageColumn img_edit = new DataGridViewImageColumn();
            dataGridView2.Columns.Add(img_edit);
            img_edit.HeaderText = "Edit";
            img_edit.Name = "Edit";
            img_edit.Image = FruPak.PF.Global.Properties.Resources.edit;
            img_edit.ReadOnly = true;

            DataGridViewButtonColumn btn_Submit = new DataGridViewButtonColumn();
            dataGridView2.Columns.Add(btn_Submit);
            btn_Submit.Text = "Submit";
            btn_Submit.Name = "Submit";
           //btn_Submit.UseColumnTextForButtonValue = true;
            
        }
        private void populate_datagridview2()
        {
            DataSet ds_Get_Info;
            DataRow dr_Get_Info;
            dataGridView2.Refresh();
            dataGridView2.Rows.Clear();

            //get list of products
            ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_A_Fruit_Purchase.Get_Info_Translated();

            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                //datagridview
                dataGridView2.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_Cell0 = new DataGridViewTextBoxCell();
                DGVC_Cell0.Value = dr_Get_Info["FruitPurchase_Id"].ToString();
                dataGridView2.Rows[i].Cells["FruitPurchase_Id"] = DGVC_Cell0;

                DataGridViewCell DGVC_Cell1 = new DataGridViewTextBoxCell();
                DGVC_Cell1.Value = dr_Get_Info["Customer_Id"].ToString();
                dataGridView2.Rows[i].Cells["Customer_Id"] = DGVC_Cell1;

                DataGridViewCell DGVC_Cell2 = new DataGridViewTextBoxCell();
                DGVC_Cell2.Value = dr_Get_Info["Name"].ToString();
                dataGridView2.Rows[i].Cells["Customer"] = DGVC_Cell2;

                DataGridViewCell DGVC_Cell3 = new DataGridViewTextBoxCell();
                DGVC_Cell3.Value = dr_Get_Info["Date_to_Office"].ToString();
                dataGridView2.Rows[i].Cells["Date_to_Office"] = DGVC_Cell3;

                if (dataGridView2.Rows[i].Cells["Date_to_Office"].Value.ToString() != "")
                {

                    dataGridView2.Rows[i].Cells["Submit"].Value = "Resend";
                }
                else
                {
                    dataGridView2.Rows[i].Cells["Submit"].Value = "Submit";
                }

                DataGridViewCell DGVC_Cell4 = new DataGridViewTextBoxCell();
                DGVC_Cell4.Value = dr_Get_Info["Comments"].ToString();
                dataGridView2.Rows[i].Cells["Comments"] = DGVC_Cell4;

                DataGridViewCell DGVC_Cell5 = new DataGridViewTextBoxCell();
                DGVC_Cell5.Value = dr_Get_Info["Cost"].ToString();
                dataGridView2.Rows[i].Cells["Cost"] = DGVC_Cell5;
            }
            ds_Get_Info.Dispose();
            Column_resize();
        }
        private void SizeAllColumns(Object sender, EventArgs e)
        {
            Column_resize();
        }
        private void Column_resize()
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

            dataGridView2.AutoResizeColumn(0, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView2.AutoResizeColumn(1, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView2.AutoResizeColumn(2, DataGridViewAutoSizeColumnMode.AllCells);            
            dataGridView2.AutoResizeColumn(3, DataGridViewAutoSizeColumnMode.AllCells);            
            dataGridView2.AutoResizeColumn(5, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView2.AutoResizeColumn(6, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView2.AutoResizeColumn(7, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView2.AutoResizeColumn(8, DataGridViewAutoSizeColumnMode.AllCells);  
       
        }
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btn_reset_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void Reset()
        {
            customer1.Customer_Id = 0;
            customer1.Enabled = true;
            btn_Add.Text = "&Add Submission(s) to Purchase";
            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();
            nud_total.Value = 0;

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCheckBoxCell checkbox = (DataGridViewCheckBoxCell)dataGridView1.CurrentCell;
            bool isChecked = (bool)checkbox.EditedFormattedValue;
            if ((bool)checkbox.EditedFormattedValue == true)
            {
                nud_total.Value = nud_total.Value + Convert.ToDecimal(dataGridView1.CurrentRow.Cells["Total"].Value.ToString());
            }
            else
            {
                nud_total.Value = nud_total.Value - Convert.ToDecimal(dataGridView1.CurrentRow.Cells["Total"].Value.ToString());
            }
            
        }
        private void btn_Add_Click(object sender, EventArgs e)
        {
            DialogResult DLR_Message = new DialogResult();
            string str_msg = "";
            int int_result = 0;

            if (customer1.Customer_Id <= 0)
            {
                str_msg = str_msg + "Invalid Customer. Please Select a valid Customer from the drop down list." + Environment.NewLine;
            }

            //check if any have been selected
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell checkbox = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells["Select"];
                
                if ((bool)checkbox.EditedFormattedValue == true)
                {
                    int_result = int_result + 1;
                }
            }
            if (int_result == 0)
            {
                str_msg = str_msg + "No Submissions have been selected for Payment. Please select valid submissions from the list." + Environment.NewLine;
            }

            if (str_msg.Length > 0)
            {
                DLR_Message = MessageBox.Show(str_msg, "Process Factory - Fruit Payment.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (DLR_Message != System.Windows.Forms.DialogResult.OK)
            {
                switch (btn_Add.Text)
                {
                    #region ------------- Update -------------
                    case "&Update":
                        // reset fruit_purchade_ID to null 
                        int_result = int_result + FruPak.PF.Data.AccessLayer.GH_Submission.Reset_Purchase(int_DGV_Cell0, int_Current_User_Id);

                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            DataGridViewCheckBoxCell checkbox = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells["Select"];
                            if ((bool)checkbox.EditedFormattedValue == true)
                            {
                                int_result = int_result + FruPak.PF.Data.AccessLayer.GH_Submission.Update_Purchase(Convert.ToInt32(dataGridView1.Rows[i].Cells["Submission_Id"].Value.ToString()), int_DGV_Cell0, int_Current_User_Id);
                            }
                        }

                        int_result = int_result + FruPak.PF.Data.AccessLayer.PF_A_Fruit_Purchase.Update(int_DGV_Cell0, customer1.Customer_Id, nud_total.Value, txt_Comments.Text, int_Current_User_Id);
                        break;
                    #endregion
                    #region ------------- default -------------
                    default:
                        int int_FruitPurchase_Id = FruPak.PF.Common.Code.General.int_max_user_id("PF_A_Fruit_Purchase");
                        int_result = FruPak.PF.Data.AccessLayer.PF_A_Fruit_Purchase.insert(int_FruitPurchase_Id,customer1.Customer_Id, nud_total.Value, txt_Comments.Text, int_Current_User_Id);

                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            DataGridViewCheckBoxCell checkbox = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells["Select"];
                
                            if ((bool)checkbox.EditedFormattedValue == true)
                            {
                                int_result = int_result + FruPak.PF.Data.AccessLayer.GH_Submission.Update_Purchase(Convert.ToInt32(dataGridView1.Rows[i].Cells["Submission_Id"].Value.ToString()), int_FruitPurchase_Id, int_Current_User_Id);
                            }
                        }
                        break;
                    #endregion
                }
            }

            if (int_result > 0)
            {
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                lbl_message.Text = "Record has been Saved";
            }
            else
            {
                lbl_message.ForeColor = System.Drawing.Color.Red;
                lbl_message.Text = "Record failed to Save.";
            }
            populate_datagridview2();
            Reset();
        }
        private void btn_sub_Click(int int_fruitPurchase_id, string Customer)
        {

            FruPak.PF.Data.Excel.Excel.FilePath = FruPak.PF.Common.Code.General.Get_Single_System_Code("PF-SPath");
      
            str_save_filename = Customer +" - " + DateTime.Now.ToString("yyyyMMdd") + ".PDF";
            FruPak.PF.Data.Excel.Excel.FileName = str_save_filename;

            FruPak.PF.Data.Excel.Excel.startExcel();
            
            FruPak.PF.Data.Excel.Excel.Add_Data_Text("A1", "Customer: " + Customer);
            FruPak.PF.Data.Excel.Excel.Font_Size("A1", 18);
            FruPak.PF.Data.Excel.Excel.Font_Bold("A1", true);

            int ip = 3;
            FruPak.PF.Data.Excel.Excel.Add_Data_Text("B" + Convert.ToString(ip), "Fruit Type");
            FruPak.PF.Data.Excel.Excel.Font_Bold("B" + Convert.ToString(ip), true);
            FruPak.PF.Data.Excel.Excel.Font_Size("B" + Convert.ToString(ip), 12);
            FruPak.PF.Data.Excel.Excel.Add_Data_Text("C" + Convert.ToString(ip), "Fruit Variety");
            FruPak.PF.Data.Excel.Excel.Font_Bold("C" + Convert.ToString(ip), true);
            FruPak.PF.Data.Excel.Excel.Font_Size("C" + Convert.ToString(ip), 12);
            FruPak.PF.Data.Excel.Excel.Add_Data_Text("D" + Convert.ToString(ip), "Bins");
            FruPak.PF.Data.Excel.Excel.Font_Bold("D" + Convert.ToString(ip), true);
            FruPak.PF.Data.Excel.Excel.Font_Size("D" + Convert.ToString(ip), 12);
            FruPak.PF.Data.Excel.Excel.Add_Data_Text("E" + Convert.ToString(ip), "Net Weight");
            FruPak.PF.Data.Excel.Excel.Font_Bold("E" + Convert.ToString(ip), true);
            FruPak.PF.Data.Excel.Excel.Font_Size("E" + Convert.ToString(ip), 12);
            FruPak.PF.Data.Excel.Excel.Add_Data_Text("F" + Convert.ToString(ip), "Cost");
            FruPak.PF.Data.Excel.Excel.Font_Bold("F" + Convert.ToString(ip), true);
            FruPak.PF.Data.Excel.Excel.Font_Size("F" + Convert.ToString(ip), 12);

            DataSet ds = FruPak.PF.Data.AccessLayer.PF_A_Fruit_Purchase.Get_Purchase_Info(int_fruitPurchase_id);
            DataRow dr;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ip++; // increase the positional counter

                dr = ds.Tables[0].Rows[i];
                FruPak.PF.Data.Excel.Excel.Add_Data_Text("B" + Convert.ToString(ip), dr["FT_Description"].ToString());
                FruPak.PF.Data.Excel.Excel.Add_Data_Text("C" + Convert.ToString(ip), dr["FV_Description"].ToString());
                FruPak.PF.Data.Excel.Excel.Add_Data_Int("D" + Convert.ToString(ip), Convert.ToInt32(dr["Bin_Count"].ToString()));
                FruPak.PF.Data.Excel.Excel.Set_Horizontal_Aligment("D" + Convert.ToString(ip), "Center");
                FruPak.PF.Data.Excel.Excel.Add_Data_Dec("E" + Convert.ToString(ip), Convert.ToDecimal(dr["Gross_Weight"].ToString()) - Convert.ToDecimal(dr["Tare_Weight"].ToString()), 2);
                FruPak.PF.Data.Excel.Excel.Set_Horizontal_Aligment("E" + Convert.ToString(ip), "Center");
                FruPak.PF.Data.Excel.Excel.Add_Data_Dec("F" + Convert.ToString(ip), Convert.ToDecimal(dr["Cost"].ToString()),2);                
                
                FruPak.PF.Data.Excel.Excel.Set_Column_Width(2);
                FruPak.PF.Data.Excel.Excel.Set_Column_Width(3);
                FruPak.PF.Data.Excel.Excel.Set_Column_Width(4);
                FruPak.PF.Data.Excel.Excel.Set_Column_Width(5);
            }
            ds.Dispose();

            FruPak.PF.Data.Excel.Excel.SaveAS();
            FruPak.PF.Data.Excel.Excel.SaveASPDF();
            FruPak.PF.Data.Excel.Excel.CloseExcel();
        }
        private static int int_DGV_Cell0 = 0;
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string str_save_filename = "";
            int int_result = 0;

            #region ------------- Submit Button -------------
            if (e.ColumnIndex == 8)
            {
                btn_sub_Click(Convert.ToInt32(dataGridView2.CurrentRow.Cells["FruitPurchase_Id"].Value.ToString()), dataGridView2.CurrentRow.Cells["Customer"].Value.ToString());

                int_result = FruPak.PF.Common.Code.Outlook.Email_Office("FP-Off2%", str_save_filename, "");

                if (int_result == 0)
                {
                    int_result = FruPak.PF.Data.AccessLayer.PF_A_Fruit_Purchase.Update(Convert.ToInt32(dataGridView2.CurrentRow.Cells[""].Value.ToString()), DateTime.Now.ToString("yyyyMMdd"), int_Current_User_Id);
                }
                if (int_result > 0)
                {
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    lbl_message.Text = "Fruit Purchase has been sent to the office";
                    Reset();
                    populate_datagridview2();
                }
                else
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = "Fruit_Purchase failed to sent to the Office";
                }
            }
            #endregion
            #region ------------- Edit -------------
            else if (e.ColumnIndex == 7)
            {
                btn_Add.Text = "&Update";
                int_DGV_Cell0 = Convert.ToInt32(dataGridView2.CurrentRow.Cells["FruitPurchase_Id"].Value.ToString());
                customer1.Customer_Id = Convert.ToInt32(dataGridView2.CurrentRow.Cells["Customer_Id"].Value.ToString());
                customer1.Enabled = false;
                nud_total.Value = Convert.ToDecimal(dataGridView2.CurrentRow.Cells["Cost"].Value.ToString());
                populate_datagridview1();
            }
            #endregion
            #region ------------- Delete -------------
            else if (e.ColumnIndex == 6)
            {
                string Data = "";
                Data = Data + dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                Data = Data + ":" + Convert.ToString(int_Current_User_Id);

                int_result = FruPak.PF.Common.Code.General.Delete_Record("PF_A_Fruit_Purchase", Data, dataGridView2.Rows[e.RowIndex].Cells["Customer"].Value.ToString() + " (" + dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString() + ")");

                if (int_result > 0)
                {
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    lbl_message.Text = dataGridView2.Rows[e.RowIndex].Cells["Customer"].Value.ToString() + " (" + dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString() + ")" + " has been deleted";
                }
                else
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = dataGridView2.Rows[e.RowIndex].Cells["Customer"].Value.ToString() + " (" + dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString() + ")" + " failed to delete";
                }
                populate_datagridview2();
            }
            #endregion
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
        private void Fruit_Purchase_KeyDown(object sender, KeyEventArgs e)
        {
            // Note that the KeyPreview property must be set to true for this to work
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        #endregion

        private void Fruit_Purchase_FormClosing(object sender, FormClosingEventArgs e)
        {
            logger.Log(LogLevel.Info, "---------------------[ FruPak.PF.Accounts.Fruit_Purchase Form Closing]--- :" + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToLongTimeString());
        }
    }
}