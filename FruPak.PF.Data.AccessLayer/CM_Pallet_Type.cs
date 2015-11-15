using FruPak.Utils.Data;
using NLog;
using System.Data;

namespace FruPak.PF.Data.AccessLayer
{
    /*Description
    -----------------
    CM_Pallet_Type Class.
     *
     * This Class is a data access layer to the CM_Pallet_Type table
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

    public class CM_Pallet_Type
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            logger.Log(LogLevel.Info, LogCodeStatic("SELECT max(PalletType_Id) as Current_Id FROM CM_Pallet_Type"));
            return SQLAccessLayer.Run_Query("SELECT max(PalletType_Id) as Current_Id FROM CM_Pallet_Type");
        }

        public static DataSet Get_Info(string Code)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            logger.Log(LogLevel.Info, LogCodeStatic("SELECT * FROM CM_Pallet_Type WHERE PF_Active_Ind = 1 AND Code = '" + Code + "'"));
            return SQLAccessLayer.Run_Query("SELECT * FROM CM_Pallet_Type WHERE PF_Active_Ind = 1 AND Code = '" + Code + "'");
        }

        public static int Insert(int PalletType_Id, string Code, string Description, bool PF_Active_Ind, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            logger.Log(LogLevel.Info, LogCodeStatic("INSERT INTO CM_Pallet_Type(PalletType_Id, Code, Description, PF_Active_Ind, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + PalletType_Id + ",'" + Code + "','" + Description + "','" + PF_Active_Ind + "', GETDATE()," + Mod_User_Id + ")"));
            return SQLAccessLayer.Run_NonQuery("INSERT INTO CM_Pallet_Type(PalletType_Id, Code, Description, PF_Active_Ind, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + PalletType_Id + ",'" + Code + "','" + Description + "','" + PF_Active_Ind + "', GETDATE()," + Mod_User_Id + ")");
        }

        public static int Delete(int PalletType_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            logger.Log(LogLevel.Info, LogCodeStatic("DELETE FROM CM_Pallet_Type WHERE PalletType_Id = " + PalletType_Id));
            return SQLAccessLayer.Run_NonQuery("DELETE FROM CM_Pallet_Type WHERE PalletType_Id = " + PalletType_Id);
        }

        public static int Update(int PalletType_Id, string Code, string Description, bool PF_Active_Ind, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            logger.Log(LogLevel.Info, LogCodeStatic("UPDATE CM_Pallet_Type SET Code = '" + Code + "', " +
                                                                  "Description = '" + Description + "', " +
                                                                  "PF_Active_Ind = '" + PF_Active_Ind + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE PalletType_Id = " + PalletType_Id));
            return SQLAccessLayer.Run_NonQuery("UPDATE CM_Pallet_Type SET Code = '" + Code + "', " +
                                                                  "Description = '" + Description + "', " +
                                                                  "PF_Active_Ind = '" + PF_Active_Ind + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE PalletType_Id = " + PalletType_Id);
        }

        #region ------------- Stored Procedures -------------

        #region ------------- Select -------------

        public static DataSet Get_Info()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            logger.Log(LogLevel.Info, LogCodeStatic("CM_Pallet_Type_Get_Info"));
            return SQLAccessLayer.RunSP_Query("dbo.CM_Pallet_Type_Get_Info");
        }

        #endregion ------------- Select -------------

        #endregion ------------- Stored Procedures -------------

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