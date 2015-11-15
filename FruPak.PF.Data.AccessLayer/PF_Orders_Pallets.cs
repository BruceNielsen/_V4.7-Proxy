using FruPak.Utils.Data;
using System.Data;

namespace FruPak.PF.Data.AccessLayer
{
    /*Description
    -----------------
    PF_Orders_Pallets Class.
     *
     * This Class is a data access layer to the PF_Orders_Pallets table
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

    public class PF_Orders_Pallets
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(OrderPallet_Id) as Current_Id FROM PF_Orders_Pallets");
        }

        public static DataSet Get_Info()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Orders_Pallets");
        }

        public static DataSet Get_Info_for_Order(int Order_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT OP.*, PT.Code FROM PF_Orders_Pallets OP INNER JOIN dbo.CM_Pallet_Type PT ON PT.PalletType_Id = OP.PalletType_Id WHERE Order_Id = " + Order_Id);
        }

        public static int Insert(int OrderPallet_Id, int Order_Id, int PalletType_Id, decimal Number, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_Orders_Pallets(OrderPallet_Id, Order_Id, PalletType_Id, Number, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + OrderPallet_Id + "," + Order_Id + "," + PalletType_Id + "," + Number + ", GETDATE()," + Mod_User_Id + ")");
        }

        public static int Delete(int Order_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Orders_Pallets WHERE Order_Id = " + Order_Id);
        }
    }
}