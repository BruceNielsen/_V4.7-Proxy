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
    PF_Pallet Class.
     * 
     * This Class is a data access layer to the PF_Pallet table
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
    public class PF_Pallet
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Pallet_Id) as Current_Id FROM dbo.PF_Pallet");
        }
        public static DataSet Get_Info()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM dbo.PF_Pallet WHERE PF_Active_Ind = 1");
        }

        public static DataSet Get_Info_distinct_order(int Order_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM dbo.PF_Pallet WHERE Order_Id = " + Order_Id);
        }
        public static DataSet Get_Info_Translated(int Work_Order_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT DISTINCT P.*, PT.Code + ' - ' + PT.Description AS PT_Combined " +
                                                      ",L.Code AS L_Code, L.Code + ' - ' + L.Description as L_Combined " +
                                                      ",T.Code AS T_Code, T.Code + ' - ' + T.Description as T_Combined " +
                                            "FROM dbo.PF_Pallet P " +
                                            "INNER JOIN dbo.CM_Pallet_Type PT ON PT.PalletType_Id = P.PalletType_Id " +
                                            "INNER JOIN dbo.CM_Location L ON L.Location_Id = P.Location_Id " +
                                            "INNER JOIN dbo.CM_Trader T ON T.Trader_Id = P.Trader_Id " +
                                            "INNER JOIN dbo.PF_Pallet_Details PD ON PD.Pallet_Id = P.Pallet_Id " +
                                            "WHERE P.PF_Active_Ind = 1 AND PD.Work_Order_Id = " + Work_Order_Id + " " +
                                            "ORDER BY P.Pallet_Id DESC");
        }

        public static DataSet Get_Info_for_Packing_Slips(int Order_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT G.Description G_Description,FT.Description AS FT_Description, FV.Description AS FV_Description, GM.Growing_Method_Id, " +
                                                    "GM.Description AS GM_Description, S.Description AS S_Description, SUM(PD.Quantity) as Quantity " +
                                            "FROM dbo.PF_Pallet P " +
                                            "INNER JOIN dbo.PF_Pallet_Details PD ON PD.Pallet_Id = P.Pallet_Id " +
                                            "INNER JOIN dbo.CM_Material M ON M.Material_Id = PD.Material_Id " +
                                            "INNER JOIN dbo.CM_Grade G ON G.Grade_Id = M.Grade_Id " +
                                            "INNER JOIN dbo.CM_Fruit_Type FT ON FT.FruitType_Id = M.FruitType_Id " +
                                            "INNER JOIN dbo.CM_Fruit_Variety FV ON FV.Variety_Id = M.Variety_Id " +
                                            "INNER JOIN dbo.CM_Growing_Method GM ON GM.Growing_Method_Id = M.Growing_Method_Id " +
                                            "INNER JOIN dbo.CM_Size S ON S.Size_Id = M.Size_Id " +
                                            "WHERE P.Order_Id = " + Order_Id + " " +
                                            "GROUP BY P.Order_Id,M.Material_Num, G.Description,FT.Description,FV.Description,GM.Growing_Method_Id,GM.Description,S.Description");
        }
        public static DataSet Get_Info_for_Order(int Order_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT  P.Barcode, P.PCardPrinted_Ind " +
                                            "FROM dbo.PF_Pallet P " +
                                            "WHERE P.Order_Id = " + Order_Id );
        }
        public static DataSet Get_Info_for_Order(int Order_Id, int Product_Id, int FruitType_id, int Variety_id )
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT P.*,PD.Batch_Num, PD.Quantity as Quantity, Pr.Code as Product_Code, Pr.Description as Product, WO.Fruit_Type_Id, WO.Fruit_Variety_Id " +
                                            "FROM dbo.PF_Pallet P " +
                                            "INNER JOIN dbo.PF_Pallet_Details PD ON PD.Pallet_Id = P.Pallet_Id " +
                                            "INNER JOIN dbo.PF_Work_Order WO ON WO.Work_Order_Id = PD.Work_Order_Id " +
                                            "INNER JOIN dbo.PF_Product Pr ON Pr.Product_Id = WO.Product_Id " +
                                            "WHERE P.Order_Id = " + Order_Id + " AND Pr.Product_Id =" + Product_Id + " AND WO.Fruit_Type_Id = " + FruitType_id + 
                                            " AND WO.Fruit_Variety_Id = " + Variety_id + " AND PD.Quantity > 0");
        }
        public static DataSet Get_Info_for_Order(int Order_Id, int Pallet_Id, int Product_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT P.*,PD.Batch_Num, PD.Quantity as Quantity, Pr.Code as Product_Code, Pr.Description as Product, WO.Fruit_Type_Id, WO.Fruit_Variety_Id " +
                                            "FROM dbo.PF_Pallet P " +
                                            "INNER JOIN dbo.PF_Pallet_Details PD ON PD.Pallet_Id = P.Pallet_Id " +
                                            "INNER JOIN dbo.PF_Work_Order WO ON WO.Work_Order_Id = PD.Work_Order_Id " +
                                            "INNER JOIN dbo.PF_Product Pr ON Pr.Product_Id = WO.Product_Id " +
                                            "WHERE P.Order_Id = " + Order_Id + " AND P.Pallet_id = " + Pallet_Id + " AND Pr.Product_Id =" + Product_Id);
        }
        public static DataSet Get_Info_for_Order(string str_WHERE)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT SUM(PD.Quantity) as Quantity, M.Material_Id, M.Material_Num, M.Description as M_Description " +
                                            "FROM  dbo.PF_Pallet P " +
                                            "INNER JOIN  dbo.PF_Pallet_Details PD ON PD.Pallet_Id = P.Pallet_Id " +
                                            "INNER JOIN  dbo.CM_Material M ON PD.Material_Id = M.Material_Id " +
                                            "WHERE P.Order_Id in " + str_WHERE + " " +
                                            "GROUP BY M.Material_Id, M.Material_Num, M.Description ");
        }
        public static DataSet Get_Info_for_Order_Pallet(int Order_Id, int Pallet_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT P.*,PD.Batch_Num, PD.Quantity as Quantity, Pr.Description as Product, Pr.Product_Id AS Product_Id, FT.Description as FT_Description, FV.Description as FV_Description " +
                                            "FROM dbo.PF_Pallet P " +
                                            "INNER JOIN dbo.PF_Pallet_Details PD ON PD.Pallet_Id = P.Pallet_Id " +
                                            "INNER JOIN dbo.PF_Work_Order WO ON WO.Work_Order_Id = PD.Work_Order_Id " +
                                            "INNER JOIN dbo.PF_Product Pr ON Pr.Product_Id = WO.Product_Id " +
                                            "INNER JOIN dbo.CM_Fruit_Type FT ON FT.FruitType_Id = WO.Fruit_Type_Id " +
                                            "INNER JOIN dbo.CM_Fruit_Variety FV ON FV.Variety_Id = WO.Fruit_Variety_Id " +
                                            "WHERE P.Order_Id = " + Order_Id + " AND P.Pallet_Id =" + Pallet_Id);
        }
        public static DataSet Get_Info_for_Order_prod(int Order_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT distinct Pr.Description as Product, Pr.Product_Id AS Product_Id, FT.FruitType_Id, FT.Description as FruitType, FV.Variety_Id,FV.Description as Varity, S.Description as Size " +
                                            "FROM dbo.PF_Pallet P " +
                                            "INNER JOIN  dbo.PF_Pallet_Details PD ON PD.Pallet_Id = P.Pallet_Id " +
                                            "INNER JOIN  dbo.PF_Work_Order WO ON WO.Work_Order_Id = PD.Work_Order_Id " +
                                            "INNER JOIN  dbo.PF_Product Pr ON Pr.Product_Id = WO.Product_Id " +
                                            "INNER JOIN dbo.CM_Fruit_Type FT ON FT.FruitType_Id = WO.Fruit_Type_Id " +
                                            "INNER JOIN dbo.CM_Fruit_Variety FV ON FV.Variety_Id = WO.Fruit_Variety_Id " +
                                            "INNER JOIN dbo.CM_Material M ON M.Material_Id = PD.Material_Id " +
                                            "INNER JOIN dbo.CM_Size S ON S.Size_Id = M.Size_Id " +
                                            "WHERE P.Order_Id = " + Order_Id);
        }
        public static int Insert(int Pallet_Id, int PalletType_Id, string Barcode, int Location_Id, int Trader_Id, bool PF_Active_Ind, int Season, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO  dbo.PF_Pallet(Pallet_Id, PalletType_Id, Barcode, Location_Id, Trader_Id, PF_Active_Ind, Season, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + Pallet_Id + "," + PalletType_Id + ",'" + Barcode + "'," + Location_Id + "," + Trader_Id + ",'" + PF_Active_Ind + "'," + Season + ", GETDATE()," + Mod_User_Id + ")");
        }
        public static int Insert(int Pallet_Id, int PalletType_Id, string Barcode, int Location_Id, int Trader_Id, int Order_Id, bool PF_Active_Ind, int Season, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO  dbo.PF_Pallet(Pallet_Id, PalletType_Id, Barcode, Location_Id, Trader_Id, Order_Id,   PF_Active_Ind, Season, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + Pallet_Id + "," + PalletType_Id + ",'" + Barcode + "'," + Location_Id + "," + Trader_Id + "," + Order_Id + ",'" + PF_Active_Ind + "'," + Season + ", GETDATE()," + Mod_User_Id + ")");
        }
        public static int Insert_On_Hold(int Pallet_Id, int PalletType_Id, string Barcode, int Location_Id, int Trader_Id, int Hold_For_Order_Id, bool PF_Active_Ind, int Season, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO  dbo.PF_Pallet(Pallet_Id, PalletType_Id, Barcode, Location_Id, Trader_Id, Hold_For_Order_Id,   PF_Active_Ind, Season, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + Pallet_Id + "," + PalletType_Id + ",'" + Barcode + "'," + Location_Id + "," + Trader_Id + "," + Hold_For_Order_Id + ",'" + PF_Active_Ind + "'," + Season + ", GETDATE()," + Mod_User_Id + ")");
        }
        public static int Insert(int Pallet_Id, int PalletType_Id, string Barcode, int Location_Id, int Trader_Id, bool PF_Active_Ind, int Season, int Mod_User_Id, int Pallet_Number, decimal Weight_Gross, decimal Weight_Tare)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO  dbo.PF_Pallet(Pallet_Id, PalletType_Id, Barcode, Location_Id, Trader_Id, PF_Active_Ind, Season, Mod_Date, Mod_User_Id, Pallet_Number, Weight_Gross, Weight_Tare) " +
                                                "VALUES ( " + Pallet_Id + "," + PalletType_Id + ",'" + Barcode + "'," + Location_Id + "," + Trader_Id + ",'" + PF_Active_Ind + "'," + Season + ", GETDATE()," + Mod_User_Id + ", " + Pallet_Number + ", " + Weight_Gross + ", " + Weight_Tare + ")");
        }
        public static int Delete(int Pallet_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM  dbo.PF_Pallet WHERE Pallet_Id = " + Pallet_Id);
        }
        public static int Update(int Pallet_Id, int PalletType_Id, int Location_Id, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE  dbo.PF_Pallet SET PalletType_Id = " + PalletType_Id + ", " +
                                                                  "Location_Id = " + Location_Id + ", " +                                                                  
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Pallet_Id = " + Pallet_Id);
        }
        public static int Update_remove_from_Order(int Pallet_Id, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE  dbo.PF_Pallet SET Order_Id = null, " +
                                                                  "PF_Active_Ind = 1, " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Pallet_Id = " + Pallet_Id);
        }
        public static int Update_For_PCard(int Pallet_Id, bool PCardPrinted_Ind, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE  dbo.PF_Pallet SET PCardPrinted_Ind = '" + PCardPrinted_Ind + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Pallet_Id = " + Pallet_Id);
        }
        public static int Update_Active_status(int Pallet_Id, bool PF_Active_Ind, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE  dbo.PF_Pallet SET PF_Active_Ind = '" + PF_Active_Ind+ "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Pallet_Id = " + Pallet_Id);
        }
        public static int Update_For_PalletType(int Pallet_Id, int PalletType_Id, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE  dbo.PF_Pallet SET PalletType_Id = '" + PalletType_Id + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Pallet_Id = " + Pallet_Id);
        }
        public static int Update_Gross_Weight(int Pallet_Id, decimal Weight_Gross, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE	dbo.PF_Pallet " +
	                                            "SET Weight_Gross = " + Weight_Gross + " ," +
		                                            "Mod_date = GETDATE(), " +
		                                            "Mod_User_Id = " + Mod_User_Id + 
	                                            "WHERE Pallet_Id = " +Pallet_Id);
        }
        public static int Update_Tare_Weight(int Pallet_Id, decimal Weight_Tare, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE	dbo.PF_Pallet " +
                                                "SET Weight_Tare = " + Weight_Tare + " ," +
                                                    "Mod_date = GETDATE(), " +
                                                    "Mod_User_Id = " + Mod_User_Id +
                                                "WHERE Pallet_Id = " + Pallet_Id);
        }
        public static int Update_Old_Pallet_Id(int Pallet_Id, int Old_Pallet_Id, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE  dbo.PF_Pallet SET Old_Pallet_Id = '" + Old_Pallet_Id + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Pallet_Id = " + Pallet_Id);
        }
        public static DataSet Get_Invoiced(int Invoice_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT AID.Quantity as Quantity, G.Description + ' ' + FT.Description as G_Description, AID.Cost, O.Customer_Order, AID.Rates_Id, R.Code, R.Description as R_Description " +
                                            "FROM  dbo.PF_A_Invoice AI " +
                                            "INNER JOIN  dbo.PF_A_Invoice_Details AID ON AID.Invoice_Id = AI.Invoice_Id " +
                                            "INNER JOIN  dbo.PF_Orders O ON O.Order_Id = AID.Order_Num " +
                                            "INNER JOIN dbo.CM_Material M ON M.Material_Id = AID.Material_Id " +
                                            "INNER JOIN dbo.CM_Grade G ON G.Grade_Id = M.Grade_Id " +
                                            "INNER JOIN dbo.CM_Fruit_Type FT ON FT.FruitType_Id = M.FruitType_Id " +
                                            "left outer join dbo.PF_A_Rates R on R.Rates_Id = AID.Rates_Id "+
                                            "WHERE AI.Invoice_Id = " + Invoice_Id );
        }
        public static DataSet Get_PalletCard(string Barcode)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT DISTINCT P.Barcode, PD.Work_Order_Id, PD.Material_Id, WO.Process_Date, wo.Fruit_Type_Id, wo.Product_Id, wo.Fruit_Variety_Id " +
                                            "FROM dbo.PF_Pallet P " +
                                            "INNER JOIN dbo.PF_Pallet_Details PD ON PD.Pallet_Id = P.Pallet_Id " +
                                            "INNER JOIN dbo.PF_Work_Order WO ON WO.Work_Order_Id = PD.Work_Order_Id " +
                                            "WHERE P.Barcode = '" + Barcode + "'");
        }
        public static DataSet Get_Max_Pallet_Numberd(int Work_Order_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("select MAX(P.Pallet_Number) as Pallet_Num " +
                                            "FROM dbo.PF_Pallet P " +
                                            "INNER JOIN dbo.PF_Pallet_Details PD on PD.Pallet_Id = P.Pallet_Id " +
                                            "WHERE PD.Work_Order_Id = " + Work_Order_Id);
        }
        public static DataSet Get_Pallet_barocde(int Work_Order_Id, int Pallet_Number)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("select P.Pallet_Id, P.Barcode " +
                                            "FROM dbo.PF_Pallet P " +
                                            "INNER JOIN dbo.PF_Pallet_Details PD on PD.Pallet_Id = P.Pallet_Id " +
                                            "WHERE PD.Work_Order_Id = " + Work_Order_Id + " AND P.Pallet_Number = " + Pallet_Number);
        }
        public static DataSet Get_WPCCard(string Barcode)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT FT.Description as FT_Description, FV.Description as FV_Description, P.Mod_Date,P.Pallet_Number, " +
                                                   "P.Weight_Gross, P.Weight_Gross - P.Weight_Tare AS Weight_Nett, " +
                                                    "(SELECT P.Mod_Date " +
                                                     "FROM dbo.PF_Pallet P " +
                                                     "INNER JOIN dbo.PF_Pallet_Details PD ON PD.Pallet_Id = P.Pallet_Id " +
                                                     "WHERE PD.Work_Order_Id = ( SELECT PD.Work_Order_Id " +
							                                                    "FROM dbo.PF_Pallet P " +
							                                                    "INNER JOIN dbo.PF_Pallet_Details PD ON PD.Pallet_Id = P.Pallet_Id " +
							                                                    "WHERE P.Barcode = '" + Barcode + "') " +
                                                     "AND P.Pallet_Number = ( SELECT P.Pallet_Number-1 " +
							                                                 "FROM dbo.PF_Pallet P " +
							                                                 "INNER JOIN dbo.PF_Pallet_Details PD ON PD.Pallet_Id = P.Pallet_Id " +
							                                                 "WHERE P.Barcode = '" + Barcode + "') " +
                                                     ") as Start_Time, WO.Start_Time as WO_Start_Time " +
                                            "FROM dbo.PF_Pallet P " +
                                            "INNER JOIN dbo.PF_Pallet_Details PD ON PD.Pallet_Id = P.Pallet_Id " +
                                            "INNER JOIN dbo.PF_Work_Order WO ON WO.Work_Order_Id = PD.Work_Order_Id " +
                                            "INNER JOIN dbo.CM_Fruit_Type FT ON FT.FruitType_Id = WO.Fruit_Type_Id " +
                                            "INNER JOIN dbo.CM_Fruit_Variety FV ON FV.Variety_Id = WO.Fruit_Variety_Id " +
                                            "WHERE P.Barcode = '" + Barcode + "'");
        }
        #region ------------- Stored Procedures -------------
            #region ------------- Select -------------
                public static DataSet Get_Barcode_For_Order(int Order_Id)
                {
                    FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
                    return SQLAccessLayer.RunSP_Query("dbo.PF_Pallet_Get_Barcode_For_Order", Order_Id);
                }
                public static DataSet Get_Info_for_Barcode(string barcode)
                {
                    FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
                    return SQLAccessLayer.RunSP_Query("dbo.PF_Pallet_Get_Info_for_Barcode", barcode);
                }
                public static DataSet Get_Info_For_Pallet_Id(int Pallet_Id)
                {
                    FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
                    return SQLAccessLayer.RunSP_Query("dbo.PF_Pallet_Get_Info_For_Pallet_Id", Pallet_Id);
                }
                public static DataSet Get_Pallet_Id_From_Baracode(string Barcode)
                {
                    FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
                    return SQLAccessLayer.RunSP_Query("dbo.PF_Pallet_Get_Pallet_Id_From_Baracode", Barcode);
                }
                public static DataSet Get_Details_For_Barcode_BatchNum(string Barcode, int Batch_Num, int Quantity)
                {
                    FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
                    return SQLAccessLayer.RunSP_Query("dbo.PF_Pallet_Get_Details_For_Barcode_BatchNum", Barcode, Batch_Num, Quantity);
                }
                public static DataSet Get_Material_info(string Barcode)
                {
                    FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
                    return SQLAccessLayer.RunSP_Query("dbo.PF_Pallet_Get_Material_info", Barcode);
                }
            #endregion
            #region ------------- Updates -------------
                public static int Update_Order_Id(int Pallet_Id, int Order_Id, int Mod_User_Id)
                {
                    FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
                    object ret_val;

                    SQLAccessLayer.RunSP_NonQuery("dbo.PF_Pallet_Update_Order_Id", out ret_val, Pallet_Id, Order_Id, Mod_User_Id);

                    //get the return value
                    var nameOfProperty = "Value";
                    var propertyInfo = ret_val.GetType().GetProperty(nameOfProperty);
                    var return_value = propertyInfo.GetValue(ret_val, null);

                    return Convert.ToInt32(return_value.ToString());
                }
                public static int Update__Hold_Order_Id(int Pallet_Id, int Order_Id, int Mod_User_Id)
                {
                    FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
                    object ret_val;

                    SQLAccessLayer.RunSP_NonQuery("dbo.PF_Pallet_Update_Hold_Order_Id", out ret_val, Pallet_Id, Order_Id, Mod_User_Id);

                    //get the return value
                    var nameOfProperty = "Value";
                    var propertyInfo = ret_val.GetType().GetProperty(nameOfProperty);
                    var return_value = propertyInfo.GetValue(ret_val, null);

                    return Convert.ToInt32(return_value.ToString());
                }


            #endregion
        #endregion
    }
}
