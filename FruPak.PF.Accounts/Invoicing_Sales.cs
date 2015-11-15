using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using FruPak.PF.CustomSettings;

namespace FruPak.PF.Accounts
{
    public partial class Invoicing_Sales : Form
    {
        private static PhantomCustomSettings Settings;

        #region Delegates to pass messages - place just after public partial class

        // add a delegate
        public delegate void HistoryUpdateHandler(object sender, HistoryUpdateEventArgs e);

        // add an event of the delegate type
        public event HistoryUpdateHandler HistoryUpdated;

        #endregion Delegates to pass messages - place just after public partial class

        #region Variable Declarations

        private static bool bol_write_access;
        private static DialogResult DLR_Message;
        private static int int_Current_User_Id = 0;
        private static int int_Invoice_Id = 0;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static string str_delivery_name = "";
        private static string str_save_filename = "";
        private List<int> int_Where = new List<int>();
        private string str_WHERE = "";
        private List<string> view_list = new List<string>();
        #endregion Variable Declarations

        private List<string> lst_filenames = new List<string>();

        public Invoicing_Sales(int int_C_User_id, bool bol_w_a)
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

            #endregion Log any interesting events from the UI to the CSV log file
        }

        public void populate_combobox()
        {
            DataSet ds_Get_Info;

            ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_A_Rates.Get_Info();
            cmb_Rates.DataSource = ds_Get_Info.Tables[0];
            cmb_Rates.DisplayMember = "Description";
            cmb_Rates.ValueMember = "Rates_Id";
            cmb_Rates.Text = null;
            ds_Get_Info.Dispose();
        }

        /// <summary>
        /// Determines whether [is filein use] [the specified file]. // BN 17-03-2014
        /// Followup: 12/05/2015 Didn't work. Will try a much more heavy-duty method.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        protected virtual bool IsFileinUse(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            return false;
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
            var col7 = new DataGridViewTextBoxColumn();
            var col8 = new DataGridViewTextBoxColumn();

            col0.HeaderText = "Description";
            col0.Name = "Description";
            col0.ReadOnly = true;

            col1.HeaderText = "Quantity";
            col1.Name = "Quantity";
            col1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col1.ReadOnly = true;

            col2.HeaderText = "Price";
            col2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            col2.ReadOnly = true;

            col3.HeaderText = "Total Amount";
            col3.Name = "Total_Amount";
            col3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            col4.HeaderText = "Discount";
            col4.Name = "Discount";
            col4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            col4.ReadOnly = true;

            col5.HeaderText = "Material_Id";
            col5.Name = "Material_Id";
            col5.ReadOnly = true;
            col5.Visible = false;

            col6.HeaderText = "Customer Order Number";
            col6.Name = "Customer_order";
            col6.ReadOnly = true;

            col7.HeaderText = "Frupak Order Number";
            col7.Name = "Frupak_order";
            col7.ReadOnly = true;

