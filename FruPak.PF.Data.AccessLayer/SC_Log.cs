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
    SC_Log Class.
     * 
     * This Class is a data access layer to the SC_Log table
     * Where possible the following standard method names are used and standard column names used.
     *  1. Variable names as input to a method are the same as the column names they refer to.
     *
     * Methods.
     *  Get_Max_ID: Return the max ID from the table, and is stored as Current_Id
     *  Insert:     Inserts a new record


    Date        Author     Desc
    -------------------------------------------------------------------------------------------------------------------------------------------------
    01/09/2013  Dave       Creation
    */
    public class SC_Log
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Log_Id) as Current_Id FROM  dbo.SC_Log");
        }
        public static int Insert(int Log_Id, int User_Id, int Action_Id, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO  dbo.SC_Log(Log_Id, User_Id, Action_Id, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + Log_Id + "," + User_Id + "," + Action_Id + ", GETDATE()," + Mod_User_Id + ")");
        }
    }
}