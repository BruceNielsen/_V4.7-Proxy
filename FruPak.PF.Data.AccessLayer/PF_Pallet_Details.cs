using FruPak.Utils.Data;
using System;
using System.Data;

namespace FruPak.PF.Data.AccessLayer
{
    /*Description
    -----------------
    PF_Pallet_Details Class.
     *
     * This Class is a data access layer to the PF_Pallet_Details table
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

    public class PF_Pallet_Details
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(PalletDetails_Id) as Current_Id FROM PF_Pallet_Details");
        }

        public static DataSet Get_Info()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Pallet_Details ");
        }

        public static DataSet Get_Info_For_Pallet(int Pallet_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Pallet_Details WHERE Pallet_Id = " + Pallet_Id);
        }

        public static DataSet Check_BatchNum(int Work_Order_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("WITH CTE AS (SELECT rownum = ROW_NUMBER() OVER (ORDER BY pd.[Batch_Num]), pd.Batch_Num " +
                                                         "FROM dbo.PF_Pallet_Details pd " +
                                                         "WHERE Work_Order_Id = " + Work_Order_Id + " )" +
                                            "SELECT prev.Batch_Num PreviousValue, CTE.Batch_Num " +
                                            "FROM CTE " +
                                            "LEFT JOIN CTE prev ON prev.rownum = CTE.rownum - 1");
        }

        public static DataSet Get_Info(int Work_Order_Id, int Pallet_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Pallet_Details WHERE Work_Order_Id = " + Work_Order_Id + " AND Pallet_Id = " + Pallet_Id + " ORDER BY Pallet_Id DESC");
        }

        public static DataSet Get_Info(string Barcode, int Material_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT PD.*, FT.Code as FT_Code, FV.Description as FV_Description, S.Description as S_Description, WO.Product_Id, G.Code as G_Code " +
                                            "FROM PF_Pallet_Details PD " +
                                            "INNER JOIN PF_PALLET P ON P.Pallet_Id = PD.Pallet_Id " +
                                            "INNER JOIN dbo.CM_Material M ON PD.Material_Id = M.Material_Id " +
                                            "INNER JOIN dbo.CM_Fruit_Type FT ON FT.FruitType_Id = M.FruitType_Id " +
                                            "INNER JOIN dbo.CM_Fruit_Variety FV ON FV.Variety_Id = M.Variety_Id " +
                                            "INNER JOIN dbo.CM_Size S On S.Size_Id = M.Size_Id " +
                                            "INNER JOIN dbo.CM_Grade G ON G.Grade_Id = M.Grade_Id " +
                                            "INNER JOIN dbo.PF_Work_Order WO ON WO.Work_Order_Id = PD.Work_Order_Id " +
                                            "WHERE P.Barcode = '" + Barcode + "' AND PD.Material_Id = " + Material_Id); // AND PD.Quantity > 0");
        }

        public static DataSet Check_Material(string Barcode)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * " +
                                            "FROM PF_Pallet_Details PD " +
                                            "INNER JOIN PF_PALLET P ON P.Pallet_Id = PD.Pallet_Id " +
                                            "INNER JOIN dbo.CM_Material M on M.Material_Id = PD.Material_Id " +
                                            "where P.Barcode = '" + Barcode + "' " +
                                            "AND M.Material_Num between 60000000 and 69999999 ");
        }

        public static DataSet Get_Info_For_WorkOrder(string WHERE, string CODE)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT DISTINCT Pallet_Id, SUM(Quantity) as Quantity " +
                                            "FROM PF_Pallet_Details PD " +
                                            "INNER JOIN dbo.CM_Material_Rates_Relationship MRR ON MRR.Material_Id = PD.Material_Id " +
                                            "INNER JOIN dbo.PF_A_Rates R ON R.Rates_Id = MRR.Rates_Id " +
                                            "WHERE PD.Work_Order_Id IN " + WHERE + "AND R.Code LIKE '" + CODE + "%'" +
                                            "GROUP BY Pallet_Id ");
        }

        public static DataSet Get_Batches_For_WorkOrder(string WHERE, string CODE)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT DISTINCT PD.Batch_Num " +
                                            "FROM PF_Pallet_Details PD " +
                                            "INNER JOIN dbo.CM_Material_Rates_Relationship MRR ON MRR.Material_Id = PD.Material_Id " +
                                            "INNER JOIN dbo.PF_A_Rates R ON R.Rates_Id = MRR.Rates_Id " +
                                            "WHERE PD.Work_Order_Id IN " + WHERE + "AND R.Code LIKE '" + CODE + "%'");
        }

        public static DataSet Count_for_WorkOrder(int Work_Order_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT SUM(Quantity) as Total FROM dbo.PF_Pallet_Details WHERE Work_Order_Id = " + Work_Order_Id);
        }

        public static DataSet Count_of_Pallets(int Work_Order_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT COUNT(DISTINCT Pallet_Id) as Total FROM dbo.PF_Pallet_Details WHERE Work_Order_Id = " + Work_Order_Id);
        }

        public static DataSet Current_Batch_for_WorkOrder(int Work_Order_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT MAX(Batch_Num) as Current_Batch FROM dbo.PF_Pallet_Details WHERE Work_Order_Id = " + Work_Order_Id);
        }

        public static DataSet Count_for_Current_Batch_for_WorkOrder(int Work_Order_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT SUM(Quantity) as Current_Total FROM dbo.PF_Pallet_Details " +
                                            "WHERE Work_Order_Id = " + Work_Order_Id +
                                            "AND Batch_Num = (SELECT MAX(Batch_Num) FROM dbo.PF_Pallet_Details WHERE Work_Order_Id = " + Work_Order_Id + " )");
        }

        public static int Delete(int PalletDetails_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Pallet_Details WHERE PalletDetails_Id = " + PalletDetails_Id);
        }

        public static int Delete_Pallet(int Pallet_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Pallet_Details WHERE Pallet_Id = " + Pallet_Id);
        }

        public static int Update(int PalletDetails_Id, int Batch_Num, int Material_Id, decimal Quantity, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_Pallet_Details SET Batch_Num = " + Batch_Num + ", " +
                                                                  "Material_Id = " + Material_Id + ", " +
                                                                  "Quantity = " + Quantity + ", " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE PalletDetails_Id = " + PalletDetails_Id);
        }

        public static int Update_Material(int PalletDetails_Id, int Material_Id, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_Pallet_Details SET Material_Id = " + Material_Id + ", " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE PalletDetails_Id = " + PalletDetails_Id);
        }

        public static int Update_EOR_Weight(int PalletDetails_Id, decimal EOR_Weight, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_Pallet_Details SET EOR_Weight = " + EOR_Weight + ", " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE PalletDetails_Id = " + PalletDetails_Id);
        }

        #region ------------- Stored Procedures -------------

        #region ------------- Select -------------

        public static DataSet Get_Info(int PalletDetails_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.RunSP_Query("dbo.PF_Pallet_Details_Get_Info_for_PalletDetail_Id", PalletDetails_Id);
        }

        public static DataSet Get_Pallet_Quantity(string Barcode)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.RunSP_Query("dbo.PF_Pallet_Details_Get_Pallet_Quantity", Barcode);
        }

        #endregion ------------- Select -------------

        #region ------------- Insert -------------

        public static int Insert(int PalletDetails_Id, int Pallet_Id, int Batch_Num, int Material_Id, int Work_Order_Id, decimal Quantity, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            object ret_val;

            SQLAccessLayer.RunSP_NonQuery("dbo.PF_Pallet_Details_Insert", out ret_val, PalletDetails_Id, Pallet_Id, Batch_Num, Material_Id, Work_Order_Id, Quantity, Mod_User_Id);

            //get the return value
            var nameOfProperty = "Value";
            var propertyInfo = ret_val.GetType().GetProperty(nameOfProperty);
            var return_value = propertyInfo.GetValue(ret_val, null);

            return Convert.ToInt32(return_value.ToString());
        }

        #endregion ------------- Insert -------------

        #region ------------- Update -------------

        public static int Update_Quantity(int PalletDetails_Id, decimal Quantity, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            object ret_val;

            SQLAccessLayer.RunSP_NonQuery("dbo.PF_Pallet_Details_Update_Quantity", out ret_val, PalletDetails_Id, Quantity, Mod_User_Id);

            //get the return value
            var nameOfProperty = "Value";
            var propertyInfo = ret_val.GetType().GetProperty(nameOfProperty);
            var return_value = propertyInfo.GetValue(ret_val, null);

            return Convert.ToInt32(return_value.ToString());
        }

        #endregion ------------- Update -------------

        #endregion ------------- Stored Procedures -------------
    }
}