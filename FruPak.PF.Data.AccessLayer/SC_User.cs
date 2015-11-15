using FruPak.Utils.Data;
using System.Data;

namespace FruPak.PF.Data.AccessLayer
{
    /*Description
    -----------------
    SC_User Class.
     *
     * This Class is a data access layer to the SC_User table
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

    public class SC_User
    {
        /// <summary>
        /// Get the max value for user_id
        /// </summary>
        /// <returns></returns>
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(user_id) as Current_Id FROM  dbo.SC_User");
        }

        public static DataSet Get_Info()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM  dbo.SC_User ORDER BY  First_Name, Last_Name");
        }

        public static DataSet Get_For_Combo()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT *, First_Name + ' ' + Last_Name as Full_Name FROM  dbo.SC_User ORDER BY  First_Name, Last_Name");
        }

        /// <summary>
        /// Get all user data, for a single user.
        /// </summary>
        /// <param name="Logon"></param>
        /// <returns></returns>
        public static DataSet Get_Info(string Logon)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM  dbo.SC_User WHERE Logon = '" + Logon + "' AND Active_Ind = 1");
        }

        /// <summary>
        /// Insert a new record
        /// </summary>
        /// <param name="User_Id"></param>
        /// <param name="Logon"></param>
        /// <param name="First_Name"></param>
        /// <param name="Last_Name"></param>
        /// <param name="Password"></param>
        /// <param name="Active"></param>
        /// <param name="Mod_date"></param>
        /// <param name="Mod_User_Id"></param>
        /// <returns></returns>
        public static int Insert(int User_Id, string Logon, string First_Name, string Last_Name, string Password, bool Active_Ind, bool Allow_Test, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO  dbo.SC_User(User_Id, Logon, First_Name, Last_Name, Password, Active_Ind, Allow_Test,Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + User_Id + ",'" + Logon + "','" + First_Name + "','" + Last_Name + "','" + Password + "','" +
                                                            Active_Ind + "','" + Allow_Test + "', GETDATE()," + Mod_User_Id + ")");
        }

        public static int Delete(int User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM  dbo.SC_User WHERE User_Id = " + User_Id);
        }

        public static int Update(int User_Id, string First_Name, string Last_Name, bool Active_Ind, bool Allow_Test, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE  dbo.SC_User SET First_Name = '" + First_Name + "', " +
                                                                  "Last_Name = '" + Last_Name + "', " +
                                                                  "Active_Ind = '" + Active_Ind + "', " +
                                                                  "Allow_Test = '" + Allow_Test + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE User_Id = " + User_Id);
        }

        public static int Update(int User_Id, string First_Name, string Last_Name, string Logon, bool Active_Ind, bool Allow_Test, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE  dbo.SC_User SET First_Name = '" + First_Name + "', " +
                                                                  "Last_Name = '" + Last_Name + "', " +
                                                                  "Logon = '" + Logon + "', " +
                                                                  "Active_Ind = '" + Active_Ind + "', " +
                                                                  "Allow_Test = '" + Allow_Test + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE User_Id = " + User_Id);
        }

        public static int Update_User_Password(int User_Id, string Password, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE  dbo.SC_User SET Password = '" + Password + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE User_Id = " + User_Id);
        }

        public static DataSet Get_User_Menus(int User_id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT DISTINCT M.Menu_Id, M.Name AS MENU ,MP.MenuPan_Id, MP.Name AS SUBMENU, MPGR.Write_Access " +
                                            "FROM  dbo.SC_User U " +
                                            "INNER JOIN  dbo.SC_User_Group_Relationship UGR ON U.User_Id = UGR.User_Id " +
                                            "INNER JOIN  dbo.SC_Menu_Group_Relationship MGR ON MGR.UserGroup_Id = UGR.UserGroup_Id " +
                                            "INNER JOIN  dbo.SC_Menu_Panel_Group_Relationship MPGR ON MPGR.UserGroup_Id = UGR.UserGroup_Id " +
                                            "INNER JOIN  dbo.SC_Menu_Panel MP ON MP.MenuPan_Id = MPGR.MenuPan_Id " +
                                            "INNER JOIN  dbo.SC_Menu M ON M.Menu_Id = MGR.Menu_Id AND M.Menu_Id = MP.Menu_Id " +
                                            "WHERE U.User_Id = " + User_id);
        }
    }
}