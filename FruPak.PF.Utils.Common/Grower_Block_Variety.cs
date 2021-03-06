﻿using System;
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
    public partial class Grower_Block_Variety : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static bool bol_write_access;
        private static int int_Current_User_Id = 0;
        private static int int_Grower_Id = 0;
        private static int int_Block_Id = 0;

        public Grower_Block_Variety(int int_Grow_Id, int int_blk_Id, int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();

            int_Current_User_Id = int_C_User_id;
            int_Grower_Id = int_Grow_Id;
            txt_rpin.Text = FruPak.PF.Data.AccessLayer.CM_Grower.Get_RPIN(int_Grower_Id);

            int_Block_Id = int_blk_Id;
            txt_Block.Text = FruPak.PF.Data.AccessLayer.CM_Block.Get_Block(int_Block_Id);

            //restrict access
            bol_write_access = bol_w_a;
            btn_Add.Enabled = bol_w_a;
            //check if testing or not

            //if (FruPak.PF.Global.Global.bol_Testing == true)
            //{
            //    this.Text = "FruPak Process Factory - " + this.Text + " - Test Environment";
            //}
            //else
            //{
            //    this.Text = "FruPak Process Factory";
            //}
            AddColumnsProgrammatically();
            populate_datagridview1();
            populate_combobox();

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

        private void AddColumnsProgrammatically()
        {
            var col0 = new DataGridViewTextBoxColumn();
            var col1 = new DataGridViewTextBoxColumn();
            var col2 = new DataGridViewTextBoxColumn();
            var col3 = new DataGridViewTextBoxColumn();
            var col4 = new DataGridViewTextBoxColumn();
            var col5 = new DataGridViewTextBoxColumn();
            var col6 = new DataGridViewTextBoxColumn();
            var col7 = new DataGridViewTextBoxColumn();

            col0.HeaderText = "Id";
            col0.Name = "Id";
            col0.ReadOnly = true;
            col0.Visible = false;

            col1.HeaderText = "Block_Id";
            col1.Name = "Block_Id";
            col1.ReadOnly = true;
            col1.Visible = false;

            col2.HeaderText = "Block";
            col2.Name = "Block";
            col2.ReadOnly = true;

            col3.HeaderText = "Variety_Id";
            col3.Name = "Variety_Id";
            col3.ReadOnly = true;
            col3.Visible = false;

            col4.HeaderText = "Variety";
            col4.Name = "Variety";
            col4.ReadOnly = true;

            col5.HeaderText = "Description";
            col5.Name = "Description";
            col5.ReadOnly = true;

            col6.HeaderText = "Mod_Date";
            col6.Name = "Mod_Date";
            col6.ReadOnly = true;
            col6.Visible = false;

            col7.HeaderText = "Mod_User_id";
            col7.Name = "Mod_User_id";
            col7.ReadOnly = true;
            col7.Visible = false;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2, col3, col4, col5, col6, col7 });
            DataGridViewImageColumn img_delete = new DataGridViewImageColumn();
            dataGridView1.Columns.Add(img_delete);
            img_delete.HeaderText = "Delete";
            img_delete.Name = "Delete";
            img_delete.Image = FruPak.PF.Global.Properties.Resources.delete;
            img_delete.ReadOnly = true;
            img_delete.Visible = bol_write_access;

        }
        private void populate_datagridview1()
        {
            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();

            DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Block_Variety_Relationship.Get_Info(int_Block_Id);
            DataRow dr_Get_Info;
            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                dataGridView1.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_Id = new DataGridViewTextBoxCell();
                DGVC_Id.Value = dr_Get_Info["BlkVar_Relat_Id"].ToString();
                dataGridView1.Rows[i].Cells[0] = DGVC_Id;

                DataGridViewCell DGVC_Block_Id = new DataGridViewTextBoxCell();
                DGVC_Block_Id.Value = dr_Get_Info["Block_Id"].ToString();
                dataGridView1.Rows[i].Cells[1] = DGVC_Block_Id;

                DataGridViewCell DGVC_Block = new DataGridViewTextBoxCell();
                DGVC_Block.Value = dr_Get_Info["Block"].ToString();
                dataGridView1.Rows[i].Cells[2] = DGVC_Block;

                DataGridViewCell DGVC_Variety_Id = new DataGridViewTextBoxCell();
                DGVC_Variety_Id.Value = dr_Get_Info["Variety_Id"].ToString();
                dataGridView1.Rows[i].Cells[3] = DGVC_Variety_Id;

                DataGridViewCell DGVC_Variety = new DataGridViewTextBoxCell();
                DGVC_Variety.Value = dr_Get_Info["Variety"].ToString();
                dataGridView1.Rows[i].Cells[4] = DGVC_Variety;

                DataGridViewCell DGVC_Description = new DataGridViewTextBoxCell();
                DGVC_Description.Value = dr_Get_Info["Description"].ToString();
                dataGridView1.Rows[i].Cells[5] = DGVC_Description;

                DataGridViewCell DGVC_Mod_date = new DataGridViewTextBoxCell();
                DGVC_Mod_date.Value = dr_Get_Info["Mod_date"].ToString();
                dataGridView1.Rows[i].Cells[6] = DGVC_Mod_date;

                DataGridViewCell DGVC_Mod_User_Id = new DataGridViewTextBoxCell();
                DGVC_Mod_User_Id.Value = dr_Get_Info["Mod_User_Id"].ToString();
                dataGridView1.Rows[i].Cells[7] = DGVC_Mod_User_Id;

                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                ds_Get_Info.Dispose();
            }
        }
        private void SizeAllColumns(Object sender, EventArgs e)
        {
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }
        private void populate_combobox()
        {
            DataSet ds_Get_Variety;
            DataRow dr_Get_Variety;
            ds_Get_Variety = FruPak.PF.Data.AccessLayer.EX_Pipfruit_Orchard.Get_Variety(txt_rpin.Text, txt_Block.Text.Substring(0, 1), txt_Block.Text.Substring(1, 1));


            for (int i = 0; i < Convert.ToInt32(ds_Get_Variety.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Variety = ds_Get_Variety.Tables[0].Rows[i];
                cmb_variety.Items.Add(dr_Get_Variety["Variety_code"].ToString() + ": " + dr_Get_Variety["Description"].ToString() + " : " +dr_Get_Variety["Code"].ToString());
            }
            ds_Get_Variety.Dispose();

            ds_Get_Variety = FruPak.PF.Data.AccessLayer.CM_Fruit_Variety.Get_non_pipfruit();


            for (int i = 0; i < Convert.ToInt32(ds_Get_Variety.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Variety = ds_Get_Variety.Tables[0].Rows[i];
                cmb_variety.Items.Add(dr_Get_Variety["fvcode"].ToString() + ": " + dr_Get_Variety["Description"].ToString() + " : " +dr_Get_Variety["ftcode"].ToString());
            }
            ds_Get_Variety.Dispose();
        }
        private static DataSet ds_Get_Info;
        private void btn_Add_Click(object sender, EventArgs e)
        {
            Add_btn();
        }
        private void Add_btn()
        {
            DialogResult DLR_MessageBox = new DialogResult();
            int int_result = 0;
            int int_variety_Id = 0;
            string str_msg = "";

            if (btn_Add.Text == "&Add")
            {
                DataSet ds_Get_Variety_ID = FruPak.PF.Data.AccessLayer.CM_Fruit_Variety.Get_Info(cmb_variety.SelectedItem.ToString().Substring(0, cmb_variety.SelectedItem.ToString().IndexOf(":")));
                DataRow dr_Get_Variety_ID;
                for (int i = 0; i < Convert.ToInt32(ds_Get_Variety_ID.Tables[0].Rows.Count.ToString()); i++)
                {
                    dr_Get_Variety_ID = ds_Get_Variety_ID.Tables[0].Rows[i];
                    int_variety_Id = Convert.ToInt32(dr_Get_Variety_ID["Variety_Id"].ToString());

                    ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Block_Variety_Relationship.Get_Info(int_Block_Id, int_variety_Id);
                }

                if (Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()) > 0)
                {
                    str_msg = str_msg + "Invalid Variety: This variety has already been allocated to a Grower/Block. Please select another Variety" + Environment.NewLine;
                }
            }
            if (str_msg.Length > 0)
            {
                DLR_MessageBox = MessageBox.Show(str_msg, "Common - Grower", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (DLR_MessageBox != DialogResult.OK)
            {
                switch (btn_Add.Text)
                {
                    case "&Add":
                        int_result = FruPak.PF.Data.AccessLayer.CM_Block_Variety_Relationship.Insert(FruPak.PF.Common.Code.General.int_max_user_id("CM_Block_Variety_Relationship"), int_Block_Id, int_variety_Id, int_Current_User_Id);
                        break;
                }
            }

            if (int_result > 0)
            {
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                lbl_message.Text = "Variety has been added";
            }
            else
            {
                lbl_message.ForeColor = System.Drawing.Color.Red;
                lbl_message.Text = "Variety failed to be Added / Updated";
            }
            populate_datagridview1();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult DLR_Message = new DialogResult();
            int int_result = 0;

            //Delete
            if (e.ColumnIndex == 8)
            {
                string str_msg;

                str_msg = "You are about to delete " + Environment.NewLine;
                str_msg = str_msg + "Variety: " + dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString() +" "+ dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString() + Environment.NewLine;
                str_msg = str_msg + "Do you want to continue?";
                DLR_Message = MessageBox.Show(str_msg, "Common - Grower Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (DLR_Message == DialogResult.Yes)
                {
                    int_result = FruPak.PF.Data.AccessLayer.CM_Block_Variety_Relationship.Delete(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                }

                if (int_result > 0)
                {
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    lbl_message.Text = "Variety has been Deleted";
                }
                else
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = "Variety failed to Delete";
                }
            }
            populate_datagridview1();
        }
        private void btn_reset_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void Reset()
        {
            btn_Add.Text = "&Add";
        }
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }
        private void Enter_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13)
            {
                Add_btn();
            }
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
        private void Grower_Block_Variety_KeyDown(object sender, KeyEventArgs e)
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
