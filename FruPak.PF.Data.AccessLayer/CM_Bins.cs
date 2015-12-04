using FP.Utils.Data;
using System;
using System.Data;

namespace PF.Data.AccessLayer
{
    /*Description
    -----------------
    CM_Bins Class.
     *
     * This Class is a data access layer to the CM_Bins table
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

    public class CM_Bins
    {
        public static DataSet Get_Max_ID()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Bins_Id) as Current_Id FROM CM_Bins");
        }

        public static DataSet Get_fruit_from_bin(int Bins_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("Select WO.Fruit_Type_Id, WO.Fruit_Variety_Id " +
                                            "from dbo.CM_Bins B inner join dbo.PF_Work_Order WO on WO.Work_Order_Id = B.Work_Order_In_Id " +
                                            "where B.Bins_Id = " + Bins_Id);
        }

        public static DataSet Get_Info()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM CM_Bins ");
        }

        public static DataSet Count_Bins_for_WorkOrder(int Work_Order_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT COUNT(*) AS Total FROM CM_Bins B WHERE B.Work_Order_Id = " + Work_Order_Id);
        }

        public static DataSet Get_Info_for_Work_Order(int Work_Order_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * " +
                                            "FROM CM_Bins B " +
                                            "LEFT OUTER JOIN dbo.CM_Material_Rates_Relationship MRR ON MRR.Material_Id = B.Material_Id " +
                                            "LEFT OUTER JOIN dbo.PF_A_Rates R ON R.Rates_Id = MRR.Rates_Id " +
                                            "WHERE B.Work_Order_Id = " + Work_Order_Id);
        }

        public static DataSet BinCard_Work_Order_In(int Bins_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * " +
                                            "FROM CM_Bins B " +
                                            "WHERE B.Bins_Id = " + Bins_Id);
        }

        public static DataSet Get_Info_for_Work_Order(string WHERE)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * " +
                                            "FROM CM_Bins B " +
                                            "INNER JOIN dbo.CM_Material_Rates_Relationship MRR ON MRR.Material_Id = B.Material_Id " +
                                            "INNER JOIN dbo.PF_A_Rates R ON R.Rates_Id = MRR.Rates_Id " +
                                            "WHERE B.Work_Order_Id in " + WHERE);
        }

        public static DataSet Get_unused_Rejects(int Work_Order_In_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * " +
                                            "FROM dbo.CM_Bins " +
                                            "WHERE Work_Order_In_Id = " + Work_Order_In_Id +
                                            " AND Material_Id is null AND Tipped_Date is null AND Dump_Ind is null");
        }

        public static DataSet Get_Info_for_Work_Order(string WHERE, string Code)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * " +
                                            "FROM CM_Bins B " +
                                            "INNER JOIN dbo.CM_Material_Rates_Relationship MRR ON MRR.Material_Id = B.Material_Id " +
                                            "INNER JOIN dbo.PF_A_Rates R ON R.Rates_Id = MRR.Rates_Id " +
                                            "WHERE B.Work_Order_Id in " + WHERE + "AND Code like '" + Code + "%'");
        }

        public static DataSet Get_Bin_Storage(string WHERE)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT DATEDIFF(MONTH, S.Sub_date, B.Tipped_Date) as Storage_Time, Count(*) as Bins " +
                                            "FROM dbo.CM_Bins B " +
                                            "INNER JOIN dbo.GH_Submission S ON S.Submission_Id = B.Submission_Id " +
                                            "WHERE Work_Order_Id IN " + WHERE + " AND DATEDIFF(MONTH, S.Sub_date, B.Tipped_Date) > 0 " +
                                            "GROUP BY DATEDIFF(MONTH, S.Sub_date, B.Tipped_Date)");
        }

        public static DataSet Get_Info_Submission(int Submission_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT  COUNT(B.Bins_Id) AS NumBins, B.Work_Order_Id, B.PF_Active_Ind, " +
                                                    "B.Location_Id, L.Code AS Location, " +
                                                    "B.Material_Id, M.Material_Num AS Material, M.FruitType_Id, M.Variety_Id," +
                                                    "B.Storage_Id, S.Code AS Storage, " +
                                                    "B.ESP_Id, E.Code AS ESP " +
                                            "FROM [dbo].[CM_Bins] B " +
                                            "INNER JOIN dbo.CM_Location L ON B.Location_Id = L.Location_Id " +
                                            "INNER JOIN dbo.CM_Material M ON B.Material_Id = M.Material_Id " +
                                            "INNER JOIN dbo.CM_Storage S ON B.Storage_Id = S.Storage_Id " +
                                            "INNER JOIN dbo.CM_ESP E ON B.ESP_Id = E.ESP_Id " +
                                            "WHERE B.Submission_Id = " + Submission_Id +
                                            "GROUP BY B.Work_Order_Id, B.PF_Active_Ind, B.Location_Id,L.Code,B.Material_Id, M.Material_Num,M.FruitType_Id, M.Variety_Id,B.Storage_Id, S.Code,B.ESP_Id, E.Code");
        }

        public static int InsertSubmission(int Bins_Id, int Submission_Id, string Barcode, int Location_Id, int Material_Id, int Storage_Id, int ESP_Id, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO CM_Bins(Bins_Id, Submission_Id, Barcode, Location_Id, Material_Id, Storage_Id, ESP_Id, PF_Active_Ind, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + Bins_Id + "," + Submission_Id + "," + Barcode + "," + Location_Id + "," + Material_Id + "," + Storage_Id + "," + ESP_Id + ",1, GETDATE()," + Mod_User_Id + ")");
        }

        //includes Weight_Tare
        public static int InsertSubmission(int Bins_Id, int Submission_Id, string Barcode, int Location_Id, int Material_Id, int Storage_Id, int ESP_Id, int Mod_User_Id, decimal Weight_Tare)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO CM_Bins(Bins_Id, Submission_Id, Barcode, Location_Id, Material_Id, Storage_Id, ESP_Id, PF_Active_Ind, Mod_Date, Mod_User_Id, Weight_Tare) " +
                                                "VALUES ( " + Bins_Id + "," + Submission_Id + "," + Barcode + "," + Location_Id + "," + Material_Id + "," + Storage_Id + "," + ESP_Id + ",1, GETDATE()," + Mod_User_Id + ", " + Weight_Tare + " )");
        }

        public static int InsertWorkOrder(int Bins_Id, int Work_Order_In_Id, string Barcode, int Location_Id, int Storage_Id, int ESP_Id, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO CM_Bins(Bins_Id, Work_Order_In_Id, Barcode, Location_Id, Storage_Id, ESP_Id, PF_Active_Ind, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + Bins_Id + "," + Work_Order_In_Id + "," + Barcode + "," + Location_Id + "," + Storage_Id + "," + ESP_Id + ",1, GETDATE()," + Mod_User_Id + ")");
        }

        public static int InsertGDIOverrun(int Bins_Id, string Barcode, string GDIBarcode, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO CM_Bins(Bins_Id, Barcode, GDIBarcode, PF_Active_Ind, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + Bins_Id + "," + Barcode + "," + GDIBarcode + ",1, GETDATE()," + Mod_User_Id + ")");
        }

        public static int InsertWorkOrder(int Bins_Id, int Work_Order_In_Id, string Barcode, int Location_Id, int Material_Id, int Storage_Id, int ESP_Id, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO CM_Bins(Bins_Id, Work_Order_In_Id, Barcode, Location_Id, Material_Id, Storage_Id, ESP_Id, PF_Active_Ind, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + Bins_Id + "," + Work_Order_In_Id + "," + Barcode + "," + Location_Id + "," + Material_Id + "," + Storage_Id + "," + ESP_Id + ",1, GETDATE()," + Mod_User_Id + ")");
        }

        public static int Delete(int Bins_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM CM_Bins WHERE Bins_Id = " + Bins_Id);
        }

        public static int Delete_Submission(int Submission_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM CM_Bins WHERE Submission_Id = " + Submission_Id);
        }

        public static int Update(int Bins_Id, int Submission_Id, int Work_Order_In_Id, string Barcode, int Location_Id, int Material_Id, int Storage_Id, int ESP_Id, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE CM_Bins SET Submission_Id = " + Submission_Id + ", " +
                                                                  "Work_Order_In_Id = " + Work_Order_In_Id + ", " +
                                                                  "Barcode = '" + Barcode + "', " +
                                                                  "Location_Id = " + Location_Id + ", " +
                                                                  "Material_Id = " + Material_Id + ", " +
                                                                  "Storage_Id = " + Storage_Id + ", " +
                                                                  "ESP_Id = " + ESP_Id + ", " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Bins_Id = " + Bins_Id);
        }

        public static int Update(int Submission_Id, int Location_Id, int Material_Id, int Storage_Id, int ESP_Id, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE CM_Bins SET  = Location_Id = " + Location_Id + ", " +
                                                                  "Material_Id = " + Material_Id + ", " +
                                                                  "Storage_Id = " + Storage_Id + ", " +
                                                                  "ESP_Id = " + ESP_Id + ", " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Submission_Id = " + Submission_Id);
        }

        public static int Update_Print_Ind(string Barcode, bool Print_ind)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE CM_Bins SET Print_ind = '" + Print_ind + "', " +
                                                                  "Mod_date = GETDATE() " +
                                              " WHERE Barcode = '" + Barcode + "'");
        }

        public static int Update_Dump_Ind(int Bins_Id, string Dump_Ind, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE dbo.CM_Bins SET Dump_Ind = '" + Dump_Ind + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  " Mod_User_Id = " + Mod_User_Id + " " +
                                              " WHERE Bins_Id = " + Bins_Id);
        }

        public static int Update_Material_Id(int Bins_Id, int Material_Id, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE dbo.CM_Bins SET Material_Id = " + Material_Id + ", " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  " Mod_User_Id = " + Mod_User_Id + " " +
                                              " WHERE Bins_Id = " + Bins_Id);
        }

        public static int Update_Weight_Gross(int Bins_Id, decimal Weight_Gross, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE dbo.CM_Bins	SET Weight_Gross = " + Weight_Gross + ", " +
                                                                       "Mod_date = GETDATE(), " +
                                                                       "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Bins_Id = " + Bins_Id);
        }

        public static int Update_Tare_Weight(int Bins_Id, decimal Weight_Tare, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE	dbo.CM_Bins	SET Weight_Tare = " + Weight_Tare + ", " +
                                                                       "Mod_date = GETDATE(), " +
                                                                       "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Bins_Id = " + Bins_Id);
        }

        public static DataSet Get_NET_Bin_Weight_by_Work_Order(int Work_Order_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT SUM(B.Weight_Gross)- SUM(B.Weight_Tare) as NET_Weight " +
                                            "FROM dbo.CM_Bins B " +
                                            "INNER JOIN dbo.PF_Work_Order WO ON WO.Work_Order_Id = B.Work_Order_Id " +
                                            "WHERE WO.Work_Order_Id = " + Work_Order_Id + " " +
                                            "GROUP by WO.Work_Order_Id ");
        }

        public static DataSet Get_All_For_Barcode(string barcode)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM dbo.CM_Bins WHERE barcode = '" + barcode + "' ");
        }

        /// <summary>
        /// BN 4/12/2015 for automated testing
        /// </summary>
        /// <returns></returns>
        public static DataSet Get_All_Barcodes_Where_Tipped_Date_Is_Null()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            //return SQLAccessLayer.Run_Query("SELECT * FROM dbo.CM_Bins WHERE Tipped_Date = '" + null + "' ");
            return SQLAccessLayer.Run_Query("SELECT * FROM dbo.CM_Bins WHERE Tipped_Date IS NULL");
        }

        public static DataSet Get_All_For_GDIBarcode(string barcode)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM dbo.CM_Bins WHERE GDIBarcode = '" + barcode + "' ");
        }

        #region ------------- Stored Procedures -------------

        #region ------------- Select -------------

        public static DataSet Get_Info_For_Barcode(string barcode)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.RunSP_Query("dbo.CM_Bins_Get_Info_For_Barcode", barcode);
        }

        public static DataSet Get_Info_For_GDIBarcode(string barcode)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.RunSP_Query("dbo.CM_Bins_Get_Info_For_GDIBarcode", barcode);
        }

        public static DataSet Get_Weights(string barcode)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.RunSP_Query("dbo.CM_Bins_Get_Weights", barcode);
        }

        #endregion ------------- Select -------------

        #region ------------- Updates -------------

        public static int Update_Tipped_Date(int Bins_Id, int Work_Order_Id, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            object ret_val;
            SQLAccessLayer.RunSP_NonQuery("dbo.CM_Bins_Update_Tipped_Date", out ret_val, Bins_Id, Work_Order_Id, Mod_User_Id);
            //get the return value
            var nameOfProperty = "Value";
            var propertyInfo = ret_val.GetType().GetProperty(nameOfProperty);
            var return_value = propertyInfo.GetValue(ret_val, null);

            return Convert.ToInt32(return_value.ToString());
        }

        #endregion ------------- Updates -------------

        #endregion ------------- Stored Procedures -------------
    }
}