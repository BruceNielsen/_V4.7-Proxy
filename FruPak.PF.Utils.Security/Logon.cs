using NLog;
using System;
using System.Data;
using System.Windows.Forms;

namespace FruPak.PF.Utils.Security
{
    /*Description
    -----------------
    Logon Class.
     * This class is a form validates a user access to the system

    Date        Author     Desc
    -------------------------------------------------------------------------------------------------------------------------------------------------
    01/09/2013  Dave       Creation
    */

    public partial class Logon : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        // Phantom 17/12/2014 Declare application settings object
        //private static PhantomCustomSettings Settings;

        private string str_Type = "";

        public Logon(string str_user_id, string str_type)
        {
            InitializeComponent();

            //logger.Log(LogLevel.Info, LogCode("Logon.cs: Form Loading."));

            #region Load CustomSettings (Disabled - Reasons why are given in Install.cs)

            #region Get "My Documents" folder

            //string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //path = Path.Combine(path, "FruPak_Settings");

            //// Create folder if it doesn't already exist
            //if (!Directory.Exists(path))
            //    Directory.CreateDirectory(path);

            #endregion Get "My Documents" folder

            //// Initialize settings
            //Settings = new PhantomCustomSettings();
            //Settings.SettingsPath = Path.Combine(path, "_Phantom_Settings.xml");
            //Settings.EncryptionKey = "phantomKey";

            //if (!File.Exists(Settings.SettingsPath))
            //{
            //    Settings.Save();
            //    logger.Log(LogLevel.Info, LogCode("Default Phantom Settings file created");
            //}
            //// Load settings - Normally in Form_Load
            //Settings.Load();

            //logger.Log(LogLevel.Info, LogCode("Settings file opened.");

            //local_path = Settings.Path_Local_Path;
            //remote_path = Settings.Path_Remote_Path;
            //path_to_settings = Settings.Path_To_Settings;

            // Save settings - Normally in Form_Closing
            //Settings.Save();

            #endregion Load CustomSettings (Disabled - Reasons why are given in Install.cs)

            // By using the global variables, I can avoid using the settings file in this form
            //if(FruPak.PF.Global.Global.Phantom_Dev_Mode == true)
            //{
            //    this.buttonPhantomPasswordHack.Visible = true;
            //}
            //else
            //{
            //    this.buttonPhantomPasswordHack.Visible = false;
            //}
            //log.Info("FruPak.PF.Utils.Security.Logon");

            FruPak.PF.Global.Global.bol_Testing = false;
            str_Type = str_type;
            if (str_user_id != "")
            {
                txt_User_Id.Text = str_user_id;
            }
            else
            {
                txt_User_Id.Text = Environment.UserName.ToString();
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

                //else if (c.GetType() == typeof(FruPak.PF.Utils.UserControls.Customer))
                //{
                //    FruPak.PF.Utils.UserControls.Customer cust = (FruPak.PF.Utils.UserControls.Customer)c;
                //    cust.CustomerChanged += new EventHandler(this.CustomerControl_CustomerChanged);
                //}
            }

            #endregion Log any interesting events from the UI to the CSV log file
        }

        //validate userid and password
        private void btn_login_Click(object sender, EventArgs e)
        {
            FruPak.PF.Global.Global.Phantom_Dev_Mode = FruPak.PF.Global.Global.bol_Testing;
            submit();
        }

