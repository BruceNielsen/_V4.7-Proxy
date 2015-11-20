using Microsoft.Office.Interop.Outlook;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;

namespace PF.Data.Outlook
{
    /*Description
    -----------------
    Interface to Outlook.

    Date        Author     Desc
    -------------------------------------------------------------------------------------------------------------------------------------------------
    01/09/2013  Dave       Creation
    14/04/2015  Bruce      Added error handling to return null if anything goes wrong with the Outlook connection
    */

    public class Contacts
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Create a new Contact using the Outlook form.
        /// </summary>
        ///
        public static void Create_Contact()
        {
            if (Outlook.Folder_Name_Locationfield != null)
            {
                _Items oItems = Outlook.Folder_Name_Locationfield.Items;
                _ContactItem oContact = (_ContactItem)oItems.Add("IPM.Contact");
                oContact.Display(true);
            }
            else
            {
                logger.Log(LogLevel.Debug, "Outlook.Folder_Name_Locationfield == null");
            }
        }

        public static string Create_Contact2(bool bol_save_display)
        {
            if (Outlook.Folder_Name_Locationfield != null)
            {
                _Items oItems = Outlook.Folder_Name_Locationfield.Items;
                _ContactItem oContact = (_ContactItem)oItems.Add("IPM.Contact");
                oContact.FirstName = Contact_First_Name;
                oContact.LastName = Contact_Last_Name;
                oContact.CompanyName = Contact_Company_Name;

                if (Contact_Company_Name == "")
                {
                    oContact.Email1DisplayName = Contact_First_Name + " " + Contact_Last_Name;
                }
                else
                {
                    oContact.Email1DisplayName = Contact_Company_Name;
                }

                oContact.BusinessTelephoneNumber = Contact_Business_Telephone_Number;
                oContact.Business2TelephoneNumber = Contact_Business_Telephone_Number2;

                oContact.BusinessFaxNumber = Contact_Business_Fax_Number;

                //update email addresses
                int ei = 1;
                foreach (string email in Contact_Email)
                {
                    switch (ei)
                    {
                        case 1:
                            oContact.Email1Address = email;
                            break;

                        case 2:
                            oContact.Email2Address = email;
                            break;

                        case 3:
                            oContact.Email3Address = email;
                            break;
                    }
                    ei++;
                }
                oContact.MobileTelephoneNumber = Contact_Mobile;
                oContact.JobTitle = Contact_Job_Title;
                oContact.BusinessFaxNumber = Contact_Business_Fax_Number;
                oContact.Body = Contact_Notes;

                if (bol_save_display == true)
                {
                    oContact.Save();
                }
                else
                {
                    oContact.Display(true);
                }

                return oContact.EntryID;
            }
            else
            {
                logger.Log(LogLevel.Debug, "Outlook.Folder_Name_Locationfield == null");
                return null;
            }
        }

        public static string Contact_First_Name
        {
            get;
            set;
        }

        public static string Contact_Last_Name
        {
            get;
            set;
        }

        public static string Contact_Company_Name
        {
            get;
            set;
        }

        public static string Contact_Job_Title
        {
            get;
            set;
        }

        public static string Contact_Business_Telephone_Number
        {
            get;
            set;
        }

        public static string Contact_Business_Telephone_Number2
        {
            get;
            set;
        }

        public static string Contact_Business_Fax_Number
        {
            get;
            set;
        }

        public static string Contact_Mobile
        {
            get;
            set;
        }

        public static List<string> Contact_Email
        {
            get;
            set;
        }

        public static string Contact_Notes
        {
            get;
            set;
        }

        public static string Contact_Business_Address
        {
            get;
            set;
        }

        public static string Contact_Business_City
        {
            get;
            set;
        }

        public static string Contact_Business_Postal_Code
        {
            get;
            set;
        }

        public static string Contact_Other_Address
        {
            get;
            set;
        }

        public static string Contact_Other_City
        {
            get;
            set;
        }

        public static string Contact_Other_Postal_Code
        {
            get;
            set;
        }

        public static int Contact_Display(string EntryId)
        {
            int return_code = 0;
            try
            {
                ContactItem contact = Outlook.OutLook_App.Session.GetItemFromID(EntryId, Outlook.Folder_Name_Locationfield.StoreID);
                contact.Display(true);
                return_code = 0;
            }
            catch
            {
                return_code = 1;
            }
            return return_code;
        }

        public static string Contact_Get_Entry_ID(string str_FirstName, string str_LastName, string str_jobtitle)
        {
            string Entry_Id = "";

            if (str_FirstName.Trim().Length == 0)
            {
                str_FirstName = null;
            }
            else
            {
                str_FirstName = str_FirstName.Trim();
            }

            if (str_LastName.Trim().Length == 0)
            {
                str_LastName = null;
            }
            else
            {
                str_LastName = str_LastName.Trim();
            }

            if (Outlook.Folder_Name_Locationfield != null)
            {
                foreach (object obj in Outlook.Folder_Name_Locationfield.Items)
                {
                    if (obj is _ContactItem)
                    {
                        var contact = (_ContactItem)obj;
                        try
                        {
                            if (contact.FirstName == str_FirstName && contact.LastName == str_LastName && contact.JobTitle == str_jobtitle)
                            {
                                Entry_Id = contact.EntryID;
                            }
                        }
                        catch (System.Exception ex)
                        {
                            logger.Log(LogLevel.Debug, ex.Message);
                        }
                    }
                }

                return Entry_Id;
            }
            else
            {
                logger.Log(LogLevel.Debug, "Outlook.Folder_Name_Locationfield == null");
                return null;
            }
        }

