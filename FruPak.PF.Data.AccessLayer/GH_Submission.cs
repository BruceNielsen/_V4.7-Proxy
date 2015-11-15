using FruPak.Utils.Data;
using System.Data;

namespace FruPak.PF.Data.AccessLayer
{
    /*Description
    -----------------
    GH_Submission Class.
     *
     * This Class is a data access layer to the GH_Submission table
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

    public class GH_Submission
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Submission_Id) as Current_Id FROM GH_Submission");
        }

        public static DataSet Get_Info()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM GH_Submission ORDER BY Submission_Id");
        }

        public static DataSet Get_for_Combo()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT Submission_Id, CONVERT(varchar, Submission_Id)+ ' - ' + SUBSTRING(Comments, 23,5) as Combined FROM GH_Submission  WHERE SUBSTRING(Comments, 1,3) = 'GDI' ORDER BY Submission_Id DESC");
        }

        public static DataSet Get_Info(int Submission_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM GH_Submission WHERE Submission_Id = " + Submission_Id);
        }

        public static DataSet Get_Info_Translated()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT S.Submission_Id, S.Trader_Id, S.Grower_Id, S.Block_Id, S.Grower_Reference, S.Sub_date, S.Harvest_date, S.Pick_Num, S.Comments, T.Code AS Trader, G.Rpin, " +
                                            "B.Code AS Block, COUNT(Bins.Bins_Id) AS NumBins, Min(Mat.FruitType_Id) as FruitType_Id, Min(Mat.Variety_Id) as Variety_Id, FT.Description AS FruitType, FV.Description as Variety " +
                                            "FROM [dbo].[GH_Submission] S " +
                                            "INNER JOIN dbo.CM_Grower G ON S.Grower_Id = G.Grower_Id " +
                                            "INNER JOIN dbo.CM_Trader T ON S.Trader_Id = T.Trader_Id " +
                                            "INNER JOIN dbo.CM_Block B ON S.Block_Id = B.Block_Id " +
                                            "LEFT JOIN dbo.CM_Bins Bins ON Bins.Submission_Id = S.Submission_Id " +
                                            "LEFT JOIN dbo.CM_Material Mat ON Bins.Material_Id = Mat.Material_Id " +
                                            "LEFT JOIN dbo.CM_Fruit_Type FT ON Mat.FruitType_Id = FT.FruitType_Id " +
                                            "LEFT JOIN dbo.CM_Fruit_Variety FV ON Mat.Variety_Id = FV.Variety_Id " +
                                            "GROUP BY S.Submission_Id, S.Trader_Id, S.Grower_Id, S.Block_Id, S.Grower_Reference, S.Sub_date, S.Harvest_date, S.Pick_Num, S.Comments, T.Code, G.Rpin, B.Code, FT.Description, FV.Description");
        }

        public static DataSet Get_Submission_Info(int Submission_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM dbo.[V_Submission_Details] WHERE Submission_Id = " + Submission_Id);
        }

        public static int Insert(int Submission_Id, int Trader_Id, int Grower_Id, int Block_Id, string Grower_Reference, string Sub_date, string Harvest_date, int Pick_Num, string Comments, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO GH_Submission(Submission_Id, Trader_Id, Grower_Id, Block_Id, Grower_Reference, Sub_date, Harvest_date, Pick_Num, Comments, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + Submission_Id + "," + Trader_Id + "," + Grower_Id + "," + Block_Id + ",'" + Grower_Reference + "','" + Sub_date + "','" + Harvest_date + "'," + Pick_Num + ",'" + Comments + "', GETDATE()," + Mod_User_Id + ")");
        }

        public static int Delete(int Submission_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM GH_Submission WHERE Submission_Id = " + Submission_Id);
        }

        public static int Update(int Submission_Id, int Trader_Id, int Grower_Id, int Block_Id, string Grower_Reference, string Sub_date, string Harvest_date, int Pick_Num, string Comments, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE GH_Submission SET Trader_Id = " + Trader_Id + ", " +
                                                                  "Grower_Id = " + Grower_Id + ", " +
                                                                  "Block_Id = " + Block_Id + ", " +
                                                                  "Grower_Reference = '" + Grower_Reference + "', " +
                                                                  "Sub_date = '" + Sub_date + "', " +
                                                                  "Harvest_date = '" + Harvest_date + "', " +
                                                                  "Pick_Num = " + Pick_Num + ", " +
                                                                  "Comments = '" + Comments + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Submission_Id = " + Submission_Id);
        }

        public static int Update_Purchase(int Submission_Id, int FruitPurchase_Id, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE GH_Submission SET FruitPurchase_Id = " + FruitPurchase_Id + ", " +
                                                                      "Mod_date = GETDATE(), " +
                                                                      "Mod_User_Id = " + Mod_User_Id +
                                                  " WHERE Submission_Id = " + Submission_Id);
        }

        public static int Reset_Purchase(int FruitPurchase_Id, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE GH_Submission SET FruitPurchase_Id = null , " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE FruitPurchase_Id = " + FruitPurchase_Id);
        }

        public static DataSet Get_Fruit_NOT_Purchased(int Customer_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT S.Submission_Id, S.Sub_date, S.FruitPurchase_Id, B.Material_Id,M.Material_Num,FT.Description AS FT_Description, FV.Description AS FV_Description,CSR.Value, " +
                                                    "COUNT(*) as Bin_Count, SUM(Weight_Gross) as Gross_Weight, SUM(Weight_Tare)as Tare_Weight " +
                                            "FROM dbo.GH_Submission S " +
                                            "INNER JOIN dbo.CM_Bins B on B.Submission_Id = S.Submission_Id " +
                                            "INNER JOIN dbo.CM_Trader T ON T.Trader_Id = S.Trader_Id " +
                                            "INNER JOIN dbo.CM_Material M ON M.Material_Id = B.Material_Id " +
                                            "INNER JOIN dbo.CM_Fruit_Type FT ON FT.FruitType_Id = M.FruitType_Id " +
                                            "INNER JOIN dbo.CM_Fruit_Variety FV ON FV.Variety_Id = M.Variety_Id " +
                                            "LEFT OUTER JOIN dbo.PF_A_Customer_Sales_Rates CSR ON CSR.Customer_Id = T.Customer_Id AND CSR.Material_Id = B.Material_Id " +
                                            "WHERE T.Customer_Id = " + Customer_Id + " AND FruitPurchase_Id is NULL " +
                                            "GROUP BY S.Submission_Id, S.Sub_date,S.FruitPurchase_Id, B.Material_Id,M.Material_Num,FT.Description, FV.Description,CSR.Value " +
                                            "ORDER BY S.Submission_Id DESC ");
        }

        public static DataSet Get_ALL_Fruit_For_Customer(int Customer_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT S.Submission_Id, S.Sub_date,S.FruitPurchase_Id, B.Material_Id,M.Material_Num,FT.Description AS FT_Description, FV.Description AS FV_Description,CSR.Value, " +
                                                    "COUNT(*) as Bin_Count, SUM(Weight_Gross) as Gross_Weight, SUM(Weight_Tare)as Tare_Weight " +
                                            "FROM dbo.GH_Submission S " +
                                            "INNER JOIN dbo.CM_Bins B on B.Submission_Id = S.Submission_Id " +
                                            "INNER JOIN dbo.CM_Trader T ON T.Trader_Id = S.Trader_Id " +
                                            "INNER JOIN dbo.CM_Material M ON M.Material_Id = B.Material_Id " +
                                            "INNER JOIN dbo.CM_Fruit_Type FT ON FT.FruitType_Id = M.FruitType_Id " +
                                            "INNER JOIN dbo.CM_Fruit_Variety FV ON FV.Variety_Id = M.Variety_Id " +
                                            "LEFT OUTER JOIN dbo.PF_A_Customer_Sales_Rates CSR ON CSR.Customer_Id = T.Customer_Id AND CSR.Material_Id = B.Material_Id " +
                                            "WHERE T.Customer_Id = " + Customer_Id +
                                            "GROUP BY S.Submission_Id, S.Sub_date,S.FruitPurchase_Id, B.Material_Id,M.Material_Num,FT.Description, FV.Description,CSR.Value " +
                                            "ORDER BY S.Submission_Id DESC ");
        }
    }
}