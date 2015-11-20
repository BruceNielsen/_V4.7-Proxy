using PF.CustomSettings;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PF.Common.Code
{
    public partial class Send_Email : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static PF.CustomSettings.PhantomCustomSettings Settings;

        /// <summary>
        /// Outlook will check here to see if it's empty. 
        /// If not, then the address has been overridden and it will use this instead of the outlook contact.
        /// BN 6/11/2015
        /// </summary>
        public static List<string> EmailOverriddenList     // BN
        {
            get;
            set;
        }

        public static string Email_From_Address
        {
            get;
            set;
        }

        public static string Network_UserId
        {
            get;
            set;
        }

        public static string Email_Subject
        {
            get;
            set;
        }

        public static bool Email_sender_Copy
        {
            get;
            set;
        }

        public static string Email_Message
        {
            get;
            set;
        }

        public static string Network_Password
        {
            get;
            set;
        }

        public Send_Email()
        {
            InitializeComponent();
            EmailOverriddenList = new List<string>();

            //check if testing or not

            //if (PF.Global.Global.bol_Testing == true)
            //{
            //    this.Text = "FP Process Factory - " + this.Text + " - Test Environment";
            //}
            //else
            //{
            //    this.Text = "FP Process Factory";
            //}

            txt_Email_To.ResetText();
            if (PF.Data.Outlook.Contacts.Contact_Email != null)
            {
                foreach (string email in PF.Data.Outlook.Contacts.Contact_Email)
                {
                    txt_Email_To.Text = txt_Email_To.Text + email + Environment.NewLine;
                    logger.Log(LogLevel.Info, "Email To: " + txt_Email_To.Text);
                }
            }
            else
            {
                txt_Email_To.Text = "No Email Address found" + Environment.NewLine;
                logger.Log(LogLevel.Info, "Email To: " + "No Email Address found.");
            }

            txt_Attachments.ResetText();
            try
            {
                foreach (string attach in PF.Common.Code.SendEmail.attachment)
                {
                    txt_Attachments.Text = txt_Attachments.Text + attach + Environment.NewLine;
                    logger.Log(LogLevel.Info, "Attachments: " + txt_Attachments.Text);
                }
            }
            catch
            {
                txt_Attachments.Text = "";
            }

            txt_Email_From.Text = Email_From_Address;
            logger.Log(LogLevel.Info, "Email From Address: " + txt_Email_From.Text);

            txt_Email_Subject.Text = Email_Subject;
            logger.Log(LogLevel.Info, "Email Subject: " + txt_Email_Subject.Text);

            txt_Email_Message.Text = Email_Message;
            logger.Log(LogLevel.Info, "Email Message: " + txt_Email_Message.Text);

            txt_Network_Id.Text = Network_UserId;
            logger.Log(LogLevel.Info, "Email Network Id: " + txt_Network_Id.Text);

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

                //else if (c.GetType() == typeof(PF.Utils.UserControls.Customer))
                //{
                //    PF.Utils.UserControls.Customer cust = (PF.Utils.UserControls.Customer)c;
                //    cust.CustomerChanged += new EventHandler(this.CustomerControl_CustomerChanged);
                //}
            }

            #endregion Log any interesting events from the UI to the CSV log file
        }

        private DialogResult DLR_Message;

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
                Send_btn();
            }
        }

        private void btn_Send_Click(object sender, EventArgs e)
        {
            Send_btn();

            // Wrong place - is getting called too soon - have to wait until after the second email has gone
            //DeleteAnyPdfsInTheTempFolder();

        }

        private void Send_btn()
        {

            DLR_Message = new DialogResult();
            string str_msg = "";

            // Inside here I can switch off all network validation so i can test for correct email functionality
            // BN 11/08/2015 -----------------------------------------------------------------------------------

            //if (txt_Email_From.TextLength <= 0)
            //{
            //    str_msg = str_msg + "Invalid Email Address. Please enter a valid FP Email Address." + Environment.NewLine;
            //}
            //if (txt_Network_Id.TextLength <= 0)
            //{
            //    str_msg = str_msg + "Invalid UserId. Please enter the valid network UserId, that is associated with the supplied Email Address " + Environment.NewLine;
            //}
            //if (txt_Network_Pswd.TextLength <= 0)
            //{
            //    str_msg = str_msg + "Invalid Password. Please enter the valid network Password, that is associated with the supplied UserId" + Environment.NewLine;
            //}
            //if (txt_Network_Id.TextLength > 0 && txt_Network_Pswd.TextLength > 0)
            //{
            //    if (Validate_User(txt_Network_Id.Text, txt_Network_Pswd.Text) == false)
            //    {
            //        str_msg = str_msg + "Invalid UserId/Password. You have entered either an Invalid UserId or and Invalid Password. Please try again." + Environment.NewLine;
            //    }
            //}

            // End of switch off network validation ---------------------------------------------------------

            if (str_msg.Length > 0)
            {
                DLR_Message = MessageBox.Show(str_msg, "Process Factory - Invoicing(Email)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (DLR_Message != System.Windows.Forms.DialogResult.OK)
            {
                Email_From_Address = txt_Email_From.Text;
                Email_Message = txt_Email_Message.Text;
                Email_Subject = txt_Email_Subject.Text;
                Email_sender_Copy = chk_email_copy.Checked;
                Network_UserId = txt_Network_Id.Text;
                Network_Password = txt_Network_Pswd.Text;
            }
            else
            {
                DLR_Message = System.Windows.Forms.DialogResult.Retry;
            }
        }

        private bool Validate_User(string username, string password)
        {
            bool valid = false;
            using (System.DirectoryServices.AccountManagement.PrincipalContext context = new System.DirectoryServices.AccountManagement.PrincipalContext(System.DirectoryServices.AccountManagement.ContextType.Domain))
            {
                valid = context.ValidateCredentials(username, password);
            }
            return valid;
        }

        private void Invoice_email_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DLR_Message == System.Windows.Forms.DialogResult.Retry)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }

            // Save the address no matter what

            #region Get "Application.StartupPath" folder

            string path = Application.StartupPath;

            #endregion Get "Application.StartupPath" folder

            Settings = new CustomSettings.PhantomCustomSettings();
            Settings.SettingsPath = Path.Combine(path, "FP.Phantom.config");
            Settings.EncryptionKey = "phantomKey";

            Settings.Load();
            Settings.CcToAddress = this.textBoxCcTo.Text;
            Settings.Save();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            DLR_Message = System.Windows.Forms.DialogResult.Cancel;
            DeleteAnyPdfsInTheTempFolder();
        }

        private void btn_Cancel_KeyPress(object sender, KeyPressEventArgs e)
        {
            DLR_Message = System.Windows.Forms.DialogResult.Cancel;
        }

        private void Send_Email_Shown(object sender, EventArgs e)
        {
            try
            {
                //PDF found
                if (txt_Attachments.Text.ToUpper().LastIndexOf(".PDF") > 0)
                {
                }
                else
                {
                    MessageBox.Show("Attachment may be missing, please check.", "Process Factory - Send Email", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("Attachment may be missing, please check.", "Process Factory - Send Email", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        //private void CustomerControl_CustomerChanged(object sender, EventArgs e)
        //{
        //    PF.Utils.UserControls.Customer cust = (PF.Utils.UserControls.Customer)sender;
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
        private void Send_Email_KeyDown(object sender, KeyEventArgs e)
        {
            // Note that the KeyPreview property must be set to true for this to work
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        #endregion Methods to log UI events to the CSV file. BN 29/01/2015

        private void Send_Email_Load(object sender, EventArgs e)
        {
            #region Get "Application.StartupPath" folder

            string path = Application.StartupPath;

            #endregion Get "Application.StartupPath" folder

            Settings = new CustomSettings.PhantomCustomSettings();
            Settings.SettingsPath = Path.Combine(path, "FP.Phantom.config");
            Settings.EncryptionKey = "phantomKey";

            Settings.Load();

            this.textBoxCcTo.Text = Settings.CcToAddress;
        }

        private void checkBoxCcTo_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCcTo.CheckState == CheckState.Checked)
            {
                PF.Data.Outlook.Contacts.Contact_Email.Add(this.textBoxCcTo.Text);   // + Environment.NewLine);
                this.txt_Email_To.AppendText(this.textBoxCcTo.Text);
            }
            else
            {
                txt_Email_To.ResetText();

                // Can't use a foreach here, as I'm modifying the enumeration.
                for (int i = 0; i < PF.Data.Outlook.Contacts.Contact_Email.Count; i++)
                {
                    if (PF.Data.Outlook.Contacts.Contact_Email[i] == this.textBoxCcTo.Text)
                    {
                        PF.Data.Outlook.Contacts.Contact_Email.Remove(this.textBoxCcTo.Text);
                    }
                }

                foreach (string email in PF.Data.Outlook.Contacts.Contact_Email)
                {
                    txt_Email_To.Text = txt_Email_To.Text + email + Environment.NewLine;
                }

            }
        }

        public static void DeleteAnyPdfsInTheTempFolder()
        {
            logger.Log(LogLevel.Info, "DeleteAnyPdfsInTheTempFolder");

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
                    //PF.Common.Code.SendEmail.attachment.Add(TempPath + "\\" + fi.Name);

                    logger.Log(LogLevel.Info, "Deleting: " + fi.Name);

                    // Delete the file
                    fi.Delete();
                }
            }

            #endregion

        }

        private void buttonUpdateEmailToAddress_Click(object sender, EventArgs e)
        {

            var textLines = txt_Email_To.Text.Split('\n');
            foreach (var line in textLines)
            {
                EmailOverriddenList.Add(line);
            }
            buttonUpdateEmailToAddress.Text = "Address Updated";
            logger.Log(LogLevel.Warn, ("Email To has been updated."));

        }
    }
}