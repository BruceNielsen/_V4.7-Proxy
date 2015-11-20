//#define FPWebServer
//#undef FPWebServer

//#define PhantomWebServer
//#undef PhantomWebServer

// Usage
//#if (FPWebServer)
//        Console.WriteLine("FPWebServer is enabled.");
//#endif

//#if (PhantomWebServer)
//     Console.WriteLine("PhantomWebServer is enabled.");
//#endif
// -------------------------- Output: FPWebServer is enabled.

#region Imports (23)

// I need to investigate this soon, for Outlook - BN 8/9/2015
// Decorate the class with this - Does it stop Outlook complaining about security?
// [PermissionSet(SecurityAction.Demand, Name="FullTrust")]
// ------------------------------------------------------------------------------------------------------------------
// BN - Answer: 12/11/2015 - Nope - Just go into the Trust Center settings in Outlook on the client machines, 
// and disable the programmatic access crap.
//
// Outlook --> Options --> Trust Centre --> Trust Centre Settings --> Programmatic Access --> 
// Check: Never warn me about suspicious activity (not recommended) --> Ok
//
// You must be logged on as Administrator first though.
// In the case of a Windows 10 box, that approach doesn't work, as Chris found out.
// The only answer there is to run a registry script.
//
// Explanation: In all variants of Windows up to 10, you can log on as (run the program as) administrator, 
// and set the option which is normally disabled for users - What it does is log on as the user, 
// but with elevated permissions.
//
// In the case of Windows 10, you always log on as Administrator, and any changes you make only affect 
// the Admin account and not the users, which is not the desired outcome.
//
// Note - There is an answer to all this, but my understanding at this time (on a programming level) is it 
// involves certificates and a whole bunch of research which I doubt is warranted,
// given that this is an app for one local company and not a world audience.
//
// ----------------------------------------------------------------
// When setting up a new SQL (2014) installation:
// Create a jobs login off the Security --> Logins branch - off the root
// Disable the password policy
// Login name is jobs, password is jobs
// Default database id Process_Factory
// Server role is public
// User Mapping is Process_Factory
// Securables - Leave as is
// Status - Connect = Grant, Login = Enabled
// ----------------------------------------------------------------
// Inside the Databases --> Process_Factory branch --> Security --> Users branch
// Create a new user (jobs), and all you need to do is under the jobs account
// is grant ownership (Double-click on jobs, click membership, then db_owner and ok etc.
// ----------------------------------------------------------------
// Note: SQL login is set to Windows Authentication, but the database is set to mixed-mode.
// ----------------------------------------------------------------

using AutoUpdaterDotNET;
using PF.CustomSettings;
using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Management;

//using SystemMonitor;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using TreeViewSerialization;

#endregion Imports (23)

namespace PF.Menu
{
    /// <summary>
    /// Class which contains all the Main Menu logic
    /// </summary>
    public partial class Main_Menu : Form
    {

        //public static string LoggedOnUserName
        //{
        //    get;
        //    set;
        //}

        #region Members of Main_Menu (20)

        private const int WM_SYSCOMMAND = 0x112;
        private const int MF_STRING = 0x0;
        private const int MF_SEPARATOR = 0x800;
        private int SYSMENU_ABOUT_ID = 0x1;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static PhantomCustomSettings Settings;
        private static Reversible.UndoRedoSession undoRedoSession = new Reversible.UndoRedoSession();

        // These entries are from when I was decorating the log file - I stopped doing that a while back
        //private static int nodeIndex = 0;
        //private string openPad = " --- [ ";
        //private string closePad = " ] --- ";
        //private string intro = "--->   { ";
        //private string outro = " }   <---";

        private string MaxTreeFileSize = string.Empty;
        private static int int_User_id = 0;
        private DataSet ds_Get_Info;
        private DataRow dr_Get_Info;
        private static string str_date_combo = "";
        private static int int_prod_combo = 0;
        private System.Windows.Forms.Timer Clock;
        private int int_current_WO_Id = 0;

        #region SystemMonitor

        //private SystemMonitor.DataBar dataBarCPU;
        //private SystemMonitor.DataBar dataBarMemP;
        //private SystemMonitor.DataBar dataBarMemV;
        //private SystemMonitor.DataChart dataChartDiskR;
        //private SystemMonitor.DataChart dataChartDiskW;
        //private SystemMonitor.DataChart dataChartNetI;
        //private SystemMonitor.DataChart dataChartNetO;
        //private SystemMonitor.DataChart dataChartCPU;
        //private SystemMonitor.DataChart dataChartMem;

        //private ArrayList _listDiskR = new ArrayList();
        //private ArrayList _listDiskW = new ArrayList();
        //private ArrayList _listNetI = new ArrayList();
        //private ArrayList _listNetO = new ArrayList();

        //private ArrayList _listCPU = new ArrayList();
        //private ArrayList _listMem = new ArrayList();

        //private SystemData sd = new SystemData();
        //private Size _sizeOrg;
        //private Size _size;

        #endregion SystemMonitor
        // This is used in Main_Menu_Resize to detect when the form is maximised
        //FormWindowState LastWindowState;

        #endregion Members of Main_Menu (20)

        #region Constructors of Main_Menu (1)

        /// <summary>
        /// Main user interface method
        /// </summary>
        public Main_Menu()
        {
            InitializeComponent();

            #region SystemMonitor

            //_size = Size;
            //_sizeOrg = Size;

            //labelModel.Text = sd.QueryComputerSystem("manufacturer") + ", " + sd.QueryComputerSystem("model");
            //textBoxProcessor.Text = sd.QueryEnvironment("%PROCESSOR_IDENTIFIER%");
            //labelNames.Text = "User: " + sd.QueryComputerSystem("username");  //sd.LogicalDisk();

            //UpdateData();
            //timerSysMon.Interval = 1000;
            //timerSysMon.Start();

            //string host = Dns.GetHostName();
            //IPHostEntry ip = Dns.GetHostEntry(host);
            ////Console.WriteLine(ip.AddressList[0].ToString());
            //this.labelIpAddressValue.Text = ip.AddressList[0].ToString();

            // First get the host name of local machine.
            string strHostName = Dns.GetHostName();
            this.labelMachineNameValue.Text = strHostName;

            // Then using host name, get the IP address list..
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;

            for (int i = 0; i < addr.Length; i++)
            {
                //Console.WriteLine("IP Address {0}: {1} ", i, addr[i].ToString());
                this.labelIpAddressValue.Text = addr[i].ToString() + ",";
            }
            labelIpAddressValue.Text = labelIpAddressValue.Text.TrimEnd(',');  // Remove trailing comma

            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            this.labelUserNameValue.Text = userName;

            this.labelDayOfYearValue.Text = DateTime.Now.DayOfYear.ToString();
            this.labelDayOfWeekValue.Text = DateTime.Now.DayOfWeek.ToString();

            ShowSystemUptime();

            #endregion SystemMonitor

            //int id = 0;     // The id of the hotkey.
            //RegisterHotKey(this.Handle, id, (int)KeyModifier.Shift, Keys.A.GetHashCode());       // Register Shift + A as global hotkey.

            //logger.Log(LogLevel.Info, LogCode("Main_Menu.cs: Form Loading."));

            #region Load CustomSettings

            #region Get "Application.StartupPath" folder

            string path = Application.StartupPath;

            #endregion Get "Application.StartupPath" folder

            // Initialize settings
            Settings = new PhantomCustomSettings();
            Settings.SettingsPath = Path.Combine(path, "FP.Phantom.config");
            Settings.EncryptionKey = "phantomKey";

            if (!File.Exists(Settings.SettingsPath))
            {
                Settings.Save();
                logger.Log(LogLevel.Info, LogCode("Default Phantom Settings file created"));
            }
            // Load settings - Normally in Form_Load
            Settings.Load();

            this.selectPrinterToolStripMenuItem1.Text = "Selected Printer: " + Settings.Printer_Name;

            // Make sure this is loaded now
            this.MaxTreeFileSize = Settings.MaxTreeFileSizeInMegabytes;

            //logger.Log(LogLevel.Info, LogCode("Settings file opened."));

            #endregion Load CustomSettings

            var result = PF.Utils.Security.Logon.Execute(Environment.UserName, "Computer");

            // This is where the main menu text is selected
            if (PF.Global.Global.bool_Testing == true)
            {
                this.Text = "FP - Test Environment";
                //logger.Log(LogLevel.Info, LogCode("FP - Test Environment - Startup"));
            }
            else
            {
                this.Text = "FP - Production Environment";
                //logger.Log(LogLevel.Info, LogCode("FP - Production Environment - Startup"));
            }

            if (Settings.ShowWordWarningMessage == true)
            {
                this.labelWarningAboutWordDocuments.Visible = true;
            }
            else
            {
                this.labelWarningAboutWordDocuments.Visible = false;
            }

            if (result.Result == DialogResult.Cancel)
            {
                logger.Log(LogLevel.Info, LogCode("FP - Logon Cancelled"));
                this.Close();
            }
            else
            {
                // clean up old files
                try
                {
                    //logger.Log(LogLevel.Info, LogCode("Calling: Delete_After_Printing."));

                    PF.Common.Code.General.Delete_After_Printing("", "*.*");
                }
                catch (Exception ex)
                {
                    logger.Log(LogLevel.Debug, ex.Message);
                }
                int_User_id = Convert.ToInt32(result.Field1.ToString());

                if (Environment.UserName.ToString().ToUpper() == "PROCESS")
                {
                    //logger.Log(LogLevel.Info, LogCode("Environment.UserName.ToString().ToUpper == PROCESS, Setting bol_Timer_Msg = false."));
                    PF.Global.Global.bool_Timer_Msg = false;
                }
                else
                {
                    //logger.Log(LogLevel.Info, LogCode("Environment.UserName.ToString().ToUpper != PROCESS, Setting bol_Timer_Msg = true."));
                    PF.Global.Global.bool_Timer_Msg = true;
                }

                // Count down Message for Robin J

                if (PF.Global.Global.bool_Timer_Msg == true)
                {
                    //logger.Log(LogLevel.Info, LogCode("Showing personal user messages."));

                    DateTime dt1 = DateTime.Parse("24/05/2014");
                    DateTime dt2 = DateTime.Now;
                    //if (int_User_id == 1)
                    //{
                    //    if (dt1.Date > dt2.Date)
                    //    {
                    //        Form frm_djb = new RobinJ("David has ", "23/05/2014 17:00:00");
                    //        frm_djb.ShowDialog();
                    //    }
                    //    Form frm_rbj = new RobinJ("Robin has ", "31/07/2017 17:00:00");
                    //    frm_rbj.ShowDialog();
                    //}
                    if (int_User_id == 3)
                    {
                        if (dt1.Date > dt2.Date)
                        {
                            Form frm_djb = new RobinJ("David has", "23/05/2014 17:00:00");
                            frm_djb.ShowDialog();
                        }
                        Form frm_rbj = new RobinJ("Robin has ", "31/07/2017 17:00:00");
                        frm_rbj.ShowDialog();
                    }
                    Clock = new System.Windows.Forms.Timer();
                    Clock.Interval = 1000;
                    Clock.Start();
                    Clock.Tick += new EventHandler(Timer_Tick);
                }

                //logger.Log(LogLevel.Info, LogCode("Calling: hide_Tabs."));
                hide_Tabs();

                //logger.Log(LogLevel.Info, LogCode("Calling: populate_Date_Combo."));
                populate_Date_Combo();

                //logger.Log(LogLevel.Info, LogCode("Calling: populate_Baseload_Comboboxes."));
                populate_Baseload_Comboboxes();

                //logger.Log(LogLevel.Info, LogCode("Calling: populate_Reports."));
                populate_Reports();

                // New 12/05/2015 - BN - Loads the groupedComboBox
                populate_Location();
                // New 12/05/2015 - BN - Loads the cuedComboBoxes (Have a hint text preloaded)
                populate_Types();

                //// New 13/05/2015 - BN - Hide the ribbon at last
                //// Note - You can't just call the click event and expect it to work;
                //// you have to set the CheckState first
                //hideRibbonToolStripMenuItem.CheckState = CheckState.Checked;
                //hideRibbonToolStripMenuItem_Click(this, EventArgs.Empty);

                toolTipMainMenu.SetToolTip(this.buttonDispatchBaseLoadAddNew, "After adding a new item," + Environment.NewLine + "press the Reset button" + Environment.NewLine + "to refresh the BaseLoad List.");

                // This is used in Main_Menu_Resize to detect when the form is maximised
                //LastWindowState = FormWindowState.Minimized;
            }
        }

        #endregion Constructors of Main_Menu (1)

        #region Methods of Main_Menu (265)

