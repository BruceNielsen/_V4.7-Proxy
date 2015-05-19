using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;

namespace FruPak.PF.Utils.Common
{
    public partial class Material_Rate_Relat : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();     

        private static int int_Current_User_Id = 0;
        private static bool bol_write_access;
        private static string str_table = "";

        public Material_Rate_Relat(string table, int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();
            //restrict access



            bol_write_access = bol_w_a;
            btn_Update_Relationship.Enabled = bol_w_a;
            int_Current_User_Id = int_C_User_id;
            str_table = table;
            switch (str_table)
            {
                case "PF_A_Rates":
                    this.Text = "Material Rate Relationship";
                    break;
                case "PF_Stock_Item":
                    this.Text = "Material Stock Relationship";
                    break;
            }
            //check if testing or not
            //if (FruPak.PF.Global.Global.bol_Testing == true)
            //{
            //    this.Text = "FruPak Process Factory - " + this.Text + " - Test Environment";
            //}
            //else
            //{
            //    this.Text = "FruPak Process Factory";
            //}
            populate_check_boxList1();
            populate_check_boxList2();

            #region Log any interesting events from the UI to the CSV log file
            foreach (Control c in this.Controls)
            {
                if (c.GetType() == typeof(Button))
                {
                    c.Click += new EventHandler(this.Control_Click);
                }
                else if (c.GetType() == typeof(TextBox))
                {
                    c.Validated += new EventHandler(this.Control_Validated);
                }
                else if (c.GetType() == typeof(ComboBox))
                {
                    ComboBox cb = (ComboBox)c;
                    cb.SelectedValueChanged += new EventHandler(this.Control_SelectedValueChanged);
                }
                else if (c.GetType() == typeof(DateTimePicker))
                {
                    DateTimePicker dtp = (DateTimePicker)c;
                    dtp.ValueChanged += new EventHandler(this.Control_ValueChanged);
                }
                else if (c.GetType() == typeof(NumericUpDown))
                {
                    NumericUpDown nud = (NumericUpDown)c;
                    nud.ValueChanged += new EventHandler(this.Control_NudValueChanged);
                }
                else if (c.GetType() == typeof(CheckBox))
                {
                    CheckBox cb = (CheckBox)c;
                    cb.CheckedChanged += new EventHandler(this.Control_CheckedChanged);
                }

                else if (c.GetType() == typeof(FruPak.PF.Utils.UserControls.Customer))
                {
                    FruPak.PF.Utils.UserControls.Customer cust = (FruPak.PF.Utils.UserControls.Customer)c;
                    cust.CustomerChanged += new EventHandler(this.CustomerControl_CustomerChanged);
                }
            }
            #endregion

        }

