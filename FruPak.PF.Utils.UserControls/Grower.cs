using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FruPak.PF.Utils.UserControls
{
    public partial class Grower : UserControl
    {
        // Events raised by this class to indicate when variety and fruit type combo boxes have changed.
        // In method that captures the combobox changed events need to raise these events.
        public event EventHandler OrchardistChanged;
        public event EventHandler GrowerChanged;

        public bool bol_test
        {
            get;
            set;
        }


        public Grower()
        {
            InitializeComponent();

            // BN 23/06/2015 This is screwing up the test function,
            // causing the program to look in the wrong database,
            // which leads to completely unexpected results when testing what's needed to clean out the database
            // I don't know what further ramifications this will have later on.
            // ----------------------------------------------
            //FruPak.PF.Global.Global.bol_Testing = bol_test;
            // ----------------------------------------------

            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            {
                Console.WriteLine("FruPak.PF.Utils.UserControls.Grower - UsageMode = Designtime - Skipping populate()");
            }
            else
            {
                Console.WriteLine("FruPak.PF.Utils.UserControls.Grower - UsageMode = Runtime - Running populate()");
                populate_Orchardist();
            }

        }

        private void populate_Orchardist()
        {
            DataSet ds_get_info;
            DataRow dr_get_info;

            // populate orchardist combobox, default value is the first.
            ds_get_info = FruPak.PF.Data.AccessLayer.CM_Orchardist.Get_Info();

            ArrayList Orchardists = new ArrayList();
            cmb_Orchardist.DataSource = null;
            for (int i = 0; i < Convert.ToInt32(ds_get_info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_get_info = ds_get_info.Tables[0].Rows[i];
                Orchardists.Add(new ComboboxValues(dr_get_info["Orchardist_Id"].ToString(), dr_get_info["Description"].ToString()));
            }

            cmb_Orchardist.DisplayMember = "GetDescription";
            cmb_Orchardist.ValueMember = "GetValue";
            cmb_Orchardist.DataSource = Orchardists;
            cmb_Orchardist.SelectedIndex = -1;

            // populate variety combo box.
            if (cmb_Orchardist.SelectedItem == null)
                populate_Grower_Box(0);
            else
                populate_Grower_Box(Convert.ToInt32((cmb_Orchardist.SelectedItem as ComboboxValues).GetValue));


        }

        private void populate_Grower_Box(int Orchardist_Id)
        {
            DataSet ds_get_info;
            // Get list of Growers based on selected Orchardist

            if (Orchardist_Id == 0)
            {
                ds_get_info = FruPak.PF.Data.AccessLayer.CM_Grower.Get_Info();
            }
            else
            {
                ds_get_info = FruPak.PF.Data.AccessLayer.CM_Grower.Get_Info(Convert.ToInt32(Orchardist_Id));
            }

            // Populate the list box using an array as DataSource.
            ArrayList Growers = new ArrayList();
            DataRow dr_get_info;
            cmb_Grower.DataSource = null;

            for (int i = 0; i < Convert.ToInt32(ds_get_info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_get_info = ds_get_info.Tables[0].Rows[i];
                Growers.Add(new ComboboxValues(dr_get_info["Grower_Id"].ToString(), dr_get_info["RPin"].ToString()));
            }

            cmb_Grower.DisplayMember = "GetDescription";
            cmb_Grower.ValueMember = "GetValue";
            cmb_Grower.DataSource = Growers;
            cmb_Grower.Text = null;
        }

        private void cmb_Orchardist_SelectedIndexChanged(object sender, EventArgs e)
        {
            int orchardist_id;
            if ((sender as ComboBox).SelectedItem == null)
            {
                orchardist_id = 0;
            }
            else
            {
                orchardist_id = Convert.ToInt32(((sender as ComboBox).SelectedItem as ComboboxValues).GetValue);
            }
            populate_Grower_Box(orchardist_id);


            // passes on the orchardist changed event outside of this control.  Use OrchardistChanged property.
            EventHandler handler = OrchardistChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        // Crashes here in design mode when opening WO_Create designer
        // set and get for the grower id for the selected item.
        public int Grower_Id
        {
            get
            {
                //if (!DesignMode)
                //{
                //}


                    if (cmb_Grower.SelectedItem != null)
                    {
                        return Convert.ToInt32((cmb_Grower.SelectedItem as ComboboxValues).GetValue);
                    }

                return 0;
            }
            set
            {
                if (value == 0)
                {
                    // Avoid designer crash
                    if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                    {
                        Console.WriteLine("FruPak.PF.WorkOrder - UsageMode = Designtime");
                        cmb_Grower.SelectedIndex = 0;
                    }
                }
                else
                {
                    if (cmb_Grower.Items.Count > 0)
                    {
                        for (int i = 0; i < cmb_Grower.Items.Count; i++)
                        {
                            if (Convert.ToInt32((cmb_Grower.Items[i] as ComboboxValues).GetValue) == value)
                            {
                                cmb_Grower.SelectedIndex = i;
                                i = cmb_Grower.Items.Count + 1;
                            }
                        }
                    }
                }
            }
        }

        // set and get for orchardist id for the selected item
        public int Orchardist_Id
        {
            get
            {
                if (cmb_Orchardist.SelectedItem != null)
                {
                    return Convert.ToInt32((cmb_Orchardist.SelectedItem as ComboboxValues).GetValue);
                }
                return 0;
            }
            set
            {
                if (value == 0)
                {
                    cmb_Orchardist.SelectedIndex = -1;
                }
                else
                {
                    if (cmb_Orchardist.Items.Count > 0)
                    {
                        for (int i = 0; i < cmb_Orchardist.Items.Count; i++)
                        {
                            if (Convert.ToInt32((cmb_Orchardist.Items[i] as ComboboxValues).GetValue) == value)
                            {
                                cmb_Orchardist.SelectedIndex = i;
                                i = cmb_Orchardist.Items.Count + 1;
                                // call handler to modify grower list
                                EventHandler handler = OrchardistChanged;
                                if (handler != null)
                                {
                                    handler(this, null);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void cmb_Grower_SelectedIndexChanged(object sender, EventArgs e)
        {
            // passes on the grower changed event outside of this control.  Use GrowerChanged property.
            EventHandler handler = GrowerChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }




    }


}
