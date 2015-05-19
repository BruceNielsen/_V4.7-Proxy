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
    PF_Batch_Test Class.
     * 
     * This Class is a data access layer to the PF_Batch_Test table
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
    public class PF_Batch_Test
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(BatchTest_Id) as Current_Id FROM PF_Batch_Test");
        }
        public static DataSet Get_Info()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Batch_Test ORDER BY Work_Order_Id, Batch_Num");
        }
        public static DataSet Get_Info(int Work_Order_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Batch_Test WHERE Work_Order_Id = " + Work_Order_Id + "ORDER BY Batch_Num DESC");
        }
        public static int Insert(int BatchTest_Id, int Batch_Num, string Time, int Work_Order_Id, int Test_Id, bool Test_Passed, string Results, string Comments, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_Batch_Test (BatchTest_Id, Batch_Num, Time, Work_Order_Id, Test_Id, Test_Passed, Results, Comments, Mod_Date, Mod_User_Id)" +
                                               "VALUES( " + BatchTest_Id + "," + Batch_Num + ",'" + Time + "'," + Work_Order_Id + "," + Test_Id + ",'" + Test_Passed + "','" + Results + "','" + Comments + "', GETDATE()," + Mod_User_Id + ")");
        }
        public static int Delete(int BatchTest_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Batch_Test WHERE BatchTest_Id = " + BatchTest_Id);
        }
        public static int Update(int BatchTest_Id, bool Test_Passed, string Results, string Comments)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_Batch_Test SET Test_Passed = '" + Test_Passed + "', " +
                                                                   "Results = '" + Results + "', " +
                                                                   "Comments = '" + Comments + "' " +
                                                "WHERE BatchTest_Id = " + BatchTest_Id);
        }
    }
}
