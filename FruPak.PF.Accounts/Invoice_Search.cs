using DataGridViewAutoFilter;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using PF.CustomSettings;

namespace PF.Accounts
{
    public partial class Invoice_Search : Form
    {
        private static PhantomCustomSettings Settings;

        #region Delegates to pass messages - place just after public partial class

        // add a delegate
        public delegate void HistoryUpdateHandler(object sender, HistoryUpdateEventArgs e);

        // add an event of the delegate type
        public event HistoryUpdateHandler HistoryUpdated;

        #endregion Delegates to pass messages - place just after public partial class

        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static bool bol_write_access;
        private static int int_Current_User_Id = 0;
        private BindingSource bs_dataSource;

        public Invoice_Search(int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();
            int_Current_User_Id = int_C_User_id;

            //restrict access
            bol_write_access = bol_w_a;

            this.toolTip1.SetToolTip(this.btn_Email_Customer,
                "Send a copy of the invoice to the customer," +
                Environment.NewLine +
                "and include the COA if selected." +
                Environment.NewLine +
                Environment.NewLine +
                "A copy of the invoice only is also emailed to the office.");  // Set multiline tooltip
                                                                               //check if testing or not

            //if (PF.Global.Global.bol_Testing == true)
            //{
            //    this.Text = "FP Process Factory - " + this.Text + " - Test Environment";
            //}
            //else
            //{
            //    this.Text = "FP Process Factory";
            //}

            populate_DataGridView1();
            //dataGridView1.Rows[1].Selected = true;
            //dataGridView1.Rows[0].Selected = true;

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

        private void binding()
        {
            // Continue only if the data source has been set.
            if (dataGridView1.DataSource == null)
            {
                return;
            }

            dataGridView1.Columns[0].HeaderText = "Invoice Num";
            dataGridView1.Columns[0].Name = "Invoice_Id";
            dataGridView1.Columns[0].ReadOnly = true;

            dataGridView1.Columns[1].HeaderText = "Invoice Date";
            dataGridView1.Columns[1].Name = "Invoice_Date";
            dataGridView1.Columns[1].ReadOnly = true;

            dataGridView1.Columns[2].HeaderText = "Customer";
            dataGridView1.Columns[2].Name = "Customer";

            dataGridView1.Columns[3].HeaderText = "Trader_Id";
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[3].Visible = false;

            dataGridView1.Columns[4].HeaderText = "Greentree_Date";
            dataGridView1.Columns[4].Name = "Greentree_Date";
            dataGridView1.Columns[4].ReadOnly = true;

            dataGridView1.Columns[5].HeaderText = "Order Num";
            dataGridView1.Columns[5].Name = "Order_Num";
            dataGridView1.Columns[5].ReadOnly = true;

            dataGridView1.Columns[6].HeaderText = "Comments";
            dataGridView1.Columns[6].ReadOnly = true;

            // Resize the columns to fit their contents.
            dataGridView1.AutoResizeColumns();

            // Add the AutoFilter header cell to each column.
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (col.Index != 7)
                {
                    col.HeaderCell = new DataGridViewAutoFilterColumnHeaderCell(col.HeaderCell);
                }
            }
        }

        private void populate_DataGridView1()
        {
            dataGridView1.Refresh();
            DataSet ds_Get_Info;
            ds_Get_Info = PF.Data.AccessLayer.PF_A_Invoice.Get_Info_for_Invoice_search();
            bs_dataSource = new BindingSource(ds_Get_Info, "Table");
            dataGridView1.DataSource = bs_dataSource;
            binding();
        }

