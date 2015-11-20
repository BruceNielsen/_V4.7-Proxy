using FP.Utils.Data;
using System.Data;

namespace PF.Data.AccessLayer
{
    /*Description
 -----------------
 CM_Growing_Method Class.
  *
  * This Class is a data access layer to the CM_Growing_Method table
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
 09/09/2013  Alison     Creation
 */

    public class CM_Growing_Method
    {
        public static DataSet Get_Max_ID()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Growing_Method_Id) as Current_Id FROM CM_Growing_Method");
        }

        public static DataSet Get_Info()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT *, Code + ' - ' + Description as Combined FROM CM_Growing_Method WHERE PF_Active_Ind = 1 ORDER BY Code");
        }

        public static DataSet Get_Info(int Growing_Method_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM CM_Growing_Method WHERE Growing_Method_Id = " + Growing_Method_Id);
        }

        public static DataSet Get_Info(string Code)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM CM_Growing_Method WHERE PF_Active_Ind = 1 AND Code = '" + Code + "'");
        }

        public static int Insert(int Growing_Method_Id, string Code, string Description, bool PF_Active_Ind, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO CM_Growing_Method(Growing_Method_Id, Code, Description, PF_Active_Ind, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + Growing_Method_Id + ",'" + Code + "','" + Description + "','" + PF_Active_Ind + "', GETDATE()," + Mod_User_Id + ")");
        }

        public static int Delete(int Growing_Method_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM CM_Growing_Method WHERE Growing_Method_Id = " + Growing_Method_Id);
        }

        public static int Update(int Growing_Method_Id, string Code, string Description, bool PF_Active_Ind, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE CM_Growing_Method SET Code = '" + Code + "', " +
                                                                  "Description = '" + Description + "', " +
                                                                  "PF_Active_Ind = '" + PF_Active_Ind + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Growing_Method_Id = " + Growing_Method_Id);
        }
    }
}