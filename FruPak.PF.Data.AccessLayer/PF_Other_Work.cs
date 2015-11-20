using FP.Utils.Data;
using System.Data;

namespace PF.Data.AccessLayer
{
    /*Description
    -----------------
    PF_Other_Work Class.
     *
     * This Class is a data access layer to the PF_Other_Work table
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

    public class PF_Other_Work
    {
        public static DataSet Get_Max_ID()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Other_Work_Id) as Current_Id FROM PF_Other_Work");
        }

        public static DataSet Get_Info()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Other_Work ORDER BY Other_Work_Id DESC");
        }

        public static DataSet Get_Info_Translated()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT OW.*, OWT.Code as OWT_Code, OWT.Description as OWT_Description, S.First_Name + ' ' + S.Last_Name as Name " +
                                            "FROM dbo.PF_Other_Work OW " +
                                            "INNER JOIN dbo.PF_Other_Work_Types OWT ON OWT.Other_Work_Types_Id = OW.Other_Work_Types_Id " +
                                            "INNER JOIN dbo.PF_Staff S ON S.Staff_Id = OW.Staff_Id  " +
                                            "ORDER BY OW.Other_Work_Id DESC");
        }

        public static int Insert(int Other_Work_Id, int Other_Work_Types_Id, int Staff_Id, string Start_DateTime, string Finish_DateTime, string Description, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("SET DATEFORMAT dmy " +
                                               "INSERT INTO PF_Other_Work(Other_Work_Id, Other_Work_Types_Id, Staff_Id, Start_DateTime, Finish_DateTime, Description,  Mod_Date, Mod_User_Id) " +
                                               "VALUES ( " + Other_Work_Id + "," + Other_Work_Types_Id + "," + Staff_Id + ",'" + Start_DateTime + "','" + Finish_DateTime + "','" + Description + "', GETDATE()," + Mod_User_Id + ")");
        }

        public static int Update(int Other_Work_Id, int Other_Work_Types_Id, int Staff_Id, string Start_DateTime, string Finish_DateTime, string Description, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_Other_Work SET Other_Work_Types_Id = " + Other_Work_Types_Id + ", " +
                                                                   "Staff_Id = " + Staff_Id + ", " +
                                                                   "Start_DateTime = '" + Start_DateTime + "', " +
                                                                   "Finish_DateTime = '" + Finish_DateTime + "', " +
                                                                   "Description = '" + Description + "' ," +
                                                                   "Mod_Date = GETDATE(), " +
                                                                   "Mod_User_Id = " + Mod_User_Id + " " +
                                                                   "WHERE Other_Work_Id = " + Other_Work_Id);
        }

        public static int Delete(int Other_Work_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Other_Work WHERE Other_Work_Id = " + Other_Work_Id);
        }
    }
}