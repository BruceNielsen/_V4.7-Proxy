using FP.Utils.Data;
using System.Data;

namespace PF.Data.AccessLayer
{
    /*Description
    -----------------
    PF_Work_Order_Material_Relationship Class.
     *
     * This Class is a data access layer to the PF_Work_Order_Material_Relationship table
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

    public class PF_Work_Order_Material_Relationship
    {
        public static DataSet Get_Max_ID()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(WOMat_Relat_Id) as Current_Id FROM PF_Work_Order_Material_Relationship");
        }

        public static int Insert(int WOMat_Relat_Id, int work_Order_Id, int Material_Id, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_Work_Order_Material_Relationship(WOMat_Relat_Id, work_Order_Id, Material_Id, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + WOMat_Relat_Id + "," + work_Order_Id + "," + Material_Id + ", GETDATE()," + Mod_User_Id + ")");
        }

        public static DataSet Get_Info_By_Work_Order(int work_Order_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Work_Order_Material_Relationship WHERE work_Order_Id = " + work_Order_Id);
        }

        public static DataSet Get_Info_By_Material(int Material_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Work_Order_Material_Relationship WHERE Material_Id = " + Material_Id);
        }

        public static DataSet Get_Info_By_WO_Translated(int work_Order_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT DISTINCT WOMR.Material_Id,M.Description, M.ProductGroup_Id, PG.Description as ProductGroup, CONVERT(varchar(max),M.Material_Num) + ' - ' + M.Description + ' - ' + PG.Description as Combined  " +
                                            "FROM dbo.PF_Work_Order_Material_Relationship WOMR " +
                                            "INNER JOIN dbo.CM_Material M ON M.Material_Id = WOMR.Material_Id " +
                                            "INNER JOIN dbo.CM_Product_Group PG ON PG.ProductGroup_Id = M.ProductGroup_Id " +
                                            "WHERE WOMR.work_Order_Id = " + work_Order_Id);
        }

        public static DataSet Get_Info_By_WO_Translated_M(int Material_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT DISTINCT WOMR.Material_Id,M.Description, M.ProductGroup_Id, PG.Description as ProductGroup, CONVERT(varchar(max),M.Material_Num) + ' - ' + M.Description + ' - ' + PG.Description as Combined  " +
                                            "FROM dbo.PF_Work_Order_Material_Relationship WOMR " +
                                            "INNER JOIN dbo.CM_Material M ON M.Material_Id = WOMR.Material_Id " +
                                            "INNER JOIN dbo.CM_Product_Group PG ON PG.ProductGroup_Id = M.ProductGroup_Id " +
                                            "WHERE WOMR.Material_Id = " + Material_Id);
        }

        public static int Delete(int work_Order_Id, int Material_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Work_Order_Material_Relationship WHERE work_Order_Id = " + work_Order_Id + " AND Material_Id = " + Material_Id);
        }

        public static int Delete_WO(int work_Order_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Work_Order_Material_Relationship WHERE work_Order_Id = " + work_Order_Id);
        }
    }
}