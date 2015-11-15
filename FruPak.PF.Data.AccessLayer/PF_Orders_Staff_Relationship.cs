using FruPak.Utils.Data;
using System.Data;

namespace FruPak.PF.Data.AccessLayer
{
    /*Description
    -----------------
    PF_Orders_Staff_Relationship Class.
     *
     * This Class is a data access layer to the PF_Orders_Staff_Relationship table
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

    public class PF_Orders_Staff_Relationship
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(OStf_Relat_Id) as Current_Id FROM PF_Orders_Staff_Relationship");
        }

        public static DataSet Get_Info()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Orders_Staff_Relationship ");
        }

        public static DataSet Get_Info_Translated(int Order_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM dbo.PF_Orders_Staff_Relationship OSR " +
                                            "INNER JOIN dbo.PF_Staff S ON S.Staff_Id = OSR.Staff_Id " +
                                            "WHERE OSR.Order_Id = " + Order_Id);
        }

        public static int Insert(int OStf_Relat_Id, int Order_Id, int Staff_Id, string Start, string Finish, string Work_Type, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_Orders_Staff_Relationship(OStf_Relat_Id, Order_Id, Staff_Id, Start_Time, Finish_Time, Work_Type, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + OStf_Relat_Id + "," + Order_Id + "," + Staff_Id + ",'" + Start + "','" + Finish + "','" + Work_Type + "',GETDATE()," + Mod_User_Id + ")");
        }

        public static int Delete(int OStf_Relat_Id, int Staff_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Orders_Staff_Relationship WHERE OStf_Relat_Id = " + OStf_Relat_Id + " AND Staff_Id = " + Staff_Id);
        }

        public static int Delete_Order(int Order_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Orders_Staff_Relationship WHERE Order_Id = " + Order_Id);
        }

        public static int Update(int OStf_Relat_Id, int Staff_Id, string Start, string Finish, string Work_Type, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_Orders_Staff_Relationship SET Staff_Id = " + Staff_Id + ", " +
                                                                 "Start_Time = '" + Start + "', " +
                                                                 "Finish_Time = '" + Finish + "', " +
                                                                 "Work_Type = '" + Work_Type + "', " +
                                                                 "Mod_date = GETDATE(), " +
                                                                 "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE OStf_Relat_Id = " + OStf_Relat_Id);
        }
    }
}