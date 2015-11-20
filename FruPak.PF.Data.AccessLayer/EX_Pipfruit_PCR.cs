using FP.Utils.Data;
using System.Data;

namespace PF.Data.AccessLayer
{
    public class EX_Pipfruit_PCR
    {
        public static DataSet Get_Max_ID()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(PCR_Id) as Current_Id FROM EX_Pipfruit_PCR");
        }

        public static int Insert(int PCR_Id, string RPIN, string Variety, string Mgmtarea, string Growing_Method, string Harvest_Date, string Market)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO EX_Pipfruit_PCR(PCR_Id, RPIN, Variety, Mgmtarea, Growing_Method, Harvest_Date, Market) " +
                                               "VALUES ( " + PCR_Id + ", '" + RPIN + "' ,'" + Variety + "','" + Mgmtarea + "','" + Growing_Method + "','" + Harvest_Date + "','" + Market + "')");
        }

        public static int Delete()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM EX_Pipfruit_PCR");
        }
    }
}