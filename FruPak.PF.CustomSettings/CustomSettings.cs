using System.ComponentModel;

namespace FruPak.PF.CustomSettings
{
    /// <summary>
    /// Create customized Settings class
    /// Phantom 17/12/2014
    /// </summary>
    public class PhantomCustomSettings : SettingsBase
    {
        public PhantomCustomSettings()
        {
            // One-time only setup to be commented out once the very first settings file has been generated.
            Path_Local_Path = @"C:\FruPak";
            Path_Local_PDF_Files = @"C:\FruPak\Client\Printing\Temp";
            Path_Remote_Path = @"P:\FruPak";
            Path_AcroWrap_Path = @"C:\FruPak\Client";
            Path_Printer_Temp = @"\\FRUPAK-SBS\\PublicDocs\\FruPak\\Server\\Temp";
            Path_Helpfile_Path = @"C:\\FruPak\\Documentation\\Help\\Process Factory.chm";
            Path_SMTP_Host = "FRUPAK-SBS.frupak.local";
            //Path_To_Settings = "Hello";   // Set over in Install.cs
            Printer_Name = "Brother HL-2040 series";
            CopyAtStartup = false;
            CopyDelayInMinutes = 60;
            //MaxTreeFileSizeInMegabytes = "1";
            CcToAddress = "suel@FruPak.co.nz";

            ShowWordWarningMessage = true;

            UpdateAddress = "http://192.168.80.69:8080/FruPak-Update/FruPak-PF-Update.xml";
            UpdateProxyUsername = "administrator";
            UpdateProxyPassword = "1234";
            UpdateProxyURI = "http://FruPak-Sql";
            UpdateProxyPort = "8090";
            UpdateUseProxy = true;

            #region FruPak Test Mode Database Connection Settings

            TestMode_Server = "FRUPAK-SQL";
            TestMode_Database = "Process_Factory_Test";
            TestMode_User_Id = "jobs";
            TestMode_Password = "jobs";

            #endregion FruPak Test Mode Database Connection Settings

            #region FruPak Production Mode Database Connection Settings

            ProductionMode_Server = "FRUPAK-SQL";
            ProductionMode_Database = "Process_Factory";
            ProductionMode_User_Id = "jobs";
            ProductionMode_Password = "jobs";

            #endregion FruPak Production Mode Database Connection Settings

            #region Phantom Test Mode Database Connection Settings

            Phantom_Dev_TestMode_Server = @"-\SQLEXPRESS";
            Phantom_Dev_TestMode_Database = "Process_Factory_Test";
            Phantom_Dev_TestMode_User_Id = "jobs";
            Phantom_Dev_TestMode_Password = "jobs";

            #endregion Phantom Test Mode Database Connection Settings

            #region Phantom Production Mode Database Connection Settings

            Phantom_Dev_ProductionMode_Server = @"-\SQLEXPRESS";
            Phantom_Dev_ProductionMode_Database = "Process_Factory";
            Phantom_Dev_ProductionMode_User_Id = "jobs";
            Phantom_Dev_ProductionMode_Password = "jobs";

            #endregion Phantom Production Mode Database Connection Settings

            Phantom_Dev_Mode = true;
            Phantom_Dev_Use_FruPak_SQL = true;
            //Phantom_Dev_Test_Mode = true;
            //Phantom_Dev_EnableResize = true;
            //EncryptionKey = "phantomKey"; // The key has already been set over in Install.cs
        }

        // Declare application settings

        #region Phantom Settings

        [DisplayName("Developer Mode")]
        [Description("Allows form resize, minimise/maximise etc..")]
        [Category("Phantom Technologies")]
        public bool Phantom_Dev_Mode { get; set; }

        [DisplayName("Use FruPak-SQL")]
        [Description("True = use FruPak database. False = Use Phantom copy")]
        [Category("Phantom Technologies")]
        public bool Phantom_Dev_Use_FruPak_SQL { get; set; }

        [DisplayName("Copy At Startup")]
        [Description("True = Copy files from the Remote path to the Local path.\r\nFalse = Do not copy.")]
        [Category("File Copy")]
        public bool CopyAtStartup { get; set; }

