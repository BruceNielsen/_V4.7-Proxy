using FP.Utils.Data;
using System.Data;

namespace PF.Data.AccessLayer
{
    /*Description
    -----------------
    PF_PreSystem_Pallet_Stock Class.
     *
     * This Class is a data access layer to the PF_PreSystem_Pallet_Stock table
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

    public class PF_PreSystem_Pallet_Stock
    {
        public static DataSet Get_Max_ID()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(PrePalletStock_ID) as Current_Id FROM PF_PreSystem_Pallet_Stock");
        }

        public static DataSet Get_Info()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_PreSystem_Pallet_Stock ");
        }

        public static DataSet Get_Info(int Material_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_PreSystem_Pallet_Stock WHERE Material_Id = " + Material_Id);
        }

        public static DataSet Get_Info_Translated()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT PPS.*, M.Material_Num, M.Description " +
                                            "FROM dbo.PF_PreSystem_Pallet_Stock PPS " +
                                            "INNER JOIN dbo.CM_Material M ON M.Material_Id = PPS.Material_Id");
        }

        public static int Delete(int PrePalletStock_ID)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_PreSystem_Pallet_Stock WHERE PrePalletStock_ID = " + PrePalletStock_ID);
        }

        public static int Insert(int PrePalletStock_ID, int Material_Id, decimal Quantity, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_PreSystem_Pallet_Stock(PrePalletStock_ID, Material_Id, Quantity,  Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + PrePalletStock_ID + "," + Material_Id + "," + Quantity + ", GETDATE()," + Mod_User_Id + ")");
        }

        public static int Update(int PrePalletStock_ID, decimal Quantity, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_PreSystem_Pallet_Stock SET Quantity = " + Quantity + ", " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE PrePalletStock_ID = " + PrePalletStock_ID);
        }

        public static int Update(int PrePalletStock_ID, int Material_Id, decimal Quantity, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_PreSystem_Pallet_Stock SET Material_Id = " + Material_Id + ", " +
                                                                  "Quantity = " + Quantity + ", " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE PrePalletStock_ID = " + PrePalletStock_ID);
        }
    }
}