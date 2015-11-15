using FruPak.PF.CustomSettings;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace FruPak.ProcessFactory
{
    /// <summary>
    /// Install form which gets called at startup
    /// </summary>
    public partial class Install : Form
    {
        // Declare application settings object
        private static PhantomCustomSettings Settings;

        private static Logger logger = LogManager.GetCurrentClassLogger();

        private string local_path = string.Empty;
        private string remote_path = string.Empty;
        private bool copyAtStartup = false;

        /// <summary>
        /// How much time must elapse before copying at startup is initiated
        /// </summary>
        public static double CopyDelayInMinutes;

        /// <summary>
        /// Path to the Acrobat Wrapper exe
        /// </summary>
        public static string AcroWrap_path = string.Empty;

        //private string path_to_settings = string.Empty;

        /// <summary>
        /// Constructor
        /// </summary>
        public Install()
        {
            InitializeComponent();

            //logger.Log(LogLevel.Info, LogCode("Install.cs: Form Loading."));

            //#region Load CustomSettings

            //#region Get "My Documents" folder

            //string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //path = Path.Combine(path, "FruPak_Settings");

            //// Create folder if it doesn't already exist
            //if (!Directory.Exists(path))
            //    Directory.CreateDirectory(path);

            //#endregion Get "My Documents" folder

            //// Initialize settings
            //Settings = new PhantomCustomSettings();
            //Settings.SettingsPath = Path.Combine(path, "FruPak.Phantom.config");
            //Settings.EncryptionKey = "phantomKey";

            //if (!File.Exists(Settings.SettingsPath))
            //{
            //    Settings.Save();
            //    logger.Log(LogLevel.Info, LogCode("Install.cs: Default settings file created"));
            //}
            //// Load settings - Normally in Form_Load
            //Settings.Load();
            //local_path = Settings.Path_Local_Path;
            //remote_path = Settings.Path_Remote_Path;
            //AcroWrap_path = Settings.Path_AcroWrap_Path; // Don't actually need this here'
            //copyAtStartup = Settings.CopyAtStartup;
            //CopyDelayInMinutes = Settings.CopyDelayInMinutes;

            //// These are accessible from the Logon form, so I don't need to reopen the settings file there
            //// Was getting screwed up as the Main_Menu form is actually opened before the logon form,
            //// but stays hidden.

            ////FruPak.PF.Global.Global.Phantom_Dev_Mode = Settings.Phantom_Dev_Mode;

            ////FruPak.PF.Global.Global.Phantom_Dev_Test = Settings.Phantom_Dev_Test_Mode;
            ////FruPak.PF.Global.Global.Phantom_Dev_EnableResize = Settings.Phantom_Dev_EnableResize;

            //logger.Log(LogLevel.Info, LogCode("Install.cs: Settings file opened."));

            ////path_to_settings = Settings.Path_To_Settings;

            //// Save settings - Normally in Form_Closing
            ////Settings.Save();

            //#endregion Load CustomSettings
        }

        private string current_filename
        {
            get;
            set;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (copyAtStartup == true)
            {
                DirectoryInfo d_local = new DirectoryInfo(local_path);
                DirectoryInfo d_remote = new DirectoryInfo(remote_path);

                if (!Directory.Exists(local_path))
                {
                    logger.Log(LogLevel.Trace, "Local path does not exist: " + local_path + ", Creating it.");
                    Directory.CreateDirectory(local_path);
                }
                FileInfo[] Files_local = d_local.GetFiles("*", SearchOption.AllDirectories); //Getting Text files
                FileInfo[] Files_remote = d_remote.GetFiles("*", SearchOption.AllDirectories);

                int remoteFilesCount = Files_remote.Length;
                logger.Log(LogLevel.Info, LogCode("There are " + remoteFilesCount.ToString() + " files to be copied from " + remote_path + " to " + local_path + "."));

                int i = 0;

                foreach (FileInfo R_file in Files_remote)
                {
                    i++;
                    decimal percent = Convert.ToDecimal(i) / Convert.ToDecimal(Files_remote.Length) * 100;

                    current_filename = "Copying " + R_file.FullName.ToString() + " to C" + R_file.FullName.Substring(R_file.FullName.IndexOf(':'));
                    logger.Log(LogLevel.Trace, current_filename);

                    Thread.Sleep(100);
                    backgroundWorker1.ReportProgress(Convert.ToInt32(percent));

                    string path = "";
                    path = "C" + R_file.DirectoryName.ToString().Substring(R_file.DirectoryName.ToString().IndexOf(':'));

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                        logger.Log(LogLevel.Trace, "Remote path does not exist: " + path + ", Creating it.");
                    }

                    try
                    {
                        File.Copy(R_file.FullName, "C" + R_file.FullName.Substring(R_file.FullName.IndexOf(':')), true);

                        // This log call creates unnecessary noise in the csv log file
                        //logger.Log(LogLevel.Trace, "File.Copy: " + R_file.FullName, "C" + R_file.FullName.Substring(R_file.FullName.IndexOf(':')));
                    }
                    catch (Exception ex)
                    {
                        // This Exception is caused by a crash leaving WinWord running hidden in the background
                        // Use task manager to find and kill off the process.
                        // ------------------------------------------------------------- Phantom 16/12/2014
                        // "System.IO.IOException: The process cannot access the file
                        // 'C:\FruPak\Client\Printing\Templates\PF-BinCard.dotx' because it is being used by another process.
                        logger.Log(LogLevel.Trace, "Exception - probable cause is WinWord running in the background.");

                        logger.Log(LogLevel.Debug, ex.Message);
                    }
                }
            }
            else
            {
                logger.Log(LogLevel.Info, LogCode("Skipped file copy at startup."));
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Change the value of the ProgressBar to the BackgroundWorker progress.
            // progressBar1.Value = e.ProgressPercentage;
            // Set the text.
            this.Text = "File Checking has Completed " + e.ProgressPercentage.ToString() + "%";

            if (e.ProgressPercentage < 100)
            {
                lbl_current_File.Text = current_filename;
            }
            else
            {
                lbl_current_File.Text = "Copy Complete.";
            }
            this.progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //logger.Log(LogLevel.Trace, "backgroundWorker1_RunWorkerCompleted.");

            string MAC_Address = "";
            int Install_id = 0;
            MAC_Address = get_MAC();

            try
            {
                //logger.Log(LogLevel.Trace, "Attempting to: Get_Install_Id, " + MAC_Address);

                Install_id = Get_Install_Id(MAC_Address);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message);
                //logger.Log(LogLevel.Fatal, "Unable to recover after the BackgroundWorker has completed. Exiting Application.");
                //Application.Exit();
            }

            //insert
            if (Install_id == 0)
            {
                logger.Log(LogLevel.Trace, "Install_id == 0 (Insert)");

                FruPak.PF.Data.AccessLayer.SC_Install.Insert(FruPak.PF.Common.Code.General.int_max_user_id("SC_Install"), MAC_Address);
            }
            //Update
            else
            {
                logger.Log(LogLevel.Trace, "Install_id != 0 (Update), Install ID is: " + Install_id.ToString() + ", MAC: " + MAC_Address);

                FruPak.PF.Data.AccessLayer.SC_Install.Update(Install_id, MAC_Address);
            }
            this.Close();
        }

        private void Install_Load(object sender, EventArgs e)
        {
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.RunWorkerAsync();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Get Install ID
        /// <para>
        /// This is where fatal crashes/hangs happen if there's a MAC address problem.
        /// MAC addresses are stored in the database table SC_Install.
        ///
        /// For testing I have added in this laptop's Wired and Wireless addresses,
        /// but the app is actually only using the wireless address.
        /// Get_MAC: 38:B1:DB:B6:39:0B
        /// </para>
        /// </summary>
        /// <param name="MAC">MAC address as a string</param>
        /// <returns>Integer (Matches an entry in the database)</returns>
        public int Get_Install_Id(string MAC)
        {
            if (MAC == string.Empty || MAC == null)
            {
                logger.Log(LogLevel.Warn, "MAC address is empty, or no matching record in SC_Install table. Do you have a working network connection?");

                MessageBox.Show("MAC address is empty, or no matching record in SC_Install table.\r\nDo you have a working network connection?", "Warning: MAC Address is empty", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int int_install_id = 0;
                DataSet ds = FruPak.PF.Data.AccessLayer.SC_Install.Get_MAC(MAC);
                DataRow dr;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dr = ds.Tables[0].Rows[i];
                    int_install_id = Convert.ToInt32(dr["Install_Id"].ToString());
                }
                //logger.Log(LogLevel.Info, LogCode("Get_Install_Id: MAC: " + MAC + ", int_install_id: " + int_install_id.ToString()));
                return int_install_id;
            }
            return -1;  // Network has failed - BN 6-8-2015
        }

        /// <summary>
        /// Gets the date and time the application was last executed
        /// </summary>
        /// <param name="MAC">The executing computers MAC address</param>
        /// <returns>A DateTime value</returns>
        public static DateTime Get_Last_execution(string MAC)
        {
            DateTime dt = new DateTime();
            DataSet ds = FruPak.PF.Data.AccessLayer.SC_Install.Get_MAC(MAC);
            DataRow dr;
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dr = ds.Tables[0].Rows[i];
                    dt = Convert.ToDateTime(dr["last_update_date"].ToString());
                }
                //logger.Log(LogLevel.Info, LogCodeStatic("Get_Last_execution: MAC: " + MAC + ", last_update_date: " + dt.ToString()));
                return dt;
            }
            else
            {
                logger.Log(LogLevel.Warn, "Install.cs: Crash avoided; DataSet was null");
                DateTime dtNow = DateTime.Now;  // Just return Now

                return dtNow;
            }
        }

        /// <summary>
        /// Gets the MAC address of the computer running the application
        /// </summary>
        /// <returns>A MAC address</returns>
        /// <example>
        /// <code>Am testing a code tag buried in an outer enclosing example tag AFTER the closing summary tag</code>
        /// </example>
        public static string get_MAC()
        {
            System.Management.ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration where IPEnabled=true");
            IEnumerable<System.Management.ManagementObject> objects = searcher.Get().Cast<System.Management.ManagementObject>();
            string mac = (from o in objects orderby o["IPConnectionMetric"] select o["MACAddress"].ToString()).FirstOrDefault();

            logger.Log(LogLevel.Trace, "get_MAC: " + mac);

            return mac;
        }

        private void Install_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Save();
            Settings = null;
            //logger.Log(LogLevel.Info, LogCode("Install.cs: Install_FormClosing: Settings file saved."));
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

        #region Log Code Static

        /// <summary>
        /// Formats the output to the debug log so that the actual log contents stand out amongst the machine-generated stuff.
        /// Refers to machine-generated content, not calling specifics.
        /// </summary>
        /// <param name="input">The code function that we want to log.</param>
        /// <returns>A formatted (Bracketed) string</returns>
        public static string LogCodeStatic(string input)
        {
            string Output = "___[ " + input + " ]___";
            return Output;
        }

        #endregion Log Code Static
    }
}