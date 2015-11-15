using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace FruPak.PF.Common.Code
{
    public class Outlook
    {
        //public static bool EmailAddressManuallyOverriden    // BN
        //{
        //    get;
        //    set;
        //}

        //public static List <string> EmailOverriddenList     // BN
        //{
        //    get;
        //    set;
        //}

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static string SetUp_Location(string str_code)
        {
            // Returns Process Factory - Contacts   (BN)
            string return_code = "";
            //setup Outlook location
            DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_System.Get_Info_Like(str_code);
            DataRow dr_Get_Info;

            // Table Column names are:
            // System_ID, Code, Description, Value, Active_Ind, Mod_Date, Mod_User_ID

            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                try
                {
                    // So this line says: If the value in the 'Code' field has a value of ??? what does the % mean in this context?

                    // SQL supports following two wildcard operators in conjunction with the LIKE operator:
                    // The percent sign (%)	Matches one or more characters.
                    // Note that MS Access uses the asterisk (*) wildcard character instead of the percent sign (%) wildcard character.
                    // The underscore (_)	Matches one character.
                    // Note that MS Access uses a question mark (?) instead of the underscore (_) to match any one character.

                    // The percent sign represents zero, one, or multiple characters. The underscore represents a single number or character.
                    // The symbols can be used in combinations.
                    // ----------------------------------------

                    // 23-03-2015
                    // Outlook just blew up: Resolved by appending a % symbol to OutLook- OutLook% in CM_System
                    // populate_outlook_Combo: Object reference not set to an instance of an object.
                    // Reversed the change in the CM_System table, and altered the code in PF_Customer line 46 to remove the % at the end of the OutLook string
                    // Also altered line 55 in Common_Maintenance2 to remove the % at the end of the OutLook string
                    // These are the only two places where OutLook% are mentioned.

                    //// Code column has index of 1 (Zero based), and never seems to contain the % character, causing an exception
                    //if (dr_Get_Info.ItemArray.Contains("%") == true)
                    //{
                    //    if (dr_Get_Info["Code"].ToString() == str_code.Substring(0, str_code.IndexOf('%')))
                    //    {
                    //        // Then return the value in the 'Value' field
                    //        return_code = dr_Get_Info["Value"].ToString();
                    //    }
                    //}
                    //else
                    //{
                    if (dr_Get_Info["Code"].ToString() == str_code)
                    {
                        return_code = dr_Get_Info["Value"].ToString();
                    }
                    //}
                }
                catch (Exception ex)
                {
                    logger.Log(LogLevel.Warn, "Not really an error; just means a match on '%' was not found. " + ex.Message);

                    if (dr_Get_Info["Code"].ToString() == str_code)
                    {
                        return_code = dr_Get_Info["Value"].ToString();
                    }
                }
            }
            return return_code;
        }

        public static string Check_Outlook(int Customer_Id, string str_msg, string str_type)
        {
            //set up outlook locations
            FruPak.PF.Data.Outlook.Outlook.Folder_Name = FruPak.PF.Common.Code.Outlook.SetUp_Location("PF-Contact");
            FruPak.PF.Data.Outlook.Outlook.Folder_Name_Location = FruPak.PF.Data.Outlook.Outlook.Folder_Name_Location;

            DataSet ds = FruPak.PF.Data.AccessLayer.PF_Customer.Get_Info(Customer_Id);
            DataRow dr;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dr = ds.Tables[0].Rows[i];
                FruPak.PF.Data.Outlook.Contacts.Contact_Details(dr["Outlook_Key"].ToString());
            }
            ds.Dispose();

            switch (str_type)
            {
                case "Print":
                    str_msg = str_msg + FruPak.PF.Common.Code.General.Check_Address();
                    break;

                case "Email":
                    str_msg = str_msg + FruPak.PF.Common.Code.General.Check_Email();
                    break;
            }
            str_msg = str_msg + FruPak.PF.Common.Code.General.Check_Name();
            return str_msg;
        }

        private static bool bol_copy_to_office = true;



        public static int Email(string str_msg, bool copy_to_office)
        {
            int int_return = 0;
            System.Windows.Forms.DialogResult DLR_Message = new System.Windows.Forms.DialogResult();

            str_msg = str_msg + FruPak.PF.Common.Code.General.Check_Email();

            if (str_msg.Length > 0)
            {
                DLR_Message = System.Windows.Forms.MessageBox.Show(str_msg, "Process Factory - Accounting (Invoice - Email)", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            bol_copy_to_office = copy_to_office;

            DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_System_Email.Get_Info_Like("PF%");
            DataRow dr_Get_Info;

            for (int i = 0; i < ds_Get_Info.Tables[0].Rows.Count; i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                switch (dr_Get_Info["Code"].ToString())
                {
                    case "PF-Addr":
                        FruPak.PF.Common.Code.Send_Email.Email_From_Address = dr_Get_Info["Value"].ToString();
                        break;

                    case "PF-UserID":
                        FruPak.PF.Common.Code.Send_Email.Network_UserId = dr_Get_Info["Value"].ToString();
                        break;

                    case "PF-InvSub":
                        if (FruPak.PF.Common.Code.Send_Email.Email_Subject == "")
                        {
                            FruPak.PF.Common.Code.Send_Email.Email_Subject = dr_Get_Info["Value"].ToString();
                        }
                        break;

                    case "PF-InvMsg":
                        if (FruPak.PF.Common.Code.Send_Email.Email_Message == "")
                        {
                            FruPak.PF.Common.Code.Send_Email.Email_Message = dr_Get_Info["Value"].ToString();
                        }
                        else
                        {
                            FruPak.PF.Common.Code.Send_Email.Email_Message = FruPak.PF.Common.Code.Send_Email.Email_Message + Environment.NewLine + FruPak.PF.Common.Code.Send_Email.Network_UserId;
                        }
                        break;
                }
            }
            ds_Get_Info.Dispose();

            Form frm = new FruPak.PF.Common.Code.Send_Email();
            DLR_Message = frm.ShowDialog();

            if (DLR_Message == System.Windows.Forms.DialogResult.OK)
            {
                FruPak.PF.Common.Code.SendEmail.From_Email_Address = FruPak.PF.Common.Code.Send_Email.Email_From_Address;
                FruPak.PF.Common.Code.SendEmail.Network_UserId = FruPak.PF.Common.Code.Send_Email.Network_UserId;
                FruPak.PF.Common.Code.SendEmail.Network_Password = FruPak.PF.Common.Code.Send_Email.Network_Password;

                FruPak.PF.Common.Code.SendEmail.Subject = FruPak.PF.Common.Code.Send_Email.Email_Subject;
                FruPak.PF.Common.Code.SendEmail.IsBodyHtml = false;
                FruPak.PF.Common.Code.SendEmail.message = FruPak.PF.Common.Code.Send_Email.Email_Message;

                if (FruPak.PF.Common.Code.Send_Email.EmailOverriddenList.Count > 0)
                {
                    //logger.Log(LogLevel.Warn, "Email recipient has been overridden: " + FruPak.PF.Common.Code.SendEmail.Recipient);
                    FruPak.PF.Common.Code.SendEmail.Recipient.Clear();
                    for (int i = 0; i < Send_Email.EmailOverriddenList.Count; i++)
                    {
                        if(Send_Email.EmailOverriddenList[i] != "")
                        {
                            FruPak.PF.Common.Code.SendEmail.Recipient.Add(Send_Email.EmailOverriddenList[i]);
                        }
                    }
                    FruPak.PF.Common.Code.Send_Email.EmailOverriddenList.Clear();
                }
                else
                {
                    FruPak.PF.Common.Code.SendEmail.Recipient = FruPak.PF.Data.Outlook.Contacts.Contact_Email;
                }

                FruPak.PF.Common.Code.SendEmail.BCC_Email_Address = new List<string>();
                if (FruPak.PF.Common.Code.Send_Email.Email_sender_Copy == true)
                {
                    try
                    {
                        FruPak.PF.Common.Code.SendEmail.BCC_Email_Address.Add(FruPak.PF.Common.Code.Send_Email.Email_From_Address);
                    }
                    catch (Exception ex)
                    {
                        logger.Log(LogLevel.Debug, ex.Message);
                    }
                }

                str_msg = "";
                str_msg = FruPak.PF.Common.Code.SendEmail.send_mail();
                if (str_msg.Length > 0)
                {
                    DLR_Message = MessageBox.Show(str_msg, "Process Factory - Accounting (Invoice - Email)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int_return = 0;
                }
            }
            else
            {
                int_return = 9;
            }

            return int_return;
        }

        public static int Email_Office(string str_Office, string str_save_filename, string str_msg)
        {
            int int_result = 0;
            //Get Offcie Email Address
            string str_office_email = "";
            string str_staff_Name = "";

            List<string> str_email_msg = new List<string>();
            DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_System_Email.Get_Info_Like(str_Office);
            DataRow dr_Get_Info;
            for (int i = 0; i < ds_Get_Info.Tables[0].Rows.Count; i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                switch (dr_Get_Info["Code"].ToString())
                {
                    case "FP-Off1":
                    case "FP-Off2":
                    case "FP-Off3":
                        str_office_email = dr_Get_Info["Value"].ToString();
                        str_staff_Name = str_office_email.Substring(0, str_office_email.IndexOf('@'));
                        break;

                    case "FP-Off1Mg1":
                    case "FP-Off1Mg2":
                    case "FP-Off1Mg3":
                    case "FP-Off2Mg1":
                    case "FP-Off2Mg2":
                    case "FP-Off2Mg3":
                    case "FP-Off3Mg1":
                    case "FP-Off3Mg2":
                    case "FP-Off3Mg3":
                        str_email_msg.Add(dr_Get_Info["Value"].ToString());
                        break;
                }
            }
            ds_Get_Info.Dispose();

            switch (str_Office)
            {
                case "FP-Off1%":
                    FruPak.PF.Common.Code.Send_Email.Email_Subject = "Invoice for " + str_save_filename;
                    break;

                case "FP-Off2%":
                    FruPak.PF.Common.Code.Send_Email.Email_Subject = "Fruit Purchase for" + str_save_filename;
                    break;

                case "FP-Off3%":
                    FruPak.PF.Common.Code.Send_Email.Email_Subject = "Outstanding Invoices";
                    break;
            }

            try
            {
                FruPak.PF.Common.Code.Send_Email.Email_Message = str_staff_Name + Environment.NewLine;
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message);
            }
            try
            {
                FruPak.PF.Common.Code.Send_Email.Email_Message = FruPak.PF.Common.Code.Send_Email.Email_Message + str_email_msg[0] + Environment.NewLine;
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message);
            }

            try
            {
                switch (str_Office)
                {
                    case "FP-Off1%":
                    case "FP-Off2%":
                        FruPak.PF.Common.Code.Send_Email.Email_Message = FruPak.PF.Common.Code.Send_Email.Email_Message + str_save_filename + Environment.NewLine;
                        break;

                    default:
                        FruPak.PF.Common.Code.Send_Email.Email_Message = FruPak.PF.Common.Code.Send_Email.Email_Message + Environment.NewLine;
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message);
            }
            try
            {
                FruPak.PF.Common.Code.Send_Email.Email_Message = FruPak.PF.Common.Code.Send_Email.Email_Message + str_email_msg[1] + Environment.NewLine;
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message);
            }
            try
            {
                FruPak.PF.Common.Code.Send_Email.Email_Message = FruPak.PF.Common.Code.Send_Email.Email_Message + str_email_msg[2];
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message);
            }

            FruPak.PF.Data.Outlook.Contacts.Contact_Email = new List<string>();
            FruPak.PF.Data.Outlook.Contacts.Contact_Email.Clear();
            FruPak.PF.Data.Outlook.Contacts.Contact_Email.Add(str_office_email);
            int_result = Outlook.Email(str_msg, false);
            return int_result;
        }
    }
}