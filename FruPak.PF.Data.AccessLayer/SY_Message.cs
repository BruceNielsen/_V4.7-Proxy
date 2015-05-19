using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using FruPak.Utils.Data;

namespace FruPak.PF.Data.AccessLayer
{
    /*Description
    -----------------
    SY_Message Class.
     * 
     * This Class is a data access layer to the SY_Message table
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
    public class SY_Message
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Message_Id) as Current_Id FROM SY_Message");
        }
        public static DataSet Get_Info()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM dbo.SY_Message ORDER BY END_DATE");
        }
        public static DataSet Get_Info_Translated()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT M.*, U.First_Name + ' ' + U.Last_Name as Name " +
                                            "FROM dbo.SY_Message M " +
                                            "LEFT OUTER JOIN dbo.SC_User U on U.User_Id = M.User_Id " +
                                            "ORDER BY M.User_Id, END_DATE");
        }

        public static int Delete(int Message_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM  dbo.SY_Message WHERE Message_Id = " + Message_Id);
        }
        public static int Insert(int Message_Id, int User_Id, string Message, string End_Date, string Repeat_ind)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO SY_Message(Message_Id, User_Id, Message, End_Date, Repeat_ind) " +
                                                "VALUES ( " + Message_Id + "," + User_Id + ",'" + Message + "','" + End_Date + "','" + Repeat_ind + "')");
        }
        public static int Update_End_Date(int Message_Id, string End_Date)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE SY_Message  SET End_Date = '" + End_Date + "' " +
                                              " WHERE Message_Id = " + Message_Id);
        }
        public static int Update(int Message_Id, int User_Id, string Message, string End_Date, string Repeat_ind)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("Update SY_Message SET User_Id = " + User_Id + ", " +
                                                                     "Message = '" + Message + "', " +
                                                                     "End_Date = '" + End_Date + "', " +
                                                                     "Repeat_ind = '" + Repeat_ind + "' " +
                                               "WHERE Message_Id = " + Message_Id);
        }
    }
}
