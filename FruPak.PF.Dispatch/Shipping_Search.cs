using DataGridViewAutoFilter;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using PF.CustomSettings;

namespace PF.Dispatch
{
    public partial class Shipping_Search : Form
    {
        private static PhantomCustomSettings Settings;

        private static bool bol_chb_All = false;
        private static bool bol_list_all = true;
        private static bool bol_write_access;
        private static int int_Current_User_Id = 0;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private bool bol_chb_check_pass = true;
        private DialogResult DLR_Message = new DialogResult();
        private ToolStripStatusLabel filterStatusLabel = new ToolStripStatusLabel();
        private List<string> lst_filenames = new List<string>();
        private ToolStripStatusLabel showAllLabel = new ToolStripStatusLabel("Show &All");
        private StatusStrip statusStrip1 = new StatusStrip();
        private string str_barcode = "";

        private List<string> view_list = new List<string>();

        public Shipping_Search(int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();
            int_Current_User_Id = int_C_User_id;

            //restrict access
            bol_write_access = bol_w_a;
            //check if testing or not

            //if (PF.Global.Global.bol_Testing == true)
            //{
            //    this.Text = "FP Process Factory - " + this.Text + " - Test Environment";
            //}
            //else
            //{
            //    this.Text = "FP Process Factory";
            //}
            //filter labels
            showAllLabel.Visible = false;
            showAllLabel.IsLink = true;
            showAllLabel.LinkBehavior = LinkBehavior.HoverUnderline;
            showAllLabel.Click += new EventHandler(showAllLabel_Click);

            statusStrip1.Cursor = Cursors.Default;
            statusStrip1.Items.AddRange(new ToolStripItem[] { filterStatusLabel, showAllLabel });
            this.Controls.AddRange(new Control[] { dataGridView1, statusStrip1 });
            //

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

            try
            {
                //set up outlook locations
                PF.Data.Outlook.Outlook.Folder_Name = PF.Common.Code.Outlook.SetUp_Location("PF-Contact");

                // Crashes here if Outlook is open
                Console.WriteLine("Outlook.Folder_Name: " + PF.Data.Outlook.Outlook.Folder_Name);

                PF.Data.Outlook.Outlook.Folder_Name_Location = PF.Data.Outlook.Outlook.Folder_Name_Location;
                //AddColumnsProgrammatically();
                populate_DataGridView1(bol_list_all);
            }
            catch (Exception ex)
            {
                // Crash
                logger.Log(LogLevel.Debug, "Exception: Shipping_Search - " + ex.Message);
                MessageBox.Show(ex.Message +
                    "\r\n" +
                    "This crash sometimes happens if Outlook is already open." + "\r\n" +
                    "Alternatively, it can also be caused by not having a valid MAC address.",
                    "Shipping_Search", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void binding()
        {
            // Continue only if the data source has been set.
            if (dataGridView1.DataSource == null)
            {
                return;
            }

            dataGridView1.Columns[0].HeaderText = "Order";
            dataGridView1.Columns[0].ReadOnly = true;

            dataGridView1.Columns[1].HeaderText = "Load Date";
            dataGridView1.Columns[1].Name = "Load_Date";
            dataGridView1.Columns[1].ReadOnly = true;

            dataGridView1.Columns[2].HeaderText = "Customer_Id";
            dataGridView1.Columns[2].Name = "Customer_Id";
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[2].Visible = false;

            dataGridView1.Columns[3].HeaderText = "Customer";
            dataGridView1.Columns[3].ReadOnly = true;

            dataGridView1.Columns[4].HeaderText = "Customer Order";
            dataGridView1.Columns[4].ReadOnly = true;

            dataGridView1.Columns[5].HeaderText = "Truck_Id";
            dataGridView1.Columns[5].ReadOnly = true;
            dataGridView1.Columns[5].Visible = false;

            dataGridView1.Columns[6].HeaderText = "Truck";
            dataGridView1.Columns[6].ReadOnly = true;

            dataGridView1.Columns[7].HeaderText = "Destination_Id";
            dataGridView1.Columns[7].ReadOnly = true;
            dataGridView1.Columns[7].Visible = false;

            dataGridView1.Columns[8].HeaderText = "Destination";
            dataGridView1.Columns[8].ReadOnly = true;

            dataGridView1.Columns[9].HeaderText = "Freight Docket";
            dataGridView1.Columns[9].ReadOnly = true;

            // Resize the columns to fit their contents.
            dataGridView1.AutoResizeColumns();
            // Add the AutoFilter header cell to each column.
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.HeaderCell = new DataGridViewAutoFilterColumnHeaderCell(col.HeaderCell);
            }
            Overdue_Orders();
            SizeColumns();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            Reset();
            // --------------------------------------------------------------------------
            //PF.Common.Code.KillAllWordInstances.KillAllWordProcesses();
            this.Close();
        }

        private void SendEmail()
        {
            //btn_Email_Click(this, EventArgs.Empty);

            //// Clear out old files
            //ClearOutTempFolder();   // BN

        }

        private void Generate_The_Email()
        {
            //logger.Log(LogLevel.Info, DecorateString("PF.Dispatch.Shipping Search", "btn_Email_Click", "(Email)"));
            DLR_Message = new DialogResult();   // Added 24/9/2015 to fix problem where a success view or print operation left the DLR_Message with a result of OK
            try
            {
                PF.Common.Code.SendEmail.attachment = new List<string>();

                string str_msg = "";
                if (chb_All.Checked == true)
                {
                    str_msg = str_msg + "You can only email the Certificate of Analysis and the Packing Slip" + Environment.NewLine;
                    chb_All.Checked = false;
                    chb_PC.Checked = false;
                    chb_ADDR.Checked = false;

                    chb_PS.Checked = true;
                    chb_COA.Checked = true;
                }
                else if (chb_ADDR.Checked == true || chb_PC.Checked == true)
                {
                    str_msg = str_msg + "You can only email the Certificate of Analysis and the Packing Slip" + Environment.NewLine;
                }

                if (str_msg.Length > 0)
                {
                    DLR_Message = MessageBox.Show(str_msg, "Process Factory - Dispatch(Search)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (DLR_Message != System.Windows.Forms.DialogResult.OK)
                {
                    #region ------------- defaults -------------

                    string Data = "";
                    Data = create_defaults();

                    string[] DataParts = Data.Split(':');

                    string str_delivery_name = DataParts[0];
                    int int_Loop_count = Convert.ToInt32(DataParts[1]);

                    #endregion ------------- defaults -------------

                    #region ------------- Certificate of Analysis -------------

                    // Checkbox COA and DialogResult
                    if (chb_COA.Checked == true && DLR_Message != DialogResult.OK)
                    {
                        // this line is common to View, Print and Email - Just a path statement pulled from the Database
                        PF.PrintLayer.Word.FilePath = PF.Common.Code.General.Get_Path("PF-TPPath");

                        // This call is where it branches off from the rest (View and Print)
                        // The battle is won at last - setting print to true generates the pdf files correctly,
                        // which now allows me to attach and send them. What a mission it has been to get to this point - 13-10-2015 BN
                        create_COA(str_delivery_name, "Email", true);

                        AttachAnyPdfsInTheTempFolder();

                        // Do not delete the pdfs until either the Send or Cancel button is pressed in Send_Email.cs.
                        // Will need to add in a delay to allow the send handler to do its thing

                        // Original single-file code
                        //PF.Common.Code.SendEmail.attachment.Add(PF.PrintLayer.Word.FilePath + "\\" + PF.PrintLayer.Word.FileName + ".PDF");

                    }

                    #endregion ------------- Certificate of Analysis -------------

                    #region ------------- print Packing Slip -------------

                    if (chb_PS.Checked == true && DLR_Message != DialogResult.OK)
                    {
                        create_Packing_Slip(int_Loop_count, str_delivery_name, "Email", true);

                        PF.Common.Code.SendEmail.attachment.Add(PF.PrintLayer.Word.FilePath + "\\" + PF.PrintLayer.Word.FileName + ".PDF");
                    }

                    #endregion ------------- print Packing Slip -------------

                    #region ------------- Send Email -------------

                    str_msg = str_msg + PF.Common.Code.Outlook.Check_Outlook(Convert.ToInt32(dataGridView1.CurrentRow.Cells["Customer_id"].Value.ToString()), "", "Email");

                    if (str_msg.Length > 0)
                    {
                        DLR_Message = MessageBox.Show(str_msg, "Process Factory - Dispatch (Search)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (DLR_Message != System.Windows.Forms.DialogResult.OK)
                    {
                        PF.Common.Code.Send_Email.Email_Subject = "Delivery Documentation";
                        PF.Common.Code.Send_Email.Email_Message = "Please find attached the documentation the order that was shipped from us on  " + Environment.NewLine
                            + dataGridView1.CurrentRow.Cells["Load_Date"].Value.ToString() + Environment.NewLine + "Thanks" + Environment.NewLine;
                        int int_result = PF.Common.Code.Outlook.Email(str_msg, true);

                        if (int_result == 0)
                        {
                            lbl_message.Text = lbl_message.Text + "Documents have been emailed to the Customer.";
                            Reset();
                        }
                    }

                    #endregion ------------- Send Email -------------
                }

            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex.Message);
                logger.Log(LogLevel.Error, ex.StackTrace);
            }
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            //logger.Log(LogLevel.Info, DecorateString("PF.Dispatch.Shipping Search", "btn_Print_Click", "(Print)"));
            ClearOutTempFolder();

            PF.PrintLayer.Word.FilePath = PF.Common.Code.General.Get_Path("PF-TPPath");
            Print_Selected_Data(true);

            // Print, then delete - NEW 13-10-2015 BN
            PrintAnyPdfsInTheTempFolder();

            Cursor.Current = Cursors.Default;

        }

        private void btn_View_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //ClearOutTempFolder();

            //logger.Log(LogLevel.Info, DecorateString("PF.Dispatch.Shipping Search", "btn_View_Click", "(View)"));

            view_list.Clear();
            //Reset();

            PF.PrintLayer.Word.FilePath = PF.Common.Code.General.Get_Single_System_Code("PF-TPPath");

            Print_Selected_Data(false);
            int int_return = 0;
            foreach (string view_file in view_list)
            {
                PF.PrintLayer.Word.StartWord_No_Template();
                PF.PrintLayer.Word.FileName = view_file;
                PF.PrintLayer.Word.OpenDoc();
                int_return = PF.PrintLayer.Word.SaveAsPdf();
                PF.PrintLayer.Word.CloseWord();

                if (int_return == 9)
                {
                    MessageBox.Show("This wont work with this version of Word", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    // All this may just be hunting a red herring, as I'm thinking the call to word is supplying the extension - BN 05-03-2015
                    //Console.WriteLine("Appending .pdf: " + PF.PrintLayer.Word.FilePath + "\\" + PF.PrintLayer.Word.FileName + ".pdf");
                    //logger.Log(LogLevel.Info, "Appending .pdf: " + PF.PrintLayer.Word.FilePath + "\\" + PF.PrintLayer.Word.FileName + ".pdf");
                    PF.Common.Code.General.Report_Viewer(PF.PrintLayer.Word.FilePath + "\\" + PF.PrintLayer.Word.FileName + ".pdf");
                }
            }

            //view_list.Clear();
            Cursor.Current = Cursors.Default;

            // Might need to kill all word instances, but not sure if that will work here
        }

        private void chb_ADDR_CheckedChanged(object sender, EventArgs e)
        {
            if (bol_chb_All == false)
            {
                this.chb_All.CheckedChanged -= new System.EventHandler(this.chb_All_CheckedChanged);
                chb_All.Checked = false;
                this.chb_All.CheckedChanged += new System.EventHandler(this.chb_All_CheckedChanged);
            }
            if (chb_ADDR.Checked == true && chb_COA.Checked == true && chb_PC.Checked == true && chb_PS.Checked == true)
            {
                chb_All.Checked = true;
            }
        }

        private void chb_All_CheckedChanged(object sender, EventArgs e)
        {
            switch ((sender as CheckBox).Name.ToString())
            {
                case "chb_PS":
                    chb_PS.Checked = true;
                    break;

                case "chb_COA":
                    chb_COA.Checked = true;
                    break;

                case "chb_PC":
                    chb_PC.Checked = true;
                    break;

                case "chb_ADDR":
                    chb_ADDR.Checked = true;
                    break;

                default:
                    bol_chb_All = true;
                    chb_ADDR.Checked = (sender as CheckBox).Checked;
                    chb_COA.Checked = (sender as CheckBox).Checked;
                    chb_PC.Checked = (sender as CheckBox).Checked;
                    chb_PS.Checked = (sender as CheckBox).Checked;
                    bol_chb_All = false;
                    break;
            }
        }

        private void chb_COA_CheckedChanged(object sender, EventArgs e)
        {
            if (bol_chb_All == false)
            {
                this.chb_All.CheckedChanged -= new System.EventHandler(this.chb_All_CheckedChanged);
                chb_All.Checked = false;
                this.chb_All.CheckedChanged += new System.EventHandler(this.chb_All_CheckedChanged);
            }
            if (chb_ADDR.Checked == true && chb_COA.Checked == true && chb_PC.Checked == true && chb_PS.Checked == true)
            {
                chb_All.Checked = true;
            }
        }

        private void chb_PC_CheckedChanged(object sender, EventArgs e)
        {
            if (bol_chb_All == false)
            {
                this.chb_All.CheckedChanged -= new System.EventHandler(this.chb_All_CheckedChanged);
                chb_All.Checked = false;
                this.chb_All.CheckedChanged += new System.EventHandler(this.chb_All_CheckedChanged);
            }
            if (chb_ADDR.Checked == true && chb_COA.Checked == true && chb_PC.Checked == true && chb_PS.Checked == true)
            {
                chb_All.Checked = true;
            }
        }

        private void chb_PS_CheckedChanged(object sender, EventArgs e)
        {
            if (bol_chb_All == false)
            {
                this.chb_All.CheckedChanged -= new System.EventHandler(this.chb_All_CheckedChanged);
                chb_All.Checked = false;
                this.chb_All.CheckedChanged += new System.EventHandler(this.chb_All_CheckedChanged);
            }
            if (chb_ADDR.Checked == true && chb_COA.Checked == true && chb_PC.Checked == true && chb_PS.Checked == true)
            {
                chb_All.Checked = true;
            }
        }

        private string check_tickboxs()
        {
            string str_msg = "";
            if (chb_ADDR.Checked == false && chb_COA.Checked == false &&
                chb_PC.Checked == false && chb_PS.Checked == false)
            {
                str_msg = str_msg + "Invalid Document selection. You need to select which document you wish to Print/Email/View." + Environment.NewLine;
                bol_chb_check_pass = false;
            }
            return str_msg;
        }

        private void cmb_Customers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (customer1.Customer_Id > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                populate_DataGridView1(bol_list_all);

                //if (customer1.Customer_Name != null && customer1.Customer_Name != string.Empty)
                //{
                //    logger.Log(LogLevel.Info, DecorateString("cmb_Customers_SelectedIndexChanged", customer1.Customer_Name, "(Changed)"));
                //}
                Cursor.Current = Cursors.Default;
            }
        }

        private void create_address_label(string str_delivery_name, bool bol_print)
        {
            try
            {
                string Data = "";
                Data = Data + str_delivery_name;
                Data = Data + ":" + PF.Data.Outlook.Contacts.Contact_Business_Address.ToString();
                Data = Data + ":" + PF.Data.Outlook.Contacts.Contact_Business_City.ToString();
                Cursor = Cursors.WaitCursor;
                //logger.Log(LogLevel.Info, DecorateString("PF.Dispatch.Shipping Search", "create_address_label", Data));

                //set printer
                //PF.PrintLayer.Word.Printer = PF.Common.Code.General.Get_Printer("Custom");
                PF.PrintLayer.Word.FileName = "SL" + dataGridView1.CurrentRow.Cells[0].Value.ToString();
                view_list.Add(PF.PrintLayer.Word.FileName);
                PF.PrintLayer.Sticky_Address_Label.Print(Data, bol_print);
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex.Message);
            }
        }

        private int create_COA(string str_delivery_name, string str_type, bool bol_print)
        {
            //logger.Log(LogLevel.Info, DecorateString("PF.Dispatch.Shipping Search", "create_COA", "Delivery Name: " + str_delivery_name + ", Type: " + str_type + ", Print: " + bol_print.ToString()));
            // Massive errors which were appearing here were caused by the broken Outlook links again.
            // Re-Link in Common -> Customers Form BN 2/02/2015
            try
            {
                int return_code = 0;
                string Data = "";

                Data = Data + dataGridView1.CurrentRow.Cells[0].Value.ToString();
                Data = Data + ":" + str_delivery_name;

                // Changed 19/11/2015 - BN
                if (str_delivery_name.StartsWith("There is no matching entry in your Outlook Contacts list."))
                {
                    Data = Data + ":" + "No Address Found.";
                }
                else
                {
                    Data = Data + ":" + PF.Data.Outlook.Contacts.Contact_Business_Address.ToString();
                }

                Data = Data + ":" + str_type;

                Cursor = Cursors.WaitCursor;
                //logger.Log(LogLevel.Info, DecorateString("PF.Dispatch.Shipping Search", "create_COA", Data));

                //set printer
                PF.PrintLayer.Word.Printer = PF.Common.Code.General.Get_Printer("A4");
                PF.PrintLayer.Word.FileName = "COA" + dataGridView1.CurrentRow.Cells[0].Value.ToString();

                return_code = PF.PrintLayer.Certificate_Of_Analysis.Print(Data, bol_print);

                view_list.AddRange(System.IO.Directory.GetFiles(PF.PrintLayer.Word.FilePath, "COA" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "*", System.IO.SearchOption.TopDirectoryOnly)
                     .Select(path => Path.GetFileNameWithoutExtension(path))
                     .ToArray());

                Cursor = Cursors.Default;
                return return_code;
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex.Message);
                return 0;
            }
        }

        private string create_defaults()
        {
            string str_msg = "";

            // Step over this line
            //[DebuggerStepThrough] would be nice

            DataSet ds_Get_Info = PF.Data.AccessLayer.PF_Orders.Get_Info(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
            DataRow dr_Get_Info;
            DateTime Load_Date = new DateTime();
            string str_Order_Num = "";
            string str_Freight_Docket = "";
            string str_delivery_name = "";
            int int_Loop_count = 0;
            //bool DataMatchFailed = false;   // BN 19/11/2015

            for (int i = 0; i <= Convert.ToInt32(Math.Floor(Convert.ToDecimal(ds_Get_Info.Tables[0].Rows.Count.ToString()) / 10)); i++)
            {
                int_Loop_count = Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString());

                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                Load_Date = Convert.ToDateTime(dr_Get_Info["Load_Date"].ToString());
                str_Order_Num = dr_Get_Info["Customer_Order"].ToString();
                str_Freight_Docket = dr_Get_Info["Freight_Docket"].ToString();

                DataSet ds = PF.Data.AccessLayer.PF_Customer.Get_Info(Convert.ToInt32(dr_Get_Info["Customer_Id"].ToString()));
                DataRow dr;
                string sCustomerName = "";  // Declare out here so I can get to it later
                

                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    dr = ds.Tables[0].Rows[j];
                    PF.Data.Outlook.Contacts.Contact_Details(dr["Outlook_Key"].ToString());
                    sCustomerName = dr["Name"].ToString();
                    string s = dr["Outlook_Key"].ToString();
                    Console.WriteLine("Customer Name:\t\t" + sCustomerName);
                    Console.WriteLine("Outlook Key:\t\t" + s);
                }
                ds.Dispose();

                // There's been an error present from day one - The first time you create a COA, everything is correct,
                // but if you print a second one, the name and address are printed as the first customer, and not the current one.

                // Name and everything else is correct up to this point  - BN 19/11/2015

                // What I need to do here and check that the customer name in the data (Which is always correct)
                // matches Contact_Company_Name - if it doesn't, that means theres no matching entry in the contact list.
                try
                {
                    if (PF.Data.Outlook.Contacts.Contact_Business_Address != null)
                    {
                        // Null reference exception
                        if (PF.Data.Outlook.Contacts.Contact_Business_Address.ToString() == "")
                        {
                            str_msg = str_msg + "No Street Address loaded in OutLook for this Customer. Please load an Address and try again." + Environment.NewLine;
                        }

                        // Yet again no checking for nulls
                        if (PF.Data.Outlook.Contacts.Contact_Company_Name == "" ||
                            PF.Data.Outlook.Contacts.Contact_Company_Name == null)
                        {
                            if (PF.Data.Outlook.Contacts.Contact_First_Name == "" && PF.Data.Outlook.Contacts.Contact_Last_Name == "")
                            {
                                str_msg = str_msg + "No Names (Company, First, Last) are loaded in Outlook for this Customer. PLease load a Name and try again." + Environment.NewLine;
                            }
                            else
                            {

                                // Insert the matching check here - If match is incorrect, change the delivery name to a brief error message
                                if (PF.Data.Outlook.Contacts.Contact_Company_Name != sCustomerName)
                                {
                                    str_delivery_name = "There is no matching entry in your Outlook Contacts list.";
                                    //DataMatchFailed = true;
                                }
                                else
                                {
                                    str_delivery_name = PF.Data.Outlook.Contacts.Contact_First_Name + " " + PF.Data.Outlook.Contacts.Contact_Last_Name;
                                }
                            }
                        }
                        else
                        {
                            // Insert the matching check here - If match is incorrect, change the delivery name to a brief error message
                            if (PF.Data.Outlook.Contacts.Contact_Company_Name != sCustomerName)
                            {
                                str_delivery_name = "There is no matching entry in your Outlook Contacts list.";
                                //DataMatchFailed = true;
                            }
                            else
                            {
                                str_delivery_name = PF.Data.Outlook.Contacts.Contact_First_Name + " " + PF.Data.Outlook.Contacts.Contact_Last_Name;
                            }

                            //str_delivery_name = PF.Data.Outlook.Contacts.Contact_Company_Name;
                        }

                        //str_msg = check_tickboxs();

                        if (str_msg.Length > 0)
                        {
                            DLR_Message = MessageBox.Show(str_msg, "Process Factory - Dispatch(Order - Print)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // This appears to be caused by the entryId's not matching up - these are the long strings of numbers.
                        // You probably need to go an re-link the tables in orer to get them to match.
                        logger.Log(LogLevel.Warn, "Contact_Business_Address is null - You probably need to re-link the customer ID's.");
                        MessageBox.Show("Contact_Business_Address is null\r\nYou probably need to re-link the customer ID's.\r\nGo to Common --> Grower --> Customer to relink.", "Shipping_Search - Create Defaults", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    logger.Log(LogLevel.Error, ex.Message);
                    MessageBox.Show(ex.ToString(), "Shipping_Search - Create Defaults", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            ds_Get_Info.Dispose();

            string Data = "";
            //if (DataMatchFailed == false)
            //{
                Data = Data + str_delivery_name;
                Data = Data + ":" + Convert.ToString(int_Loop_count);
            //}
            //else
            //{
            //    Data = Data + str_delivery_name;
            //    Data = Data + ":" + Convert.ToString(int_Loop_count);

            //}
            return Data;
        }

        private void create_Packing_Slip(int int_Loop_count, string str_delivery_name, string str_type, bool bol_print)
        {
            try
            {
                string Data = "";
                Data = Data + Convert.ToString(int_Loop_count);
                Data = Data + ":" + dataGridView1.CurrentRow.Cells[0].Value.ToString();
                Data = Data + ":" + str_delivery_name;
                Data = Data + ":" + PF.Data.Outlook.Contacts.Contact_Business_Address.ToString();
                Data = Data + ":" + str_type;

                Cursor = Cursors.WaitCursor;
                //logger.Log(LogLevel.Info, DecorateString("PF.Dispatch.Shipping Search", "create_Packing_Slip", Data));

                //set printer
                PF.PrintLayer.Word.Printer = PF.Common.Code.General.Get_Printer("A4");
                PF.PrintLayer.Word.FileName = "PS" + dataGridView1.CurrentRow.Cells[0].Value.ToString();
                view_list.Add(PF.PrintLayer.Word.FileName);
                PF.PrintLayer.Packing_Slip.Print(Data, bol_print);
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex.Message);
            }
        }

        // Configures the autogenerated columns, replacing their header
        // cells with AutoFilter header cells.
        private void dataGridView1_BindingContextChanged(object sender, EventArgs e)
        {
            binding();
        }

        // Updates the filter status label.
        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            String filterStatus = DataGridViewAutoFilterColumnHeaderCell.GetFilterStatus(dataGridView1);
            if (String.IsNullOrEmpty(filterStatus))
            {
                showAllLabel.Visible = false;
                filterStatusLabel.Visible = false;
            }
            else
            {
                showAllLabel.Visible = true;
                filterStatusLabel.Visible = true;
                filterStatusLabel.Text = filterStatus;
            }
        }

        //// Displays the drop-down list when the user presses
        //// ALT+DOWN ARROW or ALT+UP ARROW.
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up))
            {
                DataGridViewAutoFilterColumnHeaderCell filterCell =
                    dataGridView1.CurrentCell.OwningColumn.HeaderCell as DataGridViewAutoFilterColumnHeaderCell;
                if (filterCell != null)
                {
                    filterCell.ShowDropDownList();
                    e.Handled = true;
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // Excel keyboard shortcuts
            // ---------------------------------------------------------
            // Ctrl + A     = Select all cells
            // Alt + H,O,I  = Autosize columns
            // Ctrl + Shift + 2 = Format Time (select column first)
            // ---------------------------------------------------------

            // This was causing nullreference exceptions BN // 15/01/2014
            //if (dataGridView1.CurrentRow.Cells[0].Value != null)
            // It was preventing Sel from doing any printing (I think)

            if (dataGridView1.CurrentRow != null)
            {
                txt_Selected.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                //logger.Log(LogLevel.Info, DecorateString("dataGridView1_SelectionChanged", "Order: " + txt_Selected.Text, "(Changed)"));
            }
            //else
            //{
            //    MessageBox.Show("Value is null", "Problem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

        private void Enter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Print_Selected_Data(true);
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

        private void Overdue_Orders()
        {
            System.Windows.Forms.DataGridViewCellStyle boldStyle = new System.Windows.Forms.DataGridViewCellStyle();

            boldStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);

            DateTime dt_current = new DateTime();
            dt_current = DateTime.Now;

            int int_dt_current = Convert.ToInt32(dt_current.ToString("yyyyMMdd"));

            DataSet ds = PF.Data.AccessLayer.PF_Orders.Get_Overdue_Orders();
            DataRow dr;
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dr = ds.Tables[0].Rows[i];

                    DateTime dt_order = new DateTime();
                    dt_order = Convert.ToDateTime(dr["Load_Date"].ToString());
                    int int_dt_order = Convert.ToInt32(dt_order.ToString("yyyyMMdd"));

                    //do for each row
                    DataGridViewRow dr2;
                    for (int j = 0; j < dataGridView1.Rows.Count; j++)
                    {
                        dr2 = dataGridView1.Rows[j];

                        if (int_dt_order < int_dt_current)
                        {
                            if (dr2.Cells[0].Value.ToString() == dr["Order_Id"].ToString())
                            {
                                dr2.DefaultCellStyle = boldStyle;
                                //count the number of cells
                                for (int k = 0; k < dr2.Cells.Count; k++)
                                {
                                    dataGridView1[k, j].Style.BackColor = Color.LightCoral;
                                }
                            }
                        }
                        else
                        {
                            if (dr2.Cells[0].Value.ToString() == dr["Order_Id"].ToString())
                            {
                                dr2.DefaultCellStyle = boldStyle;
                                //count the number of cells
                                for (int k = 0; k < dr2.Cells.Count; k++)
                                {
                                    dataGridView1[k, j].Style.BackColor = Color.LightGreen;
                                }
                            }
                        }
                    }
                }
                ds.Dispose();
            }
            //MessageBox.Show("The Dataset returned null", "No Overdue Orders Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            logger.Log(LogLevel.Info, "The Dataset returned null; No Overdue Orders Found");
        }

        private void populate_DataGridView1(bool bol_list_all)
        {
            dataGridView1.Refresh();
            dataGridView1.DataSource = null;

            DataSet ds_Get_Info;

            if (customer1.Customer_Id > 0)
            {
                ds_Get_Info = PF.Data.AccessLayer.PF_Orders.Get_Order_Info_For_Cust(customer1.Customer_Id, bol_list_all);
            }
            else
            {
                ds_Get_Info = PF.Data.AccessLayer.PF_Orders.Get_Order_Info(bol_list_all);
            }

            BindingSource dataSource = new BindingSource(ds_Get_Info, "Table");

            dataGridView1.DataSource = dataSource;
            binding();
        }

        private void Print_Selected_Data(bool bol_print)
        {
            DataSet ds_Get_Info;
            DataRow dr_Get_Info;
            int return_code = 0;
            DLR_Message = System.Windows.Forms.DialogResult.None;

            #region ------------- defaults -------------

            string Data = "";
            Data = create_defaults();

            string[] DataParts = Data.Split(':');

            string str_delivery_name = DataParts[0];
            int int_Loop_count = Convert.ToInt32(DataParts[1]);

            #endregion ------------- defaults -------------

            #region ------------- Pallet Card -------------

            if (chb_PC.Checked == true && DLR_Message != DialogResult.OK)
            {
                //Gets the pallet details for the order
                ds_Get_Info = PF.Data.AccessLayer.PF_Pallet.Get_Info_for_Order(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
                {
                    dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                    str_barcode = dr_Get_Info["Barcode"].ToString();

                    Cursor = Cursors.WaitCursor;
                    //set printer
                    PF.PrintLayer.Word.Printer = PF.Common.Code.General.Get_Printer("A4");
                    PF.PrintLayer.Word.FileName = "PC" + str_barcode;

                    PF.PrintLayer.Pallet_Card.Print(str_barcode, bol_print, int_Current_User_Id);
                    Cursor = Cursors.Default;

                    view_list.Add(PF.PrintLayer.Word.FileName);
                }
                ds_Get_Info.Dispose();
            }

            #endregion ------------- Pallet Card -------------

            #region ------------- Certificate of Analysis -------------

            if (chb_COA.Checked == true && DLR_Message != DialogResult.OK)
            {
                //logger.Log(LogLevel.Info, DecorateString("PF.Dispatch.Shipping Search", "Print_btn: create_COA", "Delivery Name: " + str_delivery_name + ", Type: " + ", Print: " + bol_print.ToString()));

                return_code = create_COA(str_delivery_name, "Print", bol_print);
            }

            #endregion ------------- Certificate of Analysis -------------

            #region ------------- print Packing Slip -------------

            if (chb_PS.Checked == true && DLR_Message != DialogResult.OK)
            {
                create_Packing_Slip(int_Loop_count, str_delivery_name, "Print", bol_print);
            }

            #endregion ------------- print Packing Slip -------------

            #region ------------- Address Label -------------

            if (chb_ADDR.Checked == true && DLR_Message != DialogResult.OK)
            {
                create_address_label(str_delivery_name, bol_print);
            }

            #endregion ------------- Address Label -------------

            switch (return_code)
            {
                case 0:
                    if (bol_chb_check_pass == false)
                    {
                        lbl_message.Text = "";
                    }
                    else
                    {
                        lbl_message.Text = "Reports have been sent to the Printer";
                    }
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    break;

                case 96:
                    lbl_message.Text = "NO Product has been tipped on to this order";
                    lbl_message.ForeColor = System.Drawing.Color.Red;

                    break;
            }

            //Clean up
            Reset();
        }

        private void rdb_list_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_list_curr.Checked == true)
            {
                bol_list_all = false;
                logger.Log(LogLevel.Info, DecorateString("rdb_list_CheckedChanged", "Current: " + rdb_list_curr.Checked.ToString(), "(Changed)"));
            }
            else if (rdb_List_All.Checked == true)
            {
                bol_list_all = true;
                //logger.Log(LogLevel.Info, DecorateString("rdb_list_CheckedChanged", "All: " + rdb_List_All.Checked.ToString(), "(Changed)"));
            }
            populate_DataGridView1(bol_list_all);
        }

        private void Reset()
        {
            // Modified to hopefully reset everything and stop the viewing the same record bug 2nd time around

            // --------------------------------------------------------------------------
            // BN 23-04-2015
            // Shipping_Search isn't clearing the last search when you close the form

            //view_list.Clear();
            chb_All.Checked = false;
            //ClearOutTempFolder();

            lbl_message.Text = "";
        }

        private void ShippingSearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            // All region comments I've added for clarity (BN 12/08/2015)

            #region Old Notes BN 27-4-2015

            // Am wondering if the file variable is empty, what effect will a filename of ** have?
            // Is that a valid wildcard like *, or does it equate to something else?
            //
            // Also, if a test to see if the file is already open returns true, is it possible to forcibly close it
            // with no confirmations
            //

            #endregion Old Notes BN 27-4-2015

            ClearOutTempFolder();

        }

        private void ClearOutTempFolder()
        {
            // All region comments I've added for clarity (BN 12/08/2015)

            PF.PrintLayer.Word.FilePath = PF.Common.Code.General.Get_Path("PF-TPPath");
            DirectoryInfo di = new DirectoryInfo(PF.PrintLayer.Word.FilePath);
            if (di.Exists)
            {
                foreach (FileInfo fi in di.GetFiles())
                {
                    //System.IO.File.Move(fi.FullName, fi.FullName + ".delete");

                    //fi.Delete();
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

                        //using (new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.None)) { }
                        //FileStream fileStream = new FileStream(filename, FileMode.Truncate);

                        // This is the solution to files being locked by another process. BN 10/11/2015
                        System.GC.Collect();
                        System.GC.WaitForPendingFinalizers();

                        //fileStream.Unlock(0, fileStream.Length);
                        //fileStream.Close();
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

                        List<Process> procs = Common.Code.FileUtil.WhoIsLocking(filename);

                        if (procs.Count > 0) // System.Diagnostics.Process (WINWORD)
                        {
                            logger.Log(LogLevel.Warn, filename + " is locked open by: " + procs[0].ProcessName);

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
            //System.Threading.Thread.Sleep(1000);
            //if (di.Exists)
            //{
            //    foreach (FileInfo fi in di.GetFiles())
            //    {
            //        //System.IO.File.Move(fi.FullName, fi.FullName + ".delete");

            //        fi.Delete();
            //        //lst_filenames.Add(fi.FullName);
            //    }
            //}

            lst_filenames.Clear();
        }

        // Clears the filter when the user clicks the "Show All" link
        // or presses ALT+A.
        private void showAllLabel_Click(object sender, EventArgs e)
        {
            DataGridViewAutoFilterColumnHeaderCell.RemoveFilter(dataGridView1);
        }

        private void SizeAllColumns(Object sender, EventArgs e)
        {
            SizeColumns();
        }

        private void SizeColumns()
        {
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
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

        #endregion Log Code

        #region Methods to log UI events to the CSV file. BN 29/01/2015

        private void Control_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            //logger.Log(LogLevel.Info, DecorateString(cb.Name, cb.Checked.ToString(), "CheckedChanged"));
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
                //logger.Log(LogLevel.Info, DecorateString(b.Name, b.Text, "Click"));
            }
        }

        private void Control_NudValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            //logger.Log(LogLevel.Info, DecorateString(nud.Name, nud.Text, "NudValueChanged"));
        }

        private void Control_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            //logger.Log(LogLevel.Info, DecorateString(cb.Name, cb.Text, "SelectedValueChanged"));
        }

        private void Control_Validated(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            //logger.Log(LogLevel.Info, DecorateString(t.Name, t.Text, "Validated"));
        }

        private void Control_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;
            //logger.Log(LogLevel.Info, DecorateString(dtp.Name, dtp.Text, "ValueChanged"));
        }