        [DisplayName("Copy Delay In Minutes")]
        [Description("Delay time in minutes. 120 = 2 Hours etc.")]
        [Category("File Copy")]
        public double CopyDelayInMinutes { get; set; }

        [DisplayName("Max Tree Size In Megabytes")]
        [Description("Max Tree File Size In Megabytes. 1, 2 etc = 1 or 2 Megabytes.")]
        [Category("Phantom Technologies")]
        public string MaxTreeFileSizeInMegabytes { get; set; }

        [Browsable(false)]
        [DisplayName("XML File Address")]
        [Description("Http location of XML file. IP or /url:Port/Filename. ie. http://127.0.0.1:8099/FruPak-PF-Update.xml")]
        [Category("Update Settings")]
        public string UpdateAddress { get; set; }

        [Browsable(false)]
        [DisplayName("Proxy Username")]
        [Description("Proxy Username")]
        [Category("Update Settings")]
        public string UpdateProxyUsername { get; set; }

        [Browsable(false)]
        [DisplayName("Proxy Password")]
        [Description("Proxy Password")]
        [Category("Update Settings")]
        public string UpdateProxyPassword { get; set; }

        [Browsable(false)]
        [DisplayName("Proxy URI")]
        [Description("Proxy URI")]
        [Category("Update Settings")]
        public string UpdateProxyURI { get; set; }

        [Browsable(false)]
        [DisplayName("Proxy Port")]
        [Description("Proxy Port")]
        [Category("Update Settings")]
        public string UpdateProxyPort { get; set; }

        [Browsable(false)]
        [DisplayName("Use Proxy")]
        [Description("Use the proxy or not")]
        [Category("Update Settings")]
        public bool UpdateUseProxy { get; set; }

        [DisplayName("Show Word warning message")]
        [Description("Warn the user that any open Word documents will be closed without warning.")]
        [Category("Phantom Technologies")]
        public bool ShowWordWarningMessage { get; set; }

        //[DisplayName("Developer Size Mode")]
        //[Description("Allows or Disallows resizing the Main Menu Form.")]
        //[Category("Phantom Technologies")]

        //public bool Phantom_Dev_EnableResize { get; set; }

        #region Local Path

        [DisplayName("Local Path")]
        [Description("Local path to the FruPak directory.")]
        [Category("Path Settings")]
        public string Path_Local_Path { get; set; }

        #endregion Local Path

        #region Path_Local_PDF_Files

        [DisplayName("Local PDF Path")]
        [Description("Local path to the PDF directory.")]
        [Category("Path Settings")]
        public string Path_Local_PDF_Files { get; set; }

        #endregion Local PDF Path
        
        #region Remote Path

        [DisplayName("Remote Path")]
        [Description("Remote path to the FruPak directory.\r\nCurrently, the app will crash if it can't find the folder.\r\nOn my list of things to fix.")]
        [Category("Path Settings")]
        public string Path_Remote_Path { get; set; }

        #endregion Remote Path

        #region AcroWrap Path

        [DisplayName("AcroWrap Path")]
        [Description("Remote path to the AcroWrap Executable directory.")]
        [Category("Path Settings")]
        public string Path_AcroWrap_Path { get; set; }

        #endregion AcroWrap Path

        #region Printer Temp Path

        [DisplayName("Printer Temp Path")]
        [Description("Path to the Printer Temp directory.")]
        [Category("Path Settings")]
        public string Path_Printer_Temp { get; set; }

        #endregion Printer Temp Path

        #region Helpfile Path

        [DisplayName("Helpfile Path")]
        [Description("Path to the Helpfile (.chm).")]
        [Category("Path Settings")]
        public string Path_Helpfile_Path { get; set; }

        #endregion Helpfile Path

        #region SMTP Host

        [DisplayName("SMTP Host")]
        [Description("SMTP Host name")]
        [Category("Path Settings")]
        public string Path_SMTP_Host { get; set; }

        #endregion SMTP Host

        // Path_Printer_Temp_Folder

        //[DisplayName("Path to the XML settings file")]
        //[Description("Hardcoded (for now) to Documents\\FruPak_Settings.")]
        //[Category("General Settings")]

        //public string Path_To_Settings { get; set; }

        [DisplayName("Printer Name")]
        [Description("Pallet Card Printer.\r\nAll 3 hardcoded references have now been removed,\r\nso now change to a new printer at will.")]
        [Category("General Settings")]
        public string Printer_Name { get; set; }

