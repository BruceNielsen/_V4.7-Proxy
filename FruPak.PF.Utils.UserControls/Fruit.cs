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
    public partial class Fruit : UserControl
    {
        // Events raised by this class to indicate when variety and fruit type combo boxes have changed.
        // In method that captures the combobox changed events need to raise these events.
        public event EventHandler FruitVarietyChanged;
        public event EventHandler FruitTypeChanged;


        private int int_Block_Id = 0;
        
        public Fruit()
        {
            InitializeComponent();
            populate_Combos();
        }

        public int Block_Id
        {
            get
            {
                return int_Block_Id;
            }
            set
            {
                int_Block_Id = value;
                populate_Combos();
            }
            
        }

        private void populate_Combos()
        {
            DataSet ds_get_info;
            DataRow dr_get_info;

            // populate fruit type combobox, default value is the first.
            if (int_Block_Id != 0)
            {
                ds_get_info = FruPak.PF.Data.AccessLayer.CM_Fruit_Type.Get_Info_B(int_Block_Id);
            }
            else
            {
                ds_get_info = FruPak.PF.Data.AccessLayer.CM_Fruit_Type.Get_Info();
            }
            //cmb_Fruit_Type.Items.Clear();
            ArrayList FruitTypes = new ArrayList();
            cmb_Fruit_Type.DataSource = null;
            for (int i = 0; i < Convert.ToInt32(ds_get_info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_get_info = ds_get_info.Tables[0].Rows[i];
                FruitTypes.Add(new ComboboxValues(dr_get_info["FruitType_Id"].ToString(), dr_get_info["Description"].ToString()));
            }
            
            cmb_Fruit_Type.DisplayMember = "GetDescription";
            cmb_Fruit_Type.ValueMember = "GetValue";
            cmb_Fruit_Type.DataSource = FruitTypes;
      

            // populate variety combo box using block info if have it.
            populate_Variety_Box(Convert.ToInt32((cmb_Fruit_Type.SelectedItem as ComboboxValues).GetValue));


        }

        private void populate_Variety_Box(int fruittype)
        {
            DataSet ds_get_info;
            // Get list of varieties based on selected fruit type, use block info as well if have it
            if (int_Block_Id != 0)
            {
                if (fruittype == 0)
                {
                    ds_get_info = FruPak.PF.Data.AccessLayer.CM_Block_Variety_Relationship.Get_Info(int_Block_Id);
                }
                else
                {
                    ds_get_info = FruPak.PF.Data.AccessLayer.CM_Block_Variety_Relationship.Get_Info_Varieties(int_Block_Id, fruittype);
                }
            }
            else
            {
                if (fruittype == 0)
                {
                    ds_get_info = FruPak.PF.Data.AccessLayer.CM_Fruit_Variety.Get_Info();
                }
                else
                {
                    ds_get_info = FruPak.PF.Data.AccessLayer.CM_Fruit_Variety.Get_Info(fruittype);
                }
            }
            // Populate the list box using an array as DataSource.
            ArrayList Varieties = new ArrayList();
            DataRow dr_get_info;
            cmb_Variety.DataSource = null;

            for (int i = 0; i < Convert.ToInt32(ds_get_info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_get_info = ds_get_info.Tables[0].Rows[i];
                Varieties.Add(new ComboboxValues(dr_get_info["Variety_Id"].ToString(), dr_get_info["Variety"].ToString() + " : " + dr_get_info["Description"].ToString()));
            }

            cmb_Variety.DisplayMember = "GetDescription";
            cmb_Variety.ValueMember = "GetValue";
            cmb_Variety.DataSource = Varieties;
            cmb_Variety.Text = null;
        }

 
        private void cmb_Fruit_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            int fruittype;
            if ((sender as ComboBox).SelectedItem == null)
            {
                fruittype = 0;
            } else {
                fruittype = Convert.ToInt32(((sender as ComboBox).SelectedItem as ComboboxValues).GetValue);
            }
            populate_Variety_Box(fruittype);


            // passes on the fruittype changed event outside of this control.  Use FruitTypeChanged property.
            EventHandler handler = FruitTypeChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        // set and get for the fruit variety for the selected item.
        public int FruitVariety_Id
        {
            get
            {
                if (cmb_Variety.SelectedItem != null)
                {
                    return Convert.ToInt32((cmb_Variety.SelectedItem as ComboboxValues).GetValue);
                }
                return 0;
            }
            set
            {
                if (value == 0)
                {
                    cmb_Variety.SelectedIndex = -1;
                }
                else
                {
                    if (cmb_Variety.Items.Count > 0)
                    {
                        for (int i = 0; i < cmb_Variety.Items.Count; i++)
                        {
                            if (Convert.ToInt32((cmb_Variety.Items[i] as ComboboxValues).GetValue) == value)
                            {
                                cmb_Variety.SelectedIndex = i;
                                i = cmb_Variety.Items.Count + 1;
                            }
                        }
                    }
                }
            }
        }

        // set and get for fruittype id for the selected item
        public int FruitType_Id
        {
            get
            {
                if (cmb_Fruit_Type.SelectedItem != null)
                {
                    return Convert.ToInt32((cmb_Fruit_Type.SelectedItem as ComboboxValues).GetValue);
                }
                return 0;
            }
            set
            {
                if (value == 0)
                {
                    cmb_Fruit_Type.SelectedIndex = -1;
                } 
                else
                {
                    if (cmb_Fruit_Type.Items.Count > 0)
                    {
                        for (int i = 0; i < cmb_Fruit_Type.Items.Count; i++)
                        {
                            if (Convert.ToInt32((cmb_Fruit_Type.Items[i] as ComboboxValues).GetValue) == value)
                            {
                                cmb_Fruit_Type.SelectedIndex = i;
                                i = cmb_Fruit_Type.Items.Count + 1;
                                // call handler to modify variety list
                                EventHandler handler = FruitTypeChanged;
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

        private void cmb_Variety_SelectedIndexChanged(object sender, EventArgs e)
        {
            // passes on the fruittype changed event outside of this control.  Use FruitVarietyChanged property.
            EventHandler handler = FruitVarietyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }




    // Class to hold a value_member and a description_member for each combobox.  
    // Enables the description to be displayed in the drop down box and a different value to be returned when an item is selected.
    public class ComboboxValues
    {
        private string myValue;
        private string myDescription;

        public ComboboxValues(string strValue, string strDescription)
        {

            this.myValue = strValue;
            this.myDescription = strDescription;
        }

        public string GetValue
        {
            get
            {
                return myValue;
            }
            set
            {
                myValue = value;
            }
        }

        public string GetDescription
        {

            get
            {
                return myDescription;
            }
            set
            {
                myDescription = value;
            }
        }

    }
}