        private void SizeAllColumns(Object sender, EventArgs e)
        {
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void cmb_Customers_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (customer1.Customer_Id > 0)
            {
                FireHistoryMessage("Customers ComboBox: " + customer1.Customer_Name);

                //this._callingForm = this.f

                //Main_Menu.historyAdd("yo");

                // Message=Cannot find column [Trader_Id]
                // bs_dataSource.Filter = "Trader_Id = " + customer1.Customer_Id;

                // BN 21/01/2014
                bs_dataSource.Filter = "Customer_Id = " + customer1.Customer_Id;
            }

            Cursor.Current = Cursors.Default;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            FireHistoryMessage("Invoice Search Close Button Clicked");
            //PF.Common.Code.KillAllWordInstances.KillAllWordProcesses();

            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            GetHistoryDataRowFromDataGridView(sender, e);

            string str_msg = "";

            //Print

            #region ------------- Print -------------

            if (e.ColumnIndex == 2)
            {
            }
            if (str_msg.Length > 0)
            {
                MessageBox.Show(str_msg, "Process Factory - Invoice Search", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion ------------- Print -------------
        }

        private static int int_Trader_Id = 0;
        private static string str_save_filename = "";
        private static string str_delivery_name = "";

        private string Create_PDF_Defaults(string str_type, int Invoice_Id, string Invoice_Date, string Order_Num)
        {
            DataSet ds_Get_Info;
            DataRow dr_Get_Info;
            decimal dec_GST = 0;

            string str_msg = "";

            #region ------------- defaults -------------

            ds_Get_Info = PF.Data.AccessLayer.CM_System.Get_Info_Like("PF%");

            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                switch (dr_Get_Info["Code"].ToString())
                {
                    case "PF-TPath":
                        PF.PrintLayer.Word.TemplatePath = dr_Get_Info["Value"].ToString();
                        break;

                    case "PF-TInv1":
                        PF.PrintLayer.Word.TemplateName = dr_Get_Info["Value"].ToString();
                        break;

                    case "PF-SPath":
                        PF.PrintLayer.Word.FilePath = dr_Get_Info["Value"].ToString();
                        break;
                }
            }
            ds_Get_Info.Dispose();

            //get GST
            ds_Get_Info = PF.Data.AccessLayer.CM_System.Get_Info_Like("GST");
            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                dec_GST = Convert.ToDecimal(dr_Get_Info["Value"].ToString());
            }
            ds_Get_Info.Dispose();

            #endregion ------------- defaults -------------

            #region ------------- Outlook details -------------

            ds_Get_Info = PF.Data.AccessLayer.PF_A_Invoice.Get_Info(Invoice_Id);
            for (int i = 0; i < ds_Get_Info.Tables[0].Rows.Count; i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                str_msg = str_msg + PF.Common.Code.Outlook.Check_Outlook(Convert.ToInt32(dr_Get_Info["Customer_Id"].ToString()), str_msg, str_type);
                try
                {
                    if (dr_Get_Info["Trader_ID"] != null && dr_Get_Info["Trader_ID"] != DBNull.Value)
                    {
                        //Console.WriteLine(dr_Get_Info["Trader_Id"]);
                        Convert.ToInt32(dr_Get_Info["Trader_Id"].ToString());
                        int_Trader_Id = Convert.ToInt32(dr_Get_Info["Trader_Id"].ToString());
                    }
                    else
                    {
                        int_Trader_Id = 0;
                        logger.Log(LogLevel.Warn, "FYI: Trader_Id is DBNull or null.");
                    }
                }
                catch (Exception ex)
                {
                    logger.Log(LogLevel.Debug, ex.Message);
                }
            }
            ds_Get_Info.Dispose();

            str_delivery_name = PF.Common.Code.General.delivery_name;
            str_save_filename = str_delivery_name;

            #endregion ------------- Outlook details -------------

            if (str_msg.Length <= 0)
            {
                if (int_Trader_Id > 0)
                {
                    string Data = "";
                    Data = Data + Convert.ToString(Invoice_Id);
                    Data = Data + ":" + Convert.ToString(int_Trader_Id);
                    Data = Data + ":" + Invoice_Date;
                    Data = Data + ":" + Order_Num;
                    Data = Data + ":" + str_delivery_name;
                    Data = Data + ":" + str_save_filename;
                    Data = Data + ":" + Convert.ToString(dec_GST);
                    Data = Data + ":" + str_type;
                    int int_result = 0;
                    int_result = PF.PrintLayer.Invoice_Work_Orders.Print(Data, true);
                }
                else
                {
                    string Data = "";

                    Data = Data + Convert.ToString(Invoice_Id);
                    Data = Data + ":" + Invoice_Date;
                    Data = Data + ":" + Order_Num;
                    Data = Data + ":" + str_delivery_name;
                    Data = Data + ":" + str_save_filename;
                    Data = Data + ":" + Convert.ToString(dec_GST);
                    Data = Data + ":" + str_type;

                    int int_result = 0;
                    int_result = PF.PrintLayer.Invoice_Sales.Print(Data, true);
                }
            }

            //Print_COA();

            //if(ckb_COA.CheckState = CheckState.Checked)
            //{
            //}

            return str_msg;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                txt_Selected.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                this.btn_Email_Customer.Enabled = true;
            }
        }

