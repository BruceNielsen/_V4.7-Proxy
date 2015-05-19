using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using FruPak.Utils.Data;

namespace FruPak.PF.Data.AccessLayer
{
    /*Description
    -----------------
    PF_Reports Class.
     * 
     * This Class is a data access layer to the PF_Reports table
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
    public class PF_Reports
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Report_Id) as Current_Id FROM PF_Reports");
        }
        public static DataSet Get_Info()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Reports WHERE PF_Active_Ind = 1 ORDER BY Description");
        }
        public static DataSet Get_Info(int Report_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Reports WHERE PF_Active_Ind = 1 AND  Report_Id = " + Report_Id);
        }
        public static DataSet Get_Info(string Code)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Reports WHERE PF_Active_Ind = 1 AND Code = '" + Code + "'");
        }



        public static DataSet Get_Info_by_like_Code(string Code)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Reports WHERE PF_Active_Ind = 1 AND Code like" + Code + "'");
        }
        public static int Insert(int Report_Id, string Code, string Description, string Value, bool PF_Active_Ind, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_Reports(Report_Id,  Code, Description, Value, PF_Active_Ind, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + Report_Id + ",'" + Code + "','" + Description + "','" + Value + "','" + PF_Active_Ind + "', GETDATE()," + Mod_User_Id + ")");
        }
        public static int Delete(int Report_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Reports WHERE Report_Id = " + Report_Id);
        }
        public static int Update(int Report_Id, string Code, string Description, string Value, bool PF_Active_Ind, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_Reports SET Code = '" + Code + "', " +
                                                                  "Description = '" + Description + "', " +
                                                                  "Value = '" + Value + "', " +
                                                                  "PF_Active_Ind = '" + PF_Active_Ind + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Report_Id = " + Report_Id);
        }
    }
}