        private void populate_check_boxList1()
        {
            checkedListBox1.Items.Clear();

            //get list of products
            DataSet ds_dgv1 = FruPak.PF.Data.AccessLayer.CM_Material.Get_Info();
            DataRow dr_dgv1;


            for (int i = 0; i < Convert.ToInt32(ds_dgv1.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_dgv1 = ds_dgv1.Tables[0].Rows[i];
                checkedListBox1.Items.Add(dr_dgv1["Material_Num"].ToString() + " " + dr_dgv1["Description"].ToString());

            }
            ds_dgv1.Dispose();
        }

        private void populate_check_boxList2()
        {
            checkedListBox2.Items.Clear();
            DataSet ds_dgv2 = null;
            //get list of products
            switch (str_table)
            {
                case "PF_A_Rates":
                    ds_dgv2 = FruPak.PF.Data.AccessLayer.PF_A_Rates.Get_Info();
                    break;
                case "PF_Stock_Item":
                    ds_dgv2 = FruPak.PF.Data.AccessLayer.PF_Stock_Item.Get_Info();
                    break;
            }         
         
            DataRow dr_dgv2;
            for (int i = 0; i < Convert.ToInt32(ds_dgv2.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_dgv2 = ds_dgv2.Tables[0].Rows[i];

                switch (str_table)
                {
                    case "PF_A_Rates":
                        checkedListBox2.Items.Add(dr_dgv2["Rates_Id"].ToString() + " " + dr_dgv2["Code"].ToString() + " " + dr_dgv2["Description"].ToString());
                        break;
                    case "PF_Stock_Item":
                        checkedListBox2.Items.Add(dr_dgv2["Stock_Item_Id"].ToString() + " " + dr_dgv2["Code"].ToString() + " " + dr_dgv2["Description"].ToString());
                        break;
                }             
            }
            ds_dgv2.Dispose();
        }

        private void btn_Update_Relationship_Click(object sender, EventArgs e)
        {
            int int_result = 0;
            int int_result_tot = 0;
            int int_material_Id = 0;

            for (int i_mat = 0; i_mat < Convert.ToInt32(checkedListBox1.Items.Count.ToString()); i_mat++)
            {
                decimal dec_Material_Num = Convert.ToDecimal(checkedListBox1.Items[i_mat].ToString().Substring(0, checkedListBox1.Items[i_mat].ToString().IndexOf(' ')));
                

                if (checkedListBox1.GetItemCheckState(i_mat) == CheckState.Checked)
                {
                    //get the material_id
                    DataSet ds_material = FruPak.PF.Data.AccessLayer.CM_Material.Get_Info(dec_Material_Num);
                    DataRow dr_material;
                    for (int j = 0; j < Convert.ToInt32(ds_material.Tables[0].Rows.Count.ToString()); j++)
                    {
                        dr_material = ds_material.Tables[0].Rows[j];
                        int_material_Id = Convert.ToInt32(dr_material["Material_Id"].ToString());
                    }

                    switch (str_table)
                    {
                        case "PF_A_Rates":
                            FruPak.PF.Data.AccessLayer.CM_Material_Rates_Relationship.Delete_Material(int_material_Id);
                            break;
                        case "PF_Stock_Item":
                            FruPak.PF.Data.AccessLayer.CM_Material_ST_Product_Relationship.Delete_Material(int_material_Id);
                            break;
                    }                   

                    for (int i_rate = 0; i_rate < Convert.ToInt32(checkedListBox2.Items.Count.ToString()); i_rate++)
                    {
                        int int_Id = Convert.ToInt32(checkedListBox2.Items[i_rate].ToString().Substring(0, checkedListBox2.Items[i_rate].ToString().IndexOf(' ')));
                        if (checkedListBox2.GetItemCheckState(i_rate) == CheckState.Checked)
                        {
                            switch (str_table)
                            {
                                case "PF_A_Rates":
                                    int_result = FruPak.PF.Data.AccessLayer.CM_Material_Rates_Relationship.Insert(FruPak.PF.Common.Code.General.int_max_user_id("CM_Material_Rates_Relationship"), int_material_Id, int_Id, int_Current_User_Id);
                                    break;
                                case "PF_Stock_Item":
                                    int_result = FruPak.PF.Data.AccessLayer.CM_Material_ST_Product_Relationship.Insert(FruPak.PF.Common.Code.General.int_max_user_id("CM_Material_ST_Product_Relationship"), int_material_Id, int_Id, int_Current_User_Id);
                                    break;
                            }                           
                        }

                        if (int_result > 0)
                        {
                            int_result_tot = int_result_tot + int_result;
                        }
                        else
                        {
                            int_result_tot = 0;
                        }
                    }
                }
                if (int_result_tot > 0)
                {
                    lbl_message.Text = "Selected Material Number(s) have been updated";
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                }
                else
                {
                    lbl_message.Text = "Selected Material Number(s) have NOT been updated";
                    lbl_message.ForeColor = System.Drawing.Color.Red;

                }
            }
            Reset();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int int_material_Id = 0;
            int i_mat = Convert.ToInt32((sender as CheckedListBox).SelectedIndex.ToString());


            decimal dec_Material_Num = Convert.ToDecimal(checkedListBox1.Items[i_mat].ToString().Substring(0, checkedListBox1.Items[i_mat].ToString().IndexOf(' ')));
            //get the material_id
            DataSet ds_material;
            DataRow dr_material;

            ds_material = FruPak.PF.Data.AccessLayer.CM_Material.Get_Info(dec_Material_Num);
            
            for (int j = 0; j < Convert.ToInt32(ds_material.Tables[0].Rows.Count.ToString()); j++)
            {
                dr_material = ds_material.Tables[0].Rows[j];
                int_material_Id = Convert.ToInt32(dr_material["Material_Id"].ToString());
            }
            ds_material.Dispose();


            switch (str_table)
            {
                case "PF_A_Rates":
                    ds_material = FruPak.PF.Data.AccessLayer.CM_Material_Rates_Relationship.Get_Info_For_Material(int_material_Id);
                    break;
                case "PF_Stock_Item":
                    ds_material = FruPak.PF.Data.AccessLayer.CM_Material_ST_Product_Relationship.Get_Info_For_Material(int_material_Id);
                    break;
            }   

            for (int i = 0; i < Convert.ToInt32(checkedListBox2.Items.Count.ToString()); i++)
            {
                string item = checkedListBox2.Items[i].ToString();
                int int_clb_id = Convert.ToInt32(item.Substring(0, item.IndexOf(' ')));
                checkedListBox2.SetItemCheckState(i, CheckState.Unchecked);
                for (int j = 0; j < Convert.ToInt32(ds_material.Tables[0].Rows.Count.ToString()); j++)
                {
                    dr_material = ds_material.Tables[0].Rows[j];

                    int int_rate_Id = 0;
                    
                    switch (str_table)
                    {
                        case "PF_A_Rates":
                            int_rate_Id = Convert.ToInt32(dr_material["Rates_Id"].ToString());
                            break;
                        case "PF_Stock_Item":
                            int_rate_Id = Convert.ToInt32(dr_material["Stock_Item_Id"].ToString());
                            break;
                    } 

                    if (Convert.ToInt32(item.Substring(0, item.IndexOf(' '))) == int_rate_Id)
                    {
                        checkedListBox2.SetItemCheckState(i, CheckState.Checked);
                    }
                }
            }
            ds_material.Dispose();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            Reset();   
        }
        private void Reset()
        {
            populate_check_boxList1();
            populate_check_boxList2();
        }

        #region Methods to log UI events to the CSV file. BN 29/01/2015
        /// <summary>
        /// Method to log the identity of controls we are interested in into the CSV log file. 
        /// BN 29/01/2015
        /// </summary>
        /// <param name="sender">Control</param>
        /// <param name="e">EventArgs</param>
        private void Control_Click(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(Button))
            {
                Button b = (Button)sender;
                logger.Log(LogLevel.Info, DecorateString(b.Name, b.Text, "Click"));

            }
        }
        private void Control_Validated(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            logger.Log(LogLevel.Info, DecorateString(t.Name, t.Text, "Validated"));
        }
        private void Control_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            logger.Log(LogLevel.Info, DecorateString(cb.Name, cb.Text, "SelectedValueChanged"));
        }
        private void Control_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;
            logger.Log(LogLevel.Info, DecorateString(dtp.Name, dtp.Text, "ValueChanged"));
        }
        private void Control_NudValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            logger.Log(LogLevel.Info, DecorateString(nud.Name, nud.Text, "NudValueChanged"));
        }
        private void Control_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            logger.Log(LogLevel.Info, DecorateString(cb.Name, cb.Checked.ToString(), "CheckedChanged"));
        }
        private void CustomerControl_CustomerChanged(object sender, EventArgs e)
        {
            FruPak.PF.Utils.UserControls.Customer cust = (FruPak.PF.Utils.UserControls.Customer)sender;
            logger.Log(LogLevel.Info, DecorateString(cust.Name, cust.Customer_Name, "TextChanged"));
        }

        #region Decorate String
        // DecorateString
        private string openPad = " --- [ ";
        private string closePad = " ] --- ";
        private string intro = "--->   { ";
        private string outro = " }   <---";

        /// <summary>
        /// Formats the output to the debug log so that the relevant bits stand out amongst the machine-generated stuff.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="input"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private string DecorateString(string name, string input, string action)
        {
            // This code is intentionally duplicated from Main_Menu.cs, as it's faster
            // to find the faulty code - This Class/Form is a known major problem area.

            string output = string.Empty;

            output = intro + name + openPad + input + closePad + action + outro;
            return output;
        }
        #endregion

        /// <summary>
        /// Close the form with the Esc key (Sel request 11-02-2015 BN)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Material_Rate_Relat_KeyDown(object sender, KeyEventArgs e)
        {
            // Note that the KeyPreview property must be set to true for this to work
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        #endregion

    }
}
