using FP.Utils.Data;
using System.Data;

namespace PF.Data.AccessLayer
{
    /*Description
    -----------------
    PF_A_Customer_Sales_Rates Class.
     *
     * This Class is a data access layer to the PF_A_Customer_Sales_Rates table
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

    public class PF_A_Customer_Sales_Rates
    {
        public static DataSet Get_Max_ID()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(CustSalesRate_Id) as Current_Id FROM PF_A_Customer_Sales_Rates");
        }

        public static int Insert(int CustSalesRate_Id, int Material_Id, int Customer_Id, decimal Value, bool PF_Active_Ind, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_A_Customer_Sales_Rates(CustSalesRate_Id, Material_Id, Customer_Id, Value, PF_Active_Ind, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + CustSalesRate_Id + "," + Material_Id + "," + Customer_Id + ", " + Value + ",'" + PF_Active_Ind + "', GETDATE()," + Mod_User_Id + ")");
        }

        public static DataSet Get_Info()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_A_Customer_Sales_Rates");
        }

        public static DataSet Get_Info(int Material_Id, int Customer_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_A_Customer_Sales_Rates WHERE PF_Active_Ind = 1 AND Material_Id =" + Material_Id + " AND Customer_Id = " + Customer_Id);
        }

        public static DataSet Get_Info_Translated()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT CSR.CustSalesRate_Id, CSR.Customer_Id, C.Name as Customer, CSR.Material_Id, CAST(M.Material_Num as Varchar) + ' - ' + M.Description as Combined, " +
                                            "CSR.Value, CSR.PF_Active_Ind " +
                                            "FROM dbo.PF_A_Customer_Sales_Rates CSR " +
                                            "INNER JOIN dbo.CM_Material M ON M.Material_Id = CSR.Material_Id " +
                                            "INNER JOIN dbo.PF_Customer C ON C.Customer_Id = CSR.Customer_Id " +
                                            "ORDER BY C.Name");
        }

        public static int Update(int CustSalesRate_Id, int Material_Id, int Customer_Id, decimal Value, bool PF_Active_Ind, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_A_Customer_Sales_Rates SET Material_Id = " + Material_Id + ", " +
                                                                  "Customer_Id = " + Customer_Id + ", " +
                                                                  "Value = " + Value + ", " +
                                                                  "PF_Active_Ind = '" + PF_Active_Ind + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE CustSalesRate_Id = " + CustSalesRate_Id);
        }

        public static int Update_Active(int CustSalesRate_Id, bool PF_Active_Ind, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_A_Customer_Sales_Rates SET " +
                                                                  "PF_Active_Ind = '" + PF_Active_Ind + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE CustSalesRate_Id = " + CustSalesRate_Id);
        }

        public static int Delete(int CustSalesRate_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_A_Customer_Sales_Rates WHERE CustSalesRate_Id = " + CustSalesRate_Id);
        }
    }
}