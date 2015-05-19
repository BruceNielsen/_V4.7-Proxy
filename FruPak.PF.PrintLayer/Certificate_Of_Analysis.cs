using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NLog;

namespace FruPak.PF.PrintLayer
{
    public class Certificate_Of_Analysis
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static int ip = 0; // BN This is being reset where there are more than one pallets being assigned to an order 12/03/2015        
        /// <summary>
        /// BN This is a bit of a hack to fix an unintended side effect of a little fix I made earlier on the 12th. 18-3-2015. (Ip value is just a counter)
        /// </summary>
        public static void ResetIpValue()
        {
            ip = 0;
        }

        public static int Print(string Data, bool bol_print)
        {
            int return_code = 98;

            string[] DataParts = Data.Split(':');

            return_code = Print(Convert.ToInt32(DataParts[0]), DataParts[1], DataParts[2], DataParts[3],bol_print);
            return return_code;
        }
        private static DateTime dt_Load_Date;
        public static int Print(int int_Order_Id, string str_delivery_name, string str_Address,string str_type, bool bol_print)
        {
            logger.Log(LogLevel.Info, "Certificate_Of_Analysis: Print: Order_Id" + int_Order_Id.ToString());

            int return_code = 97;

            DataSet ds_default = FruPak.PF.Data.AccessLayer.CM_System.Get_Info_Like("PF%");
            DataRow dr_default;


            for (int i_d = 0; i_d < ds_default.Tables[0].Rows.Count; i_d++)
            {
                dr_default = ds_default.Tables[0].Rows[i_d];
                switch (dr_default["Code"].ToString())
                {
                    case "PF-TPath":
                        FruPak.PF.PrintLayer.Word.TemplatePath = dr_default["Value"].ToString();
                        break;
                    case "PF-TCOA":
                        FruPak.PF.PrintLayer.Word.TemplateName = dr_default["Value"].ToString();
                        break;
                }
            }
            ds_default.Dispose();

            //find the products and add them to the COA
            DataSet ds_Pallet = FruPak.PF.Data.AccessLayer.PF_Pallet.Get_Info_for_Order_prod(int_Order_Id);
            DataRow dr_Pallet;

            // --------------- BN 12/03/2015 ------------------
            // This is a little fix to stop the numbers being reset when there are more than one pallet to an order.
            // What is supposed to happen is an incrementing number is appended to each orders filename which looks like 
            // -1, -2, -3 etc.
            // It works, but is being reset when more than one pallete is attached to an order, and it resets to 1.
            // This results in COA's being printed out physically when they are only ever meant to be emailed.
            if (ip == 0)    
            {
                ip = 1;
            }

            int int_tot_quantity = 0;

            // checking for not pallets loaded against the order
            try
            {
                Convert.ToInt32(ds_Pallet.Tables[0].Rows.Count);
                if (ds_Pallet.Tables[0].Rows.Count <= 0)
                {
                    return_code = 96;
                }
            }
            catch
            {
                return_code = 96;
            }

            string str_cust_order = "";
            string str_Freight_Docket = "";
            for (int i = 0; i < ds_Pallet.Tables[0].Rows.Count; i++)
            {
                FruPak.PF.PrintLayer.Word.StartWord();

                #region ------------- Order Details -------------
                DataSet ds_Order = FruPak.PF.Data.AccessLayer.PF_Orders.Get_Info(int_Order_Id);
                DataRow dr_Order;
                for (int i_order = 0; i_order < ds_Order.Tables[0].Rows.Count; i_order++)
                {
                    dr_Order = ds_Order.Tables[0].Rows[i_order];
                    dt_Load_Date = Convert.ToDateTime(dr_Order["Load_Date"].ToString());                    
                    FruPak.PF.PrintLayer.Word.ReplaceText("Load_Date", dt_Load_Date.Date.ToString("dd/MM/yyyy"));
                    str_cust_order = dr_Order["Customer_Order"].ToString();
                    FruPak.PF.PrintLayer.Word.ReplaceText("Order_Num", dr_Order["Customer_Order"].ToString());
                    str_Freight_Docket = dr_Order["Freight_Docket"].ToString();
                    FruPak.PF.PrintLayer.Word.ReplaceText("Freight_Docket", dr_Order["Freight_Docket"].ToString());
                }              
                
                               
                FruPak.PF.PrintLayer.Word.ReplaceText("Address", str_delivery_name + Environment.NewLine + str_Address);

                int_tot_quantity = 0;
                dr_Pallet = ds_Pallet.Tables[0].Rows[i];

                string str_product = dr_Pallet["Product"].ToString() + ": " + dr_Pallet["FruitType"].ToString();
                string str_size = dr_Pallet["Size"].ToString();
                string str_variety = "Variety: " + dr_Pallet["Varity"].ToString();

                FruPak.PF.PrintLayer.Word.ReplaceText("Product", str_product + " " + str_size);
                FruPak.PF.PrintLayer.Word.ReplaceText("Variety", str_variety);
                #endregion

                DataSet ds_Get_Info3;
                DataRow dr_Get_Info3;

                #region ------------- Lab Results -------------
                DataSet ds_LabResults = FruPak.PF.Data.AccessLayer.PF_Product_LabResults.Get_Info_by_Prod(Convert.ToInt32(dr_Pallet["Product_Id"].ToString()), Convert.ToInt32(dr_Pallet["FruitType_Id"].ToString()), Convert.ToInt32(dr_Pallet["Variety_Id"].ToString()));
                DataRow dr_LabResults;
                string str_ph = "";
                string str_moist = "";
                string str_brix = "";
                for (int j = 0; j < Convert.ToInt32(ds_LabResults.Tables[0].Rows.Count.ToString()); j++)
                {
                    dr_LabResults = ds_LabResults.Tables[0].Rows[j];
                    switch (dr_LabResults["Code"].ToString())
                    {
                        case "pH":
                            str_ph = dr_LabResults["Value"].ToString();
                            FruPak.PF.PrintLayer.Word.ReplaceText("PH", dr_LabResults["Value"].ToString());
                            break;
                        case "Moist":
                            str_moist = dr_LabResults["Value"].ToString();
                            FruPak.PF.PrintLayer.Word.ReplaceText("MOIST", dr_LabResults["Value"].ToString());
                            break;
                        case "Brix":
                            str_brix = dr_LabResults["Value"].ToString();
                            FruPak.PF.PrintLayer.Word.ReplaceText("BRIX", dr_LabResults["Value"].ToString());
                            break;
                    }
                }
                ds_LabResults.Dispose();
                #endregion
                #region ------------- Pallet Number-------------
                //pallet numbers
                int int_total_Chep = 0;
                int int_total_Oth = 0;
                DataSet ds_Orders_Pallets = FruPak.PF.Data.AccessLayer.PF_Orders_Pallets.Get_Info_for_Order(int_Order_Id);
                DataRow dr_Orders_Pallets;
                for (int j = 0; j < Convert.ToInt32(ds_Orders_Pallets.Tables[0].Rows.Count.ToString()); j++)
                {
                    dr_Orders_Pallets = ds_Orders_Pallets.Tables[0].Rows[j];
                    if (dr_Orders_Pallets["Code"].ToString().ToUpper().Contains("CHEP"))
                    {
                        int_total_Chep = int_total_Chep + Convert.ToInt32(dr_Orders_Pallets["Number"].ToString());
                    }
                    else
                    {
                        int_total_Oth = int_total_Oth + Convert.ToInt32(dr_Orders_Pallets["Number"].ToString());
                    }
                }
                ds_Orders_Pallets.Dispose();

                FruPak.PF.PrintLayer.Word.ReplaceText("Chep_Pallets", Convert.ToString(int_total_Chep));
                FruPak.PF.PrintLayer.Word.ReplaceText("Oth_Pallets", Convert.ToString(int_total_Oth));
                #endregion

                //check and reset output filename - This is where the .pdf extension is missing - BN 05-03-2015
                Console.WriteLine("PrintLayer.Certificate_Of_Analysis - Print: Check and reset filename: " + FruPak.PF.PrintLayer.Word.FileName);
                logger.Log(LogLevel.Info, "Print: Check and reset filename: " + FruPak.PF.PrintLayer.Word.FileName);

                if (FruPak.PF.PrintLayer.Word.FileName.LastIndexOf('-') > 0)
                {
                    FruPak.PF.PrintLayer.Word.FileName =
                        FruPak.PF.PrintLayer.Word.FileName.Substring(0, FruPak.PF.PrintLayer.Word.FileName.LastIndexOf('-')) + "-" + Convert.ToString(ip);
                }
                else
                {
                    FruPak.PF.PrintLayer.Word.FileName = FruPak.PF.PrintLayer.Word.FileName.Substring(0, FruPak.PF.PrintLayer.Word.FileName.Length) + "-" + Convert.ToString(ip);
                }

                #region ------------- Best Before Dates-------------
                //Get BBD (Best Before Date), number of months to add on to production date
                DataSet ds_Other = FruPak.PF.Data.AccessLayer.PF_Product_Other.Get_Info(Convert.ToInt32(dr_Pallet["Product_Id"].ToString()), "BBD");
                DataRow dr_Other;
                int int_BBD_months = 0;
                for (int bbd = 0; bbd < ds_Other.Tables[0].Rows.Count; bbd++)
                {
                    dr_Other = ds_Other.Tables[0].Rows[bbd];
                    try
                    {
                        int_BBD_months = Convert.ToInt32(dr_Other["Value"].ToString());
                    }
                    catch
                    {
                        int_BBD_months = 0;
                    }
                }
                ds_Other.Dispose();
                #endregion

                #region ------------- Batch Details -------------
                //get batch info
                ds_Get_Info3 = FruPak.PF.Data.AccessLayer.PF_Pallet.Get_Info_for_Order(int_Order_Id, Convert.ToInt32(dr_Pallet["Product_Id"].ToString()), Convert.ToInt32(dr_Pallet["FruitType_Id"].ToString()), Convert.ToInt32(dr_Pallet["Variety_Id"].ToString()));
                int jp = 0;
                int kp = 0;
                for (int j = 0; j < Convert.ToInt32(ds_Get_Info3.Tables[0].Rows.Count.ToString()); j++)
                {
                    dr_Get_Info3 = ds_Get_Info3.Tables[0].Rows[j];

                    //check to see if the batches are on teh same pallet, if not add a blank (date, Batch) between Pallets
                    if (j > 0)
                    {
                        if (Convert.ToInt32(dr_Get_Info3["Pallet_Id"].ToString()) == Convert.ToInt32(ds_Get_Info3.Tables[0].Rows[j - 1]["Pallet_Id"].ToString()))
                        {
                            jp++;
                        }
                        else
                        {
                            jp = jp + 2;
                        }
                    }
                    else
                    {
                        jp++;
                    }

                    if (jp > 18)
                    {
                        //check and reset output filename           
                        kp++;
                        if (FruPak.PF.PrintLayer.Word.FileName.LastIndexOf('-') > 0)
                        {
                            FruPak.PF.PrintLayer.Word.FileName =
                                FruPak.PF.PrintLayer.Word.FileName.Substring(0, FruPak.PF.PrintLayer.Word.FileName.LastIndexOf('-')) + "-" + Convert.ToString(kp);
                        }
                        else
                        {
                            FruPak.PF.PrintLayer.Word.FileName = FruPak.PF.PrintLayer.Word.FileName.Substring(0, FruPak.PF.PrintLayer.Word.FileName.Length) + "-" + Convert.ToString(kp);
                        }

                        if (bol_print == true)
                        {
                            return_code = FruPak.PF.PrintLayer.Word.Print();
                        }
                        else
                        {
                            return_code = FruPak.PF.PrintLayer.Word.SaveAS();
                        }
                        //
                        FruPak.PF.PrintLayer.Word.CloseWord();
                        FruPak.PF.PrintLayer.Word.StartWord();
                        
                        FruPak.PF.PrintLayer.Word.ReplaceText("Load_Date", dt_Load_Date.Date.ToString("dd/MM/yyyy"));
                        FruPak.PF.PrintLayer.Word.ReplaceText("Order_Num", str_cust_order);
                        FruPak.PF.PrintLayer.Word.ReplaceText("Freight_Docket", str_Freight_Docket);
                        FruPak.PF.PrintLayer.Word.ReplaceText("Address", str_delivery_name + Environment.NewLine + str_Address);
                        FruPak.PF.PrintLayer.Word.ReplaceText("Product", str_product + " " + str_size);
                        FruPak.PF.PrintLayer.Word.ReplaceText("Variety", str_variety);                                        
                        FruPak.PF.PrintLayer.Word.ReplaceText("PH", str_ph);
                        FruPak.PF.PrintLayer.Word.ReplaceText("MOIST", str_moist);
                        FruPak.PF.PrintLayer.Word.ReplaceText("BRIX", str_brix);
                        FruPak.PF.PrintLayer.Word.ReplaceText("Chep_Pallets", Convert.ToString(int_total_Chep));
                        FruPak.PF.PrintLayer.Word.ReplaceText("Oth_Pallets", Convert.ToString(int_total_Oth));
                        
                        jp = 1;
                    }

                    DateTime dt = new DateTime();
                    dt = Convert.ToDateTime(dr_Get_Info3["Batch_Num"].ToString().Substring(6, 2) + "/" + dr_Get_Info3["Batch_Num"].ToString().Substring(4, 2) + "/" + dr_Get_Info3["Batch_Num"].ToString().Substring(0, 4)).AddMonths(int_BBD_months);

                    FruPak.PF.PrintLayer.Word.ReplaceText("Date" + Convert.ToString(jp), dr_Get_Info3["Batch_Num"].ToString().Substring(6, 2) + "/" + dr_Get_Info3["Batch_Num"].ToString().Substring(4, 2) + "/" + dr_Get_Info3["Batch_Num"].ToString().Substring(0, 4));
                    if (int_BBD_months > 0)
                    {
                        FruPak.PF.PrintLayer.Word.ReplaceText("BBD" + Convert.ToString(jp), dt.ToShortDateString());
                    }
                    FruPak.PF.PrintLayer.Word.ReplaceText("Batch" + Convert.ToString(jp), dr_Get_Info3["Batch_Num"].ToString().Substring(8) + " - " + dr_Get_Info3["Quantity"].ToString());
                    int_tot_quantity = int_tot_quantity + Convert.ToInt32(dr_Get_Info3["Quantity"].ToString());
                #endregion

                }
                FruPak.PF.PrintLayer.Word.ReplaceText("Quantity", Convert.ToString(int_tot_quantity));

                if (bol_print == true)
                {
                    switch (str_type)
                    {
                        case "Print":
                            return_code = FruPak.PF.PrintLayer.Word.Print();
                            break;
                        case "Email":
                            return_code = FruPak.PF.PrintLayer.Word.SaveAsPdf();
                            break;
                    }
                }
                else
                {
                    return_code = FruPak.PF.PrintLayer.Word.SaveAS();
                }
                FruPak.PF.PrintLayer.Word.CloseWord();
                ip++;
            }
            ds_Pallet.Dispose();                    


            return return_code;
        }
    }
}
