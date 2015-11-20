using System.Data;

namespace PF.PrintLayer
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
            DataSet ds_default = PF.Data.AccessLayer.CM_System.Get_Info_Like("PF%");
            DataRow dr_default;
            for (int i_d = 0; i_d < ds_default.Tables[0].Rows.Count; i_d++)
            {
                dr_default = ds_default.Tables[0].Rows[i_d];
                switch (dr_default["Code"].ToString())
                {
                    case "PF-TPath":
                        PF.PrintLayer.Word.TemplatePath = dr_default["Value"].ToString();
                        break;

                    case "PF-TAdrlb1":
                        PF.PrintLayer.Word.TemplateName = dr_default["Value"].ToString();
                        break;
                }
            }
            ds_default.Dispose();

            PF.PrintLayer.Word.StartWord();
            PF.PrintLayer.Word.ReplaceText("Customer", str_delivery_name);
            PF.PrintLayer.Word.ReplaceText("Address", str_Address);
            PF.PrintLayer.Word.ReplaceText("City", str_City);

            if (bol_print == true)
            {
                return_code = PF.PrintLayer.Word.Print();
            }
            else
            {
                return_code = PF.PrintLayer.Word.SaveAS();
            }

            PF.PrintLayer.Word.CloseWord();
            return return_code;
        }
    }
}