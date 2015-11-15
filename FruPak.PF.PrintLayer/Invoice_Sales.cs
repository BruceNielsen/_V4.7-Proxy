using System;
using System.Collections.Generic;
using System.Data;

namespace FruPak.PF.PrintLayer
{
    public class Invoice_Sales
    {
        //     private static int int_Current_User_Id = 0;
        public static int Print(string Data, bool bol_print)
        {
            int return_code = 98;

            string[] DataParts = Data.Split(':');

            return_code = Print(Convert.ToInt32(DataParts[0]), DataParts[1], DataParts[2], DataParts[3], DataParts[4], Convert.ToDecimal(DataParts[5]), DataParts[6], bol_print);
            return return_code;
        }

        public static int Print(int Invoice_Id, string Invoice_Date, string Order_Num, string str_delivery_name, string str_save_filename, decimal dec_GST, string str_type, bool bol_print)
        {
            FruPak.PF.PrintLayer.General.view_list = new List<string>();
            int return_code = 97;

            string str_Invoice_Date = "";
            string str_Invoice_Date_yyyymmdd = "";

            str_Invoice_Date = Invoice_Date.Substring(8, 2) + "/" + Invoice_Date.Substring(5, 2) + "/" + Invoice_Date.Substring(0, 4);
            str_Invoice_Date_yyyymmdd = Invoice_Date.Substring(0, 4) + Invoice_Date.Substring(5, 2) + Invoice_Date.Substring(8, 2);

            //new code for extra page loop

            decimal dec_tot_xgst = 0;

            DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Pallet.Get_Invoiced(Invoice_Id);
            DataRow dr_Get_Info;

            for (int h = 0; h <= Convert.ToInt32(Math.Floor((Convert.ToDecimal(ds_Get_Info.Tables[0].Rows.Count.ToString()) - 1) / 8)); h++)
            {
                FruPak.PF.PrintLayer.Word.StartWord();
                str_delivery_name = str_delivery_name + FruPak.PF.Common.Code.General.Create_Billing_Address();
                FruPak.PF.PrintLayer.Word.ReplaceText("Customer", str_delivery_name);
                FruPak.PF.PrintLayer.Word.ReplaceText("Date", str_Invoice_Date);  //Invoice date
                FruPak.PF.PrintLayer.Word.ReplaceText("InvoiceNum", Convert.ToString(Invoice_Id));  //Invoice Number / Invoice_Id
                FruPak.PF.PrintLayer.Word.ReplaceText("OrderNum", Order_Num);  //Order Number

                int ip = 1;
                for (int i = h * 8; i < ds_Get_Info.Tables[0].Rows.Count && i < (h * 8) + 8; i++)
                //    for (int i = 0; i < ds_Get_Info.Tables[0].Rows.Count; i++)
                {
                    dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                    DataSet ds1 = FruPak.PF.Data.AccessLayer.PF_A_Invoice_Details.Get_Order_Nums(Invoice_Id);
                    DataRow dr1;
                    string str_get_wo = "(";
                    for (int i_wo = 0; i_wo < ds1.Tables[0].Rows.Count; i_wo++)
                    {
                        dr1 = ds1.Tables[0].Rows[i_wo];
                        str_get_wo = str_get_wo + dr1["Order_Num"] + ",";
                    }
                    ds1.Dispose();
                    str_get_wo = str_get_wo + ")";

                    string str_WHERE = str_get_wo.Replace(",)", ")");
                    string str_Quantity = "";
                    try
                    {
                        switch (dr_Get_Info["Code"].ToString())
                        {
                            case "Storage-2":
                            case "Storage-3":
                                str_Quantity = FruPak.PF.Common.Code.General.Calculate_Totals(dr_Get_Info["Code"].ToString().ToUpper(), str_WHERE);
                                FruPak.PF.PrintLayer.Word.ReplaceText("Description" + Convert.ToString(ip), dr_Get_Info["R_Description"].ToString());
                                break;

                            case "Freight-1":
                            case "Freight-2":
                                str_Quantity = FruPak.PF.Common.Code.General.Calculate_Totals(dr_Get_Info["Code"].ToString().ToUpper().Substring(0, dr_Get_Info["Code"].ToString().IndexOf('-')), str_WHERE);
                                FruPak.PF.PrintLayer.Word.ReplaceText("Description" + Convert.ToString(ip), dr_Get_Info["R_Description"].ToString());
                                break;

                            default:
                                str_Quantity = Convert.ToString(Math.Round(Convert.ToDecimal(dr_Get_Info["Quantity"].ToString()), 0));
                                FruPak.PF.PrintLayer.Word.ReplaceText("Description" + Convert.ToString(ip), dr_Get_Info["G_Description"].ToString());
                                FruPak.PF.PrintLayer.Word.ReplaceText("COrder" + Convert.ToString(ip), dr_Get_Info["Customer_Order"].ToString());
                                break;
                        }
                    }
                    catch
                    {
                        str_Quantity = Convert.ToString(Math.Round(Convert.ToDecimal(dr_Get_Info["Quantity"].ToString()), 0));
                        FruPak.PF.PrintLayer.Word.ReplaceText("Description" + Convert.ToString(ip), dr_Get_Info["G_Description"].ToString());
                        FruPak.PF.PrintLayer.Word.ReplaceText("COrder" + Convert.ToString(ip), dr_Get_Info["Customer_Order"].ToString());
                    }

                    FruPak.PF.PrintLayer.Word.ReplaceText("Quantity" + Convert.ToString(ip), String.Format("{0:#,###0}", str_Quantity));
                    FruPak.PF.PrintLayer.Word.ReplaceText("LTotal" + Convert.ToString(ip), String.Format("{0:#,###0.00}", Math.Round(Convert.ToDecimal(dr_Get_Info["Cost"].ToString()), 2)));

                    FruPak.PF.PrintLayer.Word.ReplaceText("Price" + Convert.ToString(ip), String.Format("{0:#,###0.00}", Math.Round(Convert.ToDecimal(dr_Get_Info["Cost"].ToString()) / Convert.ToDecimal(str_Quantity), 2)));

                    //discount
                    string str_discount = "";
                    DataSet ds = FruPak.PF.Data.AccessLayer.PF_A_Invoice.Get_Discount_for_Invoice(Invoice_Id);
                    DataRow dr;
                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        dr = ds.Tables[0].Rows[j];
                        str_discount = dr["Value"].ToString() + "% Discount for an on Time Payment";
                    }

                    if (str_discount.Length > 0)
                    {
                        FruPak.PF.PrintLayer.Word.ReplaceText("Discount", str_discount);
                    }

                    dec_tot_xgst = dec_tot_xgst + Math.Round(Convert.ToDecimal(dr_Get_Info["Cost"].ToString()), 2);
                    ip++;

                    if (i == ds_Get_Info.Tables[0].Rows.Count - 1)
                    {
                        FruPak.PF.PrintLayer.Word.ReplaceText("STotal", String.Format("{0:#,###0.00}", dec_tot_xgst));
                        FruPak.PF.PrintLayer.Word.ReplaceText("TGST", String.Format("{0:#,###0.00}", Math.Round(dec_tot_xgst * (dec_GST / 100), 2)));
                        FruPak.PF.PrintLayer.Word.ReplaceText("Total", String.Format("{0:#,###0.00}", (Math.Round(dec_tot_xgst * (dec_GST / 100), 2)) + dec_tot_xgst));
                    }
                }

                return_code = FruPak.PF.PrintLayer.General.Print(str_type, str_save_filename + "-" + Convert.ToString(h), str_Invoice_Date_yyyymmdd);
                FruPak.PF.PrintLayer.Word.CloseWord();
            }

            return return_code;
        }
    }
}