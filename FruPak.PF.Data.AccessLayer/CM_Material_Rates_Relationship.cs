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
    CM_Material_Rates_Relationship Class.
     * 
     * This Class is a data access layer to the CM_Material_Rates_Relationship table
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
    public class CM_Material_Rates_Relationship
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(MaterialRates_Relat_Id) as Current_Id FROM CM_Material_Rates_Relationship");
        }
        public static DataSet Get_Info_For_Material(int Material_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM CM_Material_Rates_Relationship WHERE Material_Id = " + Material_Id);
        }
        public static int Insert(int MaterialRates_Relat_Id, int Material_Id, int Rates_Id, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO CM_Material_Rates_Relationship(MaterialRates_Relat_Id, Material_Id, Rates_Id, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + MaterialRates_Relat_Id + "," + Material_Id + "," + Rates_Id + ", GETDATE()," + Mod_User_Id + ")");
        }
        public static int Delete_Material(int Material_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM CM_Material_Rates_Relationship WHERE Material_Id = " + Material_Id);
        }
        public static int Delete_Rates(int Rates_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM CM_Material_Rates_Relationship WHERE Rates_Id = " + Rates_Id);
        }
    }
}
