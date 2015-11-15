using FruPak.Utils.Data;
using System.Data;

namespace FruPak.PF.Data.AccessLayer
{
    /*Description
    -----------------
    EX_GDI_Bins Class.
     *
     * This Class is a data access layer to the EX_GDI_Bins table
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

    public class EX_GDI_Bins
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Bins_Id) as Current_Id FROM EX_GDI_Bins");
        }

        public static DataSet Get_Info(int Bins_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM EX_GDI_Bins WHERE PF_Active_ind = 1 AND Bins_Id = " + Bins_Id);
        }

        public static DataSet Get_Info(string barcode)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM EX_GDI_Bins WHERE PF_Active_ind = 1 AND GDIBarcode = '" + barcode + "'");
        }

        public static int InsertSubmission(int Bins_Id, int Submission_Id, string Barcode, int Location_Id, int Material_Id, int Storage_Id, int ESP_Id, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO EX_GDI_Bins(Bins_Id, Submission_Id, Barcode, Location_Id, Material_Id, Storage_Id, ESP_Id, PF_Active_Ind, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + Bins_Id + "," + Submission_Id + "," + Barcode + "," + Location_Id + "," + Material_Id + "," + Storage_Id + "," + ESP_Id + ",1, GETDATE()," + Mod_User_Id + ")");
        }

        //includes Weight_Tare
        public static int InsertSubmission(int Bins_Id, int Submission_Id, string Barcode, int Location_Id, int Material_Id, int Storage_Id, int ESP_Id, int Mod_User_Id, decimal Weight_Tare)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO EX_GDI_Bins(Bins_Id, Submission_Id, Barcode, Location_Id, Material_Id, Storage_Id, ESP_Id, PF_Active_Ind, Mod_Date, Mod_User_Id, Weight_Tare) " +
                                                "VALUES ( " + Bins_Id + "," + Submission_Id + "," + Barcode + "," + Location_Id + "," + Material_Id + "," + Storage_Id + "," + ESP_Id + ",1, GETDATE()," + Mod_User_Id + ", " + Weight_Tare + " )");
        }

        public static int Update_Active(int Bins_Id, bool PF_Active_Ind, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE EX_GDI_Bins SET PF_Active_Ind = '" + PF_Active_Ind + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Bins_Id = " + Bins_Id);
        }
    }
}