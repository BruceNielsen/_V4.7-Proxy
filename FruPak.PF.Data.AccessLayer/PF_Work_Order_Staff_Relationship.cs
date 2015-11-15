using FruPak.Utils.Data;
using System.Data;

namespace FruPak.PF.Data.AccessLayer
{
    /*Description
    -----------------
    PF_Staff Class.
     *
     * This Class is a data access layer to the PF_Staff table
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

    public class PF_Work_Order_Staff_Relationship
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(WOStf_Relat_Id) as Current_Id FROM PF_Work_Order_Staff_Relationship");
        }

        public static DataSet Get_Info()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Work_Order_Staff_Relationship ");
        }

        public static DataSet Get_Info_Translated(int Work_Order_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT WOSR.*, S.First_Name + ' ' + S.Last_Name as Name " +
                                            "FROM PF_Work_Order_Staff_Relationship WOSR " +
                                            "INNER JOIN dbo.PF_Staff S ON WOSR.Staff_Id = S.Staff_Id " +
                                            "WHERE Work_Order_Id = " + Work_Order_Id);
        }

        public static int Insert(int WOStf_Relat_Id, int Work_Order_Id, int Staff_Id, bool Process_Hours, decimal Extra_Hours, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_Work_Order_Staff_Relationship(WOStf_Relat_Id, Work_Order_Id, Staff_Id, Process_Hours, Extra_Hours, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + WOStf_Relat_Id + "," + Work_Order_Id + "," + Staff_Id + ",'" + Process_Hours + "'," + Extra_Hours + ", GETDATE()," + Mod_User_Id + ")");
        }

        public static int Delete(int WOStf_Relat_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Work_Order_Staff_Relationship WHERE WOStf_Relat_Id = " + WOStf_Relat_Id);
        }

        public static int Delete_WO(int Work_Order_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Work_Order_Staff_Relationship WHERE Work_Order_Id = " + Work_Order_Id);
        }

        public static int Update(int WOStf_Relat_Id, int Work_Order_Id, int Staff_Id, bool Process_Hours, decimal Extra_Hours, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_Work_Order_Staff_Relationship SET Work_Order_Id = " + Work_Order_Id + ", " +
                                                                                           "Staff_Id = " + Staff_Id + ", " +
                                                                                           "Process_Hours = '" + Process_Hours + "', " +
                                                                                           "Extra_Hours = " + Extra_Hours + ", " +
                                                                                           "Mod_Date = GETDATE(), " +
                                                                                           "Mod_User_Id = " + Mod_User_Id + " " +
                                              "WHERE WOStf_Relat_Id = " + WOStf_Relat_Id);
        }
    }
}