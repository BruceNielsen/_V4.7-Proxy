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
    CM_Trader Class.
     * 
     * This Class is a data access layer to the CM_Trader table
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
    public class CM_Trader
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Trader_Id) as Current_Id FROM CM_Trader");
        }
        public static DataSet Get_Info()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT *, Code + ' - ' + Description as Combined FROM CM_Trader WHERE PF_Active_Ind = 1 ORDER BY Code ");
        }
        public static DataSet Get_Info(int Trader_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM CM_Trader WHERE PF_Active_Ind = 1 AND Trader_Id = " + Trader_Id);
        }
        public static DataSet Get_Customer_Id(int Trader_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT C.Customer_Id " +
                                            "FROM CM_Trader T "+
                                            "INNER JOIN dbo.PF_Customer C on C.Customer_Id = T.Customer_Id "+
                                            "WHERE Trader_Id =" + Trader_Id);
        }
        public static DataSet Check_Trader(int Trader_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT Trader_Id FROM dbo.GH_Submission WHERE Trader_Id = " + Trader_Id + " "+
                                            "UNION "+
                                            "SELECT Trader_Id FROM dbo.PF_Work_Order WHERE Trader_Id = " + Trader_Id + " "+
                                            "UNION " +
                                            "SELECT Trader_Id FROM dbo.PF_Pallet WHERE Trader_Id = " + Trader_Id );
        }
        public static DataSet Get_Info(string Code)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM CM_Trader WHERE PF_Active_Ind = 1 AND Code = '" + Code + "'");
        }
        public static int Insert(int Trader_Id, string Code, string Description, int Customer_Id, string Barcode_Num, bool PF_Active_Ind, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO CM_Trader(Trader_Id, Code, Description, Customer_Id, Barcode_Num, PF_Active_Ind, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + Trader_Id + ",'" + Code + "','" + Description + "'," + Customer_Id + ",'" + Barcode_Num + "','" + PF_Active_Ind + "', GETDATE()," + Mod_User_Id + ")");
        }
        public static int Delete(int Trader_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM CM_Trader WHERE Trader_Id = " + Trader_Id);
        }
        public static int Update(int Trader_Id, string Code, string Description, int Customer_Id, string Barcode_Num, bool PF_Active_Ind, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE CM_Trader SET Code = '" + Code + "', " +
                                                                  "Description = '" + Description + "', " +
                                                                  "Customer_Id = " + Customer_Id + ", " +
                                                                  "Barcode_Num = '" + Barcode_Num + "', " +
                                                                  "PF_Active_Ind = '" + PF_Active_Ind + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Trader_Id = " + Trader_Id);
        }
    }
}