        // This must be in sync across: 1. Logon.cs, 2. Install.cs and 3. Main_menu.cs - on the list of things to get to... (BN)
        //[DisplayName("Encryption Key")]
        //[Description("DES key to use when calculating password hashes.")]
        //[Category("General Settings")]

        //public new string EncryptionKey { get; set; }   // Using the new keyword to hide the other one deep down in the UserSettings class

        // Most of this is duplicated, but coded for ultimate flexibility

        #region FruPak Test Mode Database Connection Settings

        [DisplayName("1. Test Server")]
        [Description("FruPak Server Name.")]
        [Category("FruPak DB Test")]
        public string TestMode_Server { get; set; }

        [DisplayName("2. Test Database")]
        [Description("FruPak Database Name.")]
        [Category("FruPak DB Test")]
        public string TestMode_Database { get; set; }

        [DisplayName("3. Test User ID")]
        [Description("FruPak Server User ID.")]
        [Category("FruPak DB Test")]
        public string TestMode_User_Id { get; set; }

        [DisplayName("4. Test Password")]
        [Description("Decrypted password. Is stored encrypted in the XML file.")]
        [Category("FruPak DB Test")]
        public string TestMode_Password { get; set; }

        #endregion FruPak Test Mode Database Connection Settings

        #region FruPak Production Mode Database Connection Settings

        [DisplayName("1. Production Server")]
        [Description("FruPak Server Name.")]
        [Category("FruPak DB Production")]
        public string ProductionMode_Server { get; set; }

        [DisplayName("2. Production Database")]
        [Description("FruPak Database Name.")]
        [Category("FruPak DB Production")]
        public string ProductionMode_Database { get; set; }

        [DisplayName("3. Production User ID")]
        [Description("FruPak Server User ID.")]
        [Category("FruPak DB Production")]
        public string ProductionMode_User_Id { get; set; }

        [DisplayName("4. Production Password")]
        [Description("Decrypted password. Is stored encrypted in the XML file.")]
        [Category("FruPak DB Production")]
        public string ProductionMode_Password { get; set; }

        #endregion FruPak Production Mode Database Connection Settings

        #region Phantom Test Mode Database Connection Settings

        [DisplayName("1. Test Server")]
        [Description("Phantom Server Name.")]
        [Category("Phantom DB Test")]
        public string Phantom_Dev_TestMode_Server { get; set; }

        [DisplayName("2. Test Database")]
        [Description("Phantom Database Name.")]
        [Category("Phantom DB Test")]
        public string Phantom_Dev_TestMode_Database { get; set; }

        [DisplayName("3. Test User ID")]
        [Description("Phantom Server User ID.")]
        [Category("Phantom DB Test")]
        public string Phantom_Dev_TestMode_User_Id { get; set; }

        [DisplayName("4. Test Password")]
        [Description("Decrypted password. Is stored encrypted in the XML file.")]
        [Category("Phantom DB Test")]
        public string Phantom_Dev_TestMode_Password { get; set; }

        #endregion Phantom Test Mode Database Connection Settings

        #region Phantom Production Mode Database Connection Settings

        [DisplayName("1. Production Server")]
        [Description("Phantom Server Name.")]
        [Category("Phantom DB Production")]
        public string Phantom_Dev_ProductionMode_Server { get; set; }

        [DisplayName("2. Production Database")]
        [Description("Phantom Database Name.")]
        [Category("Phantom DB Production")]
        public string Phantom_Dev_ProductionMode_Database { get; set; }

        [DisplayName("3. Production User ID")]
        [Description("Phantom Server User ID.")]
        [Category("Phantom DB Production")]
        public string Phantom_Dev_ProductionMode_User_Id { get; set; }

        [DisplayName("4. Production Password")]
        [Description("Decrypted password. Is stored encrypted in the XML file.")]
        [Category("Phantom DB Production")]
        public string Phantom_Dev_ProductionMode_Password { get; set; }

        #endregion Phantom Production Mode Database Connection Settings

        [DisplayName("CC To Address")]
        [Description("CC To Address.")]
        [Category("Email")]
        public string CcToAddress { get; set; }

