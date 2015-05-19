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
    PF_A_Invoice Class.
     * 
     * This Class is a data access layer to the PF_A_Invoice table
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
    public class PF_A_Invoice
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Invoice_Id) as Current_Id FROM PF_A_Invoice");
        }
        public static DataSet Get_Info(int Invoice_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_A_Invoice WHERE Invoice_Id = " + Invoice_Id); 
        }
        public static DataSet Get_Info()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT Invoice_Id ,Invoice_Date, Customer_Id ,Trader_Id  ,Greentree_Date ,Order_Num ,Comments FROM PF_A_Invoice ORDER BY Invoice_Id DESC");
        }
        public static DataSet Get_Unpaid()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT Invoice_Id ,Invoice_Date, Customer_Id ,Trader_Id  ,Greentree_Date ,Order_Num ,Comments FROM PF_A_Invoice WHERE Payment_Received is null ORDER BY Invoice_Id DESC");
        }
        public static DataSet Get_Info_for_Invoice_search()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT Invoice_Id ,Invoice_Date, C.Name as Customer, C.Customer_Id  ,Greentree_Date ,Order_Num ,Comments "+
                                            "FROM PF_A_Invoice AI "+
                                            "INNER JOIN dbo.PF_Customer C ON C.Customer_Id = AI.Customer_Id "+
                                            "ORDER BY Invoice_Id DESC");
        }
        public static DataSet Get_Info_Payments_Not_Recevied()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT AI.Invoice_Id ,Invoice_Date, C.Name as Customer, C.Customer_Id  ,Greentree_Date  ,SUM(AID.Cost) as Cost " +
                                            "FROM PF_A_Invoice AI " +
                                            "INNER JOIN dbo.PF_Customer C ON C.Customer_Id = AI.Customer_Id " +
                                            "INNER JOIN dbo.PF_A_Invoice_Details AID ON AID.Invoice_Id = AI.Invoice_Id " +
                                            "WHERE AI.Payment_Received is NULL " +
                                            "GROUP BY AI.Invoice_Id ,Invoice_Date, C.Name , C.Customer_Id  ,Greentree_Date " +
                                            "ORDER BY C.Customer_Id, AI.Invoice_Id DESC");
        }
        public static DataSet Get_Info_Payments_Not_Recevied(string where)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT AI.Invoice_Id ,Invoice_Date, C.Name as Customer, C.Customer_Id  ,Greentree_Date  ,SUM(AID.Cost) as Cost " +
                                            "FROM PF_A_Invoice AI " +
                                            "INNER JOIN dbo.PF_Customer C ON C.Customer_Id = AI.Customer_Id " +
                                            "INNER JOIN dbo.PF_A_Invoice_Details AID ON AID.Invoice_Id = AI.Invoice_Id " +
                                            "WHERE C.Customer_Id in " + where + " AND AI.Payment_Received is NULL " +
                                            "GROUP BY AI.Invoice_Id ,Invoice_Date, C.Name , C.Customer_Id  ,Greentree_Date " +
                                            "ORDER BY C.Customer_Id, AI.Invoice_Id DESC");
        }
        public static DataSet Get_Info_For_Trader(int Trader_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT Invoice_Id ,Invoice_Date ,Trader_Id  ,Greentree_Date ,Order_Num ,Comments FROM PF_A_Invoice WHERE Trader_Id = " + Trader_Id + " ORDER BY Invoice_Id DESC");
        }
        public static int Insert(int Invoice_Id, string Invoice_Date, int Trader_Id, string Comments, string Order_Num, int Mod_User_Id, int Customer_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_A_Invoice(Invoice_Id,  Invoice_Date, Trader_Id, Comments,Order_Num, Mod_Date, Mod_User_Id, Customer_Id) " +
                                                "VALUES ( " + Invoice_Id + ",'" + Invoice_Date + "'," + Trader_Id + ",'" + Comments + "','" + Order_Num + "', GETDATE()," + Mod_User_Id + "," + Customer_Id + ")");
        }
        public static int Insert_Sales(int Invoice_Id, string Invoice_Date, int Customer_Id, string Comments, string Order_Num, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_A_Invoice(Invoice_Id,  Invoice_Date, Customer_Id, Comments,Order_Num, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + Invoice_Id + ",'" + Invoice_Date + "'," + Customer_Id + ",'" + Comments + "','" + Order_Num + "', GETDATE()," + Mod_User_Id + ")");
        }
        public static DataSet Get_Info_for_GreenTree(int Invoice_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT AI.*, AID.Cost, P.Code, P.Description " +
                                            "FROM dbo.PF_A_Invoice AI " +
                                            "INNER JOIN dbo.PF_A_Invoice_Details AID ON AID.Invoice_Id = AI.Invoice_Id " +
                                            "INNER JOIN dbo.PF_Product P ON P.Product_Id = AID.Product_Id "+
                                            "LEFT OUTER JOIN dbo.CM_Trader T ON T.Trader_Id = AI.Trader_Id " +
                                            "WHERE AI.Invoice_Id = " + Invoice_Id);
        }
        public static int Update_Greentree_Date(int Invoice_Id, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE  dbo.PF_A_Invoice SET Greentree_Date = GETDATE(), " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Invoice_Id = " + Invoice_Id);
        }
        public static DataSet Get_Discount_for_Invoice(int Invoice_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("select CSD.Value " +
                                            "from dbo.PF_A_Invoice I "+
                                            "INNER JOIN dbo.PF_A_Invoice_Details ID on ID.Invoice_Id = I.Invoice_Id " +
                                            "inner join dbo.PF_A_Customer_Sales_Discount CSD ON CSD.Customer_Id = I.Customer_Id and ID.Material_Id = CSD.Material_Id " +
                                            "WHERE I.Invoice_Id = " + Invoice_Id);
        }
        public static int Update_Payment_Received(int Invoice_Id, string Payment_Received, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE  dbo.PF_A_Invoice SET Payment_Received = '" + Payment_Received + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Invoice_Id = " + Invoice_Id);
        }
        public static DataSet Get_Statement(string Where, int Month)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT C.Customer_Id,C.Name,I.Invoice_Id, I.Invoice_Date, I.Payment_Received, AID.Cost " +
                                            "FROM dbo.PF_A_Invoice I " +
                                            "INNER JOIN dbo.PF_A_Invoice_Details AID ON I.Invoice_Id = AID.Invoice_Id " +
                                            "INNER JOIN dbo.PF_Customer C ON C.Customer_Id = I.Customer_Id " +
                                            "WHERE I.Customer_Id in " + Where + " AND (MONTH(I.Invoice_Date) = " + Month + " OR Payment_Received is null) "+
                                            "UNION " +
                                            "SELECT C.Customer_Id,C.Name,null, null, null, SUM(AID.Cost) " +
                                            "FROM dbo.PF_A_Invoice I "+
                                            "INNER JOIN dbo.PF_A_Invoice_Details AID ON I.Invoice_Id = AID.Invoice_Id "+
                                            "INNER JOIN dbo.PF_Customer C ON C.Customer_Id = I.Customer_Id "+
                                            "WHERE I.Customer_Id in " + Where + " AND (MONTH(I.Invoice_Date) = " + Month + " OR Payment_Received is null) " +
                                            "GROUP BY C.Customer_Id, C.Name "+
                                            "ORDER BY C.Customer_Id, I.Invoice_Id DESC");
        }
        public static int Delete(int Invoice_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_A_Invoice WHERE Invoice_Id = " + Invoice_Id);
        }
    }
}

