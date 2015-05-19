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
    PF_Cleaning_Area_Parts_Completed Class.
     * 
     * This Class is a data access layer to the PF_Cleaning_Area_Parts_Completed table
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
    public class PF_Cleaning_Area_Parts_Completed
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(CleanAreaPartCmp_Id) as Current_Id FROM PF_Cleaning_Area_Parts_Completed");
        }
        public static DataSet Get_Info(int CleanAreaCmp_Id, int CleanAreaParts_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Cleaning_Area_Parts_Completed WHERE CleanAreaCmp_Id = " + CleanAreaCmp_Id + " AND CleanAreaParts_Id = " + CleanAreaParts_Id );
        }
        public static DataSet Get_Info(int CleanArea_Id, int CleanAreaParts_Id, string Complete_Date)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Cleaning_Area_Parts_Completed CAPC " + 
                                                     "INNER JOIN PF_Cleaning_Area_Completed CAC on CAC.CleanAreaCmp_Id = CAPC.CleanAreaCmp_Id " +
                                            "WHERE CAC.CleanArea_Id = " + CleanArea_Id + " AND CAPC.CleanAreaParts_Id = " + CleanAreaParts_Id + " AND CAC.Complete_Date = '" + Complete_Date + "'");
        }
        public static int Insert(int CleanAreaPartCmp_Id, int CleanAreaCmp_Id, int CleanAreaParts_Id, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_Cleaning_Area_Parts_Completed(CleanAreaPartCmp_Id, CleanAreaCmp_Id, CleanAreaParts_Id, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + CleanAreaPartCmp_Id + "," + CleanAreaCmp_Id + "," + CleanAreaParts_Id + ", GETDATE()," + Mod_User_Id + ")");
        }
        public static DataSet Delete(int CleanAreaPartCmp_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("DELETE FROM PF_Cleaning_Area_Parts_Completed WHERE CleanAreaPartCmp_Id = " + CleanAreaPartCmp_Id);
        }
    }
}