        #endregion Phantom Settings

        // Must override WriteSettings() to write values
        public override void WriteSettings(UserSettingsWriter writer)
        {
            #region Local, Remote, AcroWrap and Printer etc

            writer.Write("Local_Path", Path_Local_Path);
            writer.Write("Local_PDF_Path", Path_Local_PDF_Files);
            

            writer.Write("Remote_Path", Path_Remote_Path);
            writer.Write("AcroWrap_Path", Path_AcroWrap_Path);
            writer.Write("Printer_Temp_Path", Path_Printer_Temp);
            writer.Write("Helpfile_Path", Path_Helpfile_Path);
            writer.Write("SMTP_Host_Name", Path_SMTP_Host);

            //writer.Write("Path_To_Settings", Path_To_Settings);
            writer.Write("Printer_Name", Printer_Name);
            writer.Write("Cc_To_Address", CcToAddress);

            #endregion Local, Remote, AcroWrap and Printer etc

            #region FruPak Test Mode Database Connection Settings

            writer.Write("TestMode_Server", TestMode_Server);
            writer.Write("TestMode_Database", TestMode_Database);
            writer.Write("TestMode_User_Id", TestMode_User_Id);
            writer.WriteEncrypted("TestMode_Password", TestMode_Password);

            #endregion FruPak Test Mode Database Connection Settings

            #region FruPak Production Mode Database Connection Settings

            writer.Write("ProductionMode_Server", ProductionMode_Server);
            writer.Write("ProductionMode_Database", ProductionMode_Database);
            writer.Write("ProductionMode_User_Id", ProductionMode_User_Id);
            writer.WriteEncrypted("ProductionMode_Password", ProductionMode_Password);

            #endregion FruPak Production Mode Database Connection Settings

            #region Phantom Test Mode Database Connection Settings

            writer.Write("Phantom_Dev_TestMode_Server", Phantom_Dev_TestMode_Server);
            writer.Write("Phantom_Dev_TestMode_Database", Phantom_Dev_TestMode_Database);
            writer.Write("Phantom_Dev_TestMode_User_Id", Phantom_Dev_TestMode_User_Id);
            writer.WriteEncrypted("Phantom_Dev_TestMode_Password", Phantom_Dev_TestMode_Password);

            #endregion Phantom Test Mode Database Connection Settings

            #region Phantom Production Mode Database Connection Settings

            writer.Write("Phantom_Dev_ProductionMode_Server", Phantom_Dev_ProductionMode_Server);
            writer.Write("Phantom_Dev_ProductionMode_Database", Phantom_Dev_ProductionMode_Database);
            writer.Write("Phantom_Dev_ProductionMode_User_Id", Phantom_Dev_ProductionMode_User_Id);
            writer.WriteEncrypted("Phantom_Dev_ProductionMode_Password", Phantom_Dev_ProductionMode_Password);

            #endregion Phantom Production Mode Database Connection Settings

            // Booleans
            writer.Write("Phantom_Dev_Mode", Phantom_Dev_Mode);
            writer.Write("Use_FruPak-SQL", Phantom_Dev_Use_FruPak_SQL);
            writer.Write("Copy_At_Startup", CopyAtStartup);

            writer.Write("Copy_Delay_In_Minutes", CopyDelayInMinutes);
            writer.Write("Max_Tree_File_Size_In_Megabytes", MaxTreeFileSizeInMegabytes);

            writer.Write("Update_Address", UpdateAddress);
            writer.Write("Update_Proxy_Username", UpdateProxyUsername);
            writer.Write("Update_Proxy_Password", UpdateProxyPassword);
            writer.Write("Update_Proxy_URI", UpdateProxyURI);
            writer.Write("Update_Proxy_Port", UpdateProxyPort);
            writer.Write("Update_Use_Proxy", UpdateUseProxy);

            writer.Write("Show_Word_Warning", ShowWordWarningMessage);

            

            //writer.Write("Phantom_Dev_EnableResize", Phantom_Dev_EnableResize);
        }

