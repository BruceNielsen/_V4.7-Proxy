using System;
using System.Data;
using System.Windows.Forms;

namespace PF.Utils.UserControls
{
    public partial class Customer : UserControl
    {
        // Events raised by this class to indicate when variety and fruit type combo boxes have changed.
        // In method that captures the combobox changed events need to raise these events.
        public event EventHandler CustomerChanged;

        public Customer()
        {
            InitializeComponent();

            // 20/05/2015 - BN - Identified source of really annoying bug - PF.Utils.UserControls.Customer
            // This customer control was executing code in the designer instead of ONLY at runtime,
            // causing VS/SQL to crash, and would delete the code and control if you tried to ignore the error,
            // thereby mangling the code and rendering the customer UserControl useless and breaking the host form.
            //
            // Initially I found it to be trying to use an old Sql Instance
            // (Bruce-Laptop) instead of the new one (-\SQLEXPRESS) which was a leftover from the default config file,
            // But on further investigation I ran across this article on CodeProject:
            // http://www.codeproject.com/Tips/59203/Prevent-code-executing-at-Design-Time-in-Visual-St?msg=4455135#xx4455135xx
            // Which solved the problem after a little bit of tinkering
            //
            // This does not work reliably:
            //if (!DesignMode)
            //{
            //    Console.WriteLine("DesignMode = False");
            //}
            //else
            //{
            //    Console.WriteLine("DesignMode = True");
            //}

            // This works perfectly:
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            {
                Console.WriteLine("PF.Utils.UserControls.Customer - UsageMode = Designtime - Skipping populate()");
            }
            else
            {
                Console.WriteLine("PF.Utils.UserControls.Customer - UsageMode = Runtime - Running populate()");
                populate();
            }
        }

        public void populate()
        {
            DataSet ds = PF.Data.AccessLayer.PF_Customer.Get_Info();
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
                    string str_Name = "";
                    DataSet ds = PF.Data.AccessLayer.PF_Customer.Get_Info(Customer_Id);
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

        public bool CFocused { get; set; }

        /// <summary>
        /// Made this property public so I can trigger it externally - BN 4/12/2015
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void cmb_Customer_SelectionChangeCommitted(object sender, EventArgs e)
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