using FP.Utils.Data;
using System.Data;

namespace PF.Data.AccessLayer
{
    public class EX_GDI_Overrun
    {
        public static DataSet Get_GDIBarcode(string Barcode)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT * FROM EX_GDI_Overrun WHERE GDIBarcode = " + Barcode + " AND PF_Active_Ind = 1");
        }

        public static int Update_Active(string Barcode, bool PF_Active_Ind)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE EX_GDI_Overrun SET PF_Active_Ind = '" + PF_Active_Ind + "' " +
                                              " WHERE GDIBarcode = " + Barcode);
        }

        public static int Update_Weight(string Barcode, decimal weight)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE EX_GDI_Overrun SET Weight_Gross =" + weight + " " +
                                              " WHERE GDIBarcode = " + Barcode);
        }
    }
}