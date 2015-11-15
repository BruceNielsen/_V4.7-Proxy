using System.Data;

namespace FruPak.PF.PrintLayer
{
    public class Sticky_Address_Label
    {
        public static int Print(string Data, bool bol_print)
        {
            int return_code = 98;

            string[] DataParts = Data.Split(':');

            return_code = Print(DataParts[0], DataParts[1], DataParts[2], bol_print);
            return return_code;
        }

        public static int Print(string str_delivery_name, string str_Address, string str_City, bool bol_print)
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

                    case "PF-TAdrlb1":
                        FruPak.PF.PrintLayer.Word.TemplateName = dr_default["Value"].ToString();
                        break;
                }
            }
            ds_default.Dispose();

            FruPak.PF.PrintLayer.Word.StartWord();
            FruPak.PF.PrintLayer.Word.ReplaceText("Customer", str_delivery_name);
            FruPak.PF.PrintLayer.Word.ReplaceText("Address", str_Address);
            FruPak.PF.PrintLayer.Word.ReplaceText("City", str_City);

            if (bol_print == true)
            {
                return_code = FruPak.PF.PrintLayer.Word.Print();
            }
            else
            {
                return_code = FruPak.PF.PrintLayer.Word.SaveAS();
            }

            FruPak.PF.PrintLayer.Word.CloseWord();
            return return_code;
        }
    }
}