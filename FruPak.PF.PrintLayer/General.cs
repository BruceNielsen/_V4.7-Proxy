using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace FruPak.PF.PrintLayer
{
    public class General
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static void Write_Log(string Log_File, string txt_msg)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Log_File, true))
            {
                file.WriteLine(txt_msg);
                file.WriteLine(" ");
            }
        }
        public static List<string> view_list
        {
            get;
            set;
        }
        public static int Print(string str_type, string str_save_filename, string str_Invoice_Date_yyyymmdd)
        {
            Console.WriteLine("PrintLayer.General Print: Filename: " + str_save_filename);
            logger.Log(LogLevel.Info, "Filename: " + str_save_filename);

            int int_result = 0;
            

            switch (str_type)
            {
                case "Print":
                    FruPak.PF.PrintLayer.Word.Print();
                    break;
                case "Email":
                case "EmailO":
                case "View":
                    str_save_filename = str_save_filename + " - " + str_Invoice_Date_yyyymmdd + ".PDF";
                    FruPak.PF.PrintLayer.Word.FileName = str_save_filename;
                    int_result = FruPak.PF.PrintLayer.Word.SaveAsPdf();
                    FruPak.PF.PrintLayer.General.view_list.Add(str_save_filename);
                    logger.Log(LogLevel.Info, "Final Filename: " + str_save_filename);
                    break;
            }
            return int_result;  // 0 = good
        } 
    }
}
