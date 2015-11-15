using System;
using System.Data;
using System.Windows.Forms;

namespace FruPak.PF.Utils.UserControls
{
    public partial class WODisplay : UserControl
    {
        public WODisplay()
        {
            InitializeComponent();
        }

        public int Work_Order_Id
        {
            get;
            set;
        }

        public int Product_Id
        {
            get;
            set;
        }

        public int FruitType_Id
        {
            get;
            set;
        }

        public int Variety_Id
        {
            get;
            set;
        }

        public string Process_Date
        {
            get;
            set;
        }

        public void populate()
        {
            DataSet ds_Get_Info;
            DataRow dr_Get_Info;
            ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Work_Order.Get_Info_translated(Work_Order_Id);

            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                txt_Process_Date.Text = dr_Get_Info["Process_Date"].ToString();
                Process_Date = dr_Get_Info["Process_Date"].ToString();
                txt_Product.Text = dr_Get_Info["Product"].ToString();
                Product_Id = Convert.ToInt32(dr_Get_Info["Product_Id"].ToString());
                txt_Work_Order.Text = dr_Get_Info["Work_Order_Id"].ToString();
                txt_Fruit_Type.Text = dr_Get_Info["FruityType"].ToString();
                FruitType_Id = Convert.ToInt32(dr_Get_Info["FruitType_Id"].ToString());
                txt_Fruit_Variety.Text = dr_Get_Info["FruitVariety"].ToString();
                Variety_Id = Convert.ToInt32(dr_Get_Info["Variety_Id"].ToString());
            }
        }
    }
}