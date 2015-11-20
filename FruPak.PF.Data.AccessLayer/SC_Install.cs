using FP.Utils.Data;
using NLog;
using System.Data;

namespace PF.Data.AccessLayer
{
    /*Description
    -----------------
    SC_Install Class.
     *
     * This Class is a data access layer to the SC_Install table
     * Where possible the following standard method names are used and standard column names used.
     *  1. Variable names as input to a method are the same as the column names they refer to.
     *
     * Methods.
     *  Get_Max_ID: Return the max ID from the table, and is stored as Current_Id
     *  Get_Info:   returns all data from the table or return data for a requested record
     *  Delete:     Deletes a single record, with an ID supplied
     *  Insert:     Inserts a new record
     *  Update:     Updates an existing record

    Date        Author     Desc
    -------------------------------------------------------------------------------------------------------------------------------------------------
    01/09/2013  Dave       Creation
    */

    //This class got overlooked when originally installing the logging code. BN 05-01-2015

    public class SC_Install
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static DataSet Get_Max_ID()
        {
            logger.Log(LogLevel.Info, LogCodeStatic("Get_Max_ID"));

            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Install_Id) as Current_Id FROM SC_Install");
        }

        public static DataSet Get_MAC(string MAC)
        {
            if (MAC != string.Empty && MAC != null)
            {
                logger.Log(LogLevel.Info, LogCodeStatic("Get_MAC: " + MAC));

                PF.Data.AccessLayer.DConfig.CreateDConfig();
                return SQLAccessLayer.Run_Query("SELECT * FROM SC_Install WHERE MAC ='" + MAC + "'");
            }
            else
            {
                logger.Log(LogLevel.Info, LogCodeStatic("Get_MAC: (empty)"));
                // Phantom 16/12/2014
                // Mac address is empty, but let the program continue with a dummy address
                // so the error logging can trace what exactly happened
                PF.Data.AccessLayer.DConfig.CreateDConfig();
                return SQLAccessLayer.Run_Query("SELECT * FROM SC_Install WHERE MAC ='" + "Null Mac Address" + "'");
            }
        }

        public static int Insert(int Install_Id, string MAC)
        {
            logger.Log(LogLevel.Info, LogCodeStatic("Insert: Install_Id: " + Install_Id.ToString() + ", " + MAC));

            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO SC_Install(Install_Id, MAC, last_update_date) " +
                                                "VALUES ( " + Install_Id + ",'" + MAC + "', GETDATE())");
        }

        public static int Update(int Install_Id, string MAC)
        {
            logger.Log(LogLevel.Info, LogCodeStatic("Update: Install_Id: " + Install_Id.ToString() + ", " + MAC));

            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE SC_Install SET MAC = '" + MAC + "', " +
                                               "last_update_date = GETDATE() " +
                                               " WHERE Install_Id = " + Install_Id);
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
    }
}