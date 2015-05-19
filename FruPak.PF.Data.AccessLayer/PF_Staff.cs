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
    PF_Staff Class.
     * 
     * This Class is a data access layer to the PF_Staff table
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
    public class PF_Staff
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Staff_Id) as Current_Id FROM PF_Staff");
        }
        public static DataSet Get_Info()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT *, First_Name + ' ' + Last_Name as Name FROM PF_Staff WHERE PF_Active_Ind = 1 ORDER BY PF_Active_Ind DESC");
        }
        public static DataSet Get_Info_for_multi_selectbox()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT convert(varchar(10),Staff_Id) + '-' + First_Name + ' ' + Last_Name as Name FROM PF_Staff WHERE PF_Active_Ind = 1 ORDER BY PF_Active_Ind DESC");
        }
        public static DataSet Get_Info_ALL()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT *, First_Name + ' ' + Last_Name as Name FROM PF_Staff ORDER BY PF_Active_Ind DESC");
        }
        public static DataSet Check_Employee_Num(int Emp_Number)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT *, First_Name + ' ' + Last_Name as Name FROM PF_Staff WHERE Emp_Number = " + Emp_Number);
        }

        public static DataSet Get_Info(int Staff_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT *, First_Name + ' ' + Last_Name as Name FROM PF_Staff WHERE PF_Active_Ind = 1 AND Staff_Id = " + Staff_Id);
        }
        public static DataSet Get_Info_Perm_Staff()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT *, First_Name + ' ' + Last_Name as Name FROM PF_Staff WHERE UPPER(Emp_Type) = 'P' AND PF_Active_Ind = 1");
        }
        public static int Insert(int Staff_Id, string First_Name, string Last_Name, int Emp_Number, decimal Hourly_Rate, bool PF_Active_Ind, string Emp_Type, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_Staff(Staff_Id, First_Name, Last_Name, Emp_Number, Emp_Type, Hourly_Rate, PF_Active_Ind, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + Staff_Id + ",'" + First_Name + "','" + Last_Name + "'," + Emp_Number + ",'" + Emp_Type + "'," + Hourly_Rate + ",'" + PF_Active_Ind + "', GETDATE()," + Mod_User_Id + ")");
        }
        public static int Update(int Staff_Id, string First_Name, string Last_Name, int Emp_Number, decimal Hourly_Rate, bool PF_Active_Ind, string Emp_Type, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_Staff SET First_Name = '" + First_Name + "' ," +
                                                                   "Last_Name = '" + Last_Name + "' ," +
                                                                   "Emp_Number = " + Emp_Number + " ," +
                                                                   "Hourly_Rate = " + Hourly_Rate + " ," +
                                                                   "PF_Active_Ind = '" + PF_Active_Ind + "' ," +
                                                                   "Emp_Type = '" + Emp_Type + "' ," +
                                                                   "Mod_Date = GETDATE() ," +
                                                                   "Mod_User_Id = " + Mod_User_Id + " " +
                                                                   "WHERE Staff_Id = " + Staff_Id);
        }
        public static int Update_Active(int Staff_Id, bool PF_Active_Ind, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_Staff SET PF_Active_Ind = '" + PF_Active_Ind + "' ," +
                                                                   "Mod_Date = GETDATE() ," +
                                                                   "Mod_User_Id = " + Mod_User_Id + " " +
                                                                   "WHERE Staff_Id = " + Staff_Id);
        }
    }
}