        private void CustomerControl_CustomerChanged(object sender, EventArgs e)
        {
            PF.Utils.UserControls.Customer cust = (PF.Utils.UserControls.Customer)sender;
            //logger.Log(LogLevel.Info, DecorateString(cust.Name, cust.Customer_Name, "TextChanged"));
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
        private void Shipping_Search_KeyDown(object sender, KeyEventArgs e)
        {
            // Note that the KeyPreview property must be set to true for this to work
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        #endregion Methods to log UI events to the CSV file. BN 29/01/2015

        public static void PrintAnyPdfsInTheTempFolder()
        {
            try
            {

                // This is new, now that I've finally got the COA print to pdf working - BN 30/10/2015

                // Pull the temp path from the database
                string TempPath = PF.Common.Code.General.Get_Single_System_Code("PF-TPPath");


                //string path = Application.StartupPath;
                //Settings = new PhantomCustomSettings();
                //Settings.SettingsPath = Path.Combine(path, "FP.Phantom.config");
                //Settings.EncryptionKey = "phantomKey";
                //Settings.Load();
                //TempPath = Settings.Path_Local_PDF_Files;

                DirectoryInfo di = new DirectoryInfo(TempPath);
                foreach (FileInfo fi in di.GetFiles())
                {
                    if (fi.Extension == ".pdf")
                    {
                        SendToPrinter(TempPath, fi.Name);

                        // This is the solution to files being locked by another process. BN 10/11/2015
                        System.GC.Collect();
                        System.GC.WaitForPendingFinalizers();

                        // Delete the file
                        fi.Delete();
                    }
                }

                
            }
            catch (Exception ex)
            {
                Common.Code.DebugStacktrace.StackTrace(ex);

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
                if (fi.Extension == ".pdf")
                {
                    //PF.Common.Code.SendEmail.attachment.Add(PF.PrintLayer.Word.FilePath + "\\" + PF.PrintLayer.Word.FileName + ".PDF");
                    PF.Common.Code.SendEmail.attachment.Add(TempPath + "\\" + fi.Name);

                    // Delete the file
                    //fi.Delete();
                }
            }

                #endregion
            }
            catch (Exception ex)
            {
                Common.Code.DebugStacktrace.StackTrace(ex);

            }

        }
        private static void SendToPrinter(string FilePath, string FileName)
        {
            try
            { 
            ProcessStartInfo info = new ProcessStartInfo();
            info.Verb = "print";
            info.FileName = FilePath + "\\" + FileName;     // @"c:\output.pdf";
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;

            Process p = new Process();
                
            p.StartInfo = info;
            p.Start();
                // See if the GC trick works here ?
            p.WaitForInputIdle();
            System.Threading.Thread.Sleep(3000);
            if (false == p.CloseMainWindow())
                p.Kill();
            }
            catch (Exception ex)
            {

                logger.Log(LogLevel.Error, ex.Message);
                Common.Code.DebugStacktrace.StackTrace(ex);

            }

        }

        private void buttonNewView_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            ClearOutTempFolder();

            // Duplicate of send email, without the email part

            //logger.Log(LogLevel.Info, DecorateString("PF.Dispatch.Shipping Search", "btn_Email_Click", "(Email)"));
            DLR_Message = new DialogResult();   // Added 24/9/2015 to fix problem where a success view or print operation left the DLR_Message with a result of OK
            try
            {
                //PF.Common.Code.SendEmail.attachment = new List<string>();

                //string str_msg = "";
                //if (chb_All.Checked == true)
                //{
                //    str_msg = str_msg + "You can only email the Certificate of Analysis and the Packing Slip" + Environment.NewLine;
                //    chb_All.Checked = false;
                //    chb_PC.Checked = false;
                //    chb_ADDR.Checked = false;

                //    chb_PS.Checked = true;
                //    chb_COA.Checked = true;
                //}
                //else if (chb_ADDR.Checked == true || chb_PC.Checked == true)
                //{
                //    str_msg = str_msg + "You can only email the Certificate of Analysis and the Packing Slip" + Environment.NewLine;
                //}

                //if (str_msg.Length > 0)
                //{
                //    DLR_Message = MessageBox.Show(str_msg, "Process Factory - Dispatch(Search)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}

                //if (DLR_Message != System.Windows.Forms.DialogResult.OK)
                //{
                    #region ------------- defaults -------------

                    string Data = "";
                    Data = create_defaults();

                    string[] DataParts = Data.Split(':');

                    string str_delivery_name = DataParts[0];
                    int int_Loop_count = Convert.ToInt32(DataParts[1]);

                    #endregion ------------- defaults -------------

                    #region ------------- Certificate of Analysis -------------

                    // Checkbox COA and DialogResult
                    if (chb_COA.Checked == true && DLR_Message != DialogResult.OK)
                    {
                        // this line is common to View, Print and Email - Just a path statement pulled from the Database
                        PF.PrintLayer.Word.FilePath = PF.Common.Code.General.Get_Path("PF-TPPath");

                        // This call is where it branches off from the rest (View and Print)
                        // The battle is won at last - setting print to true generates the pdf files correctly,
                        // which now allows me to attach and send them. What a mission it has been to get to this point - 13-10-2015 BN
                        create_COA(str_delivery_name, "Print", false);

                    // open the pdfs
                    ViewAnyPdfsInTheTempFolder();
                    //PF.Common.Code.General.Report_Viewer(PF.PrintLayer.Word.FilePath + "\\" + view_file);

                    //AttachAnyPdfsInTheTempFolder();

                    // Do not delete the pdfs until either the Send or Cancel button is pressed in Send_Email.cs.
                    // Will need to add in a delay to allow the send handler to do its thing

                    // Original single-file code
                    //PF.Common.Code.SendEmail.attachment.Add(PF.PrintLayer.Word.FilePath + "\\" + PF.PrintLayer.Word.FileName + ".PDF");

                }

                #endregion ------------- Certificate of Analysis -------------

                #region ------------- print Packing Slip -------------

                if (chb_PS.Checked == true && DLR_Message != DialogResult.OK)
                    {
                        create_Packing_Slip(int_Loop_count, str_delivery_name, "Print", false);

                        //PF.Common.Code.SendEmail.attachment.Add(PF.PrintLayer.Word.FilePath + "\\" + PF.PrintLayer.Word.FileName + ".PDF");
                    }

                    #endregion ------------- print Packing Slip -------------

                    #region ------------- Send Email -------------

                    //str_msg = str_msg + PF.Common.Code.Outlook.Check_Outlook(Convert.ToInt32(dataGridView1.CurrentRow.Cells["Customer_id"].Value.ToString()), "", "Email");

                    //if (str_msg.Length > 0)
                    //{
                    //    DLR_Message = MessageBox.Show(str_msg, "Process Factory - Dispatch (Search)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}

                    //if (DLR_Message != System.Windows.Forms.DialogResult.OK)
                    //{
                    //    PF.Common.Code.Send_Email.Email_Subject = "Delivery Documentation";
                    //    PF.Common.Code.Send_Email.Email_Message = "Please find attached the documentation the order that was shipped from us on  " + Environment.NewLine
                    //        + dataGridView1.CurrentRow.Cells["Load_Date"].Value.ToString() + Environment.NewLine + "Thanks" + Environment.NewLine;
                    //    int int_result = PF.Common.Code.Outlook.Email(str_msg, true);

                    //    if (int_result == 0)
                    //    {
                    //        lbl_message.Text = lbl_message.Text + "Documents have been emailed to the Custoemr.";
                    //        Reset();
                    //    }
                    //}

                    #endregion ------------- Send Email -------------
                //}
                // Clear out old files
                //ClearOutTempFolder();   // BN

            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex.Message);
            }

