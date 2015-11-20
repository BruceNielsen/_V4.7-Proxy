using FP.Utils.Data;
using System.Data;

namespace PF.Data.AccessLayer
{
    /*Description
    -----------------
    PF_Orders Class.
     *
     * This Class is a data access layer to the PF_Orders table
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
    public class PF_Orders
    {
        public static DataSet Get_Max_ID()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Order_Id) as Current_Id FROM PF_Orders");
        }
        public static DataSet Get_Info()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Orders WHERE PF_Active_Ind = 1 ");
        }
        public static DataSet Get_Info(int Order_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Orders WHERE Order_Id = " + Order_Id);
        }
        public static DataSet Get_Order_id_for_Invoice(int Invoice_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT O.Order_Id, P.Pallet_Id " +
                                            "FROM PF_Orders O " +
                                            "INNER JOIN dbo.PF_Pallet P on P.Hold_For_Order_Id = O.Order_Id " +
                                            "WHERE Invoice_Id = " + Invoice_Id);
        }
        public static DataSet Get_Info_incl_Outlook(int Order_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT O.*, CC.Outlook_Key " +
                                            "FROM PF_Orders O " +
                                            "INNER JOIN dbo.PF_Customer CC ON CC.Customer_Id = O.Customer_Id " +
                                            "WHERE O.PF_Active_Ind = 1 AND Order_Id = " + Order_Id);
        }
        public static DataSet Get_Info_Translated(int Order_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT O.Order_Id, O.Load_Date, O.Customer_Id,CC.Name as Customer,O.Customer_Order,O.Truck_ID,T.Description AS Truck,O.City_Id,C.Description AS City, O.Freight_Docket, O.Comments " +
                                            "FROM PF_Orders O " +
                                            "INNER JOIN dbo.CM_Truck T ON T.Truck_Id = O.Truck_Id " +
                                            "INNER JOIN dbo.CM_Cities C ON C.City_Id = O.City_Id " +
                                            "INNER JOIN dbo.PF_Customer CC ON CC.Customer_Id = O.Customer_Id " +
                                            "WHERE Order_Id = " + Order_Id + "ORDER BY O.Order_Id DESC");
        }
        public static DataSet Get_Storage(string WHERE)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT DATEDIFF(MONTH, WO.Process_Date, O.Load_Date) as Storage_Time, SUM(PD.Quantity) as Total " +
                                            "FROM dbo.PF_Orders O " +
                                            "INNER JOIN dbo.PF_Pallet P ON P.Order_Id = O.Order_Id " +
                                            "INNER JOIN dbo.PF_Pallet_Details PD on PD.Pallet_Id = P.Pallet_Id " +
                                            "INNER JOIN dbo.PF_Work_Order WO on WO.Work_Order_Id = PD.Work_Order_Id " +
                                            "WHERE O.Order_Id in " + WHERE + " AND DATEDIFF(MONTH, WO.Process_Date, O.Load_Date) > 0 " +
                                            "GROUP BY DATEDIFF(MONTH, WO.Process_Date, O.Load_Date)");
        }
        public static int Insert(int Order_Id, int Truck_Id, int City_Id, string Load_Date, string Customer_Order, string Freight_Docket, int Customer_Id, string Comments, bool PF_Active_Ind, bool Hold_For_Payment, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_Orders(Order_Id, Truck_Id, City_Id, Load_Date,  Customer_Order,Freight_Docket, Customer_Id, Comments, PF_Active_Ind, Hold_For_Payment, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + Order_Id + "," + Truck_Id + "," + City_Id + ",'" + Load_Date + "','" + Customer_Order + "','" + Freight_Docket + "'," + Customer_Id + ",'" + Comments + "','" + PF_Active_Ind + "','" + Hold_For_Payment + "', GETDATE()," + Mod_User_Id + ")");
        }
        public static int Update(int Order_Id, int Truck_Id, int City_Id, string Load_Date, string Customer_Order, string Freight_Docket, int Customer_Id, string Comments, bool PF_Active_Ind, bool Hold_For_Payment, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_Orders SET Truck_Id = " + Truck_Id + ", " +
                                                                  "City_Id = " + City_Id + ", " +
                                                                  "Load_Date = '" + Load_Date + "', " +
                                                                  "Customer_Order = '" + Customer_Order + "', " +
                                                                  "Freight_Docket = '" + Freight_Docket + "', " +
                                                                  "Customer_Id = " + Customer_Id + ", " +
                                                                  "Comments = '" + Comments + "', " +
                                                                  "PF_Active_Ind = '" + PF_Active_Ind + "', " +
                                                                  "Hold_For_Payment = '" + Hold_For_Payment + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Order_Id = " + Order_Id);
        }
        public static int Delete(int Order_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Orders WHERE Order_Id = " + Order_Id);
        }
        public static int Update_Invoiced(int Order_Id, int Invoice_Id, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_Orders SET Invoice_Id = " + Invoice_Id + " ," +
                                                                        "PF_Active_Ind = 0 ," +
                                                                        "Mod_Date = GETDATE() ," +
                                                                        "Mod_User_Id = " + Mod_User_Id + " " +
                                                                        "WHERE Order_Id = " + Order_Id);
        }
        public static int Remove_From_Invoiced(int Invoice_Id, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_Orders SET Invoice_Id = NULL, " +
                                                                     "PF_Active_Ind = 1 ," +
                                                                     "Mod_Date = GETDATE() ," +
                                                                     "Mod_User_Id = " + Mod_User_Id + " " +
                                                                     "WHERE Invoice_Id = " + Invoice_Id);
        }
        #region ------------- PF_Orders -------------
        #region ------------- Select -------------
        public static DataSet Get_Info_Desc()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.RunSP_Query("dbo.PF_Orders_Get_Info_Desc");
        }
        public static DataSet Get_Order_Info(bool bol_incld_all)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.RunSP_Query("dbo.PF_Orders_Get_Order_Info", bol_incld_all);
        }
        public static DataSet Get_Order_Info_For_Cust(int Customer_Id, bool bol_incld_all)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.RunSP_Query("dbo.PF_Orders_Get_Order_Info_For_Cust", Customer_Id, bol_incld_all);
        }
        public static DataSet Get_Overdue_Orders()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.RunSP_Query("dbo.PF_Orders_Get_Overdue_Orders");
        }
        public static DataSet Get_UnInvoiced()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.RunSP_Query("dbo.PF_Orders_Get_UnInvoiced");
        }
        public static DataSet Get_UnInvoiced(int Customer_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.RunSP_Query("dbo.PF_Orders_Get_UnInvoiced_For_Cust", Customer_Id);
        }
        #endregion ------------- Select -------------
        #endregion ------------- PF_Orders -------------
    }
}