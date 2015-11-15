using System;
using System.Collections.Generic;
using System.Data;

namespace FruPak.PF.PrintLayer
{
    public class Bin_Card
    {
        private static int num_of_Bins = 0;

        public static int Print(string Data, bool bol_print)
        {
            int return_code = 98;

            string[] DataParts = Data.Split(':');

            if (DataParts[0] == "W")
            {
                num_of_Bins = Convert.ToInt32(DataParts[3]);
            }

            return_code = Print(DataParts[0], Convert.ToInt32(DataParts[1]), Convert.ToBoolean(DataParts[2]), bol_print);
            return return_code;
        }

        private static string str_TempPath = "";
        private static string str_PF_BlkBCrd = "";

        public static int Print(string str_WS_Ind, int int_sub_id, bool bol_reprint, bool bol_print)
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

                    case "PF-TBin":
                        FruPak.PF.PrintLayer.Word.TemplateName = dr_default["Value"].ToString();
                        break;

                    case "PF-TPPath":
                        str_TempPath = dr_default["Value"].ToString();
                        break;

                    case "PF-BlkBCrd":
                        str_PF_BlkBCrd = dr_default["Value"].ToString();
                        break;
                }
            }
            ds_default.Dispose();

            DataSet ds_bincard = null;

            switch (str_WS_Ind)
            {
                case "S":
                    ds_bincard = FruPak.PF.Data.AccessLayer.Temp_submission.BinCard(int_sub_id);
                    break;

                case "W":
                    //int_sub_id here is the Bin_Id
                    ds_bincard = FruPak.PF.Data.AccessLayer.CM_Bins.BinCard_Work_Order_In(int_sub_id);
                    break;
            }
            DataRow dr_bincard;

            for (int i = 0; i < ds_bincard.Tables[0].Rows.Count; i++)
            {
                FruPak.PF.PrintLayer.Word.StartWord();
                FruPak.PF.PrintLayer.Word.OpenTemplate();
                dr_bincard = ds_bincard.Tables[0].Rows[i];
                //start word and replace bookmarks

                //create barcode1
                string str_image_location = "";
                FruPak.PF.PrintLayer.Barcode.str_Barcode = dr_bincard["Barcode"].ToString();
                FruPak.PF.PrintLayer.Barcode.Width = 450;
                FruPak.PF.PrintLayer.Barcode.Height = 100;
                FruPak.PF.PrintLayer.Barcode.RotateFlip = System.Drawing.RotateFlipType.Rotate270FlipNone;
                str_image_location = FruPak.PF.PrintLayer.Barcode.Generate_Barcode();

                FruPak.PF.PrintLayer.Word.BarcodeFull = str_image_location;
                FruPak.PF.PrintLayer.Word.Add_Barcode("Barcode1");

                //create barcode2
                str_image_location = "";
                FruPak.PF.PrintLayer.Barcode.str_Barcode = dr_bincard["Barcode"].ToString();
                FruPak.PF.PrintLayer.Barcode.Width = 450;
                FruPak.PF.PrintLayer.Barcode.Height = 100;
                FruPak.PF.PrintLayer.Barcode.RotateFlip = System.Drawing.RotateFlipType.RotateNoneFlipNone;
                str_image_location = FruPak.PF.PrintLayer.Barcode.Generate_Barcode();

                FruPak.PF.PrintLayer.Word.BarcodeFull = str_image_location;
                FruPak.PF.PrintLayer.Word.Add_Barcode("Barcode2");

                DateTime dt = new DateTime();
                switch (str_WS_Ind)
                {
                    case "W":
                        FruPak.PF.PrintLayer.Word.ReplaceText("Fruit", "REJECT");

                        dt = DateTime.Now;
                        FruPak.PF.PrintLayer.Word.ReplaceText("Date", dt.Day.ToString() + "/" + dt.Month.ToString() + "/" + dt.Year.ToString());
                        FruPak.PF.PrintLayer.Word.ReplaceText("NumBins", Convert.ToString(num_of_Bins));

                        break;

                    default:
                        FruPak.PF.PrintLayer.Word.ReplaceText("Submission", int_sub_id.ToString());
                        FruPak.PF.PrintLayer.Word.ReplaceText("Fruit", dr_bincard["Fruit"].ToString().ToUpper());
                        FruPak.PF.PrintLayer.Word.ReplaceText("Variety", dr_bincard["Variety"].ToString().ToUpper());

                        dt = Convert.ToDateTime(dr_bincard["Sub_Date"].ToString());
                        FruPak.PF.PrintLayer.Word.ReplaceText("Date", dt.Day.ToString() + "/" + dt.Month.ToString() + "/" + dt.Year.ToString());

                        FruPak.PF.PrintLayer.Word.ReplaceText("Rpin", dr_bincard["Rpin"].ToString());

                        FruPak.PF.PrintLayer.Word.ReplaceText("Trader", dr_bincard["Trader"].ToString().ToUpper());
                        FruPak.PF.PrintLayer.Word.ReplaceText("Trader1", dr_bincard["Full_Trader"].ToString().ToUpper());

                        //Number of bin in submission
                        DataSet ds = FruPak.PF.Data.AccessLayer.CM_Bins.Get_Info_Submission(int_sub_id);
                        DataRow dr;
                        num_of_Bins = 0; //reset
                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {
                            dr = ds.Tables[0].Rows[j];
                            num_of_Bins = num_of_Bins + Convert.ToInt32(dr["NumBins"].ToString());
                        }
                        ds.Dispose();
                        FruPak.PF.PrintLayer.Word.ReplaceText("NumBins", Convert.ToString(num_of_Bins));
                        break;
                }
                //check bool
                bool bol_Print_ind = false;
                try
                {
                    bol_Print_ind = Convert.ToBoolean(dr_bincard["Print_Ind"].ToString());
                }
                catch
                {
                    bol_Print_ind = false;
                }

                if (bol_reprint == true)
                {
                    bol_Print_ind = false;
                }

                if (bol_Print_ind == false)
                {
                    return_code = FruPak.PF.PrintLayer.Word.Print();
                    FruPak.PF.Data.AccessLayer.CM_Bins.Update_Print_Ind(dr_bincard["Barcode"].ToString(), true);
                }
                FruPak.PF.PrintLayer.Word.CloseWord();
            }

            //Delete temp files after printer
            List<string> lst_filenames = new List<string>();
            lst_filenames.AddRange(System.IO.Directory.GetFiles(str_TempPath, "*.jpg", System.IO.SearchOption.TopDirectoryOnly));

            foreach (string filename in lst_filenames)
            {
                System.IO.File.Delete(filename);
            }

            return return_code;
        }

        private static List<string> filetomerge = new List<string>();
    }
}