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
    EX_GDI_Submission Class.
     * 
     * This Class is a data access layer to the EX_GDI_Submission table
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
    02/10/2013  Alison       Creation
    */
    public class EX_GDI_Submission
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Submission_Id) as Current_Id FROM EX_GDI_Submission");
        }
        public static int Insert(int Submission_Id, int Trader_Id, int Grower_Id, int Block_Id, string Grower_Reference, string Sub_date, string Harvest_date, int Pick_Num, string Comments, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO EX_GDI_Submission(Submission_Id, Trader_Id, Grower_Id, Block_Id, Grower_Reference, Sub_date, Harvest_date, Pick_Num, Comments, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + Submission_Id + "," + Trader_Id + "," + Grower_Id + "," + Block_Id + ",'" + Grower_Reference + "','" + Sub_date + "','" + Harvest_date + "'," + Pick_Num + ",'" + Comments + "', GETDATE()," + Mod_User_Id + ")");
        }
    }
}
