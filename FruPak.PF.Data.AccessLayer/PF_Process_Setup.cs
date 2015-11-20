using FP.Utils.Data;
using System.Data;

namespace PF.Data.AccessLayer
{
    /*Description
    -----------------
    PF_Process_Setup Class.
     *
     * This Class is a data access layer to the PF_Process_Setup table
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

    public class PF_Process_Setup
    {
        public static DataSet Get_Max_ID()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Processsetup_Id) as Current_Id FROM PF_Process_Setup");
        }

        public static DataSet Get_Info()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Process_Setup WHERE PF_Active_Ind = 1 ");
        }

        public static DataSet Get_Info_Translated()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT PS.*, S.First_Name + ' ' + S.Last_Name as Name " +
                                            "FROM dbo.PF_Process_Setup PS " +
                                            "INNER JOIN dbo.PF_Staff S ON S.Staff_Id = PS.Staff_Id " +
                                            "WHERE PS.PF_Active_Ind = 1");
        }

        public static int Insert(int Processsetup_Id, int Staff_Id, string Start_Date, string Start_Time, string Finish_Date, string Finish_Time, string Comments, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_Process_Setup(Processsetup_Id, Staff_Id, Start_Date, Start_Time, Finish_Date, Finish_Time, Comments, PF_Active_Ind, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + Processsetup_Id + "," + Staff_Id + ",'" + Start_Date + "','" + Start_Time + "','" + Finish_Date + "','" + Finish_Time + "','" + Comments + "','true', GETDATE()," + Mod_User_Id + ")");
        }

        public static int Delete(int Processsetup_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Process_Setup WHERE Processsetup_Id = " + Processsetup_Id);
        }

        public static int Update(int Processsetup_Id, int Staff_Id, string Start_Date, string Start_Time, string Finish_Date, string Finish_Time, string Comments, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_Process_Setup SET Staff_Id = " + Staff_Id + ", " +
                                                                  "Start_Date = '" + Start_Date + "', " +
                                                                  "Start_Time = '" + Start_Time + "', " +
                                                                  "Finish_Date = '" + Finish_Date + "', " +
                                                                  "Finish_Time = '" + Finish_Time + "', " +
                                                                  "Comments = '" + Comments + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Processsetup_Id = " + Processsetup_Id);
        }

        public static int Update_Active(bool PF_Active_Ind, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_Process_Setup SET PF_Active_Ind = '" + PF_Active_Ind + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id);
        }
    }
}