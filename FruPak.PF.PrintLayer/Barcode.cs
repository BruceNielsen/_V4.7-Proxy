using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FruPak.PF.PrintLayer
{

    public class Barcode
    {          
        
        public static string str_Barcode
        {
            get;
            set;
        }
        public static string Counter
        {
            get;
            set;
        }
        public static string Country
        { 
            get;
            set;
        }
        public static string Trader
        {
            get;
            set;
        }

        public static int Width
        {
            get;
            set;
        }
        public static  int Height
        {
            get;
            set;
        }
        public static System.Drawing.RotateFlipType RotateFlip
        {
            get;
            set;
        }
        public static string Generate_Barcode()
        {
            if (str_Barcode == "")
            {
                str_Barcode = Country + Trader + Counter;
            }

            BarcodeLib.Barcode bar = new BarcodeLib.Barcode();
            System.Windows.Forms.PictureBox pb = new System.Windows.Forms.PictureBox();
            bar.IncludeLabel = true;

            bar.RotateFlipType = RotateFlip;
          
            pb.Image = bar.Encode(BarcodeLib.TYPE.CODE128, str_Barcode, Width, Height);


            string str_path = "";

            DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_System.Get_Info_Like("PF%");
            DataRow dr_Get_Info;
            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                switch (dr_Get_Info["Code"].ToString())
                {
                    case "PF-TPPath":
                        str_path = dr_Get_Info["Value"].ToString();
                        break;

                }
            }
            ds_Get_Info.Dispose();

            bar.SaveImage(str_path + @"\" + str_Barcode + ".jpg", BarcodeLib.SaveTypes.JPG);

            return str_path + @"\" + str_Barcode + ".jpg";
           
        
        }

    }


}
