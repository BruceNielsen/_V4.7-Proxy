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
    PF_Product_Tests_Relationship Class.
     * 
     * This Class is a data access layer to the PF_Product_Tests_Relationship table
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
    public class PF_Product_Tests_Relationship
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(ProdTest_Relat_Id) as Current_Id FROM PF_Product_Tests_Relationship");
        }
        public static int Insert(int ProdTest_Relat_Id, int Product_Id, int Test_Id, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_Product_Tests_Relationship(ProdTest_Relat_Id, Product_Id, Test_Id, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + ProdTest_Relat_Id + "," + Product_Id + "," + Test_Id + ", GETDATE()," + Mod_User_Id + ")");
        }
        public static DataSet Get_Info(int Product_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT PTR.ProdTest_Relat_Id, PTR.Product_Id, P.Code as Product, PTR.Test_Id, T.Code as Test,PTR.Mod_Date, PTR.Mod_User_Id " +
                                            "FROM PF_Product_Tests_Relationship PTR " +
                                            "INNER JOIN PF_Product P ON P.Product_Id = PTR.Product_Id " +
                                            "INNER JOIN PF_Tests T ON T.Test_Id = PTR.Test_Id " +
                                            "WHERE PTR.Product_Id = " + Product_Id);
        }
        public static DataSet Get_Info(int Product_Id, string TType)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT PTR.ProdTest_Relat_Id, PTR.Product_Id, P.Code as Product, PTR.Test_Id, T.Code as Test,PTR.Mod_Date, PTR.Mod_User_Id " +
                                            "FROM PF_Product_Tests_Relationship PTR " +
                                            "INNER JOIN PF_Product P ON P.Product_Id = PTR.Product_Id " +
                                            "INNER JOIN PF_Tests T ON T.Test_Id = PTR.Test_Id " +
                                            "WHERE PTR.Product_Id = " + Product_Id + " AND T.Type = '" + TType + "'");
        }
        public static DataSet Get_Info_By_Test(int Test_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Product_Tests_Relationship WHERE Test_Id = " + Test_Id);
        }
        public static int Delete(int ProdTest_Relat_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Product_Tests_Relationship WHERE ProdTest_Relat_Id = " + ProdTest_Relat_Id);
        }
        public static int Delete_Test_From_Product(int Test_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Product_Tests_Relationship WHERE Test_Id = " + Test_Id);
        }
        public static int Delete_Product_From_Test(int Product_Id, int Test_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Product_Tests_Relationship WHERE Test_Id = " + Test_Id + " AND Product_Id = " + Product_Id);
        }
    }
}
