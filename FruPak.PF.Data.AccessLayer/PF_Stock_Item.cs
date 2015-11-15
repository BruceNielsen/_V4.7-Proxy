using FruPak.Utils.Data;
using System.Data;

namespace FruPak.PF.Data.AccessLayer
{
    /*Description
    -----------------
    PF_Stock_Item Class.
     *
     * This Class is a data access layer to the PF_Stock_Item table
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

    public class PF_Stock_Item
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Stock_Item_Id) as Current_Id FROM PF_Stock_Item");
        }

        public static DataSet Get_Info()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT *, Code + ' - ' + Description as Combined FROM PF_Stock_Item WHERE PF_Active_Ind = 1 ORDER BY Code");
        }

        public static DataSet Get_Info(int Stock_Item_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Stock_Item WHERE PF_Active_Ind = 1 AND  Stock_Item_Id = " + Stock_Item_Id);
        }

        public static DataSet Get_Info_for_material(int Material_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT STP.* FROM dbo.PF_Stock_Item STP " +
                                            "INNER JOIN dbo.CM_Material_ST_Product_Relationship MSTPR on STP.Stock_Item_Id = MSTPR.Stock_Item_Id " +
                                            "WHERE MSTPR.Material_Id = " + Material_Id);
        }

        public static DataSet Get_Info(string Code)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Stock_Item WHERE PF_Active_Ind = 1 AND Code = '" + Code + "'");
        }

        public static int Insert(int Stock_Item_Id, string Code, string Description, decimal Multiplier, int Trig, bool PF_Active_Ind, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_Stock_Item(Stock_Item_Id,  Code, Description, Multiplier, Trig, PF_Active_Ind, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + Stock_Item_Id + ",'" + Code + "','" + Description + "'," + Multiplier + ", " + Trig + ",'" + PF_Active_Ind + "', GETDATE()," + Mod_User_Id + ")");
        }

        public static int Update(int Stock_Item_Id, string Code, string Description, decimal Multiplier, int Trig, bool PF_Active_Ind, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_Stock_Item SET Code = '" + Code + "', " +
                                                                  "Description = '" + Description + "', " +
                                                                  "Multiplier = " + Multiplier + ", " +
                                                                  "Trig = " + Trig + ", " +
                                                                  "PF_Active_Ind = '" + PF_Active_Ind + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Stock_Item_Id = " + Stock_Item_Id);
        }

        public static int Update_Email(int Stock_Item_Id, bool Email_Sent, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_Stock_Item SET Email_Sent = '" + Email_Sent + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Stock_Item_Id = " + Stock_Item_Id);
        }

        public static int Delete(int Stock_Item_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Stock_Item WHERE Stock_Item_Id = " + Stock_Item_Id);
        }
    }
}