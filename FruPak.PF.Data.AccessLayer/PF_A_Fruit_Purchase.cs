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
    PF_A_Fruit_Purchase Class.
     * 
     * This Class is a data access layer to the PF_A_Fruit_Purchase table
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
    public class PF_A_Fruit_Purchase
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(FruitPurchase_Id) as Current_Id FROM PF_A_Fruit_Purchase");
        }
        public static DataSet Get_Info()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_A_Fruit_Purchase ");
        }
        public static DataSet Get_Info_Translated()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT FP.*, C.Name " +
                                            "FROM dbo.PF_A_Fruit_Purchase FP " +
                                            "INNER JOIN dbo.PF_Customer C ON C.Customer_Id = FP.Customer_Id " +
                                            "ORDER BY C.Name, FP.FruitPurchase_Id DESC ");
        }
        public static int insert(int FruitPurchase_Id, int Customer_Id, decimal Cost, string Comments, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_A_Fruit_Purchase(FruitPurchase_Id, Customer_Id, Cost, Comments, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + FruitPurchase_Id + "," + Customer_Id + "," + Cost + ",'" + Comments + "', GETDATE()," + Mod_User_Id + ")");
        }
        public static int Delete(int FruitPurchase_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_A_Fruit_Purchase WHERE FruitPurchase_Id = " + FruitPurchase_Id);
        }
        public static DataSet Get_Purchase_Info(int FruitPurchase_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT FP.FruitPurchase_Id, FP.Cost, FP.Comments,FT.Description AS FT_Description, FV.Description AS FV_Description, " + 
                                                    "COUNT(*) as Bin_Count, SUM(B.Weight_Gross) as Gross_Weight, SUM(B.Weight_Tare)as Tare_Weight " +
                                            "FROM   dbo.PF_A_Fruit_Purchase FP " +
                                            "INNER JOIN dbo.GH_Submission S ON S.FruitPurchase_Id = FP.FruitPurchase_Id " +
                                            "INNER JOIN dbo.CM_Bins B ON B.Submission_Id = S.Submission_Id " +
                                            "INNER JOIN dbo.CM_Material M ON M.Material_Id = B.Material_Id " +
                                            "INNER JOIN dbo.CM_Fruit_Type FT ON FT.FruitType_Id = M.FruitType_Id " +
                                            "INNER JOIN dbo.CM_Fruit_Variety FV ON FV.Variety_Id = M.Variety_Id " +
                                            "WHERE FP.FruitPurchase_Id = " + FruitPurchase_Id + " " +
                                            "GROUP BY FP.FruitPurchase_Id, FP.Customer_Id, FP.Cost, FP.Comments, B.Material_Id,M.Material_Num,FT.Description, FV.Description" );
        }
        public static int Update(int FruitPurchase_Id, int Customer_Id, decimal Cost, string Comments, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_A_Fruit_Purchase SET Customer_Id = " + Customer_Id + ", " +
                                                                  "Cost = " + Cost + ", " +
                                                                  "Comments = '" + Comments + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE FruitPurchase_Id = " + FruitPurchase_Id);
        }
        public static int Update(int FruitPurchase_Id, string Date_to_Office, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_A_Fruit_Purchase SET Date_to_Office = '" + Date_to_Office + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE FruitPurchase_Id = " + FruitPurchase_Id);
        }
    }
}
