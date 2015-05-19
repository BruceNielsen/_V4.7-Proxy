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
    PF_Stock_Holding Class.
     * 
     * This Class is a data access layer to the PF_Stock_Holding table
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
    public class PF_Stock_Holding
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Stock_Hold_Id) as Current_Id FROM PF_Stock_Holding");
        }
        public static DataSet Get_Info_Translated()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT SH.*, SI.Code as SI_Code, SI.Description AS SI_Description " +
                                            "FROM dbo.PF_Stock_Holding SH " +
                                            "INNER JOIN dbo.PF_Stock_Item SI ON SI.Stock_Item_Id = SH.Stock_Item_Id " +
                                            "ORDER BY SH.Arrival_Date DESC");
        }
        public static int Insert(int Stock_Hold_Id, int Stock_Item_Id, decimal Season, string Invoice_Num, string Arrival_Date, int Quantity, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_Stock_Holding(Stock_Hold_Id,  Stock_Item_Id, Season, Invoice_Num, Arrival_Date, Quantity, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + Stock_Hold_Id + "," + Stock_Item_Id + "," + Season + ",'" + Invoice_Num + "', '" + Arrival_Date + "'," + Quantity + ", GETDATE()," + Mod_User_Id + ")");
        }
        public static int Update(int Stock_Hold_Id, int Stock_Item_Id, string Invoice_Num, string Arrival_Date, int Quantity, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_Stock_Holding SET Stock_Item_Id = " + Stock_Item_Id + ", " +
                                                                  "Invoice_Num = '" + Invoice_Num + "', " +
                                                                  "Arrival_Date = '" + Arrival_Date + "', " +
                                                                  "Quantity = " + Quantity + ", " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Stock_Hold_Id = " + Stock_Hold_Id);
        }
        public static int Delete(int Stock_Hold_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Stock_Holding WHERE Stock_Hold_Id = " + Stock_Hold_Id);
        }
        public static DataSet Holding_Sum()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT SH.Stock_Item_Id, SUM(SH.Quantity)as Holding FROM dbo.PF_Stock_Holding SH GROUP BY SH.Stock_Item_Id");
        }
    }
}
