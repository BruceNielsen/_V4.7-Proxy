using System.IO;

namespace FruPak.PF.PrintLayer
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
            FruPak.PF.PrintLayer.Word.TemplatePath = TemplatePath;
            FruPak.PF.PrintLayer.Word.TemplateName = TemplateName;

            string str_image_location = "";
            FruPak.PF.PrintLayer.Barcode.str_Barcode = Barcode;
            FruPak.PF.PrintLayer.Barcode.Width = 450;
            FruPak.PF.PrintLayer.Barcode.Height = 100;
            FruPak.PF.PrintLayer.Barcode.RotateFlip = System.Drawing.RotateFlipType.Rotate270FlipNone;
            str_image_location = FruPak.PF.PrintLayer.Barcode.Generate_Barcode();

            FruPak.PF.PrintLayer.Word.StartWord();

            FruPak.PF.PrintLayer.Word.BarcodeFull = str_image_location;
            FruPak.PF.PrintLayer.Word.Add_Barcode("Barcode1");

            FruPak.PF.PrintLayer.Word.ReplaceText("Material", Material);
            try
            {
                FruPak.PF.PrintLayer.Word.ReplaceText("Trader", Trader.ToUpper().Substring(0, 12));
            }
            catch
            {
                FruPak.PF.PrintLayer.Word.ReplaceText("Trader", Trader.ToUpper());
            }
            try
            {
                FruPak.PF.PrintLayer.Word.ReplaceText("Variety", Variety.ToUpper().Substring(0, 10));
            }
            catch
            {
                FruPak.PF.PrintLayer.Word.ReplaceText("Variety", Variety.ToUpper());
            }
            FruPak.PF.PrintLayer.Word.ReplaceText("Grade", Grade.ToUpper());
            FruPak.PF.PrintLayer.Word.ReplaceText("Size", Size.ToUpper());
            FruPak.PF.PrintLayer.Word.ReplaceText("Date", Date);

            if (testing == false)
            {
                FruPak.PF.PrintLayer.Word.Print();
            }
            else
            {
                FruPak.PF.PrintLayer.Word.FilePath = @"C:\dave";
                FruPak.PF.PrintLayer.Word.FileName = "Bin.docx";
                FruPak.PF.PrintLayer.Word.SaveAS();
            }

            //close word
            FruPak.PF.PrintLayer.Word.CloseWord();

            //Clean Up
            if (File.Exists(str_image_location) == true)
            {
                File.Delete(str_image_location);
            }
        }
    }
}