            Cursor.Current = Cursors.Default;

        }

        public static void ViewAnyPdfsInTheTempFolder()
        {
            try
            {
                // Pull the temp path from the database
                string TempPath = PF.Common.Code.General.Get_Single_System_Code("PF-TPPath");

                //// This is new, now that I've finally got the COA print to pdf working
                //string TempPath = string.Empty;

                //#region Pull the temp path from the database
                //string path = Application.StartupPath;
                //Settings = new PhantomCustomSettings();
                //Settings.SettingsPath = Path.Combine(path, "FP.Phantom.config");
                //Settings.EncryptionKey = "phantomKey";
                //Settings.Load();
                //TempPath = Settings.Path_Local_PDF_Files;
                //logger.Log(LogLevel.Info, "TempPath: " + TempPath);

                DirectoryInfo di = new DirectoryInfo(TempPath);
                foreach (FileInfo fi in di.GetFiles())
                {
                    logger.Log(LogLevel.Info, "Filename: " + fi.Name);

                    if (fi.Extension == ".pdf")
                    {
                        //PF.Common.Code.General.Report_Viewer(PF.PrintLayer.Word.FilePath + "\\" + view_file);
                        logger.Log(LogLevel.Info, "PDF Found: " + TempPath + "\\" + fi.Name);

                        PF.Common.Code.General.Report_Viewer(TempPath + "\\" + fi.Name);

                        //SendToPrinter(TempPath, fi.Name);
                        // Delete the file
                        //fi.Delete();
                    }
                }

                //#endregion
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex.Message);
                MessageBox.Show(ex.Message);
           
            }
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            ViewAnyPdfsInTheTempFolder();
        }

        private void buttonCheckFilesInTheTempFolder_Click(object sender, EventArgs e)
        {
            // BN 23/10/2015
            PF.Common.Code.OpenPdfFileLocations.OpenTempPdfFileLocation();
        }

        private void btn_Email_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            Generate_The_Email();

            // Clear out old files
            ClearOutTempFolder();   // BN

            Cursor.Current = Cursors.Default;
        }

        private void buttonDeleteFiles_Click(object sender, EventArgs e)
        {
            // Clear out old files
            ClearOutTempFolder();   // BN
        }
    }
}