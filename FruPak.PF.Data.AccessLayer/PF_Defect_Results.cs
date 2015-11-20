using FP.Utils.Data;
using System.Data;

namespace PF.Data.AccessLayer
{
    /*Description
    -----------------
    PF_Defect_Results Class.
     *
     * This Class is a data access layer to the PF_Defect_Results table
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

    public class PF_Defect_Results
    {
        public static DataSet Get_Max_ID()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(DefectResult_Id) as Current_Id FROM PF_Defect_Results");
        }

        public static DataSet Get_Info_Translated(int Work_Order_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT DR.*, WO.Process_Date, G.Rpin, D.Code, D.Description " +
                                            "FROM dbo.PF_Defect_Results DR " +
                                            "INNER JOIN dbo.PF_Work_Order WO ON WO.Work_Order_Id = DR.Work_Order_Id " +
                                            "INNER JOIN dbo.CM_Grower G ON G.Grower_Id = WO.Grower_Id " +
                                            "INNER JOIN dbo.CM_Defect D ON D.Defect_Id = DR.Defect_Id " +
                                            "WHERE DR.Work_Order_Id = " + Work_Order_Id);
        }

        public static int Insert(int DefectResult_Id, int Work_Order_Id, int Defect_Id, decimal num_found, string Comments, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_Defect_Results(DefectResult_Id, Work_Order_Id, Defect_Id, num_found, Comments, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + DefectResult_Id + "," + Work_Order_Id + "," + Defect_Id + "," + num_found + ",'" + Comments + "', GETDATE()," + Mod_User_Id + ")");
        }

        public static int Update(int DefectResult_Id, int Work_Order_Id, int Defect_Id, decimal num_found, string Comments, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_Defect_Results SET Work_Order_Id = " + Work_Order_Id + ", " +
                                                                  "Defect_Id = " + Defect_Id + ", " +
                                                                  "num_found = " + num_found + ", " +
                                                                  "Comments = '" + Comments + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE DefectResult_Id = " + DefectResult_Id);
        }

        public static int Delete(int DefectResult_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Defect_Results WHERE DefectResult_Id = " + DefectResult_Id);
        }
    }
}