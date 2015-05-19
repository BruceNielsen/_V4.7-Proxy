using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FruPak.PF.Utils.UserControls
{
    public partial class BatchNum : UserControl
    {
        public int Work_Order_Id
        {
            get;
            set;
        }
        public BatchNum()
        {
            InitializeComponent();
            populate();
        }
        public void populate()
        {
            DataSet ds_Get_Info;
            //batch numbers
            ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Work_Order.Get_Info(Work_Order_Id);
            DataRow dr_Get_Info;

            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                for (int j = 0; j < Convert.ToInt32(dr_Get_Info["Num_Batches"].ToString()); j++)
                {
                    cmb_Batch.Items.Add(j + 1);
                }
            }
            cmb_Batch.Text = null;
            ds_Get_Info.Dispose();
        }
        // set and get for the batch number for the selected item.
        public int Batch_Num
        {
            get
            {
                if (cmb_Batch.SelectedItem != null)
                {
                   // return Convert.ToInt32((cmb_Batch.SelectedItem as ComboboxValues).GetValue);
                    return Convert.ToInt32(cmb_Batch.SelectedItem);
                }
                return 0;
            }
            set
            {
                if (value == 0)
                {
                    cmb_Batch.SelectedIndex = -1;
                }
                else
                {
                    if (cmb_Batch.Items.Count > 0)
                    {
                        for (int i = 0; i < cmb_Batch.Items.Count; i++)
                        {
                            if (Convert.ToInt32((cmb_Batch.Items[i] as ComboboxValues).GetValue) == value)
                            {
                                cmb_Batch.SelectedIndex = i;
                                i = cmb_Batch.Items.Count + 1;
                            }
                        }
                    }
                }
            }
        }
    }
}
