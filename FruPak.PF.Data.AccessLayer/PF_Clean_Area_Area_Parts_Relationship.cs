using FruPak.Utils.Data;
using System.Data;

namespace FruPak.PF.Data.AccessLayer
{
    /*Description
    -----------------
    PF_Clean_Area_Area_Parts_Relationship Class.
     *
     * This Class is a data access layer to the PF_Clean_Area_Area_Parts_Relationship table
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

    public class PF_Clean_Area_Area_Parts_Relationship
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(CleanAreaParts_Relat_Id) as Current_Id FROM PF_Clean_Area_Area_Parts_Relationship");
        }

        public static DataSet Get_Info_Area(int CleanArea_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Clean_Area_Area_Parts_Relationship WHERE CleanArea_Id = " + CleanArea_Id);
        }

        public static DataSet Get_Info_Area_Translated(int CleanArea_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("select * FROM dbo.PF_Clean_Area_Area_Parts_Relationship CAAPR INNER JOIN dbo.PF_Cleaning_Area_Parts CAP ON CAP.CleanAreaParts_Id = CAAPR.CleanAreaParts_Id " +
                                            "WHERE CAAPR.CleanArea_Id = " + CleanArea_Id + " ORDER BY CAP.CleanAreaParts_Id");
        }

        public static DataSet Get_Info_Parts(int CleanAreaParts_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM PF_Clean_Area_Area_Parts_Relationship WHERE CleanAreaParts_Id = " + CleanAreaParts_Id);
        }

        public static int Insert(int CleanAreaParts_Relat_Id, int CleanArea_Id, int CleanAreaParts_Id, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_Clean_Area_Area_Parts_Relationship(CleanAreaParts_Relat_Id, CleanArea_Id, CleanAreaParts_Id, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + CleanAreaParts_Relat_Id + "," + CleanArea_Id + "," + CleanAreaParts_Id + ", GETDATE()," + Mod_User_Id + ")");
        }

        public static int Delete_Cleaning_Area(int CleanArea_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Clean_Area_Area_Parts_Relationship WHERE CleanArea_Id = " + CleanArea_Id);
        }

        public static int Delete_Cleaning_Area_Part(int CleanArea_Id, int CleanAreaParts_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Clean_Area_Area_Parts_Relationship WHERE CleanArea_Id = " + CleanArea_Id + " AND CleanAreaParts_Id = " + CleanAreaParts_Id);
        }

        public static int Delete_Cleaning_Part(int CleanAreaParts_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Clean_Area_Area_Parts_Relationship WHERE CleanAreaParts_Id = " + CleanAreaParts_Id);
        }

        public static int Delete(int CleanAreaParts_Relat_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Clean_Area_Area_Parts_Relationship WHERE CleanAreaParts_Relat_Id = " + CleanAreaParts_Relat_Id);
        }
    }
}