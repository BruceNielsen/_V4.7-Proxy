using FP.Utils.Data;
using System.Data;

namespace PF.Data.AccessLayer
{
    /*Description
    -----------------
    PF_Product_Cleaning_Area_Relationship Class.
     *
     * This Class is a data access layer to the PF_Product_Cleaning_Area_Relationship table
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

    public class PF_Product_Cleaning_Area_Relationship
    {
        public static DataSet Get_Max_ID()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(ProdCleaning_Relat_Id) as Current_Id FROM PF_Product_Cleaning_Area_Relationship");
        }

        public static int Insert(int ProdCleaning_Relat_Id, int Product_Id, int CleanArea_Id, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_Product_Cleaning_Area_Relationship(ProdCleaning_Relat_Id, Product_Id, CleanArea_Id, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + ProdCleaning_Relat_Id + "," + Product_Id + "," + CleanArea_Id + ", GETDATE()," + Mod_User_Id + ")");
        }

        public static DataSet Get_Info_By_Product(int Product_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Product_Cleaning_Area_Relationship WHERE Product_Id = " + Product_Id);
        }

        public static DataSet Get_Info_By_Area(int CleanArea_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Product_Cleaning_Area_Relationship WHERE CleanArea_Id = " + CleanArea_Id);
        }

        public static int Delete(int Product_Id, int CleanArea_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Product_Cleaning_Area_Relationship WHERE Product_Id = " + Product_Id + " AND CleanArea_Id =" + CleanArea_Id);
        }
    }
}