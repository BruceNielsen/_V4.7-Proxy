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
    CM_Fruit_Variety Class.
     * 
     * This Class is a data access layer to the CM_Fruit_Variety table
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
    public class CM_Fruit_Variety
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Variety_Id) as Current_Id FROM CM_Fruit_Variety");
        }
        public static DataSet Get_Info()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT FV.Variety_Id, FT.Code as Fruit, FV.Code as Variety, FV.Description, FV.Mod_date, FV.Mod_User_Id " +
                                            "FROM CM_Fruit_Variety FV " +
                                            "INNER JOIN CM_Fruit_Type FT ON FT.FruitType_Id = FV.FruitType_Id " +
                                            "ORDER BY FT.Code, FV.Code");
        }
        public static DataSet Get_Info(string str_code)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT FV.Variety_Id, FT.Code as Fruit, FV.Code as Variety, FV.Description, FV.Mod_date, FV.Mod_User_Id " +
                                            "FROM CM_Fruit_Variety FV " +
                                            "INNER JOIN CM_Fruit_Type FT ON FT.FruitType_Id = FV.FruitType_Id " +
                                            "WHERE FV.Code = '" + str_code + "'" +
                                            "ORDER BY FV.Code");
        }
        public static DataSet Get_Info(int Fruit_Type_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT FV.Variety_Id, FT.Code as Fruit, FV.Code as Variety, FV.Description, FV.Mod_date, FV.Mod_User_Id " +
                                            "FROM CM_Fruit_Variety FV " +
                                            "INNER JOIN CM_Fruit_Type FT ON FT.FruitType_Id = FV.FruitType_Id " +
                                            "WHERE FT.FruitType_Id = " + Fruit_Type_Id  +
                                            "ORDER BY FV.Code");
        }

        public static DataSet Get_Info_std(int Variety_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM CM_Fruit_Variety WHERE Variety_Id = " + Variety_Id);
        }
        public static DataSet Get_non_pipfruit()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT FV.Variety_Id, FV.code as fvcode,FV.Description,FT.Code as ftcode " +
                                            "FROM dbo.CM_Fruit_Variety FV " +
                                            "INNER JOIN dbo.CM_Fruit_Type FT on FT.FruitType_Id = FV.FruitType_Id " +
                                            "WHERE FT.Code <> 'Apples'");
        }
        public static int Insert(int Variety_Id, int FruitType_Id, string Code, string Description, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO CM_Fruit_Variety(Variety_Id, FruitType_Id, Code, Description, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + Variety_Id + "," + FruitType_Id + ",'" + Code + "','" + Description + "', GETDATE()," + Mod_User_Id + ")");
        }
        public static int Delete(int Variety_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM CM_Fruit_Variety WHERE Variety_Id = " + Variety_Id);
        }
        public static int Update(int Variety_Id, int FruitType_Id, string Code, string Description, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE CM_Fruit_Variety SET FruitType_Id = " + FruitType_Id + ", Code = '" + Code + "', " +
                                                                  "Description = '" + Description + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Variety_Id = " + Variety_Id);
        }
    }
}