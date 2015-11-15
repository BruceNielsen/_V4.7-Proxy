using FruPak.Utils.Data;
using System.Data;

namespace FruPak.PF.Data.AccessLayer
{
    /*Description
    -----------------
    CM_Cities Class.
     *
     * This Class is a data access layer to the CM_Cities table
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

    public class CM_Defect
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Defect_Id) as Current_Id FROM CM_Defect");
        }

        public static DataSet Get_Info()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT *, Code + ' - ' + Description as Combined FROM CM_Defect WHERE PF_Active_Ind = 1 ORDER BY Code");
        }

        public static DataSet Get_Info(int Defect_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM CM_Defect WHERE PF_Active_Ind = 1 AND Defect_Id = " + Defect_Id);
        }

        public static DataSet Get_Info(string Code)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM CM_Defect WHERE PF_Active_Ind = 1 AND Code = '" + Code + "'");
        }

        public static DataSet Get_Info_BY_Defect_Class(string DC_Code)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT D.*, DC.Code as DC_Code " +
                                            "FROM CM_Defect D " +
                                            "INNER JOIN dbo.CM_Defect_Defect_Class_Relationship DDCR ON DDCR.Defect_Id = D.Defect_Id " +
                                            "INNER JOIN dbo.CM_Defect_Class DC ON DC.DefectClass_Id = DDCR.DefectClass_Id " +
                                            "WHERE D.PF_Active_Ind = 1 AND DC.Code = '" + DC_Code + "'");
        }

        public static int Insert(int Defect_Id, string Code, string Description, bool PF_Active_Ind, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO CM_Defect(Defect_Id, Code, Description,  PF_Active_Ind, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + Defect_Id + ",'" + Code + "','" + Description + "','" + PF_Active_Ind + "', GETDATE()," + Mod_User_Id + ")");
        }

        public static int Delete(int Defect_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM CM_Defect WHERE Defect_Id = " + Defect_Id);
        }

        public static int Update(int Defect_Id, string Code, string Description, bool PF_Active_Ind, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE CM_Defect SET Code = '" + Code + "', " +
                                                                  "Description = '" + Description + "', " +
                                                                  "PF_Active_Ind = '" + PF_Active_Ind + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Defect_Id = " + Defect_Id);
        }
    }
}