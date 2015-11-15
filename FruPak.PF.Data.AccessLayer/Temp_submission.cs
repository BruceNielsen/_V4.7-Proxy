using FruPak.Utils.Data;
using System.Data;

namespace FruPak.PF.Data.AccessLayer
{
    /*Description
    -----------------
    Temp_submission Class.
     *
     * This Class is a data access layer with anything to do with submissions, that has been considered temperory
     * At some point in time all this should be replaced/moved to the correct class.
     *

    Date        Author     Desc
    -------------------------------------------------------------------------------------------------------------------------------------------------
    02/10/2013  Alison       Creation
    */

    public class Temp_submission
    {
        public static DataSet Get_Info_Translated()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT S.*, T.Code as T_Code, T.Description as T_Description, " +
                                                        "G.Rpin as G_Rpin, " +
                                                        "B.Code as B_Code, B.Description as B_Description " +
                                            "FROM dbo.GH_Submission S " +
                                            "INNER JOIN dbo.CM_Trader T ON T.Trader_Id = S.Trader_Id " +
                                            "INNER JOIN dbo.CM_Grower G ON G.Grower_Id = S.Grower_Id " +
                                            "INNER JOIN dbo.CM_Block B ON B.Block_Id = S.Block_Id " +
                                            "ORDER BY S.Submission_Id DESC");
        }

        public static DataSet BinCard(int Submission_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT S.Submission_Id, B.Barcode, FT.Description AS Fruit,FV.Description AS Variety,S.Sub_date, G.Rpin, BL.Code AS Block, T.Code AS Trader, T.Description AS Full_Trader, B.Print_Ind  " +
                                            "FROM dbo.GH_Submission S " +
                                            "INNER JOIN dbo.CM_Bins B ON B.Submission_Id = S.Submission_Id " +
                                            "INNER JOIN dbo.CM_Material M ON M.Material_Id = B.Material_Id " +
                                            "INNER JOIN dbo.CM_Fruit_Type FT ON FT.FruitType_Id = M.FruitType_Id " +
                                            "INNER JOIN dbo.CM_Fruit_Variety FV ON FV.Variety_Id = M.Variety_Id " +
                                            "INNER JOIN dbo.CM_Grower G ON G.Grower_Id = S.Grower_Id " +
                                            "INNER JOIN dbo.CM_Block BL ON BL.Block_Id = S.Block_Id " +
                                            "INNER JOIN dbo.CM_Trader T ON T.Trader_Id = S.Trader_Id " +
                                            "WHERE S.Submission_Id = " + Submission_Id);
        }

        public static int Update_GDIBarcode(int Bins_Id, string GDIBarcode, int Mod_User_Id)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE CM_Bins SET GDIBarcode = '" + GDIBarcode + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE Bins_Id = " + Bins_Id);
        }
    }
}