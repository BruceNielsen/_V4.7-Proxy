using FP.Utils.Data;
using System.Data;

namespace PF.Data.AccessLayer
{
    /*Description
    -----------------
    CM_ESP Class.
     *
     * This Class is a data access layer to the CM_ESP table
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

    public class PF_Work_Order
    {
        public static DataSet Get_Max_ID()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Work_Order_Id) as Current_Id FROM PF_Work_Order");
        }

        public static DataSet Get_Info()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Work_Order");
        }

        public static DataSet Get_Info(int Work_Order_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT WO.*, G.Rpin as Grower, T.Description as T_Description " +
                                            "FROM PF_Work_Order WO " +
                                            "INNER JOIN dbo.CM_Grower G ON WO.Grower_Id = G.Grower_Id " +
                                            "INNER JOIN dbo.CM_Trader T ON T.Trader_Id = WO.Trader_Id " +
                                            "WHERE Work_Order_Id = " + Work_Order_Id);
        }

        public static DataSet Check_Grower(int Grower_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Work_Order WHERE Grower_Id = " + Grower_Id);
        }

        public static DataSet Get_WO_For_SetUP(string str_join, string str_where)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT DISTINCT WO.*, G.Rpin as Grower, P.Code as Product, FT.Code as FruityType, FT.FruitType_Id, FV.Code + ' - ' + FV.Description as FruitVariety , FV.Variety_Id " +
                                            "FROM PF_Work_Order WO " +
                                            "INNER JOIN dbo.CM_Grower G ON WO.Grower_Id = G.Grower_Id " +
                                            "INNER JOIN dbo.PF_Product P ON P.Product_Id = WO.Product_Id " +
                                            "INNER JOIN dbo.CM_Fruit_Type FT ON FT.FruitType_Id = WO.Fruit_Type_Id " +
                                            "INNER JOIN dbo.CM_Fruit_Variety FV ON FV.Variety_Id = WO.Fruit_Variety_Id " +
                                            str_join + " " +
                                            str_where + " Order BY WO.Work_Order_Id DESC");
        }

        public static DataSet Get_Info_translated(int Work_Order_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT WO.*, G.Rpin as Grower, P.Code as Product, FT.Code as FruityType, FT.FruitType_Id, FV.Code + ' - ' + FV.Description as FruitVariety , FV.Variety_Id " +
                                            "FROM PF_Work_Order WO " +
                                            "INNER JOIN dbo.CM_Grower G ON WO.Grower_Id = G.Grower_Id " +
                                            "INNER JOIN dbo.PF_Product P ON P.Product_Id = WO.Product_Id " +
                                            "INNER JOIN dbo.CM_Fruit_Type FT ON FT.FruitType_Id = WO.Fruit_Type_Id " +
                                            "INNER JOIN dbo.CM_Fruit_Variety FV ON FV.Variety_Id = WO.Fruit_Variety_Id " +
                                            "WHERE Work_Order_Id = " + Work_Order_Id);
        }

        public static DataSet Get_Info_Exclude_Current(int Season)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Work_Order WO WHERE WO.Work_Order_Id NOT IN (SELECT BL.Work_Order_Id FROM dbo.PF_BaseLoad BL) AND Current_Ind = 0 AND WO.Season = " + Season + " ORDER BY Work_Order_Id DESC");
        }

        public static int Insert(int Work_Order_Id, decimal Season, string Process_Date, string Start_Time, string Finish_Time, int Trader_Id, int Product_Id,
                                 int Orchardist_Id, int Grower_Id, int Growing_Method_Id, int Fruit_Type_Id, int Fruit_Variety_Id, decimal Num_Batches, string Comments, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_Work_Order(Work_Order_Id, Season, Process_Date, Start_Time, Finish_Time,Trader_Id, " +
                                                                  "Product_Id, Orchardist_Id, Grower_Id, Growing_Method_Id, Fruit_Type_Id, Fruit_Variety_Id, " +
                                                                  "Num_Batches, Current_Ind, Comments, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + Work_Order_Id + "," + Season + ",'" + Process_Date + "','" + Start_Time + "','" + Finish_Time + "'," + Trader_Id + "," +
                                                              Product_Id + "," + Orchardist_Id + "," + Grower_Id + "," + Growing_Method_Id + "," + Fruit_Type_Id + "," + Fruit_Variety_Id + "," +
                                                              Num_Batches + ",'false','" + Comments + "', GETDATE() ," + Mod_User_Id + ")");
        }

        public static int Update_Completed(int Work_Order_Id, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_Work_Order SET Completed_Date = GETDATE(), Mod_Date = GETDATE(), " +
                                                                        "Mod_User_Id = " + Mod_User_Id +
                                               "WHERE Work_Order_Id = " + Work_Order_Id);
        }

        public static int Update_UnCompleted(int Work_Order_Id, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_Work_Order SET Completed_Date = null, Mod_Date = GETDATE(), " +
                                                                        "Mod_User_Id = " + Mod_User_Id +
                                               "WHERE Work_Order_Id = " + Work_Order_Id);
        }

        public static int Update_Current_Ind(int Work_Order_Id, bool Current_Ind, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_Work_Order SET Current_Ind = '" + Current_Ind + "', " +
                                                                        "Mod_Date = GETDATE(), " +
                                                                        "Mod_User_Id = " + Mod_User_Id +
                                               "WHERE Work_Order_Id = " + Work_Order_Id);
        }

        public static int Delete(int Work_Order_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE PF_Work_Order WHERE Work_Order_Id = " + Work_Order_Id);
        }

        public static DataSet Get_Combo_Prod_by_Date(string Processs_Date)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT DISTINCT P.Product_Id, P.Code FROM PF_Work_Order WO INNER JOIN PF_Product P ON P.Product_Id = WO.Product_Id " +
                                            "WHERE WO.Process_Date = '" + Processs_Date + "'");
        }

        public static DataSet Get_Combo()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Work_Order WO WHERE  WO.Completed_Date IS NULL ORDER BY Current_Ind Desc, Work_Order_Id DESC");
        }

        public static DataSet Get_Combo_Date()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT DISTINCT Process_Date FROM PF_Work_Order WO WHERE WO.Completed_Date IS NULL ORDER BY Process_Date DESC");
        }

        public static DataSet Get_Combo_Date(int Product_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT DISTINCT Process_Date FROM PF_Work_Order WO WHERE WO.Completed_Date IS NULL AND Product_Id = " + Product_Id + "ORDER BY Process_Date DESC");
        }

        public static DataSet Get_Combo_Work_Order()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Work_Order WO WHERE WO.Completed_Date IS NULL AND WO.Work_Order_Id NOT IN (SELECT BL.Work_Order_Id FROM dbo.PF_BaseLoad BL) ORDER BY Work_Order_Id DESC");
        }

        public static DataSet Get_Combo_Work_Order(string Process_Date)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Work_Order WO WHERE WO.Completed_Date IS NULL AND WO.Work_Order_Id NOT IN (SELECT BL.Work_Order_Id FROM dbo.PF_BaseLoad BL) AND Process_Date = '" + Process_Date + "' ORDER BY Work_Order_Id DESC");
        }

        public static DataSet Get_Combo_Work_Order(int Product_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Work_Order WO WHERE WO.Completed_Date IS NULL AND WO.Work_Order_Id NOT IN (SELECT BL.Work_Order_Id FROM dbo.PF_BaseLoad BL) AND Product_Id = " + Product_Id + " ORDER BY Work_Order_Id DESC");
        }

        public static DataSet Get_Combo_Work_Order(string Process_Date, int Product_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Work_Order WO WHERE WO.Completed_Date IS NULL AND WO.Work_Order_Id NOT IN (SELECT BL.Work_Order_Id FROM dbo.PF_BaseLoad BL) AND Process_Date = '" + Process_Date + "' AND Product_Id = " + Product_Id + " ORDER BY Work_Order_Id DESC");
        }

        public static int Update(int Work_Order_Id, decimal Season, string Process_Date, string Start_Time, string Finish_Time, int Trader_Id, int Product_Id,
                                 int Orchardist_Id, int Grower_Id, int Growing_Method_Id, int Fruit_Type_Id, int Fruit_Variety_Id, decimal Num_Batches, string Comments, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("Update PF_Work_Order SET Season = " + Season + ", " +
                                                                        "Process_Date = '" + Process_Date + "', " +
                                                                        "Start_Time = '" + Start_Time + "', " +
                                                                        "Finish_Time = '" + Finish_Time + "', " +
                                                                        "Trader_Id = " + Trader_Id + ", " +
                                                                        "Product_Id = " + Product_Id + ", " +
                                                                        "Orchardist_Id = " + Orchardist_Id + ", " +
                                                                        "Grower_Id = " + Grower_Id + ", " +
                                                                        "Growing_Method_Id = " + Growing_Method_Id + ", " +
                                                                        "Fruit_Type_Id = " + Fruit_Type_Id + ", " +
                                                                        "Fruit_Variety_Id = " + Fruit_Variety_Id + ", " +
                                                                        "Num_Batches = " + Num_Batches + ", " +
                                                                        "Comments = '" + Comments + "', " +
                                                                        "Mod_Date = GETDATE() ," +
                                                                        "Mod_User_Id = " + Mod_User_Id + " " +
                                                  "WHERE Work_Order_Id = " + Work_Order_Id);
        }

        public static DataSet Get_Cleaning(string Process_Date)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT DISTINCT WO.Product_Id, P.Code as P_Code, P.Description as P_Description, PCAR.CleanArea_Id, CA.Code as CA_Code, CA.Description as CA_Description " +
                                            "FROM dbo.PF_Work_Order WO " +
                                            "INNER JOIN dbo.PF_Product P ON P.Product_Id = WO.Product_Id " +
                                            "INNER JOIN dbo.PF_Product_Cleaning_Area_Relationship PCAR ON PCAR.Product_Id = WO.Product_Id " +
                                            "INNER JOIN dbo.PF_Cleaning_Area CA ON CA.CleanArea_Id = PCAR.CleanArea_Id " +
                                            "WHERE WO.Process_Date = '" + Process_Date + "'");
        }

        public static DataSet Get_UnInvoiced()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Work_Order WHERE Invoice_Id IS NULL ");
        }

        public static DataSet Get_Invoiced(int Invoice_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT *  FROM PF_Work_Order WO WHERE Invoice_Id = " + Invoice_Id);
        }

        public static DataSet Get_UnInvoiced_for_Trader(int Trader_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT *, P.Code as P_Code, P.Description as P_Description, FT.Code as FT_Code, FT.Description as FT_Description " +
                                            "FROM PF_Work_Order WO " +
                                            "INNER JOIN dbo.PF_Product P ON P.Product_Id = WO.Product_Id " +
                                            "INNER JOIN dbo.CM_Fruit_Type FT ON FT.FruitType_Id = WO.Fruit_Type_Id " +
                                            "WHERE Trader_Id = " + Trader_Id + " AND Invoice_Id IS NULL ");
        }

        public static DataSet Get_Invoice_Rate(string WHERE)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("Select distinct WO.Work_Order_Id, B.Material_Id, R.Rates_Id, R.Code, R.Description, R.Value " +
                                            "FROM dbo.PF_Work_Order WO " +
                                            "Inner join dbo.CM_Bins B on B.Work_Order_Id = WO.Work_Order_Id " +
                                            "LEFT OUTER JOIN dbo.CM_Material_Rates_Relationship MRR ON MRR.Material_Id = B.Material_Id " +
                                            "LEFT OUTER JOIN dbo.PF_A_Rates R ON R.Rates_Id = MRR.Rates_Id " +
                                            "WHERE WO.Work_Order_Id in " + WHERE);
        }

        public static DataSet Get_Staff_for_Work_Order(string WHERE)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT *, DATEDIFF(MINUTE,WO.Start_Time, WO.Finish_Time) + Extra_Hours * 60 as mins  " +
                                            "FROM dbo.PF_Work_Order WO " +
                                            "INNER JOIN dbo.PF_Work_Order_Staff_Relationship WOSR on WOSR.Work_Order_Id = WO.Work_Order_Id " +
                                            "INNER JOIN dbo.PF_Staff S ON S.Staff_Id = WOSR.Staff_Id " +
                                            "WHERE WO.Work_Order_Id in " + WHERE);
        }

        public static DataSet Get_Invoice_Rate(string WHERE, string CODE)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * " +
                                            "FROM dbo.PF_Work_Order WO " +
                                            "INNER JOIN dbo.PF_Work_Order_Material_Relationship WOMR ON WOMR.work_Order_Id = WO.Work_Order_Id " +
                                            "INNER JOIN dbo.CM_Material_Rates_Relationship MRR ON MRR.Material_Id = WOMR.Material_Id " +
                                            "INNER JOIN dbo.PF_A_Rates R ON R.Rates_Id = MRR.Rates_Id " +
                                            "WHERE WO.Work_Order_Id in " + WHERE + " AND R.Code like '" + CODE + "%'");
        }

        public static int Update_Invoiced(int Work_Order_Id, int Invoice_Id, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_Work_Order SET Invoice_Id = " + Invoice_Id + " ," +
                                                                        "Mod_Date = GETDATE() ," +
                                                                        "Mod_User_Id = " + Mod_User_Id + " " +
                                                                        "WHERE Work_Order_Id = " + Work_Order_Id);
        }

        #region ------------- Stored Procedures -------------

        #region ------------- Select -------------

        public static DataSet Get_Current()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.RunSP_Query("dbo.PF_Work_Order_Get_Current");
        }

        #endregion ------------- Select -------------

        #endregion ------------- Stored Procedures -------------
    }
}