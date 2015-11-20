using FP.Utils.Data;
using System.Data;

namespace PF.Data.AccessLayer
{
    /*Description
    -----------------
    SC_Menu_Panel Class.
     *
     * This Class is a data access layer to the SC_Menu_Panel table
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

    public class SC_Menu_Panel
    {
        public static DataSet Get_Max_ID()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(MenuPan_Id) as Current_Id FROM  dbo.SC_Menu_Panel");
        }

        public static int Insert(int MenuPan_Id, int Menu_Id, string Name, string Description, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO  dbo.SC_Menu_Panel(MenuPan_Id, Menu_Id, Name, Description, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + MenuPan_Id + "," + Menu_Id + ",'" + Name + "','" + Description + "', GETDATE()," + Mod_User_Id + ")");
        }

        public static DataSet Get_Info()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT MP.MenuPan_Id, M.Name AS MENU, MP.Name, MP.Description, MP.Mod_date, MP.Mod_User_Id " +
                                            "FROM  dbo.SC_Menu_Panel MP INNER JOIN SC_Menu M ON M.Menu_Id = MP.Menu_Id ORDER BY M.Name, MP.Name");
        }

        public static DataSet Get_Info(int Menu_Id, string str_Name)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT MP.MenuPan_Id, M.Name AS MENU, MP.Name, MP.Description, MP.Mod_date, MP.Mod_User_Id " +
                                            "FROM  dbo.SC_Menu_Panel MP INNER JOIN SC_Menu M ON M.Menu_Id = MP.Menu_Id WHERE MP.Menu_Id = " + Menu_Id + " AND MP.Name = '" + str_Name + "'");
        }

        public static int Update(int MenuPan_Id, int Menu_Id, string Name, string Description, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE  dbo.SC_Menu_Panel SET Menu_Id = " + Menu_Id + ", Name = '" + Name + "', " +
                                                                  "Description = '" + Description + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE MenuPan_Id = " + MenuPan_Id);
        }

        public static int Delete(int MenuPan_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM  dbo.SC_Menu_Panel WHERE MenuPan_Id = " + MenuPan_Id);
        }
    }
}