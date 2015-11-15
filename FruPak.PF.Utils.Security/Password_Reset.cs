using NLog;
using System;
using System.Windows.Forms;

namespace FruPak.PF.Utils.Security
{
    /*Description
    -----------------
    Password Reset Class.
        *
        * This class is a form used change a users password.
        * It is executed by the user when they wish to change there password at logon.

    Date        Author     Desc
    -------------------------------------------------------------------------------------------------------------------------------------------------
    01/09/2013  Dave       Creation
    */

    public partial class Password_Reset : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private string str_Logon1 = "";
        private int int_User_id = 0;

        public Password_Reset(string str_Logon)
        {
            InitializeComponent();
            str_Logon1 = str_Logon;

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

        private void btn_Update_Click(object sender, EventArgs e)
        {
            int_User_id = General.User_Id_Retrival(str_Logon1);

            DialogResult DLR_MessageBox_Old = new DialogResult();
            DialogResult DLR_MessageBox_New = new DialogResult();

            if (txt_Password_Old.TextLength == 0)
            {
                DLR_MessageBox_Old = MessageBox.Show("Please Enter your old Password", "Security - Password Change/Reset", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (General.Password_Validate(str_Logon1, txt_Password_Old.Text) == false)
                {
                    DLR_MessageBox_Old = MessageBox.Show("Old Password is Invalid- Please try again", "Security - Password Change/Reset", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (DLR_MessageBox_Old != DialogResult.OK)
            {
                if (txt_Password_1.TextLength == 0)
                {
                    DLR_MessageBox_New = MessageBox.Show("Please Enter your New Password", "Security - Password Change/Reset", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txt_Password_1.Text != txt_Password_2.Text)
                {
                    DLR_MessageBox_New = MessageBox.Show("Your Re-Entered New Password does NOT match your New Password", "Security - Password Change/Reset", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (DLR_MessageBox_New != DialogResult.OK)
                {
                    int int_result = 0;

                    int_result = FruPak.PF.Data.AccessLayer.SC_User.Update_User_Password(int_User_id, General.Password_Encryption(txt_Password_1.Text), int_User_id);

                    if (int_result > 0)
                    {
                        lbl_message.ForeColor = System.Drawing.Color.Blue;
                        lbl_message.Text = "Password has been updated";
                        Reset();
                    }
                    else
                    {
                        lbl_message.ForeColor = System.Drawing.Color.Red;
                        lbl_message.Text = "Password failed to update - Please try again";
                    }
                }
            }
            FruPak.PF.Common.Code.General.write_log(int_User_id, "PWD - Updated", int_User_id);
        }

        private void Reset()
        {
            txt_Password_1.ResetText();
            txt_Password_2.ResetText();
            txt_Password_Old.ResetText();
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            FruPak.PF.Common.Code.General.write_log(int_User_id, "PWD - Cancelled", int_User_id);
            this.Close();
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
        private void Password_Reset_KeyDown(object sender, KeyEventArgs e)
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