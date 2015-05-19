using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NetOffice;
using _Word = NetOffice.WordApi;
using NetOffice.WordApi.Enums;
using NLog;

using System.Reflection;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FruPak.PF.CustomSettings;

namespace FruPak.PF.PrintLayer
{
    public class Word
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        // Phantom 17/12/2014 Declare application settings object
        private static PhantomCustomSettings Settings;

        private static string _templatePath;
        private static _Word.Application wordApplication;
        private static _Word.Document newDocument;
        public static string TemplatePath
        {
            get;
            set;
        }
        public static string TemplateName
        {
            get;
            set;
        }
        public static string FilePath
        {
            get;
            set;
        }
        public static string FileName
        {
            get;
            set;
        }
        public static string fileExt
        {
            get;
            set;
        }
        public static string Printer
        {
            get;
            set;
        }
        public static string BarcodePath
        {
            get;
            set;
        }
        public static string BarcodeName
        {
            get;
            set;
        }
        public static string BarcodeFull
        {
            get;
            set;
        }
        public static void Server_Print( string Data, int int_Current_User_Id)
        {
            FruPak.PF.Data.AccessLayer.SY_PrintRequest.Insert(FruPak.PF.Common.Code.General.int_max_user_id("SY_PrintRequest"), Printer, TemplateName, FileName, Data, int_Current_User_Id);
        }
        public static bool Test_for_Word()
        {
            bool bol_Word = false;
            try
            {
                // System.IO.FileNotFoundException
                wordApplication = new _Word.Application();
                logger.Log(LogLevel.Info, LogCodeStatic("Word Version: " + wordApplication.Version + " is installed."));
                bol_Word = true;
                CloseWord();
            }
            catch
            {
                bol_Word = false;
            }
            return bol_Word;
        }
        public static void StartWord()
        {           
            // start word and turn off msg boxes
            try
            {
                wordApplication = new _Word.Application();
                wordApplication.DisplayAlerts = WdAlertLevel.wdAlertsNone;
                _templatePath = @TemplatePath + "\\" + TemplateName + GetTemplateExtension(wordApplication);
                fileExt = GetDefaultExtension(wordApplication);

                OpenTemplate();
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, "StartWord: " + ex.Message + " - " + ex.StackTrace);
                //MessageBox.Show("StartWord: " + ex.Message + " - " + ex.StackTrace, "StartWord", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                // for future reference BN
                
                // close word and dispose reference 
                //wordApplication.Quit();
                //wordApplication.Dispose();
            }
        }
        public static void StartWord_No_Template()
        {
            try
            {
                
            // start word and turn off msg boxes
            wordApplication = new _Word.Application();
            wordApplication.DisplayAlerts = WdAlertLevel.wdAlertsNone;
            _templatePath = @TemplatePath + "\\" + TemplateName + GetTemplateExtension(wordApplication);
            fileExt = GetDefaultExtension(wordApplication);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, "StartWord_No_Template: " + ex.Message + " - " + ex.StackTrace);
                //MessageBox.Show("StartWord_No_Template: " + ex.Message + " - " + ex.StackTrace, "StartWord_No_Template", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }

        }
        public static void OpenTemplate()
        {
            try
            {
                // add a new document
                newDocument = wordApplication.Documents.Add(Path.GetFullPath(_templatePath));
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message + " - " + ex.StackTrace);
                //MessageBox.Show("OpenTemplate: newDocument is null.", "OpenTemplate", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        public static void OpenDoc()
        {
            object missing = System.Reflection.Missing.Value;
            object readOnly = false;
            object isVisible = true;

 
            try
            {
                // add a new document
                newDocument = wordApplication.Documents.Open(@FilePath + "\\" + FileName + GetDefaultExtension(wordApplication), missing, missing, missing, missing, missing, missing, missing, missing, missing, 
                    missing, isVisible,missing);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message + " - " + ex.StackTrace);
                //MessageBox.Show("OpenDoc: newDocument is null.", "OpenDoc", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        public static void OpenDoc_to_View()
        {
            object missing = System.Reflection.Missing.Value;
            object readOnly = true;
            object isVisible = false;

            try
            {
                // add a new document
                newDocument = wordApplication.Documents.Open(@FilePath + "\\" + FileName + GetDefaultExtension(wordApplication), missing, readOnly, missing, missing, missing, missing, missing, missing, missing,
                    missing, isVisible, missing);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message + " - " + ex.StackTrace);
                //MessageBox.Show("OpenDoc_to_View: newDocument is null.", "OpenDoc_to_View", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        public static void Merge(List<string> filesToMerge)
        {
            // Make a Word selection object.
            _Word.Selection selection = wordApplication.Selection;
            

            // Loop through each of the Word documents
            foreach (string file in filesToMerge)
            {
                // Insert the files to our template
               // selection.InsertNewPage();
                selection.InsertFile(file);
                
            }

        }

        public static void ReplaceText(string Bookmark, string NewText)
        {
            if (newDocument != null)
            {
                if (newDocument.Bookmarks.Exists(Bookmark))
                {
                    newDocument.Bookmarks[Bookmark].Range.Text = NewText;
                }
            }
            else
            {
                logger.Log(LogLevel.Info, "ReplaceText: newDocument is null.");
                //MessageBox.Show("ReplaceText: newDocument is null.", "ReplaceText", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        public static void ChangeFontSize(string Bookmark, float font_size)
        {
            _Word.Range range = newDocument.Bookmarks[Bookmark].Range;

            range.Font.Size = font_size;
            newDocument.Bookmarks[Bookmark].Range.Font.Size = font_size;
        }
        public static void Add_Barcode(string Bookmark)
        {
            if (newDocument != null)
            {
                string text = newDocument.Content.Text;

                if (newDocument.Bookmarks.Exists(Bookmark))
                {
                    object saveWithDocument = true;
                    object missing = Type.Missing;

                    Object name = Bookmark;
                    _Word.Range range = newDocument.Bookmarks[Bookmark].Range;

                    if (BarcodeFull == "" || BarcodeFull == null)
                    {
                        BarcodeFull = @BarcodePath + "\\" + BarcodeName;
                    }
                    newDocument.InlineShapes.AddPicture(BarcodeFull, missing, saveWithDocument, range);
                }
            }
            else
            {
                logger.Log(LogLevel.Info, "Add_Barcode: newDocument is null.");
                //MessageBox.Show("Add_Barcode: newDocument is null", "Add_Barcode", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        public static int SaveAS()
        {
            int return_code = 0;
            try
            {
                newDocument.SaveAs(@FilePath + "\\" + FileName + GetDefaultExtension(wordApplication));
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message + " - " + ex.StackTrace);
                return_code = 9;
                //MessageBox.Show("SaveAS: newDocument is null", "SaveAS", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            return return_code;
        }
        /// <summary>
        /// returns the valid file extension for the instance. for example ".doc" or ".docx"
        /// </summary>
        /// <param name="application">the instance</param>
        /// <returns>the extension</returns>
        private static string GetDefaultExtension(_Word.Application application)
        {
            double version = Convert.ToDouble(application.Version, CultureInfo.InvariantCulture);
            //if (version >= 12.00)
            if (version > 11.0)
                return ".docx";
            else
                return ".doc";
        }
        private static string GetTemplateExtension(_Word.Application application)
        {
            double version = Convert.ToDouble(application.Version, CultureInfo.InvariantCulture);
            //System.Windows.Forms.MessageBox.Show(Convert.ToString(version));
            //if (version >= 12.00)
            if (version > 11.0)
            {
                
                return ".dotx";
            }
            else
                return ".dot";
        }
        public static int Print()
        {
            if (wordApplication != null)
            {
                int return_code = 0;
                string default_printer = "";
                //store current default printer - "Send To OneNote 2013"

                // Changed to use the Phantom Settings class - The default behaviour prior to this was to use
                // the default installed printer on the system. This gives more flexibility, but probably breaks
                // MS Guidelines BN 2/02/2015

                // Initialize settings
                //#region Get "Application.StartupPath" folder
                string path = Application.StartupPath;
                //#endregion


                Settings = new PhantomCustomSettings();
                Settings.SettingsPath = Path.Combine(path, "FruPak.Phantom.config");
                Settings.EncryptionKey = "phantomKey";

                //if (!File.Exists(Settings.SettingsPath))
                //{
                //    Settings.Save();
                //    //logger.Log(LogLevel.Info, LogCode("Default Phantom Settings file created"));
                //}
                //// Load settings - Normally in Form_Load
                Settings.Load();
                default_printer = Settings.Printer_Name;

                // Reverted to the original, as it was causing problems on-site BN 4/02/2015
                //default_printer = wordApplication.ActivePrinter;

                // This code has been in place since day 1.
                //try
                //{
                //    // Is this it????
                //    //change printer
                //    wordApplication.ActivePrinter = Printer;
                //}
                //catch (Exception ex)
                //{
                //    logger.Log(LogLevel.Debug, ex.Message + " - " + ex.StackTrace);
                //}


                double version = Convert.ToDouble(wordApplication.Version, CultureInfo.InvariantCulture);
                object oBackground = false;

                FileInfo wordFile = new FileInfo(@FilePath + "\\" + FileName + GetDefaultExtension(wordApplication));
                object fileObject = wordFile.FullName;
                object outputfile = @FilePath + "\\" + FileName;

                object oMissing = System.Reflection.Missing.Value;
                object oTrue = true;

                logger.Log(LogLevel.Info, LogCodeStatic("FruPak.PF.PrintLayer.Word: Word version = " + version.ToString()));

                //Word 2007
                if (version == 12.0)
                {
                    // Found, Noted, but for now, do not touch - 12-01-2015 BN
                    // Hang on, isn't that path illegal anyway? Network paths ALWAYS have to be double quoted(backslashed) to be valid. ??? - 12-01-2015 BN
                    // Unless they are verbatim, which means that if you start with an at (@) symbol, the rest will be treated as a literal path:
                    // @"c:\test\test.txt"; is equal to "c:\\test\\test.txt';

                    //trial for 2007 start
                    FilePath = @"\\FruPak-SBS\PublicDocs\\FruPak\Client\Printing\Saved";
                    FileName = "Dave1";
                    return_code = FruPak.PF.PrintLayer.Word.SaveAS();


                    Microsoft.Office.Interop.Word.Application wordInstance = new Microsoft.Office.Interop.Word.Application();



                    Microsoft.Office.Interop.Word.Document doc = wordInstance.Documents.Open(ref fileObject, ref oMissing, ref oTrue, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                    doc.Activate();
                    doc.PrintOut(oBackground, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);


                    //end
                }
                else
                {
                    try
                    {
                        // old code that worked for Robin, Glenys, and Dave's machines
                        wordApplication.PrintOut(oBackground);
                    }
                    catch (Exception ex)
                    {
                        logger.Log(LogLevel.Debug, ex.Message + " - " + ex.StackTrace);
                        return_code = 9;
                    }

                }

                //reset Default Printer
                wordApplication.ActivePrinter = default_printer;

                return return_code;
            }
            else
            {
                logger.Log(LogLevel.Debug, "Print: wordApplication is null.");
                //MessageBox.Show("Print: wordApplication is null.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //return_code = 0;

                return 0;
            }
        }
        public static void CloseWord()
        {
            try
            {
                // close word and dispose reference
                object ofalse = false;
                wordApplication.Quit(ofalse);
                wordApplication.Dispose();
                
                // Com crash
                // The object's type must be __ComObject or derived from __ComObject.
                // Parameter name: o

                if (wordApplication != null)
                {
                    //System.Runtime.InteropServices.Marshal.FinalReleaseComObject(wordApplication);
                    wordApplication = null;
                }

            }
                catch(COMException comex)
            {
                // This error is not worthy of entering into the log file BN 2/02/2015
                //logger.Log(LogLevel.Debug, "COMException" + comex.Message);
                Console.WriteLine("COMException" + comex.Message);
            }
            catch (Exception ex)
            {
                // This error is not worthy of entering into the log file BN 2/02/2015
                //logger.Log(LogLevel.Debug, "This error message can be safely ignored: " + ex.Message);
                Console.WriteLine("CloseWord(): This error message can be safely ignored: " + ex.Message + " - " + ex.StackTrace);
            }
        }
        public static int SaveAsPdf()
        {
            int return_code = 0;

            object missing = System.Reflection.Missing.Value;

            string paramExportFilePath = @FilePath + @"\" + FileName;
            
            WdExportFormat paramExportFormat = WdExportFormat.wdExportFormatPDF;
            bool paramOpenAfterExport = false;
            WdExportOptimizeFor paramExportOptimizeFor = WdExportOptimizeFor.wdExportOptimizeForPrint;
            WdExportRange paramExportRange = WdExportRange.wdExportAllDocument;
            int paramStartPage = 0;
            int paramEndPage = 0;
            WdExportItem paramExportItem = WdExportItem.wdExportDocumentContent;
            bool paramIncludeDocProps = true;
            bool paramKeepIRM = true;
            WdExportCreateBookmarks paramCreateBookmarks = WdExportCreateBookmarks.wdExportCreateWordBookmarks;
            bool paramDocStructureTags = true;
            bool paramBitmapMissingFonts = true;
            bool paramUseISO19005_1 = false;

            // Glenys hits this 3 times:
            // First time is the correct invoice with a pdf extension
            // Second is the COA with no extension
            // Third is the COA again, also with no extension
            try
            {
                Console.WriteLine("SaveAsPdf: ExportFilePath: " + paramExportFilePath);
                logger.Log(LogLevel.Info, "---------------------------------------------");
                logger.Log(LogLevel.Info, "SaveAsPdf: ExportFilePath: " + paramExportFilePath);
                logger.Log(LogLevel.Info, "About to call newDocument.ExportAsFixedFormat...");
                logger.Log(LogLevel.Info, "---------------------------------------------");
                logger.Log(LogLevel.Info, "paramExportFilePath: " + paramExportFilePath);
                logger.Log(LogLevel.Info, "paramExportFormat: " + paramExportFormat.ToString());
                logger.Log(LogLevel.Info, "paramOpenAfterExport: " + paramOpenAfterExport);
                logger.Log(LogLevel.Info, "paramExportOptimizeFor: " + paramExportOptimizeFor.ToString());
                logger.Log(LogLevel.Info, "paramExportRange: " + paramExportRange.ToString());
                logger.Log(LogLevel.Info, "paramStartPage: " + paramStartPage);
                logger.Log(LogLevel.Info, "paramEndPage: " + paramEndPage);
                logger.Log(LogLevel.Info, "paramExportItem: " + paramExportItem.ToString());
                logger.Log(LogLevel.Info, "paramIncludeDocProps: " + paramIncludeDocProps);
                logger.Log(LogLevel.Info, "paramKeepIRM: " + paramKeepIRM);
                logger.Log(LogLevel.Info, "paramCreateBookmarks: " + paramCreateBookmarks.ToString());
                logger.Log(LogLevel.Info, "paramDocStructureTags: " + paramDocStructureTags);
                logger.Log(LogLevel.Info, "paramBitmapMissingFonts: " + paramBitmapMissingFonts);
                logger.Log(LogLevel.Info, "paramUseISO19005_1: " + paramUseISO19005_1);
                logger.Log(LogLevel.Info, "missing: " + missing.ToString());
                logger.Log(LogLevel.Info, "---------------------------------------------");

                newDocument.ExportAsFixedFormat(
                    paramExportFilePath,
                    paramExportFormat, 
                    paramOpenAfterExport,
                    paramExportOptimizeFor, 
                    paramExportRange, 
                    paramStartPage,
                    paramEndPage, 
                    paramExportItem, 
                    paramIncludeDocProps,
                    paramKeepIRM, 
                    paramCreateBookmarks, 
                    paramDocStructureTags,
                    paramBitmapMissingFonts, 
                    paramUseISO19005_1,
                    missing);
            }
            catch (Exception ex)
            {
                newDocument.Close();

                Console.WriteLine("SaveAsPdf: ExportFilePath: " + paramExportFilePath);
                logger.Log(LogLevel.Debug, "SaveAsPdf: ExportFilePath: " + paramExportFilePath);
                logger.Log(LogLevel.Debug, "Message: " + ex.Message + ", StackTrace: " + ex.StackTrace);
                return_code = 9;
                System.Windows.Forms.MessageBox.Show(ex.Message + " - " + ex.StackTrace);
            }

            return return_code;
        }
        public static int SaveASXPS()
        {
            int return_code = 0;

            object missing = System.Reflection.Missing.Value;

            string paramExportFilePath = @FilePath + "\\" + FileName;
            WdExportFormat paramExportFormat = WdExportFormat.wdExportFormatXPS;
            bool paramOpenAfterExport = false;
            WdExportOptimizeFor paramExportOptimizeFor = WdExportOptimizeFor.wdExportOptimizeForPrint;
            WdExportRange paramExportRange = WdExportRange.wdExportAllDocument;
            int paramStartPage = 0;
            int paramEndPage = 0;
            WdExportItem paramExportItem = WdExportItem.wdExportDocumentContent;
            bool paramIncludeDocProps = true;
            bool paramKeepIRM = true;
            WdExportCreateBookmarks paramCreateBookmarks = WdExportCreateBookmarks.wdExportCreateWordBookmarks;
            bool paramDocStructureTags = true;
            bool paramBitmapMissingFonts = true;
            bool paramUseISO19005_1 = false;

            try
            {
                Console.WriteLine("SaveAsXPS: ExportFilePath: " + paramExportFilePath);
                logger.Log(LogLevel.Info, "SaveAsXPS: ExportFilePath: " + paramExportFilePath);

                newDocument.ExportAsFixedFormat(paramExportFilePath,
                    paramExportFormat, paramOpenAfterExport,
                    paramExportOptimizeFor, paramExportRange, paramStartPage,
                    paramEndPage, paramExportItem, paramIncludeDocProps,
                    paramKeepIRM, paramCreateBookmarks, paramDocStructureTags,
                    paramBitmapMissingFonts, paramUseISO19005_1,
                    missing);
            }
            catch (Exception ex)
            {
                Console.WriteLine("SaveAsXPS: ExportFilePath: " + paramExportFilePath);
                logger.Log(LogLevel.Debug, "SaveAsXPS: ExportFilePath: " + paramExportFilePath);
                return_code = 9;
                logger.Log(LogLevel.Debug, "Message: " + ex.Message + ", StackTrace: " + ex.StackTrace);
            }

            return return_code;
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