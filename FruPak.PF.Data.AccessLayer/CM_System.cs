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
    CM_System Class.
     * 
     * This Class is a data access layer to the CM_System table
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
    public class CM_System
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(System_Id) as Current_Id FROM CM_System");
        }
        public static DataSet Get_Info()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT *, Code + ' - ' + Description as Combined FROM CM_System ORDER BY Code");
        }
        public static DataSet Get_Info(int System_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM CM_System WHERE System_Id = " + System_Id );
        }
        public static DataSet Get_Info_Like(string Code)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM CM_System WHERE Code Like '" + Code + "'");
        }
        #region ------------- Stored Procedures -------------
            #region ------------- Select -------------
                public static DataSet Get_Info_For_Code(string code)
                {
                    FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
                    return SQLAccessLayer.RunSP_Query("dbo.CM_System_Get_Info_For_Code", code);
                }
            #endregion
        #endregion
    }
}
