using Microsoft.Office.Interop.Outlook;
using NLog;
using System;

namespace FruPak.PF.Data.Outlook
{
    /*Description
    -----------------
    Interface to Outlook.

    Date        Author     Desc
    -------------------------------------------------------------------------------------------------------------------------------------------------
    01/09/2013  Dave       Creation
    */

    public class Outlook
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static MAPIFolder mapifolder;
        public static Microsoft.Office.Interop.Outlook.Application OutLook_App = new Microsoft.Office.Interop.Outlook.Application();

        /// <summary>
        /// Gets / Sets the value for the MAPIFolder to be used
        /// </summary>
        public static string Folder_Name
        {
            get;
            set;
        }

        public static MAPIFolder Folder_Name_Locationfield;

        /// <summary>
        /// Get the MapiFolder Name and location to be used
        /// </summary>
        public static MAPIFolder Folder_Name_Location
        {
            get
            {
                return Get_folder_Name();
            }
            set
            {
                Folder_Name_Locationfield = Get_folder_Name();
            }
        }

        /// <summary>
        /// This will find the location within Outlook of the folder you are looking for.
        /// </summary>
        /// <param name="foldername"></param>
        /// <returns>MAPIFolder that is then used as the location for updating/inserting information</returns>
        ///
        private static MAPIFolder Get_folder_Name()
        {
            MAPIFolder Folder_Name;
            try
            {
                foreach (MAPIFolder folder in OutLook_App.Session.Folders)
                {
                    GetFolders(folder);
                }
            }
            catch (System.Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message);
            }
            //logger.Log(LogLevel.Info, LogCode(LogCode("MAPIFolder: " + mapifolder.Name.ToString())));

            return Folder_Name = mapifolder;
        }

        private static void GetFolders(MAPIFolder folder)
        {
            if (folder.Folders.Count == 0)
            {
                //Console.WriteLine(folder.FullFolderPath);

                string djb = folder.FullFolderPath.ToString();

                //Console.WriteLine("djb: " + djb);

                if (djb.Length > 4)
                {
                    if (djb.Substring(djb.LastIndexOf("\\") + 1) == Folder_Name)
                    {
                        mapifolder = folder;
                        Console.WriteLine("Folder name match: " + Folder_Name);
                    }
                }
            }
            else
            {
                foreach (MAPIFolder subFolder in folder.Folders)
                {
                    GetFolders(subFolder);

                    //Console.WriteLine(subFolder.FullFolderPath);
                    // subFolder has a count of 16 items; the first two subtrees are ignored
                }
            }
        }

        #region Log Code

        /// <summary>
        /// Formats the output to the debug log so that the actual log contents stand out amongst the machine-generated stuff.
        /// Refers to machine-generated content, not calling specifics.
        /// </summary>
        /// <param name="input">The code function that we want to log.</param>
        /// <returns>A formatted (Bracketed) string</returns>
        public static string LogCode(string input)
        {
            string Output = "___[ " + input + " ]___";
            return Output;
        }

        #endregion Log Code
    }
}