using FP.Utils.Data;
using System;
using System.Data;

namespace PF.Data.AccessLayer
{
    /*Description
    -----------------
    CM_Block Class.
     *
     * This Class is a data access layer to the CM_Block table
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

    public class CM_Block
    {
        public static DataSet Get_Max_ID()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Block_Id) as Current_Id FROM CM_Block");
        }

        public static DataSet Get_Info()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT B.*, G.Rpin FROM CM_Block B INNER JOIN CM_Grower G on B.Grower_Id = G.Grower_Id WHERE B.PF_Active_Ind = 1 ORDER BY B.Grower_Id, B.Code");
        }

        public static DataSet Get_Info(int Grower_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT B.*, G.Rpin FROM CM_Block B INNER JOIN CM_Grower G on B.Grower_Id = G.Grower_Id WHERE B.PF_Active_Ind = 1 AND  B.Grower_Id = " + Grower_Id);
        }

        public static DataSet Get_Info(int Grower_Id, string Code)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT B.*, G.Rpin FROM CM_Block B INNER JOIN CM_Grower G on B.Grower_Id = G.Grower_Id WHERE B.PF_Active_Ind = 1 AND B.Code = '" + Code + "' AND  B.Grower_Id = " + Grower_Id);
        }

        public static DataSet Get_Info(string Code)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM CM_Block WHERE PF_Active_Ind = 1 AND Code = '" + Code + "'");
        }

        public static string Get_Block(int Block_Id)
        {
            string str_Block = "";
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            DataSet ds = SQLAccessLayer.Run_Query("SELECT Code FROM dbo.CM_Block WHERE PF_Active_Ind = 1 AND Block_Id = " + Block_Id);
            DataRow dr;

            for (int i = 0; i < Convert.ToInt32(ds.Tables[0].Rows.Count.ToString()); i++)
            {
                dr = ds.Tables[0].Rows[i];
                str_Block = dr["Code"].ToString();
            }
            ds.Dispose();
            return str_Block;
        }

        public static int Insert(int Block_Id, int Grower_Id, string Code, string Description, bool PF_Active_Ind, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO CM_Block(Block_Id, Grower_Id, Code, Description, PF_Active_Ind, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + Block_Id + "," + Grower_Id + ",'" + Code + "','" + Description + "','" + PF_Active_Ind + "', GETDATE()," + Mod_User_Id + ")");
        }

        public static int Delete(int Block_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM CM_Block WHERE Block_Id = " + Block_Id);
        }

        public static int Delete_All_for_Grower(int Grower_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM CM_Block WHERE Grower_Id = " + Grower_Id);
        }

        public static int Update(int Block_Id, int Grower_Id, string Code, string Description, bool PF_Active_Ind, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE CM_Block SET Grower_Id = " + Grower_Id + ", " +
                                                                  "Code = '" + Code + "', " +
                                                                  "Description = '" + Description + "', " +
                                                                  "PF_Active_Ind = '" + PF_Active_Ind + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Block_Id = " + Block_Id);
        }
    }
}