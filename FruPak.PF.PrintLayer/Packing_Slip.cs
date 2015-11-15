using System;
using System.Data;
using FruPak.PF.CustomSettings;
using System.Windows.Forms;
using System.IO;

namespace FruPak.PF.PrintLayer
{
    public class Packing_Slip
    {
        private static PhantomCustomSettings Settings;

        public static int Print(string Data, bool bol_print)
        {
            int return_code = 98;

            string[] DataParts = Data.Split(':');

            return_code = Print(Convert.ToDecimal(DataParts[0]), Convert.ToInt32(DataParts[1]), DataParts[2], DataParts[3], DataParts[4], bol_print);
            return return_code;
        }

        public static int Print(decimal loop_count, int int_Order_Id, string str_delivery_name, string str_Address, string str_type, bool bol_print)
        {
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

                    case "PF-TPack":
                        FruPak.PF.PrintLayer.Word.TemplateName = dr_default["Value"].ToString();
                        break;
                }
            }
            ds_default.Dispose();

            FruPak.PF.PrintLayer.Word.StartWord();

            // a Packing Slip is restricted to 9 items, so if more than 9 exist then another packing slip is required
            for (int i = 0; i <= Convert.ToInt32(Math.Floor(loop_count / 10)); i++)
            {
                DataSet ds_Order = FruPak.PF.Data.AccessLayer.PF_Orders.Get_Info(int_Order_Id);
                DataRow dr_Order;
                for (int i_order = 0; i_order < ds_Order.Tables[0].Rows.Count; i_order++)
                {
                    dr_Order = ds_Order.Tables[0].Rows[i_order];
                    DateTime dt_Load_Date = Convert.ToDateTime(dr_Order["Load_Date"].ToString());
                    FruPak.PF.PrintLayer.Word.ReplaceText("Date", dt_Load_Date.Date.ToString("dd/MM/yyyy"));
                    FruPak.PF.PrintLayer.Word.ReplaceText("Order_Num", dr_Order["Customer_Order"].ToString());
                    FruPak.PF.PrintLayer.Word.ReplaceText("Freight_Docket", dr_Order["Freight_Docket"].ToString());
                }
                ds_Order.Dispose();

                FruPak.PF.PrintLayer.Word.ReplaceText("Address", str_delivery_name + Environment.NewLine + str_Address);

                //find the products and add them to the packing slip
                DataSet ds_Get_Info2 = FruPak.PF.Data.AccessLayer.PF_Pallet.Get_Info_for_Packing_Slips(int_Order_Id);
                // DataSet ds_Get_Info2A = ds_Get_Info2;
                DataRow dr_Get_Info2;
                int ip = 1;

                for (int j = i * 9; j < Convert.ToInt32(ds_Get_Info2.Tables[0].Rows.Count.ToString()) && j < (i * 9) + 9; j++)
                {
                    dr_Get_Info2 = ds_Get_Info2.Tables[0].Rows[j];

                    string desc = "";
                    //check to see if growing method is Convential
                    if (Convert.ToInt32(dr_Get_Info2["Growing_Method_Id"].ToString()) == 1)
                    {
                        desc = dr_Get_Info2["G_Description"].ToString() + " " + dr_Get_Info2["FT_Description"].ToString() + " " + dr_Get_Info2["FV_Description"].ToString() + " " + dr_Get_Info2["S_Description"].ToString();
                    }
                    else
                    {
                        desc = dr_Get_Info2["G_Description"].ToString() + " " + dr_Get_Info2["FT_Description"].ToString() + " " + dr_Get_Info2["FV_Description"].ToString() +
                            " " + dr_Get_Info2["GM_Description"].ToString() + " " + dr_Get_Info2["S_Description"].ToString();
                    }
                    FruPak.PF.PrintLayer.Word.ReplaceText("Description" + Convert.ToString(ip), desc);
                    FruPak.PF.PrintLayer.Word.ReplaceText("Quantity" + Convert.ToString(ip), Convert.ToString(dr_Get_Info2["Quantity"].ToString()));
                    ip++;
                }
                ds_Get_Info2.Dispose();
            }

            if (bol_print == true)
            {
                switch (str_type)
                {
                    case "Print":
                        return_code = FruPak.PF.PrintLayer.Word.Print();
                        break;

                    case "Email":
                        //return_code = FruPak.PF.PrintLayer.Word.SaveAS();
                        return_code = FruPak.PF.PrintLayer.Word.SaveAsPdf();
                        break;
                }
            }
            else
            {
                return_code = FruPak.PF.PrintLayer.Word.SaveAS();
                //return_code = FruPak.PF.PrintLayer.Word.SaveAsPdf();    // Added 21/10/2015 BN
                //ViewAnyPdfsInTheTempFolder();
            }

            FruPak.PF.PrintLayer.Word.CloseWord();
            return return_code;
        }

        public static void ViewAnyPdfsInTheTempFolder()
        {
            // This is new, now that I've finally got the COA print to pdf working
            string TempPath = string.Empty;

            #region Pull the temp path from the database
            string path = Application.StartupPath;
            Settings = new PhantomCustomSettings();
            Settings.SettingsPath = Path.Combine(path, "FruPak.Phantom.config");
            Settings.EncryptionKey = "phantomKey";
            Settings.Load();
            TempPath = Settings.Path_Local_PDF_Files;

            DirectoryInfo di = new DirectoryInfo(TempPath);
            foreach (FileInfo fi in di.GetFiles())
            {
                if (fi.Name.StartsWith("PS"))
                {
                    if (fi.Extension == ".pdf")
                    {
                        //FruPak.PF.Common.Code.General.Report_Viewer(FruPak.PF.PrintLayer.Word.FilePath + "\\" + view_file);

                        FruPak.PF.Common.Code.General.Report_Viewer(TempPath + "\\" + fi.Name);

                        //SendToPrinter(TempPath, fi.Name);
                        // Delete the file
                        //fi.Delete();
                    }
                }
            }

            #endregion

        }
    }
}