        // Must override ReadSettings() to read values
        public override void ReadSettings(UserSettingsReader reader)
        {
            #region Local, Remote, Path_To_Settings and Printer

            Path_Local_Path = reader.Read("Local_Path", "");
            Path_Local_PDF_Files = reader.Read("Local_PDF_Path", "");

            Path_Remote_Path = reader.Read("Remote_Path", "");
            Path_AcroWrap_Path = reader.Read("AcroWrap_Path", "");
            Path_Printer_Temp = reader.Read("Printer_Temp_Path", "");
            Path_Helpfile_Path = reader.Read("Helpfile_Path", "");
            Path_SMTP_Host = reader.Read("SMTP_Host_Name", "");

            //

            //Path_To_Settings = reader.Read("Path_To_Settings", "");
            Printer_Name = reader.Read("Printer_Name", "");
            CcToAddress = reader.Read("Cc_To_Address", "");

            #endregion Local, Remote, Path_To_Settings and Printer

            #region FruPak Test Mode Database Connection Settings

            TestMode_Server = reader.Read("TestMode_Server", "");
            TestMode_Database = reader.Read("TestMode_Database", "");
            TestMode_User_Id = reader.Read("TestMode_User_Id", "");
            TestMode_Password = reader.ReadEncrypted("TestMode_Password", "");

            #endregion FruPak Test Mode Database Connection Settings

            #region FruPak Production Mode Database Connection Settings

            ProductionMode_Server = reader.Read("ProductionMode_Server", "");
            ProductionMode_Database = reader.Read("ProductionMode_Database", "");
            ProductionMode_User_Id = reader.Read("ProductionMode_User_Id", "");
            ProductionMode_Password = reader.ReadEncrypted("ProductionMode_Password", "");

            #endregion FruPak Production Mode Database Connection Settings

            #region Phantom Test Mode Database Connection Settings

            Phantom_Dev_TestMode_Server = reader.Read("Phantom_Dev_TestMode_Server", "");
            Phantom_Dev_TestMode_Database = reader.Read("Phantom_Dev_TestMode_Database", "");
            Phantom_Dev_TestMode_User_Id = reader.Read("Phantom_Dev_TestMode_User_Id", "");
            Phantom_Dev_TestMode_Password = reader.ReadEncrypted("Phantom_Dev_TestMode_Password", "");

            #endregion Phantom Test Mode Database Connection Settings

            #region Phantom Production Mode Database Connection Settings

            Phantom_Dev_ProductionMode_Server = reader.Read("Phantom_Dev_ProductionMode_Server", "");
            Phantom_Dev_ProductionMode_Database = reader.Read("Phantom_Dev_ProductionMode_Database", "");
            Phantom_Dev_ProductionMode_User_Id = reader.Read("Phantom_Dev_ProductionMode_User_Id", "");
            Phantom_Dev_ProductionMode_Password = reader.ReadEncrypted("Phantom_Dev_ProductionMode_Password", "");

            #endregion Phantom Production Mode Database Connection Settings

            Phantom_Dev_Mode = reader.Read("Phantom_Dev_Mode", Phantom_Dev_Mode);
            Phantom_Dev_Use_FruPak_SQL = reader.Read("Use_FruPak-SQL", Phantom_Dev_Use_FruPak_SQL);
            CopyAtStartup = reader.Read("Copy_At_Startup", CopyAtStartup);
            CopyDelayInMinutes = reader.Read("Copy_Delay_In_Minutes", CopyDelayInMinutes);
            MaxTreeFileSizeInMegabytes = reader.Read("Max_Tree_File_Size_In_Megabytes", MaxTreeFileSizeInMegabytes);

            UpdateAddress = reader.Read("Update_Address", UpdateAddress);
            UpdateProxyUsername = reader.Read("Update_Proxy_Username", UpdateProxyUsername);
            UpdateProxyPassword = reader.Read("Update_Proxy_Password", UpdateProxyPassword);
            UpdateProxyURI = reader.Read("Update_Proxy_URI", UpdateProxyURI);
            UpdateProxyPort = reader.Read("Update_Proxy_Port", UpdateProxyPort);
            UpdateUseProxy = reader.Read("Update_Use_Proxy", UpdateUseProxy);

            ShowWordWarningMessage = reader.Read("Show_Word_Warning", ShowWordWarningMessage);

            //Phantom_Dev_EnableResize = reader.Read("Phantom_Dev_EnableResize", Phantom_Dev_EnableResize);
        }
    }
}