﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using FruPak.Utils.Data;

namespace FruPak.PF.Data.AccessLayer
{
    /*Description
    -----------------
    SC_User_Groups Class.
     * 
     * This Class is a data access layer to the SC_User_Groups table
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
    public class SC_User_Groups
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(UserGroup_Id) as Current_Id FROM  dbo.SC_User_Groups");
        }
        public static DataSet Get_Info()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM  dbo.SC_User_Groups");
        }
        public static DataSet Get_Info(string str_Name)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM  dbo.SC_User_Groups WHERE Name = '" + str_Name + "'");
        }
        public static int Insert(int UserGroup_Id, string Name, string Description, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO  dbo.SC_User_Groups(UserGroup_Id, Name, Description, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + UserGroup_Id + ",'" + Name + "','" + Description + "', GETDATE()," + Mod_User_Id + ")");
        }
        public static int Delete(int UserGroup_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM  dbo.SC_User_Groups WHERE UserGroup_Id = " + UserGroup_Id);
        }
        public static int Update(int UserGroup_Id, string Name, string Description, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE  dbo.SC_User_Groups SET Name = '" + Name + "', " +
                                                                  "Description = '" + Description + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE UserGroup_Id = " + UserGroup_Id);
        }
    }
}