        public static void Contact_Delete(string Entry_Id)
        {
            if (Outlook.Folder_Name_Locationfield != null)
            {
                foreach (object obj in Outlook.Folder_Name_Locationfield.Items)
                {
                    if (obj is _ContactItem)
                    {
                        var contact = (_ContactItem)obj;
                        try
                        {
                            if (contact.EntryID == Entry_Id)
                            {
                                contact.Delete();
                            }
                        }
                        catch (System.Exception ex)
                        {
                            logger.Log(LogLevel.Debug, ex.Message);
                        }
                    }
                }
            }
            else
            {
                logger.Log(LogLevel.Debug, "Outlook.Folder_Name_Locationfield == null");
            }
        }

        public static void Contact_Update(string Entry_Id)
        {
            if (Outlook.Folder_Name_Locationfield != null)
            {
                foreach (object obj in Outlook.Folder_Name_Locationfield.Items)
                {
                    if (obj is _ContactItem)
                    {
                        var contact = (_ContactItem)obj;
                        try
                        {
                            if (contact.EntryID == Entry_Id)
                            {
                                contact.FirstName = Contact_First_Name;
                                contact.LastName = Contact_Last_Name;
                                contact.CompanyName = Contact_Company_Name;
                                contact.JobTitle = Contact_Job_Title;
                                contact.Save();
                            }
                        }
                        catch (System.Exception ex)
                        {
                            logger.Log(LogLevel.Debug, ex.Message);
                        }
                    }
                }
            }
            else
            {
                logger.Log(LogLevel.Debug, "Outlook.Folder_Name_Locationfield == null");
            }
        }

        public static void Contact_Details(string str_Entry_Id)
        {
            try
            {
                if (Outlook.Folder_Name_Locationfield != null)
                {
                    string outlookFolderName = Outlook.Folder_Name;
                    string outlookFolderNameLocation = Outlook.Folder_Name_Location.FolderPath;
                    Console.WriteLine(outlookFolderName + outlookFolderNameLocation);
                    // This retrieves the type name (MAPIfolder)
                    //object selection = Outlook.Folder_Name_Locationfield;
                    //if (selection != null)
                    //{
                    //    string typeName = ComUtils.ComHelper.GetTypeName(selection);
                    //    //Debug.Print(typeName);

                    //    // If you release this here, it breaks everything, unsurprisingly enough.
                    //    Marshal.ReleaseComObject(selection);
                    //}

                    foreach (object obj in Outlook.Folder_Name_Locationfield.Items)
                    {
                        if (obj is _ContactItem)
                        {
                            var contact = (_ContactItem)obj;
                            try
                            {
                                //Console.WriteLine("PF.Data.Outlook.Contacts.Contact_Details:");
                                //Console.WriteLine("contact.EntryID: \t" + contact.EntryID);
                                //Console.WriteLine("str_Entry_Id: \t\t" + str_Entry_Id);
                                //Console.WriteLine("---\r\n");
                                if (contact.EntryID == str_Entry_Id)
                                {
                                    Console.WriteLine("PF.Data.Outlook.Contacts.Contact_Details: Match between contact id and entry id.");
                                    if (contact.CompanyName != "" && contact.CompanyName != null) // BN
                                    {
                                        Contact_Company_Name = contact.CompanyName.ToString();
                                    }
                                    if (contact.FirstName == null)
                                    {
                                        Contact_First_Name = "";
                                    }
                                    else
                                    {
                                        Contact_First_Name = contact.FirstName.ToString();
                                    }
                                    if (contact.LastName == null)
                                    {
                                        Contact_Last_Name = "";
                                    }
                                    else
                                    {
                                        Contact_Last_Name = contact.LastName.ToString();
                                    }
                                    if (contact.BusinessAddressStreet == null)
                                    {
                                        Contact_Business_Address = "";
                                    }
                                    else
                                    {
                                        Contact_Business_Address = contact.BusinessAddressStreet;
                                    }
                                    if (contact.BusinessAddressCity == null)
                                    {
                                        Contact_Business_City = "";
                                    }
                                    else
                                    {
                                        Contact_Business_City = contact.BusinessAddressCity;
                                    }
                                    if (contact.BusinessAddressPostalCode == null)
                                    {
                                        Contact_Business_Postal_Code = "";
                                    }
                                    else
                                    {
                                        Contact_Business_Postal_Code = contact.BusinessAddressPostalCode;
                                    }
                                    if (contact.OtherAddressStreet == null)
                                    {
                                        Contact_Other_Address = "";
                                    }
                                    else
                                    {
                                        Contact_Other_Address = contact.OtherAddressStreet;
                                    }
                                    if (contact.OtherAddressCity == null)
                                    {
                                        Contact_Other_City = "";
                                    }
                                    else
                                    {
                                        Contact_Other_City = contact.OtherAddressCity;
                                    }
                                    if (contact.OtherAddressPostalCode == null)
                                    {
                                        Contact_Other_Postal_Code = "";
                                    }
                                    else
                                    {
                                        Contact_Other_Postal_Code = contact.OtherAddressPostalCode;
                                    }
                                    //get email addresses
                                    Contact_Email = new List<string>();
                                    if (contact.Email1Address != null)
                                    {
                                        Contact_Email.Add(contact.Email1Address.ToString());
                                    }
                                    if (contact.Email2Address != null)
                                    {
                                        Contact_Email.Add(contact.Email2Address.ToString());
                                    }
                                    if (contact.Email3Address != null)
                                    {
                                        Contact_Email.Add(contact.Email3Address.ToString());
                                    }
                                }
                            }
                            catch
                            {
                                Contact_Company_Name = "";  //BN
                                Contact_First_Name = "";
                                Contact_Last_Name = "";
                                Contact_Business_Address = "";
                                Contact_Business_City = "";
                                Contact_Business_Postal_Code = "";
                                Contact_Other_Address = "";
                                Contact_Other_City = "";
                                Contact_Other_Postal_Code = "";
                            }
                        }
                    }
                }
                else
                {
                    logger.Log(LogLevel.Debug, "Outlook.Folder_Name_Locationfield == null");
                }
            }
            catch (System.Exception ex)
            {
                // Can't get to this: Microsoft.Office.Interop.Outlook.Exception
                // Probable cause is Outlook has been closed.
                logger.Log(LogLevel.Debug, "Probable Microsoft.Office.Interop.Outlook.Exception - Usually caused by Outlook having been closed while the program is running. " + ex.Message);
            }
        }

