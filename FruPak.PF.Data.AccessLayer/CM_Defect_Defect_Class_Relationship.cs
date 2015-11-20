using FP.Utils.Data;
using System.Data;

namespace PF.Data.AccessLayer
{
    /*Description
    -----------------
    CM_Defect_Defect_Class_Relationship Class.
     *
     * This Class is a data access layer to the CM_Defect_Defect_Class_Relationship table
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

    public class CM_Defect_Defect_Class_Relationship
    {
        public static DataSet Get_Max_ID()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(DefectDefectClass_Relat_Id) as Current_Id FROM CM_Defect_Defect_Class_Relationship");
        }

        public static int Insert(int DefectDefectClass_Relat_Id, int Defect_Id, int DefectClass_Id, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO CM_Defect_Defect_Class_Relationship(DefectDefectClass_Relat_Id, Defect_Id, DefectClass_Id, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + DefectDefectClass_Relat_Id + "," + Defect_Id + "," + DefectClass_Id + ", GETDATE()," + Mod_User_Id + ")");
        }

        public static DataSet Get_Info_By_Defect(int Defect_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM CM_Defect_Defect_Class_Relationship WHERE Defect_Id = " + Defect_Id);
        }

        public static DataSet Get_Info_By_DefectClass(int DefectClass_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM CM_Defect_Defect_Class_Relationship WHERE DefectClass_Id = " + DefectClass_Id);
        }

        public static int Delete(int DefectDefectClass_Relat_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM CM_Defect_Defect_Class_Relationship WHERE DefectDefectClass_Relat_Id = " + DefectDefectClass_Relat_Id);
        }

        public static int Delete_Defect_From_Class(int Defect_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM CM_Defect_Defect_Class_Relationship WHERE Defect_Id = " + Defect_Id);
        }

        public static int Delete_Defect_From_Class(int Defect_Id, int DefectClass_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM CM_Defect_Defect_Class_Relationship WHERE Defect_Id = " + Defect_Id + " AND DefectClass_Id = " + DefectClass_Id);
        }

        public static int Delete_Class_From_Defect(int DefectClass_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM CM_Defect_Defect_Class_Relationship WHERE DefectClass_Id = " + DefectClass_Id);
        }
    }
}