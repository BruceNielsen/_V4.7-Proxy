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
    CM_Growers Class.
     * 
     * This Class is a data access layer to the CM_Growers table
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
    public class CM_Grower
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Grower_Id) as Current_Id FROM CM_Grower");
        }
        public static DataSet Get_Info()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT G.*, O.Description FROM dbo.CM_Grower G LEFT OUTER JOIN dbo.CM_Orchardist O ON O.Orchardist_Id = G.Orchardist_Id WHERE G.PF_Active_Ind = 1 ORDER BY RPIN");
        }
        public static DataSet Get_Info(string RPIN)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT G.*, O.Description FROM dbo.CM_Grower G INNER JOIN dbo.CM_Orchardist O ON O.Orchardist_Id = G.Orchardist_Id WHERE G.PF_Active_Ind = 1 AND RPIN = '" + RPIN + "'");
        }
        public static DataSet Get_Info(int Orchardist_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT G.*, O.Description FROM dbo.CM_Grower G INNER JOIN dbo.CM_Orchardist O ON O.Orchardist_Id = G.Orchardist_Id WHERE G.PF_Active_Ind = 1 AND G.Orchardist_Id = " + Orchardist_Id);
        }
        public static DataSet Get_Orchardist(int Grower_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM dbo.CM_Grower WHERE Grower_Id = " + Grower_Id);
        }
        public static string Get_RPIN(int Grower_Id)
        {
            string str_RPIN = "";
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            DataSet ds = SQLAccessLayer.Run_Query("SELECT RPIN FROM dbo.CM_Grower WHERE GROWER_Id = " + Grower_Id);
            DataRow dr;

            for (int i = 0; i < Convert.ToInt32(ds.Tables[0].Rows.Count.ToString()); i++)
            {
                dr = ds.Tables[0].Rows[i];
                str_RPIN = dr["RPIN"].ToString();
            }
            ds.Dispose();
            return str_RPIN;
        }

        public static int Insert(int Grower_Id, string Rpin, int Orchardist_Id, decimal Global_Gap_Number, decimal GST, bool PF_Active_Ind, int Customer_Id, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO CM_Grower(Grower_Id, Rpin, Orchardist_Id, Global_Gap_Number, GST, PF_Active_Ind, Customer_Id, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + Grower_Id + ",'" + Rpin + "'," + Orchardist_Id + "," + Global_Gap_Number + "," + GST + ",'" + PF_Active_Ind + "'," + Customer_Id + ", GETDATE()," + Mod_User_Id + ")");
        }
        public static int Delete(int Grower_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM CM_Grower WHERE Grower_Id = " + Grower_Id);
        }
        public static int Update(int Grower_Id, string Rpin, int Orchardist_Id, decimal Global_Gap_Number, decimal GST, bool PF_Active_Ind, int Mod_User_Id, int Customer_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE CM_Grower SET Rpin = '" + Rpin + "', " +
                                                                  "Orchardist_Id = " + Orchardist_Id + ", " +
                                                                  "Global_Gap_Number = " + Global_Gap_Number + ", " +
                                                                  "Customer_Id = " + Customer_Id + ", " +
                                                                  "GST = " + GST + ", " +
                                                                  "PF_Active_Ind = '" + PF_Active_Ind + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Grower_Id = " + Grower_Id);
        }
    }
}