        public static DataSet Contact_List_All()
        {
            DataSet ds_contacts = new DataSet();
            DataTable dt_contacts = new DataTable();

            ds_contacts.Tables.Add(dt_contacts);

            DataColumn dc_combined = new DataColumn("combined", typeof(string));
            dt_contacts.Columns.Add(dc_combined);
            DataColumn dc_company = new DataColumn("company", typeof(string));
            dt_contacts.Columns.Add(dc_company);
            DataColumn dc_FirstName = new DataColumn("FirstName", typeof(string));
            dt_contacts.Columns.Add(dc_FirstName);
            DataColumn dc_LastName = new DataColumn("LastName", typeof(string));
            dt_contacts.Columns.Add(dc_LastName);
            DataColumn dc_JobTitle = new DataColumn("JobTitle", typeof(string));
            dt_contacts.Columns.Add(dc_JobTitle);

            DataColumn dc_Entry_Id = new DataColumn("Entry_Id", typeof(string));
            dt_contacts.Columns.Add(dc_Entry_Id);

            dt_contacts.Rows.Add(dt_contacts.NewRow());

            // Not sure if this warning is real - I don't think it is.
            foreach (object obj in Outlook.Folder_Name_Locationfield.Items)
            {
                DataRow dr = dt_contacts.NewRow();

                if (obj is _ContactItem)
                {
                    var contact = (_ContactItem)obj;
                    try
                    {
                        if (contact.CompanyName != null && contact.CompanyName.ToString() != "")
                        {
                            dr[0] = contact.CompanyName.ToString();
                            dr[1] = contact.CompanyName.ToString();
                        }
                        if (contact.FirstName != null && contact.FirstName.ToString() != "")
                        {
                            dr[0] = dr[0] + " " + contact.FirstName.ToString();
                            dr[2] = contact.FirstName.ToString();
                        }
                        if (contact.LastName != null && contact.LastName.ToString() != "")
                        {
                            dr[0] = dr[0] + " " + contact.LastName.ToString();
                            dr[3] = contact.LastName.ToString();
                        }
                        // Added trap for null to get rid of all the zillion error messages about the job title
                        // BN 30/12/2014
                        if (contact.JobTitle != null && contact.JobTitle.ToString() != "")
                        {
                            dr[4] = contact.JobTitle.ToString();
                        }
                    }
                    catch (System.Exception ex)
                    {
                        logger.Log(LogLevel.Debug, ex.Message);
                    }
                    dr[5] = contact.EntryID.ToString();
                    dt_contacts.Rows.Add(dr);
                }
            }

            dt_contacts.DefaultView.Sort = "company";

            dt_contacts = dt_contacts.DefaultView.ToTable();

            return ds_contacts;
        }

        // Experimenting with adding a constructor (Didn't work) BN 22-04-2015
        //public Contacts()
        //{
        //    DataSet ds = Contact_List_All();
        //    Console.WriteLine(ds.Tables.Count.ToString());
        //}
    }
}