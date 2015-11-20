using System;
using System.Data;

namespace PF.PrintLayer
{
    public class WPC_Card
    {
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
            int return_code = 98;
            int_Current_User_Id = Current_User_Id;
            Get_Paths();
            DataSet ds_Get_Info = PF.Data.AccessLayer.PF_Pallet.Get_WPCCard(Barcode);
            DataRow dr_Get_Info;
            for (int i = 0; i < ds_Get_Info.Tables[0].Rows.Count; i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                DateTime dt1 = new DateTime();
                DateTime dt2 = new DateTime();
                dt1 = Convert.ToDateTime(dr_Get_Info["Mod_Date"].ToString());
                try
                {
                    dt2 = Convert.ToDateTime(dr_Get_Info["Start_Time"].ToString());
                }
                catch
                {
                    dt2 = Convert.ToDateTime(dr_Get_Info["WO_Start_Time"].ToString());
                }

                PF.PrintLayer.Word.StartWord();
                PF.PrintLayer.Word.ReplaceText("Fruit", dr_Get_Info["FT_Description"].ToString());
                PF.PrintLayer.Word.ReplaceText("Pdate", dt1.Day.ToString() + "/" + dt1.Month.ToString() + "/" + dt1.Year.ToString());
                PF.PrintLayer.Word.ReplaceText("stime", dt2.Hour.ToString() + ":" + String.Format("{0:mm}", dt2));
                PF.PrintLayer.Word.ReplaceText("ftime", dt1.Hour.ToString() + ":" + String.Format("{0:mm}", dt1));
                PF.PrintLayer.Word.ReplaceText("Variety", dr_Get_Info["FV_Description"].ToString());
                PF.PrintLayer.Word.ReplaceText("Batch", dr_Get_Info["Pallet_Number"].ToString());
                PF.PrintLayer.Word.ReplaceText("Net", dr_Get_Info["Weight_Nett"].ToString());
                PF.PrintLayer.Word.ReplaceText("Gross", dr_Get_Info["Weight_Gross"].ToString());

                if (bol_print == true)
                {
                    return_code = PF.PrintLayer.Word.Print();
                    //PF.PrintLayer.Word.FilePath = @"C:\dave";
                    //PF.PrintLayer.Word.FileName = "dave";
                    //PF.PrintLayer.Word.SaveAsPdf();
                }
                else
                {
                    return_code = PF.PrintLayer.Word.SaveAS();
                }

                PF.PrintLayer.Word.CloseWord();
            }
            ds_Get_Info.Dispose();

            return return_code;
        }

        private static void Get_Paths()
        {
            DataSet ds_Get_Info = PF.Data.AccessLayer.CM_System.Get_Info_Like("PF%");
            DataRow dr_Get_Info;
            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                switch (dr_Get_Info["Code"].ToString())
                {
                    case "PF-TPath":
                        PF.PrintLayer.Word.TemplatePath = dr_Get_Info["Value"].ToString();

                        break;

                    case "PF-TWPC":
                        PF.PrintLayer.Word.TemplateName = dr_Get_Info["Value"].ToString();
                        break;
                }
            }
            ds_Get_Info.Dispose();
        }
    }
}