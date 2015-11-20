using FP.Utils.Data;
using System.Data;

namespace PF.Data.AccessLayer
{
    /*Description
    -----------------
    CM_Material_ST_Product_Relationship Class.
     *
     * This Class is a data access layer to the CM_Material_ST_Product_Relationship table
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

    public class CM_Material_ST_Product_Relationship
    {
        public static DataSet Get_Max_ID()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(MaterialSTProd_Relat_Id) as Current_Id FROM CM_Material_ST_Product_Relationship");
        }

        public static DataSet Get_Info_For_Material(int Material_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM CM_Material_ST_Product_Relationship WHERE Material_Id = " + Material_Id);
        }

        public static DataSet Get_Update_Info(int old_Material_Id, int new_Material_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT Stock_Item_Id FROM dbo.CM_Material_ST_Product_Relationship WHERE Material_Id = " + new_Material_Id + " " +
                                            "EXCEPT " +
                                            "SELECT Stock_Item_Id FROM dbo.CM_Material_ST_Product_Relationship WHERE Material_Id = " + old_Material_Id);
        }

        public static int Insert(int MaterialSTProd_Relat_Id, int Material_Id, int Stock_Item_Id, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO CM_Material_ST_Product_Relationship(MaterialSTProd_Relat_Id, Material_Id, Stock_Item_Id, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + MaterialSTProd_Relat_Id + "," + Material_Id + "," + Stock_Item_Id + ", GETDATE()," + Mod_User_Id + ")");
        }

        public static int Delete_Material(int Material_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM CM_Material_ST_Product_Relationship WHERE Material_Id = " + Material_Id);
        }
    }
}