        private void Accounting_Costs_Rates_Click(object sender, EventArgs e)
        {
            historyAdd("Accounting - Costs - Rates");
            logger.Log(LogLevel.Info, DecorateString("Accounting - Costs", "Accounting_Costs_Rates_Click", "(Rates)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Accounts.Accounts_Maintenance("PF_A_Rates", int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.Accounts.Accounts_Maintenance (Accounting --> Costs --> Rates)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Accounting_Invoicing_FruitPurchase_Click(object sender, EventArgs e)
        {
            historyAdd("Accounting - Invoicing - Fruit Purchase");
            logger.Log(LogLevel.Info, DecorateString("Accounting - Invoicing", "Accounting_Invoicing_FruitPurchase_Click", "(Fruit Purchase)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Accounts.Fruit_Purchase(int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.Accounts.Fruit_Purchase (Accounting --> Invoicing --> Fruit Purchase)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Accounting_Invoicing_Intent_Order_Click(object sender, EventArgs e)
        {
            historyAdd("Accounting - Intent - Order");
            logger.Log(LogLevel.Info, DecorateString("Accounting - Intent", "Accounting_Invoicing_Intent_Order_Click", "(Order)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Accounts.Intended_Orders(int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.Accounts.Intended_Orders (Accounting --> Intent --> Order)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Accounting_Invoicing_OrderInvoiceLink_Click(object sender, EventArgs e)
        {
            historyAdd("Accounting - Invoicing - Order/Invoice Link");
            logger.Log(LogLevel.Info, DecorateString("Accounting - Invoicing", "Accounting_Invoicing_OrderInvoiceLink_Click", "(Order/Invoice Link)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Accounts.Order_Invoice_Link(int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.Accounts.Order_Invoice_Link (Accounting --> Invoicing --> Order/Invoice Link)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Accounting_Invoicing_PaymentReceived_Click(object sender, EventArgs e)
        {
            historyAdd("Accounting - Invoicing - Payments Received");
            logger.Log(LogLevel.Info, DecorateString("Accounting - Invoicing", "Accounting_Invoicing_PaymentReceived_Click", "(Payments Recieved)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Accounts.Payment_Received(int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.Accounts.Payment_Received (Accounting --> Invoicing --> Payments Received)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Accounting_Invoicing_Sales_Click(object sender, EventArgs e)
        {
            historyAdd("Accounting - Invoicing - Sales");
            logger.Log(LogLevel.Info, DecorateString("Accounting - Invoicing", "Accounting_Invoicing_Sales_Click", "(Sales)"));

            Cursor = Cursors.WaitCursor;

            PF.Accounts.Invoicing_Sales frm = new PF.Accounts.Invoicing_Sales(int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            //Form frm = new PF.Accounts.Invoicing_Sales(int_User_id, Convert.ToBoolean((sender as RibbonButton).Tag.ToString()));
            frm.Text = "PF.Accounts.Invoicing_Sales (Accounting --> Invoicing --> Sales)";

            frm.HistoryUpdated += frm_HistoryUpdated;   // Attach to the EventHandler

            //frm.TopMost = true; // 21/10/2015 BN
            // 31-3-2015 BN
            frm.Show(this); // Changed this to add the Form parameter in the constructor - same thing, but a bit different

            //frm.Show();

            Cursor = Cursors.Default;
        }

        private void Accounting_Invoicing_Search_Click(object sender, EventArgs e)
        {
            historyAdd("Accounting - Invoicing - Search");
            logger.Log(LogLevel.Info, DecorateString("Accounting - Invoicing", "Accounting_Invoicing_Search_Click", "(Search)"));

            Cursor = Cursors.WaitCursor;

            // This was really difficult to find; just declaring this as type Form meant it was hiding the eventhandler. BN
            //Form frm = new PF.Accounts.Invoice_Search(int_User_id, Convert.ToBoolean((sender as RibbonButton).Tag.ToString()));

            PF.Accounts.Invoice_Search frm = new PF.Accounts.Invoice_Search(int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.Accounts.Invoice_Search (Accounting --> Invoicing --> Search)";

            frm.HistoryUpdated += frm_HistoryUpdated;   // Attach to the EventHandler

            // 31-3-2015 BN
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show(this); // Changed this to add the Form parameter in the constructor - same thing, but a bit different
                                //frm.Show();

            Cursor = Cursors.Default;
        }

        private void Accounting_Invoicing_WorkOrder_Click(object sender, EventArgs e)
        {
            historyAdd("Accounting - Invoicing - Work Order");
            logger.Log(LogLevel.Info, DecorateString("Accounting - Invoicing", "Accounting_Invoicing_WorkOrder_Click", "(Work Order)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Accounts.Invoicing_WO(int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.Accounts.Invoicing_WO (Accounting --> Invoicing --> Work Order)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Accounting_Sales_Discounts_Click(object sender, EventArgs e)
        {
            historyAdd("Accounting - Sales - Discounts");
            logger.Log(LogLevel.Info, DecorateString("Accounting - Sales", "Accounting_Sales_Discounts_Click", "(Discounts)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Accounts.Sales_Rates("PF_A_Customer_Sales_Discount", int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.Accounts.Sales_Rates (Accounting --> Sales --> Discounts)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Accounting_Sales_Rates_Click(object sender, EventArgs e)
        {
            historyAdd("Accounting - Sales - Rates");
            logger.Log(LogLevel.Info, DecorateString("Accounting - Sales", "Accounting_Sales_Rates_Click", "(Rates)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Accounts.Sales_Rates("PF_A_Customer_Sales_Rates", int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.Accounts.Sales_Rates (Accounting --> Sales --> Rates)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void accountingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.ribbon1.ActiveTab = this.ribbonTab_Accounting;
            this.tabControlMain.SelectedTab = tabPageAccounting;
        }

        private void accountingToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            //this.ribbon1.ActiveTab = this.ribbonTab_Accounting;
            //this.tabControlMain.SelectedTab = tabPageAccounting;
            //this.toolStripStatusLabelActive.Text = this.rbt_Accounting.Text + " tab selected.";
        }

        private void addNewMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Home_Message_AddNew_Click(this, EventArgs.Empty);
            Home_Message_AddNew_Click(this.buttonHomeAddNewMessage, EventArgs.Empty);
        }

        private void addNewReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Reports_Maintenance_AddNewReport_Click(this.ribbonButton_Reports_Maintenance_AddNewReport, EventArgs.Empty);
            Reports_Maintenance_AddNewReport_Click(this.buttonReportsMaintenanceAddNewReport, EventArgs.Empty);
        }

        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Dispatch_BaseLoad_AddNew_Click(this.ribbonButton_Dispatch_BaseLoad_AddNew, EventArgs.Empty);
            Dispatch_BaseLoad_AddNew_Click(this.buttonDispatchBaseLoadAddNew, EventArgs.Empty);
        }

        private void addOutputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Dispatch_BaseLoad_AddOutput_Click(this.ribbonButton_Dispatch_BaseLoad_AddOutput, EventArgs.Empty);
            Dispatch_BaseLoad_AddOutput_Click(this.buttonDispatchBaseLoadAddOutput, EventArgs.Empty);
        }

        private void addProductToOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Dispatch_Scanning_AddProductToOrder_Click(this.ribbonButton_Dispatch_Scanning_AddProductToOrder, EventArgs.Empty);

            Dispatch_Scanning_AddProductToOrder_Click(this.buttonDispatchScanningAddProductToOrder, EventArgs.Empty);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool AppendMenu(IntPtr hMenu, int uFlags, int uIDNewItem, string lpNewItem);

        private void areaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Maintenance_Cleaning_Area_Click(this.ribbonButton_Maintenance_Cleaning_Area, EventArgs.Empty);
            Maintenance_Cleaning_Area_Click(this.buttonPfMaintenanceCleaningArea, EventArgs.Empty);
        }

        private void baseloadListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cueComboBoxDispatchBaseLoadList.Focus();

            // Special Case
            //rbcmb_BaseLoad_DropDownItemClicked
            //(this, EventArgs.Empty);

            //MessageBox.Show("Not yet implemented.\r\nPlease use the Ribbon for the time being.",
            //    "Work in Progress", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void brandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Common_Types_Material_Brand_Click(this.ribbonButton_Common_Types_Material_Brand, EventArgs.Empty);
            Common_Types_Material_Brand_Click(this.cueComboBoxCommonTypesMaterials, EventArgs.Empty);
        }

        /// <summary>
        /// FP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void brotherHL2040SeriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Printer_Name = "Brother HL-2040 series";
            this.propertyGridDebug.Refresh();
            Settings.Save();
        }

        private void Button_Common_Material_UpdateMaterial_Click(object sender, EventArgs e)
        {
            historyAdd("Common - Material - Update Material");
            logger.Log(LogLevel.Info, DecorateString("Common - Material", "Button_Common_Material_UpdateMaterial_Click", "(Update Material)"));

            try
            {
                //Convert.ToDecimal(ribbonText_Common_Material_MaterialNumber.TextBoxText);

                // New 12/05/2015 - BN - This seems like a pointless call as it's being done in the next step anyway
                //Convert.ToDecimal(hintedTextBox_Common_Material_MaterialNumber.Text);
                // Aftera bit of exploring, it appears it was designed to throw an exception at the first hint of a problem.
                // I've modified it a bit to at least see if it's empty or not.

                // If it is, it will be caught by an exception.
                DataSet ds_Get_Info = null;

                //Type type = sender.GetType();
                //if(type == typeof(RibbonButton))
                //{
                //    if (ribbonText_Common_Material_MaterialNumber.TextBoxText != string.Empty)
                //    {
                //        ds_Get_Info = PF.Data.AccessLayer.CM_Material.Get_Info(Convert.ToDecimal(ribbonText_Common_Material_MaterialNumber.TextBoxText));
                //    }
                //}
                //else if (type == typeof(Button))
                //{
                if (hintedTextBox_Common_Material_MaterialNumber.Text != string.Empty)
                {
                    ds_Get_Info = PF.Data.AccessLayer.CM_Material.Get_Info(Convert.ToDecimal(hintedTextBox_Common_Material_MaterialNumber.Text));
                }
                //}

                //DataSet ds_Get_Info = PF.Data.AccessLayer.CM_Material.Get_Info(Convert.ToDecimal(ribbonText_Common_Material_MaterialNumber.TextBoxText));
                DataRow dr_Get_Info;

                if (Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()) > 0)
                {
                    for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
                    {
                        dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                        if (Convert.ToBoolean(dr_Get_Info["PF_Active_Ind"].ToString()) == true)
                        {
                            PF.Data.AccessLayer.CM_Material.update_active(Convert.ToInt32(dr_Get_Info["Material_Id"].ToString()), false, int_User_id);
                        }
                        else
                        {
                            PF.Data.AccessLayer.CM_Material.update_active(Convert.ToInt32(dr_Get_Info["Material_Id"].ToString()), true, int_User_id);
                        }
                    }
                    //lbl_message.Text = "Active Status on Material Number: " + ribbonText_Common_Material_MaterialNumber.TextBoxText + " has been updated";
                    //lbl_message.ForeColor = System.Drawing.Color.Blue;

                    labelTabControlMainBottomStatus.Text = "Active Status on Material Number: " + hintedTextBox_Common_Material_MaterialNumber.Text + " has been updated";
                    labelTabControlMainBottomStatus.ForeColor = System.Drawing.Color.Blue;
                }
                else
                {
                    //lbl_message.Text = "Invalid Material Number. Please enter a valid number";
                    //lbl_message.ForeColor = System.Drawing.Color.Magenta;

                    labelTabControlMainBottomStatus.Text = "Invalid Material Number. Please enter a valid number";
                    labelTabControlMainBottomStatus.ForeColor = System.Drawing.Color.Magenta;
                }
            }
            catch
            {
                lbl_message.Text = "Exception: Invalid Material Number. Please enter a valid number";
                lbl_message.ForeColor = System.Drawing.Color.Red;

                labelTabControlMainBottomStatus.Text = "Exception: Invalid Material Number. Please enter a valid number";
                labelTabControlMainBottomStatus.ForeColor = System.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// New handler for handling the new run button - This is for replacing the ribbon completely - BN 11/05/2015
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Reports_SSRS_Run_Click(object sender, EventArgs e)
        {
            historyAdd("Reports - SSRS - Run");
            logger.Log(LogLevel.Info, DecorateString("Reports - SSRS", "ribbonButton_Reports_SSRS_Run_Click", "(Run)"));

            if (cueComboBoxReportsSSRSReportList.SelectedItem == null)
            {
                MessageBox.Show("You must select a report from the drop down list", "Process Factory - Complete", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Cursor = Cursors.WaitCursor;
                // Because the new cueComboBox has an array of ComboboxItems, we need to get at the underlying data a bit differently.
                ComboboxItem ci = cueComboBoxReportsSSRSReportList.SelectedItem as ComboboxItem;

                PF.Common.Code.General.Report_Viewer(ci.Value);
                //PF.Common.Code.General.Report_Viewer(cueComboBoxReportsSSRSReportList.SelectedValue.ToString());
                Cursor = Cursors.Default;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //using (Reversible.Transaction txn = undoRedoSession.Begin("Add item"))
            //{
            //    // Increment node index reversibly
            //    Reversible.Transaction.AddPropertyChange(v => nodeIndex = v, nodeIndex, nodeIndex + 1);
            //    nodeIndex++;

            //    TreeNode currentNode = treeViewUndoRedo.SelectedNode;
            //    if (currentNode == null)
            //    {
            //        treeViewUndoRedo.Nodes.Add_Reversible(new TreeNode("Node" + nodeIndex));
            //        //treeViewUndoRedo.Nodes.Add_Reversible(new TreeNode("Node"));
            //    }
            //    else
            //    {
            //        currentNode.Nodes.Add_Reversible(new TreeNode("Node" + nodeIndex));
            //        //currentNode.Nodes.Add_Reversible(new TreeNode("Node"));
            //    }
            //    txn.Commit();
            //}
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            //using (Reversible.Transaction txn = undoRedoSession.Begin("Clear items"))
            //{
            //    treeViewUndoRedo.Nodes.Clear_Reversible();

            //    txn.Commit();
            //}
        }

        private void buttonRedo_Click(object sender, EventArgs e)
        {
            undoRedoSession.Redo();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            //if (treeViewUndoRedo.SelectedNode != null)
            //{
            //    using (Reversible.Transaction txn = undoRedoSession.Begin("Remove item"))
            //    {
            //        nodeIndex++;
            //        treeViewUndoRedo.Nodes.RemoveAt_Reversible(treeViewUndoRedo.SelectedNode.Index);

            //        txn.Commit();
            //    }
            //}
        }

        private void buttonUndo_Click(object sender, EventArgs e)
        {
            undoRedoSession.Undo();
        }

        /// <summary>
        /// New 13/05/2015 - BN
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="example"></param>
        /// <returns></returns>
        public static T CastByExample<T>(object input, T example)
        {
            // Cast an Anonymous Type in Object and retrieve one Field
            // http://stackoverflow.com/questions/6901506/cast-an-anonymous-types-in-object-and-retrieve-one-field

            return (T)input;
        }

        private void chemicalsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_ProcessFactory_WorkOrders_Chemicals_Click(this.ribbonButton_ProcessFactory_WorkOrders_Chemicals, EventArgs.Empty);
            ProcessFactory_WorkOrders_Chemicals_Click(this.buttonProcessFactoryWorkOrdersChemicals, EventArgs.Empty);
        }

        private void chemicalsToolStripMenuItem_Maintenance_Click(object sender, EventArgs e)
        {
            //ribbonButton_Maintenance_Types_Chemicals_Click(this.ribbonButton_Maintenance_Types_Chemicals, EventArgs.Empty);
            Maintenance_Types_Chemicals_Click(this.buttonPfMaintenanceTypesChemicals, EventArgs.Empty);
        }

        private void citiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Common_Location_OffSite_Cities_Click(this.ribbonButton_Common_Location_OffSite_Cities, EventArgs.Empty);
            Common_Location_OffSite_Cities_Click(this.groupedComboBoxCommonLocation, EventArgs.Empty);
        }

        private void cleaningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_ProcessFactory_Cleaning_Cleaning_Click(this.ribbonButton_ProcessFactory_Cleaning_Cleaning, EventArgs.Empty);

            ProcessFactory_Cleaning_Cleaning_Click(this.buttonProcessFactoryCleaningCleaning, EventArgs.Empty);
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxDebug.Clear();
        }

        private void Common_ESP_ESP_Click(object sender, EventArgs e)
        {
            historyAdd("Common - ESP - ESP");
            logger.Log(LogLevel.Info, DecorateString("Common - ESP", "Common_ESP_ESP_Click", "(ESP)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Utils.Common.Common_Maintenance("CM_ESP", int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.Utils.Common.Common Maintenance (Common --> ESP --> ESP)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Common_Fruit_Type_Click(object sender, EventArgs e)
        {
            historyAdd("Common - Fruit - Type");
            logger.Log(LogLevel.Info, DecorateString("Common - Fruit", "Common_Fruit_Type_Click", "(Type)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Utils.Common.Common_Maintenance("CM_Fruit_Type", int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.Utils.Common.Common Maintenance (Common --> Fruit --> Type)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Common_Fruit_Variety_Click(object sender, EventArgs e)
        {
            historyAdd("Common - Fruit - Variety");
            logger.Log(LogLevel.Info, DecorateString("Common - Fruit", "Common_Fruit_Variety_Click", "(Variety)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Utils.Common.Fruit_Variety(int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.Utils.Common.Common Maintenance (Common --> Fruit --> Variety)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Common_Groups_ProductGroups_Click(object sender, EventArgs e)
        {
            historyAdd("Common - Groups - Product Groups");
            logger.Log(LogLevel.Info, DecorateString("Common - Groups", "Common_Groups_ProductGroups_Click", "(Product Groups)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Utils.Common.Common_Maintenance("CM_Product_Group", int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.Utils.Common.Common Maintenance (Common --> Groups --> Product Groups)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Common_Grower_Customer_Click(object sender, EventArgs e)
        {
            historyAdd("Common - Grower - Customer");
            logger.Log(LogLevel.Info, DecorateString("Common - Grower", "Common_Grower_Customer_Click", "(Customer)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Utils.Common.PF_Customer(int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.Utils.Common.Common Maintenance (Common --> Grower --> Customer)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Common_Grower_Grower_Click(object sender, EventArgs e)
        {
            historyAdd("Common - Grower - Grower");
            logger.Log(LogLevel.Info, DecorateString("Common - Grower", "Common_Grower_Grower_Click", "(Grower)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Utils.Common.Grower(int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.Utils.Common.Common Maintenance (Common --> Grower --> Grower)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Common_Grower_Orchardist_Click(object sender, EventArgs e)
        {
            historyAdd("Common - Grower - Orchardist");
            logger.Log(LogLevel.Info, DecorateString("Common - Grower", "Common_Grower_Orchardist_Click", "(Orchardist)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Utils.Common.Common_Maintenance2("CM_Orchardist", int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.Utils.Common Maintenance2 (Common --> Grower --> Orchardist)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Common_Grower_Trader_Click(object sender, EventArgs e)
        {
            historyAdd("Common - Grower - Trader");
            logger.Log(LogLevel.Info, DecorateString("Common - Grower", "Common_Grower_Trader_Click", "(Trader)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Utils.Common.Common_Maintenance2("CM_Trader", int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.Utils.Common Maintenance2 (Common --> Grower --> Trader)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Common_Location_OffSite_Cities_Click(object sender, EventArgs e)
        {
            historyAdd("Common - Location - OffSite - Cities");
            logger.Log(LogLevel.Info, DecorateString("Common - Location - Offsite", "Common_Location_OffSite_Cities_Click", "(Cities)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Utils.Common.Common_Maintenance("CM_Cities", int_User_id, Convert.ToBoolean((sender as GroupedComboBox).Tag.ToString()));
            frm.Text = "PF.Utils.Common.Common_Maintenance (Common --> Location --> Cities)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Common_Location_OffSite_Trucks_Click(object sender, EventArgs e)
        {
            historyAdd("Common - Location - OffSite - Trucks");
            logger.Log(LogLevel.Info, DecorateString("Common - Location - Offsite", "Common_Location_OffSite_Trucks_Click", "(Trucks)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Utils.Common.Common_Maintenance("CM_Truck", int_User_id, Convert.ToBoolean((sender as GroupedComboBox).Tag.ToString()));
            frm.Text = "PF.Utils.Common.Common_Maintenance (Common --> Location --> Trucks)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Common_Location_OnSite_Location_Click(object sender, EventArgs e)
        {
            historyAdd("Common - Location - OnSite - Location");
            logger.Log(LogLevel.Info, DecorateString("Common - Location - Onsite", "Common_Location_OnSite_Location_Click", "(Location)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Utils.Common.Common_Maintenance("CM_Location", int_User_id, Convert.ToBoolean((sender as GroupedComboBox).Tag.ToString()));
            frm.Text = "PF.Utils.Common.Common_Maintenance (Common --> Location --> Location)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Common_Material_Material_Click(object sender, EventArgs e)
        {
            historyAdd("Common - Material - Material");
            logger.Log(LogLevel.Info, DecorateString("Common - Material", "Common_Material_Material_Click", "(Material)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Utils.Common.Material(int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.Utils.Common.Common Maintenance (Common --> Material --> Material)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Common_Types_Material_Brand_Click(object sender, EventArgs e)
        {
            historyAdd("Common - Types - Material - Brand");
            logger.Log(LogLevel.Info, DecorateString("Common - Types - Material", "Common_Types_Material_Brand_Click", "(Brand)"));
            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Utils.Common.Common_Maintenance("CM_Brand", int_User_id, Convert.ToBoolean((sender as Tools.CueComboBox).Tag.ToString()));
            frm.Text = "PF.Utils.Common.Common_Maintenance (Common --> Types --> Material --> Brand)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Common_Types_Material_Count_Click(object sender, EventArgs e)
        {
            historyAdd("Common - Types - Material - Count");
            logger.Log(LogLevel.Info, DecorateString("Common - Types - Material", "ribbonButton_Common_Types_Material_Count_Click", "(Count)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Utils.Common.Common_Maintenance("CM_Count", int_User_id, Convert.ToBoolean((sender as Tools.CueComboBox).Tag.ToString()));
            frm.Text = "PF.Utils.Common.Common_Maintenance (Common --> Types --> Material --> Count)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Common_Types_Material_Grade_Click(object sender, EventArgs e)
        {
            historyAdd("Common - Types - Material - Grade");
            logger.Log(LogLevel.Info, DecorateString("Common - Types - Material", "Common_Types_Material_Grade_Click", "(Grade)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Utils.Common.Common_Maintenance("CM_Grade", int_User_id, Convert.ToBoolean((sender as Tools.CueComboBox).Tag.ToString()));
            frm.Text = "PF.Utils.Common.Common_Maintenance (Common --> Types --> Material --> Grade)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Common_Types_Material_GrowingMethod_Click(object sender, EventArgs e)
        {
            historyAdd("Common - Types - Material - Growing Method");
            logger.Log(LogLevel.Info, DecorateString("Common - Types - Material", "Common_Types_Material_GrowingMethod_Click", "(Growing Method)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Utils.Common.Common_Maintenance("CM_Growing_Method", int_User_id, Convert.ToBoolean((sender as Tools.CueComboBox).Tag.ToString()));
            frm.Text = "PF.Utils.Common.Common_Maintenance (Common --> Types --> Material --> Growing Method)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Common_Types_Material_MarketAttribute_Click(object sender, EventArgs e)
        {
            historyAdd("Common - Types - Material - Market Attribute");
            logger.Log(LogLevel.Info, DecorateString("Common - Types - Material", "Common_Types_Material_MarketAttribute_Click", "(Market Attribute)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Utils.Common.Common_Maintenance("CM_Market_Attribute", int_User_id, Convert.ToBoolean((sender as Tools.CueComboBox).Tag.ToString()));
            frm.Text = "PF.Utils.Common.Common_Maintenance (Common --> Types --> Material --> Market Attribute)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Common_Types_Material_Size_Click(object sender, EventArgs e)
        {
            historyAdd("Common - Types - Material - Size");
            logger.Log(LogLevel.Info, DecorateString("Common - Types - Material", "Common_Types_Material_Size_Click", "(Size)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Utils.Common.Common_Maintenance("CM_Size", int_User_id, Convert.ToBoolean((sender as Tools.CueComboBox).Tag.ToString()));
            frm.Text = "PF.Utils.Common.Common_Maintenance (Common --> Types --> Material --> Size Treatment)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Common_Types_Material_Treatment_Click(object sender, EventArgs e)
        {
            historyAdd("Common - Types - Material - Treatment");
            logger.Log(LogLevel.Info, DecorateString("Common - Types - Material", "Common_Types_Material_Treatment_Click", "(Treatment)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Utils.Common.Common_Maintenance("CM_Treatment", int_User_id, Convert.ToBoolean((sender as Tools.CueComboBox).Tag.ToString()));
            frm.Text = "PF.Utils.Common.Common_Maintenance (Common --> Types --> Material --> Treatment)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Common_Types_Types_Defect_Click(object sender, EventArgs e)
        {
            historyAdd("Common - Types - Types - Defect");
            logger.Log(LogLevel.Info, DecorateString("Common - Types - Types", "Common_Types_Types_Defect_Click", "(Defect)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Utils.Common.Common_Maintenance("CM_Defect", int_User_id, Convert.ToBoolean((sender as Tools.CueComboBox).Tag.ToString()));
            frm.Text = "PF.Utils.Common.Common_Maintenance (Common --> Types --> Type --> Defect)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Common_Types_Types_DefectClass_Click(object sender, EventArgs e)
        {
            historyAdd("Common - Types - Types - Defect Class");
            logger.Log(LogLevel.Info, DecorateString("Common - Types - Types", "Common_Types_Types_DefectClass_Click", "(Defect Class)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Utils.Common.Common_Maintenance("CM_Defect_Class", int_User_id, Convert.ToBoolean((sender as Tools.CueComboBox).Tag.ToString()));
            frm.Text = "PF.Utils.Common.Common_Maintenance (Common --> Types --> Type --> Defect Class)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Common_Types_Types_PackTypes_Click(object sender, EventArgs e)
        {
            historyAdd("Common - Types - Types - Pack Types");
            logger.Log(LogLevel.Info, DecorateString("Common - Types - Types", "Common_Types_Types_PackTypes_Click", "(Pack Types)"));
            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Utils.Common.Common_Maintenance("CM_Pack_Type", int_User_id, Convert.ToBoolean((sender as Tools.CueComboBox).Tag.ToString()));
            frm.Text = "PF.Utils.Common.Common_Maintenance (Common --> Types --> Type --> Pack Types)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Common_Types_Types_Pallets_Click(object sender, EventArgs e)
        {
            historyAdd("Common - Types - Types - Pallets");
            logger.Log(LogLevel.Info, DecorateString("Common - Types - Types", "Common_Types_Types_Pallets_Click", "(Pallets)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Utils.Common.Common_Maintenance("CM_Pallet_Type", int_User_id, Convert.ToBoolean((sender as Tools.CueComboBox).Tag.ToString()));
            frm.Text = "PF.Utils.Common.Common_Maintenance (Common --> Types --> Type --> Pallets)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Common_Types_Types_Storage_Click(object sender, EventArgs e)
        {
            historyAdd("Common - Types - Types - Storage");
            logger.Log(LogLevel.Info, DecorateString("Common - Types - Types", "Common_Types_Types_Storage_Click", "(Storage)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Utils.Common.Common_Maintenance("CM_Storage", int_User_id, Convert.ToBoolean((sender as Tools.CueComboBox).Tag.ToString()));
            frm.Text = "PF.Utils.Common.Common_Maintenance (Common --> Types --> Type --> Storage)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void commonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.ribbon1.ActiveTab = this.ribbonTab_Common;
            this.tabControlMain.SelectedTab = tabPageCommon;
        }

        private void commonToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            //this.ribbon1.ActiveTab = this.ribbonTab_Common;
            //this.tabControlMain.SelectedTab = tabPageCommon;
            //this.toolStripStatusLabelActive.Text = this.rbt_Common.Text + " tab selected.";
        }

        private void completeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_ProcessFactory_CompleteWorkOrders_Complete_Click(this.ribbonButton_ProcessFactory_CompleteWorkOrders_Complete, EventArgs.Empty);

            ProcessFactory_CompleteWorkOrders_Complete_Click(this.buttonProcessFactoryCompleteWorkOrdersComplete, EventArgs.Empty);
        }

        private void completeToolStripMenuItem_Dispatch_Click(object sender, EventArgs e)
        {
            //ribbonButton_Dispatch_BaseLoad_Complete_Click(this.ribbonButton_Dispatch_BaseLoad_Complete, EventArgs.Empty);
            Dispatch_BaseLoad_Complete_Click(this.buttonDispatchBaseLoadComplete, EventArgs.Empty);
        }

        private void consumableMaintenanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Consumables_Maintenance_Maintenance_Click(this.ribbonButton_Consumables_Maintenance_Maintenance, EventArgs.Empty);
            Consumables_Maintenance_Maintenance_Click(this.buttonConsumablesMaintenance, EventArgs.Empty);
        }

        private void Consumables_Maintenance_Holding_Click(object sender, EventArgs e)
        {
            historyAdd("Consumables - Maintenance - Holding");
            logger.Log(LogLevel.Info, DecorateString("Consumables - Maintenance", "Consumables_Maintenance_Holding_Click", "(Holding)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.StockControl.Stock_Holding(int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.StockControl.Stock_Holding (Consumables --> Maintenance --> Holding)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Consumables_Maintenance_Maintenance_Click(object sender, EventArgs e)
        {
            historyAdd("Consumables - Maintenance - Maintenance");
            logger.Log(LogLevel.Info, DecorateString("Consumables - Maintenance", "Consumables_Maintenance_Maintenance_Click", "(Maintenance)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.StockControl.Stock_Control_Maintenance("PF_Stock_Item", int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.StockControl.Stock_Control_Maintenance (Consumables --> Maintenance --> Maintenance)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Consumables_Maintenance_UsageAdjustment_Click(object sender, EventArgs e)
        {
            historyAdd("Consumables - Maintenance - Usage Adjustment");
            logger.Log(LogLevel.Info, DecorateString("Consumables - Maintenance", "Consumables_Maintenance_UsageAdjustment_Click", "(Usage Adjustment)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.StockControl.Stock_Used(int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.StockControl.Stock_Used (Consumables --> Maintenance --> Usage Adjustment)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void consumablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.ribbon1.ActiveTab = this.ribbonTab_Consumables;
            this.tabControlMain.SelectedTab = tabPageConsumables;
        }

        private void consumablesToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            //this.ribbon1.ActiveTab = this.ribbonTab_Consumables;
            //this.tabControlMain.SelectedTab = tabPageConsumables;
            //this.toolStripStatusLabelActive.Text = this.rbt_Stock.Text + " tab selected.";
        }

        private void copyAllTextToTheClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string rtbText = richTextBoxDebug.Text;
            Clipboard.SetText(rtbText, TextDataFormat.Text);
        }

        private void countToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Common_Types_Material_Count_Click(this.ribbonButton_Common_Types_Material_Count, EventArgs.Empty);
            Common_Types_Material_Count_Click(this.cueComboBoxCommonTypesMaterials, EventArgs.Empty);
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Dispatch_Orders_Create_Click(this.ribbonButton_Dispatch_Orders_Create, EventArgs.Empty);

            Dispatch_Orders_Create_Click(this.buttonDispatchOrdersCreate, EventArgs.Empty);
        }

        private void createViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_ProcessFactory_WorkOrders_CreateView_Click(this.ribbonButton_ProcessFactory_WorkOrders_CreateView, EventArgs.Empty);
            ProcessFactory_WorkOrders_CreateView_Click(this.buttonProcessFactoryWorkOrdersCreateView, EventArgs.Empty);
        }

        /// <summary>
        /// New 12/05/2015 - BN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cueComboBoxCommonTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Make sure we don't recursively fire the other cueComboBox accidentally
            if (cueComboBoxCommonTypes.SelectedIndex != -1)
            {
                cueComboBoxCommonTypesMaterials.SelectedIndex = -1;

                // Logic to show whichever form was selected

                #region cueComboBoxCommonTypes.SelectedItem

                switch (cueComboBoxCommonTypes.SelectedItem.ToString())
                {
                    case "Defect":
                        {
                            // Call new method
                            Common_Types_Types_Defect_Click(cueComboBoxCommonTypes, EventArgs.Empty);
                            break;
                        }
                    case "Defect Class":
                        {
                            // Call new method
                            Common_Types_Types_DefectClass_Click(cueComboBoxCommonTypes, EventArgs.Empty);
                            break;
                        }
                    case "Pack Types":
                        {
                            // Call new method
                            Common_Types_Types_PackTypes_Click(cueComboBoxCommonTypes, EventArgs.Empty);
                            break;
                        }
                    case "Pallets":
                        {
                            // Call new method
                            Common_Types_Types_Pallets_Click(cueComboBoxCommonTypes, EventArgs.Empty);
                            break;
                        }
                    case "Storage":
                        {
                            // Call new method
                            Common_Types_Types_Storage_Click(cueComboBoxCommonTypes, EventArgs.Empty);
                            break;
                        }
                }

                #endregion cueComboBoxCommonTypes.SelectedItem

                // Deselect the entry
                cueComboBoxCommonTypes.SelectedIndex = -1;
                // Set the focus away from the cueComboBox so it reads 'Select a Type...' again
                tabPageCommon.Select();
            }
        }

        /// <summary>
        /// New 12/05/2015 - BN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cueComboBoxCommonTypesMaterials_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Make sure we don't recursively fire the other cueComboBox accidentally
            if (cueComboBoxCommonTypesMaterials.SelectedIndex != -1)
            {
                cueComboBoxCommonTypes.SelectedIndex = -1;

                // Logic to show whichever form was selected

                #region cueComboBoxCommonTypesMaterials.SelectedItem

                switch (cueComboBoxCommonTypesMaterials.SelectedItem.ToString())
                {
                    case "Brand":
                        {
                            // Call new method
                            Common_Types_Material_Brand_Click(cueComboBoxCommonTypesMaterials, EventArgs.Empty);
                            break;
                        }
                    case "Count":
                        {
                            // Call new method
                            Common_Types_Material_Count_Click(cueComboBoxCommonTypesMaterials, EventArgs.Empty);
                            break;
                        }
                    case "Grade":
                        {
                            // Call new method
                            Common_Types_Material_Grade_Click(cueComboBoxCommonTypesMaterials, EventArgs.Empty);
                            break;
                        }
                    case "Growing Method":
                        {
                            // Call new method
                            Common_Types_Material_GrowingMethod_Click(cueComboBoxCommonTypesMaterials, EventArgs.Empty);
                            break;
                        }
                    case "Market Attribute":
                        {
                            // Call new method
                            Common_Types_Material_MarketAttribute_Click(cueComboBoxCommonTypesMaterials, EventArgs.Empty);
                            break;
                        }
                    case "Size Treatment":
                        {
                            // Call new method
                            Common_Types_Material_Size_Click(cueComboBoxCommonTypesMaterials, EventArgs.Empty);
                            break;
                        }
                    case "Treatment":
                        {
                            // Call new method
                            Common_Types_Material_Treatment_Click(cueComboBoxCommonTypesMaterials, EventArgs.Empty);
                            break;
                        }
                }

                #endregion cueComboBoxCommonTypesMaterials.SelectedItem

                // Deselect the entry
                cueComboBoxCommonTypesMaterials.SelectedIndex = -1;

                // Set the focus away from the cueComboBox so it reads 'Select a Material...' again
                tabPageCommon.Select();
            }
        }

        private void cueComboBoxReportsSSRSReportList_DropDown(object sender, EventArgs e)
        {
            //historyAdd("Reports - SSRS - Report List: " + ribbonCombo_Reports_SSRS_ReportList.SelectedItem.Text);
            if (cueComboBoxReportsSSRSReportList.SelectedItem != null)
            {
                historyAdd("Reports - SSRS - Report List: " + cueComboBoxReportsSSRSReportList.SelectedItem.ToString());
            }
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Common_Grower_Customer_Click(this.ribbonButton_Common_Grower_Customer, EventArgs.Empty);
            Common_Grower_Customer_Click(this.buttonCommonGrowerCustomer, EventArgs.Empty);
        }

        private void dateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cueComboBoxProcessFactoryWorkOrdersSelectDate.Focus();

            //// Special Case
            ////rcmb_Date_DropDownItemClicked
            ////(this, EventArgs.Empty);

            ////MessageBox.Show("Not yet implemented.\r\nPlease use the Ribbon for the time being.",
            ////    "Work in Progress", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //// Show new input form
            //// Load ComboBoxes

            //// On OK, pass selected value back
            //// On Cancel, deselect and close

            ////InputWorkOrder iwo = new InputWorkOrder();
            //iwo = new InputWorkOrder();
            //// Subscribe to event
            //iwo.GP_Changed += iwo_GP_Changed;

            //// Populate the ComboBoxes

            //foreach (object o in ribbonCombo_ProcessFactory_WorkOrders_Date.DropDownItems)
            //{
            //    RibbonButton rb = (RibbonButton)o;
            //    iwo.comboBoxDate.Items.Add(rb.Text);
            //}

            //foreach (object o in ribbonCombo_ProcessFactory_WorkOrders_Product.DropDownItems)
            //{
            //    RibbonButton rb = (RibbonButton)o;
            //    iwo.comboBoxProduct.Items.Add(rb.Text);
            //}

            //foreach (object o in ribbonCombo_ProcessFactory_WorkOrders_WorkOrder.DropDownItems)
            //{
            //    RibbonButton rb = (RibbonButton)o;
            //    iwo.comboBoxWorkOrder.Items.Add(rb.Text);
            //}

            //iwo.ShowDialog();

            //// Unsubscribe from event
            //iwo.GP_Changed -= iwo_GP_Changed;
            //iwo.Dispose();
            //iwo = null;
        }

        /// <summary>
        /// Formats the output to the debug log so that the relevant bits stand out amongst the machine-generated stuff.
        /// Refers to UI elements that the user interacts with.
        /// </summary>
        /// <param name="name">Name of the calling UI element</param>
        /// <param name="input">Name of the calling Tab</param>
        /// <param name="action">What it is doing</param>
        /// <returns></returns>
        private string DecorateString(string name, string input, string action)
        {
            string output = string.Empty;

            // Simplify the output 10/08/2015 BN
            //output = intro + name + openPad + input + closePad + action + outro;
            // -------------------------------------------------------------------

            // Massively simplify what we see in the debug output, now that
            // we're almost finished
            output = input;
            return output;
        }

        private void defectClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Common_Types_Types_DefectClass_Click(this.ribbonButton_Common_Types_Types_DefectClass, EventArgs.Empty);
            Common_Types_Types_DefectClass_Click(this.cueComboBoxCommonTypes, EventArgs.Empty);
        }

        private void defectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Common_Types_Types_Defect_Click(this.ribbonButton_Common_Types_Types_Defect, EventArgs.Empty);
            Common_Types_Types_Defect_Click(this.cueComboBoxCommonTypes, EventArgs.Empty);
        }

        /// <summary>
        /// Handles the Click event of the deleteLogcsvToolStripMenuItem control. 27/02/2015
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void deleteLogcsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If log.csv.exist then delete
            if (File.Exists(Application.StartupPath + @"\logs\log.csv"))
            {
                try
                {
                    File.Delete(Application.StartupPath + @"\logs\log.csv");
                    MessageBox.Show(Application.StartupPath + @"\logs\log.csv" + " - Deleted!", "Successful Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Handle exception++-  -`+1``C     `
                    Console.WriteLine(ex.Message);
                    MessageBox.Show(Application.StartupPath + @"\logs\log.csv" + " - Failed to delete - Is it still open in Excel?", "Deletion Failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            // Could put an else clause in here, but I think it best to just let Excel handle it.
        }

        private void deleteWorkOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_ProcessFactory_WorkOrders_DeleteWorkOrder_Click(this.ribbonButton_ProcessFactory_WorkOrders_DeleteWorkOrder, EventArgs.Empty);
            ProcessFactory_WorkOrders_DeleteWorkOrder_Click(this.buttonProcessFactoryWorkOrdersDeleteWO, EventArgs.Empty);
        }

        private void discountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Accounting_Sales_Discounts_Click(this.ribbonButton_Accounting_Sales_Discounts, EventArgs.Empty);
            Accounting_Sales_Discounts_Click(this.buttonAccountingSalesDiscounts, EventArgs.Empty);
        }

        private void Dispatch_BaseLoad_AddNew_Click(object sender, EventArgs e)
        {
            historyAdd("Dispatch - BaseLoad - Add New");
            logger.Log(LogLevel.Info, DecorateString("Dispatch - Baseload", "Dispatch_BaseLoad_AddNew_Click", "(Add New)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.BaseLoad.Base_Load_Maintenance(int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.BaseLoad.Base_Load_Maintenance (Dispatch --> BaseLoad --> Add New)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Dispatch_BaseLoad_AddOutput_Click(object sender, EventArgs e)
        {
            historyAdd("Dispatch - BaseLoad - Add Output");
            logger.Log(LogLevel.Info, DecorateString("Dispatch - Baseload", "Dispatch_BaseLoad_AddOutput_Click", "(Add Output)"));

            DialogResult DLR_Message = new DialogResult();
            if (cueComboBoxDispatchBaseLoadList.SelectedItem != null)
            {
                ComboboxItem cbi = (ComboboxItem)cueComboBoxDispatchBaseLoadList.SelectedItem;

                if (cbi.Value == null)
                {
                    DLR_Message = MessageBox.Show("Invalid Baseload Selection, Please select a valid Baseload from the drop down list.", "Process Factory Dispatch (BaseLoad).", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (DLR_Message != System.Windows.Forms.DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    Form frm = new PF.WorkOrder.WO_Output(Convert.ToInt32(cbi.Value.ToString()), int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
                    frm.Text = "PF.WorkOrder.WO_Output (Dispatch --> BaseLoad --> Add Output)";
                    //frm.TopMost = true; // 21/10/2015 BN
                    frm.Show();
                    Cursor = Cursors.Default;
                }
            }
            else
            {
                DLR_Message = MessageBox.Show("Nothing Selected; Please select a valid Baseload from the drop down list.", "Process Factory Dispatch (BaseLoad)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Dispatch_BaseLoad_Complete_Click(object sender, EventArgs e)
        {
            historyAdd("Dispatch - BaseLoad - Complete");
            logger.Log(LogLevel.Info, DecorateString("Dispatch - Baseload", "Dispatch_BaseLoad_Complete_Click", "(Complete)"));

            string str_msg = "";

            int int_result = 0;
            //cueComboBoxDispatchBaseLoadList
            if (cueComboBoxDispatchBaseLoadList.SelectedItem != null)
            {
                ComboboxItem cbi = (ComboboxItem)cueComboBoxDispatchBaseLoadList.SelectedItem;
                if (cbi.Value == null)
                {
                    MessageBox.Show("Nothing Selected; Please select a valid Baseload from the drop down list.", "Process Factory Dispatch (BaseLoad Complete)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (cbi.Tag != null)
                {
                    try
                    {
                        int_result = PF.Data.AccessLayer.PF_Work_Order.Update_Completed(Convert.ToInt32(cbi.Value.ToString()), int_User_id);
                        int_result = PF.Data.AccessLayer.PF_BaseLoad.Complete(Convert.ToInt32(cbi.Tag.ToString()), false, int_User_id);

                        //labelTabControlMainBottomStatus.ForeColor = SystemColors.ControlText;
                        labelTabControlMainBottomStatus.ForeColor = Color.Blue;
                        labelTabControlMainBottomStatus.Text = "Baseload has been Completed";
                    }
                    catch
                    {
                        str_msg = "You must select a BaseLoad from the drop down list";
                    }
                }

                if (int_result <= 0 && str_msg.Length == 0)
                {
                    str_msg = "BaseLoad could NOT be Completed";
                }
                if (str_msg.Length > 0)
                {
                    MessageBox.Show(str_msg, "Process Factory - Complete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                cueComboBoxDispatchBaseLoadList.Text = string.Empty;
                populate_Baseload_Comboboxes();
            }
            else
            {
                MessageBox.Show("Nothing Selected; Please select a valid BaseLoad from the drop down list.", "Process Factory Dispatch (BaseLoad Complete)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Dispatch_BaseLoad_Reset_Click(object sender, EventArgs e)
        {
            historyAdd("Dispatch - BaseLoad - Reset");
            logger.Log(LogLevel.Info, DecorateString("Dispatch - Baseload", "Dispatch_BaseLoad_Reset_Click", "(Reset)"));

            cueComboBoxDispatchBaseLoadList.Text = string.Empty;
            populate_Baseload_Comboboxes();
        }

        private void Dispatch_BaseLoad_Run_Click(object sender, EventArgs e)
        {
            historyAdd("Dispatch - BaseLoad - Run");
            logger.Log(LogLevel.Info, DecorateString("Dispatch - Baseload", "Dispatch_BaseLoad_Run_Click", "(Run)"));

            DialogResult DLR_Message = new DialogResult();
            if (cueComboBoxDispatchBaseLoadList.SelectedItem != null)
            {
                //cueComboBoxDispatchBaseLoadList
                ComboboxItem cbi = (ComboboxItem)cueComboBoxDispatchBaseLoadList.SelectedItem;

                if (cbi.Value == null)
                {
                    DLR_Message = MessageBox.Show("Invalid Baseload Selection, Please select a valid Baseload from the drop down list.", "Process Factory Dispatch (BaseLoad).", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (DLR_Message != System.Windows.Forms.DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    Form frm = new PF.BaseLoad.BLWO_Labels(Convert.ToInt32(cbi.Value.ToString()), int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
                    frm.Text = "PF.BaseLoad.BLWO_Labels (Dispatch --> BaseLoad --> Run)";
                    //frm.TopMost = true; // 21/10/2015 BN
                    frm.Show();
                    Cursor = Cursors.Default;
                }
            }
            else
            {
                DLR_Message = MessageBox.Show("Nothing Selected; Please select a valid Baseload from the drop down list.", "Process Factory Dispatch (BaseLoad)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Dispatch_Orders_Create_Click(object sender, EventArgs e)
        {
            historyAdd("Dispatch - Orders - Create");
            logger.Log(LogLevel.Info, DecorateString("Dispatch - Orders", "Dispatch_Orders_Create_Click", "(Create)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Dispatch.Shipping_Order(int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.Dispatch.Shipping_Order (Dispatch --> Orders --> Create)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Dispatch_Orders_Search_Click(object sender, EventArgs e)
        {
            historyAdd("Dispatch - Orders - Search");
            logger.Log(LogLevel.Info, DecorateString("Dispatch - Orders", "Dispatch_Orders_Search_Click", "(Search)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Dispatch.Shipping_Search(int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.Dispatch.Shipping_Search (Dispatch --> Orders --> Search)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();

            Cursor = Cursors.Default;
        }

        private void Dispatch_Other_OtherWork_Click(object sender, EventArgs e)
        {
            historyAdd("Dispatch - Other - Other Work");
            logger.Log(LogLevel.Info, DecorateString("Dispatch - Other", "Dispatch_Other_OtherWork_Click", "(Other Work)"));
            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Dispatch.Other_Work(int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.Dispatch.Other_Work (Dispatch --> Other --> Other Work)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Dispatch_Packing_Pack_Click(object sender, EventArgs e)
        {
            historyAdd("Dispatch - Packing - Pack");
            logger.Log(LogLevel.Info, DecorateString("Dispatch - Packing", "Dispatch_Packing_Pack_Click", "(Pack)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Utils.Scanning.Repack(int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.Utils.Scanning.Repack (Dispatch --> Packing --> Pack)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Dispatch_Scanning_AddProductToOrder_Click(object sender, EventArgs e)
        {
            historyAdd("Dispatch - Scanning - Add Product to Order");
            logger.Log(LogLevel.Info, DecorateString("Dispatch - Scanning", "Dispatch_Scanning_AddProductToOrder_Click", "(Add Product to Order)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Utils.Scanning.Tipping_Onto_Order(int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.Utils.Scanning.Tipping_Onto_Order (Dispatch --> Scanning --> Add Product to Order)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Dispatch_Scanning_PalletTransfer_Click(object sender, EventArgs e)
        {
            historyAdd("Dispatch - Scanning - Pallet Transfer");
            logger.Log(LogLevel.Info, DecorateString("Dispatch - Scanning", "Dispatch_Scanning_PalletTransfer_Click", "(Pallet Transfer)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Utils.Scanning.Pallet_Transfer(int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.Utils.Scanning.Pallet_Transfer (Dispatch --> Scanning --> Pallet Transfer)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Dispatch_Scanning_PrintPalletCard_Click(object sender, EventArgs e)
        {
            historyAdd("Dispatch - Scanning - Print Pallet Card");
            logger.Log(LogLevel.Info, DecorateString("Dispatch - Scanning", "Dispatch_Scanning_PrintPalletCard_Click", "(Print Pallet Card)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Utils.Scanning.Reprint_Palletcards(int_User_id);
            frm.Text = "PF.Utils.Scanning.Reprint_Palletcards (Dispatch --> Scanning --> Print Pallet Card)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void dispatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.ribbon1.ActiveTab = this.ribbonTab_Dispatch;
            this.tabControlMain.SelectedTab = tabPageDispatch;
        }

        private void dispatchToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            //this.ribbon1.ActiveTab = this.ribbonTab_Dispatch;
            //this.tabControlMain.SelectedTab = tabPageDispatch;
            //this.toolStripStatusLabelActive.Text = this.rbt_PF_Dispatch.Text + " tab selected.";
        }

        private void eSPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Common_ESP_ESP_Click(this.ribbonButton_Common_ESP_ESP, EventArgs.Empty);
            Common_ESP_ESP_Click(this.buttonCommonEsp, EventArgs.Empty);
        }

        /// <summary>
        /// Handles the Click event of the excelKeyboardShortcutsToolStripMenuItem control. 27/02/2015 -
        /// For those moments when your mind goes blank whilst trying to remember the Excel shortcut keys...
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void excelKeyboardShortcutsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // This obtuse line translates out to a lovely formatted message box.
            // \t means tab, and \r\n means carriage return + newline.
            MessageBox.Show(
                "Select All: \t\t\tCTRL + A\r\n" +
                "AutoSize Headers: \t\tALT + H + O + I\r\n" +
                "Select Column 2: \t\tI don't know how just yet...\r\n" +
                "Format Column as DateTime: \tCTRL + SHIFT + 2.",
                "Excel Keyboard Shortcuts", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Another menu handler screwed up by the designer crash.
            // At some stage I will rename them all to be correct, but for now just leave as is
            // BN 21-04-2015
            // ---
            Application.Exit();
        }

        private void Extra_GateHouse_Submission_Click(object sender, EventArgs e)
        {
            historyAdd("Extra - GateHouse - Submission");
            logger.Log(LogLevel.Info, DecorateString("Extra - Gatehouse", "Extra_GateHouse_Submission_Click", "(Submission)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Temp.Submission(int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.Temp.Submission (Extra --> Gatehouse --> Submissions)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Extra_History_LoadOldStockNumbers_Click(object sender, EventArgs e)
        {
            historyAdd("Extra - History - Load Old Stock Numbers");
            logger.Log(LogLevel.Info, DecorateString("Extra - History", "Extra_History_LoadOldStockNumbers_Click", "(Load Old Stock Numbers)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Temp.Pre_System_Stock(int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.Temp.Pre_System_Stock (Extra --> History --> Load Old Stock Numbers)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Extra_Scanning_PalletWeights_Click(object sender, EventArgs e)
        {
            historyAdd("Extra - Scanning - Pallet Weights");
            logger.Log(LogLevel.Info, DecorateString("Extra - Scanning", "Extra_Scanning_PalletWeights_Click", "(Pallet Weights)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Utils.Scanning.Pallet_Weight(int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.Utils.Scanning.Pallet_Weight (Extra --> Scanning --> Pallet Weights)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Extra_Scanning_ReprintPalletCard_Click(object sender, EventArgs e)
        {
            historyAdd("Extra - Scanning - Reprint Pallet Card");
            logger.Log(LogLevel.Info, DecorateString("Extra - Scanning", "Extra_Scanning_ReprintPalletCard_Click", "(Reprint P/Card)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Utils.Scanning.Reprint_Palletcards(int_User_id);
            frm.Text = "PF.Utils.Scanning.Reprint_Palletcards (Extra --> Scanning --> Reprint Pallet Card)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Extra_Scanning_TabletMenu_Click(object sender, EventArgs e)
        {
            historyAdd("Extra - Scanning - Tablet Menu");
            logger.Log(LogLevel.Info, DecorateString("Extra - Scanning", "Extra_Scanning_TabletMenu_Click", "(Tablet Menu)"));

            Cursor = Cursors.WaitCursor;
            //Form frm = new PF.Utils.Scanning.Tablet_Menu();
            Form frm = new PF.Menu.Tablet.Tablet_Menu();
            frm.Text = "PF.Menu.Tablet.Tablet_Menu (Extra --> Scanning --> Tablet Menu)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void extraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.ribbon1.ActiveTab = this.ribbonTab_Extra;
            this.tabControlMain.SelectedTab = tabPageExtra;
        }

        private void extraToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            //this.ribbon1.ActiveTab = this.ribbonTab_Extra;
            //this.tabControlMain.SelectedTab = tabPageExtra;
            //this.toolStripStatusLabelActive.Text = this.rbt_Extra.Text + " tab selected.";
        }

        private void factoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_ProcessFactory_Setup_Factory_Click(this.ribbonButton_ProcessFactory_Setup_Factory, EventArgs.Empty);
            ProcessFactory_Setup_Factory_Click(this.buttonProcessFactorySetupFactory, EventArgs.Empty);
        }

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        private static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        private void Form_Load(object sender, EventArgs e)
        {
            // This took forever to figure out. This form is loaded BEFORE the logon form,
            // causing me hours of frustration trying to find the problem.

            // Update the settings object with the value set in the logon form
            //Settings.Phantom_Dev_Test_Mode = PF.Global.Global.Phantom_Dev_Test;
            //Settings.Save();

            this.propertyGridDebug.SelectedObject = Settings;
            this.labelCurrentPropertyGridSelectedObject.Text = "Settings";

            #region RichTextBox Logging

            RichTextBoxTarget target = new RichTextBoxTarget();
            target.Name = "richTextBoxDebug";
            target.Layout = "${longdate} ${level:uppercase=true} ${logger} ${message} ${callsite:fileName=true:includeSourcePath=false}\r\n";   // Added newline at the end - easier to read
            target.ControlName = "richTextBoxDebug";
            target.FormName = "Main_Menu";
            target.AutoScroll = true;
            target.MaxLines = 1000000;
            target.UseDefaultRowColoringRules = false;
            target.RowColoringRules.Add(
                new RichTextBoxRowColoringRule(
                    "level == LogLevel.Trace", // condition
                    "DarkGray", // font color
                    "White", // background color
                    FontStyle.Regular
                )
            );
            target.RowColoringRules.Add(new RichTextBoxRowColoringRule("level == LogLevel.Debug", "Green", "White"));
            target.RowColoringRules.Add(new RichTextBoxRowColoringRule("level == LogLevel.Info", "ControlText", "White"));
            target.RowColoringRules.Add(new RichTextBoxRowColoringRule("level == LogLevel.Warn", "Blue", "White"));
            target.RowColoringRules.Add(new RichTextBoxRowColoringRule("level == LogLevel.Error", "Red", "White"));
            target.RowColoringRules.Add(new RichTextBoxRowColoringRule("level == LogLevel.Fatal", "DarkRed", "White"));
            //target.RowColoringRules.Add(new RichTextBoxRowColoringRule("level == LogLevel.Error", "Red", "White", FontStyle.Bold));
            //target.RowColoringRules.Add(new RichTextBoxRowColoringRule("level == LogLevel.Fatal", "DarkRed", "White", FontStyle.Bold));

            #endregion RichTextBox Logging

            // specify what gets logged to the above target
            var loggingRuleRTB = new LoggingRule("*", LogLevel.Trace, target);

            // add target and rule to configuration
            LogManager.Configuration.AddTarget("targetRichTextBox", target);
            LogManager.Configuration.LoggingRules.Add(loggingRuleRTB);
            LogManager.Configuration.Reload();
            LogManager.ReconfigExistingLoggers();

            DateTime time = DateTime.Now;              // Use current time
                                                       //string format = "ddd MMM ddd d HH:mm yyyy";    // Use this format
                                                       //Console.WriteLine(time.ToString(format));  // Write to console

            //FileInfo fi = new FileInfo(Application.StartupPath + @"\Serialise.xml");
            //if (fi.Exists == true)
            //{
            //    //FormSerialisor.Deserialise(this, Application.StartupPath + @"\serialise.xml");
            //    TreeViewSerializer serializer = new TreeViewSerializer();
            //    serializer.DeserializeTreeView(this.treeViewUndoRedo, Application.StartupPath + @"\Serialise.xml");

            //    DateTime dt = DateTime.Now;
            //    //string sTime = String.Format("{0:T}", dt);  // "4:05:07 PM" LongTime
            //    string sTime = String.Format("{0:hh:mm:ss}", dt);  // Hour 12/24 (12)
            //    //historyAdd(dt.ToShortDateString() + " " + sTime + " Session Begin");
            //    historyAdd(sTime + " Session Begin");
            //    Console.WriteLine("Time: " + sTime);
            //}

            #region Check to see if Outlook is running, and if not, start it

            // Gotchas may include Outlook running with different permissions to this application
            Process[] pName = Process.GetProcessesByName("OUTLOOK");
            if (pName.Length == 0)
            {
                //MessageBox.Show("Outlook is not running."); // Open Outlook anew.
                System.Diagnostics.Process.Start("OUTLOOK.EXE");
            }
            //else
            //{
            //    MessageBox.Show("Outlook is already running."); // Do not re-open Outlook.
            //}

            #endregion Check to see if Outlook is running, and if not, start it
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            this.collapsibleSplitterDebugPanel.ToggleState();   // Force a refresh
            this.collapsibleSplitterDebugPanel.ToggleState();
            this.richTextBoxDebug.Refresh();    // Doesn't really work - Should try ScrollToEnd - 07-04-2015
        }

        private void frm_HistoryUpdated(object sender, Accounts.HistoryUpdateEventArgs e)
        {
            HistoryMessageSink(e.History);
        }

        private void fruitPurchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Accounting_Invoicing_FruitPurchase_Click(this.ribbonButton_Accounting_Invoicing_FruitPurchase, EventArgs.Empty);
            Accounting_Invoicing_FruitPurchase_Click(this.buttonAccountingInvoicingFruitPurchase, EventArgs.Empty);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        /// <summary>
        /// Countdown timer for RobinJ
        /// </summary>
        /// <returns>Time as a string</returns>
        public string GetTime()
        {
            string TimeInString = "";
            int hour = DateTime.Now.Hour;
            int min = DateTime.Now.Minute;
            int sec = DateTime.Now.Second;

            TimeInString = (hour < 10) ? "0" + hour.ToString() : hour.ToString();
            TimeInString += ":" + ((min < 10) ? "0" + min.ToString() : min.ToString());
            TimeInString += ":" + ((sec < 10) ? "0" + sec.ToString() : sec.ToString());
            return TimeInString;
        }

        public static TimeSpan GetUptime()
        {
            ManagementObject mo = new ManagementObject(@"\\.\root\cimv2:Win32_OperatingSystem=@");
            DateTime lastBootUp = ManagementDateTimeConverter.ToDateTime(mo["LastBootUpTime"].ToString());
            return DateTime.Now.ToUniversalTime() - lastBootUp.ToUniversalTime();
        }

        private void gradeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Common_Types_Material_Grade_Click(this.ribbonButton_Common_Types_Material_Grade, EventArgs.Empty);
            Common_Types_Material_Grade_Click(this.cueComboBoxCommonTypesMaterials, EventArgs.Empty);
        }

        private void groupedComboBoxCommonLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (groupedComboBoxCommonLocation.SelectedIndex != 0 && groupedComboBoxCommonLocation.SelectedItem != null)
            {
                //Console.WriteLine(groupedComboBoxCommonLocation.SelectedItem.GetType().ToString());

                object item = groupedComboBoxCommonLocation.SelectedItem; // Get the value from the control

                // Specify the "example" using the same property names, types and order as elsewhere.
                var cast = CastByExample(item, new
                {
                    Group = default(string),
                    Value = default(int),
                    Display = default(string)
                });

                var result = cast.Display;
                //Console.WriteLine(result.ToString());

                switch (result.ToString())
                {
                    case "Cities":
                        {
                            // Call new method
                            Common_Location_OffSite_Cities_Click(groupedComboBoxCommonLocation, EventArgs.Empty);
                            break;
                        }
                    case "Trucks":
                        {
                            // Call new method
                            Common_Location_OffSite_Trucks_Click(groupedComboBoxCommonLocation, EventArgs.Empty);
                            break;
                        }
                    case "Location":
                        {
                            // Call new method
                            Common_Location_OnSite_Location_Click(groupedComboBoxCommonLocation, EventArgs.Empty);
                            break;
                        }
                }
                // Deselect the entry
                //groupedComboBoxCommonLocation.SelectedIndex = -1;

                // Set the focus away from the cueComboBox
                tabPageCommon.Select();
            }
        }

        private void groupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Security_Maintenance_Group_Click(this.ribbonButton_Security_Maintenance_Group, EventArgs.Empty);
            Security_Maintenance_Group_Click(this.buttonSecurityMaintenanceGroup, EventArgs.Empty);
        }

        private void growerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Common_Grower_Grower_Click(this.ribbonButton_Common_Grower_Grower, EventArgs.Empty);
            Common_Grower_Grower_Click(this.buttonCommonGrowerGrower, EventArgs.Empty);
        }

        private void growingMethodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Common_Types_Material_GrowingMethod_Click(this.ribbonButton_Common_Types_Material_GrowingMethod, EventArgs.Empty);
            Common_Types_Material_GrowingMethod_Click(this.cueComboBoxCommonTypesMaterials, EventArgs.Empty);
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //rbbtn_H_H_Help_Click(this, EventArgs.Empty);
            Home_Help_Help_Click(this, EventArgs.Empty);
        }

        private void hide_Tabs()
        {
            DataSet ds_Get_User_Menus = PF.Data.AccessLayer.SC_User.Get_User_Menus(int_User_id);
            DataRow dr_Get_User_Menus;

            #region Original version for Ribbon

            // ------------------------------------------------------------- BN
            // Hide everything and set the Tag property to false.
            // The program uses the Tag property to allow/disallow access
            // to the more sensitive areas of the program
            // ------------------------------------------------------------- BN

            //foreach (var pb in this.Controls.OfType<Ribbon>())
            //{
            //    #region Hide everything
            //    for (int it = 1; it < Convert.ToInt32(pb.Tabs.Count.ToString()); it++)
            //    {
            //        for (int ip = 0; ip < Convert.ToInt32(pb.Tabs[it].Panels.Count.ToString()); ip++)
            //        {
            //            try
            //            {
            //                pb.Tabs[it].Panels[ip].Visible = false;
            //            }
            //            catch (Exception ex)
            //            {
            //                logger.Log(LogLevel.Debug, ex.Message);
            //            }
            //            for (int ii = 0; ii < Convert.ToInt32(pb.Tabs[it].Panels[ip].Items.Count.ToString()); ii++)
            //            {
            //                pb.Tabs[it].Panels[ip].Items[ii].Tag = false;
            //            }
            //        }
            //        pb.Tabs[it].Visible = false;
            //    }
            //    #endregion

            //    #region Make certain tabs and controls visible depending on the security level
            //    for (int it = 1; it < Convert.ToInt32(pb.Tabs.Count.ToString()); it++)
            //    {
            //        for (int i = 0; i < Convert.ToInt32(ds_Get_User_Menus.Tables[0].Rows.Count.ToString()); i++)
            //        {
            //            dr_Get_User_Menus = ds_Get_User_Menus.Tables[0].Rows[i];

            //            //Console.WriteLine("Line 804: dr_Get_User_Menus[MENU].ToString()" + dr_Get_User_Menus["MENU"].ToString());
            //            // Just gets the tab names: Common, Dispatch etc.

            //            if (dr_Get_User_Menus["MENU"].ToString() == pb.Tabs[it].Text)
            //            {
            //                //Console.WriteLine("Line 807: Match on " + pb.Tabs[it].Text);
            //                //Console.WriteLine("MENU " + pb.Tabs[it].Text);

            //                pb.Tabs[it].Visible = true;
            //            }
            //            for (int ip = 0; ip < Convert.ToInt32(pb.Tabs[it].Panels.Count.ToString()); ip++)
            //            {
            //                if (dr_Get_User_Menus["MENU"].ToString() == pb.Tabs[it].Text && dr_Get_User_Menus["SUBMENU"].ToString() == pb.Tabs[it].Panels[ip].Text)
            //                {
            //                    //Console.WriteLine("SUBMENU " + pb.Tabs[it].Panels[ip].Text);

            //                    pb.Tabs[it].Panels[ip].Visible = true;
            //                    for (int ii = 0; ii < Convert.ToInt32(pb.Tabs[it].Panels[ip].Items.Count.ToString()); ii++)
            //                    {
            //                        pb.Tabs[it].Panels[ip].Items[ii].Tag = dr_Get_User_Menus["Write_Access"].ToString();
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    #endregion
            //}

            #endregion Original version for Ribbon

            // ------------------------------------------------------------- BN
            // New version for standard tab controls
            // ------------------------------------------------------------- BN
            foreach (var pb in this.Controls.OfType<TabControl>())
            {
                TabControl tc = (TabControl)this.tabControlMain;
                // Get all the controls here
                Control.ControlCollection controlCollection = tc.Controls;

                // Counter for the TabPages
                // Counter for the GroupBoxes
                //int iTabPageCount = 0;
                //int iGroupBoxCount = 0;

                // Manually drill down through the control collection.
                foreach (Control c in controlCollection)
                {
                    Type cType = c.GetType();

                    // TabPage
                    if (cType == typeof(TabPage))
                    {
                        //iTabPageCount++;
                        TabPage tp = (TabPage)c;
                        //Console.WriteLine(tp.Name);

                        foreach (Control tpSubControl in tp.Controls)
                        {
                            Type tpSubControlType = tpSubControl.GetType();

                            // GradientPanel
                            if (tpSubControlType == typeof(Tools.GradientPanel.GradientPanel))
                            {
                                //Console.WriteLine(tpSubControlType.Name);
                                Tools.GradientPanel.GradientPanel gp = (Tools.GradientPanel.GradientPanel)tpSubControl;
                                foreach (Control gpSubControl in gp.Controls)
                                {
                                    Type gpSubControlType = gpSubControl.GetType();

                                    // Grouper
                                    if (gpSubControlType == typeof(Tools.Grouper))
                                    {
                                        //Console.WriteLine(gpSubControlType.Name);

                                        // Everything living inside the Grouper
                                        Tools.Grouper tempGroup = (Tools.Grouper)gpSubControl;

                                        // Help and Message are exempt from the security check
                                        if (tempGroup.GroupTitle != "Help" && tempGroup.GroupTitle != "Message")
                                        {
                                            //iGroupBoxCount++;
                                            tempGroup.Visible = false;
                                            //tempGroup.Tag = false;
                                            foreach (Control ccControl in tempGroup.Controls)
                                            {
                                                ccControl.Tag = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        // It would have been nice to either hide or lock the TabPage here,
                        // but that's not possible with any version of the .Net framework
                    }
                }
                //Console.WriteLine("iTabPageCount: " + iTabPageCount.ToString());
                //Console.WriteLine("iGroupBoxCount: " + iGroupBoxCount.ToString());

                #region Basic sequence explanation

                // Basic sequence of events:
                // Loop over the RibbonTabs in the RibbonTabCollection
                // Loop over the RibbonPanels in the RibonPanelCollection
                // Switch the Visibility of each item to False (Not visible)
                //
                // Set the Tag property of each item to false
                // Loop over the RibbonTabs in the RibbonTabCollection
                // Loop over the RibbonPanels in the RibonPanelCollection
                // Look up in the database

                #endregion Basic sequence explanation

                #region Hide everything

                //for (int it = 1; it < Convert.ToInt32(pb.Tabs.Count.ToString()); it++)
                //    {
                //    for (int ip = 0; ip < Convert.ToInt32(pb.Tabs[it].Panels.Count.ToString()); ip++)
                //    {
                //        try
                //        {
                //            pb.Tabs[it].Panels[ip].Visible = false;
                //        }
                //        catch (Exception ex)
                //        {
                //            logger.Log(LogLevel.Debug, ex.Message);
                //        }
                //        for (int ii = 0; ii < Convert.ToInt32(pb.Tabs[it].Panels[ip].Items.Count.ToString()); ii++)
                //        {
                //            pb.Tabs[it].Panels[ip].Items[ii].Tag = false;
                //        }
                //    }
                //    pb.Tabs[it].Visible = false;
                //}

                #endregion Hide everything

                //for (int it = 1; it < Convert.ToInt32(pb.Tabs.Count.ToString()); it++)
                //{
                //    for (int i = 0; i < Convert.ToInt32(ds_Get_User_Menus.Tables[0].Rows.Count.ToString()); i++)
                //    {
                //        dr_Get_User_Menus = ds_Get_User_Menus.Tables[0].Rows[i];

                #region Untranslatable

                // I can't translate this bit, as there is no equivalent option to hide a TabPage in any version of the .NET framework
                // The TabPage has to stay visible, but empty of controls
                //
                //        if (dr_Get_User_Menus["MENU"].ToString() == pb.Tabs[it].Text)
                //        {
                //            pb.Tabs[it].Visible = true;
                //        }

                #endregion Untranslatable

                // Looks like the whole idea of using the counters was to use indexing to switch on and off visibility
                // and to set the Tag property.
                // Since the names of everything are stored in the database anyway, I don't see why the guy bothered doing it this way,
                // as he was the one writing the code and had to hardcode the text names to the form controls in the first place.
                // I'll just use my pattern of looping through the controls till I get a hit, which is exactly what Dave was doing, just
                // using counters for some reason.

                //        for (int ip = 0; ip < Convert.ToInt32(pb.Tabs[it].Panels.Count.ToString()); ip++)
                //        {
                //            if (dr_Get_User_Menus["MENU"].ToString() == pb.Tabs[it].Text && dr_Get_User_Menus["SUBMENU"].ToString() == pb.Tabs[it].Panels[ip].Text)
                //            {
                //                pb.Tabs[it].Panels[ip].Visible = true;
                //                for (int ii = 0; ii < Convert.ToInt32(pb.Tabs[it].Panels[ip].Items.Count.ToString()); ii++)
                //                {
                //                    pb.Tabs[it].Panels[ip].Items[ii].Tag = dr_Get_User_Menus["Write_Access"].ToString();
                //                }
                //            }
                //        }
                //    }
                //}

                // 1. Look for a match on Menu name and the TabPage Text
                // 2. Look for a match on the Submenu name and the Grouper.GroupTitle
                string menuName = string.Empty;
                string subMenuName = string.Empty;

                for (int i = 0; i < Convert.ToInt32(ds_Get_User_Menus.Tables[0].Rows.Count.ToString()); i++)
                {
                    dr_Get_User_Menus = ds_Get_User_Menus.Tables[0].Rows[i];

                    menuName = dr_Get_User_Menus["MENU"].ToString();
                    subMenuName = dr_Get_User_Menus["SUBMENU"].ToString();

                    //if (dr_Get_User_Menus["MENU"].ToString() == pb.Tabs[it].Text &&
                    //dr_Get_User_Menus["SUBMENU"].ToString() == pb.Tabs[it].Panels[ip].Text)

                    // Manually drill down through the control collection.
                    foreach (Control c in controlCollection)
                    {
                        Type cType = c.GetType();

                        // TabPage
                        if (cType == typeof(TabPage))
                        {
                            //iTabPageCount++;
                            TabPage tp = (TabPage)c;
                            //Console.WriteLine(tp.Name);

                            foreach (Control tpSubControl in tp.Controls)
                            {
                                Type tpSubControlType = tpSubControl.GetType();

                                // GradientPanel
                                if (tpSubControlType == typeof(Tools.GradientPanel.GradientPanel))
                                {
                                    //Console.WriteLine(tpSubControlType.Name);
                                    Tools.GradientPanel.GradientPanel gp = (Tools.GradientPanel.GradientPanel)tpSubControl;
                                    foreach (Control gpSubControl in gp.Controls)
                                    {
                                        Type gpSubControlType = gpSubControl.GetType();

                                        // Grouper
                                        if (gpSubControlType == typeof(Tools.Grouper))
                                        {
                                            //Console.WriteLine(gpSubControlType.Name);

                                            // Everything living inside the Grouper
                                            Tools.Grouper tempGroup = (Tools.Grouper)gpSubControl;

                                            if (tp.Text == menuName && tempGroup.GroupTitle == subMenuName)
                                            {
                                                // Help and Message are exempt from the security check
                                                //if (tempGroup.GroupTitle != "Help" && tempGroup.GroupTitle != "Message")
                                                //{
                                                //iGroupBoxCount++;
                                                tempGroup.Visible = true;
                                                foreach (Control ccControl in tempGroup.Controls)
                                                {
                                                    ccControl.Tag = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            // It would have been nice to either hide or lock the TabPage here,
                            // but that's not possible with any version of the .Net framework
                        }
                    }
                }
            }

            // -------------------------
            ds_Get_User_Menus.Dispose();
        }

        private void hintedTextBox_Common_Material_MaterialNumber_Validated(object sender, EventArgs e)
        {
            historyAdd("Common - Material Number: " + hintedTextBox_Common_Material_MaterialNumber.Text);
        }

        public void historyAdd(string historyAction)
        {
            //using (Reversible.Transaction txn = undoRedoSession.Begin("Add item"))
            //{
            //    // Increment node index reversibly
            //    Reversible.Transaction.AddPropertyChange(v => nodeIndex = v, nodeIndex, nodeIndex + 1);
            //    nodeIndex++;

            //    //TreeView treeViewUndoRedo = new TreeView ();

            //    // Always add to the root of the TreeView
            //    // Don't add the nodeIndex number at the end
            //    //treeViewUndoRedo.Nodes.Add_Reversible(new TreeNode(historyAction + nodeIndex));

            //    // Prefix with short time
            //    //DateTime.Now.ToShortTimeString();

            //    DateTime dt = DateTime.Now;

            //    //String.Format("{0:h mm ss tt}", dt);
            //    //string sTime = String.Format("{0:T}", dt);  // "4:05:07 PM" LongTime
            //    string sTime = String.Format("{0:hh:mm:ss}", dt);  // "4:05:07 PM" LongTime

            //    #region Date and Time stamp the very first entry in the tree
            //    if (treeViewUndoRedo.Nodes.Count == 0)
            //    {
            //        TreeNode tNew = new TreeNode(dt.ToShortDateString() + " " + sTime + " Session Begin");
            //        tNew.ImageIndex = 11;
            //        tNew.SelectedImageIndex = 11;
            //        treeViewUndoRedo.Nodes.Add_Reversible(tNew);
            //    }
            //    else
            //    {
            //        // Check to see if the date is earlier than today's date.
            //        //int lastBackslash = treeViewUndoRedo.Nodes[0].Text.LastIndexOf("/");
            //        //string datePart = treeViewUndoRedo.Nodes[0].Text.Substring(0, (lastBackslash + 5)); // If you don't add 5 you'll just get 8/04
            //        //string today = dt.ToString("d");    // Return Date value in the form 8/4/2015
            //        //if (datePart.StartsWith(today) != true)
            //        //{
            //        // Date doesn't match so clear the tree after saving to a backup
            //        FileInfo fi = new FileInfo(Application.StartupPath + @"\Serialise.xml");
            //        if (fi.Exists == true)
            //        {
            //            string s = ExtensionMethods.ToFileSize(fi.Length);
            //            Console.WriteLine("FileSize in Bytes: " + s);
            //            Console.WriteLine("TreeSize in Megabytes: " + this.MaxTreeFileSize);

            //            // Bytes to Megabytes = Bytes / (1024 * 1024)
            //            // Megabytes to Bytes = Megabytes * (1024 * 1024.0)

            //            // 1.   Convert MaxTreeSize to Bytes
            //            // 2.   Compare fi.Length to converted size

            //            float MaxTree = float.Parse(MaxTreeFileSize);   // float.Parse is totally new to me :)
            //            float Max = MaxTree * (1024F * 1024.0F);
            //            Console.WriteLine("TreeSize in Megabytes as float: " + Max.ToString());
            //            if (fi.Length > Max)
            //            {
            //                // Copy to a new file, overwriting any previously existing file.
            //                fi.CopyTo(Application.StartupPath + @"\Serialise_Backup.xml", true);
            //                fi.Delete();

            //                treeViewUndoRedo.Nodes.Clear();

            //                TreeNode tNew = new TreeNode(dt.ToShortDateString() + " " + sTime + " Session Begin");
            //                tNew.ImageIndex = 11;
            //                tNew.SelectedImageIndex = 11;
            //                treeViewUndoRedo.Nodes.Add_Reversible(tNew);

            //            }

            //        }
            //        //}
            //    }
            //    #endregion

            //    if (treeViewUndoRedo.SelectedNode == null)
            //    {
            //        // Just select the last available node
            //        int iCount = treeViewUndoRedo.Nodes.Count;
            //        treeViewUndoRedo.SelectedNode = treeViewUndoRedo.Nodes[iCount - 1];
            //    }

            //    TreeNode currentNode = treeViewUndoRedo.SelectedNode;
            //    if (historyAction.StartsWith("TabPage"))                    //if (historyAction.StartsWith("Ribbon Tab"))
            //    {
            //        currentNode = null;
            //        TreeNode tn = new TreeNode(sTime + " " + historyAction);

            //        if (historyAction.EndsWith("Home"))
            //        {
            //            tn.ImageIndex = 1;
            //            tn.SelectedImageIndex = 1;
            //        }
            //        else if (historyAction.EndsWith("Security"))
            //        {
            //            tn.ImageIndex = 2;
            //            tn.SelectedImageIndex = 2;
            //        }
            //        else if (historyAction.EndsWith("Common"))
            //        {
            //            tn.ImageIndex = 3;
            //            tn.SelectedImageIndex = 3;
            //        }
            //        else if (historyAction.EndsWith("Process Factory"))
            //        {
            //            tn.ImageIndex = 4;
            //            tn.SelectedImageIndex = 4;
            //        }
            //        else if (historyAction.EndsWith("Dispatch"))
            //        {
            //            tn.ImageIndex = 5;
            //            tn.SelectedImageIndex = 5;
            //        }
            //        else if (historyAction.EndsWith("PF - Maintenance"))
            //        {
            //            tn.ImageIndex = 6;
            //            tn.SelectedImageIndex = 6;
            //        }
            //        else if (historyAction.EndsWith("Accounting"))
            //        {
            //            tn.ImageIndex = 7;
            //            tn.SelectedImageIndex = 7;
            //        }
            //        else if (historyAction.EndsWith("Consumables"))
            //        {
            //            tn.ImageIndex = 8;
            //            tn.SelectedImageIndex = 8;
            //        }
            //        else if (historyAction.EndsWith("Extra"))
            //        {
            //            tn.ImageIndex = 9;
            //            tn.SelectedImageIndex = 9;
            //        }
            //        else if (historyAction.EndsWith("Reports"))
            //        {
            //            tn.ImageIndex = 10;
            //            tn.SelectedImageIndex = 10;
            //        }
            //        //else
            //        //{
            //        //    tn.ImageIndex = -1;
            //        //}
            //        treeViewUndoRedo.Nodes.Add_Reversible(tn);
            //        treeViewUndoRedo.SelectedNode = tn;
            //    }
            //    else if (historyAction.StartsWith("Button"))
            //    {
            //        currentNode.Nodes.Add_Reversible(new TreeNode(sTime + " " + historyAction));
            //    }
            //    else if (historyAction.EndsWith("Session Begin"))
            //    {
            //        TreeNode tNew = new TreeNode(dt.ToShortDateString() + " " + sTime + " Session Begin");
            //        tNew.ImageIndex = 11;
            //        tNew.SelectedImageIndex = 11;
            //        treeViewUndoRedo.Nodes.Add_Reversible(tNew);
            //    }
            //    else
            //    {
            //        currentNode.Nodes.Add_Reversible(new TreeNode(sTime + " " + historyAction));
            //    }

            //    treeViewUndoRedo.ExpandAll();

            //    // Make sure bottom node is visible
            //    treeViewUndoRedo.Nodes[treeViewUndoRedo.Nodes.Count - 1].EnsureVisible();

            //    //if (currentNode == null)
            //    //{
            //    //    treeViewUndoRedo.Nodes.Add_Reversible(new TreeNode(historyAction + nodeIndex));
            //    //}
            //    //else
            //    //{
            //    //    currentNode.Nodes.Add_Reversible(new TreeNode(historyAction + nodeIndex));
            //    //}

            //    //treeViewUndoRedo.Nodes.Add_Reversible(new TreeNode(sTime + " " + historyAction));

            //    //TreeNode currentNode = treeViewUndoRedo.SelectedNode;
            //    //if (currentNode == null)
            //    //{
            //    //    treeViewUndoRedo.Nodes.Add_Reversible(new TreeNode(historyAction + nodeIndex));
            //    //}
            //    //else
            //    //{
            //    //    currentNode.Nodes.Add_Reversible(new TreeNode(historyAction + nodeIndex));
            //    //}

            //    txn.Commit();

            //}
        }

        public void HistoryMessageSink(string message)
        {
            historyAdd(message);
        }

        private void holdingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Consumables_Maintenance_Holding_Click(this.ribbonButton_Consumables_Maintenance_Holding, EventArgs.Empty);
            Consumables_Maintenance_Holding_Click(this.buttonConsumablesMaintenanceHolding, EventArgs.Empty);
        }

        private void Home_Help_Help_Click(object sender, EventArgs e)
        {
            historyAdd("Home - Help - Help");
            logger.Log(LogLevel.Info, DecorateString("Home - Message", "Home_Help_Help_Click", "(Help)"));

            if (System.IO.File.Exists("C:\\FP\\Documentation\\Help\\Process Factory.chm"))
            {
                Help.ShowHelp(this, "file://C:\\FP\\Documentation\\Help\\Process Factory.chm");
            }
            else
            {
                MessageBox.Show("Help File is missing.\r\nPlease contact Jim @ Phantom or the FP Development Office", "Help file is missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Home_Message_AddNew_Click(object sender, EventArgs e)
        {
            historyAdd("Home - Message - Add New");
            logger.Log(LogLevel.Info, DecorateString("Home - Message", "Home_Message_AddNew_Click", "(Add New)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Menu.Fun_Messages();
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.ribbon1.ActiveTab = this.ribbonTab_Home;
            this.tabControlMain.SelectedTab = tabPageHome;
        }

        private void homeToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            // Must be in opening, not opened, otherwise the dropdown menu is not fully drawn. BN 21-4-2015
            //this.ribbon1.ActiveTab = this.ribbonTab_Home;
            //this.tabControlMain.SelectedTab = tabPageHome;
            //this.toolStripStatusLabelActive.Text = this.rbt_Home.Text + " tab selected.";
        }

        private void imn_NewTextChanged(object sender, TextEventArgs e)
        {
            //this.ribbonText_Common_Material_MaterialNumber.TextBoxText = e.Text;
            this.materialNumberToolStripMenuItem.Text = "Material Number: " + e.Text;
            if (e.Text != string.Empty)
            {
                this.updateMaterialToolStripMenuItem.Enabled = true;
            }
            else
            {
                this.materialNumberToolStripMenuItem.Text = "Material Number: None";
                this.updateMaterialToolStripMenuItem.Enabled = false;
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool InsertMenu(IntPtr hMenu, int uPosition, int uFlags, int uIDNewItem, string lpNewItem);

        private void inspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            propertyGridDebug.SelectedObject = richTextBoxDebug;
            this.labelCurrentPropertyGridSelectedObject.Text = "TextBox Debug";

            if (this.collapsibleSplitterDebugPanel.IsCollapsed)
            {
                this.collapsibleSplitterDebugPanel.ToggleState();
            }
        }

        private void iwo_GP_Changed(object sender, GeneralPurposeObjectEventArgs e)
        {
            // These are set up and ready to go
            //e.GP_Description;
            //e.GP_Object;
            //e.GP_Text;
        }

        /// <summary>
        /// Phantom Work
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void kyoceraFS1061DNKPSLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Printer_Name = "Kyocera FS-1061DN KPSL";
            this.propertyGridDebug.Refresh();
            Settings.Save();
        }

        /// <summary>
        /// Phantom Flat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void kyoceraFS1300DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Printer_Name = "Kyocera FS-1300D";
            this.propertyGridDebug.Refresh();
            Settings.Save();
        }

        private void labResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Maintenance_Lab_LabResults_Click(this.ribbonButton_Maintenance_Lab_LabResults, EventArgs.Empty);
            Maintenance_Lab_LabResults_Click(this.buttonPfMaintenanceLabLabResults, EventArgs.Empty);
        }

        private void loadHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //openFileDialog1.DefaultExt = "xml";
            //openFileDialog1.InitialDirectory = Application.ExecutablePath;
            //openFileDialog1.Title = "Load History File...";
            //openFileDialog1.Filter = "XML History Files|*.xml";
            //openFileDialog1.FileName = "Serialise*.xml";

            //// Show the dialog and get result.
            //DialogResult result = openFileDialog1.ShowDialog();

            //if (result == DialogResult.OK) // Test result.
            //{
            //    // Overkill
            //    //FileInfo fi = new FileInfo(Application.StartupPath + @"\serialise.xml");
            //    FileInfo fi = new FileInfo(openFileDialog1.FileName);
            //    if (fi.Exists == true)
            //    {
            //        TreeViewSerializer serializer = new TreeViewSerializer();
            //        serializer.DeserializeTreeView(this.treeViewUndoRedo, openFileDialog1.FileName);

            //        DateTime dt = DateTime.Now;
            //        //string sTime = String.Format("{0:T}", dt);  // "4:05:07 PM" LongTime
            //        //historyAdd(dt.ToShortDateString() + " " + sTime + " History Loaded");

            //        string sTime = String.Format("{0:hh:mm:ss}", dt);  // Hour 12/24 (12)
            //        //historyAdd(dt.ToShortDateString() + " " + sTime + " Session Begin");
            //        historyAdd(sTime + " Session Begin");
            //        //Console.WriteLine("Time: " + sTime);

            //        TreeNode tNew = new TreeNode(dt.ToShortDateString() + " " + sTime + " History Loaded");
            //        tNew.ImageIndex = 13;
            //        tNew.SelectedImageIndex = 13;
            //        treeViewUndoRedo.Nodes.Add_Reversible(tNew);

            //    }
            //}
            ////Console.WriteLine(result); // <-- For debugging use.

            ////FileInfo fi = new FileInfo(Application.StartupPath + @"\serialise.xml");
            ////if (fi.Exists == true)
            ////{
            ////    TreeViewSerializer serializer = new TreeViewSerializer();
            ////    serializer.DeserializeTreeView(this.treeViewUndoRedo, Application.StartupPath + @"\serialise.xml");

            ////    DateTime dt = DateTime.Now;
            ////    string sTime = String.Format("{0:T}", dt);  // "4:05:07 PM" LongTime
            ////    historyAdd(dt.ToShortDateString() + " " + sTime + " Session Begin");
            ////}
        }

        private void loadOldStockNumbersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Extra_History_LoadOldStockNumbers_Click(this.ribbonButton_Extra_History_LoadOldStockNumbers, EventArgs.Empty);
            Extra_History_LoadOldStockNumbers_Click(this.buttonExtraHistoryLoadOldStockNumbers, EventArgs.Empty);
        }

        private void locationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Common_Location_OnSite_Location_Click(this.ribbonButton_Common_Location_OnSite_Location, EventArgs.Empty);
            Common_Location_OnSite_Location_Click(this.groupedComboBoxCommonLocation, EventArgs.Empty);
        }

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

        private void Main_Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            //UnregisterHotKey(this.Handle, 0);       // Unregister hotkey with id 0 before closing the form. You might want to call this more than once with different id values if you are planning to register more than one hotkey.

            timer.Stop();
            timerSysMon.Stop();

            Settings.Save();
            Settings = null;
            logger.Log(LogLevel.Info, LogCode("Main_Menu.cs: Main_Menu_FormClosing: Settings file saved."));
            logger.Log(LogLevel.Info, "---------------------[ Program Ending ]--- :" + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToLongTimeString());
            ////FormSerialisor.Serialise(this, Application.StartupPath + @"\serialise.xml");
            //TreeViewSerializer serializer = new TreeViewSerializer();
            ////serializer.SerializeTreeView(this.treeViewUndoRedo, saveFile.FileName);
            //serializer.SerializeTreeView(this.treeViewUndoRedo, Application.StartupPath + @"\Serialise.xml");

            //PF.Common.Code.KillAllWordInstances.KillAllWordProcesses();
        }

        /// <summary>
        /// Close the form with the Esc key (Sel request 11-02-2015 BN)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Menu_KeyDown(object sender, KeyEventArgs e)
        {
            // Note that the KeyPreview property must be set to true for this to work
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void Main_Menu_ResizeEnd(object sender, EventArgs e)
        {
            //// When window state changes
            //if (WindowState != LastWindowState)
            //{
            //    LastWindowState = WindowState;


            //    if (WindowState == FormWindowState.Maximized)
            //    {
            //        // Maximized!
            //        this.collapsibleSplitterDebugPanel.ToggleState();
            //        this.collapsibleSplitterDebugPanel.ToggleState();
            //        this.collapsibleSplitterDebug.ToggleState();
            //        this.collapsibleSplitterDebug.ToggleState();
            //    }
            //    if (WindowState == FormWindowState.Normal)
            //    {
            //        // Restored!
            //        this.collapsibleSplitterDebugPanel.ToggleState();
            //        this.collapsibleSplitterDebugPanel.ToggleState();
            //        this.collapsibleSplitterDebug.ToggleState();
            //        this.collapsibleSplitterDebug.ToggleState();
            //    }
            //}

            this.richTextBoxDebug.Refresh();
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                //MessageBox.Show("Max!");
                this.collapsibleSplitterDebugPanel.ToggleState();
                this.collapsibleSplitterDebugPanel.ToggleState();
                this.collapsibleSplitterDebug.ToggleState();
                this.collapsibleSplitterDebug.ToggleState();

            }

            if (this.WindowState == FormWindowState.Normal)
            {
                //MessageBox.Show("Max!");
                this.collapsibleSplitterDebugPanel.ToggleState();
                this.collapsibleSplitterDebugPanel.ToggleState();
                this.collapsibleSplitterDebug.ToggleState();
                this.collapsibleSplitterDebug.ToggleState();

            }

            base.OnSizeChanged(e);
        }
        /// <summary>
        /// This code gets executed just before the Interface is shown. Handy spot to upda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Menu_Shown(object sender, EventArgs e)
        {
            // Collapse the CollapsableSplitter to the collapsed state. Phantom 20/12/2014
            this.collapsibleSplitterDebugPanel.ToggleState();
            this.collapsibleSplitterDebug.ToggleState();
            this.labelTabControlMainBottomStatus.Text = string.Empty;

            #region Get how long since the system was last restarted - BN 01/04/2015

            int iDay = GetUptime().Days; int iHour = GetUptime().Hours; int iMinute = GetUptime().Minutes;
            string sDay = string.Empty; string sHour = string.Empty; string sMinute = string.Empty;

            // Format nicely
            if (iDay == 0) { sDay = " Days"; } else if (iDay == 1) { sDay = " Day"; } else { sDay = " Days"; }
            if (iHour == 0) { sHour = " Hours"; } else if (iHour == 1) { sHour = " Hour"; } else { sHour = " Hours"; }
            if (iMinute == 0) { sMinute = " Minutes"; } else if (iMinute == 1) { sMinute = " Minute"; } else { sMinute = " Minutes"; }

            string UpTime =
                "System UpTime: " +
                iDay + sDay + ", " +
                iHour + sHour + ", " +
                iMinute + sMinute + ".";

            #endregion Get how long since the system was last restarted - BN 01/04/2015

            //The version number has four parts, as follows:
            //<major version>.<minor version>.<build number>.<revision>
            
            // Having the windows logon name is causing confusion
            this.Text += " V" + Application.ProductVersion + " (20/11/2015)";

            //this.Text += " V" + Application.ProductVersion + " (10/11/2015) " +
            //    System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            //"Special Proxy Test Version (05/08/2015)";

            // Make a note of the date the version number changed "
            // (Started off with v.1.3, for no reason at all.)
            // --------------------------------------------------
            // V.1.3 (06/01/2015)
            // V.1.4 (09/01/2015)
            // V.1.5 (12/01/2015)
            // V.1.6 (15/01/2015)
            // V.1.7 (20/01/2015)
            // v1.8 (3/02/2015)
            // v1.9 (4/02/2015)
            // v2.0 (10/02/2015)
            // v2.1 (11/02/2015)
            // v2.2 (13/02/2015)    // Fixed the hidden printer bug
            // v2.3 (17/02/2015)    // Restricted the debug logging while we are hunting down the Customer_Id/Trader_Id/Invoice_Id problem
            // v2.4 (26/02/2015)    //
            // v2.5 (27/02/2015)    //
            //" v2.6 (04/03/2015)"; // Finished investigating the COA code - no real changes
            //this.Text += " v2.7 (09/03/2015)"; //
            //this.Text += " v2.8 (12/03/2015)"; // Reasonably sure the COA stuff is fixed now (18-3-15)
            //this.Text += " v2.9 (18/03/2015)"; // Fixed the OutLook% problem in PF_Customer, fixed reset extra freight in Invoicing_Sales
            //this.Text += " v3.0 (23/03/2015)";//
            //this.Text += " v3.1 (14/04/2015)";//
            //this.Text += " v3.2 (15/04/2015)";// Replaced Ribbon.dll with the last stable one
            //this.Text += " v3.3 (21/04/2015)"; // Installed alternate menu
            //this.Text += " v3.4 (12/05/2015)"; // Installed alternate Menu & Tabs + Solved the security issue
            //this.Text += " v4.0 (14/05/2015)"; // Ribbon finally ready to be hidden
            //this.Text += " v4.5.1 (21/05/2015)"; // Ribbon is now completely removed. Jumped version number as a reminder that we now need the .NET Framework v4.5
            //this.Text += " v4.5.2 (22/05/2015) - " + PF.Global.Global.LogonName; // Menu handlers now hooked up correctly. Missing Packing section in Dispatch restored.
            //this.Text += " v4.5.3 (06/06/2015) - " + PF.Global.Global.LogonName; // Keyboard Accelerators and Tab Stops corrected in all forms + Sel's requested changes to the Tipping Forms
            //this.Text += " v4.6 (10/06/2015) - " + PF.Global.Global.LogonName; // Added File --> Update functionality
            //this.Text += " v4.7 (01/07/2015) - " + PF.Global.Global.LogonName; // Fixed hardcoded string printer names + accelerators
            //this.Text += " v4.7 (04/08/2015) - " + PF.Global.Global.LogonName; // Fixed hardcoded string printer names + accelerators

            // -----------------------------------------------------------------------------------------------------------
            // Update has this as a post-build command line. /because of the way the update project is set up
            // I have to physically copy these files to the correct directory manually.
            // -----------------------------------------------------------------------------------------------------------
            // copy /Y "$(TargetDir)$(ProjectName).exe" "$(SolutionDir)\FP.ProcessFactory\$(OutDir)$(ProjectName).exe"
            // copy /Y "$(TargetDir)$(ProjectName).pdb" "$(SolutionDir)\FP.ProcessFactory\$(OutDir)$(ProjectName).pdb"
            // -----------------------------------------------------------------------------------------------------------

            logger.Log(LogLevel.Info, "---------------------[ Program Start ]--- :" + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToLongTimeString());
            logger.Log(LogLevel.Info, LogCode(UpTime));
            logger.Log(LogLevel.Info, LogCode(this.Text));

            Check_The_Templates_Folder_Exists();

            this.labelPFLoginValue.Text = PF.Global.Global.LogonName;

            // Bill Burr - These lead to other videos (I think)
            // https://youtu.be/z11AHUDodZk
            // https://youtu.be/_0LG2i79i-8
            //
            //


        }

        private void Check_The_Templates_Folder_Exists()
        {
            string TemplatePath = string.Empty;
            string TemplateName = string.Empty;

            DataSet ds_default = PF.Data.AccessLayer.CM_System.Get_Info_Like("PF%");
            DataRow dr_default;
            for (int i_d = 0; i_d < ds_default.Tables[0].Rows.Count; i_d++)
            {
                dr_default = ds_default.Tables[0].Rows[i_d];
                switch (dr_default["Code"].ToString())
                {
                    case "PF-TPath":
                        TemplatePath = dr_default["Value"].ToString();
                        break;

                    case "PF-TPack":
                        TemplateName = dr_default["Value"].ToString();
                        break;
                }
            }
            ds_default.Dispose();

            if (Directory.Exists(TemplatePath))
            {
                logger.Log(LogLevel.Info, LogCode("Template directory found at: " + TemplatePath));
            }
            else
            {
                logger.Log(LogLevel.Info, LogCode("Template directory was not found at: " + TemplatePath));

                MessageBox.Show("Template directory was not found at: " + TemplatePath + "\r\n\r\n" + "Printing will not function correctly", "Severe Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Maintenance_Cleaning_Area_Click(object sender, EventArgs e)
        {
            historyAdd("PF - Maintenance - Cleaning - Area");
            logger.Log(LogLevel.Info, DecorateString("PF - Maintenance - Cleaning", "Maintenance_Cleaning_Area_Click", "(Area)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.WorkOrder.Work_Order_Maintenance("PF_Cleaning_Area", int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.WorkOrder.Work_Order_Maintenance (PF - Maintenance --> Cleaning --> Area)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Maintenance_Cleaning_Parts_Click(object sender, EventArgs e)
        {
            historyAdd("PF - Maintenance - Cleaning - Parts");
            logger.Log(LogLevel.Info, DecorateString("PF - Maintenance - Cleaning", "Maintenance_Cleaning_Parts_Click", "(Parts)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.WorkOrder.Work_Order_Maintenance("PF_Cleaning_Area_Parts", int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.WorkOrder.Work_Order_Maintenance (PF - Maintenance --> Cleaning --> Parts)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Maintenance_Lab_LabResults_Click(object sender, EventArgs e)
        {
            historyAdd("PF - Maintenance - Lab - Lab Results");
            logger.Log(LogLevel.Info, DecorateString("PF - Maintenance - Lab", "Maintenance_Lab_LabResults_Click", "(Lab Results)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.WorkOrder.Work_Order_Maintenance("PF_Product_LabResults", int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.WorkOrder.Work_Order_Maintenance (PF - Maintenance --> Lab --> Lab Results)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Maintenance_Staff_Maintenance_Click(object sender, EventArgs e)
        {
            historyAdd("PF - Maintenance - Staff - Maintenance");
            logger.Log(LogLevel.Info, DecorateString("PF - Maintenance - Staff", "Maintenance_Staff_Maintenance_Click", "(Maintenance)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Utils.Common.PF_Staff(int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.Utils.Common.PF_Staff (PF - Maintenance --> Staff --> Maintenance)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Maintenance_Staff_OtherWorkAreas_Click(object sender, EventArgs e)
        {
            historyAdd("PF - Maintenance - Staff - Other Work Areas");
            logger.Log(LogLevel.Info, DecorateString("PF - Maintenance - Staff", "Maintenance_Staff_OtherWorkAreas_Click", "(Other Work Areas)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.Utils.Common.Common_Maintenance("PF_Other_Work_Types", int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.Utils.Common.Common_Maintenance (PF - Maintenance --> Staff --> Other Work Areas)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Maintenance_Types_Chemicals_Click(object sender, EventArgs e)
        {
            historyAdd("PF - Maintenance - Types - Chemicals");
            logger.Log(LogLevel.Info, DecorateString("PF - Maintenance - Types", "Maintenance_Types_Chemicals_Click", "(Chemicals)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.WorkOrder.Work_Order_Maintenance("PF_Chemicals", int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.WorkOrder.Work_Order_Maintenance (PF - Maintenance --> Types --> Chemicals)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Maintenance_Types_OtherValues_Click(object sender, EventArgs e)
        {
            historyAdd("PF - Maintenance - Types - Other Values");
            logger.Log(LogLevel.Info, DecorateString("PF - Maintenance - Types", "Maintenance_Types_OtherValues_Click", "(Other Values)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.WorkOrder.Work_Order_Maintenance("PF_Product_Other", int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.WorkOrder.Work_Order_Maintenance (PF - Maintenance --> Types --> Other Values)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Maintenance_Types_Products_Click(object sender, EventArgs e)
        {
            historyAdd("PF - Maintenance - Types - Products");
            logger.Log(LogLevel.Info, DecorateString("PF - Maintenance - Types", "Maintenance_Types_Products_Click", "(Products)"));
            Cursor = Cursors.WaitCursor;
            Form frm = new PF.WorkOrder.Work_Order_Maintenance("PF_Product", int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.WorkOrder.Work_Order_Maintenance (PF - Maintenance --> Types --> Products)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void Maintenance_Types_Tests_Click(object sender, EventArgs e)
        {
            historyAdd("PF - Maintenance - Types - Tests");
            logger.Log(LogLevel.Info, DecorateString("PF - Maintenance - Types", "Maintenance_Types_Tests_Click", "(Tests)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.WorkOrder.Work_Order_Maintenance("PF_Tests", int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.WorkOrder.Work_Order_Maintenance (PF - Maintenance --> Types --> Tests)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void maintenanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Maintenance_Staff_Maintenance_Click(this.ribbonButton_Maintenance_Staff_Maintenance, EventArgs.Empty);
            Maintenance_Staff_Maintenance_Click(this.buttonPfMaintenanceStaffMaintenance, EventArgs.Empty);
        }

        private void marketAttributeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Common_Types_Material_MarketAttribute_Click(this.ribbonButton_Common_Types_Material_MarketAttribute, EventArgs.Empty);
            Common_Types_Material_MarketAttribute_Click(this.cueComboBoxCommonTypesMaterials, EventArgs.Empty);
        }

        private void materialNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.hintedTextBox_Common_Material_MaterialNumber

            // This is a special case, and will need a small form with a textbox and button
            // rbtxt_Material_TextBoxValidated
            //(this, EventArgs.Empty);

            //MessageBox.Show("Not yet implemented.\r\nPlease use the Ribbon for the time being.",
            //    "Work in Progress", MessageBoxButtons.OK, MessageBoxIcon.Information);

            InputMaterialNumber imn = new InputMaterialNumber();
            imn.NewTextChanged += imn_NewTextChanged;
            //imn.NewGeneralPurposeObjectChanged +=imn_NewGeneralPurposeObjectChanged;

            imn.ShowDialog();

            // Unsubscribe here
            //imn.NewGeneralPurposeObjectChanged -= imn_NewGeneralPurposeObjectChanged;
            imn.NewTextChanged -= imn_NewTextChanged;
            imn.Dispose();
            imn = null;
        }

        private void materialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Common_Material_Material_Click(this.ribbonButton_Common_Material_Material, EventArgs.Empty);
            Common_Material_Material_Click(this.buttonCommonMaterialMaterial, EventArgs.Empty);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // Get a handle to a copy of this form's system (window) menu
            IntPtr hSysMenu = GetSystemMenu(this.Handle, false);

            // Add a separator
            AppendMenu(hSysMenu, MF_SEPARATOR, 0, string.Empty);

            // Add the About menu item
            AppendMenu(hSysMenu, MF_STRING, SYSMENU_ABOUT_ID, "&About…");
        }

        /// <summary>
        /// Handles the Click event of the openLogcsvToolStripMenuItem control. 27/02/2015
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void openLogcsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor;

            // Move the mouse to the center of the screen (Not multi-monitor aware)
            Cursor.Position = new Point(Screen.PrimaryScreen.Bounds.Width / 2,
                            Screen.PrimaryScreen.Bounds.Height / 2);

            // This form of syntax I've never seen before - Might be new to v4.5 of the framework?
            var processes = from p in Process.GetProcessesByName("EXCEL")
                            select p;

            foreach (var process in processes)
            {
                //Console.WriteLine("Process: " + process.ProcessName);
                //Console.WriteLine("MainWindowTitle: " + process.MainWindowTitle);

                if (process.MainWindowTitle == "Log.csv - Excel")
                {
                    Console.WriteLine("Stopping Excel...");

                    process.Kill();
                    Thread.Sleep(1000);
                }
            }
            Console.WriteLine("Starting Excel...");

            //System.Diagnostics.Process.Start("EXCEL.EXE", Application.StartupPath + @"\logs\log.csv");

            Process ExcelProcess;
            ExcelProcess = Process.Start("EXCEL.EXE", Application.StartupPath + @"\logs\log.csv");
            ExcelProcess.WaitForInputIdle();
            Thread.Sleep(1000);

            // Now that Excel is reliably started up, get a new list of processes
            var excelProcess = from p in Process.GetProcessesByName("EXCEL")
                               select p;

            foreach (var process in excelProcess)
            {
                Console.WriteLine("Process: " + process.ProcessName);
                Console.WriteLine("MainWindowTitle: " + process.MainWindowTitle);

                if (process.MainWindowTitle == "Log.csv - Excel")
                {
                    Console.WriteLine("Bingo!");
                    //None of these worked reliably, leaving here for reference
                    //IntPtr excelHandle = FindWindow("EXCEL", "Log.csv - Excel");
                    //IntPtr excelHandle = FindWindow("EXCEL", "");
                    //IntPtr excelHandle = FindWindowByCaption(IntPtr.Zero, "Log.csv - Excel");
                    IntPtr excelHandle = process.MainWindowHandle;

                    // Figured out the keyboard commands to automate formatting the spreadsheet, so...
                    // Verify that Excel is a running process.
                    if (excelHandle == IntPtr.Zero)
                    {
                        //MessageBox.Show("Excel is not running.", "Cannot find Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Console.WriteLine(" Excel not found - what other name could it be running under?");
                        return;
                    }
                    else
                    {
                        // Make Excel the foreground application and send it some keyboard shortcuts
                        SetForegroundWindow(excelHandle);
                        Thread.Sleep(500);              // Give it time to wake up...
                        SendKeys.Send("^{HOME}");       // CTRL + HOME          - Go to cell A1
                        SendKeys.Send("{TAB}");         // TAB                  - Move right one cell
                        SendKeys.Send("^( )");          // CTRL + SPACE         - Select entire column
                        SendKeys.Send("^+(2)");         // CTRL + SHIFT + 2     - Format column as DateTime
                        SendKeys.Send("^(a)");          // CTRL + A             - Select All
                        SendKeys.Send("%(hoi)");        // ALT + H + O + I      - Size all columns to fit
                                                        // -------------------------------------------------------------------------------
                                                        // -------------------------------------------------------------------------------
                                                        //string date = DateTime.Now.ToShortDateString();
                                                        //string time = DateTime.Now.ToShortTimeString();

                        // This works - sort of. Not consistently though, so is disabled for now.
                        //SendKeys.Send("+( )");                    // SHIFT + SPACE        - Select the current row
                        //SendKeys.Send("%(ir)");                   // ALT + I + R          - Insert row above current
                        //SendKeys.Send(date);                      // Type DATE into cell
                        //SendKeys.Send("{TAB}");                   // TAB                  - Move right one cell
                        //SendKeys.Send(time);                      // Type TIME into cell
                        //SendKeys.Send("{TAB}");                   // TAB                  - Move right one cell
                        //SendKeys.Send("Auto");                    // Enter "Auto" as text
                        //SendKeys.Send("{TAB}");                   // TAB                  - Move right one cell
                        //SendKeys.Send("Finished automation");     // Enter "Finished automation" as text
                        //SendKeys.Send("^{HOME}");                 // CTRL + HOME          - Go to cell A1
                    }
                }
            }
            // Set cursor as default arrow
            Cursor.Current = Cursors.Default;
        }

        private void orchardistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Common_Grower_Orchardist_Click(this.ribbonButton_Common_Grower_Orchardist, EventArgs.Empty);
            Common_Grower_Orchardist_Click(this.buttonCommonGrowerOrchardist, EventArgs.Empty);
        }

        private void orderInvoiceLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Accounting_Invoicing_OrderInvoiceLink_Click(this.ribbonButton_Accounting_Invoicing_OrderInvoiceLink, EventArgs.Empty);
            Accounting_Invoicing_OrderInvoiceLink_Click(this.buttonAccountingInvoicingOrderInvoiceLink, EventArgs.Empty);
        }

        private void orderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Accounting_Invoicing_Intent_Order_Click(this.ribbonButton_Accounting_Invoicing_Intent_Order, EventArgs.Empty);

            Accounting_Invoicing_Intent_Order_Click(this.buttonAccountingIntentOrder, EventArgs.Empty);
        }

        private void otherValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Maintenance_Types_OtherValues_Click(this.ribbonButton_Maintenance_Types_OtherValues, EventArgs.Empty);
            Maintenance_Types_OtherValues_Click(this.buttonPfMaintenanceTypesOtherValues, EventArgs.Empty);
        }

        private void otherWorkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Dispatch_Other_OtherWork_Click(this.ribbonButton_Dispatch_Other_OtherWork, EventArgs.Empty);

            Dispatch_Other_OtherWork_Click(this.buttonDispatchOtherOtherWork, EventArgs.Empty);
        }

        private void outputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_ProcessFactory_WorkOrders_Output_Click(this.ribbonButton_ProcessFactory_WorkOrders_Output, EventArgs.Empty);
            ProcessFactory_WorkOrders_Output_Click(this.buttonProcessFactoryWorkOrdersOutput, EventArgs.Empty);
        }

        private void packToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Dispatch_Packing_Pack_Click(this.ribbonButton_Dispatch_Packing_Pack, EventArgs.Empty);

            Dispatch_Packing_Pack_Click(this.buttonDispatchPackingPack, EventArgs.Empty);
        }

        private void packTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Common_Types_Types_PackTypes_Click(this.ribbonButton_Common_Types_Types_PackTypes, EventArgs.Empty);
            Common_Types_Types_PackTypes_Click(this.cueComboBoxCommonTypes, EventArgs.Empty);
        }

        private void palletsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Common_Types_Types_Pallets_Click(this.ribbonButton_Common_Types_Types_Pallets, EventArgs.Empty);
            Common_Types_Types_Pallets_Click(this.cueComboBoxCommonTypes, EventArgs.Empty);
        }

        private void palletsToolStripMenuItem_ProcessFactory_Click(object sender, EventArgs e)
        {
            //ribbonButton_ProcessFactory_WorkOrders_Pallets_Click(this.ribbonButton_ProcessFactory_WorkOrders_Pallets, EventArgs.Empty);
            ProcessFactory_WorkOrders_Pallets_Click(this.buttonProcessFactoryWorkOrdersPallets, EventArgs.Empty);
        }

        private void palletTransferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Dispatch_Scanning_PalletTransfer_Click(this.ribbonButton_Dispatch_Scanning_PalletTransfer, EventArgs.Empty);

            Dispatch_Scanning_PalletTransfer_Click(this.buttonDispatchScanningPalletTransfer, EventArgs.Empty);
        }

        private void palletWeightsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Extra_Scanning_PalletWeights_Click(this.ribbonButton_Extra_Scanning_PalletWeights, EventArgs.Empty);
            Extra_Scanning_PalletWeights_Click(this.buttonExtraScanningPalletWeights, EventArgs.Empty);
        }

        private void panelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Security_Menus_Panels_Click(this.ribbonButton_Security_Menus_Panels, EventArgs.Empty);
            Security_Menus_Panels_Click(this.buttonSecurityMenusPanels, EventArgs.Empty);
        }

        private void partsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Maintenance_Cleaning_Parts_Click(this.ribbonButton_Maintenance_Cleaning_Parts, EventArgs.Empty);
            Maintenance_Cleaning_Parts_Click(this.buttonPfMaintenanceCleaningParts, EventArgs.Empty);
        }

        private void paymentsReceivedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Accounting_Invoicing_PaymentReceived_Click(this.ribbonButton_Accounting_Invoicing_PaymentReceived, EventArgs.Empty);
            Accounting_Invoicing_PaymentReceived_Click(this.buttonAccountingInvoicingPaymentsReceived, EventArgs.Empty);
        }

        private void pFMaintenanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.ribbon1.ActiveTab = this.ribbonTab_Maintenance;
            this.tabControlMain.SelectedTab = tabPageMaintenance;
        }

        private void pFMaintenanceToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            //this.ribbon1.ActiveTab = this.ribbonTab_Maintenance;
            //this.tabControlMain.SelectedTab = tabPageMaintenance;
            //this.toolStripStatusLabelActive.Text = this.rbt_PF_Maintenance.Text + " tab selected.";
        }

        private void populate_Baseload_Comboboxes()
        {
            try
            {
                string description = string.Empty;
                string value = string.Empty;
                object tag = null;

                DataSet ds_Get_Info;

                ds_Get_Info = PF.Data.AccessLayer.PF_BaseLoad.Get_Info();

                //ribbonCombo_BaseLoad_BaseLoadList.DropDownItems.Clear();

                // New - 12/05/2015 BN
                cueComboBoxDispatchBaseLoadList.Items.Clear();

                for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
                {
                    //rbcmb_BaseLoad1 = new RibbonButton();

                    dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                    //rbcmb_BaseLoad1.Text = dr_Get_Info["Description"].ToString() + " (" + dr_Get_Info["Work_Order_Id"].ToString() + ")";
                    //rbcmb_BaseLoad1.Value = dr_Get_Info["Work_Order_Id"].ToString();
                    //rbcmb_BaseLoad1.Tag = dr_Get_Info["BaseLoad_Id"].ToString();

                    //ribbonCombo_BaseLoad_BaseLoadList.DropDownItems.Add(rbcmb_BaseLoad1);

                    // New - 12/05/2015 BN
                    description = dr_Get_Info["Description"].ToString();
                    value = dr_Get_Info["Work_Order_Id"].ToString();
                    tag = dr_Get_Info["BaseLoad_Id"].ToString();

                    ComboboxItem item = new ComboboxItem();
                    item.Text = description;
                    item.Value = value;
                    item.Tag = tag;

                    cueComboBoxDispatchBaseLoadList.Items.Add(item);
                }
                ds_Get_Info.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void populate_Date_Combo()
        {
            //Not Completed Process Date
            if (int_prod_combo == 0)
            {
                ds_Get_Info = PF.Data.AccessLayer.PF_Work_Order.Get_Combo_Date();
            }
            else
            {
                ds_Get_Info = PF.Data.AccessLayer.PF_Work_Order.Get_Combo_Date(int_prod_combo);
            }
            //ribbonCombo_ProcessFactory_WorkOrders_Date.DropDownItems.Clear();

            // New - 12/05/2015 BN
            cueComboBoxProcessFactoryWorkOrdersSelectDate.Items.Clear();

            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                //rbtn_Combo_Date = new RibbonButton();

                // New - 12/05/2015 BN
                ComboboxItem item = new ComboboxItem();

                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                //rbtn_Combo_Date.Text = dr_Get_Info["Process_Date"].ToString();

                // New - 12/05/2015 BN
                item.Text = dr_Get_Info["Process_Date"].ToString();

                //ribbonCombo_ProcessFactory_WorkOrders_Date.DropDownItems.Add(rbtn_Combo_Date);

                // New - 12/05/2015 BN
                cueComboBoxProcessFactoryWorkOrdersSelectDate.Items.Add(item);
            }

            ds_Get_Info.Dispose();
            populate_Prod_Combo();
            populate_WO_Combo();
        }

        /// <summary>
        /// New 12/05/2015 BN
        /// </summary>
        private void populate_Location()
        {
            // GroupedComboBox: define our collection of list items
            var groupedItems = new[] {
                new { Group = " ", Value = 0, Display = "Please Select" },
                new { Group = "Offsite", Value = 1, Display = "Cities" },
                new { Group = "Offsite", Value = 2, Display = "Trucks" },
                new { Group = "Onsite", Value = 3, Display = "Location" }
            };

            // Grouped combobox
            this.groupedComboBoxCommonLocation.ValueMember = "Value";
            this.groupedComboBoxCommonLocation.DisplayMember = "Display";
            this.groupedComboBoxCommonLocation.GroupMember = "Group";
            this.groupedComboBoxCommonLocation.DataSource = new BindingSource(groupedItems, String.Empty);
            //this.groupedComboBoxLocation.DropDownHeight = 150;
        }

        private void populate_Prod_Combo()
        {
            //Not Completed
            if (str_date_combo == "")
            {
                ds_Get_Info = PF.Data.AccessLayer.PF_Product.Get_Info();
            }
            else
            {
                ds_Get_Info = PF.Data.AccessLayer.PF_Work_Order.Get_Combo_Prod_by_Date(str_date_combo);
            }
            //ribbonCombo_ProcessFactory_WorkOrders_Product.DropDownItems.Clear();

            // New - 12/05/2015 BN
            cueComboBoxProcessFactoryWorkOrdersSelectProduct.Items.Clear();

            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                //rbtn_Combo_Product = new RibbonButton();

                // New - 12/05/2015 BN
                ComboboxItem item = new ComboboxItem();

                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                //rbtn_Combo_Product.Text = dr_Get_Info["Code"].ToString();
                //rbtn_Combo_Product.Value = dr_Get_Info["Product_Id"].ToString();

                // New - 12/05/2015 BN
                item.Text = dr_Get_Info["Code"].ToString();
                item.Value = dr_Get_Info["Product_Id"].ToString();

                //ribbonCombo_ProcessFactory_WorkOrders_Product.DropDownItems.Add(rbtn_Combo_Product);

                // New - 12/05/2015 BN
                cueComboBoxProcessFactoryWorkOrdersSelectProduct.Items.Add(item);
            }
            ds_Get_Info.Dispose();
        }

        private void populate_Reports()
        {
            try
            {
                string description = string.Empty;
                string value = string.Empty;

                ds_Get_Info = PF.Data.AccessLayer.PF_Reports.Get_Info();

                //ribbonCombo_Reports_SSRS_ReportList.DropDownItems.Clear();

                for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
                {
                    //rbcmb_Report_1 = new RibbonButton();

                    dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                    //rbcmb_Report_1.Text = dr_Get_Info["Description"].ToString();
                    //rbcmb_Report_1.Value = dr_Get_Info["Value"].ToString();

                    description = dr_Get_Info["Description"].ToString();
                    value = dr_Get_Info["Value"].ToString();

                    //ribbonCombo_Reports_SSRS_ReportList.DropDownItems.Add(rbcmb_Report_1);

                    ComboboxItem item = new ComboboxItem();
                    item.Text = description;
                    item.Value = value;

                    cueComboBoxReportsSSRSReportList.Items.Add(item);
                }

                ds_Get_Info.Dispose();

                //cueComboBoxReportsSSRSReportList.DisplayMember = comboSource[0];
                //cueComboBoxReportsSSRSReportList.ValueMember = value;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// New 12/05/2015 BN
        /// </summary>
        private void populate_Types()
        {
            //ComboboxItem item = new ComboboxItem();
            //item.Text = description;
            //item.Value = value;

            cueComboBoxCommonTypes.Items.Add("Defect");
            cueComboBoxCommonTypes.Items.Add("Defect Class");
            cueComboBoxCommonTypes.Items.Add("Pack Types");
            cueComboBoxCommonTypes.Items.Add("Pallets");
            cueComboBoxCommonTypes.Items.Add("Storage");

            cueComboBoxCommonTypesMaterials.Items.Add("Brand");
            cueComboBoxCommonTypesMaterials.Items.Add("Count");
            cueComboBoxCommonTypesMaterials.Items.Add("Grade");
            cueComboBoxCommonTypesMaterials.Items.Add("Growing Method");
            cueComboBoxCommonTypesMaterials.Items.Add("Market Attribute");
            cueComboBoxCommonTypesMaterials.Items.Add("Size Treatment");
            cueComboBoxCommonTypesMaterials.Items.Add("Treatment");
        }

        private void populate_WO_Combo()
        {
            //Not Completed
            if (str_date_combo == "" && int_prod_combo == 0)
            {
                ds_Get_Info = PF.Data.AccessLayer.PF_Work_Order.Get_Combo_Work_Order();
            }
            else if (str_date_combo != "" && int_prod_combo == 0)
            {
                ds_Get_Info = PF.Data.AccessLayer.PF_Work_Order.Get_Combo_Work_Order(str_date_combo);
            }
            else if (str_date_combo == "" && int_prod_combo != 0)
            {
                ds_Get_Info = PF.Data.AccessLayer.PF_Work_Order.Get_Combo_Work_Order(int_prod_combo);
            }
            else
            {
                ds_Get_Info = PF.Data.AccessLayer.PF_Work_Order.Get_Combo_Work_Order(str_date_combo, int_prod_combo);
            }
            //ribbonCombo_ProcessFactory_WorkOrders_WorkOrder.DropDownItems.Clear();

            // New - 12/05/2015 BN
            cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.Items.Clear();

            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                //rbtn_Combo_Work_Order = new RibbonButton();

                // New - 12/05/2015 BN
                ComboboxItem item = new ComboboxItem();

                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                //rbtn_Combo_Work_Order.Text = dr_Get_Info["Work_Order_Id"].ToString() + " (" + dr_Get_Info["Process_Date"].ToString() + ")";
                //rbtn_Combo_Work_Order.Value = dr_Get_Info["Work_Order_Id"].ToString();

                // New - 12/05/2015 BN
                item.Text = dr_Get_Info["Work_Order_Id"].ToString() + " (" + dr_Get_Info["Process_Date"].ToString() + ")";
                item.Value = dr_Get_Info["Work_Order_Id"].ToString();

                // I wondered what this was for - it puts the little green blob next to the entry.
                if (Convert.ToBoolean(dr_Get_Info["Current_Ind"].ToString()) == true)
                {
                    //rbtn_Combo_Work_Order.SmallImage = PF.Global.Properties.Resources.Current_sml;

                    item.Text = item.Text + " <--"; // New - 12/05/2015 BN - Can't use a graphic so just draw an arrow

                    int_current_WO_Id = Convert.ToInt32(dr_Get_Info["Work_Order_Id"].ToString());
                }

                //ribbonCombo_ProcessFactory_WorkOrders_WorkOrder.DropDownItems.Add(rbtn_Combo_Work_Order);

                // New - 12/05/2015 BN
                cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.Items.Add(item);
            }
            ds_Get_Info.Dispose();
        }

        private void printPalletCardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Dispatch_Scanning_PrintPalletCard_Click(this.ribbonButton_Dispatch_Scanning_PrintPalletCard, EventArgs.Empty);

            Dispatch_Scanning_PrintPalletCard_Click(this.buttonDispatchScanningPrintPalletCard, EventArgs.Empty);
        }

        private void printTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var print = new PrintHelper();
            //print.PrintPreviewTree(this.treeViewUndoRedo, "FP History");
        }

        private void ProcessFactory_Cleaning_Cleaning_Click(object sender, EventArgs e)
        {
            historyAdd("Process Factory - Cleaning - Cleaning");
            logger.Log(LogLevel.Info, DecorateString("Process Factory - Cleaning", "ProcessFactory_Cleaning_Cleaning_Click", "(Cleaning)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.WorkOrder.Cleaning(int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.WorkOrder.Cleaning (Process Factory --> Cleaning --> Cleaning)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void ProcessFactory_CompleteWorkOrders_Complete_Click(object sender, EventArgs e)
        {
            historyAdd("Process Factory - Complete Work Orders - Complete");
            logger.Log(LogLevel.Info, DecorateString("Process Factory - Complete Work Orders", "ProcessFactory_CompleteWorkOrders_Complete_Click", "(Complete)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.WorkOrder.WO_Complete(int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.WorkOrder.WO_Complete (Process Factory --> Complete Work Orders --> Complete)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void ProcessFactory_Setup_Factory_Click(object sender, EventArgs e)
        {
            historyAdd("Process Factory - Setup - Factory");
            logger.Log(LogLevel.Info, DecorateString("Process Factory - Setup", "ProcessFactory_Setup_Factory_Click", "(Factory)"));

            Cursor = Cursors.WaitCursor;
            Form frm = new PF.WorkOrder.Process_Set_Up(int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.WorkOrder.Process_Set_Up (Process Factory --> Setup --> Factory)";
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
            Cursor = Cursors.Default;
        }

        private void ProcessFactory_WorkOrders_Chemicals_Click(object sender, EventArgs e)
        {
            historyAdd("Process Factory - Work Orders - Chemicals");
            logger.Log(LogLevel.Info, DecorateString("Process Factory - Work Orders", "ProcessFactory_WorkOrders_Chemicals_Click", "(Chemicals)"));

            labelTabControlMainBottomStatus.ForeColor = SystemColors.ControlText;
            labelTabControlMainBottomStatus.Text = "";

            DialogResult DLR_MessageBox = new DialogResult();
            Cursor = Cursors.WaitCursor;
            int int_WO_Id = 0;
            //cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder

            if (cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedItem != null && cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.Text != "Work Order")
            {
                //int_WO_Id = Convert.ToInt32(cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedValue.ToString());

                ComboboxItem cbi = (ComboboxItem)cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedItem;
                string tempString = cbi.Value.ToString();
                int_WO_Id = Convert.ToInt32(tempString);
            }
            if (int_WO_Id == 0 && int_current_WO_Id == 0)
            {
                DLR_MessageBox = MessageBox.Show("Invalid Work Order - Please select a valid Work Order from the Drop Down List.", "FP - Menu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (int_WO_Id == 0 && int_current_WO_Id > 0)
            {
                int_WO_Id = int_current_WO_Id;
            }
            if (DLR_MessageBox != DialogResult.OK)
            {
                Form frm = new PF.WorkOrder.WO_Chemicals(int_WO_Id, int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
                frm.Text = "PF.WorkOrder.WO_Chemicals (Process Factory --> Work Orders --> Chemicals)";
                //frm.TopMost = true; // 21/10/2015 BN
                frm.Show();
            }
            Cursor = Cursors.Default;
        }

        private void ProcessFactory_WorkOrders_CreateView_Click(object sender, EventArgs e)
        {
            historyAdd("Process Factory - Work Orders - Create/View");

            logger.Log(LogLevel.Info, DecorateString("Process Factory - Work Orders", "ProcessFactory_WorkOrders_CreateView_Click", "(Create / View)"));

            labelTabControlMainBottomStatus.ForeColor = SystemColors.ControlText;
            labelTabControlMainBottomStatus.Text = "";

            Cursor = Cursors.WaitCursor;
            int int_WO_Id = 0;
            //cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder
            //
            //if (cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedItem != null && cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.Text != "Work Order")

            if (cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.Text != "Select Work Order" && cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.Text != "")
            {
                //string strValue = cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedItem.GetType().ToString();
                // 4/8/2015 Found stupid mistake causing the work orders to come
                // up blank

                // BN added 18/111/2015

                int index = cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.FindString(cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.Text);
                cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedIndex = index;

                ComboboxItem cbi = (ComboboxItem)cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedItem;
                string tempString = cbi.Value.ToString();

                // First, if the work order is current, it will have a " <--"
                // appended to it, so strip that off
                //if (cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedItem.ToString().EndsWith(" <--"))
                //{
                //    tempString = BnExtension.TrimStringFromEnd(cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedItem.ToString(), " <--");
                //}
                //else
                //{
                //    tempString = cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedItem.ToString();
                //}
                //int_WO_Id = Convert.ToInt32(cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedValue.ToString());

                // Now there's another problem - the date is included
                //ComboBoxItem cbi = (ComboBoxItem)cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedItem;

                int_WO_Id = Convert.ToInt32(tempString);
            }
            Form frm = new PF.WorkOrder.WO_Create(int_WO_Id, int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PF.WorkOrder.WO_Create (Process Factory --> Work Orders --> Create/View)";
            frm.ShowDialog();

            populate_Date_Combo();
            populate_Prod_Combo();
            populate_WO_Combo();
            Cursor = Cursors.Default;
        }

        private void ProcessFactory_WorkOrders_DeleteWorkOrder_Click(object sender, EventArgs e)
        {
            historyAdd("Process Factory - Work Orders - Delete Work Order");
            logger.Log(LogLevel.Info, DecorateString("Process Factory - Work Orders", "ProcessFactory_WorkOrders_DeleteWorkOrder_Click", "(Delete Work Order)"));

            labelTabControlMainBottomStatus.ForeColor = SystemColors.ControlText;
            labelTabControlMainBottomStatus.Text = "";

            DialogResult DLR_MessageBox = new DialogResult();
            Cursor = Cursors.WaitCursor;
            int int_WO_Id = 0;
            //cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder
            if (cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedItem != null)
            {
                ComboboxItem cbi = (ComboboxItem)cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedItem;
                if (cbi.Value != null && cbi.Text != "Work Order")
                //if (cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedValue != null && cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.Text != "Work Order")
                {
                    int_WO_Id = Convert.ToInt32(cbi.Value.ToString());
                }
                if (int_WO_Id == 0)
                {
                    DLR_MessageBox = MessageBox.Show("Invalid Work Order - Please select a valid Work Order from the Drop Down List.", "FP - Menu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DLR_MessageBox = MessageBox.Show("Are you sure you want to delete Work Order " + Convert.ToString(int_WO_Id), "FP - Menu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
                if (DLR_MessageBox == DialogResult.Yes)
                {
                    try
                    {
                        try
                        {
                            PF.Data.AccessLayer.PF_Work_Order_Material_Relationship.Delete_WO(int_WO_Id);
                        }
                        catch (Exception ex)
                        {
                            logger.Log(LogLevel.Debug, ex.Message);
                        }
                        PF.Data.AccessLayer.PF_Work_Order_Staff_Relationship.Delete_WO(int_WO_Id);
                        PF.Data.AccessLayer.PF_Work_Order.Delete(int_WO_Id);

                        lbl_message.Text = "Work Order " + int_WO_Id + " has been deleted";
                        lbl_message.ForeColor = System.Drawing.Color.Blue;

                        labelTabControlMainBottomStatus.ForeColor = System.Drawing.Color.Blue;
                        labelTabControlMainBottomStatus.Text = "Work Order " + int_WO_Id + " has been deleted";
                        Reset();
                    }
                    catch
                    {
                        lbl_message.Text = "Work Order " + int_WO_Id + " failed to delete";
                        lbl_message.ForeColor = System.Drawing.Color.Red;

                        labelTabControlMainBottomStatus.ForeColor = System.Drawing.Color.Red;
                        labelTabControlMainBottomStatus.Text = "Work Order " + int_WO_Id + " failed to delete";
                    }
                }
            }
            else
            {
                MessageBox.Show("Nothing to do", "Nothing Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            Cursor = Cursors.Default;
        }

        private void ProcessFactory_WorkOrders_Output_Click(object sender, EventArgs e)
        {
            historyAdd("Process Factory - Work Orders - Output");
            logger.Log(LogLevel.Info, DecorateString("Process Factory - Work Orders", "ProcessFactory_WorkOrders_Output_Click", "(Output)"));

            labelTabControlMainBottomStatus.ForeColor = SystemColors.ControlText;
            labelTabControlMainBottomStatus.Text = "";

            DialogResult DLR_MessageBox = new DialogResult();
            Cursor = Cursors.WaitCursor;
            int int_WO_Id = 0;
            //cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder

            if (cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedItem != null && cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.Text != "Work Order")
            {
                //int_WO_Id = Convert.ToInt32(cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedValue.ToString());

                ComboboxItem cbi = (ComboboxItem)cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedItem;
                string tempString = cbi.Value.ToString();
                int_WO_Id = Convert.ToInt32(tempString);
            }
            if (int_WO_Id == 0 && int_current_WO_Id == 0)
            {
                DLR_MessageBox = MessageBox.Show("Invalid Work Order - Please select a valid Work Order from the Drop Down List.", "FP - Menu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (int_WO_Id == 0 && int_current_WO_Id > 0)
            {
                int_WO_Id = int_current_WO_Id;
            }
            if (DLR_MessageBox != DialogResult.OK)
            {
                Form frm = new PF.WorkOrder.WO_Output(int_WO_Id, int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
                frm.Text = "PF.WorkOrder.WO_Output (Process Factory --> Work Orders --> Output)";
                //frm.TopMost = true; // 21/10/2015 BN
                frm.Show();
            }
            Cursor = Cursors.Default;
        }

        private void ProcessFactory_WorkOrders_Pallets_Click(object sender, EventArgs e)
        {
            historyAdd("Process Factory - Work Orders - Pallets");
            logger.Log(LogLevel.Info, DecorateString("Process Factory - Work Orders", "ProcessFactory_WorkOrders_Pallets_Click", "(Pallets)"));

            labelTabControlMainBottomStatus.ForeColor = SystemColors.ControlText;
            labelTabControlMainBottomStatus.Text = "";

            DialogResult DLR_MessageBox = new DialogResult();
            Cursor = Cursors.WaitCursor;
            int int_WO_Id = 0;
            //cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder

            if (cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedItem != null && cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.Text != "Work Order")
            {
                //int_WO_Id = Convert.ToInt32(cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedValue.ToString());

                ComboboxItem cbi = (ComboboxItem)cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedItem;
                string tempString = cbi.Value.ToString();
                int_WO_Id = Convert.ToInt32(tempString);
            }
            if (int_WO_Id == 0 && int_current_WO_Id == 0)
            {
                DLR_MessageBox = MessageBox.Show("Invalid Work Order - Please select a valid Work Order from the Drop Down List.", "FP - Menu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (int_WO_Id == 0 && int_current_WO_Id > 0)
            {
                int_WO_Id = int_current_WO_Id;
            }
            if (DLR_MessageBox != DialogResult.OK)
            {
                Form frm = new PF.WorkOrder.WO_Labels(int_WO_Id, int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
                frm.Text = "PF.WorkOrder.WO_Labels (Process Factory --> Work Orders --> Pallets)";
                //frm.TopMost = true; // 21/10/2015 BN
                frm.Show();
            }
            Cursor = Cursors.Default;
        }

        private void ProcessFactory_WorkOrders_QC_Click(object sender, EventArgs e)
        {
            historyAdd("Process Factory - Work Orders - QC");
            logger.Log(LogLevel.Info, DecorateString("Process Factory - Work Orders", "ProcessFactory_WorkOrders_QC_Click", "(QC)"));

            labelTabControlMainBottomStatus.ForeColor = SystemColors.ControlText;
            labelTabControlMainBottomStatus.Text = "";

            DialogResult DLR_MessageBox = new DialogResult();
            Cursor = Cursors.WaitCursor;
            int int_WO_Id = 0;
            //cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder

            if (cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedItem != null && cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.Text != "Work Order")
            {
                //int_WO_Id = Convert.ToInt32(cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedValue.ToString());

                ComboboxItem cbi = (ComboboxItem)cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedItem;
                string tempString = cbi.Value.ToString();
                int_WO_Id = Convert.ToInt32(tempString);
            }
            if (int_WO_Id == 0 && int_current_WO_Id == 0)
            {
                DLR_MessageBox = MessageBox.Show("Invalid Work Order - Please select a valid Work Order from the Drop Down List.", "FP - Menu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (int_WO_Id == 0 && int_current_WO_Id > 0)
            {
                int_WO_Id = int_current_WO_Id;
            }
            if (DLR_MessageBox != DialogResult.OK)
            {
                Form frm = new PF.WorkOrder.QC(int_WO_Id, int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
                frm.Text = "PF.WorkOrder.QC (Process Factory --> Work Orders --> QC)";
                //frm.TopMost = true; // 21/10/2015 BN
                frm.Show();
            }
            Cursor = Cursors.Default;
        }

        private void ProcessFactory_WorkOrders_Reset_Click(object sender, EventArgs e)
        {
            historyAdd("Process Factory - Work Orders - Reset");
            logger.Log(LogLevel.Info, DecorateString("Process Factory - Work Orders", "ProcessFactory_WorkOrders_Reset_Click", "(Reset)"));

            labelTabControlMainBottomStatus.ForeColor = SystemColors.ControlText;
            labelTabControlMainBottomStatus.Text = "";

            Reset();

            labelTabControlMainBottomStatus.ForeColor = SystemColors.ControlText;
            labelTabControlMainBottomStatus.Text = "Work Orders Reset.";
        }

        private void ProcessFactory_WorkOrders_SetCurrent_Click(object sender, EventArgs e)
        {
            historyAdd("Process Factory - Work Orders - Set Current");
            logger.Log(LogLevel.Info, DecorateString("Process Factory - Work Orders", "ProcessFactory_WorkOrders_SetCurrent_Click", "(Set Current)"));

            labelTabControlMainBottomStatus.ForeColor = SystemColors.ControlText;
            labelTabControlMainBottomStatus.Text = "";

            DialogResult DLR_MessageBox = new DialogResult();
            int int_result = 0;
            Cursor = Cursors.WaitCursor;
            int int_WO_Id = 0;
            //cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder
            // ComboboxItem
            //if (cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedItem != null)
                if (cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.Text != null)
                {
                // BN added 18/111/2015
                int index = cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.FindString(cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.Text);
                cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedIndex = index;

                ComboboxItem cbi = (ComboboxItem)cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedItem;
                if (cbi.Value != null && cbi.Text != "Select Work Order")
                //if (cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedItem != null && cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.Text != "Work Order")
                {
                    //int_WO_Id = Convert.ToInt32(cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedValue.ToString());
                    int_WO_Id = Convert.ToInt32(cbi.Value);
                }
                if (int_WO_Id == 0)
                {
                    DLR_MessageBox = MessageBox.Show("Invalid Work Order - Please select a valid Work Order from the Drop Down List.", "FP - Menu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (DLR_MessageBox != DialogResult.OK)
                {
                    DataSet ds = PF.Data.AccessLayer.PF_Work_Order.Get_Current();
                    DataRow dr;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dr = ds.Tables[0].Rows[i];
                        PF.Data.AccessLayer.PF_Work_Order.Update_Current_Ind(Convert.ToInt32(dr["Work_Order_Id"].ToString()), false, int_User_id);
                    }
                    ds.Dispose();
                    int_result = PF.Data.AccessLayer.PF_Work_Order.Update_Current_Ind(int_WO_Id, true, int_User_id);
                }

                if (int_result > 0)
                {
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    lbl_message.Text = "Work Order: " + Convert.ToString(cbi.Value.ToString()) + " has been set to Current";

                    labelTabControlMainBottomStatus.ForeColor = System.Drawing.Color.Blue;
                    labelTabControlMainBottomStatus.Text = "Work Order: " + Convert.ToString(cbi.Value.ToString()) + " has been set to Current";
                    Reset();
                }
            }
            else
            {
                MessageBox.Show("Nothing to do", "Nothing selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            Cursor = Cursors.Default;
        }

        private void ProcessFactory_WorkOrders_Staff_Click(object sender, EventArgs e)
        {
            historyAdd("Process Factory - Work Orders - Staff");
            logger.Log(LogLevel.Info, DecorateString("Process Factory - Work Orders", "ProcessFactory_WorkOrders_Staff_Click", "(Staff)"));

            labelTabControlMainBottomStatus.ForeColor = SystemColors.ControlText;
            labelTabControlMainBottomStatus.Text = "";

            DialogResult DLR_MessageBox = new DialogResult();
            Cursor = Cursors.WaitCursor;
            int int_WO_Id = 0;
            //cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder

            if (cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedItem != null && cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.Text != "Work Order")
            {
                //int_WO_Id = Convert.ToInt32(cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedValue.ToString());
                ComboboxItem cbi = (ComboboxItem)cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedItem;
                string tempString = cbi.Value.ToString();
                int_WO_Id = Convert.ToInt32(tempString);
            }
            if (int_WO_Id == 0 && int_current_WO_Id == 0)
            {
                DLR_MessageBox = MessageBox.Show("Invalid Work Order - Please select a valid Work Order from the Drop Down List.", "FP - Menu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (int_WO_Id == 0 && int_current_WO_Id > 0)
            {
                int_WO_Id = int_current_WO_Id;
            }
            if (DLR_MessageBox != DialogResult.OK)
            {
                Form frm = new PF.WorkOrder.WO_Staff(int_WO_Id, int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
                frm.Text = "PF.WorkOrder.WO_Staff (Process Factory --> Work Orders --> Staff)";
                //frm.TopMost = true; // 21/10/2015 BN
                frm.Show();
            }
            Cursor = Cursors.Default;
        }

        private void ProcessFactory_WorkOrders_Tests_Click(object sender, EventArgs e)
        {
            historyAdd("Process Factory - Work Orders - Tests");
            logger.Log(LogLevel.Info, DecorateString("Process Factory - Work Orders", "ProcessFactory_WorkOrders_Tests_Click", "(Tests)"));

            labelTabControlMainBottomStatus.ForeColor = SystemColors.ControlText;
            labelTabControlMainBottomStatus.Text = "";

            DialogResult DLR_MessageBox = new DialogResult();
            Cursor = Cursors.WaitCursor;
            int int_WO_Id = 0;
            //cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder

            if (cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedItem != null && cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.Text != "Work Order")
            {
                //int_WO_Id = Convert.ToInt32(cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedValue.ToString());

                ComboboxItem cbi = (ComboboxItem)cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.SelectedItem;
                string tempString = cbi.Value.ToString();
                int_WO_Id = Convert.ToInt32(tempString);
            }
            if (int_WO_Id == 0 && int_current_WO_Id == 0)
            {
                DLR_MessageBox = MessageBox.Show("Invalid Work Order - Please select a valid Work Order from the Drop Down List.", "FP - Menu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (int_WO_Id == 0 && int_current_WO_Id > 0)
            {
                int_WO_Id = int_current_WO_Id;
            }
            if (DLR_MessageBox != DialogResult.OK)
            {
                Form frm = new PF.WorkOrder.WO_Tests(int_WO_Id, int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
                frm.Text = "PF.WorkOrder.WO_Tests (Process Factory --> Work Orders --> Tests)";
                //frm.TopMost = true; // 21/10/2015 BN
                frm.Show();
            }
            Cursor = Cursors.Default;
        }

        private void processFactoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.ribbon1.ActiveTab = this.ribbonTab_ProcessFactory;
            this.tabControlMain.SelectedTab = tabPageProcessFactory;
        }

        private void processFactoryToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            //this.ribbon1.ActiveTab = this.ribbonTab_ProcessFactory;
            //this.tabControlMain.SelectedTab = tabPageProcessFactory;
            //this.toolStripStatusLabelActive.Text = this.rbt_PF.Text + " tab selected.";
        }

        private void productGroupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Common_Groups_ProductGroups_Click(this.ribbonButton_Common_Groups_ProductGroups, EventArgs.Empty);
            Common_Groups_ProductGroups_Click(this.buttonCommonGroupsProductGroups, EventArgs.Empty);
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Maintenance_Types_Products_Click(this.ribbonButton_Maintenance_Types_Products, EventArgs.Empty);
            Maintenance_Types_Products_Click(this.buttonPfMaintenanceTypesProducts, EventArgs.Empty);
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cueComboBoxProcessFactoryWorkOrdersSelectProduct.Focus();

            // Special Case
            //rcmb_Product_DropDownItemClicked
            //(this, EventArgs.Empty);

            //MessageBox.Show("Not yet implemented.\r\nPlease use the Ribbon for the time being.",
            //    "Work in Progress", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void qCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_ProcessFactory_WorkOrders_QC_Click(this.ribbonButton_ProcessFactory_WorkOrders_QC, EventArgs.Empty);
            ProcessFactory_WorkOrders_QC_Click(this.buttonProcessFactoryWorkOrdersQC, EventArgs.Empty);
        }

        private void ratesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Accounting_Costs_Rates_Click(this.ribbonButton_Accounting_Costs_Rates, EventArgs.Empty);
            Accounting_Costs_Rates_Click(this.buttonAccountingCostsRates, EventArgs.Empty);
        }

        private void reporsToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            //this.ribbon1.ActiveTab = this.ribbonTab_Reports;
            //this.tabControlMain.SelectedTab = tabPageReports;
            //this.toolStripStatusLabelActive.Text = this.rbt_Reports.Text + " tab selected.";
        }

        private void reportListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cueComboBoxReportsSSRSReportList.Focus();
        }

        private void Reports_Maintenance_AddNewReport_Click(object sender, EventArgs e)
        {
            historyAdd("Reports - Maintenance - Add New Report");
            logger.Log(LogLevel.Info, DecorateString("Reports - Maintenance", "Reports_Maintenance_AddNewReport_Click", "(Add New Report)"));

            Cursor = Cursors.WaitCursor;

            Form frm = new PF.WorkOrder.Work_Order_Maintenance("PF_Reports", int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm.Text = "PPF.WorkOrder.Work_Order_Maintenance (Reports --> Maintenance --> Add New Report)";
            frm.ShowDialog();

            Cursor = Cursors.Default;
            populate_Reports();
        }

        private void Reports_SSRS_Run_Click(object sender, EventArgs e)
        {
            historyAdd("Reports - SSRS - Run");
            logger.Log(LogLevel.Info, DecorateString("Reports - SSRS", "Reports_SSRS_Run_Click", "(Run)"));
            ComboboxItem cbi = (ComboboxItem)cueComboBoxReportsSSRSReportList.SelectedItem;

            if (cueComboBoxReportsSSRSReportList.SelectedItem == null)
            {
                MessageBox.Show("You must select a report from the drop down list", "Reports - SSRS - Run", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Cursor = Cursors.WaitCursor;
                PF.Common.Code.General.Report_Viewer(cbi.Value.ToString());
                Cursor = Cursors.Default;
            }
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.ribbon1.ActiveTab = this.ribbonTab_Reports;
            this.tabControlMain.SelectedTab = tabPageReports;
        }

        private void reprintPalletCardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Extra_Scanning_ReprintPalletCard_Click(this.ribbonButton_Extra_Scanning_ReprintPalletCard, EventArgs.Empty);
            Extra_Scanning_ReprintPalletCard_Click(this.buttonExtraScanningReprintPalletCard, EventArgs.Empty);
        }

        private void Reset()
        {
            str_date_combo = "";
            int_prod_combo = 0;
            populate_Date_Combo();
            //ribbonCombo_ProcessFactory_WorkOrders_Date.TextBoxText = "Date";
            populate_Prod_Combo();
            //ribbonCombo_ProcessFactory_WorkOrders_Product.TextBoxText = "Product";
            populate_WO_Combo();
            //ribbonCombo_ProcessFactory_WorkOrders_WorkOrder.TextBoxText = "Work Order";
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_ProcessFactory_WorkOrders_Reset_Click(this.ribbonButton_ProcessFactory_WorkOrders_Reset, EventArgs.Empty);
            ProcessFactory_WorkOrders_Reset_Click(this.buttonProcessFactoryWorkOrdersReset, EventArgs.Empty);
        }

        private void resetToolStripMenuItem_Dispatch_Click(object sender, EventArgs e)
        {
            //ribbonButton_Dispatch_BaseLoad_Reset_Click(this.ribbonButton_Dispatch_BaseLoad_Reset, EventArgs.Empty);
            Dispatch_BaseLoad_Reset_Click(this.buttonDispatchBaseLoadReset, EventArgs.Empty);
        }

        private void richTextBoxDebug_TextChanged(object sender, EventArgs e)
        {
            //this.toolStripStatusLabelActive.Text = "Line Count: " + this.richTextBoxDebug.Lines.Length.ToString() + ".";
        }

        private void runReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Reports_SSRS_Run_Click(this.ribbonButton_Reports_SSRS_Run, EventArgs.Empty);
            Reports_SSRS_Run_Click(this.buttonReportsSSRSRun, EventArgs.Empty);
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Dispatch_BaseLoad_Run_Click(this.ribbonButton_Dispatch_BaseLoad_Run, EventArgs.Empty);
            Dispatch_BaseLoad_Run_Click(this.buttonDispatchBaseLoadRun, EventArgs.Empty);
        }

        private void salesRatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Accounting_Sales_Rates_Click(this.ribbonButton_Accounting_Sales_Rates, EventArgs.Empty);
            Accounting_Sales_Rates_Click(this.buttonAccountingSalesRates, EventArgs.Empty);
        }

        private void salesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Accounting_Invoicing_Sales_Click(this.ribbonButton_Accounting_Invoicing_Sales, EventArgs.Empty);
            Accounting_Invoicing_Sales_Click(this.buttonAccountingInvoicingSales, EventArgs.Empty);
        }

        private void saveHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TreeViewSerializer serializer = new TreeViewSerializer();
            //serializer.SerializeTreeView(this.treeViewUndoRedo, Application.StartupPath + @"\Serialise.xml");

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.DefaultExt = "xml";
            saveFileDialog1.InitialDirectory = Application.ExecutablePath;
            saveFileDialog1.Title = "Save History File...";
            //saveFileDialog1.FileName = "Serialise*.xml";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            // Show the dialog and get result.
            //DialogResult result = saveFileDialog1.ShowDialog();

            //if (result == DialogResult.OK) // Test result.
            {
                FileInfo fi = new FileInfo(saveFileDialog1.FileName);
                if (fi.Exists == true)
                {
                    fi.Delete(); // Kill it with fire!
                }

                TreeViewSerializer serializer = new TreeViewSerializer();

                DateTime dt = DateTime.Now;
                //string sTime = String.Format("{0:T}", dt);  // "4:05:07 PM" LongTime
                //historyAdd(dt.ToShortDateString() + " " + sTime + " History Saved");

                string sTime = String.Format("{0:hh:mm:ss}", dt);  // Hour 12/24 (12)
                                                                   //historyAdd(dt.ToShortDateString() + " " + sTime + " Session Begin");
                historyAdd(sTime + " Session Begin");
                //Console.WriteLine("Time: " + sTime);

                //TreeNode tNew = new TreeNode(dt.ToShortDateString() + " " + sTime + " History Saved");
                //tNew.ImageIndex = 14;
                //tNew.SelectedImageIndex = 14;
                //treeViewUndoRedo.Nodes.Add_Reversible(tNew);

                //// Make sure the save happens after the Date/Time stamp
                //serializer.SerializeTreeView(this.treeViewUndoRedo, saveFileDialog1.FileName);
            }
        }

        private void searchToolStripMenuItem_Accounting_Click(object sender, EventArgs e)
        {
            //ribbonButton_Accounting_Invoicing_Search_Click(this.ribbonButton_Accounting_Invoicing_Search, EventArgs.Empty);
            Accounting_Invoicing_Search_Click(this.buttonAccountingInvoicingSearch, EventArgs.Empty);
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Dispatch_Orders_Search_Click(this.ribbonButton_Dispatch_Orders_Search, EventArgs.Empty);

            Dispatch_Orders_Search_Click(this.buttonDispatchOrdersSearch, EventArgs.Empty);
        }

        private void Security_Maintenance_Group_Click(object sender, EventArgs e)
        {
            historyAdd("Security - Maintenance - Group");
            logger.Log(LogLevel.Info, DecorateString("Security - Maintenance", "Security_Maintenance_Group_Click", "(Group)"));

            Cursor = Cursors.WaitCursor;
            Form frm_User = new PF.Utils.Security.User_Group_Maintenance(int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm_User.Show();
            Cursor = Cursors.Default;
        }

        private void Security_Maintenance_User_Click(object sender, EventArgs e)
        {
            historyAdd("Security - Maintenance - User");
            logger.Log(LogLevel.Info, DecorateString("Security - Maintenance", "Security_Maintenance_User_Click", "(User)"));

            Cursor = Cursors.WaitCursor;
            Form frm_User = new PF.Utils.Security.User_Maintenance(int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm_User.Show();
            Cursor = Cursors.Default;
        }

        private void Security_Menus_Panels_Click(object sender, EventArgs e)
        {
            historyAdd("Security - Menus - Panels");
            logger.Log(LogLevel.Info, DecorateString("Security - Menus", "Security_Menus_Panels_Click", "(Panels)"));

            Cursor = Cursors.WaitCursor;
            Form frm_User = new PF.Utils.Security.Menu_Panel_Maintenance(int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm_User.Show();
            Cursor = Cursors.Default;
        }

        private void Security_Menus_Tabs_Click(object sender, EventArgs e)
        {
            historyAdd("Security - Menus - Tabs");
            logger.Log(LogLevel.Info, DecorateString("Security - Menus", "Security_Menus_Tabs_Click", "(Tabs)"));

            Cursor = Cursors.WaitCursor;
            Form frm_User = new PF.Utils.Security.Menu_Maintenance(int_User_id, Convert.ToBoolean((sender as Button).Tag.ToString()));
            frm_User.Show();
            Cursor = Cursors.Default;
        }

        private void securityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.ribbon1.ActiveTab = this.ribbonTab_Security;
            this.tabControlMain.SelectedTab = tabPageSecurity;
        }

        private void securityToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            //this.ribbon1.ActiveTab = this.ribbonTab_Security;
            //this.tabControlMain.SelectedTab = tabPageSecurity;
            //this.toolStripStatusLabelActive.Text = this.rbt_Security.Text + " tab selected.";
        }

        private void selectPrinterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult result = printDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                //Console.WriteLine(printDialog1.PrinterSettings.PrinterName.ToString());
                this.selectPrinterToolStripMenuItem1.Text = "Selected Printer: " + printDialog1.PrinterSettings.PrinterName;
                Settings.Printer_Name = printDialog1.PrinterSettings.PrinterName;
                Settings.Save();
            }

            GetDefaultPrinter();
        }

        //using System.Drawing.Printing;
        private string GetDefaultPrinter()
        {
            PrinterSettings settings = new PrinterSettings();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                settings.PrinterName = printer;
                if (settings.IsDefaultPrinter)
                    return printer;
            }
            return string.Empty;
        }

        private void setCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_ProcessFactory_WorkOrders_SetCurrent_Click(this.ribbonButton_ProcessFactory_WorkOrders_SetCurrent, EventArgs.Empty);
            ProcessFactory_WorkOrders_SetCurrent_Click(this.buttonProcessFactoryWorkOrdersSetCurrent, EventArgs.Empty);
        }

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            propertyGridDebug.SelectedObject = Settings;
            this.labelCurrentPropertyGridSelectedObject.Text = "Settings";

            if (this.collapsibleSplitterDebugPanel.IsCollapsed)
            {
                this.collapsibleSplitterDebugPanel.ToggleState();
            }
        }

        private void sizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Common_Types_Material_Size_Click(this.ribbonButton_Common_Types_Material_Size, EventArgs.Empty);
            Common_Types_Material_Size_Click(this.cueComboBoxCommonTypesMaterials, EventArgs.Empty);
        }

        private void staffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_ProcessFactory_WorkOrders_Staff_Click(this.ribbonButton_ProcessFactory_WorkOrders_Staff, EventArgs.Empty);
            ProcessFactory_WorkOrders_Staff_Click(this.buttonProcessFactoryWorkOrdersStaff, EventArgs.Empty);
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Security_Server_Start_Click(this.ribbonButton_Security_Server_Start, EventArgs.Empty);
            //Security_Server_Start_Click(this, EventArgs.Empty);
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void storageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Common_Types_Types_Storage_Click(this.ribbonButton_Common_Types_Types_Storage, EventArgs.Empty);
            Common_Types_Types_Storage_Click(this.cueComboBoxCommonTypes, EventArgs.Empty);
        }

        private void submissionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Extra_GateHouse_Submission_Click(this.ribbonButton_Extra_GateHouse_Submission, EventArgs.Empty);
            Extra_GateHouse_Submission_Click(this.buttonExtraGateHouseSubmissions, EventArgs.Empty);
        }

        private void SYMessage()
        {
            DataSet ds = PF.Data.AccessLayer.SY_Message.Get_Info();
            DataRow dr;

            int int_msg_U = 0;
            int int_msg = 0;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string str_Message = "";
                string msg_date = "";

                dr = ds.Tables[0].Rows[i];

                if (Convert.ToDateTime(dr["End_Date"].ToString()) >= DateTime.Now)
                {
                    DateTime startTime = Convert.ToDateTime(dr["End_Date"].ToString());
                    double dbl_month = Math.Floor(((startTime - DateTime.Now).TotalDays / 365.25 * 12));
                    double dbl_days = 0;

                    if (dbl_month > 0)
                    {
                        msg_date = msg_date + Convert.ToString(dbl_month) + " Months ";
                        dbl_days = Math.Floor((startTime - DateTime.Now).TotalDays - (dbl_month / 12 * 365.25));
                    }
                    else
                    {
                        dbl_days = Math.Floor((startTime - DateTime.Now).TotalDays);
                    }
                    if (dbl_days > 0)
                    {
                        msg_date = msg_date + Convert.ToString(dbl_days) + " Days ";
                    }
                    if (dbl_month <= 0 && dbl_days <= 0)
                    {
                        msg_date = msg_date + ((startTime - DateTime.Now)).ToString("hh\\:mm\\:ss");
                    }

                    if (Convert.ToInt32(dr["User_Id"].ToString()) == int_User_id)
                    {
                        str_Message = str_Message + dr["Message"].ToString().Replace("%", msg_date) + " ";

                        switch (int_msg_U)
                        {
                            case 0:
                                //rblb_U_Message0.Text = "";
                                //rblb_U_Message0.Text = str_Message;
                                break;

                            case 1:
                                //rblb_U_Message1.Text = "";
                                //rblb_U_Message1.Text = str_Message;
                                break;

                            case 2:
                                //rblb_U_Message2.Text = "";
                                //rblb_U_Message2.Text = str_Message;
                                break;

                            case 3:
                                //rblb_U_Message3.Text = "";
                                //rblb_U_Message3.Text = str_Message;
                                break;
                        }
                        int_msg_U++;
                    }
                    else if (Convert.ToInt32(dr["User_Id"].ToString()) == 0)
                    {
                        str_Message = str_Message + dr["Message"].ToString().Replace("%", msg_date) + " ";
                        switch (int_msg)
                        {
                            case 0:
                                //rblbl_Message0.Text = "";
                                //rblbl_Message0.Text = str_Message;
                                break;

                            case 1:
                                //rblbl_Message1.Text = "";
                                //rblbl_Message1.Text = str_Message;
                                break;

                            case 2:
                                //rblbl_Message2.Text = "";
                                //rblbl_Message2.Text = str_Message;
                                break;

                            case 3:
                                //rblbl_Message3.Text = "";
                                //rblbl_Message3.Text = str_Message;
                                break;
                        }
                        int_msg++;
                    }
                }
                else
                {
                    try
                    {
                        DateTime dt = new DateTime();
                        string str_new_datetime = "";
                        switch (dr["Repeat_ind"].ToString().Substring(0, 1).ToUpper())
                        {
                            case "A":
                                dt = Convert.ToDateTime(dr["End_Date"].ToString());
                                str_new_datetime = update_date(dt, 1, 0, 0);
                                PF.Data.AccessLayer.SY_Message.Update_End_Date(Convert.ToInt32(dr["Message_Id"].ToString()), str_new_datetime);
                                break;

                            case "M":
                                dt = Convert.ToDateTime(dr["End_Date"].ToString());
                                str_new_datetime = update_date(dt, 0, Convert.ToInt32(dr["Repeat_ind"].ToString().Substring(1)), 0);
                                PF.Data.AccessLayer.SY_Message.Update_End_Date(Convert.ToInt32(dr["Message_Id"].ToString()), str_new_datetime);
                                break;

                            case "D":
                                dt = Convert.ToDateTime(dr["End_Date"].ToString());
                                str_new_datetime = update_date(dt, 0, 0, Convert.ToInt32(dr["Repeat_ind"].ToString().Substring(1)));
                                PF.Data.AccessLayer.SY_Message.Update_End_Date(Convert.ToInt32(dr["Message_Id"].ToString()), str_new_datetime);
                                break;

                            default:
                                PF.Data.AccessLayer.SY_Message.Delete(Convert.ToInt32(dr["Message_Id"].ToString()));
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Log(LogLevel.Debug, ex.Message);
                    }
                }
            }
            ds.Dispose();
        }

        /// <summary>
        /// Clear out any old messages when changing TabPages - 12/05/2015 BN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelTabControlMainBottomStatus.Text = string.Empty;
            labelTabControlMainBottomStatus.ForeColor = SystemColors.ControlText;

            Console.WriteLine(tabControlMain.SelectedTab.Text);
            Console.WriteLine(PF.Global.Global.bool_Testing);
            //PF.Global.Global.Phantom_Dev_Mode = PF.Global.Global.bol_Testing;

            //if (tabControlMain.SelectedTab == tabPageAccounting)
            //{
            //    historyAdd("TabPage - Accounting");

            //    // Could add this, but it's kind of overkill
            //    //logger.Log(LogLevel.Info, DecorateString("TabPage", "Accounting", "Activated"));

            //    this.toolStripStatusLabelActive.Text = this.tabPageAccounting.Text + " tab selected.";
            //}

            //Console.WriteLine(tabControlMain.SelectedTab.Text);
            switch (tabControlMain.SelectedTab.Text)
            {
                case "Home":
                    {
                        //ribbon1.ActiveTab = ribbonTab_Home;
                        historyAdd("TabPage - Home");
                        break;
                    }
                case "Security":
                    {
                        //ribbon1.ActiveTab = ribbonTab_Security;
                        historyAdd("TabPage - Security");
                        break;
                    }
                case "Common":
                    {
                        //ribbon1.ActiveTab = ribbonTab_Common;
                        historyAdd("TabPage - Common");
                        break;
                    }
                case "Process Factory":
                    {
                        //ribbon1.ActiveTab = ribbonTab_ProcessFactory;
                        historyAdd("TabPage - Process Factory");
                        break;
                    }
                case "Dispatch":
                    {
                        //ribbon1.ActiveTab = ribbonTab_Dispatch;
                        historyAdd("TabPage - Dispatch");
                        break;
                    }
                case "PF - Maintenance":
                    {
                        //ribbon1.ActiveTab = ribbonTab_Maintenance;
                        historyAdd("TabPage - PF - Maintenance");
                        break;
                    }
                case "Accounting":
                    {
                        //ribbon1.ActiveTab = ribbonTab_Accounting;
                        historyAdd("TabPage - Accounting");
                        break;
                    }
                case "Consumables":
                    {
                        //ribbon1.ActiveTab = ribbonTab_Consumables;
                        historyAdd("TabPage - Consumables");
                        break;
                    }
                case "Extra":
                    {
                        //ribbon1.ActiveTab = ribbonTab_Extra;
                        historyAdd("TabPage - Extra");
                        break;
                    }
                case "Reports":
                    {
                        //ribbon1.ActiveTab = ribbonTab_Reports;
                        historyAdd("TabPage - Reports");
                        break;
                    }
            }
        }

        private void tabletMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Extra_Scanning_TabletMenu_Click(this.ribbonButton_Extra_Scanning_TabletMenu, EventArgs.Empty);
            Extra_Scanning_TabletMenu_Click(this.buttonExtraScanningTabletMenu, EventArgs.Empty);
        }

        private void tabsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Security_Menus_Tabs_Click(this.ribbonButton_Security_Menus_Tabs, EventArgs.Empty);
            Security_Menus_Tabs_Click(this.buttonSecurityMenusTabs, EventArgs.Empty);
        }

        private void testsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_ProcessFactory_WorkOrders_Tests_Click(this.ribbonButton_ProcessFactory_WorkOrders_Tests, EventArgs.Empty);
            ProcessFactory_WorkOrders_Tests_Click(this.buttonProcessFactoryWorkOrdersTests, EventArgs.Empty);
        }

        private void testsToolStripMenuItem_Maintenance_Click(object sender, EventArgs e)
        {
            //ribbonButton_Maintenance_Types_Tests_Click(this.ribbonButton_Maintenance_Types_Tests, EventArgs.Empty);
            Maintenance_Types_Tests_Click(this.buttonPfMaintenanceTypesTests, EventArgs.Empty);
        }

        /// <summary>
        /// Fires every 100ms
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="eArgs">EventArgs</param>
        [DebuggerStepThrough]
        public void Timer_Tick(object sender, EventArgs eArgs)
        {
            if (sender == Clock)
            {
                // Phantom Technologies 9/12/2014 Disable temporarily to stop billions of error messages
                //SYMessage(); // Who cares how far away the rugby world cup is?
            }
        }

        private void timer_Tick_1(object sender, EventArgs e)
        {
        }

        private void traderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Common_Grower_Trader_Click(this.ribbonButton_Common_Grower_Trader, EventArgs.Empty);
            Common_Grower_Trader_Click(this.buttonCommonGrowerTrader, EventArgs.Empty);
        }

        private void treatmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Common_Types_Material_Treatment_Click(this.ribbonButton_Common_Types_Material_Treatment, EventArgs.Empty);
            Common_Types_Material_Treatment_Click(this.cueComboBoxCommonTypesMaterials, EventArgs.Empty);
        }

        private void trucksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Common_Location_OffSite_Trucks_Click(this.ribbonButton_Common_Location_OffSite_Trucks, EventArgs.Empty);
            Common_Location_OffSite_Trucks_Click(this.groupedComboBoxCommonLocation, EventArgs.Empty);
        }

        private void typeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Common_Fruit_Type_Click(this.ribbonButton_Common_Fruit_Type, EventArgs.Empty);
            Common_Fruit_Type_Click(this.buttonCommonFruitType, EventArgs.Empty);
        }

        private string update_date(DateTime dt_old, int Year, int Month, int Day)
        {
            string str_new_datetime = "";
            DateTime dt_new = new DateTime();
            dt_new = dt_old.AddYears(Year).AddMonths(Month).AddDays(Day);

            str_new_datetime = dt_new.Year.ToString() + "/" + dt_new.Month.ToString() + "/" + dt_new.Day.ToString() + " " +
                               dt_new.Hour.ToString() + ":" + dt_new.Minute.ToString() + ":" + dt_new.Second.ToString();
            return str_new_datetime;
        }

        private void updateMaterialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //

            Button_Common_Material_UpdateMaterial_Click(this.buttonCommonMaterialUpdatematerialNumber, EventArgs.Empty);

            // Will need a try/catch to validate the textbox input
            //ribbonButton_Common_Material_UpdateMaterial_Click(this, EventArgs.Empty);

            //MessageBox.Show("Not yet implemented.\r\nPlease use the Ribbon for the time being.",
            //    "Work in Progress", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void usageAdjustmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Consumables_Maintenance_UsageAdjustment_Click(this.ribbonButton_Consumables_Maintenance_UsageAdjustment, EventArgs.Empty);
            Consumables_Maintenance_UsageAdjustment_Click(this.buttonConsumablesMaintenanceUsageAdjustment, EventArgs.Empty);
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Security_Maintenance_User_Click(this.ribbonButton_Security_Maintenance_User, EventArgs.Empty);
            Security_Maintenance_User_Click(this.buttonSecurityMaintenanceUser, EventArgs.Empty);
        }

        private void varietyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Common_Fruit_Variety_Click(this.ribbonButton_Common_Fruit_Variety, EventArgs.Empty);
            Common_Fruit_Variety_Click(this.buttonCommonFruitVariety, EventArgs.Empty);
        }

        [DebuggerStepThrough]   // Don't stop here in debug mode BN
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // Test if the About item was selected from the system menu
            if ((m.Msg == WM_SYSCOMMAND) && ((int)m.WParam == SYSMENU_ABOUT_ID))
            {
                MessageBox.Show("Placeholder for possible unknown future function.\r\n12/06/2105 - Testing", "About FP.ProcessFactory", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void workAreasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ribbonButton_Maintenance_Staff_OtherWorkAreas_Click(this.ribbonButton_Maintenance_Staff_OtherWorkAreas, EventArgs.Empty);
            Maintenance_Staff_OtherWorkAreas_Click(this.buttonPfMaintenanceStaffOtherWorkAreas, EventArgs.Empty);
        }

        private void workOrderToolStripMenuItem_Accounting_Click(object sender, EventArgs e)
        {
            //ribbonButton_Accounting_Invoicing_WorkOrder_Click(this.ribbonButton_Accounting_Invoicing_WorkOrder, EventArgs.Empty);
            Accounting_Invoicing_WorkOrder_Click(this.buttonAccountingInvoicingWorkOrder, EventArgs.Empty);
        }

        private void workOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.Focus();

            // Special Case
            //rcmb_Work_Order_DropDownItemClicked
            //(this, EventArgs.Empty);

            //MessageBox.Show("Not yet implemented.\r\nPlease use the Ribbon for the time being.",
            //    "Work in Progress", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion Methods of Main_Menu (265)

        private void checkForUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region Comments

            // In the original demo code, this was in Form_Load

            // Uncomment below line to see Russian version
            //AutoUpdater.CurrentCulture = CultureInfo.CreateSpecificCulture("ru-RU");

            // If you want to open download page when user click on download button uncomment below line.
            //AutoUpdater.OpenDownloadPage = true;

            // Don't want user to select remind later time in AutoUpdater notification window then uncomment
            // 3 lines below so default remind later time will be set to 2 days.

            //AutoUpdater.LetUserSelectRemindLater = false;
            //AutoUpdater.RemindLaterTimeSpan = RemindLaterFormat.Days;
            //AutoUpdater.RemindLaterAt = 2;
            //AutoUpdater.Start("http://rbsoft.org/updates/right-click-enhancer.xml");

            #endregion Comments

            try
            {
                logger.Log(LogLevel.Info, "Beginning check for update...");
                Cursor.Current = Cursors.WaitCursor;

                // BN - Added proxy settings 6/7/2015
                // BN - Added temporary settings for Jim to test.
                // BN - This isn't for anyone else to use.
                // --------------------------------------------------------
                // BN - had to revert back to using the settings, as the info
                // Jim supplied didn't work as expected.
                // 22/08/2015
                AutoUpdater.proxyPassword = Settings.UpdateProxyPassword;
                AutoUpdater.proxyUsername = Settings.UpdateProxyUsername;
                AutoUpdater.proxyURI = Settings.UpdateProxyURI;
                AutoUpdater.proxyPort = Settings.UpdateProxyPort;
                AutoUpdater.useProxy = Settings.UpdateUseProxy;
                AutoUpdater.Start(Settings.UpdateAddress);

                // This is the hard-coded settings area
                // Enable once we figure out what is actually going to work out on site
                //AutoUpdater.proxyURI = "http://192.168.80.69";
                //AutoUpdater.proxyPort = "8090";
                //AutoUpdater.useProxy = true;
                //AutoUpdater.Start("http://192.168.80.69:8090/FP-Update/FP-PF-Update.xml");

                // ------------------------------------------------------------------------------------------
                // 22/08/2015 Bruce Nielsen
                // There should be a lower-level alert happening from the AutoUpdater class,
                // but for some reason it's not showing up. It should say:
                //
                // logger.Log(LogLevel.Info, "No update is available.");
                // Line 364, AutoUpdater.cs
                // ------------------------------------------------------------------------------------------
                // This is leading to confusion when trying to debug errors in the field, I get that.
                // I currently don't know why my low-level alert is being swallowed by the higher-level alert
                // ------------------------------------------------------------------------------------------

                logger.Log(LogLevel.Info, "Completed check for update.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Info, "Error occurred while checking for update.\r\n" + ex.Message);
                Cursor.Current = Cursors.Default;
            }
        }

        private void tabPageHome_Click(object sender, EventArgs e)
        {
        }

        //public static string TrimStringFromEnd(this string input, string suffixToRemove)
        //{
        //    // Changed from

        //    // TrimEnd() (and the other trim methods) accept characters to be
        //    // trimmed, but not strings. If you really want a version that can
        //    // trim whole strings then you could create an extension method.
        //    // For example...
        //    if (input != null && suffixToRemove != null
        //      && input.EndsWith(suffixToRemove))
        //    {
        //        return input.Substring(0, input.Length - suffixToRemove.Length);
        //    }
        //    else return input;
        //}

        public TimeSpan UpTime
        {
            get
            {
                using (var uptime = new PerformanceCounter("System", "System Up Time"))
                {
                    uptime.NextValue();       //Call this an extra time before reading its value
                    return TimeSpan.FromSeconds(uptime.NextValue());
                }
            }
        }

        private void helpToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            // Move this to where it belongs (ie. Not on home screen to be seen EVERY time you open the program)
            // BN 22/08/2015
            // Is now outdated by about 3 years.
            Home_Help_Help_Click(this, EventArgs.Empty);
        }

        private void UpdateData()
        {
            //string s = sd.GetProcessorData();
            //labelCpu.Text = s;
            //double d = double.Parse(s.Substring(0, s.IndexOf("%")));
            //dataBarCPU.Value = (int)d;
            //dataChartCPU.UpdateChart(d);

            //s = sd.GetMemoryVData();
            //labelMemV.Text = s;
            //d = double.Parse(s.Substring(0, s.IndexOf("%")));
            //dataBarMemV.Value = (int)d;
            //dataChartMem.UpdateChart(d);

            //s = sd.GetMemoryPData();
            //labelMemP.Text = s;
            //dataBarMemP.Value = (int)double.Parse(s.Substring(0, s.IndexOf("%")));

            //d = sd.GetDiskData(SystemData.DiskData.Read);
            //labelDiskR.Text = "Disk R (" + sd.FormatBytes(d) + "/s)";
            //dataChartDiskR.UpdateChart(d);

            //d = sd.GetDiskData(SystemData.DiskData.Write);
            //labelDiskW.Text = "Disk W (" + sd.FormatBytes(d) + "/s)";
            //dataChartDiskW.UpdateChart(d);

            //d = sd.GetNetData(SystemData.NetData.Received);
            //labelNetI.Text = "Net I (" + sd.FormatBytes(d) + "/s)";
            //dataChartNetI.UpdateChart(d);

            //d = sd.GetNetData(SystemData.NetData.Sent);
            //labelNetO.Text = "Net O (" + sd.FormatBytes(d) + "/s)";
            //dataChartNetO.UpdateChart(d);

            // ------------------------------
            ShowSystemUptime();
        }

        private void timerSysMon_Tick(object sender, EventArgs e)
        {
            UpdateData();
        }

        protected override void OnResize(EventArgs e)
        {
            //if (0 == _sizeOrg.Width || 0 == _sizeOrg.Height) return;

            //if (Size.Width != _size.Width && Size.Width > _sizeOrg.Width)
            //{
            //    int xChange = Size.Width - _size.Width;   // Client window

            //    // Adjust three bars
            //    int newWidth = dataBarCPU.Size.Width + xChange;
            //    int labelStart = labelCpu.Location.X + xChange;

            //    dataBarCPU.Size = new Size(newWidth, dataBarCPU.Size.Height);
            //    labelCpu.Location = new Point(labelStart, labelCpu.Location.Y);

            //    dataBarMemV.Size = new Size(newWidth, dataBarCPU.Size.Height);
            //    labelMemV.Location = new Point(labelStart, labelMemV.Location.Y);

            //    dataBarMemP.Size = new Size(newWidth, dataBarCPU.Size.Height);
            //    labelMemP.Location = new Point(labelStart, labelMemP.Location.Y);

            //    // Resize four charts
            //    int margin = 8;
            //    Rectangle rt = this.ClientRectangle;

            //    newWidth = (rt.Width - 3 * margin) / 2;
            //    labelStart = newWidth + 2 * margin;

            //    dataChartDiskR.Size = new Size(newWidth, dataChartDiskR.Size.Height);
            //    labelDiskW.Location = new Point(labelStart, labelDiskW.Location.Y);
            //    dataChartDiskW.Location = new Point(labelStart, dataChartDiskW.Location.Y);
            //    dataChartDiskW.Size = new Size(newWidth, dataChartDiskW.Size.Height);

            //    dataChartNetI.Size = new Size(newWidth, dataChartNetI.Size.Height);
            //    labelNetO.Location = new Point(labelStart, labelNetO.Location.Y);
            //    dataChartNetO.Location = new Point(labelStart, dataChartNetO.Location.Y);
            //    dataChartNetO.Size = new Size(newWidth, dataChartNetO.Size.Height);

            //    // I use Anchor so not to Resize two usage charts
            //    //				dataChartCPU.Size = new Size(rt.Width - 2*margin, dataChartCPU.Size.Height);
            //    //				dataChartMem.Size = new Size(rt.Width - 2*margin, dataChartMem.Size.Height);

            //    _size = Size;
            //}

            //Size = new Size(Size.Width, _sizeOrg.Height);
        }

        private void ShowSystemUptime()
        {
            string sDays = UpTime.Days.ToString();
            string sHours = UpTime.Hours.ToString();
            string sMinutes = UpTime.Minutes.ToString();

            string appendixDays = ""; string appendixHours = ""; string appendixMinutes = "";

            // This didn't work as I expected - Day and Days are semantically very close
            // Took a bit of extra thought to get it right
            if (UpTime.Days == 1) { appendixDays = " Day"; } else { appendixDays = " Days"; }
            if (UpTime.Hours == 1) { appendixHours = " Hour"; } else { appendixHours = " Hours"; }
            if (UpTime.Minutes == 1) { appendixMinutes = " Minute"; } else { appendixMinutes = " Minutes"; }

            this.labelUptimeValue.Text =
                sDays + appendixDays + ", " +
                sHours + appendixHours + ", " +
                sMinutes + appendixMinutes; // Also in timer update
        }

        private void propertyGridDebug_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "Show Word warning message")
            {
                if (Settings.ShowWordWarningMessage == true)
                {
                    this.labelWarningAboutWordDocuments.Visible = true;
                }
                else
                {
                    this.labelWarningAboutWordDocuments.Visible = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            throw new ArgumentException("The parameter was invalid");
        }

        private void sendDebugReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Add a division marker (of sorts) here
            logger.Log(LogLevel.Info, "Calling: SendDebugInfo");

            // Now get the debug email ready by creating a new debug form called sde.
            PF.Common.Code.SendDebugEmail sde = new PF.Common.Code.SendDebugEmail();

            sde.textBoxBugReport.Text = "This will send an automatic report without any further input.";

            // Auto-populate the error - Add a blank line ("\r\n\r\n") as a prefix so they can type in any extra info 
            // (It'll never happen)
            //sde.textBoxBugReport.Text = "\r\n\r\n" + e.Exception.Message + "\r\n\r\n" + displaytrace;

            // Deselect all text and move the caret to the first position in the text box
            // If anyone cares enough to type out an explanation, they can do so here without wiping out the error trace details.
            sde.textBoxBugReport.Select(0, 0);

            sde.Show();
            sde.TopMost = true; // 21/10/2015 BN
            // At this point they can choose to send or not, but the error has already been logged out to the CSV no matter what.

        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {

        }

        private void openSavedFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // BN 29/10/2015
            PF.Common.Code.OpenPdfFileLocations.OpenSavedPdfFileLocation();
        }

        private void openTempFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // BN 29/10/2015
            PF.Common.Code.OpenPdfFileLocations.OpenTempPdfFileLocation();
        }

        private void Main_Menu_Resize(object sender, EventArgs e)
        {
        }

        private void buttonResetCueComboBoxes_Click(object sender, EventArgs e)
        {
            cueComboBoxProcessFactoryWorkOrdersSelectDate.Text = "";
            cueComboBoxProcessFactoryWorkOrdersSelectProduct.Text = "";
            cueComboBoxProcessFactoryWorkOrdersSelectWorkOrder.Text = "";
        }

    } // End of Main Class

    /// <summary>
    /// Custom class to add Text and Values to an item without using a BindingSource - BN 11/05/2015
    /// // Update 12/05/2015 add Object Tag property - BN
    /// </summary>
    public class ComboboxItem
    {
        #region Properties of ComboboxItem (3)

        public object Tag { get; set; }

        public string Text { get; set; }

        public string Value { get; set; }

        #endregion Properties of ComboboxItem (3)

        #region Methods of ComboboxItem (1)

        public override string ToString()
        {
            return Text;
        }

        #endregion Methods of ComboboxItem (1)
    }

    public static class BnExtension
    {
        public static string TrimStringFromEnd(string input, string suffixToRemove)
        {
            // TrimEnd() (and the other trim methods) accept characters to be
            // trimmed, but not strings. If you really want a version that can
            // trim whole strings then you could create an extension method.
            // For example...
            if (input != null && suffixToRemove != null
              && input.EndsWith(suffixToRemove))
            {
                return input.Substring(0, input.Length - suffixToRemove.Length);
            }
            else return input;
        }
    }
}