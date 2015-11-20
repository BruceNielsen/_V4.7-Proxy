using FP.Utils.Data;
using System.Data;

namespace PF.Data.AccessLayer
{
    /*Description
    -----------------
    CM_Pack_Type_Weight Class.
     *
     * This Class is a data access layer to the CM_Pack_Type_Weight table
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

    public class CM_Pack_Type_Weight
    {
        public static DataSet Get_Max_ID()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(PackType_Weight_Id) as Current_Id FROM CM_Pack_Type_Weight");
        }

        public static DataSet Get_Info()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM CM_Pack_Type_Weight WHERE PF_Active_Ind = 1 ");
        }

        public static DataSet Get_Info(int PackType_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM CM_Pack_Type_Weight WHERE PF_Active_Ind = 1 AND PackType_Id = " + PackType_Id);
        }

        public static DataSet Get_Tare_Weight_for_Material(decimal Material_Number)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT PTW.Weight_Tare, M.Material_Num " +
                                            "FROM dbo.CM_Pack_Type_Weight PTW " +
                                            "INNER JOIN dbo.CM_Material M on M.PackType_Id = PTW.PackType_Id " +
                                            "WHERE M.Material_Num = " + Material_Number);
        }

        public static DataSet Get_Tare_Weight_for_Material_Id(int Material_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT PTW.Weight_Tare, M.Material_Num " +
                                            "FROM dbo.CM_Pack_Type_Weight PTW " +
                                            "INNER JOIN dbo.CM_Material M on M.PackType_Id = PTW.PackType_Id " +
                                            "WHERE M.Material_Id = " + Material_Id);
        }

        public static int Insert(int PackType_Weight_Id, int PackType_Id, decimal Weight_Tare, bool PF_Active_Ind, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO CM_Pack_Type_Weight(PackType_Weight_Id, PackType_Id, Weight_Tare, PF_Active_Ind, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + PackType_Weight_Id + "," + PackType_Id + "," + Weight_Tare + ",'" + PF_Active_Ind + "', GETDATE()," + Mod_User_Id + ")");
        }

        public static int Delete(int PackType_Weight_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM CM_Pack_Type_Weight WHERE PackType_Weight_Id = " + PackType_Weight_Id);
        }

        public static int Update(int PackType_Weight_Id, int PackType_Id, decimal Weight_Tare, bool PF_Active_Ind, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE CM_Pack_Type_Weight SET PackType_Id = " + PackType_Id + ", " +
                                                                  "Weight_Tare = " + Weight_Tare + ", " +
                                                                  "PF_Active_Ind = '" + PF_Active_Ind + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE PackType_Weight_Id = " + PackType_Weight_Id);
        }
    }
}