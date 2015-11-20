using NLog;
using PresentationControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace PF.Accounts
{
    public partial class Payment_Received : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static int int_Current_User_Id = 0;
        private static bool bol_write_access;

        public Payment_Received(int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();
            int_Current_User_Id = int_C_User_id;
            //restrict access
            bol_write_access = bol_w_a;
            btn_Add.Enabled = bol_w_a;

            //check if testing or not
            //if (PF.Global.Global.bol_Testing == true)
            //{
            //    this.Text = "FP Process Factory - " + this.Text + " - Test Environment";
            //}
            //else
            //{
            //    this.Text = "FP Process Factory";
            //}

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

        private static decimal dec_GST = 0;
        private DataTable dt = new DataTable();

        private void Payment_Received_Load(object sender, EventArgs e)
        {
            AddColumnsProgrammatically();
            populate_datagridview(null);

            //get gst
            dec_GST = PF.Common.Code.General.Get_GST();

            DataSet ds_Get_Info = PF.Data.AccessLayer.CM_System_Email.Get_Info("FP-Off3");
            DataRow dr_Get_Info;
            for (int i = 0; i < ds_Get_Info.Tables[0].Rows.Count; i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                btn_Email.Text = "Email Office (" + dr_Get_Info["Value"].ToString().Substring(0, dr_Get_Info["Value"].ToString().IndexOf('@')) + ")";
            }
            ds_Get_Info.Dispose();

            DataSet ds = PF.Data.AccessLayer.PF_Customer.Get_Info();

            cmb_test.DataSource = new ListSelectionWrapper<DataRow>(ds.Tables[0].Rows,
                "Combined" // "SomePropertyOrColumnName" will populate the Name on ObjectSelectionWrapper.
                );
            cmb_test.DisplayMemberSingleItem = "Name";
            cmb_test.DisplayMember = "NameConcatenated";
            cmb_test.ValueMember = "Selected";

            dt.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("Month_Id", typeof(int)),
                    new DataColumn("Month", typeof(string)),
                });
            dt.Rows.Add(0, "Select a Month");
            dt.Rows.Add(1, "Jan");
            dt.Rows.Add(2, "Feb");
            dt.Rows.Add(3, "Mar");
            dt.Rows.Add(4, "Apr");
            dt.Rows.Add(5, "May");
            dt.Rows.Add(6, "Jun");
            dt.Rows.Add(7, "Jul");
            dt.Rows.Add(8, "Aug");
            dt.Rows.Add(9, "Sep");
            dt.Rows.Add(10, "Oct");
            dt.Rows.Add(11, "Nov");
            dt.Rows.Add(12, "Dec");

            cmb_Month.DataSource = dt;
            cmb_Month.DisplayMember = "Month";
            cmb_Month.ValueMember = "Month_Id";
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

            col0.HeaderText = "Invoice Number";
            col0.Name = "Invoice_Id";
            col0.ReadOnly = true;

            col1.HeaderText = "Invoice Date";
            col1.Name = "Invoice_Date";
            col1.ReadOnly = true;

            col2.HeaderText = "Customer";
            col2.Name = "Customer";
            col2.ReadOnly = true;

            col3.HeaderText = "Customer ID";
            col3.Name = "Customer_Id";
            col3.ReadOnly = true;
            col3.Visible = false;

            col4.HeaderText = "Sent to Greentree on";
            col4.Name = "Greentree_Date";
            col4.ReadOnly = true;

            col5.HeaderText = "Cost";
            col5.Name = "Cost";
            col5.ReadOnly = true;

            col6.HeaderText = "Payment Received";
            col6.Name = "Payment_Received";
            col6.ReadOnly = false;
            col6.ValueType = typeof(DateTime);

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2, col3, col4, col5, col6 });
        }

        private void populate_datagridview(string where)
        {
            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();
            DataSet ds_Get_Info = null;

            if (where == null)
            {
                ds_Get_Info = PF.Data.AccessLayer.PF_A_Invoice.Get_Info_Payments_Not_Recevied();
            }
            else
            {
                ds_Get_Info = PF.Data.AccessLayer.PF_A_Invoice.Get_Info_Payments_Not_Recevied(where);
            }

            DataRow dr_Get_Info;

            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                int rowcount = dataGridView1.Rows.Count;
                dataGridView1.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_Cell0 = new DataGridViewTextBoxCell();
                DGVC_Cell0.Value = dr_Get_Info["Invoice_Id"].ToString();
                dataGridView1.Rows[rowcount].Cells["Invoice_Id"] = DGVC_Cell0;

                DataGridViewCell DGVC_Cell1 = new DataGridViewTextBoxCell();
                DGVC_Cell1.Value = dr_Get_Info["Invoice_Date"].ToString();
                dataGridView1.Rows[rowcount].Cells["Invoice_Date"] = DGVC_Cell1;

                DataGridViewCell DGVC_Cell2 = new DataGridViewTextBoxCell();
                DGVC_Cell2.Value = dr_Get_Info["Customer"].ToString();
                dataGridView1.Rows[rowcount].Cells["Customer"] = DGVC_Cell2;

                DataGridViewCell DGVC_Cell3 = new DataGridViewTextBoxCell();
                DGVC_Cell3.Value = dr_Get_Info["Customer_Id"].ToString();
                dataGridView1.Rows[rowcount].Cells["Customer_Id"] = DGVC_Cell3;

                DataGridViewCell DGVC_Cell4 = new DataGridViewTextBoxCell();
                DGVC_Cell4.Value = dr_Get_Info["Greentree_Date"].ToString();
                dataGridView1.Rows[rowcount].Cells["Greentree_Date"] = DGVC_Cell4;

                DataGridViewCell DGVC_Cell5 = new DataGridViewTextBoxCell();
                decimal dec_cost = 0;
                try
                {
                    dec_cost = Convert.ToDecimal(dr_Get_Info["Cost"].ToString());
                }
                catch
                {
                    dec_cost = 0;
                }
                decimal dec_freight = 0;
                try
                {
                    dec_freight = Convert.ToDecimal(dr_Get_Info["Freight"].ToString());
                }
                catch
                {
                    dec_freight = 0;
                }
                DGVC_Cell5.Value = Math.Round((dec_cost + dec_freight) + (((dec_cost + dec_freight) * dec_GST) / 100), 2);
                dataGridView1.Rows[rowcount].Cells["Cost"] = DGVC_Cell5;
            }

            Column_resize();
            ds_Get_Info.Dispose();
        }

        private void Column_resize()
        {
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells["Payment_Received"].Value = null;
            }

            for (int i = 0; i < cmb_test.CheckBoxItems.Count; i++)
            {
                try
                {
                    cmb_test.CheckBoxItems[i].Checked = false;
                }
                catch (Exception ex)
                {
                    logger.Log(LogLevel.Debug, ex.Message);
                }
            }
            cmb_Month.SelectedValue = 0;
            populate_datagridview(null);
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            int int_result = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells["Payment_Received"].Value != null)
                {
                    DateTime dt = new DateTime();

                    dt = Convert.ToDateTime(dataGridView1.Rows[i].Cells["Payment_Received"].Value.ToString());

                    int_result = int_result + PF.Data.AccessLayer.PF_A_Invoice.Update_Payment_Received(Convert.ToInt32(dataGridView1.Rows[i].Cells["Invoice_Id"].Value),
                                                                                                                dt.Year.ToString() + "/" + dt.Month.ToString() + "/" + dt.Day.ToString(),
                                                                                                                int_Current_User_Id);
                    DataSet ds = PF.Data.AccessLayer.PF_Orders.Get_Order_id_for_Invoice(Convert.ToInt32(dataGridView1.Rows[i].Cells["Invoice_Id"].Value));
                    DataRow dr;
                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        dr = ds.Tables[0].Rows[j];
                        int_result = PF.Data.AccessLayer.PF_Pallet.Update_Order_Id(Convert.ToInt32(dr["Pallet_Id"].ToString()), Convert.ToInt32(dr["Order_id"].ToString()), int_Current_User_Id);
                    }
                    ds.Dispose();
                }
            }

            if (int_result > 0)
            {
                lbl_message.Text = "Payment received Date has been updated";
                lbl_message.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                lbl_message.Text = "Payment received Date was not updated";
                lbl_message.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            string str_msg = "Invalid Date. Please enter a valid date in the format dd/mm/yyyy or yyyy/mm/dd.";
            MessageBox.Show(str_msg, "Process Factory - Accounting (Payment Received)", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void customer1_CustomerChanged(object sender, EventArgs e)
        {
            populate_datagridview(null);
        }

        private void btn_Email_Click(object sender, EventArgs e)
        {
            int int_result = 9;
            PF.Data.Excel.Excel.FilePath = PF.Common.Code.General.Get_Single_System_Code("PF-TPPath");

            string str_save_filename = "Outstanding Invoices as at " + DateTime.Now.ToString("yyyyMMdd");
            PF.Data.Excel.Excel.FileName = str_save_filename;

            PF.Data.Excel.Excel.startExcel();
            PF.Data.Excel.Excel.Add_Data_Text("A1", "Customer");
            PF.Data.Excel.Excel.Font_Size("A1", 12);
            PF.Data.Excel.Excel.Font_Bold("A1", true);

            PF.Data.Excel.Excel.Add_Data_Text("B1", "Invoice Num");
            PF.Data.Excel.Excel.Font_Size("B1", 12);
            PF.Data.Excel.Excel.Font_Bold("B1", true);

            PF.Data.Excel.Excel.Add_Data_Text("C1", "Invoice Date");
            PF.Data.Excel.Excel.Font_Size("C1", 12);
            PF.Data.Excel.Excel.Font_Bold("C1", true);

            PF.Data.Excel.Excel.Add_Data_Text("D1", "Cost");
            PF.Data.Excel.Excel.Font_Size("D1", 12);
            PF.Data.Excel.Excel.Font_Bold("D1", true);

            int ip = 2;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells["Payment_Received"].Value == null)
                {
                    ip++; // increase the positional counter
                    PF.Data.Excel.Excel.Add_Data_Text("A" + Convert.ToString(ip), dataGridView1.Rows[i].Cells["Customer"].Value.ToString());
                    PF.Data.Excel.Excel.Add_Data_Int("B" + Convert.ToString(ip), Convert.ToInt32(dataGridView1.Rows[i].Cells["Invoice_Id"].Value.ToString()));
                    PF.Data.Excel.Excel.Add_Data_Text("C" + Convert.ToString(ip), dataGridView1.Rows[i].Cells["Invoice_Date"].Value.ToString());
                    PF.Data.Excel.Excel.Add_Data_Dec("D" + Convert.ToString(ip), Convert.ToDecimal(dataGridView1.Rows[i].Cells["Cost"].Value.ToString()), 3);
                }
            }

            //set column width to autofit
            PF.Data.Excel.Excel.Set_Column_Width(1);
            PF.Data.Excel.Excel.Set_Column_Width(2);
            PF.Data.Excel.Excel.Set_Column_Width(3);
            PF.Data.Excel.Excel.Set_Column_Width(4);

            PF.Data.Excel.Excel.SaveAS();
            PF.Data.Excel.Excel.CloseExcel();

            PF.Common.Code.SendEmail.attachment = new List<string>();
            PF.Common.Code.SendEmail.attachment.Add(PF.Data.Excel.Excel.FilePath + "\\" + PF.Data.Excel.Excel.FileName + PF.Data.Excel.Excel.fileExt);
            int_result = PF.Common.Code.Outlook.Email_Office("FP-Off3%", str_save_filename, "");

            if (int_result >= 0)
            {
                lbl_message.Text = lbl_message.Text + "Invoice has been emailed to Customer and to the Office.";
                lbl_message.ForeColor = System.Drawing.Color.Blue;

                file_List.Add(PF.Data.Excel.Excel.FilePath + "\\" + PF.Data.Excel.Excel.FileName + PF.Data.Excel.Excel.fileExt);
            }
        }

        private List<string> file_List = new List<string>();

        private void btn_Statement_Click(object sender, EventArgs e)
        {
            string str_msg = "";
            DialogResult DLR_Message = new DialogResult();

            if (str_selected_Ids.Length == 0)
            {
                str_msg = str_msg + "Invalid Customer Selection. Please select Customer(s) from the Drop down List." + Environment.NewLine;
            }
            if (Convert.ToInt32(cmb_Month.SelectedValue.ToString()) == 0)
            {
                str_msg = str_msg + "Invalid Month Selection. Please select a valid Month from the Drop down list." + Environment.NewLine;
            }

            if (str_msg.Length > 0)
            {
                DLR_Message = MessageBox.Show(str_msg, "Process Factory - Accounting(Payment Received(Statements))", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (DLR_Message != System.Windows.Forms.DialogResult.OK)
            {
                PF.Data.Excel.Excel.FilePath = PF.Common.Code.General.Get_Path("PF-TPPath");

                PF.Data.Excel.Excel.FileName = "Statement";
                PF.Data.Excel.Excel.startExcel();
                int int_return = PF.Data.Excel.Excel.Open();
                if (int_return > 0)
                {
                    MessageBox.Show("File: " + PF.Data.Excel.Excel.FilePath + "\\" + PF.Data.Excel.Excel.FileName + PF.Data.Excel.Excel.fileExt + " Can not be found",
                        "Process Factory - Payment Recevied (Statements)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    #region ------------- Create Spreadsheet -------------

                    PF.Data.Excel.Excel.Add_Data_Text("B10", "Invoice Num");
                    PF.Data.Excel.Excel.Font_Size("B10", 12);
                    PF.Data.Excel.Excel.Font_Bold("B10", true);

                    PF.Data.Excel.Excel.Add_Data_Text("C10", "Invoice Date");
                    PF.Data.Excel.Excel.Font_Size("C10", 12);
                    PF.Data.Excel.Excel.Font_Bold("C10", true);

                    PF.Data.Excel.Excel.Add_Data_Text("D10", "Payment Received");
                    PF.Data.Excel.Excel.Font_Size("D10", 12);
                    PF.Data.Excel.Excel.Font_Bold("D10", true);

                    PF.Data.Excel.Excel.Add_Data_Text("E10", "Cost");
                    PF.Data.Excel.Excel.Font_Size("E10", 12);
                    PF.Data.Excel.Excel.Font_Bold("E10", true);

                    DataSet ds_statement = PF.Data.AccessLayer.PF_A_Invoice.Get_Statement(str_selected_Ids, Convert.ToInt32(cmb_Month.SelectedValue.ToString()));
                    DataRow dr_statement;
                    int ip = 12;
                    string str_current_name = "";
                    decimal dec_grand_Total = 0;
                    for (int i = 0; i < ds_statement.Tables[0].Rows.Count; i++)
                    {
                        bool bol_cost_total = false;
                        dr_statement = ds_statement.Tables[0].Rows[i];

                        ip++; // increase the positional counter

                        if (str_current_name != dr_statement["Name"].ToString())
                        {
                            PF.Data.Excel.Excel.Add_Data_Text("A" + Convert.ToString(ip), dr_statement["Name"].ToString());
                            str_current_name = dr_statement["Name"].ToString();
                        }
                        try
                        {
                            PF.Data.Excel.Excel.Add_Data_Int("B" + Convert.ToString(ip), Convert.ToInt32(dr_statement["Invoice_Id"].ToString()));
                            bol_cost_total = false;
                        }
                        catch
                        {
                            bol_cost_total = true;
                        }
                        PF.Data.Excel.Excel.Add_Data_Text("C" + Convert.ToString(ip), dr_statement["Invoice_Date"].ToString());
                        PF.Data.Excel.Excel.Add_Data_Text("D" + Convert.ToString(ip), dr_statement["Payment_Received"].ToString());

                        decimal dec_cost = 0;
                        try
                        {
                            dec_cost = Convert.ToDecimal(dr_statement["Cost"].ToString());
                        }
                        catch
                        {
                            dec_cost = 0;
                        }
                        decimal dec_freight = 0;
                        try
                        {
                            dec_freight = Convert.ToDecimal(dr_statement["Freight"].ToString());
                        }
                        catch
                        {
                            dec_freight = 0;
                        }

                        if (bol_cost_total == true) //print total
                        {
                            dec_grand_Total = dec_grand_Total + Math.Round((dec_cost + dec_freight) + (((dec_cost + dec_freight) * dec_GST) / 100), 2); // calculate rand total
                            PF.Data.Excel.Excel.Add_Data_Dec("F" + Convert.ToString(ip), Math.Round((dec_cost + dec_freight) + (((dec_cost + dec_freight) * dec_GST) / 100), 2), 3);
                            PF.Data.Excel.Excel.Font_Bold("F" + Convert.ToString(ip), true);
                        }
                        else
                        {
                            PF.Data.Excel.Excel.Add_Data_Dec("E" + Convert.ToString(ip), Math.Round((dec_cost + dec_freight) + (((dec_cost + dec_freight) * dec_GST) / 100), 2), 3);
                        }
                    }

                    PF.Data.Excel.Excel.Add_Data_Text("A" + Convert.ToString(ip + 2), "Grand Total");
                    PF.Data.Excel.Excel.Cell_Border("F" + Convert.ToString(ip + 2), "Top", "Continuous", "Red");
                    PF.Data.Excel.Excel.Cell_Border("F" + Convert.ToString(ip + 2), "Bottom", "Double", "Red");
                    PF.Data.Excel.Excel.Add_Data_Dec("F" + Convert.ToString(ip + 2), dec_grand_Total, 3);
                    PF.Data.Excel.Excel.Font_Bold("F" + Convert.ToString(ip + 2), true);

                    ds_statement.Dispose();
                    //set column width to autofit
                    PF.Data.Excel.Excel.Set_Column_Width(1);
                    PF.Data.Excel.Excel.Set_Column_Width(2);
                    PF.Data.Excel.Excel.Set_Column_Width(3);
                    PF.Data.Excel.Excel.Set_Column_Width(4);
                    PF.Data.Excel.Excel.Set_Column_Width(5);

                    PF.Data.Excel.Excel.FilePath = PF.Common.Code.General.Get_Path("PF-TPPath");

                    string str_save_filename = "Statement as at " + DateTime.Now.ToString("yyyyMMdd");
                    PF.Data.Excel.Excel.FileName = str_save_filename;

                    PF.Data.Excel.Excel.SaveAS();
                    PF.Data.Excel.Excel.SaveASPDF();
                    PF.Data.Excel.Excel.CloseExcel();

                    #endregion ------------- Create Spreadsheet -------------

                    #region ------------- Email Customer -------------

                    PF.Common.Code.SendEmail.attachment = new List<string>();
                    PF.Common.Code.SendEmail.attachment.Add(PF.Data.Excel.Excel.FilePath + "\\" + PF.Data.Excel.Excel.FileName + ".PDF");

                    Form frm_Select_Cust = new Statement_Email();
                    frm_Select_Cust.ShowDialog();

                    str_msg = str_msg + PF.Common.Code.Outlook.Check_Outlook(Statement_Email.Customer_Id, "", "Email");
                    PF.Common.Code.Send_Email.Email_Subject = "Monthly Statement";

                    string str_month = "";
                    foreach (DataRow dr1 in dt.Rows)
                    {
                        if (Convert.ToInt32(dr1["Month_Id"].ToString()) == Convert.ToInt32(cmb_Month.SelectedValue.ToString()))
                        {
                            str_month = dr1["Month"].ToString();
                        }
                    }

                    PF.Common.Code.Send_Email.Email_Message = "Please find attached you Monthly Statement for the month of " + str_month;
                    PF.Common.Code.Outlook.Email(str_msg, true);

                    #endregion ------------- Email Customer -------------
                }
                PF.Data.Excel.Excel.CloseExcel();
            }
        }

        private static string str_selected_Ids = "";

        private void cmb_test_CheckBoxCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string where = "(";
                foreach (var selected in cmb_test.CheckBoxItems)
                {
                    if (selected.Checked == true)
                    {
                        where = where + selected.Text.Substring(0, selected.Text.IndexOf('-') - 1) + ",";
                    }
                }

                str_selected_Ids = where.Substring(0, where.LastIndexOf(',')) + ")";
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message);
            }
            populate_datagridview(str_selected_Ids);
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
        private void Payment_Received_KeyDown(object sender, KeyEventArgs e)
        {
            // Note that the KeyPreview property must be set to true for this to work
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        #endregion Methods to log UI events to the CSV file. BN 29/01/2015
    }

    public class DataTableWrapper : ListSelectionWrapper<DataRow>
    {
        public DataTableWrapper(DataTable dataTable, string usePropertyAsDisplayName) : base(dataTable.Rows, false, usePropertyAsDisplayName)
        {
        }
    }
}