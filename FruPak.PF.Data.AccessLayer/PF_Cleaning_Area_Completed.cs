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
    PF_Cleaning_Area_Completed Class.
     * 
     * This Class is a data access layer to the PF_Cleaning_Area_Completed table
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
    public class PF_Cleaning_Area_Completed
	{
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(CleanAreaCmp_Id) as Current_Id FROM PF_Cleaning_Area_Completed");
        }

        public static DataSet Get_Info(int CleanArea_Id, string Complete_Date)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Cleaning_Area_Completed WHERE CleanArea_Id = " + CleanArea_Id + " AND Complete_Date ='" + Complete_Date + "'");
        }
        public static int Insert(int CleanAreaCmp_Id, string Complete_Date, int CleanArea_Id, int Staff_Id, string Start_Time, string Finish_Time, string Comments, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_Cleaning_Area_Completed(CleanAreaCmp_Id, Complete_Date, CleanArea_Id, Staff_Id, Start_Time, Finish_Time, Comments, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + CleanAreaCmp_Id + ",'" + Complete_Date + "'," + CleanArea_Id + "," + Staff_Id + ",'" + Start_Time + "','"+Finish_Time+"','"+Comments+"', GETDATE()," + Mod_User_Id + ")");
        }
        public static int Update(int CleanAreaCmp_Id, string Complete_Date, int Staff_Id, string Start_Time, string Finish_Time, string Comments, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_Cleaning_Area_Completed SET Complete_Date = '" + Complete_Date + "', " +                                                                  
                                                                  "Start_Time = '" + Start_Time + "', " +
                                                                  "Finish_Time = '" + Finish_Time + "', " +
                                                                  "Comments = '" + Comments + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE CleanAreaCmp_Id = " + CleanAreaCmp_Id + " AND Staff_Id = " + Staff_Id);
        }
	}
}