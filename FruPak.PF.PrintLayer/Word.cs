using PF.CustomSettings;
using NetOffice.WordApi.Enums;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using _Word = NetOffice.WordApi;

namespace PF.PrintLayer
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

		
		public static void Server_Print(string Data, int int_Current_User_Id)
		{
			PF.Data.AccessLayer.SY_PrintRequest.Insert(PF.Common.Code.General.int_max_user_id("SY_PrintRequest"), Printer, TemplateName, FileName, Data, int_Current_User_Id);
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
				Common.Code.DebugStacktrace.StackTrace(ex);

				//logger.Log(LogLevel.Debug, "StartWord: " + ex.Message + " - " + ex.StackTrace);
				//MessageBox.Show("StartWord: " + ex.Message + " - " + ex.StackTrace, "StartWord", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
				Common.Code.DebugStacktrace.StackTrace(ex);

				//logger.Log(LogLevel.Debug, "StartWord_No_Template: " + ex.Message + " - " + ex.StackTrace);
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
				Common.Code.DebugStacktrace.StackTrace(ex);

				//logger.Log(LogLevel.Debug, ex.Message + " - " + ex.StackTrace);
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
					missing, isVisible, missing);
			}
			catch (Exception ex)
			{
				Common.Code.DebugStacktrace.StackTrace(ex);

				//{"Sorry, we couldn’t find your file. Is it possible it was moved, renamed or deleted?\r
				// (C:\\FP\\...\\Temp\\COA2015000240-1.docx)"}
				// --------------------------------------------------------------------------------------
				// This exception is being caused by my using a virtual pdf printer for testing, which
				// saves the documents with a completely different filename into a different location.
				// A second exception is caused as a result of this:
				// Attempt has been made to use a COM object that does not have a backing class factory.
				if (ex.InnerException.InnerException != null)
				{
					logger.Log(LogLevel.Debug, "Exception: " + ex.InnerException.InnerException.Message);
				}
				else
				{
					logger.Log(LogLevel.Debug, "Exception: " + ex.Message);
					//MessageBox.Show("OpenDoc: newDocument is null.", "OpenDoc", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				}
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
				Common.Code.DebugStacktrace.StackTrace(ex);

				//logger.Log(LogLevel.Debug, ex.Message + " - " + ex.StackTrace);
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
					//newDocument.Bookmarks[Bookmark].Range.Text = "";    // Fix for subtle bug BN 13/10/2015
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
				Common.Code.DebugStacktrace.StackTrace(ex);

				//logger.Log(LogLevel.Debug, ex.Message + " - " + ex.StackTrace);
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
			try
			{
				if (wordApplication != null)
				{
					int return_code = 0;
					string default_printer = "";


					//store current default printer - "Send To OneNote 2013"

					// Changed to use the Phantom Settings class - The default behaviour prior to this was to use
					// the default installed printer on the system. BN 2/02/2015


					#region Initialize settings

					string path = Application.StartupPath;
					Settings = new PhantomCustomSettings();
					Settings.SettingsPath = Path.Combine(path, "FP.Phantom.config");
					Settings.EncryptionKey = "phantomKey";
					Settings.Load();
					default_printer = Settings.Printer_Name;
					wordApplication.ActivePrinter = default_printer;

					#endregion Initialize settings 
 
					#region Get word version and filename

					double version = Convert.ToDouble(wordApplication.Version, CultureInfo.InvariantCulture);
					object oBackground = false;

					FileInfo wordFile = new FileInfo(@FilePath + "\\" + FileName + GetDefaultExtension(wordApplication));
					object fileObject = wordFile.FullName;
					object outputfile = @FilePath + "\\" + FileName;

					object oMissing = System.Reflection.Missing.Value;
					object oTrue = true;

					Console.WriteLine("PF.PrintLayer.Word: Word version = " + version.ToString());
					logger.Log(LogLevel.Info, LogCodeStatic("PF.PrintLayer.Word: Word version = " + version.ToString()));

					#endregion Get word version and filename

					#region Word 2007
					if (version == 12.0)
					{
						//trial for 2007 start
						// Note from 2015 - this never gets called
						FilePath = @"\\FP-SBS\PublicDocs\\FP\Client\Printing\Saved";
						FileName = "Dave1";
						return_code = PF.PrintLayer.Word.SaveAS();

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

							// NEW NOTE 7/10/2015 This is where it happens - the 2007 code never gets called - We are all on version 15 (Office 2013)

							// This works every single time without failing or missing out pages
							// Background - Set to True to have the macro continue while Microsoft Word prints the document.
							wordApplication.PrintOut(oBackground);
						}
						catch (Exception ex)
						{
							Common.Code.DebugStacktrace.StackTrace(ex);

							if (ex.InnerException.InnerException != null)
							{
								Console.WriteLine("Inner Exception: " + ex.InnerException.InnerException.Message);
								logger.Log(LogLevel.Debug, "Inner Exception: " + ex.InnerException.InnerException.Message);
								if (ex.InnerException.InnerException != null)
								{
									Console.WriteLine("Inner Inner Exception: " + ex.InnerException.InnerException.Message);
									logger.Log(LogLevel.Debug, "Inner Inner Exception: " + ex.InnerException.InnerException.Message);
								}
							}
							Console.WriteLine("Exception: " + ex.Message);
							logger.Log(LogLevel.Debug, "Exception: " + ex.Message + " - " + ex.StackTrace);

							//logger.Log(LogLevel.Debug, ex.Message + " - " + ex.StackTrace);
							return_code = 9;
						}
					}

					#endregion Word 2007

					#region Reset default printer for some reason

					try
					{
						//reset Default Printer
						//wordApplication.ActivePrinter = default_printer;
						Console.WriteLine("wordApplication.ActivePrinter: " + wordApplication.ActivePrinter);
					}
					catch (Exception ex)
					{
						Common.Code.DebugStacktrace.StackTrace(ex);

						if (ex.InnerException != null)
						{
							Console.WriteLine("Inner Exception: " + ex.InnerException.Message.ToString());
							if (ex.InnerException.InnerException != null)
							{
								Console.WriteLine("Inner Inner Exception: " + ex.InnerException.InnerException.Message.ToString());
							}
						}
						Console.WriteLine("Exception: " + ex.Message.ToString());
					}

					#endregion Reset default printer for some reason

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
			catch (COMException cex)
			{
				{
					if (cex.InnerException != null)
					{
						Console.WriteLine("Inner Exception: " + cex.InnerException.Message);
						logger.Log(LogLevel.Debug, "Inner Exception: " + cex.InnerException.Message);

						if (cex.InnerException.InnerException != null)
						{
							Console.WriteLine("Inner Inner Exception: " + cex.InnerException.InnerException.Message);
							logger.Log(LogLevel.Debug, "Inner Inner Exception: " + cex.InnerException.InnerException.Message);
						}
					}
					Console.WriteLine("Exception: " + cex.Message.ToString());
					logger.Log(LogLevel.Debug, "Exception: " + cex.Message);

					logger.Log(LogLevel.Debug, "wordApplication.ActivePrinter: " + wordApplication.ActivePrinter);
					logger.Log(LogLevel.Debug, "default_printer: " + Settings.Printer_Name);

					return 0;
				}
			}
		}

		public static void CloseWord()
		{
			try
			{
				if (wordApplication != null)
				{
					logger.Log(LogLevel.Warn, "Closing Word");


					// close word and dispose reference
					object ofalse = false;
					//wordApplication.Documents.DisposeChildInstances();

					//wordApplication.Settings.EnableAutomaticQuit = true;
					wordApplication.Quit(ofalse);
					//wordApplication.Dispose();

					// Com crash
					// The object's type must be __ComObject or derived from __ComObject.
					// Parameter name: o

					//http://blogs.msdn.com/b/visualstudio/archive/2010/03/01/marshal-releasecomobject-considered-dangerous.aspx

					//System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApplication);
					wordApplication = null; // Set each COM Object to null
								 //
								 // After all of the COM objects have been released and set to null, do the following:
					GC.Collect(); // Start .NET CLR Garbage Collection
					GC.WaitForPendingFinalizers(); // Wait for Garbage Collection to finish

					//wordApplication = null; // BN
				}
			}
			catch (COMException comex)
			{
				// This error is not worthy of entering into the log file BN 2/02/2015
				logger.Log(LogLevel.Debug, "COMException" + comex.Message);
				Console.WriteLine("COMException" + comex.Message);
			}
			catch (Exception ex)
			{
				// This error is not worthy of entering into the log file BN 2/02/2015
				logger.Log(LogLevel.Debug, "This error message can be safely ignored: " + ex.Message);
				Console.WriteLine("CloseWord(): This error message can be safely ignored: " + ex.Message + " - " + ex.StackTrace);
			}
		}
		
		public static int SaveAsPdf()
		{
			int return_code = 0;

			object missing = System.Reflection.Missing.Value;

			// This next line has been missing the whole time - BN 29/10/2015
			PF.PrintLayer.Word.FilePath = PF.Common.Code.General.Get_Single_System_Code("PF-TPPath");

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

			 try
			{
				Console.WriteLine("SaveAsPdf: ExportFilePath: " + paramExportFilePath);
 
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
				//newDocument.Close();
				//Console.WriteLine("SaveAsPdf: ExportFilePath: " + paramExportFilePath);
				//logger.Log(LogLevel.Debug, "SaveAsPdf: ExportFilePath: " + paramExportFilePath);
				Common.Code.DebugStacktrace.StackTrace(ex);
				logger.Log(LogLevel.Debug, "Save as PDF Exception: " + ex.Message);
				if (ex.InnerException != null)
				{
					if (ex.InnerException.InnerException != null)
					{
						logger.Log(LogLevel.Debug, "Save as PDF Inner Exception: " + ex.InnerException.InnerException.Message);
					}
					else
					{
						logger.Log(LogLevel.Debug, "Save as PDF Exception: " + ex.Message);
						//MessageBox.Show("OpenDoc: newDocument is null.", "OpenDoc", MessageBoxButtons.OK, MessageBoxIcon.Stop);
					}
				}

				return_code = 9;
				//System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
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
				Common.Code.DebugStacktrace.StackTrace(ex);

				Console.WriteLine("SaveAsXPS: ExportFilePath: " + paramExportFilePath);
				//logger.Log(LogLevel.Debug, "SaveAsXPS: ExportFilePath: " + paramExportFilePath);
				return_code = 9;
				//logger.Log(LogLevel.Debug, "Message: " + ex.Message + ", StackTrace: " + ex.StackTrace);
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

		#endregion Log Code Static

		// Define the event handlers.
		private static void OnChanged(object source, FileSystemEventArgs e)
		{
			// Specify what is done when a file is changed, created, or deleted.
			Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
			logger.Log(LogLevel.Debug, "File: " + e.FullPath + " " + e.ChangeType);
		}

		private static void OnRenamed(object source, RenamedEventArgs e)
		{
			// Specify what is done when a file is renamed.
			Console.WriteLine("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
			logger.Log(LogLevel.Debug, "File: " + e.FullPath + " " + e.ChangeType);
		}
	}
}