            col8.HeaderText = "Rates_Id";
            col8.Name = "Rates_Id";
            col8.ReadOnly = true;
            col8.Visible = true;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2, col3, col4, col5, col6, col7, col8 });

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

            col0.HeaderText = "Order";
            col0.Name = "Order_Id";
            col0.ReadOnly = true;

            col1.HeaderText = "Load Date";
            col1.Name = "Load_Date";
            col1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col1.ReadOnly = true;

            col2.HeaderText = "Customer_Order";
            col2.Name = "Customer_Order";
            col2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            col2.ReadOnly = true;

            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2 });
        }

        /// <summary>
        /// This processes the informat selected from the checklistbox, and adds the base rates to the Datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Add_Click(object sender, EventArgs e)
        {
            FireHistoryMessage("Add Orders to Invoice Clicked ");

            int int_zero_selected = 0;
            // string str_get_wo = "(";
            for (int i_wo = 0; i_wo < dataGridView2.Rows.Count; i_wo++)
            {
                DataGridViewCheckBoxCell checkbox = (DataGridViewCheckBoxCell)dataGridView2.Rows[i_wo].Cells["Select"];
                bool isChecked = (bool)checkbox.EditedFormattedValue;
                if ((bool)checkbox.EditedFormattedValue == true)
                {
                    string str_get_wo = "(";

                    str_get_wo = str_get_wo + dataGridView2.Rows[i_wo].Cells["Order_Id"].Value.ToString() + ",";

                    str_get_wo = str_get_wo + ")";

                    str_WHERE = str_get_wo.Replace(",)", ")");

                    txt_order_num.Text = str_WHERE.Replace(")", "").Replace("(", "");
                    int_Where.Add(Convert.ToInt32(dataGridView2.Rows[i_wo].Cells["Order_Id"].Value.ToString()));
                    populate_datagridview(str_WHERE);
                }
                else
                {
                    int_zero_selected++;
                }
            }

            if (int_zero_selected == dataGridView2.Rows.Count)
            {
                MessageBox.Show("No Sales have been selected for this Invoice. Please select Sales from the list provide.", "Process Factory - Invoice Sales", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Extra Charges
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Add_Rates_Click(object sender, EventArgs e)
        {
            if (cmb_Rates.SelectedValue != null) // BN Fix 29/01/2015
            {
                FireHistoryMessage("Add Extra Charges Clicked ");

                DataSet ds = FruPak.PF.Data.AccessLayer.PF_A_Rates.Get_Info(Convert.ToInt32(cmb_Rates.SelectedValue.ToString()));
                DataRow dr;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dr = ds.Tables[0].Rows[i];

                    var vv = dr["Code"].ToString().ToUpper().Substring(0, dr["Code"].ToString().IndexOf("-"));
                    logger.Log(LogLevel.Warn, "Add Rates: " + vv);

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
            else
            {
                // BN 29/01/2015
                logger.Log(LogLevel.Info, DecorateString("btn_Add_Rates_Click", "Avoided crash: cmb_Rates.SelectedValue was null.", "Click"));
            }

            // Reset the values back to zero - BN 05/11/2015
            nud_freight.Value = 0;
            numericUpDownQuantity.Value = 0;

            // Re-enable the numericUpDowns
            nud_freight.Enabled = true;
            numericUpDownQuantity.Enabled = true;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            FireHistoryMessage("Close Clicked ");
            FruPak.PF.Common.Code.KillAllWordInstances.KillAllWordProcesses();

            this.Close();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            FireHistoryMessage("Delete Clicked ");

            String Data = "";
            Data = Data + Convert.ToString(int_Invoice_Id);
            Data = Data + ":" + Convert.ToString(int_Current_User_Id);
            int int_result = FruPak.PF.Common.Code.General.Delete_Record("PF_A_Invoice", Data, Convert.ToString(int_Invoice_Id));

            if (int_result > 0)
            {
                lbl_message.Text = "Invoice: " + Convert.ToString(int_Invoice_Id) + " has been deleted";
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                Reset();
            }
            else
            {
                lbl_message.Text = "Invoice: " + Convert.ToString(int_Invoice_Id) + " has NOT been deleted";
                lbl_message.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void btn_email_Click(object sender, EventArgs e)
        {
            FireHistoryMessage("Email Clicked ");

            string str_msg = "";
            int int_result = 9;
            Cursor = Cursors.WaitCursor;
            str_msg = str_msg + Create_PDF_Defaults("Email"); //Creates Invoice, which is saved as a PDF.
            FruPak.PF.Common.Code.SendEmail.attachment = new List<string>();
            FruPak.PF.Common.Code.SendEmail.attachment.Add(FruPak.PF.PrintLayer.Word.FilePath + "\\" + FruPak.PF.PrintLayer.Word.FileName);

            #region ------------- Certificate of Analysis -------------

            if (ckb_COA.Checked == true)
            {
                Cursor = Cursors.WaitCursor;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string Data = "";

                    // Original code
                    //Data = Data + dataGridView1.CurrentRow.Cells["Frupak_order"].Value.ToString();

                    // Modified code 18-03-2015 BN
                    Data = Data + dataGridView1.Rows[i].Cells["Frupak_order"].Value.ToString();

                    Data = Data + ":" + str_delivery_name;

                    if (FruPak.PF.Data.Outlook.Contacts.Contact_Business_Address != null)
                    {
                        Data = Data + ":" + FruPak.PF.Data.Outlook.Contacts.Contact_Business_Address.ToString();
                    }
                    else
                    {
                        Data = Data + ":" + "No Contact Business Address found.";
                    }
                    Data = Data + ":Email";
                    Cursor = Cursors.WaitCursor;

                    // Original code
                    //FruPak.PF.PrintLayer.Word.FileName = "COA" + dataGridView1.CurrentRow.Cells["Frupak_order"].Value.ToString();

                    // Modified code 18-03-2015 BN
                    FruPak.PF.PrintLayer.Word.FileName = "COA" + dataGridView1.Rows[i].Cells["Frupak_order"].Value.ToString();

                    FruPak.PF.PrintLayer.Certificate_Of_Analysis.Print(Data, true);

                    // Note: you can't use a foreach statement here as it modifies the collection - BN
                    for (int ia = 0; ia < FruPak.PF.Common.Code.SendEmail.attachment.Count; ia++)
                    {
                        // For some strange reason this: "C:\\FruPak\\Client\\Printing\\Saved\\" is being added to the collection
                        // So remove it - Bn 9/11/2015
                        if (FruPak.PF.Common.Code.SendEmail.attachment.Contains("C:\\FruPak\\Client\\Printing\\Saved\\"))
                        {
                            FruPak.PF.Common.Code.SendEmail.attachment.Remove("C:\\FruPak\\Client\\Printing\\Saved\\");
                        }

                        // Since I got the extra charges working, we're now getting duplicate attachments
                        // So make sure the attachment isn't already in the list - BN 9/11/2015
                            if (FruPak.PF.Common.Code.SendEmail.attachment.Contains(FruPak.PF.PrintLayer.Word.FilePath + "\\" + FruPak.PF.PrintLayer.Word.FileName + ".PDF") == false)
                        {
                            FruPak.PF.Common.Code.SendEmail.attachment.Add(FruPak.PF.PrintLayer.Word.FilePath + "\\" + FruPak.PF.PrintLayer.Word.FileName + ".PDF");
                        }
                    }
                }
            
                Cursor = Cursors.Default;
            }

            #endregion ------------- Certificate of Analysis -------------

            int_result = FruPak.PF.Common.Code.Outlook.Email(str_msg, true);

            //email office
            str_msg = str_msg + Create_PDF_Defaults("Email"); //Creates Invoice, which is saved as a PDF.
            try
            {
                FruPak.PF.Common.Code.SendEmail.attachment = new List<string>();
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message);
            }

            try
            {
                FruPak.PF.Common.Code.SendEmail.attachment.Add(FruPak.PF.PrintLayer.Word.FilePath + "\\" + FruPak.PF.PrintLayer.Word.FileName);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message);
            }

            int_result = int_result + FruPak.PF.Common.Code.Outlook.Email_Office("FP-Off1%", str_save_filename, str_msg);
            Cursor = Cursors.Default;
            if (int_result == 0)
            {
                lbl_message.Text = lbl_message.Text + "Invoice has been emailed to Customer and to the Office.";
                FruPak.PF.Data.AccessLayer.PF_A_Invoice.Update_Greentree_Date(int_Invoice_Id, int_Current_User_Id);
                Reset();
            }
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            // This is the most suspect method of them all, regarding Glenys.
            // I wonder if this will work? No?
            // Apparently there's a 15-minute delay, so I'll probably go cook breakfast now
            // 4 hour sleep test

            FireHistoryMessage("Print Clicked ");

            // BN This is a bit of a hack to fix an unintended side effect of a little fix I made earlier on the 12th. 18-3-2015
            //FruPak.PF.PrintLayer.Certificate_Of_Analysis.ResetIpValue(); // Ip is just a counter

            string str_msg = "";
            // Print for Customer
            Cursor = Cursors.WaitCursor;
            str_msg = str_msg + Create_PDF_Defaults("Print");
            Cursor = Cursors.Default;

            // print COA

            #region ------------- Certificate of Analysis -------------

            // Notes BN --------------
            // dataGridView1 is the orders which are to be invoiced: datagrid on the left
            // dataGridView2 is the potential orders can be added to the invoice: datagrid on the right
            // Notes BN --------------

            if (ckb_COA.Checked == true)
            {
                Cursor = Cursors.WaitCursor;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string Data = "";
                    Data = Data + dataGridView1.Rows[i].Cells["Frupak_order"].Value.ToString();
                    Data = Data + ":" + str_delivery_name;
                    Data = Data + ":" + FruPak.PF.Data.Outlook.Contacts.Contact_Business_Address.ToString();
                    Data = Data + ":Print";
                    Cursor = Cursors.WaitCursor;
                    //set printer
                    FruPak.PF.PrintLayer.Word.Printer = FruPak.PF.Common.Code.General.Get_Printer("A4");
                    FruPak.PF.PrintLayer.Word.FileName = "COA" + dataGridView1.Rows[i].Cells["Frupak_order"].Value.ToString();
                    FruPak.PF.PrintLayer.Certificate_Of_Analysis.Print(Data, true);

                    string watchName = "COA" + dataGridView1.Rows[i].Cells["Frupak_order"].Value.ToString();
                    Console.WriteLine(watchName);

                    logger.Log(LogLevel.Info, "Adding: " + "COA" + dataGridView1.Rows[i].Cells["Frupak_order"].Value.ToString() + " to the list of files to be printed.");

                    view_list.AddRange(System.IO.Directory.GetFiles(FruPak.PF.PrintLayer.Word.FilePath, "COA" + dataGridView1.Rows[i].Cells["Frupak_order"].Value.ToString() + "*", System.IO.SearchOption.TopDirectoryOnly)
                         .Select(path => System.IO.Path.GetFileNameWithoutExtension(path))
                         .ToArray());
                    Cursor = Cursors.Default;
                }
            }

            #endregion ------------- Certificate of Analysis -------------

            //Email to Office
            int int_result = 0;
            Cursor = Cursors.WaitCursor;
            str_msg = str_msg + Create_PDF_Defaults("EmailO");
            FruPak.PF.Common.Code.SendEmail.attachment = new List<string>();
            FruPak.PF.Common.Code.SendEmail.attachment.Add(FruPak.PF.PrintLayer.Word.FilePath + "\\" + FruPak.PF.PrintLayer.Word.FileName);

            if (str_msg.Length > 0)
            {
                DLR_Message = MessageBox.Show(str_msg, "Process Factory - Invoice Sales(Print).", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (DLR_Message != System.Windows.Forms.DialogResult.OK)
            {
                int_result = int_result + FruPak.PF.Common.Code.Outlook.Email_Office("FP-Off1%", str_save_filename, str_msg);
            }
            if (int_result == 0)
            {
                lbl_message.Text = lbl_message.Text + "Invoice has been emailed to Office and Printed for Customer.";
                FruPak.PF.Data.AccessLayer.PF_A_Invoice.Update_Greentree_Date(int_Invoice_Id, int_Current_User_Id);
                Reset();
            }
            Cursor = Cursors.Default;
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            FireHistoryMessage("Reset Clicked ");

            Reset();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            FireHistoryMessage("Save Clicked ");

            DLR_Message = new DialogResult();
            string str_msg = "";
            int int_result = 0;

            if (dataGridView1.RowCount == 0)
            {
                str_msg = str_msg + "NO Sales have been selected for Invoicing. Please select at least one Sales for this invoice." + Environment.NewLine;
            }

            if (str_msg.Length > 0)
            {
                DLR_Message = MessageBox.Show(str_msg, "Process Factory - Accounting (Invoice)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (DLR_Message != System.Windows.Forms.DialogResult.OK)
            {
                int_Invoice_Id = FruPak.PF.Common.Code.General.int_max_user_id("PF_A_Invoice");

                btn_delete.Text = "Delete Invoice Number: " + Convert.ToString(int_Invoice_Id);
                //Create Invoice Record.

                DateTime dt_invoice = new DateTime();

                dt_invoice = Convert.ToDateTime(dtp_Invoice_Date.Value.ToString());
                int_result = FruPak.PF.Data.AccessLayer.PF_A_Invoice.Insert_Sales(int_Invoice_Id, dt_invoice.Year.ToString() + '-' + dt_invoice.Month.ToString() + "-" + dt_invoice.Day.ToString(), customer1.Customer_Id, txt_comments.Text, txt_order_num.Text, int_Current_User_Id);

                // create detail records for the invoice
                for (int i = 0; i < Convert.ToInt32(dataGridView1.RowCount); i++)
                {
                    if (int_result > 0)
                    {
                        int int_Invoice_Details_id = FruPak.PF.Common.Code.General.int_max_user_id("PF_A_Invoice_Details");
                        int_result = int_result + FruPak.PF.Data.AccessLayer.PF_A_Invoice_Details.Insert_Product(int_Invoice_Details_id, int_Invoice_Id, Convert.ToInt32(dataGridView1.Rows[i].Cells["Material_Id"].Value.ToString()), Math.Round(Convert.ToDecimal(dataGridView1.Rows[i].Cells["Total_Amount"].Value.ToString()), 2), int_Current_User_Id, dataGridView1.Rows[i].Cells["Frupak_order"].Value.ToString(), Convert.ToDecimal(dataGridView1.Rows[i].Cells["Quantity"].Value.ToString()));

                        try
                        {
                            if (dataGridView1.Rows[i].Cells["Rates_Id"].Value != null)
                                {
                                if (Convert.ToDecimal(dataGridView1.Rows[i].Cells["Rates_Id"].Value.ToString()) > 0)
                                {
                                    int_result = int_result + FruPak.PF.Data.AccessLayer.PF_A_Invoice_Details.Update_Rate(int_Invoice_Details_id, Convert.ToInt32(dataGridView1.Rows[i].Cells["Rates_Id"].Value.ToString()), int_Current_User_Id);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.Log(LogLevel.Debug, ex.Message);
                            MessageBox.Show(ex.Message, "Rates_Id");
                        }
                    }
                }

                //update Selected Work Orders with Invoice_Id
                for (int i_wo = 0; i_wo < dataGridView2.Rows.Count; i_wo++)
                {
                    DataGridViewCheckBoxCell checkbox = (DataGridViewCheckBoxCell)dataGridView2.Rows[i_wo].Cells["Select"];
                    bool isChecked = (bool)checkbox.EditedFormattedValue;
                    if ((bool)checkbox.EditedFormattedValue == true)
                    {
                        int_result = int_result + FruPak.PF.Data.AccessLayer.PF_Orders.Update_Invoiced(Convert.ToInt32(dataGridView2.Rows[i_wo].Cells["Order_Id"].Value.ToString()), int_Invoice_Id, int_Current_User_Id);
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
                btn_View.Visible = true;
                ckb_COA.Visible = true;
                btn_delete.Visible = true;
            }
            else
            {
                lbl_message.Text = "Invoice failed to Save";
                lbl_message.ForeColor = System.Drawing.Color.Red;
                btn_delete.Visible = false;
            }
        }

        private void btn_view_Click(object sender, EventArgs e)
        {
            FireHistoryMessage("View Clicked ");

            string str_msg = "";
            DialogResult DLR_Message = new System.Windows.Forms.DialogResult();
            FruPak.PF.PrintLayer.Word.FilePath = FruPak.PF.Common.Code.General.Get_Single_System_Code("PF-TPPath");

            Cursor = Cursors.WaitCursor;
            str_msg = str_msg + Create_PDF_Defaults("View");
            Cursor = Cursors.Default;

            if (str_msg.Length > 0)
            {
                DLR_Message = MessageBox.Show(str_msg, "Process Factory - Invoice Search(View)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (DLR_Message != DialogResult.OK)
            {
                foreach (string view_file in FruPak.PF.PrintLayer.General.view_list)
                {
                    FruPak.PF.Common.Code.General.Report_Viewer(FruPak.PF.PrintLayer.Word.FilePath + "\\" + view_file);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the buttonOpenPdfFileLocation control. Hardcoded for now (BN) 13-03-2015
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void buttonOpenPdfFileLocation_Click(object sender, EventArgs e)
        {
            DataSet ds_Get_Info;
            DataRow dr_Get_Info;

            string savedPath = "";

            ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_System.Get_Info_Like("PF%");

            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                switch (dr_Get_Info["Code"].ToString())
                {
                    case "PF-SPath":
                        savedPath = dr_Get_Info["Value"].ToString();
                        break;
                }
            }
            ds_Get_Info.Dispose();

            // Opens the folder in explorer
            //Process.Start("explorer.exe", @"C:\FruPak\Client\Printing\Saved");
            Process.Start("explorer.exe", savedPath);
        }

        private void buttonSendDebugInfo_Click(object sender, EventArgs e)
        {
            logger.Log(LogLevel.Info, "Calling: SendDebugInfo");
            //FruPak.PF.Common.Code.EmailDebugInfo.SendDebugInfo("TODO: FruPak.PF.Accounts.Invoicing_Sales");
            FruPak.PF.Common.Code.SendDebugEmail sde = new Common.Code.SendDebugEmail();
            sde.Show();
        }

        private void cmb_Rates_SelectionChangeCommitted(object sender, EventArgs e)
        {
            // Today (16/10/2015) Glenys pointed out that only the freight works

            DataSet ds = FruPak.PF.Data.AccessLayer.PF_A_Rates.Get_Info(Convert.ToInt32(cmb_Rates.SelectedValue.ToString()));
            DataRow dr;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
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

        /// <summary>
        /// filters the work order combobox accouting to the selected trader
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmb_Trader_SelectedIndexChanged(object sender, EventArgs e)
        {
            FireHistoryMessage("Customers ComboBox: " + customer1.Customer_Name);

            if (customer1.CFocused == true)
            {
                Cursor.Current = Cursors.WaitCursor;

                dataGridView1.Rows.Clear();
                populate_datagriview2();
                txt_order_num.ResetText();

                Cursor.Current = Cursors.Default;

            }
        }

        private void Column_resize()
        {
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataGridView2.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
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

            #endregion ------------- defaults -------------

            #region ------------- Outlook details -------------

            str_msg = str_msg + FruPak.PF.Common.Code.Outlook.Check_Outlook(customer1.Customer_Id, str_msg, str_type);
            str_delivery_name = FruPak.PF.Common.Code.General.delivery_name;
            str_save_filename = str_delivery_name;

            #endregion ------------- Outlook details -------------

            if (str_msg.Length <= 0)
            {
                string Data = "";

                Data = Data + Convert.ToString(int_Invoice_Id);
                Data = Data + ":" + DateTime.Now.ToString("yyyy/MM/dd");
                string str_order_num = "";

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (str_order_num.Length == 0)
                    {
                        str_order_num = dataGridView1.Rows[i].Cells["Frupak_order"].Value.ToString();
                    }
                    else
                    {
                        str_order_num = str_order_num + ", " + dataGridView1.Rows[i].Cells["Frupak_order"].Value.ToString();
                    }
                }
                Data = Data + ":" + str_order_num;
                Data = Data + ":" + str_delivery_name;
                Data = Data + ":" + str_save_filename;
                Data = Data + ":" + Convert.ToString(dec_GST);
                Data = Data + ":" + str_type;

                int int_result = 0;
                int_result = FruPak.PF.PrintLayer.Invoice_Sales.Print(Data, true);
            }
            return str_msg;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //GetHistoryDataRowFromDataGridView(sender, e);

            DialogResult DLR_Message = new DialogResult();
            int int_result = 0;
            //Delete
            if (e.ColumnIndex == 9)
            {
                string str_msg;

                str_msg = "You are about to remove " + Environment.NewLine;
                str_msg = str_msg + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() + Environment.NewLine;
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

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 && e.RowIndex > -1)
            {
                dataGridView1.Rows[e.RowIndex].Cells[2].Value =
                    Math.Round(
                    Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString()) / Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString()),
                3);
            }
            Column_resize();
        }

        private void Delete_tempFiles()
        {
            try
            {
                if (str_save_filename != "")    // Added 25/9/2015 BN
                {
                    logger.Log(LogLevel.Info,
                        "Adding: " + "*" + str_save_filename + "*" +
                        " to the list of files to be deleted.");

                    lst_filenames.AddRange(
                        System.IO.Directory.GetFiles(
                        FruPak.PF.PrintLayer.Word.FilePath, "*" +
                        str_save_filename + "*",
                        System.IO.SearchOption.TopDirectoryOnly));
                }
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message);
            }
            foreach (string filename in lst_filenames)
            {
                try
                {
                    // Replace this with the code which actively kills any locking process

                    FileInfo file = new FileInfo(filename);

                    List<Process> processList = GetProcessesLockingFile(filename);

                    if (processList.Count > 0)
                    {
                        foreach (Process p in processList)
                        {
                            logger.Log(
                                LogLevel.Info, "Process: " + p.ProcessName +
                                " is holding " + str_save_filename + " open.");

                            try
                            {
                                p.Close();
                                System.IO.File.Delete(filename);
                            }
                            catch (Exception ex)
                            {
                                logger.Log(LogLevel.Info, ex.Message + ": " +
                                    "*" + str_save_filename + "*" +
                                    " - Process.Close() attempt failed.");
                            }
                        }
                    }
                    else
                    {
                        if (IsFileinUse(file) == false)
                        {
                            logger.Log(LogLevel.Info,
                                "File is not in use. Attempting to delete: " +
                                "*" +
                                str_save_filename + "*");
                            System.IO.File.Delete(filename);
                        }
                        else
                        {
                            logger.Log(LogLevel.Info, "Cannot delete: " + "*" +
                                str_save_filename + "*" +
                                " - it is already open in another process.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Log(LogLevel.Debug, ex.Message);
                }
            }
        }

        private void ClearOutTempFolder()
        {
            // Replace Delete_tempFiles with this - 25/9/2015

            // All region comments I've added for clarity (BN 12/08/2015)

            FruPak.PF.PrintLayer.Word.FilePath = FruPak.PF.Common.Code.General.Get_Path("PF-TPPath");
            DirectoryInfo di = new DirectoryInfo(FruPak.PF.PrintLayer.Word.FilePath);
            if (di.Exists)
            {
                foreach (FileInfo fi in di.GetFiles())
                {
                    lst_filenames.Add(fi.FullName);
                }

                foreach (string filename in lst_filenames)
                {
                    // Crashes here if the file is still open
                    try
                    {
                        #region Delete each file in the list

                        Console.WriteLine("Trying to delete: " + filename);
                        logger.Log(LogLevel.Info, "Trying to delete: " + filename);
                        
                        // This is the solution to files being locked by another process. BN 10/11/2015
                        System.GC.Collect();
                        System.GC.WaitForPendingFinalizers();

                        File.Delete(filename);
                        // Might need some sort of delay here
                        System.Threading.Thread.Sleep(250); // Milliseconds
                        if (File.Exists(filename))
                        {
                            Console.WriteLine("Delete failed: " + filename);
                            logger.Log(LogLevel.Warn, "Delete failed: " + filename);
                        }
                        else
                        {
                            Console.WriteLine("Delete success: " + filename);
                            logger.Log(LogLevel.Info, "Delete success: " + filename);
                        }

                        #endregion Delete each file in the list
                    }
                    catch (IOException ioe)
                    {
                        logger.Log(LogLevel.Error, ioe.Message + " - " + filename);

                        //MessageBox.Show(ioe.Message, "IOException", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        #region Kill the WINWORD process

                        // This failed spectacularly, killing the FruPak.ProcessFactory, thereby committing suicide
                        List<Process> procs = Common.Code.FileUtil.WhoIsLocking(filename);

                        if (procs.Count > 0) // System.Diagnostics.Process (WINWORD)
                        {
                            logger.Log(LogLevel.Info, filename + " is locked open by: " + procs[0].ProcessName);

                            if (procs[0].ProcessName == "WINWORD")
                            {
                                Console.WriteLine("Attempting to kill: " + procs[0].ProcessName);
                                logger.Log(LogLevel.Info, "Attempting to kill: " + procs[0].ProcessName);

                                procs[0].Kill();
                                procs[0].WaitForExit();

                                Console.WriteLine("Retrying to delete: " + filename);
                                logger.Log(LogLevel.Info, "Retrying to delete: " + filename);

                                File.Delete(filename);  // Retry the delete - If no exception was thrown, then it deleted ok

                                // Might need some sort of delay here
                                System.Threading.Thread.Sleep(250); // Milliseconds
                                if (File.Exists(filename))
                                {
                                    Console.WriteLine("Delete failed: " + filename);
                                    logger.Log(LogLevel.Warn, "Delete failed: " + filename);
                                }
                                else
                                {
                                    Console.WriteLine("Delete success: " + filename);
                                    logger.Log(LogLevel.Info, "Delete success: " + filename);
                                }
                            }
                        }

                        #endregion Kill the WINWORD process
                    }
                }
            }
            lst_filenames.Clear();
        }


        private void Invoicing_Sales_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Delete_tempFiles(); // Obsolete - Can't deal with locked files
            ClearOutTempFolder();

            logger.Log(LogLevel.Info, "---------------------[ FruPak.PF.Accounts.Invoicing_Sales Form Closing]--- :" + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToLongTimeString());
        }

        /// <summary>
        /// This is super-important to the extra charges - BN
        /// </summary>
        private void Manual_populate_DGV()
        {
            try
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

                    // If STORAGE
                    if (dr_Rates["Code"].ToString().ToUpper().Substring(0, dr_Rates["Code"].ToString().IndexOf('-')) == "STORAGE")
                    {
                        str_total_item = 
                            Common.Code.General.Calculate_Totals(dr_Rates["Code"].ToString().ToUpper(), str_WHERE);
                    }
                    else
                    {
                        // Default to something else
                        str_total_item =
                            Common.Code.General.Calculate_Totals(dr_Rates["Code"].ToString().ToUpper().Substring(0, dr_Rates["Code"].ToString().IndexOf('-')), str_WHERE);
                    }

                    // But then overwrite it if FREIGHT
                    if (dr_Rates["Code"].ToString().ToUpper().Substring(0, dr_Rates["Code"].ToString().IndexOf('-')) == "FREIGHT")
                    {
                        dataGridView1.Rows.Add(dr_Rates["Description"].ToString(),
                                               str_total_item,
                                               nud_freight.Value,
                                               Convert.ToDecimal(nud_freight.Value) * Convert.ToDecimal(str_total_item),
                                               0.00,
                                               dataGridView1.Rows[0].Cells["Material_Id"].Value,
                                               dataGridView1.Rows[0].Cells["Customer_order"].Value,
                                               dataGridView1.Rows[0].Cells["Frupak_order"].Value,
                                               Convert.ToInt32(cmb_Rates.SelectedValue.ToString())
                                               );
                    }
                    else
                    {
                        // And if not that, what state is str_total_item left in? Answer: Not used after this point; switches to Quantity

                        #region This part of the code is entered when the Add Extra charges to Invoice button is clicked (When it's not Freight or Storage)
                        // To peel this apart, I'll expose each one to the console
                        Console.WriteLine("Rates Desc:\t\t" + dr_Rates["Description"].ToString());
                        Console.WriteLine("Total Item:\t\t" + str_total_item);
                        Console.WriteLine("Rates Value:\t" + dr_Rates["value"].ToString());
                        Console.WriteLine("Conversion:\t\t" + Convert.ToDecimal(dr_Rates["value"].ToString()) * Convert.ToDecimal(str_total_item));
                        Console.WriteLine("0.00:\t\t\t" + 0.00);
                        Console.WriteLine("Material_Id:\t" + dataGridView1.Rows[0].Cells["Material_Id"].Value);
                        Console.WriteLine("Customer_order:\t" + dataGridView1.Rows[0].Cells["Customer_order"].Value);
                        Console.WriteLine("Frupak_order:\t" + dataGridView1.Rows[0].Cells["Frupak_order"].Value);
                        Console.WriteLine("Combo:\t\t\t" + cmb_Rates.SelectedValue.ToString());
                        Console.WriteLine("--------------------------------\r\n");
                        #endregion

                        #region This shows the data as 2 decimal places, which might not be what they want (Disabled)
                        // This shows the data as 2 decimal places, which might not be what they want
                        //var result = Convert.ToDecimal(dr_Rates["value"].ToString()) * Convert.ToDecimal(numericUpDownQuantity.Value);
                        //var formatted = string.Format("{0:0.00}", result);

                        //var rawResult = dr_Rates["value"].ToString();
                        //var formattedRates = string.Format("{0:0.00}", result);

                        //dataGridView1.Rows.Add(dr_Rates["Description"].ToString(),                                                      // Description
                        //                       numericUpDownQuantity.Value.ToString(),  //str_total_item,                               // Quantity
                        //                       formattedRates,  //dr_Rates["value"].ToString(),                                                            // Price
                        //                       formatted,       //Convert.ToDecimal(dr_Rates["value"].ToString()) * Convert.ToDecimal(numericUpDownQuantity.Value),     // Total Amount
                        //                       0.00,                                                                                    // Discount
                        //                       dataGridView1.Rows[0].Cells["Material_Id"].Value,                                        //
                        //                       dataGridView1.Rows[0].Cells["Customer_order"].Value,                                     // Customer Order Number
                        //                       dataGridView1.Rows[0].Cells["Frupak_order"].Value,                                       // FruPak Order Number
                        //                       Convert.ToInt32(cmb_Rates.SelectedValue.ToString())                                      // Rates ID (Only used here)
                        //                       );

                        #endregion

                        // I think this is it: if Quantity is not zero, use that, otherwise use str_total_item - BN 5/11/2015 8:22pm. Stopping for the night.
                        decimal flexibleValue = 0;

                        if (numericUpDownQuantity.Value > 0)
                        {
                            flexibleValue = numericUpDownQuantity.Value;
                        }
                        else if (nud_freight.Value > 0)
                        {
                            flexibleValue = nud_freight.Value;
                        }

                        dataGridView1.Rows.Add(dr_Rates["Description"].ToString(),                                                      // Description
                                               flexibleValue, //numericUpDownQuantity.Value.ToString(),  //str_total_item,                               // Quantity
                                               dr_Rates["value"].ToString(),                                                            // Price
                                               Convert.ToDecimal(dr_Rates["value"].ToString()) * Convert.ToDecimal(numericUpDownQuantity.Value),     // Total Amount
                                               0.00,                                                                                    // Discount
                                               dataGridView1.Rows[0].Cells["Material_Id"].Value,                                        //
                                               dataGridView1.Rows[0].Cells["Customer_order"].Value,                                     // Customer Order Number
                                               dataGridView1.Rows[0].Cells["Frupak_order"].Value,                                       // FruPak Order Number
                                               Convert.ToInt32(cmb_Rates.SelectedValue.ToString())                                      // Rates ID (Only used here)
                                               );
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void populate_datagridview(string WHERE)
        {
            DataSet ds_Get_Info = null;
            //dataGridView1.Refresh();
            //dataGridView1.Rows.Clear();

            ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Pallet.Get_Info_for_Order(WHERE);
            DataRow dr_Get_Info;

            DataSet ds_Get_Info2 = null;
            DataRow dr_Get_Info2;
            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                int rowcount = dataGridView1.Rows.Count;
                dataGridView1.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_Cell0 = new DataGridViewTextBoxCell();
                DGVC_Cell0.Value = dr_Get_Info["M_Description"].ToString();
                dataGridView1.Rows[rowcount].Cells[0] = DGVC_Cell0;

                DataGridViewCell DGVC_Cell1 = new DataGridViewTextBoxCell();
                DGVC_Cell1.Value = dr_Get_Info["Quantity"].ToString();
                dataGridView1.Rows[rowcount].Cells[1] = DGVC_Cell1;

                ds_Get_Info2 = FruPak.PF.Data.AccessLayer.PF_A_Customer_Sales_Rates.Get_Info(Convert.ToInt32(dr_Get_Info["Material_Id"].ToString()), customer1.Customer_Id);

                DataGridViewCell DGVC_Cell2 = new DataGridViewTextBoxCell();

                if (Convert.ToInt32(ds_Get_Info2.Tables[0].Rows.Count) > 0)
                {
                    Convert.ToInt32(ds_Get_Info2.Tables[0].Rows.Count);

                    for (int j = 0; j < ds_Get_Info2.Tables[0].Rows.Count; j++)
                    {
                        dr_Get_Info2 = ds_Get_Info2.Tables[0].Rows[j];
                        DGVC_Cell2.Value = dr_Get_Info2["Value"].ToString();
                    }
                    dataGridView1.Rows[rowcount].Cells[2] = DGVC_Cell2;
                    ds_Get_Info2.Dispose();
                }
                else
                {
                    MessageBox.Show("No Rates have been loaded for an Item. Please Load the missing Rates.", "Process Factory - Invoice Sales", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                DataGridViewCell DGVC_Cell3 = new DataGridViewTextBoxCell();

                DGVC_Cell3.Value = Math.Round(Math.Round(Convert.ToDecimal(DGVC_Cell2.Value), 2) * Math.Round(Convert.ToDecimal(DGVC_Cell1.Value), 2), 2);
                dataGridView1.Rows[rowcount].Cells[3] = DGVC_Cell3;

                ds_Get_Info2 = FruPak.PF.Data.AccessLayer.PF_A_Customer_Sales_Discount.Get_Info(Convert.ToInt32(dr_Get_Info["Material_Id"].ToString()), customer1.Customer_Id);

                DataGridViewCell DGVC_Cell4 = new DataGridViewTextBoxCell();
                DGVC_Cell4.Value = "0.00";

                for (int j = 0; j < ds_Get_Info2.Tables[0].Rows.Count; j++)
                {
                    dr_Get_Info2 = ds_Get_Info2.Tables[0].Rows[j];
                    DGVC_Cell4.Value = dr_Get_Info2["Value"].ToString();
                }

                dataGridView1.Rows[rowcount].Cells[4] = DGVC_Cell4;
                ds_Get_Info2.Dispose();

                DataGridViewCell DGVC_Cell5 = new DataGridViewTextBoxCell();
                DGVC_Cell5.Value = dr_Get_Info["Material_Id"].ToString();
                dataGridView1.Rows[rowcount].Cells["Material_Id"] = DGVC_Cell5;

                DataGridViewCell DGVC_Cell6 = new DataGridViewTextBoxCell();
                for (int j = 0; j < dataGridView2.Rows.Count; j++)
                {
                    if (txt_order_num.Text == dataGridView2.Rows[j].Cells["Order_Id"].Value.ToString())
                    {
                        DGVC_Cell6.Value = dataGridView2.Rows[j].Cells["Customer_Order"].Value.ToString();
                    }
                }
                dataGridView1.Rows[rowcount].Cells[6] = DGVC_Cell6;

                DataGridViewCell DGVC_Cell7 = new DataGridViewTextBoxCell();
                DGVC_Cell7.Value = txt_order_num.Text;
                dataGridView1.Rows[rowcount].Cells["Frupak_order"] = DGVC_Cell7;
            }

            Column_resize();
            ds_Get_Info.Dispose();
        }

        private void populate_datagriview2()
        {
            dataGridView2.Refresh();
            dataGridView2.Rows.Clear();
            DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Orders.Get_UnInvoiced(customer1.Customer_Id);
            DataRow dr_Get_Info;

            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                dataGridView2.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_Cell0 = new DataGridViewTextBoxCell();
                DGVC_Cell0.Value = dr_Get_Info["Order_Id"].ToString();
                dataGridView2.Rows[i].Cells["Order_Id"] = DGVC_Cell0;

                DataGridViewCell DGVC_Cell1 = new DataGridViewTextBoxCell();
                DGVC_Cell1.Value = dr_Get_Info["Load_Date"].ToString();
                dataGridView2.Rows[i].Cells["Load_Date"] = DGVC_Cell1;

                DataGridViewCell DGVC_Cell2 = new DataGridViewTextBoxCell();
                DGVC_Cell2.Value = dr_Get_Info["Customer_Order"].ToString();
                dataGridView2.Rows[i].Cells["Customer_Order"] = DGVC_Cell2;
            }
            ds_Get_Info.Dispose();
            Column_resize();
        }

        private void Reset()
        {
            customer1.Customer_Id = 0;
            dataGridView2.Refresh();
            dataGridView2.Rows.Clear();
            dataGridView1.Rows.Clear();
            txt_comments.ResetText();
            btn_email.Visible = false;
            btn_Print.Visible = false;
            btn_View.Visible = false;
            lbl_message.Text = "";

            ClearOutTempFolder();

            nud_freight.Visible = false;
            lbl_freight_charge.Visible = false;

            labelQuantity.Visible = false;
            numericUpDownQuantity.Visible = false;

            // Added 23-03-2015 BN
            nud_freight.Value = 0;
            cmb_Rates.SelectedIndex = -1;

            ckb_COA.Visible = false;
            btn_delete.Visible = false;

            //must be done after file deletion.
            FruPak.PF.Common.Code.Send_Email.Email_Subject = null;
            FruPak.PF.Common.Code.Send_Email.Email_Message = null;
            FruPak.PF.PrintLayer.Word.FilePath = null;
            FruPak.PF.PrintLayer.Word.FileName = null;
            FruPak.PF.Common.Code.SendEmail.attachment = null;
            FruPak.PF.Common.Code.General.delivery_name = null;
        }

        private void SizeAllColumns(Object sender, EventArgs e)
        {
            Column_resize();
        }

        // Everything below here is just housekeeping ----------------------------------------------------

        #region Methods to log UI events to the CSV file. BN 29/01/2015

        private void Control_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            logger.Log(LogLevel.Info, DecorateString(cb.Name, cb.Checked.ToString(), "CheckedChanged"));
        }

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

        private void Control_NudValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            logger.Log(LogLevel.Info, DecorateString(nud.Name, nud.Text, "NudValueChanged"));
        }

        private void Control_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            logger.Log(LogLevel.Info, DecorateString(cb.Name, cb.Text, "SelectedValueChanged"));
        }

        private void Control_Validated(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            t.Text = FruPak.PF.Global.Global.CleanSqlComment(t.Text);
            logger.Log(LogLevel.Info, DecorateString(t.Name, t.Text, "Validated (Via Sql Cleaner)"));
        }
        private void Control_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;
            logger.Log(LogLevel.Info, DecorateString(dtp.Name, dtp.Text, "ValueChanged"));
        }
        private void CustomerControl_CustomerChanged(object sender, EventArgs e)
        {
            FruPak.PF.Utils.UserControls.Customer cust = (FruPak.PF.Utils.UserControls.Customer)sender;
            logger.Log(LogLevel.Info, DecorateString(cust.Name, cust.Customer_Name, "TextChanged"));
        }

        #region Decorate String

        private string closePad = " ] --- ";

        private string intro = "--->   { ";

        // DecorateString
        private string openPad = " --- [ ";
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
        private void Invoicing_Sales_KeyDown(object sender, KeyEventArgs e)
        {
            // Note that the KeyPreview property must be set to true for this to work
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        #endregion Methods to log UI events to the CSV file. BN 29/01/2015
        #region History Messages - All this is obsolete and will be removed

        private void ckb_COA_CheckedChanged(object sender, EventArgs e)
        {
            FireHistoryMessage("Include COA: " + ckb_COA.Checked.ToString());
        }

        private void cmb_Rates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Rates.Text != string.Empty)
            {
                Cursor.Current = Cursors.WaitCursor;
                FireHistoryMessage("Rates ComboBox: " + cmb_Rates.Text);
                Cursor.Current = Cursors.Default;
                
                // Added 10/11/2015 - BN
                if(cmb_Rates.Text == "Freight Charges")
                {
                    labelQuantity.Visible = false;
                    numericUpDownQuantity.Visible = false;

                    lbl_freight_charge.Visible = true;
                    nud_freight.Visible = true;
                }
                else
                {
                    lbl_freight_charge.Visible = false;
                    nud_freight.Visible = false;

                    labelQuantity.Visible = true;
                    numericUpDownQuantity.Visible = true;
                }

            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //GetHistoryDataRowFromDataGridView(sender, e);
        }

        private void dtp_Invoice_Date_ValueChanged(object sender, EventArgs e)
        {
            FireHistoryMessage("Invoice Date: " + dtp_Invoice_Date.Value.ToLongDateString());
        }

        private void FireHistoryMessage(string Message)
        {
            // instance the event Args and pass it each value
            HistoryUpdateEventArgs args = new HistoryUpdateEventArgs(Message);

            // raise the event with the updated arguments
            // Something has changed - I suspect HistoryUpdated is not being called in the constructor all of a sudden,
            // whereas it used to work perfectly.
            if (HistoryUpdated != null)
            {
                HistoryUpdated(this, args); // This is now only firing when the close button is clicked
            }
        }

        private void GetHistoryDataRowFromDataGridView(object sender, DataGridViewCellEventArgs e)
        {
            #region Grab data from selected DataGridView Row

            // Special case as there's more than one DataGridView on this form
            DataGridView dgv = (DataGridView)sender;

            //DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            DataGridViewRow row = dgv.Rows[e.RowIndex];

            string sRowData = string.Empty;
            string sHeaderData = string.Empty;

            for (int i = 0; i < row.Cells.Count; i++)
            {
                //Console.WriteLine(row.Cells[i].Value.GetType().ToString());

                // SOLVED at last!
                // When debugging, if a DataTip shows "", in the ToolTip visualizer, it's an empty string.
                // When debugging, if a DataTip shows {}, in the ToolTip visualizer, it's a DBNull.

                if (row.Cells[i].Value != string.Empty && row.Cells[i].Value.GetType() != typeof(DBNull))
                {
                    sRowData += row.Cells[i].Value.ToString() + ", ";
                }
                else if (row.Cells[i].Value.GetType() == typeof(DBNull))
                {
                    sRowData += row.Cells[i].Value.ToString() + "DBNull, ";
                }
                else
                {
                    sRowData += "null, ";
                }
            }

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                sHeaderData += col.HeaderText + ", ";
            }

            // Remove the trailing comma and space.
            sRowData = sRowData.TrimEnd(',', ' ');
            sHeaderData = sHeaderData.TrimEnd(',', ' ');

            // Dump the headers to make it easy to read
            FireHistoryMessage("Column Titles: " + sHeaderData);

            // Then dump the actual values
            FireHistoryMessage("Cell Values  : " + sRowData);

            #endregion Grab data from selected DataGridView Row
        }
        private void nud_freight_ValueChanged(object sender, EventArgs e)
        {
            FireHistoryMessage("Freight Charge UpDown: " + nud_freight.Value.ToString());

            // It has to be one or the other
            numericUpDownQuantity.Enabled = false;
        }

        private void txt_comments_TextChanged(object sender, EventArgs e)
        {
            //FireHistoryMessage("Comments: " + txt_comments.Text);
        }

        private void txt_comments_Validated(object sender, EventArgs e)
        {
            FireHistoryMessage("Comments: " + txt_comments.Text);
        }

        private void txt_order_num_TextChanged(object sender, EventArgs e)
        {
            if (txt_order_num.Text != string.Empty)
            {
                FireHistoryMessage("Order Number: " + txt_order_num.Text);
            }
        }
        #endregion History Messages - All this is obsolete and will be removed

        public static void ViewAnyPdfsInTheTempFolder()
        {
            // This is new, now that I've finally got the COA print to pdf working
            string TempPath = string.Empty;

            #region Pull the temp path from the database
            string path = Application.StartupPath;
            Settings = new PhantomCustomSettings();
            Settings.SettingsPath = Path.Combine(path, "FruPak.Phantom.config");
            Settings.EncryptionKey = "phantomKey";
            Settings.Load();
            TempPath = Settings.Path_Local_PDF_Files;

            DirectoryInfo di = new DirectoryInfo(TempPath);
            foreach (FileInfo fi in di.GetFiles())
            {
                if (fi.Extension == ".pdf")
                {
                    //FruPak.PF.Common.Code.General.Report_Viewer(FruPak.PF.PrintLayer.Word.FilePath + "\\" + view_file);

                    FruPak.PF.Common.Code.General.Report_Viewer(TempPath + "\\" + fi.Name);

                    //SendToPrinter(TempPath, fi.Name);
                    // Delete the file
                    //fi.Delete();
                }
            }

            #endregion

        }

        #region Big Force Gun

        /// <summary>
        /// Return a list of file locks held by the process.
        /// </summary>
        public static List<string> GetFilesLockedBy(Process process)
        {
            var outp = new List<string>();

            ThreadStart ts = delegate
            {
                try
                {
                    outp = UnsafeGetFilesLockedBy(process);
                }
                catch { Ignore(); }
            };

            try
            {
                var t = new Thread(ts);
                t.IsBackground = true;
                t.Start();
                if (!t.Join(250))
                {
                    try
                    {
                        t.Interrupt();
                        t.Abort();
                    }
                    catch { Ignore(); }
                }
            }
            catch { Ignore(); }

            return outp;
        }

        public static List<Process> GetProcessesLockingFile(string filePath)
        {
            var procs = new List<Process>();

            var processListSnapshot = Process.GetProcesses();
            foreach (var process in processListSnapshot)
            {
                Console.WriteLine(process.ProcessName);
                if (process.Id <= 4) { continue; } // system processes
                var files = GetFilesLockedBy(process);
                if (files.Contains(filePath))
                {
                    Console.WriteLine("--------------->" + process.ProcessName);
                    procs.Add(process);
                }
            }
            return procs;
        }
        #region Inner Workings

        private const int CNST_SYSTEM_HANDLE_INFORMATION = 16;

        private static string GetFilePath(Win32API.SYSTEM_HANDLE_INFORMATION systemHandleInformation, Process process)
        {
            var ipProcessHwnd = Win32API.OpenProcess(Win32API.ProcessAccessFlags.All, false, process.Id);
            var objBasic = new Win32API.OBJECT_BASIC_INFORMATION();
            var objObjectType = new Win32API.OBJECT_TYPE_INFORMATION();
            var objObjectName = new Win32API.OBJECT_NAME_INFORMATION();
            var strObjectName = "";
            var nLength = 0;
            IntPtr ipTemp, ipHandle;

            if (!Win32API.DuplicateHandle(ipProcessHwnd, systemHandleInformation.Handle, Win32API.GetCurrentProcess(), out ipHandle, 0, false, Win32API.DUPLICATE_SAME_ACCESS))
                return null;

            IntPtr ipBasic = Marshal.AllocHGlobal(Marshal.SizeOf(objBasic));
            Win32API.NtQueryObject(ipHandle, (int)Win32API.ObjectInformationClass.ObjectBasicInformation, ipBasic, Marshal.SizeOf(objBasic), ref nLength);
            objBasic = (Win32API.OBJECT_BASIC_INFORMATION)Marshal.PtrToStructure(ipBasic, objBasic.GetType());
            Marshal.FreeHGlobal(ipBasic);

            IntPtr ipObjectType = Marshal.AllocHGlobal(objBasic.TypeInformationLength);
            nLength = objBasic.TypeInformationLength;
            // this one never locks...
            while ((uint)(Win32API.NtQueryObject(ipHandle, (int)Win32API.ObjectInformationClass.ObjectTypeInformation, ipObjectType, nLength, ref nLength)) == Win32API.STATUS_INFO_LENGTH_MISMATCH)
            {
                if (nLength == 0)
                {
                    Console.WriteLine("nLength returned at zero! ");
                    return null;
                }
                Marshal.FreeHGlobal(ipObjectType);
                ipObjectType = Marshal.AllocHGlobal(nLength);
            }

            objObjectType = (Win32API.OBJECT_TYPE_INFORMATION)Marshal.PtrToStructure(ipObjectType, objObjectType.GetType());
            if (Is64Bits())
            {
                ipTemp = new IntPtr(Convert.ToInt64(objObjectType.Name.Buffer.ToString(), 10) >> 32);
            }
            else
            {
                ipTemp = objObjectType.Name.Buffer;
            }

            var strObjectTypeName = Marshal.PtrToStringUni(ipTemp, objObjectType.Name.Length >> 1);
            Marshal.FreeHGlobal(ipObjectType);
            if (strObjectTypeName != "File")
                return null;

            nLength = objBasic.NameInformationLength;

            var ipObjectName = Marshal.AllocHGlobal(nLength);

            // ...this call sometimes hangs. Is a Windows error.
            while ((uint)(Win32API.NtQueryObject(ipHandle, (int)Win32API.ObjectInformationClass.ObjectNameInformation, ipObjectName, nLength, ref nLength)) == Win32API.STATUS_INFO_LENGTH_MISMATCH)
            {
                Marshal.FreeHGlobal(ipObjectName);
                if (nLength == 0)
                {
                    Console.WriteLine("nLength returned at zero! " + strObjectTypeName);
                    return null;
                }
                ipObjectName = Marshal.AllocHGlobal(nLength);
            }
            objObjectName = (Win32API.OBJECT_NAME_INFORMATION)Marshal.PtrToStructure(ipObjectName, objObjectName.GetType());

            if (Is64Bits())
            {
                ipTemp = new IntPtr(Convert.ToInt64(objObjectName.Name.Buffer.ToString(), 10) >> 32);
            }
            else
            {
                ipTemp = objObjectName.Name.Buffer;
            }

            if (ipTemp != IntPtr.Zero)
            {
                var baTemp = new byte[nLength];
                try
                {
                    Marshal.Copy(ipTemp, baTemp, 0, nLength);

                    strObjectName = Marshal.PtrToStringUni(Is64Bits() ? new IntPtr(ipTemp.ToInt64()) : new IntPtr(ipTemp.ToInt32()));
                }
                catch (AccessViolationException)
                {
                    return null;
                }
                finally
                {
                    Marshal.FreeHGlobal(ipObjectName);
                    Win32API.CloseHandle(ipHandle);
                }
            }

            string path = GetRegularFileNameFromDevice(strObjectName);
            try
            {
                return path;
            }
            catch
            {
                return null;
            }
        }

        private static IEnumerable<Win32API.SYSTEM_HANDLE_INFORMATION> GetHandles(Process process)
        {
            var nHandleInfoSize = 0x10000;
            var ipHandlePointer = Marshal.AllocHGlobal(nHandleInfoSize);
            var nLength = 0;
            IntPtr ipHandle;

            while (Win32API.NtQuerySystemInformation(CNST_SYSTEM_HANDLE_INFORMATION, ipHandlePointer, nHandleInfoSize, ref nLength) == Win32API.STATUS_INFO_LENGTH_MISMATCH)
            {
                nHandleInfoSize = nLength;
                Marshal.FreeHGlobal(ipHandlePointer);
                ipHandlePointer = Marshal.AllocHGlobal(nLength);
            }

            var baTemp = new byte[nLength];
            Marshal.Copy(ipHandlePointer, baTemp, 0, nLength);

            long lHandleCount;
            if (Is64Bits())
            {
                lHandleCount = Marshal.ReadInt64(ipHandlePointer);
                ipHandle = new IntPtr(ipHandlePointer.ToInt64() + 8);
            }
            else
            {
                lHandleCount = Marshal.ReadInt32(ipHandlePointer);
                ipHandle = new IntPtr(ipHandlePointer.ToInt32() + 4);
            }

            var lstHandles = new List<Win32API.SYSTEM_HANDLE_INFORMATION>();

            for (long lIndex = 0; lIndex < lHandleCount; lIndex++)
            {
                var shHandle = new Win32API.SYSTEM_HANDLE_INFORMATION();
                if (Is64Bits())
                {
                    shHandle = (Win32API.SYSTEM_HANDLE_INFORMATION)Marshal.PtrToStructure(ipHandle, shHandle.GetType());
                    ipHandle = new IntPtr(ipHandle.ToInt64() + Marshal.SizeOf(shHandle) + 8);
                }
                else
                {
                    ipHandle = new IntPtr(ipHandle.ToInt64() + Marshal.SizeOf(shHandle));
                    shHandle = (Win32API.SYSTEM_HANDLE_INFORMATION)Marshal.PtrToStructure(ipHandle, shHandle.GetType());
                }
                if (shHandle.ProcessID != process.Id) continue;
                lstHandles.Add(shHandle);
            }
            return lstHandles;
        }

        private static string GetRegularFileNameFromDevice(string strRawName)
        {
            string strFileName = strRawName;
            foreach (string strDrivePath in Environment.GetLogicalDrives())
            {
                var sbTargetPath = new StringBuilder(Win32API.MAX_PATH);
                if (Win32API.QueryDosDevice(strDrivePath.Substring(0, 2), sbTargetPath, Win32API.MAX_PATH) == 0)
                {
                    return strRawName;
                }
                string strTargetPath = sbTargetPath.ToString();
                if (strFileName.StartsWith(strTargetPath))
                {
                    strFileName = strFileName.Replace(strTargetPath, strDrivePath.Substring(0, 2));
                    break;
                }
            }
            return strFileName;
        }

        private static void Ignore()
        {
        }

        private static bool Is64Bits()
        {
            return Marshal.SizeOf(typeof(IntPtr)) == 8;
        }

        private static List<string> UnsafeGetFilesLockedBy(Process process)
        {
            try
            {
                var handles = GetHandles(process);
                var files = new List<string>();

                foreach (var handle in handles)
                {
                    var file = GetFilePath(handle, process);
                    if (file != null) files.Add(file);
                }

                return files;
            }
            catch
            {
                return new List<string>();
            }
        }
        internal class Win32API
        {
            public const int DUPLICATE_SAME_ACCESS = 0x2;

            public const uint FILE_SEQUENTIAL_ONLY = 0x00000004;

            public const int MAX_PATH = 260;

            public const uint STATUS_INFO_LENGTH_MISMATCH = 0xC0000004;

            public enum ObjectInformationClass
            {
                ObjectBasicInformation = 0,
                ObjectNameInformation = 1,
                ObjectTypeInformation = 2,
                ObjectAllTypesInformation = 3,
                ObjectHandleInformation = 4
            }

            [Flags]
            public enum ProcessAccessFlags : uint
            {
                All = 0x001F0FFF,
                Terminate = 0x00000001,
                CreateThread = 0x00000002,
                VMOperation = 0x00000008,
                VMRead = 0x00000010,
                VMWrite = 0x00000020,
                DupHandle = 0x00000040,
                SetInformation = 0x00000200,
                QueryInformation = 0x00000400,
                Synchronize = 0x00100000
            }

            [DllImport("kernel32.dll")]
            public static extern int CloseHandle(IntPtr hObject);

            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool DuplicateHandle(IntPtr hSourceProcessHandle,
               ushort hSourceHandle, IntPtr hTargetProcessHandle, out IntPtr lpTargetHandle,
               uint dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, uint dwOptions);

            [DllImport("kernel32.dll")]
            public static extern IntPtr GetCurrentProcess();

            [DllImport("ntdll.dll")]
            public static extern int NtQueryObject(IntPtr ObjectHandle, int
                ObjectInformationClass, IntPtr ObjectInformation, int ObjectInformationLength,
                ref int returnLength);

            [DllImport("ntdll.dll")]
            public static extern uint NtQuerySystemInformation(int
                SystemInformationClass, IntPtr SystemInformation, int SystemInformationLength,
                ref int returnLength);

            [DllImport("kernel32.dll")]
            public static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern uint QueryDosDevice(string lpDeviceName, StringBuilder lpTargetPath, int ucchMax);
            [StructLayout(LayoutKind.Sequential)]
            public struct GENERIC_MAPPING
            {
                public int GenericRead;
                public int GenericWrite;
                public int GenericExecute;
                public int GenericAll;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct OBJECT_BASIC_INFORMATION
            { // Information Class 0
                public int Attributes;
                public int GrantedAccess;
                public int HandleCount;
                public int PointerCount;
                public int PagedPoolUsage;
                public int NonPagedPoolUsage;
                public int Reserved1;
                public int Reserved2;
                public int Reserved3;
                public int NameInformationLength;
                public int TypeInformationLength;
                public int SecurityDescriptorLength;
                public System.Runtime.InteropServices.ComTypes.FILETIME CreateTime;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct OBJECT_NAME_INFORMATION
            { // Information Class 1
                public UNICODE_STRING Name;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct OBJECT_TYPE_INFORMATION
            { // Information Class 2
                public UNICODE_STRING Name;
                public int ObjectCount;
                public int HandleCount;
                public int Reserved1;
                public int Reserved2;
                public int Reserved3;
                public int Reserved4;
                public int PeakObjectCount;
                public int PeakHandleCount;
                public int Reserved5;
                public int Reserved6;
                public int Reserved7;
                public int Reserved8;
                public int InvalidAttributes;
                public GENERIC_MAPPING GenericMapping;
                public int ValidAccess;
                public byte Unknown;
                public byte MaintainHandleDatabase;
                public int PoolType;
                public int PagedPoolUsage;
                public int NonPagedPoolUsage;
            }
            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct SYSTEM_HANDLE_INFORMATION
            { // Information Class 16
                public int ProcessID;
                public byte ObjectTypeNumber;
                public byte Flags; // 0x01 = PROTECT_FROM_CLOSE, 0x02 = INHERIT
                public ushort Handle;
                public int Object_Pointer;
                public UInt32 GrantedAccess;
            }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct UNICODE_STRING
            {
                public ushort Length;
                public ushort MaximumLength;
                public IntPtr Buffer;
            }
        }

        #endregion Inner Workings

        #endregion Big Force Gun

        private void buttonNewView_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDownQuantity_ValueChanged(object sender, EventArgs e)
        {
            // It has to be one or the other
            nud_freight.Enabled = false;
        }
    }
}