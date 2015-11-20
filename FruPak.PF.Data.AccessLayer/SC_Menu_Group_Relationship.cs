using FP.Utils.Data;
using System.Data;

namespace PF.Data.AccessLayer
{
    /*Description
    -----------------
    SC_Menu_Group_Relationship Class.
     *
     * This Class is a data access layer to the SC_Menu_Group_Relationship table
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

    public class SC_Menu_Group_Relationship
    {
        public static DataSet Get_Max_ID()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT MAX(MnuGrp_Relat_Id) AS Current_Id FROM  dbo.SC_Menu_Group_Relationship");
        }

        public static int Insert(int MnuGrp_Relat_Id, int UserGroup_Id, int Menu_Id, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO  dbo.SC_Menu_Group_Relationship(MnuGrp_Relat_Id, UserGroup_Id, Menu_Id, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + MnuGrp_Relat_Id + "," + UserGroup_Id + "," + Menu_Id + ", GETDATE()," + Mod_User_Id + ")");
        }

        public static int Delete(int Menu_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM  dbo.SC_Menu_Group_Relationship WHERE Menu_Id = " + Menu_Id);
        }

        public static int Delete_UserGroup_From_Menu(int UserGroup_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM  dbo.SC_Menu_Group_Relationship WHERE UserGroup_Id = " + UserGroup_Id);
        }

        public static int Delete_UserGroup_From_Menu(int UserGroup_Id, int Menu_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM  dbo.SC_Menu_Group_Relationship WHERE UserGroup_Id = " + UserGroup_Id + " AND Menu_Id = " + Menu_Id);
        }

        public static DataSet Get_Info(int Menu_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM  dbo.SC_Menu_Group_Relationship WHERE Menu_Id = " + Menu_Id);
        }
    }
}