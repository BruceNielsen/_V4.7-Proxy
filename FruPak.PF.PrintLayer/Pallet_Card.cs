using NLog;
using System;
using System.Data;

namespace FruPak.PF.PrintLayer
{
    public class Pallet_Card
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static int int_Current_User_Id = 0;

        public static int Print(string Data, int Current_User_Id)
        {
            int return_code = 98;

            string[] DataParts = Data.Split(':');

            return_code = Print(DataParts[0], Convert.ToBoolean(DataParts[1]), Current_User_Id);
            return return_code;
        }

        public static int Print(string Barcode, bool bol_print, int Current_User_Id)
        {
            int return_code = 97;
            int_Current_User_Id = Current_User_Id;
            //Create Barcode
            string str_image_location = "";
            FruPak.PF.PrintLayer.Barcode.str_Barcode = Barcode;
            FruPak.PF.PrintLayer.Barcode.Width = 300;
            FruPak.PF.PrintLayer.Barcode.Height = 100;
            str_image_location = FruPak.PF.PrintLayer.Barcode.Generate_Barcode();

            DataSet ds = FruPak.PF.Data.AccessLayer.PF_Pallet.Get_Material_info(Barcode);
            DataRow dr;

            //this loop should produce 1 pallet card per material number.
            for (int i_ds = 0; i_ds < ds.Tables[0].Rows.Count; i_ds++)
            {
                dr = ds.Tables[0].Rows[i_ds];

                int int_Pallet_Total = 0;

                int int_material_id = Convert.ToInt32(dr["Material_Id"].ToString());

                DataSet ds_Get_Info;
                DataRow dr_Get_Info;
                DataSet ds_details = FruPak.PF.Data.AccessLayer.PF_Pallet_Details.Get_Info(Barcode, int_material_id);
                DataRow dr_details;

                for (int h = 0; h <= Convert.ToInt32(Math.Floor(Convert.ToDecimal(ds_details.Tables[0].Rows.Count.ToString()) / 4)); h++)
                {
                    dr_details = ds_details.Tables[0].Rows[h];

                    ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_System.Get_Info_Like("PF%");
                    for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
                    {
                        dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                        switch (dr_Get_Info["Code"].ToString())
                        {
                            case "PF-TPath":
                                FruPak.PF.PrintLayer.Word.TemplatePath = dr_Get_Info["Value"].ToString();

                                break;

                            case "PF-TPallet":
                                FruPak.PF.PrintLayer.Word.TemplateName = dr_Get_Info["Value"].ToString();
                                break;
                        }
                    }
                    ds_Get_Info.Dispose();

                    FruPak.PF.PrintLayer.Word.BarcodeFull = str_image_location;
                    FruPak.PF.PrintLayer.Word.StartWord();
                    FruPak.PF.PrintLayer.Word.Add_Barcode("Barcode1");

                    //Fruit Type
                    FruPak.PF.PrintLayer.Word.ReplaceText("Type", dr_details["FT_Code"].ToString());

                    //Product
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Product.Get_Info(Convert.ToInt32(dr_details["Product_Id"].ToString()));

                    for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
                    {
                        dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                        FruPak.PF.PrintLayer.Word.ReplaceText("Product", dr_Get_Info["Code"].ToString());
                    }
                    ds_Get_Info.Dispose();

                    //Variety
                    FruPak.PF.PrintLayer.Word.ReplaceText("Variety", dr_details["FV_Description"].ToString() + "(" + dr_details["G_Code"].ToString() + ")");

                    //Size
                    FruPak.PF.PrintLayer.Word.ReplaceText("Size", dr_details["S_Description"].ToString());

                    //Dates, Batches, Quantity

                    int ip = 1;
                    // the counter here needs  to select in groups of three, so for each page created it need to start at the beginning of the next group of three.
                    DataRow dr_Get_Info1;
                    for (int i = h * 3; i < ds_details.Tables[0].Rows.Count && i < (h * 3) + 3; i++)
                    {
                        dr_Get_Info1 = ds_details.Tables[0].Rows[i];

                        FruPak.PF.PrintLayer.Word.ReplaceText("Date" + Convert.ToString(ip), dr_Get_Info1["Batch_Num"].ToString().Substring(6, 2) + "/" + dr_Get_Info1["Batch_Num"].ToString().Substring(4, 2) + "/" + dr_Get_Info1["Batch_Num"].ToString().Substring(0, 4));

                        FruPak.PF.PrintLayer.Word.ReplaceText("Batch" + Convert.ToString(ip), dr_Get_Info1["Batch_Num"].ToString().Substring(8));

                        FruPak.PF.PrintLayer.Word.ReplaceText("Quantity" + Convert.ToString(ip), dr_Get_Info1["Quantity"].ToString());
                        int_Pallet_Total = int_Pallet_Total + Convert.ToInt32(dr_Get_Info1["Quantity"].ToString());
                        ip++;
                    }

                    if (h == Convert.ToInt32(Math.Floor(Convert.ToDecimal(ds_details.Tables[0].Rows.Count.ToString()) / 4)))
                    {
                        FruPak.PF.PrintLayer.Word.ReplaceText("Total", Convert.ToString(int_Pallet_Total));
                    }

                    if (bol_print == true)
                    {
                        return_code = FruPak.PF.PrintLayer.Word.Print();
                        stock_update();
                    }
                    else
                    {
                        return_code = FruPak.PF.PrintLayer.Word.SaveAS();
                    }

                    FruPak.PF.PrintLayer.Word.CloseWord();
                }
                ds_details.Dispose();
            }
            ds.Dispose();

            try
            {
                FruPak.PF.PrintLayer.Word.CloseWord();
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message);
            }
            return return_code;
        }

        private static void stock_update()
        {
            DataSet ds = FruPak.PF.Data.AccessLayer.PF_Stock_Item.Get_Info("Other-1");
            DataRow dr;
            int int_Stock_Item_Id = 0;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dr = ds.Tables[0].Rows[i];
                int_Stock_Item_Id = Convert.ToInt32(dr["Stock_Item_Id"].ToString());
            }
            ds.Dispose();

            FruPak.PF.Data.AccessLayer.PF_Stock_Used.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Stock_Used"), int_Stock_Item_Id, FruPak.PF.Common.Code.General.Get_Season(), 1, int_Current_User_Id);
            FruPak.PF.Common.Code.General.Consumable_Check(int_Current_User_Id);
        }
    }
}