using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using FruPak.Utils.Data;

namespace FruPak.PF.Data.AccessLayer
{
    /*Description
    -----------------
    PF_Product_Chemicals_Relationship Class.
     * 
     * This Class is a data access layer to the PF_Product_Chemicals_Relationship table
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
    public class PF_Product_Chemicals_Relationship
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(ProdChem_Relat_Id) as Current_Id FROM PF_Product_Chemicals_Relationship");
        }
        public static int Insert(int ProdChem_Relat_Id, int Product_Id, int Chemical_Id, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_Product_Chemicals_Relationship(ProdChem_Relat_Id, Product_Id, Chemical_Id, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + ProdChem_Relat_Id + "," + Product_Id + "," + Chemical_Id + ", GETDATE()," + Mod_User_Id + ")");
        }
        public static DataSet Get_Info(int Product_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT PCR.ProdChem_Relat_Id, PCR.Product_Id, P.Code as Product, PCR.Chemical_Id, C.Code as Chemical, PCR.Mod_Date, PCR.Mod_User_Id " +
                                            "FROM PF_Product_Chemicals_Relationship PCR " +
                                            "INNER JOIN PF_Product P ON P.Product_Id = PCR.Product_Id " +
                                            "INNER JOIN PF_Chemicals C ON C.Chemical_Id = PCR.Chemical_Id " +
                                            "WHERE PCR.Product_Id = " + Product_Id);
        }
        public static DataSet Get_Info_By_Chemical(int Chemical_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Product_Chemicals_Relationship WHERE Chemical_Id = " + Chemical_Id);
        }
        public static int Delete(int ProdChem_Relat_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Product_Chemicals_Relationship WHERE ProdChem_Relat_Id = " + ProdChem_Relat_Id);
        }
        public static int Delete_Chemical_From_Product(int Chemical_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Product_Chemicals_Relationship WHERE Chemical_Id = " + Chemical_Id);
        }
        public static int Delete_Product_From_Chemical(int Product_Id, int Chemical_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Product_Chemicals_Relationship WHERE Chemical_Id = " + Chemical_Id + " AND Product_Id = " + Product_Id);
        }
    }
}
