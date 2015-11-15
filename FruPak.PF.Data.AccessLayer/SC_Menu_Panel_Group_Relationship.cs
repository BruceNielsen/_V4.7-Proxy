using FruPak.Utils.Data;
using System.Data;

namespace FruPak.PF.Data.AccessLayer
{
    /*Description
    -----------------
    SC_Menu_Panel_Group_Relationship Class.
     *
     * This Class is a data access layer to the SC_Menu_Panel_Group_Relationship table
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

    public class SC_Menu_Panel_Group_Relationship
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(MnPGrp_Relat_Id) as Current_Id FROM  dbo.SC_Menu_Panel_Group_Relationship");
        }

        public static int Insert(int MnPGrp_Relat_Id, int UserGroup_Id, int MenuPan_Id, bool Write_Access, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO  dbo.SC_Menu_Panel_Group_Relationship(MnPGrp_Relat_Id, UserGroup_Id, MenuPan_Id, Write_Access, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + MnPGrp_Relat_Id + "," + UserGroup_Id + "," + MenuPan_Id + ",'" + Write_Access + "', GETDATE()," + Mod_User_Id + ")");
        }

        public static int Delete(int MenuPan_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM  dbo.SC_Menu_Panel_Group_Relationship WHERE MenuPan_Id = " + MenuPan_Id);
        }

        public static DataSet Get_Info(int MenuPan_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM  dbo.SC_Menu_Panel_Group_Relationship WHERE MenuPan_Id = " + MenuPan_Id);
        }
    }
}