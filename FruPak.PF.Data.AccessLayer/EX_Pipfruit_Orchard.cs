using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using FruPak.Utils.Data;

namespace FruPak.PF.Data.AccessLayer
{
    public class EX_Pipfruit_Orchard
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Orchard_Id) as Current_Id FROM EX_Pipfruit_Orchard");
        }

        public static int Insert(int Orchard_Id, string Name, string RPIN, string prodsite_code, string mgmtarea_code, string mgmtarea_contact, string mgmtarea_program, string Mgmtarea_GrowerID, string Register_code, string Register_expirydate, string Register_regno, string variety_code)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DECLARE @Value varchar(50) SET @Value = '" + Name + "' " +

            " INSERT INTO EX_Pipfruit_Orchard(Orchard_Id, Name, RPIN, prodsite_code, mgmtarea_code, mgmtarea_contact, mgmtarea_program, Mgmtarea_GrowerID, Register_code,Register_expirydate,Register_regno,variety_code) " +
                                                "VALUES ( " + Orchard_Id + ",  @Value ,'" + RPIN + "','" + prodsite_code + "','" + 
                                                              mgmtarea_code + "','" + mgmtarea_contact + "','" + mgmtarea_program + "','" + Mgmtarea_GrowerID + "','" +
                                                              Register_code + "','" + Register_expirydate + "','" + Register_regno + "','" + variety_code + "')");
        }
        public static DataSet Get_Distinct_RPINS()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT DISTINCT RPIN, PO.Name, PR.Register_Regno AS GAP, PR.name as Orchardist " + 
                                            "FROM EX_Pipfruit_Orchard PO inner join EX_PipFruit_Register PR ON PO.Mgmtarea_GrowerID = PR.GrowerID");
        }
        public static DataSet Get_Blocks(string RPIN)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT DISTINCT RPIN, PO.Name, PO.Prodsite_code,po.Mgmtarea_code FROM EX_Pipfruit_Orchard PO WHERE RPIN = '" + RPIN + "'");
        }

        public static DataSet Get_Variety(string RPIN, string Prodsite_code, string Mgmtarea_code)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT DISTINCT RPIN, PO.Name, PO.Prodsite_code,po.Mgmtarea_code, FV.Variety_Id, PO.Variety_code, PO.Variety_code, PO.Variety_code, FV.Description, FT.Code "+
                                            "FROM EX_Pipfruit_Orchard PO INNER JOIN dbo.CM_Fruit_Variety FV ON PO.Variety_code = FV.Code Inner join dbo.CM_Fruit_Type FT on FT.FruitType_Id = FV.FruitType_Id " +
                                            "WHERE RPIN = '" + RPIN + "' and PO.Prodsite_code = '" + Prodsite_code + "' and po.Mgmtarea_code = '" + Mgmtarea_code + "' ORDER BY PO.Variety_code ");
        }
        public static int Delete()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM EX_Pipfruit_Orchard");
        }
    }
}
