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
    public partial class Customer : UserControl
    {
        // Events raised by this class to indicate when variety and fruit type combo boxes have changed.
        // In method that captures the combobox changed events need to raise these events.
        public event EventHandler CustomerChanged;
        public Customer()
        {
            InitializeComponent();
            populate();
        }
        public void populate()
        {
            DataSet ds = FruPak.PF.Data.AccessLayer.PF_Customer.Get_Info();
            cmb_Customer.DataSource = ds.Tables[0];
            cmb_Customer.DisplayMember = "Name";
            cmb_Customer.ValueMember = "Customer_Id";
            cmb_Customer.Text = null;            
            ds.Dispose();
        }
        // set and get the Customer_Id
        public int Customer_Id
        {
            get
            {
                if (cmb_Customer.SelectedItem != null)
                {
                    return Convert.ToInt32(cmb_Customer.SelectedValue.ToString());
                }
                return 0;
            }
            set
            {
                if (value == 0)
                {
                    cmb_Customer.SelectedIndex = -1;
                }
                else
                {
                    cmb_Customer.SelectedValue = value;
                }
            }
        }
        public string Customer_Name
        {
            get
            {
                if (cmb_Customer.SelectedItem != null)
                {
                    string str_Name="";
                    DataSet ds = FruPak.PF.Data.AccessLayer.PF_Customer.Get_Info(Customer_Id);
                    DataRow dr;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dr = ds.Tables[0].Rows[i];                        
                        str_Name = dr["Name"].ToString();
                    }
                    ds.Dispose();
                    return str_Name;
                }
                return "Unknown Customer";
            }
        }

        public bool CFocused { get; set;}
        private void cmb_Customer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Customer_Id = Convert.ToInt32(cmb_Customer.SelectedValue.ToString());
            CFocused = true;
            // passes on the fruit type changed event outside of this control.  Use FruitVarietyChanged property.
            EventHandler handler = CustomerChanged;
           
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
