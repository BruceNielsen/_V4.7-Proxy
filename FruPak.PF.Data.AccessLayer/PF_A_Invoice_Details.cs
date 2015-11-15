using FruPak.Utils.Data;
using System.Data;

namespace FruPak.PF.Data.AccessLayer
{
    /*Description
    -----------------
    PF_A_Invoice_Details Class.
     *
     * This Class is a data access layer to the PF_A_Invoice_Details table
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

    public class PF_A_Invoice_Details
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(InvoiceDetails_Id) as Current_Id FROM PF_A_Invoice_Details");
        }

        public static DataSet Get_Info(int Invoice_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_A_Invoice_Details WHERE Invoice_Id =" + Invoice_Id);
        }

        public static DataSet Get_Info_Translated_WO(int Invoice_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT AID.*, AR.Value, AR.Code, AR.Description FROM PF_A_Invoice_Details AID " +
                                            "INNER JOIN dbo.PF_A_Rates AR ON AR.Rates_Id = AID.Rates_Id WHERE Invoice_Id =" + Invoice_Id);
        }

        public static DataSet Get_Info_Translated_Sales(int Invoice_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT AID.*,  P.Code, P.Description FROM PF_A_Invoice_Details AID " +
                                            "INNER JOIN dbo.PF_Product P ON P.Product_Id = AID.Product_Id " +
                                            "WHERE Invoice_Id =" + Invoice_Id);
        }

        public static DataSet Get_Order_Nums(int Invoice_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("select distinct AID.Order_Num FROM dbo.PF_A_Invoice_Details AID WHERE Invoice_Id =" + Invoice_Id);
        }

        public static int Insert_Rates(int InvoiceDetails_Id, int Invoice_Id, int Rates_Id, decimal Cost, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_A_Invoice_Details(InvoiceDetails_Id,  Invoice_Id, Rates_Id, Cost,  Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + InvoiceDetails_Id + "," + Invoice_Id + "," + Rates_Id + "," + Cost + ", GETDATE()," + Mod_User_Id + ")");
        }

        public static int Insert_Product(int InvoiceDetails_Id, int Invoice_Id, int Material_Id, decimal Cost, int Mod_User_Id, string Order_Num, decimal Quantity)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_A_Invoice_Details(InvoiceDetails_Id,  Invoice_Id, Material_Id, Cost,  Mod_Date, Mod_User_Id, Order_Num, Quantity) " +
                                                "VALUES ( " + InvoiceDetails_Id + "," + Invoice_Id + "," + Material_Id + "," + Cost + ", GETDATE()," + Mod_User_Id + ",'" + Order_Num + "', " + Quantity + ")");
        }

        public static int Update_Rate(int InvoiceDetails_Id, int Rates_Id, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_A_Invoice_Details SET Rates_Id = " + Rates_Id + ", Mod_Date = GETDATE(), " +
                                                                        "Mod_User_Id = " + Mod_User_Id +
                                               "WHERE InvoiceDetails_Id = " + InvoiceDetails_Id);
        }

        public static int Delete(int Invoice_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_A_Invoice_Details WHERE Invoice_Id = " + Invoice_Id);
        }
    }
}