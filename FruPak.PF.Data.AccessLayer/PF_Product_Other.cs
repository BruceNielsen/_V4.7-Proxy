using FP.Utils.Data;
using System.Data;

namespace PF.Data.AccessLayer
{
    /*Description
    -----------------
    PF_Product_Other Class.
     *
     * This Class is a data access layer to the PF_Product_Other table
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

    public class PF_Product_Other
    {
        public static DataSet Get_Max_ID()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Prod_Other_Id) as Current_Id FROM PF_Product_Other");
        }

        public static DataSet Get_Info()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Product_Other WHERE PF_Active_Ind = 1 ORDER BY Code");
        }

        public static DataSet Get_Info(int Prod_Other_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Product_Other WHERE PF_Active_Ind = 1 AND  Prod_Other_Id = " + Prod_Other_Id);
        }

        public static DataSet Get_Info(string Code)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Product_Other WHERE PF_Active_Ind = 1 AND Code = '" + Code + "'");
        }

        public static DataSet Get_Info(int Product_Id, string Code)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Product_Other WHERE PF_Active_Ind = 1 AND  Product_Id = " + Product_Id + " AND Code = '" + Code + "'");
        }

        public static DataSet Get_Info_by_Prod(int Product_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Product_Other WHERE PF_Active_Ind = 1 AND  Product_Id = " + Product_Id);
        }

        public static DataSet Get_Info_by_like_Code(string Code)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Product_Other WHERE PF_Active_Ind = 1 AND Code like" + Code + "'");
        }

        public static int Insert(int Prod_Other_Id, int Product_Id, string Code, string Description, string Value, bool PF_Active_Ind, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_Product_Other(Prod_Other_Id, Product_Id, Code, Description, Value, PF_Active_Ind, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + Prod_Other_Id + "," + Product_Id + ",'" + Code + "','" + Description + "','" + Value + "','" + PF_Active_Ind + "', GETDATE()," + Mod_User_Id + ")");
        }

        public static int Delete(int Prod_Other_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Product_Other WHERE Prod_Other_Id = " + Prod_Other_Id);
        }

        public static int Update(int Prod_Other_Id, int Product_Id, string Code, string Description, string Value, bool PF_Active_Ind, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_Product_Other SET Product_Id = " + Product_Id + ", " +
                                                                  "Code = '" + Code + "', " +
                                                                  "Description = '" + Description + "', " +
                                                                  "Value = '" + Value + "', " +
                                                                  "PF_Active_Ind = '" + PF_Active_Ind + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Prod_Other_Id = " + Prod_Other_Id);
        }
    }
}