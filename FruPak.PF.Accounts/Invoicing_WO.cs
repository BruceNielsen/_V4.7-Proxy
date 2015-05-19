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
    public partial class Invoicing_WO : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();     

        private static int int_Current_User_Id = 0;        
        private static bool bol_write_access;
        private static int int_Invoice_Id = 0;
        private static DialogResult DLR_Message;

        

        public Invoicing_WO(int int_C_User_id, bool bol_w_a)
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

            //set up outlook locations
            FruPak.PF.Data.Outlook.Outlook.Folder_Name = FruPak.PF.Common.Code.Outlook.SetUp_Location("PF-Contact");
            FruPak.PF.Data.Outlook.Outlook.Folder_Name_Location = FruPak.PF.Data.Outlook.Outlook.Folder_Name_Location;


            populate_combobox();
            AddColumnsProgrammatically();
            AddColumnsProgrammatically2();

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
        private void AddColumnsProgrammatically()
        {
            var col0 = new DataGridViewTextBoxColumn();
            var col1 = new DataGridViewTextBoxColumn();
            var col2 = new DataGridViewTextBoxColumn();
            var col3 = new DataGridViewTextBoxColumn();
            var col4 = new DataGridViewTextBoxColumn();

            col0.HeaderText = "Rates Id";
            col0.Name = "Rates_Id";
            col0.ReadOnly = true;
            col0.Visible = false;

            col1.HeaderText = "Description";
            col1.Name = "Description";
            col1.ReadOnly = true;

            col2.HeaderText = "Value";
            col2.Name = "Value";
            col2.ReadOnly = true;

            col3.HeaderText = "Total Items";
            col3.Name = "Total_Items";
            col3.ReadOnly = true;

            col4.HeaderText = "Total Amount";
            col4.Name = "Total_Amt";

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2, col3, col4 });

            DataGridViewImageColumn img_delete = new DataGridViewImageColumn();
            dataGridView1.Columns.Add(img_delete);
            img_delete.HeaderText = "Remove";
            img_delete.Name = "Remove";
            img_delete.Image = FruPak.PF.Global.Properties.Resources.delete;
            img_delete.ReadOnly = true;
            img_delete.Visible = bol_write_access;
        }
        private void AddColumnsProgrammatically2()
        {
            DataGridViewCheckBoxColumn ckb_select = new DataGridViewCheckBoxColumn();
            dataGridView2.Columns.Add(ckb_select);
            ckb_select.HeaderText = "Select";
            ckb_select.Name = "Select";

            var col0 = new DataGridViewTextBoxColumn();
            var col1 = new DataGridViewTextBoxColumn();
            var col2 = new DataGridViewTextBoxColumn();
            var col3 = new DataGridViewTextBoxColumn();
            var col4 = new DataGridViewTextBoxColumn();

            col0.HeaderText = "Work Order Id";
            col0.Name = "Work_Order_Id";
            col0.ReadOnly = true;

            col1.HeaderText = "Process Date";
            col1.Name = "Process_Date";
            col1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col1.ReadOnly = true;

            col2.HeaderText = "Customer Order";
            col2.Name = "Customer_Order";
            col2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            col2.ReadOnly = true;

            col3.HeaderText = "Process";
            col3.Name = "P_Description";
            col3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            col3.ReadOnly = true;

            col4.HeaderText = "Fruit";
            col4.Name = "FT_Description";
            col4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            col4.ReadOnly = true;
            

            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2, col3, col4 });
        }
        private void populate_datagridview(string WHERE)
        {
            DataSet ds_Get_Info = null;
            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();


            ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Work_Order.Get_Invoice_Rate(WHERE);
            DataRow dr_Get_Info;
            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                dataGridView1.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_Cell0 = new DataGridViewTextBoxCell();
                DGVC_Cell0.Value = dr_Get_Info["Rates_Id"].ToString();
                dataGridView1.Rows[i].Cells[0] = DGVC_Cell0;

                DataGridViewCell DGVC_Cell1 = new DataGridViewTextBoxCell();
                DGVC_Cell1.Value = dr_Get_Info["Description"].ToString();
                dataGridView1.Rows[i].Cells[1] = DGVC_Cell1;

                DataGridViewCell DGVC_Cell2 = new DataGridViewTextBoxCell();
                DGVC_Cell2.Value = dr_Get_Info["Value"].ToString();
                dataGridView1.Rows[i].Cells[2] = DGVC_Cell2;

                DataGridViewCell DGVC_Cell3 = new DataGridViewTextBoxCell();

                try
                {
                    string str_total_item = FruPak.PF.Common.Code.General.Calculate_Totals(dr_Get_Info["Code"].ToString().ToUpper().Substring(0, dr_Get_Info["Code"].ToString().IndexOf('-')), "(" + int_Where[i].ToString() + ")");
                    DGVC_Cell3.Value = str_total_item;
                    dataGridView1.Rows[i].Cells["Total_Items"] = DGVC_Cell3;
                }
                catch (Exception ex)
                {
                    logger.Log(LogLevel.Debug, ex.Message);
                    MessageBox.Show("Rates have not been linked to the Material Number.", "Process Factory - Invoicing Work Orders", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }                    



                DataGridViewCell DGVC_Cell4 = new DataGridViewTextBoxCell();
                try
                {
                    DGVC_Cell4.Value = Math.Round(Convert.ToDecimal(DGVC_Cell3.Value.ToString()) * Convert.ToDecimal(DGVC_Cell2.Value),2);
                    dataGridView1.Rows[i].Cells["Total_Amt"] = DGVC_Cell4;
                }
                catch (Exception ex)
                {
                    logger.Log(LogLevel.Debug, ex.Message);
                }
            }


            Column_resize();
            ds_Get_Info.Dispose();
        }
        private void populate_datagriview2()
        {
            dataGridView2.Refresh();
            dataGridView2.Rows.Clear();
            DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Work_Order.Get_UnInvoiced_for_Trader(Convert.ToInt32(cmb_Trader.SelectedValue.ToString())); 
            DataRow dr_Get_Info;

            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                dataGridView2.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_Cell0 = new DataGridViewTextBoxCell();
                DGVC_Cell0.Value = dr_Get_Info["Work_Order_Id"].ToString();
                dataGridView2.Rows[i].Cells["Work_Order_Id"] = DGVC_Cell0;

                DataGridViewCell DGVC_Cell1 = new DataGridViewTextBoxCell();
                DGVC_Cell1.Value = dr_Get_Info["Process_Date"].ToString();
                dataGridView2.Rows[i].Cells["Process_Date"] = DGVC_Cell1;

                DataGridViewCell DGVC_Cell2 = new DataGridViewTextBoxCell();
                DGVC_Cell2.Value = dr_Get_Info["P_Description"].ToString();
                dataGridView2.Rows[i].Cells["P_Description"] = DGVC_Cell2;

                DataGridViewCell DGVC_Cell3 = new DataGridViewTextBoxCell();
                DGVC_Cell3.Value = dr_Get_Info["FT_Description"].ToString();
                dataGridView2.Rows[i].Cells["FT_Description"] = DGVC_Cell3;

                
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
            // TODO: This line of code loads data into the 'moneyDataSet.List_TS_Work' table. You can move, or remove it, as needed.
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataGridView2.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);            
        }        
        public void populate_combobox()
        {
            DataSet ds_Get_Info;
            ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Trader.Get_Info();
            cmb_Trader.DataSource = ds_Get_Info.Tables[0];
            cmb_Trader.DisplayMember = "Description";
            cmb_Trader.ValueMember = "Trader_Id";
            cmb_Trader.Text = null;
            ds_Get_Info.Dispose();

            ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_A_Rates.Get_Info();
            cmb_Rates.DataSource = ds_Get_Info.Tables[0];
            cmb_Rates.DisplayMember = "Description";
            cmb_Rates.ValueMember = "Rates_Id";
            cmb_Rates.Text = null;
            ds_Get_Info.Dispose();
        }

        /// <summary>
        /// filters the work order combobox accouting to the selected trader
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmb_Trader_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Trader.Focused == true)
            {
                dataGridView1.Rows.Clear();
                populate_datagriview2();
            }
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4 && e.RowIndex > -1)
            {
                
                dataGridView1.Rows[e.RowIndex].Cells[2].Value = 
                    Math.Round(
                    Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString()) / Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString()),
                3);
            }
 
            Column_resize();
        }
        private string str_WHERE = "";
        private List<int> int_Where = new List<int>();
        /// <summary>
        /// This processes the informat selected from the checklistbox, and adds the base rates to the Datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Add_Click(object sender, EventArgs e)
        {
            int int_zero_selected = 0;
            // string str_get_wo = "(";
            for (int i_wo = 0; i_wo < dataGridView2.Rows.Count; i_wo++)
            {
                DataGridViewCheckBoxCell checkbox = (DataGridViewCheckBoxCell)dataGridView2.Rows[i_wo].Cells["Select"];
                bool isChecked = (bool)checkbox.EditedFormattedValue;
                if ((bool)checkbox.EditedFormattedValue == true)
                {
                    string str_get_wo = "(";

                    str_get_wo = str_get_wo + dataGridView2.Rows[i_wo].Cells["Work_Order_Id"].Value.ToString() + ",";

                    str_get_wo = str_get_wo + ")";

                    str_WHERE = str_get_wo.Replace(",)", ")");

                    txt_order_num.Text = str_WHERE.Replace(")", "").Replace("(", "");
                    int_Where.Add(Convert.ToInt32(dataGridView2.Rows[i_wo].Cells["Work_Order_Id"].Value.ToString()));
                    populate_datagridview(str_WHERE);
                }
                else
                {
                    int_zero_selected++;
                }
            }
            cmb_Rates.Visible = true;
            btn_Add_Rates.Visible = true;
        }
        /// <summary>
        /// this will add any extra rates required for the invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Add_Rates_Click(object sender, EventArgs e)
        {
            DataSet ds = FruPak.PF.Data.AccessLayer.PF_A_Rates.Get_Info(Convert.ToInt32(cmb_Rates.SelectedValue.ToString()));
            DataRow dr;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dr = ds.Tables[0].Rows[i];
                if (dr["Code"].ToString().ToUpper().Substring(0, dr["Code"].ToString().IndexOf("-")) == "FREIGHT")
                {
                    if (nud_freight.Value <= 0)
                    {

                        DLR_Message = MessageBox.Show("Invalid Freight Amount. Please load a valid freight amount.", "Process Factory Invoice(Sales).", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            ds.Dispose();
            if (DLR_Message != System.Windows.Forms.DialogResult.OK)
            {
                Manual_populate_DGV();
            }
        }
        private void Manual_populate_DGV()
        {
            DataSet ds_Rates = FruPak.PF.Data.AccessLayer.PF_A_Rates.Get_Info(Convert.ToInt32(cmb_Rates.SelectedValue.ToString()));
            DataRow dr_Rates;
            string str_get_wo = "(";
            foreach (int id in int_Where)
            {
                str_get_wo = str_get_wo + Convert.ToString(id) + ",";
            }
            str_get_wo = str_get_wo + ")";

            str_WHERE = str_get_wo.Replace(",)", ")");

            for (int i = 0; i < Convert.ToInt32(ds_Rates.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Rates = ds_Rates.Tables[0].Rows[i];
                string str_total_item = "";
                if (dr_Rates["Code"].ToString().ToUpper().Substring(0, dr_Rates["Code"].ToString().IndexOf('-')) == "STORAGE")
                {
                    str_total_item = FruPak.PF.Common.Code.General.Calculate_Totals(dr_Rates["Code"].ToString().ToUpper(), str_WHERE);
                }
                else
                {
                    str_total_item = FruPak.PF.Common.Code.General.Calculate_Totals(dr_Rates["Code"].ToString().ToUpper().Substring(0, dr_Rates["Code"].ToString().IndexOf('-')), str_WHERE);
                }

                if (dr_Rates["Code"].ToString().ToUpper().Substring(0, dr_Rates["Code"].ToString().IndexOf('-')) == "FREIGHT")
                {
                    dataGridView1.Rows.Add(cmb_Rates.SelectedValue.ToString(),
                                           dr_Rates["Description"].ToString(),
                                           dr_Rates["value"].ToString(),
                                           nud_freight.Value,
                                           Convert.ToDecimal(nud_freight.Value) * Convert.ToDecimal(str_total_item));
                }
                else
                {
                    dataGridView1.Rows.Add(cmb_Rates.SelectedValue.ToString(), 
                                           dr_Rates["Description"].ToString(),
                                           dr_Rates["value"].ToString(), 
                                           str_total_item, 
                                           Convert.ToDecimal(dr_Rates["value"].ToString()) * Convert.ToDecimal(str_total_item));
                }             
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult DLR_Message = new DialogResult();
            int int_result = 0;
            //Delete
            if (e.ColumnIndex == 5)
            {
                string str_msg;

                str_msg = "You are about to remove " + Environment.NewLine;
                str_msg = str_msg + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + Environment.NewLine;
                str_msg = str_msg + "Do you want to continue?";
                DLR_Message = MessageBox.Show(str_msg, "Process Factory - Invoicing(Delete)", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (DLR_Message == DialogResult.Yes)
                {
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                    int_result = 1;
                }

                if (int_result > 0)
                {
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    lbl_message.Text = "Item has been Removed from the invoice";
                }
                else
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = "Item was NOT Removed from the invoice";
                }

            }
        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
           DLR_Message = new DialogResult();
            string str_msg = "";
            int int_result = 0;

            if (dataGridView1.RowCount == 0)
            {
                str_msg = str_msg + "NO Work Orders have been selected for Invoicing. Please select at least one Work Order for this invoice." + Environment.NewLine;
            }

            if (str_msg.Length > 0)
            {
                DLR_Message = MessageBox.Show(str_msg, "Process Factory - Accounting (Invoice)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (DLR_Message != System.Windows.Forms.DialogResult.OK)
            {
                int_Invoice_Id = FruPak.PF.Common.Code.General.int_max_user_id("PF_A_Invoice");

                //Get Customer ID
                int int_customer_id = 0;
                DataSet ds = FruPak.PF.Data.AccessLayer.CM_Trader.Get_Customer_Id(Convert.ToInt32(cmb_Trader.SelectedValue.ToString()));
                DataRow dr;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dr = ds.Tables[0].Rows[i];
                    int_customer_id = Convert.ToInt32(dr["Customer_Id"].ToString());
                }

                DateTime dt_invoice = new DateTime();

                dt_invoice = Convert.ToDateTime(dtp_Invoice_Date.Value.ToString());

                //Create Invoice Record.
                int_result = FruPak.PF.Data.AccessLayer.PF_A_Invoice.Insert(int_Invoice_Id, dt_invoice.Year.ToString() + '-' + dt_invoice.Month.ToString() + "-" + dt_invoice.Day.ToString(), Convert.ToInt32(cmb_Trader.SelectedValue.ToString()), txt_comments.Text, txt_order_num.Text, int_Current_User_Id, int_customer_id);

                // create detail records for the invoice
                for (int i = 0; i < Convert.ToInt32(dataGridView1.RowCount); i++)
                {
                    if (int_result > 0)
                    {
                        int_result = int_result + FruPak.PF.Data.AccessLayer.PF_A_Invoice_Details.Insert_Rates(FruPak.PF.Common.Code.General.int_max_user_id("PF_A_Invoice_Details"), int_Invoice_Id, Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value.ToString()), Math.Round(Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value.ToString()),2), int_Current_User_Id);
                    }
                }

                //update Selected Work Orders with Invoice_Id
                for (int i_wo = 0; i_wo < dataGridView2.Rows.Count; i_wo++)
                {
                    DataGridViewCheckBoxCell checkbox = (DataGridViewCheckBoxCell)dataGridView2.Rows[i_wo].Cells["Select"];
                    bool isChecked = (bool)checkbox.EditedFormattedValue;
                    if ((bool)checkbox.EditedFormattedValue == true)
                    {
                        int_result = int_result + FruPak.PF.Data.AccessLayer.PF_Work_Order.Update_Invoiced(Convert.ToInt32(dataGridView2.Rows[i_wo].Cells["Work_Order_Id"].Value.ToString()), int_Invoice_Id, int_Current_User_Id );
                    }
                }
            }

            if (int_result > 0)
            {
                lbl_message.Text = "Invoice has been Saved";
                lbl_message.ForeColor = System.Drawing.Color.Blue;

                //allow access to email and pritn after the invioce has been saved
                btn_email.Visible = true;
                btn_Print.Visible = true;
            }
            else
            {
                lbl_message.Text = "Invoice failed to Save";
                lbl_message.ForeColor = System.Drawing.Color.Red;
            }
        }
        private string Create_PDF_Defaults(string str_type)
        { 
            DataSet ds_Get_Info;
            DataRow dr_Get_Info;
            decimal dec_GST = 0;
            string str_msg = "";
            #region ------------- defaults -------------
            ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_System.Get_Info_Like("PF%");


            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                switch (dr_Get_Info["Code"].ToString())
                {
                    case "PF-TPath":
                        FruPak.PF.PrintLayer.Word.TemplatePath = dr_Get_Info["Value"].ToString();
                        break;
                    case "PF-TInv1":
                        FruPak.PF.PrintLayer.Word.TemplateName = dr_Get_Info["Value"].ToString();
                        break;
                    case "PF-SPath":
                        FruPak.PF.PrintLayer.Word.FilePath = dr_Get_Info["Value"].ToString();
                        break;

                }
            }
            ds_Get_Info.Dispose();

            //get GST
            ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_System.Get_Info_Like("GST");
            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                dec_GST = Convert.ToDecimal(dr_Get_Info["Value"].ToString());
            }
            ds_Get_Info.Dispose();

            #endregion
            #region ------------- Outlook details -------------
            ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Trader.Get_Info(Convert.ToInt32(cmb_Trader.SelectedValue.ToString()));
            for (int i = 0; i < ds_Get_Info.Tables[0].Rows.Count; i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                str_msg = str_msg + FruPak.PF.Common.Code.Outlook.Check_Outlook(Convert.ToInt32(dr_Get_Info["Customer_Id"].ToString()), str_msg, str_type);
            }
            ds_Get_Info.Dispose();            
            str_save_filename = FruPak.PF.Common.Code.General.delivery_name;   
            #endregion

            if (str_msg.Length <= 0)
            {
                Create_PDF(str_type, dec_GST, FruPak.PF.Common.Code.General.delivery_name);
            }
            return str_msg;
        }
        private void Create_PDF(string str_type, decimal dec_GST, string str_delivery_name)
        {

            string Data = "";
            Data = Data + Convert.ToString(int_Invoice_Id);
            Data = Data + ":"+ cmb_Trader.SelectedValue.ToString();
            Data = Data + ":" + DateTime.Now.ToString("yyyy/MM/dd");
            Data = Data + ":" + txt_order_num.Text;
            Data = Data + ":" + str_delivery_name;
            Data = Data + ":" + str_save_filename;
            Data = Data + ":" + Convert.ToString(dec_GST);
            Data = Data + ":" + str_type;
            int int_result = 0;
            int_result = FruPak.PF.PrintLayer.Invoice_Work_Orders.Print(Data, true);

        }
        private void btn_email_Click(object sender, EventArgs e)
        {
            string str_msg = "";
            int int_result = 9;
            Cursor = Cursors.WaitCursor;
            //reset email data
            FruPak.PF.Common.Code.Send_Email.Email_Subject = null;
            FruPak.PF.Common.Code.Send_Email.Email_Message = null;
            str_msg = str_msg + Create_PDF_Defaults("Email");
            FruPak.PF.Common.Code.SendEmail.attachment = new List<string>();
            FruPak.PF.Common.Code.SendEmail.attachment.Add(FruPak.PF.PrintLayer.Word.FilePath + "\\" + FruPak.PF.PrintLayer.Word.FileName);
            int_result = FruPak.PF.Common.Code.Outlook.Email(str_msg, true);
           
            Cursor = Cursors.Default;

            if (str_msg.Length > 0)
            {
                MessageBox.Show(str_msg, "Process Factory - Invoice (Work Order)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Email to Office            
            Cursor = Cursors.WaitCursor;
            str_msg = str_msg + Create_PDF_Defaults("Email");
            int_result = int_result + FruPak.PF.Common.Code.Outlook.Email_Office("FP-Off1%", str_save_filename, str_msg);
            Cursor = Cursors.Default;

            if (int_result == 0)
            {
                lbl_message.Text = lbl_message.Text + "Invoice has been emailed to Customer and the Office.";
                FruPak.PF.Data.AccessLayer.PF_A_Invoice.Update_Greentree_Date(int_Invoice_Id, int_Current_User_Id);
                FruPak.PF.Common.Code.SendEmail.BCC_Email_Address.Clear();
                FruPak.PF.Common.Code.SendEmail.attachment = null;
            }
        }
        private void btn_Print_Click(object sender, EventArgs e)
        {
            string str_msg = "";
            Cursor = Cursors.WaitCursor;
            str_msg = str_msg + Create_PDF_Defaults("Print");
            Cursor = Cursors.Default;

            if (str_msg.Length > 0)
            {
                MessageBox.Show(str_msg, "Process Factory - Invoice Search", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Email to Office
            int int_result = 0;          
            Cursor = Cursors.WaitCursor;
            str_msg = str_msg + Create_PDF_Defaults("Email");
            int_result = int_result + FruPak.PF.Common.Code.Outlook.Email_Office("FP-Off1%", str_save_filename, str_msg);
            Cursor = Cursors.Default;

            if (int_result == 0)
            {
                lbl_message.Text = lbl_message.Text + "Invoice has been emailed to Office and Printed for Customer.";
                FruPak.PF.Data.AccessLayer.PF_A_Invoice.Update_Greentree_Date(int_Invoice_Id, int_Current_User_Id);
            }
        }
        private static string str_save_filename = "";
        private void btn_reset_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void Reset()
        {
            cmb_Trader.Text = null;
            cmb_Rates.Text = null;            
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            txt_comments.ResetText();
            btn_email.Visible = false;
            btn_Print.Visible = false;
            cmb_Rates.Visible = false;
            btn_Add_Rates.Visible = false;
            lbl_message.Text = "";
            nud_freight.Visible = false;
            lbl_freight_charge.Visible = false;
        }
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmb_Rates_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DataSet ds = FruPak.PF.Data.AccessLayer.PF_A_Rates.Get_Info(Convert.ToInt32(cmb_Rates.SelectedValue.ToString()));
            DataRow dr;
            for (int i =0 ; i < ds.Tables[0].Rows.Count; i ++)
            {
                dr = ds.Tables[0].Rows[i];

                if (dr["Code"].ToString().ToUpper().Substring(0, dr["Code"].ToString().IndexOf("-")) == "FREIGHT")
                {
                    nud_freight.Visible = true;
                    lbl_freight_charge.Visible = true;
                }
                else
                {
                    nud_freight.Visible = false;
                    lbl_freight_charge.Visible = false;
                }
            }
            ds.Dispose();
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
        private void Invoicing_WO_KeyDown(object sender, KeyEventArgs e)
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
