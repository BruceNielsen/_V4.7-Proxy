using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using FruPak.Utils.Data;
using FruPak.PF.CustomSettings;
using System.IO;
using NLog;
using System.Windows.Forms;


namespace FruPak.PF.Data.AccessLayer
{
	/// <summary>
	/// Summary description for DConfig.
	/// </summary>
	public class DConfig
	{
		// Phantom 11/12/2014
		private static Logger logger = LogManager.GetCurrentClassLogger();

		// Phantom 17/12/2014 Declare application settings object
		private static PhantomCustomSettings Settings;

		private static DConfig dal;
		private DConfig()
		{
			#region Load CustomSettings
			#region Get "My Documents" folder
			string path = Application.StartupPath;
			// string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			//path = Path.Combine(path, "FruPak_Settings"); // Creates a subfolder

			// Create folder if it doesn't already exist
			//if (!Directory.Exists(path))
			//    Directory.CreateDirectory(path);
			#endregion

			// Initialize settings
			Settings = new PhantomCustomSettings();
			Settings.SettingsPath = Path.Combine(path, "FruPak.Phantom.config");
			Settings.EncryptionKey = "phantomKey";

			if (!File.Exists(Settings.SettingsPath))
			{
				Settings.Save();
				logger.Log(LogLevel.Info, LogCode("Default Phantom Settings file created"));
			}
			// Load settings - Normally in Form_Load
			Settings.Load();

			//logger.Log(LogLevel.Info, LogCode("Settings file opened.");

			//local_path = Settings.Path_Local_Path;
			//remote_path = Settings.Path_Remote_Path;
			//path_to_settings = Settings.Path_To_Settings;

			// Save settings - Normally in Form_Closing
			//Settings.Save();

			#endregion

			if (FruPak.PF.Global.Global.bol_Testing == true)
			{
				if (Settings.Phantom_Dev_Use_FruPak_SQL == true)
				{
					#region FRUPAK-SQL Test mode
					SQLAccessLayer.ConnectionString = @"Provider=sqloledb;" +
												"Server=" + Settings.TestMode_Server + ";" +
												"Database=" + Settings.TestMode_Database + ";" +
												"User Id=" + Settings.TestMode_User_Id + ";" +
												"Password=" + Settings.TestMode_Password + ";";

					logger.Log(LogLevel.Info, LogCode("Dev mode connection string = " + SQLAccessLayer.ConnectionString));

					#region Original hardcoded string
					//                SQLAccessLayer.ConnectionString = @"Provider=sqloledb;
					//				                                                Server=FRUPAK-SQL;
					//				                                                Database=Process_Factory_Test;
					//				                                                User Id=jobs;
					//				                                                Password=jobs;";
					
					#endregion
					#endregion

				}
				else
				{
					#region Bruce-Laptop Test mode
					SQLAccessLayer.ConnectionString = @"Provider=sqloledb;" +
												"Server=" + Settings.Phantom_Dev_TestMode_Server + ";" +
												"Database=" + Settings.Phantom_Dev_TestMode_Database + ";" +
												"User Id=" + Settings.Phantom_Dev_TestMode_User_Id + ";" +
												"Password=" + Settings.Phantom_Dev_TestMode_Password + ";";

					logger.Log(LogLevel.Info, LogCode("Dev mode connection string = " + SQLAccessLayer.ConnectionString));

					#region Original hardcoded string
					//                SQLAccessLayer.ConnectionString = @"Provider=sqloledb;
					//												Server=Bruce-Laptop;
					//												Database=Process_Factory_Test;
					//												User Id=jobs;
					//												Password=jobs;"; 
					#endregion


					#endregion                
				}

			}
			else
			{
				if (Settings.Phantom_Dev_Use_FruPak_SQL == true)
				{
				   #region FRUPAK-SQL Production Mode
					SQLAccessLayer.ConnectionString = @"Provider=sqloledb;" +
												"Server=" + Settings.ProductionMode_Server + ";" +
												"Database=" + Settings.ProductionMode_Database + ";" +
												"User Id=" + Settings.ProductionMode_User_Id + ";" +
												"Password=" + Settings.ProductionMode_Password + ";";

					//logger.Log(LogLevel.Info, LogCode("Production mode connection string = " + SQLAccessLayer.ConnectionString);

					#region Original hardcoded string
					//                SQLAccessLayer.ConnectionString = @"Provider=sqloledb;
					//				                                                Server=FRUPAK-SQL;
					//				                                                Database=Process_Factory;
					//				                                                User Id=jobs;
					//				                                                Password=jobs;";

					#endregion
					#endregion
				}
				else
				{
 
					#region Bruce-Laptop Production Mode
					SQLAccessLayer.ConnectionString = @"Provider=sqloledb;" +
												"Server=" + Settings.Phantom_Dev_ProductionMode_Server + ";" +
												"Database=" + Settings.Phantom_Dev_ProductionMode_Database + ";" +
												"User Id=" + Settings.Phantom_Dev_ProductionMode_User_Id + ";" +
												"Password=" + Settings.Phantom_Dev_ProductionMode_Password + ";";

					//logger.Log(LogLevel.Info, LogCode("Production mode connection string = " + SQLAccessLayer.ConnectionString);

					#region Original hardcoded string
					//                SQLAccessLayer.ConnectionString = @"Provider=sqloledb;
					//												Server=Bruce-Laptop;
					//												Database=Process_Factory;
					//												User Id=jobs;
					//												Password=jobs;"; 
					#endregion

					#endregion                
				}
			}
		}
	
		public static void CreateDConfig()
		{
			dal = new DConfig();
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
		#endregion

	}
}
