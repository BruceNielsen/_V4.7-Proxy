using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Data;
using System.Xml;
using System.IO;
using System.Drawing;
using System.Drawing.Printing;
using NLog;
using FruPak.PF.CustomSettings;
using System.Windows.Forms;

namespace FruPak.PF.PrintLayer
{
    public class PDF_Print
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        // Declare application settings object
        //private static PhantomCustomSettings Settings;

        public static void print(string report, string File_Printer_txt)
        {
            //Declare and instantiate a new process component.
            try
            {
                            // Initialize settings
                PhantomCustomSettings Settings = new PhantomCustomSettings();

                ProcessStartInfo startInfo = new ProcessStartInfo();

                // Original string with typo:  @"\\FruPak=SBS\PublicDocs\FruPak\Client\acrowrap.exe" + @" ";
                //startInfo.FileName = @"\\FruPak-SBS\PublicDocs\FruPak\Client\acrowrap.exe" + @" ";

                #region Load CustomSettings
                #region Get "Application.StartupPath" folder
                string path = Application.StartupPath;
                #endregion

                // Initialize settings
                Settings = new PhantomCustomSettings();
                Settings.SettingsPath = Path.Combine(path, "FruPak.Phantom.config");
                Settings.EncryptionKey = "phantomKey";

                if (!File.Exists(Settings.SettingsPath))
                {
                    Settings.Save();
                    logger.Log(LogLevel.Info, LogCodeStatic("PDF_Print: Default Phantom Settings file created"));
                }
                // Load settings - Normally in Form_Load
                Settings.Load();

                logger.Log(LogLevel.Info, LogCodeStatic("Settings file opened."));

                #endregion
                Settings.Load();

                string folderPath = Settings.Path_AcroWrap_Path;
                startInfo.FileName = folderPath + "\\acrowrap.exe" + @" ";
                startInfo.Arguments = @"/t """ + report + @""" """ + File_Printer_txt + @"";

                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;

                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                Process p = Process.Start(startInfo);


                //Wait for the window to finish loading.
                p.WaitForInputIdle();

                //Wait for the process to end.
                p.WaitForExit();
            }
            catch (Exception ex)
            {
                //string log_file = @"\\FruPak-SBS\PublicDocs\FruPak\Client\Logs\PDFPrint.txt";
                PhantomCustomSettings Settings = new PhantomCustomSettings();
                #region Load CustomSettings
                #region Get "Application.StartupPath" folder
                string path = Application.StartupPath;
                #endregion

                // Initialize settings
                Settings = new PhantomCustomSettings();
                Settings.SettingsPath = Path.Combine(path, "FruPak.Phantom.config");
                Settings.EncryptionKey = "phantomKey";

                if (!File.Exists(Settings.SettingsPath))
                {
                    Settings.Save();
                    logger.Log(LogLevel.Info, LogCodeStatic("PDF_Print: Default Phantom Settings file created"));
                }
                // Load settings - Normally in Form_Load
                Settings.Load();

                logger.Log(LogLevel.Info, LogCodeStatic("Settings file opened."));

                #endregion
                Settings.Load();

                string folderPath = Settings.Path_AcroWrap_Path;
                string log_file = folderPath + "\\PDFPrint.txt"; // Took out the Logs folder 12-01-2015 BN (The log file is pointless anyway)
                General.Write_Log(log_file, ex.Message);
                logger.Log(LogLevel.Debug, ex.Message);
            }
        }

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
        #endregion

    }
}
