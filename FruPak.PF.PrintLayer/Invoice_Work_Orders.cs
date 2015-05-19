using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace FruPak.PF.PrintLayer
{
    public class Invoice_Work_Orders
    {

        public static int Print(string Data, bool bol_print)
        {
            int return_code = 98;

            string[] DataParts = Data.Split(':');

            return_code = Print(Convert.ToInt32(DataParts[0]), Convert.ToInt32(DataParts[1]), DataParts[2], DataParts[3], DataParts[4], DataParts[5], Convert.ToDecimal(DataParts[6]), DataParts[7]);
            return return_code;
        }
        public static int Print(int Invoice_Id, int int_Trader_Id, string Invoice_Date, string Order_Num, string str_delivery_name, string str_save_filename, decimal dec_GST, string str_type)
        {
            int return_code = 97;            

            string str_Invoice_Date = "";
            string str_Invoice_Date_yyyymmdd = "";

            str_Invoice_Date = Invoice_Date.Substring(8, 2) + "/" + Invoice_Date.Substring(5, 2) + "/" + Invoice_Date.Substring(0, 4);
            str_Invoice_Date_yyyymmdd = Invoice_Date.Substring(0, 4) + Invoice_Date.Substring(5, 2) + Invoice_Date.Substring(8, 2);

            FruPak.PF.PrintLayer.Word.StartWord();
            str_delivery_name = str_delivery_name + FruPak.PF.Common.Code.General.Create_Billing_Address();
            FruPak.PF.PrintLayer.Word.ReplaceText("Customer", str_delivery_name);
            FruPak.PF.PrintLayer.Word.ReplaceText("Date", str_Invoice_Date);  //Invoice date
            FruPak.PF.PrintLayer.Word.ReplaceText("CustomerNum", Convert.ToString(int_Trader_Id));  //Trader_Id
            FruPak.PF.PrintLayer.Word.ReplaceText("InvoiceNum", Convert.ToString(Invoice_Id));  //Invoice Number / Invoice_Id
            FruPak.PF.PrintLayer.Word.ReplaceText("OrderNum", Order_Num);  //Order Number


            int ip = 1;
            decimal dec_tot_xgst = 0;

            DataSet ds_Get_Info;
            DataRow dr_Get_Info;

            ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Work_Order.Get_Invoiced(Invoice_Id);

            //get work order numbers
            List<int> int_wo_id = new List<int>();
            string str_get_wo = "(";
            for (int i_wo = 0; i_wo < ds_Get_Info.Tables[0].Rows.Count; i_wo++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i_wo];
                str_get_wo = str_get_wo + dr_Get_Info["Work_Order_Id"] + ",";
                int_wo_id.Add(Convert.ToInt32(dr_Get_Info["Work_Order_Id"].ToString()));

            }
            ds_Get_Info.Dispose();
            str_get_wo = str_get_wo + ")";

            string str_WHERE = str_get_wo.Replace(",)", ")");

            //get invoice details
            DataSet ds = null;
            DataRow dr;
            ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_A_Invoice_Details.Get_Info_Translated_WO(Invoice_Id);
            for (int i = 0; i < ds_Get_Info.Tables[0].Rows.Count; i++)
            {
                string str_Quantity = "";
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                switch (dr_Get_Info["Code"].ToString().ToUpper().Substring(0, dr_Get_Info["Code"].ToString().IndexOf('-')))
                {
                    
                        //str_total_item = FruPak.PF.Common.Code.General.Calculate_Totals(dr_Rates["Code"].ToString().ToUpper().Substring(0, dr_Rates["Code"].ToString().IndexOf('-')), str_WHERE);
                    case "STORAGE":
                        str_Quantity = FruPak.PF.Common.Code.General.Calculate_Totals(dr_Get_Info["Code"].ToString().ToUpper(), str_WHERE);
                        break;
                    case "BAGS":
                    case "FREIGHT":
                    case "HOTFILL":
                        str_Quantity = FruPak.PF.Common.Code.General.Calculate_Totals(dr_Get_Info["Code"].ToString().ToUpper().Substring(0, dr_Get_Info["Code"].ToString().IndexOf('-')), str_WHERE);
                        break;
                    default:
                        ds = FruPak.PF.Data.AccessLayer.CM_Bins.Get_NET_Bin_Weight_by_Work_Order(int_wo_id[i]);                        
                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {
                            dr = ds.Tables[j].Rows[j];
                            str_Quantity = dr["NET_Weight"].ToString();
                        }
                        ds.Dispose();
                        break;
                }                
                string str_desc = "";
                str_desc = dr_Get_Info["Description"].ToString();

                FruPak.PF.PrintLayer.Word.ReplaceText("Description" + Convert.ToString(ip), str_desc);
                FruPak.PF.PrintLayer.Word.ReplaceText("Price" + Convert.ToString(ip), Convert.ToString(Math.Round(Convert.ToDecimal(dr_Get_Info["Value"].ToString()), 2)));
                FruPak.PF.PrintLayer.Word.ReplaceText("Quantity" + Convert.ToString(ip), str_Quantity);
                FruPak.PF.PrintLayer.Word.ReplaceText("LTotal" + Convert.ToString(ip), String.Format("{0:#,###0.00}", Math.Round(Convert.ToDecimal(dr_Get_Info["Cost"].ToString()), 2)));
                dec_tot_xgst = dec_tot_xgst + Math.Round(Convert.ToDecimal(dr_Get_Info["Cost"].ToString()), 2);
                ip++;
            }
            ds_Get_Info.Dispose();

            FruPak.PF.PrintLayer.Word.ReplaceText("STotal", String.Format("{0:#,###0.00}", dec_tot_xgst));
            FruPak.PF.PrintLayer.Word.ReplaceText("TGST", String.Format("{0:#,###0.00}", Math.Round(dec_tot_xgst * (dec_GST / 100), 2)));
            FruPak.PF.PrintLayer.Word.ReplaceText("Total", String.Format("{0:#,###0.00}", (Math.Round(dec_tot_xgst * (dec_GST / 100), 2)) + dec_tot_xgst));

            return_code = FruPak.PF.PrintLayer.General.Print(str_type, str_save_filename, str_Invoice_Date_yyyymmdd);

            FruPak.PF.PrintLayer.Word.CloseWord();
            return return_code;
        }
    }
}
