using System.IO;

namespace PF.PrintLayer
{
    public class Labels
    {
        public static string TemplatePath
        {
            get;
            set;
        }

        public static string TemplateName
        {
            get;
            set;
        }

        /// <summary>
        /// This method is for print bin lables/cards
        /// </summary>
        /// <param name="Barcode"></param>
        /// <param name="Material"></param>
        /// <param name="Trader"></param>
        /// <param name="Variety"></param>
        /// <param name="Grade"></param>
        /// <param name="Size"></param>
        /// <param name="Date"></param>
        /// <param name="testing"></param>
        public static void Bin_Labels(string Barcode, string Material, string Trader, string Variety, string Grade, string Size, string Date, bool testing)
        {
            PF.PrintLayer.Word.TemplatePath = TemplatePath;
            PF.PrintLayer.Word.TemplateName = TemplateName;

            string str_image_location = "";
            PF.PrintLayer.Barcode.str_Barcode = Barcode;
            PF.PrintLayer.Barcode.Width = 450;
            PF.PrintLayer.Barcode.Height = 100;
            PF.PrintLayer.Barcode.RotateFlip = System.Drawing.RotateFlipType.Rotate270FlipNone;
            str_image_location = PF.PrintLayer.Barcode.Generate_Barcode();

            PF.PrintLayer.Word.StartWord();

            PF.PrintLayer.Word.BarcodeFull = str_image_location;
            PF.PrintLayer.Word.Add_Barcode("Barcode1");

            PF.PrintLayer.Word.ReplaceText("Material", Material);
            try
            {
                PF.PrintLayer.Word.ReplaceText("Trader", Trader.ToUpper().Substring(0, 12));
            }
            catch
            {
                PF.PrintLayer.Word.ReplaceText("Trader", Trader.ToUpper());
            }
            try
            {
                PF.PrintLayer.Word.ReplaceText("Variety", Variety.ToUpper().Substring(0, 10));
            }
            catch
            {
                PF.PrintLayer.Word.ReplaceText("Variety", Variety.ToUpper());
            }
            PF.PrintLayer.Word.ReplaceText("Grade", Grade.ToUpper());
            PF.PrintLayer.Word.ReplaceText("Size", Size.ToUpper());
            PF.PrintLayer.Word.ReplaceText("Date", Date);

            if (testing == false)
            {
                PF.PrintLayer.Word.Print();
            }
            else
            {
                PF.PrintLayer.Word.FilePath = @"C:\dave";
                PF.PrintLayer.Word.FileName = "Bin.docx";
                PF.PrintLayer.Word.SaveAS();
            }

            //close word
            PF.PrintLayer.Word.CloseWord();

            //Clean Up
            if (File.Exists(str_image_location) == true)
            {
                File.Delete(str_image_location);
            }
        }
    }
}