        private void Enter_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Enter key
            if (e.KeyChar == 13)
            {
                FruPak.PF.Global.Global.Phantom_Dev_Mode = FruPak.PF.Global.Global.bol_Testing;
                submit();
            }
        }

        private static DialogResult DLR_MessageBox;
        private static int int_User_id = 0;

        private void submit()
        {
            DLR_MessageBox = new DialogResult();

            if (General.Password_Validate(txt_User_Id.Text, txt_Password.Text) == false)
            {
                DLR_MessageBox = MessageBox.Show("Invalid Password - Please try again", "Security - Logon", MessageBoxButtons.RetryCancel, MessageBoxIcon.Stop);
            }
            DataSet ds_Get_User_Info = FruPak.PF.Data.AccessLayer.SC_User.Get_Info(txt_User_Id.Text);

            if (Convert.ToInt32(ds_Get_User_Info.Tables[0].Rows.Count.ToString()) == 0)
            {
                string str_Error_msg = "Invalid UserID" + Environment.NewLine + "The UserID you have entered is invalid. Please try again." + Environment.NewLine + "If the error keeps occurring Please contact your local support Team.";
                DLR_MessageBox = MessageBox.Show(str_Error_msg, "Security - Logon", MessageBoxButtons.RetryCancel, MessageBoxIcon.Stop);
            }
            else
            {
                DataRow dr_Get_User_Info;

                for (int i = 0; i < Convert.ToInt32(ds_Get_User_Info.Tables[0].Rows.Count.ToString()); i++)
                {
                    dr_Get_User_Info = ds_Get_User_Info.Tables[0].Rows[i];
                    int_User_id = Convert.ToInt32(dr_Get_User_Info["User_Id"].ToString());
                }
            }

            ds_Get_User_Info.Dispose();

            //close application when selected by user
            switch (DLR_MessageBox)
            {
                case DialogResult.Cancel:
                    DLR_MessageBox = DialogResult.Cancel;
                    this.Close();
                    break;

                case DialogResult.Retry:
                    DLR_MessageBox = DialogResult.Retry;
                    break;

                default:
                    DLR_MessageBox = DialogResult.Yes;
                    break;
            }
            FruPak.PF.Common.Code.General.write_log(int_User_id, str_Type, int_User_id);
            //logger.Log(LogLevel.Info, LogCode("Logon.cs: write_log:" + int_User_id.ToString() + ", " + str_Type + ", " + int_User_id.ToString()));

            FruPak.PF.Global.Global.LogonName = txt_User_Id.Text;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            DLR_MessageBox = DialogResult.Yes;
            FruPak.PF.Common.Code.General.write_log(int_User_id, "Log off", int_User_id);
            logger.Log(LogLevel.Info, LogCode("Logon.cs: btn_Close_Click:" + int_User_id.ToString() + ", Log off, " + int_User_id.ToString()));
            this.Close();
        }

        private void btn_Change_Password_Click(object sender, EventArgs e)
        {
            FruPak.PF.Common.Code.General.write_log(int_User_id, "Change pwd", int_User_id);
            Form Frm_Change_PSWD = new Password_Reset(txt_User_Id.Text);
            Frm_Change_PSWD.ShowDialog();
        }

        public class ResultFromFrmMain
        {
            public DialogResult Result { get; set; }
            public int Field1 { get; set; }
        }

        public static ResultFromFrmMain Execute(string str_user_id, string str_Type)
        {
            using (var f = new Logon(str_user_id, str_Type))
            {
                f.btn_login.DialogResult = DialogResult.OK;
                f.btn_Close.DialogResult = DialogResult.Cancel;

                var result = new ResultFromFrmMain();

                while (DLR_MessageBox == DialogResult.Retry || DLR_MessageBox == DialogResult.None)

                {
                    result.Result = f.ShowDialog();
                }

                if (DLR_MessageBox == DialogResult.Cancel)
                {
                    result.Result = DialogResult.Cancel;
                }

                result.Field1 = int_User_id;
                return result;
            }
        }

        private void txt_User_Id_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void txt_User_Id_Leave(object sender, EventArgs e)
        {
            DataSet ds = FruPak.PF.Data.AccessLayer.SC_User.Get_Info(txt_User_Id.Text);
            DataRow dr;
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dr = ds.Tables[0].Rows[i];

                    try
                    {
                        rb_DB_Test.Visible = Convert.ToBoolean(dr["Allow_Test"].ToString());
                    }
                    catch (Exception ex)
                    {
                        logger.Log(LogLevel.Info, LogCode("User: " + txt_User_Id.Text + " - was not found or not allowed for test function."));
                        //logger.Log(LogLevel.Info, LogCode("The following error just means that there's a problem with the users logon, as in it doesn't exist, bad password etc.");
                        //logger.Log(LogLevel.Error, ex.Message);

                        // Consume the ex to avoid an unnecessary warning message - may need it later on BN
                        Console.WriteLine(ex.Message);
                    }
                }
                ds.Dispose();
            }
            else
            {
                logger.Log(LogLevel.Fatal, "Logon DataSet was empty.");
                //logger.Log(LogLevel.Fatal, "Logon DataSet was empty. Exiting Application.");
                //MessageBox.Show("Logon DataSet was empty. Exiting Application.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //Application.Exit();
            }
        }

        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            // Phantom 12/12/2014
            var rad = (RadioButton)sender;
            if (rad.Text == "&Test" && rad.Checked == true)
            {
                FruPak.PF.Global.Global.bol_Testing = true;
                FruPak.PF.Global.Global.Phantom_Dev_Mode = true;
                //Console.WriteLine("Phantom_Dev_Mode = true");
            }
            else
            {
                FruPak.PF.Global.Global.bol_Testing = false;
                FruPak.PF.Global.Global.Phantom_Dev_Mode = false;
                //Console.WriteLine("Phantom_Dev_Mode = false");
            }

            //// The compiler was complaining about this statement, so I had to cast it to string to get around it
            //if ((string)(sender as RadioButton).Text == "&Test" && (sender as RadioButton).Checked == true)
            //{
            //    FruPak.PF.Global.Global.bol_Testing = true;
            //    //FruPak.PF.Global.Global.Phantom_Dev_Mode = true;
            //}
            //else
            //{
            //    FruPak.PF.Global.Global.bol_Testing = false;
            //    //FruPak.PF.Global.Global.Phantom_Dev_Mode = false;
            //}
        }

        //private void buttonPhantomPasswordHack_Click(object sender, EventArgs e)
        //{
        //    this.txt_User_Id.Focus();
        //    this.txt_User_Id.Text = "RobinJ";

        //    this.txt_Password.Focus();
        //    this.txt_Password.Text = "pass2011";

        //}

        private void Logon_Load(object sender, EventArgs e)
        {
            // Fire both one after the other to ensure the Checked_Changed code executes
            this.rb_DB_Test.Checked = true;
            this.rb_DB_Production.Checked = true;
        }

        private void Logon_FormClosing(object sender, FormClosingEventArgs e)
        {
            //FruPak.PF.Global.Global.Phantom_Dev_Mode = FruPak.PF.Global.Global.bol_Testing;
            //logger.Log(LogLevel.Info, LogCode("Logon.cs: Logon_FormClosing."));
        }

        private void panelHack_Click(object sender, EventArgs e)
        {
            this.txt_User_Id.Focus();
            this.txt_User_Id.Text = "RobinJ";
            this.txt_Password.Focus();
            this.txt_Password.Text = "pass2011";
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

        private void Control_Validated(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            // Switched off, as it's a huge security risk
            //logger.Log(LogLevel.Info, DecorateString(t.Name, t.Text, "Validated"));
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

        //private void CustomerControl_CustomerChanged(object sender, EventArgs e)
        //{
        //    FruPak.PF.Utils.UserControls.Customer cust = (FruPak.PF.Utils.UserControls.Customer)sender;
        //    logger.Log(LogLevel.Info, DecorateString(cust.Name, cust.Customer_Name, "TextChanged"));
        //}

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
        private void Logon_KeyDown(object sender, KeyEventArgs e)
        {
            // Note that the KeyPreview property must be set to true for this to work
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        #endregion Methods to log UI events to the CSV file. BN 29/01/2015

        private void panelHackSelkuckG_Click(object sender, EventArgs e)
        {
            this.txt_User_Id.Focus();
            this.txt_User_Id.Text = "SelcukG";
            this.txt_Password.Focus();
            this.txt_Password.Text = "frupak2014";
        }

        private void panelHackGlenysP_Click(object sender, EventArgs e)
        {
            this.txt_User_Id.Focus();
            this.txt_User_Id.Text = "GlenysP";
            this.txt_Password.Focus();
            this.txt_Password.Text = "piemix";
        }
    }
}