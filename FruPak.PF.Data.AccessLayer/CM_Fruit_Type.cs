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
    CM_Fruit_Type Class.
     * 
     * This Class is a data access layer to the CM_Fruit_Type table
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
    public class CM_Fruit_Type
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(FruitType_Id) as Current_Id FROM CM_Fruit_Type");
        }
        public static DataSet Get_Info()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM CM_Fruit_Type ORDER BY Code");
        }
        public static DataSet Get_Info(int FruitType_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM CM_Fruit_Type WHERE FruitType_Id = " + FruitType_Id);
        }

        public static DataSet Get_Info(string Code)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM CM_Fruit_Type WHERE Code = '" + Code + "'");
        }
        public static DataSet Get_Info_B(int Block_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT distinct fv.FruitType_Id, ft.Description, ft.Code " +
                                            "FROM [dbo].[CM_Block_Variety_Relationship] bvr " +
                                            "INNER JOIN dbo.CM_Fruit_Variety fv ON fv.Variety_Id = bvr.Variety_Id " +
                                            "INNER JOIN dbo.CM_Fruit_Type ft ON fv.FruitType_Id = ft.FruitType_Id " +
                                            "WHERE bvr.Block_Id = " + Block_Id);
        }
        public static int Insert(int FruitType_Id, string Code, string Description, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO CM_Fruit_Type(FruitType_Id, Code, Description, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + FruitType_Id + ",'" + Code + "','" + Description + "', GETDATE()," + Mod_User_Id + ")");
        }
        public static int Delete(int FruitType_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM CM_Fruit_Type WHERE FruitType_Id = " + FruitType_Id);
        }
        public static int Update(int FruitType_Id, string Code, string Description, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE CM_Fruit_Type SET Code = '" + Code + "', " +
                                                                  "Description = '" + Description + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE FruitType_Id = " + FruitType_Id);
        }
    }
}
