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
    CM_Orchardist Class.
     * 
     * This Class is a data access layer to the CM_Orchardist table
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
    public class CM_Orchardist
    {
        public static DataSet Check_Orchardist(int Orchardist_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT Orchardist_Id FROM dbo.CM_Grower WHERE Orchardist_Id = " + Orchardist_Id + " " +
                                            "UNION " +
                                            "SELECT Orchardist_Id FROM dbo.PF_Work_Order WHERE Orchardist_Id = " + Orchardist_Id + " ");
        }
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Orchardist_Id) as Current_Id FROM CM_Orchardist");
        }
        public static DataSet Get_Info()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM CM_Orchardist WHERE PF_Active_Ind = 1 ORDER BY Code");
        }
        public static DataSet Get_Info(string Code)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM CM_Orchardist WHERE PF_Active_Ind = 1 AND Code = '" + Code + "'");
        }
        public static DataSet Get_Info(int Orchardist_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM CM_Orchardist WHERE PF_Active_Ind = 1 AND Orchardist_Id = " + Orchardist_Id);
        }
        public static int Insert(int Orchardist_Id, string Code, string Description, int Customer_Id, bool PF_Active_Ind, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO CM_Orchardist(Orchardist_Id, Code, Description, Customer_Id, PF_Active_Ind, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + Orchardist_Id + ",'" + Code + "','" + Description + "'," + Customer_Id + ",'" + PF_Active_Ind + "', GETDATE()," + Mod_User_Id + ")");
        }
        public static int Delete(int Orchardist_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM CM_Orchardist WHERE Orchardist_Id = " + Orchardist_Id);
        }
        public static int Update(int Orchardist_Id, string Code, string Description, int Customer_Id, bool PF_Active_Ind, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE CM_Orchardist SET Code = '" + Code + "', " +
                                                                  "Description = '" + Description + "', " +
                                                                  "Customer_Id = " + Customer_Id + ", " +
                                                                  "PF_Active_Ind = '" + PF_Active_Ind + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Orchardist_Id = " + Orchardist_Id);
        }
    }
}