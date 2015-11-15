using FruPak.Utils.Data;
using System.Data;

namespace FruPak.PF.Data.AccessLayer
{
    /*Description
    -----------------
    CM_Block_Variety_Relationship Class.
     *
     * This Class is a data access layer to the CM_Block_Variety_Relationship table
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

    public class CM_Block_Variety_Relationship
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(BlkVar_Relat_Id) as Current_Id FROM CM_Block_Variety_Relationship");
        }

        public static int Insert(int BlkVar_Relat_Id, int Block_Id, int Variety_Id, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO CM_Block_Variety_Relationship(BlkVar_Relat_Id, Block_Id, Variety_Id, Mod_Date, Mod_User_Id) " +
                                                "VALUES ( " + BlkVar_Relat_Id + "," + Block_Id + "," + Variety_Id + ", GETDATE()," + Mod_User_Id + ")");
        }

        public static DataSet Get_Info(int Block_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT BlkVar_Relat_Id, BVR.Block_Id, B.Code as BLOCK, BVR.Variety_Id, FV.Code as Variety, FV.Description as Description, BVR.Mod_Date, BVR.Mod_User_Id " +
                                            "FROM CM_Block_Variety_Relationship BVR " +
                                            "INNER JOIN CM_Block B ON B.Block_Id = BVR.Block_Id " +
                                            "INNER JOIN CM_Fruit_Variety FV ON FV.Variety_Id = BVR.Variety_Id " +
                                            "WHERE BVR.Block_Id = " + Block_Id);
        }

        public static DataSet Get_Info(int Block_Id, int Variety_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT BlkVar_Relat_Id, BVR.Block_Id, B.Code as BLOCK, BVR.Variety_Id, FV.Code as Variety, FV.Description as Description, BVR.Mod_Date, BVR.Mod_User_Id " +
                                            "FROM CM_Block_Variety_Relationship BVR " +
                                            "INNER JOIN CM_Block B ON B.Block_Id = BVR.Block_Id " +
                                            "INNER JOIN CM_Fruit_Variety FV ON FV.Variety_Id = BVR.Variety_Id " +
                                            "WHERE BVR.Block_Id = " + Block_Id + " AND BVR.Variety_Id = " + Variety_Id);
        }

        public static DataSet Get_Info_Varieties(int Block_Id, int FruitType_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT BVR.[BlkVar_Relat_Id], BVR.[Block_Id] ,BVR.[Variety_Id] ,FV.Code as Variety ,FV.Description as Description ,BVR.[Mod_Date] ,BVR.[Mod_User_Id] ,FV.FruitType_Id " +
                                            "FROM [dbo].[CM_Block_Variety_Relationship] BVR " +
                                            "INNER JOIN dbo.CM_Fruit_Variety FV ON BVR.Variety_Id = FV.Variety_Id " +
                                            "WHERE BVR.Block_Id = " + Block_Id + " AND FV.FruitType_Id = " + FruitType_Id);
        }

        public static int Delete(int BlkVar_Relat_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM CM_Block_Variety_Relationship WHERE BlkVar_Relat_Id = " + BlkVar_Relat_Id);
        }

        public static int Delete_For_Block(int Block_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM CM_Block_Variety_Relationship WHERE Block_Id = " + Block_Id);
        }
    }
}