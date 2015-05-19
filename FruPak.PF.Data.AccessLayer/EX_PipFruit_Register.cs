using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using FruPak.Utils.Data;

namespace FruPak.PF.Data.AccessLayer
{
    public class EX_PipFruit_Register
    {
        public static DataSet Get_Max_ID()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(Register_Id) as Current_Id FROM EX_PipFruit_Register");
        }

        public static int Insert(int Register_Id, string Name, string GrowerID, string Register_Code, string Register_Expirydate, string Register_Regno)
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DECLARE @Value varchar(50) SET @Value = '" + Name + "' " +

            " INSERT INTO EX_PipFruit_Register(Register_Id, Name, GrowerID, Register_Code, Register_Expirydate, Register_Regno) " +
                                                "VALUES ( " + Register_Id + ",  @Value ,'" + GrowerID + "','" + Register_Code + "','" + Register_Expirydate + "','" + Register_Regno + "')");
        }
        public static int Delete()
        {
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM EX_PipFruit_Register");
        }
    }
}