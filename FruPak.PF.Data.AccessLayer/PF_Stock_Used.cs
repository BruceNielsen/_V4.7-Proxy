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
    PF_Stock_Used Class.
     * 
     * This Class is a data access layer to the PF_Stock_Used table
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
    public class PF_Stock_Used
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Stock_Used_Id) as Current_Id FROM PF_Stock_Used");
        }
        public static DataSet Get_Info_Translated()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT SU.*, SI.Code as SI_Code, SI.Description AS SI_Description " +
                                            "FROM dbo.PF_Stock_Used SU " +
                                            "INNER JOIN dbo.PF_Stock_Item SI ON SI.Stock_Item_Id = SU.Stock_Item_Id " +
                                            "ORDER BY SU.Date_Used DESC");
        }
        public static int Insert(int Stock_Used_Id, int Stock_Item_Id, decimal Season, int Quantity, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_Stock_Used(Stock_Used_Id,  Stock_Item_Id, Season, Date_Used, Quantity, Comment, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + Stock_Used_Id + "," + Stock_Item_Id + "," + Season + ", GETDATE()," + Quantity + ", 'Normal Processing', GETDATE()," + Mod_User_Id + ")");
        }
        public static int Insert(int Stock_Used_Id, int Stock_Item_Id, decimal Season, string Date_Used, int Quantity, string Comment, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_Stock_Used(Stock_Used_Id,  Stock_Item_Id, Season, Date_Used, Quantity, Comment, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + Stock_Used_Id + "," + Stock_Item_Id + "," + Season + ",'" + Date_Used + "', " + Quantity + ",'" + Comment + "', GETDATE()," + Mod_User_Id + ")");
        }
        public static int Update(int Stock_Used_Id, int Stock_Item_Id, string Date_Used, int Quantity, string Comment, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_Stock_Used SET Stock_Item_Id = " + Stock_Item_Id + ", " +                                                     
                                                      "Date_Used = '" + Date_Used + "', " +
                                                      "Quantity = " + Quantity + ", " +
                                                      "Comment = '" + Comment + "', " +
                                                      "Mod_date = GETDATE(), " +
                                                      "Mod_User_Id = " + Mod_User_Id +
                                  " WHERE Stock_Used_Id = " + Stock_Used_Id);
        }
        public static DataSet Used_Sum()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT SU.Stock_Item_Id, SUM(SU.Quantity) as Used FROM dbo.PF_Stock_Used SU GROUP BY SU.Stock_Item_Id");
        }
    }
}