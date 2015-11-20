using FP.Utils.Data;
using System.Data;

namespace PF.Data.AccessLayer
{
    /*Description
    -----------------
    PF_Test Class.
     *
     * This Class is a data access layer to the PF_Test table
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

    public class PF_Tests
    {
        public static DataSet Get_Max_ID()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Test_Id) as Current_Id FROM PF_Tests");
        }

        public static DataSet Get_Info()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT *, Code + ' - ' + Description as Combined FROM PF_Tests WHERE PF_Active_Ind = 1  ORDER BY Code");
        }

        public static DataSet Get_Info(int Test_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Tests WHERE PF_Active_Ind = 1 AND Test_Id = " + Test_Id);
        }

        public static DataSet Get_Info(string Code)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Tests WHERE PF_Active_Ind = 1 AND Code = '" + Code + "'");
        }

        public static int Insert(int Test_Id, string Code, string Description, string TType, bool PF_Active_Ind, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_Tests(Test_Id, Code, Description, Type, PF_Active_Ind, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + Test_Id + ",'" + Code + "','" + Description + "','" + TType + "','" + PF_Active_Ind + "', GETDATE()," + Mod_User_Id + ")");
        }

        public static int Delete(int Test_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Tests WHERE Test_Id = " + Test_Id);
        }

        public static int Update(int Test_Id, string Code, string Description, string TType, bool PF_Active_Ind, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_Tests SET Code = '" + Code + "', " +
                                                                  "Description = '" + Description + "', " +
                                                                  "PF_Active_Ind = '" + PF_Active_Ind + "', " +
                                                                  "Type = '" + TType + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Test_Id = " + Test_Id);
        }
    }
}