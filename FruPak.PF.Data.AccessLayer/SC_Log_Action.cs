using FruPak.Utils.Data;
using System.Data;

namespace FruPak.PF.Data.AccessLayer
{
    /*Description
    -----------------
    SC_Log_Action Class.
     *
     * This Class is a data access layer to the SC_Log_Action table
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

    public class SC_Log_Action
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Action_Id) as Current_Id FROM  dbo.SC_Log_Action");
        }

        public static DataSet Get_Info()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM  dbo.SC_Log_Action ORDER BY Code");
        }

        public static DataSet Get_Info(string Code)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM  dbo.SC_Log_Action WHERE Code = '" + Code + "'");
        }

        public static int Insert(int Action_Id, string Code, string Description, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO  dbo.SC_Log_Action(Action_Id, Code, Description, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + Action_Id + ",'" + Code + "','" + Description + "', GETDATE()," + Mod_User_Id + ")");
        }

        public static int Delete(int Action_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM  dbo.SC_Log_Action WHERE Action_Id = " + Action_Id);
        }

        public static int Update(int Action_Id, string Code, string Description, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE  dbo.SC_Log_Action SET Code = '" + Code + "', " +
                                                                  "Description = '" + Description + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Action_Id = " + Action_Id);
        }
    }
}