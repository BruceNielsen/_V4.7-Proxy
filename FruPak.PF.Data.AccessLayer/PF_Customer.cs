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
    PF_Customer Class.
     * 
     * This Class is a data access layer to the PF_Customer table
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
    public class PF_Customer
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Customer_Id) as Current_Id FROM PF_Customer");
        }
        public static DataSet Get_Info()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT *, CONVERT(varchar(10), Customer_Id) + ' - ' + Name as Combined FROM PF_Customer WHERE PF_Active_Ind = 1 ORDER BY Name");
        }
        public static DataSet Get_Info(int Customer_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            Console.WriteLine("SELECT * FROM PF_Customer WHERE Customer_Id = " + Customer_Id);
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Customer WHERE Customer_Id = " + Customer_Id);
        }
        public static DataSet Get_Info(string Outlook_Key)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            Console.WriteLine("SELECT * FROM PF_Customer WHERE Outlook_Key = '" + Outlook_Key + "'");
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Customer WHERE Outlook_Key = '" + Outlook_Key + "'");
        }
        public static int Insert(int Customer_Id, string Name, string Outlook_Key, bool PF_Active_Ind, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_Customer(Customer_Id,  Name, Outlook_Key, PF_Active_Ind, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + Customer_Id + ",'" + Name + "','" + Outlook_Key + "','" + PF_Active_Ind + "', GETDATE()," + Mod_User_Id + ")");
        }
        public static int Delete(int Customer_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Customer WHERE Customer_Id = " + Customer_Id);
        }
        public static int Update(int Customer_Id, string Name, string Outlook_Key, bool PF_Active_Ind, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_Customer SET Name = '" + Name + "', " +
                                                                  "Outlook_Key = '" + Outlook_Key + "', " +
                                                                  "PF_Active_Ind = '" + PF_Active_Ind + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Customer_Id = " + Customer_Id);
        }
    }
}