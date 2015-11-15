using FruPak.Utils.Data;
using System.Data;

namespace FruPak.PF.Data.AccessLayer
{
    /*Description
    -----------------
    CM_Product_Product_Group_Relationship Class.
     *
     * This Class is a data access layer to the CM_Product_Product_Group_Relationship table
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

    public class CM_Product_Product_Group_Relationship
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(ProdProdGrp_Relat_Id) as Current_Id FROM CM_Product_Product_Group_Relationship");
        }

        public static int Insert(int ProdProdGrp_Relat_Id, int Product_Id, int ProductGroup_Id, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO CM_Product_Product_Group_Relationship(ProdProdGrp_Relat_Id, Product_Id, ProductGroup_Id, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + ProdProdGrp_Relat_Id + "," + Product_Id + "," + ProductGroup_Id + ", GETDATE()," + Mod_User_Id + ")");
        }

        public static DataSet Get_Info_By_Group(int ProductGroup_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM CM_Product_Product_Group_Relationship WHERE ProductGroup_Id = " + ProductGroup_Id);
        }

        public static DataSet Get_Info_By_Product(int Product_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM CM_Product_Product_Group_Relationship WHERE Product_Id = " + Product_Id);
        }

        public static DataSet Get_Info_By_Product_Material(int Product_Id, int int_ProductGroup_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT M.Material_Id, T.Code as Trader, PG.Code as ProductGroup, PG.Description as PG_Description,PT.Code as PackType, PT.Description as PT_Description, B.Code as Brand, Weight,Material_Num FROM CM_Product_Product_Group_Relationship PPGR " +
                                            "INNER JOIN dbo.CM_Material M on M.ProductGroup_Id =PPGR.ProductGroup_Id " +
                                            "INNER JOIN dbo.CM_Trader T on T.Trader_Id = M.Trader_Id " +
                                            "INNER JOIN dbo.CM_Product_Group PG ON PG.ProductGroup_Id = M.ProductGroup_Id " +
                                            "INNER JOIN dbo.CM_Pack_Type PT ON PT.PackType_Id = M.PackType_Id " +
                                            "INNER JOIN dbo.CM_Brand B ON B.Brand_Id = M.Brand_Id " +
                                            "WHERE Product_Id = " + Product_Id + " AND M.ProductGroup_Id = " + int_ProductGroup_Id);
        }

        public static int Delete(int ProdProdGrp_Relat_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM CM_Product_Product_Group_Relationship WHERE ProdProdGrp_Relat_Id = " + ProdProdGrp_Relat_Id);
        }

        public static int Delete_Group_From_Product(int Product_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM CM_Product_Product_Group_Relationship WHERE Product_Id = " + Product_Id);
        }

        public static int Delete_Product_From_Group(int ProductGroup_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM CM_Product_Product_Group_Relationship WHERE ProductGroup_Id = " + ProductGroup_Id);
        }

        public static int Delete_Product_From_Group(int Product_Id, int ProductGroup_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM CM_Product_Product_Group_Relationship WHERE ProductGroup_Id = " + ProductGroup_Id + " AND Product_Id = " + Product_Id);
        }
    }
}