using FruPak.Utils.Data;
using System.Data;

namespace FruPak.PF.Data.AccessLayer
{
    /*Description
    -----------------
    PR_PrintRequest Class.
     *
     * This Class is a data access layer to the PR_PrintRequest table
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

    public class SY_PrintRequest
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(PrintRequest_Id) as Current_Id FROM dbo.SY_PrintRequest");
        }

        public static DataSet Get_Info()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM dbo.SY_PrintRequest WHERE PDF_Created = 0");
        }

        public static DataSet Get_Info_All()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM dbo.SY_PrintRequest ORDER BY PrintRequest_Id DESC");
        }

        public static DataSet Get_Info(int PrintRequest_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM dbo.SY_PrintRequest WHERE PrintRequest_Id = " + PrintRequest_Id);
        }

        public static int Insert(int PrintRequest_Id, string Printer_Name, string Template_Name, string PDF_Name, string Data, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO  dbo.SY_PrintRequest(PrintRequest_Id, Printer_Name, Template_Name, PDF_Name, Data, PDF_Created, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + PrintRequest_Id + ",'" + Printer_Name + "','" + Template_Name + "','" + PDF_Name + "','" + Data + "', 'false', GETDATE()," + Mod_User_Id + ")");
        }

        public static int Update(int PrintRequest_Id, bool PDF_Created, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE  dbo.SY_PrintRequest SET PDF_Created = '" + PDF_Created + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE PrintRequest_Id = " + PrintRequest_Id);
        }

        public static int Delete(int PrintRequest_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM  dbo.SY_PrintRequest WHERE PrintRequest_Id = " + PrintRequest_Id);
        }

        public static int Delete_All()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM  dbo.SY_PrintRequest WHERE PDF_Created = 1 ");
        }
    }
}