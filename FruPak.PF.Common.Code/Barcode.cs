using System;
using System.Data;
using System.Linq;

namespace FruPak.PF.Common.Code
{
    public class Barcode
    {
        public static string Barcode_Num
        {
            get;
            set;
        }
        public static int Barcode_Trader
        {
            get;
            set;
        }

        private static string str_BarPre = "";
        private static int str_BarBin = 0;
        private static string str_BarPal = "";
        private static int str_BarPal2 = 0;

        public static void Barcode_Create(int id)
        {
            DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_System.Get_Info_Like("Bar%");
            DataRow dr_Get_Info;
            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                switch (dr_Get_Info["Code"].ToString())
                {
                    case "BarPre":
                        str_BarPre = dr_Get_Info["Value"].ToString();
                        break;

                    case "BarBin":
                        str_BarBin = Convert.ToInt32(dr_Get_Info["Value"].ToString());
                        break;

                    case "BarPal":
                        str_BarPal = dr_Get_Info["Value"].ToString();
                        break;

                    case "BarPal2":
                        str_BarPal2 = Convert.ToInt32(dr_Get_Info["Value"].ToString());
                        break;
                }
            }

            if (Barcode_Trader != 0)
            {
                // need to get the trader specific part of the barcode
                ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Trader.Get_Info(Barcode_Trader);
                string traderBarCode = ds_Get_Info.Tables[0].Rows[0]["Barcode_Num"].ToString();
                Barcode_Num = str_BarPre + traderBarCode + Convert.ToString(str_BarBin + id);
                Barcode_Num = Barcode_Num + Convert.ToString(Barcode_Check_Digit());
            }
            else
            {
                Barcode_Num = str_BarPre + str_BarPal + Convert.ToString(str_BarPal2 + id);
                Barcode_Num = Barcode_Num + Convert.ToString(Barcode_Check_Digit());
            }
        }
        private static int Barcode_Check_Digit()
        {
            int odd = Barcode_Num.ToString().Where((c, i) => i % 2 == 1).Sum(c => c - '0');
            int even = Barcode_Num.ToString().Where((c, i) => i % 2 == 0).Sum(c => c - '0');

            string total = Convert.ToString((even * 3) + odd);

            total.Substring(total.Length - 1, 1);

            if (10 - Convert.ToInt32(total.Substring(total.Length - 1, 1)) == 10)
            {
                return 0;
            }
            else
            {
                return 10 - Convert.ToInt32(total.Substring(total.Length - 1, 1));
            }
        }
    }
}