        public static void AttachAnyPdfsInTheTempFolder()
        {
            try
            {
                // This is new, now that I've finally got the COA print to pdf working
                string TempPath = string.Empty;

                #region Pull the temp path from the database
                string path = Application.StartupPath;
                Settings = new PhantomCustomSettings();
                Settings.SettingsPath = Path.Combine(path, "FP.Phantom.config");
                Settings.EncryptionKey = "phantomKey";
                Settings.Load();
                TempPath = Settings.Path_Local_PDF_Files;

                DirectoryInfo di = new DirectoryInfo(TempPath);
                foreach (FileInfo fi in di.GetFiles())
                {
                    if (fi.Extension.ToLower() == ".pdf")   // Added ToLower() 3/12/2015
                    {
                        //PF.Common.Code.SendEmail.attachment.Add(PF.PrintLayer.Word.FilePath + "\\" + PF.PrintLayer.Word.FileName + ".PDF");
                        PF.Common.Code.SendEmail.attachment.Add(TempPath + "\\" + fi.Name);
                        logger.Log(LogLevel.Info, "Adding attachment for Customer: " + fi.Name);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                Common.Code.DebugStacktrace.StackTrace(ex);

            }

        }

        private void btn_Email_Customer_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)   // BN fix 10-03-2015 - Would crash if no rows and you accidentally hit the button
            //if (dataGridView1.CurrentRow != null && PF.PrintLayer.General.view_list != null)   // BN fix 10-03-2015 - Would crash if no rows and you accidentally hit the button
            {
                errorProvider1.Clear();

                int int_result = 0;
                string str_msg = "";
                // Email Customer only
                Cursor = Cursors.WaitCursor;
                PF.Common.Code.Send_Email.Email_Subject = "";
                PF.Common.Code.Send_Email.Email_Message = "";
                str_msg = str_msg + Create_PDF_Defaults("Email", Convert.ToInt32(dataGridView1.CurrentRow.Cells["Invoice_Id"].Value.ToString()), dataGridView1.CurrentRow.Cells["Invoice_Date"].Value.ToString(), dataGridView1.CurrentRow.Cells["Order_Num"].Value.ToString());

                // I think I've just found it 3/12/2015 - This is probably the bit that fails at attaching more than one invoice
                //PF.Common.Code.SendEmail.attachment = new List<string>();

                //foreach (string view_file in PF.PrintLayer.General.view_list)
                //{
                //    PF.Common.Code.SendEmail.attachment.Add(PF.PrintLayer.Word.FilePath + "\\" + view_file);
                //    logger.Log(LogLevel.Info, "Adding attachment for Customer: " + view_file);
                //}

                AttachAnyPdfsInTheTempFolder();

                // print COA

                #region ------------- Certificate of Analysis -------------

                if (ckb_COA.Checked == true)
                {
                    Cursor = Cursors.WaitCursor;
                    DataSet ds = PF.Data.AccessLayer.PF_A_Invoice_Details.Get_Info(Convert.ToInt32(dataGridView1.CurrentRow.Cells["Invoice_Id"].Value.ToString()));
                    DataRow dr;

                    string str_order_num = "";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dr = ds.Tables[0].Rows[i];

                        //// Don't worry about this foreach bit; is just me debugging - each element in the ItemArray  (Column/Field in DataRow)
                        //foreach (var item in dr.ItemArray)
                        //{
                        //    Console.WriteLine("COA - DR: " + item);
                        //}

                        str_order_num = dr["Order_Num"].ToString(); ;
                        string Data = "";
                        Data = Data + dr["Order_Num"].ToString();
                        Data = Data + ":" + str_delivery_name;
                        Data = Data + ":" + PF.Data.Outlook.Contacts.Contact_Business_Address.ToString();
                        Data = Data + ":Email";
                        Cursor = Cursors.WaitCursor;
                        PF.PrintLayer.Word.FileName = "COA" + dr["Order_Num"].ToString() + "-" + Convert.ToString(i);
                        PF.PrintLayer.Certificate_Of_Analysis.Print(Data, true);

                        // This line is commented out - is it because it will only add one attachment? It's also not commented out in my usual style:
                        // Indented to the correct level, one space following the double forward slash indicating a comment,
                        // or indented to the correct level with no space preceding the code, indicating a temporarily disabled code line.
                        // This is not my code; must be Dave experimenting.

                        //  PF.Common.Code.SendEmail.attachment.Add(PF.PrintLayer.Word.FilePath + "\\" + PF.PrintLayer.Word.FileName + ".PDF");
                    }

                    // This is very strange; I don't really recognise the syntax... - BN 05-03-2015
                    PF.Common.Code.SendEmail.attachment.AddRange(System.IO.Directory.GetFiles(PF.PrintLayer.Word.FilePath,
                        "COA" + str_order_num + "*",
                        System.IO.SearchOption.TopDirectoryOnly)
                        .Select(path => Path.GetFullPath(path))     // Got it now. Is kind of like the With statement in VB. Is hard to read (for Me)
                        .ToArray());

                    // I think I see the intent in the preceding code - build an array of multiple file attachments so you can have more than one per
                    // email to the customer - we are inside btn_Email_Customer_Click, btw. The options chosen which get you to this point are
                    // check the COA box and hit the email to customer button.
                    // I note he's trying to filter it to a COA prefix with an order number and a wildcard following.
                    //
                    // I wonder about: the
                    //      .syntax, which really I have only ever seen in VB
                    //      the TopDirectoryOnly option - was that really necessary given we are explicitly looking in:
                    //      C:\FP\Client\Printing\Saved\Ffowcs Williams-0 - 20141013.PDF
                    // But, if that's the intent, why not just loop through each file in the file/directory list and process each one sequentially.
                    //
                    // Also, it's happening just outside a loop which iterates through each record in a DataArray pertaining to a particular InvoiceId,
                    // which is trying to print a COA anyway.
                    // This looks very strange to me.

                    Cursor = Cursors.Default;
                }

                #endregion ------------- Certificate of Analysis -------------

                int_result = PF.Common.Code.Outlook.Email(str_msg, false);
                Cursor = Cursors.Default;

                // Email Office Only
                Cursor = Cursors.WaitCursor;
                PF.Common.Code.Send_Email.Email_Subject = "";
                PF.Common.Code.Send_Email.Email_Message = "";
                str_msg = str_msg + Create_PDF_Defaults("Email", Convert.ToInt32(dataGridView1.CurrentRow.Cells["Invoice_Id"].Value.ToString()), dataGridView1.CurrentRow.Cells["Invoice_Date"].Value.ToString(), dataGridView1.CurrentRow.Cells["Order_Num"].Value.ToString());
                PF.Common.Code.SendEmail.attachment = new List<string>();
                PF.Common.Code.SendEmail.attachment.Add(PF.PrintLayer.Word.FilePath + "\\" + PF.PrintLayer.Word.FileName);

                logger.Log(LogLevel.Info, "Adding attachment Office only: " + PF.PrintLayer.Word.FileName);

                PF.Common.Code.Outlook.Email_Office("FP-Off1%", str_save_filename, str_msg);
                Cursor = Cursors.Default;

                int_result = PF.Data.AccessLayer.PF_A_Invoice.Update_Greentree_Date(Convert.ToInt32(dataGridView1.CurrentRow.Cells["Invoice_Id"].Value.ToString()), int_Current_User_Id);
                if (int_result == 0)
                {
                    lbl_message.Text = lbl_message.Text + "Invoice has been emailed to Office and Printed for Customer.";
                    PF.Data.AccessLayer.PF_A_Invoice.Update_Greentree_Date(Convert.ToInt32(dataGridView1.CurrentRow.Cells["Invoice_Id"].Value.ToString()), int_Current_User_Id);
                    Reset();
                }

                FireHistoryMessage("Email Button Clicked: " + str_msg);
            }
            else
            {
                errorProvider1.SetError(btn_Email_Customer, "There are no records available to work with");
            }
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)   // BN fix 10-03-2015 - Would crash if no rows and you accidentally hit the button
            {
                string str_msg = "";
                int int_result = 0;

                //NOthing CHecked
                if (ckb_Customer.Checked == false && ckb_Office.Checked == false)
                {
                    errorProvider1.SetError(btn_Print, "You must select who the Printout is for, either Customer, Office or Both.");
                    btn_Print.Enabled = false;
                    //MessageBox.Show("You must select who the Printout is for, either Customer, Office or Both.", "Process Factory - Invoice Search", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // print for Customer
                if (ckb_Customer.Checked == true)
                {
                    Cursor = Cursors.WaitCursor;
                    str_msg = str_msg + Create_PDF_Defaults("Print", Convert.ToInt32(dataGridView1.CurrentRow.Cells["Invoice_Id"].Value.ToString()), dataGridView1.CurrentRow.Cells["Invoice_Date"].Value.ToString(), dataGridView1.CurrentRow.Cells["Order_Num"].Value.ToString());
                    Cursor = Cursors.Default;
                }
                //Email to Office
                if (ckb_Office.Checked == true)
                {
                    Cursor = Cursors.WaitCursor;
                    str_msg = str_msg + Create_PDF_Defaults("EmailO", Convert.ToInt32(dataGridView1.CurrentRow.Cells["Invoice_Id"].Value.ToString()), dataGridView1.CurrentRow.Cells["Invoice_Date"].Value.ToString(), dataGridView1.CurrentRow.Cells["Order_Num"].Value.ToString());
                    PF.Common.Code.SendEmail.attachment = new List<string>();

                    PF.Common.Code.SendEmail.attachment.Add(PF.PrintLayer.Word.FilePath + "\\" + PF.PrintLayer.Word.FileName);

                    logger.Log(LogLevel.Info, "Adding attachment for Office: " + PF.PrintLayer.Word.FileName);

                    PF.Common.Code.Outlook.Email_Office("FP-Off1%", str_save_filename, str_msg);
                    Cursor = Cursors.Default;
                    int_result = PF.Data.AccessLayer.PF_A_Invoice.Update_Greentree_Date(Convert.ToInt32(dataGridView1.CurrentRow.Cells["Invoice_Id"].Value.ToString()), int_Current_User_Id);
                    if (int_result == 0)
                    {
                        lbl_message.Text = lbl_message.Text + "Invoice has been emailed to Office.";
                        PF.Data.AccessLayer.PF_A_Invoice.Update_Greentree_Date(Convert.ToInt32(dataGridView1.CurrentRow.Cells["Invoice_Id"].Value.ToString()), int_Current_User_Id);
                        Reset();
                    }
                }

                FireHistoryMessage("Print Button Clicked: " + str_msg);
            }
            else
            {
                MessageBox.Show("Nothing to do.", "No data to work with", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private List<string> view_list = new List<string>();

        private void btn_View_Click(object sender, EventArgs e)
        {
            string str_msg = "";
            DialogResult DLR_Message = new System.Windows.Forms.DialogResult();

            PF.PrintLayer.Word.FilePath = PF.Common.Code.General.Get_Single_System_Code("PF-TPPath");

            Cursor = Cursors.WaitCursor;
            str_msg = Create_PDF_Defaults("View", Convert.ToInt32(dataGridView1.CurrentRow.Cells["Invoice_Id"].Value.ToString()), dataGridView1.CurrentRow.Cells["Invoice_Date"].Value.ToString(), dataGridView1.CurrentRow.Cells["Order_Num"].Value.ToString());
            Cursor = Cursors.Default;

            FireHistoryMessage("View Button Clicked: " + str_msg);

            if (str_msg.Length > 0)
            {
                DLR_Message = MessageBox.Show(str_msg, "Process Factory - Invoice Search(View)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (DLR_Message != DialogResult.OK)
            {
                foreach (string view_file in PF.PrintLayer.General.view_list)
                {
                    PF.Common.Code.General.Report_Viewer(PF.PrintLayer.Word.FilePath + "\\" + view_file);
                }
            }
        }

        private void Reset()
        {
            lbl_message.Text = "";
            ckb_Customer.Checked = false;
            ckb_Office.Checked = false;
            btn_Print.Enabled = false;
            btn_Print.Text = "Print";

            //must be done after file deletion.
            PF.Common.Code.Send_Email.Email_Subject = null;
            PF.Common.Code.Send_Email.Email_Message = null;
            PF.PrintLayer.Word.FilePath = null;
            PF.PrintLayer.Word.FileName = null;
            PF.Common.Code.SendEmail.attachment = null;
            PF.Common.Code.General.delivery_name = null;
        }

        private void ckb_CheckedChanged(object sender, EventArgs e)
        {
            FireHistoryMessage("CheckBox Customer: " + ckb_Customer.Checked.ToString());
            FireHistoryMessage("CheckBox Office: " + ckb_Office.Checked.ToString());

            if (ckb_Customer.Checked == true && ckb_Office.Checked == false)
            {
                errorProvider1.Clear();
                btn_Print.Text = "Print for Customer";
                btn_Print.Enabled = true;
            }
            else if (ckb_Customer.Checked == false && ckb_Office.Checked == true)
            {
                errorProvider1.Clear();
                btn_Print.Text = "Email to Office Only";
                btn_Print.Enabled = true;
            }
            else if (ckb_Customer.Checked == true && ckb_Office.Checked == true)
            {
                errorProvider1.Clear();
                btn_Print.Text = "Print for Customer" + Environment.NewLine + "Email to Office";
                btn_Print.Enabled = true;
            }
            else
            {
                btn_Print.Text = "Print";
                btn_Print.Enabled = false;
                //errorProvider1.SetError(btn_Print, "You must select one or both checkboxes");
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

        #endregion Decorate String

        /// <summary>
        /// Close the form with the Esc key (Sel request 11-02-2015 BN)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Invoice_Search_KeyDown(object sender, KeyEventArgs e)
        {
            // Note that the KeyPreview property must be set to true for this to work
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        #endregion Methods to log UI events to the CSV file. BN 29/01/2015

        private void buttonSendDebugInfo_Click(object sender, EventArgs e)
        {
            FireHistoryMessage("Send Debug Info Button Clicked");
            logger.Log(LogLevel.Info, "Calling: SendDebugInfo");
            PF.Common.Code.SendDebugEmail sde = new Common.Code.SendDebugEmail();
            sde.Show();

            //PF.Common.Code.EmailDebugInfo.SendDebugInfo();
        }

        private void label1_TextChanged(object sender, EventArgs e)
        {
        }

        private void ckb_COA_CheckedChanged(object sender, EventArgs e)
        {
            FireHistoryMessage("CheckBox Certificate of Analysis: " + ckb_COA.Checked.ToString());
        }

        private void txt_Selected_TextChanged(object sender, EventArgs e)
        {
            FireHistoryMessage("Selected Invoice Number: " + txt_Selected.Text);
        }

        private void FireHistoryMessage(string Message)
        {
            // instance the event Args and pass it each value
            HistoryUpdateEventArgs args = new HistoryUpdateEventArgs(Message);

            // raise the event with the updated arguments
            HistoryUpdated(this, args);
        }

        private void GetHistoryDataRowFromDataGridView(object sender, DataGridViewCellEventArgs e)
        {
            #region Grab data from selected DataGridView Row

            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            string sRowData = string.Empty;
            string sHeaderData = string.Empty;

            for (int i = 0; i < row.Cells.Count; i++)
            {
                //Console.WriteLine(row.Cells[i].Value.GetType().ToString());

                // SOLVED at last!
                // When debugging, if a DataTip shows "", in the tooltip visualiser, it's an empty string.
                // When debugging, if a DataTip shows {}, in the tooltip visualiser, it's a DBNull.

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

            foreach (DataGridViewColumn col in dataGridView1.Columns)
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

        private void buttonOpenPdfFileLocation_Click(object sender, EventArgs e)
        {
            DataSet ds_Get_Info;
            DataRow dr_Get_Info;

            string savedPath = "";

            ds_Get_Info = PF.Data.AccessLayer.CM_System.Get_Info_Like("PF%");

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
            //Process.Start("explorer.exe", @"C:\FP\Client\Printing\Saved");
            Process.Start("explorer.exe", savedPath);
        }
    }
}