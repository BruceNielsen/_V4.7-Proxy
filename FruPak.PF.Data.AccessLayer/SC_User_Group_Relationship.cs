using FP.Utils.Data;
using System.Data;

namespace PF.Data.AccessLayer
{
    /*Description
    -----------------
    SC_User_Group_Relationship Class.
     *
     * This Class is a data access layer to the SC_User_Group_Relationship table
     * Where possible the following standard method names are used and standard column names used.
     *  1. Variable names as input to a method are the same as the column names they refer to.
     *
     * Methods.
     *  Get_Max_ID: Return the max ID from the table, and is stored as Current_Id
     *  Get_Info:   returns all data from the table or return data for a requested record
     *  Delete:     Deletes a single record, with an ID supplied
     *  Insert:     Inserts a new record

    Date        Author     Desc
    -------------------------------------------------------------------------------------------------------------------------------------------------
    01/09/2013  Dave       Creation
    */

    public class SC_User_Group_Relationship
    {
        public static DataSet Get_Max_ID()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(UsrGrp_Relat_Id) as Current_Id FROM  dbo.SC_User_Group_Relationship");
        }

        public static int Insert(int UsrGrp_Relat_Id, int User_Id, int UserGroup_Id, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO  dbo.SC_User_Group_Relationship(UsrGrp_Relat_Id, User_Id, UserGroup_Id, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + UsrGrp_Relat_Id + "," + User_Id + "," + UserGroup_Id + ", GETDATE()," + Mod_User_Id + ")");
        }

        public static DataSet Get_Info_By_Group(int UserGroup_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM  dbo.SC_User_Group_Relationship WHERE UserGroup_Id = " + UserGroup_Id);
        }

        public static DataSet Get_Info_By_User(int User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM  dbo.SC_User_Group_Relationship WHERE User_Id = " + User_Id);
        }

        public static int Delete_User(int User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM  dbo.SC_User_Group_Relationship WHERE User_Id = " + User_Id);
        }

        public static int Delete_User_From_Group(int UserGroup_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM  dbo.SC_User_Group_Relationship WHERE UserGroup_Id = " + UserGroup_Id);
        }

        public static int Delete_User_From_Group(int User_Id, int UserGroup_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM  dbo.SC_User_Group_Relationship WHERE UserGroup_Id = " + UserGroup_Id + " AND User_Id = " + User_Id);
        }
    }
}