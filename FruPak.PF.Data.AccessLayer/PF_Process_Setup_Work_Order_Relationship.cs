using FruPak.Utils.Data;
using System.Data;

namespace FruPak.PF.Data.AccessLayer
{
    /*Description
    -----------------
    PF_Process_Setup_Work_Order_Relationship Class.
     *
     * This Class is a data access layer to the PF_Process_Setup_Work_Order_Relationship table
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

    public class PF_Process_Setup_Work_Order_Relationship
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(ProcesssetupWorkOrder_Relat_Id) as Current_Id FROM PF_Process_Setup_Work_Order_Relationship");
        }

        public static DataSet Get_Selected(int Work_Order_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT DISTINCT Work_Order_Id FROM PF_Process_Setup_Work_Order_Relationship WHERE Work_Order_Id = " + Work_Order_Id);
        }

        public static int Insert(int ProcesssetupWorkOrder_Relat_Id, int Processsetup_Id, int Work_Order_Id, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_Process_Setup_Work_Order_Relationship(ProcesssetupWorkOrder_Relat_Id, Processsetup_Id, Work_Order_Id, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + ProcesssetupWorkOrder_Relat_Id + "," + Processsetup_Id + "," + Work_Order_Id + ", GETDATE()," + Mod_User_Id + ")");
        }

        public static int Delete(int Processsetup_Id, int Work_Order_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Process_Setup_Work_Order_Relationship WHERE Processsetup_Id = " + Processsetup_Id + " AND Work_Order_Id = " + Work_Order_Id);
        }
    }
}