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
    CM_Material Class.
     * 
     * This Class is a data access layer to the CM_Material table
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
    public class CM_Material
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Material_Id) as Current_Id FROM CM_Material");
        }
        public static DataSet Get_Max_Material_Num(int min, int max )
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Material_Num) as Max  FROM CM_Material WHERE Material_Num BETWEEN " + min + " AND " + max);
        }
        public static DataSet Get_Info()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM CM_Material WHERE PF_Active_Ind = 1 ORDER BY Material_Num");
        }
        public static DataSet Get_Info(decimal Material_Num)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM CM_Material  WHERE PF_Active_Ind = 1 AND Material_Num = " + Material_Num);
        }
        public static DataSet Check_Mat_num_Trader(decimal Material_Num, int Trader_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM CM_Material  WHERE PF_Active_Ind = 1 AND Material_Num = " + Material_Num + " AND Trader_Id = " + Trader_Id);
        }
        public static DataSet Does_It_Exist(int Brand_Id, int Count_Id, int FruitType_Id, int Grade_Id, int Growing_Method_Id, int Market_Attribute_Id, int PackType_Id,
                                            int PalletType_Id, int ProductGroup_Id, int Size_Id, int Treatment_Id, int Variety_Id, int Trader_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT M.Material_Num, M.Description as M_Description, T.Description as T_Description, M.Weight FROM dbo.CM_Material M " +
                                            "INNER JOIN dbo.CM_Trader T on T.Trader_Id = M.Trader_Id " +
                                            "WHERE M.Brand_Id = " + Brand_Id + " AND M.Count_Id = " + Count_Id + " AND M.FruitType_Id = " + FruitType_Id + " AND M.Grade_Id = " + Grade_Id + " " +
                                            "AND M.Growing_Method_Id = " + Growing_Method_Id + " AND M.Market_Attribute_Id = " + Market_Attribute_Id + " AND M.PackType_Id = " + PackType_Id + " " +
                                            "AND M.PalletType_Id = " + PalletType_Id + " AND M.ProductGroup_Id = " + ProductGroup_Id + " AND M.Size_Id = " + Size_Id + " " +
                                            "AND M.Treatment_Id = " + Treatment_Id + " AND M.Variety_Id = " + Variety_Id + " AND M.Trader_Id = " + Trader_Id);
        }
        public static DataSet Get_Info(int Material_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM CM_Material  WHERE PF_Active_Ind = 1 AND Material_Id = " + Material_Id);
        }
        public static DataSet Get_Info(int Trader_Id, int FruitType_Id, int Variety_Id, int PackType_Id, int Size_Id, int Count_Id,
                                 int Brand_Id, int Grade_Id, int Treatment_Id, int Market_Attribute_Id, int Growing_Method_Id, int ProductGroup_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT Material_Id FROM CM_Material WHERE PF_Active_Ind = 1 AND Trader_Id = " + Trader_Id +
                                            " AND FruitType_Id = " + FruitType_Id + " AND Variety_Id = " + Variety_Id +
                                            " AND PackType_Id = " + PackType_Id + " AND Size_Id = " + Size_Id +
                                            " AND Count_Id = " + Count_Id + " AND Brand_Id = " + Brand_Id +
                                            " AND Grade_Id = " + Grade_Id + " AND Treatment_Id = " + Treatment_Id +
                                            " AND Market_Attribute_Id = " + Market_Attribute_Id + " AND Growing_Method_Id = " + Growing_Method_Id +
                                            " AND ProductGroup_Id = " + ProductGroup_Id);
        }
        public static int insert(int Material_Id, decimal Material_Num, int Trader_Id, int FruitType_Id, int Variety_Id, int PackType_Id, int Size_Id, int Count_Id, 
                                 int Brand_Id, int Grade_Id, int Treatment_Id, int Market_Attribute_Id, int Growing_Method_Id, int ProductGroup_Id, decimal Weight,
                                 string Description, bool PF_Active_Ind, int Mod_User_Id, int PalletType_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO CM_Material(Material_Id, Material_Num, Trader_Id, FruitType_Id, Variety_Id, PackType_Id, Size_Id, Count_Id, Brand_Id, Grade_Id, Treatment_Id," +
                                                                       "Market_Attribute_Id, Growing_Method_Id, ProductGroup_Id, Weight, Description, PF_Active_Ind, Mod_Date, Mod_User_Id, PalletType_Id) " +
                                                "VALUES ( " + Material_Id + "," + Material_Num + "," + Trader_Id + "," + FruitType_Id + "," + 
                                                Variety_Id + "," + PackType_Id + "," + Size_Id + "," + Count_Id + "," + Brand_Id + "," + Grade_Id + "," + Treatment_Id + "," +
                                                Market_Attribute_Id + "," + Growing_Method_Id + "," + ProductGroup_Id + "," + Weight + ",'" + Description + "','" + PF_Active_Ind + "', GETDATE()," + Mod_User_Id + "," + PalletType_Id + ")");
        }
        public static int update_active(int Material_Id, bool PF_Active_Ind, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE CM_Material SET PF_Active_Ind = '" + PF_Active_Ind + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Material_Id = " + Material_Id);

        }
        public static DataSet Get_Info_By_Translated()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT M.Material_Id, M.Material_Num, " +
                                                   "M.Trader_Id, T.Code AS Trader, T.Code + ' - ' + T.Description AS T_Combined, " +
                                                   "M.ProductGroup_Id, PG.Code AS ProductGroup, PG.Code + ' - ' + PG.Description AS PG_Combined, " +
                                                   "M.PackType_Id, PT.Code as PackType, PT.Code  + ' - ' + PT.Description as PT_Combined, " +
                                                   "M.Grade_Id, G.Code as Grade, G.Code  + ' - ' + G.Description as G_Combined, " +
                                                   "M.Brand_Id, B.Code AS Brand, B.Code + ' - ' + B.Description AS B_Combined, " +
                                                   "M.Treatment_Id, TM.Code AS Treatment, TM.Code + ' - ' + TM.Description AS TM_Combined, " +
                                                   "M.Size_Id, S.Code AS Size, S.Code + ' - ' + S.Description AS S_Combined, " +
                                                   "M.Weight, " +
                                                   "M.Variety_ID, " +
                                                   "M.Growing_Method_ID " +
                                            "FROM dbo.CM_Material M " +
                                            "INNER JOIN dbo.CM_Trader T ON M.Trader_Id = T.Trader_Id " +
                                            "INNER JOIN dbo.CM_Product_Group PG ON M.ProductGroup_Id = PG.ProductGroup_Id " +
                                            "INNER JOIN dbo.CM_Pack_Type PT ON M.PackType_Id = PT.PackType_Id " +
                                            "INNER JOIN dbo.CM_Grade G ON M.Grade_Id = G.Grade_Id " +
                                            "INNER JOIN dbo.CM_Brand B ON M.Brand_Id = B.Brand_Id " +
                                            "INNER JOIN dbo.CM_Treatment TM ON M.Treatment_Id = TM.Treatment_Id " +
                                            "INNER JOIN dbo.CM_Size S ON M.Size_Id = S.Size_Id WHERE M.PF_Active_Ind = 1");
        }
        public static DataSet Get_Info_By_Translated(int Material_Id )
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT M.Material_Id, M.Material_Num, " +
                                                   "M.Trader_Id, T.Code AS Trader, T.Code + ' - ' + T.Description AS T_Combined, " +
                                                   "M.ProductGroup_Id, PG.Code AS ProductGroup, PG.Code + ' - ' + PG.Description AS PG_Combined, " +
                                                   "M.PackType_Id, PT.Code as PackType, PT.Code  + ' - ' + PT.Description as PT_Combined, " +
                                                   "M.Brand_Id, B.Code AS Brand, B.Code + ' - ' + B.Description AS B_Combined, " +
                                                   "M.Weight " +
                                            "FROM dbo.CM_Material M " +
                                            "INNER JOIN dbo.CM_Trader T ON M.Trader_Id = T.Trader_Id " +
                                            "INNER JOIN dbo.CM_Product_Group PG ON M.ProductGroup_Id = PG.ProductGroup_Id " +
                                            "INNER JOIN dbo.CM_Pack_Type PT ON M.PackType_Id = PT.PackType_Id " +
                                            "INNER JOIN dbo.CM_Brand B ON M.Brand_Id = B.Brand_Id " +
                                            "WHERE M.PF_Active_Ind = 1 AND M.Material_Id = " + Material_Id);
        }
        public static DataSet Get_For_Combo_For_Customer()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT M.Material_Id, CAST(M.Material_Num as varchar) + ' - ' + M.Description as Combined " +
                                            "FROM dbo.CM_Material M " +
                                            "INNER JOIN dbo.CM_Trader T on T.Trader_Id = m.Trader_Id " +
                                            "WHERE M.PF_Active_Ind = 1 " +
                                            "ORDER BY M.Material_Num ");
        }
        public static DataSet Get_For_Combo_For_Customer(int Customer_Id )
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT M.Material_Id, CAST(M.Material_Num as varchar) + ' - ' + M.Description as Combined " +
                                            "FROM dbo.CM_Material M "+
                                            "INNER JOIN dbo.CM_Trader T on T.Trader_Id = m.Trader_Id "+
                                            "WHERE T.Customer_Id = " + Customer_Id + 
                                            " ORDER BY M.Material_Num " );
        }
        public static DataSet Get_For_Combo_by_fruit(int FruitType_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT M.Material_Id, CAST(M.Material_Num as varchar) + ' - ' + M.Description as Combined " +
                                            "FROM dbo.CM_Material M "+
                                            "INNER JOIN dbo.CM_Trader T on T.Trader_Id = m.Trader_Id  " +
                                            "WHERE M.Material_Num > 59999999 AND M.FruitType_Id = " + FruitType_Id + " " +
                                            "ORDER BY M.Material_Num ");
        }

        #region ------------- Stored Procedures -------------
            #region ------------- Select -------------
                public static DataSet Get_Repack_List(string Barcode)
                {
                    FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
                    return SQLAccessLayer.RunSP_Query("dbo.CM_Material_Get_Repack_List", Barcode);
                }
                public static DataSet Get_Material_for_Prod_Grp(string PG_Code, int int_Fruit_Type_Id, int int_Fruit_Variety_Id)
                {
                    FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
                    return SQLAccessLayer.RunSP_Query("dbo.CM_Material_Get_Material_for_Prod_Grp", PG_Code, int_Fruit_Type_Id, int_Fruit_Variety_Id);
                }
            #endregion
        #endregion
    }
}

