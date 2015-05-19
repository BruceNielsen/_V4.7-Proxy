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
    /*Description
    -----------------
    Fruit Variety Class.
     * 
     * This class is a form used to Add, Update, and Delete Fruit Varieties, which are also linked to a fruit type

    Date        Author     Desc
    -------------------------------------------------------------------------------------------------------------------------------------------------
    01/09/2013  Dave       Creation
    */
    public partial class Fruit_Variety : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();     

        private static bool bol_Write_access;
        private static int int_Current_User_Id = 0;
        private static int int_variety_Id = 0;

        public Fruit_Variety(int int_C_User_Id, bool bol_w_a)
        {
            InitializeComponent();
            int_Current_User_Id = int_C_User_Id;
            // restrict write access
            bol_Write_access = bol_w_a;
            //check if testing or not

            //if (FruPak.PF.Global.Global.bol_Testing == true)
            //{
            //    this.Text = "FruPak Process Factory - " + this.Text + " - Test Environment";
            //}
            //else
            //{
            //    this.Text = "FruPak Process Factory";
            //}

            btn_Add.Enabled = bol_w_a;

            AddColumnsProgrammatically();
            populate_datagridview();
            populdate_combobox();

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
            var col1 = new DataGridViewTextBoxColumn();
            var col2 = new DataGridViewTextBoxColumn();
            var col3 = new DataGridViewTextBoxColumn();
            var col4 = new DataGridViewTextBoxColumn();
            var col5 = new DataGridViewTextBoxColumn();
            var col6 = new DataGridViewTextBoxColumn();

            col1.HeaderText = "Variety_Id";
            col1.Name = "Variety_Id";
            col1.ReadOnly = true;
            col1.Visible = false;

            col2.HeaderText = "Fruit";
            col2.Name = "FruitType";
            col2.ReadOnly = true;

            col3.HeaderText = "Variety";
            col3.Name = "Variety";
            col3.ReadOnly = true;

            col4.HeaderText = "Description";
            col4.Name = "Description";
            col4.Width = 250;
            col4.ReadOnly = true;

            col5.HeaderText = "Mod_Date";
            col5.Name = "Mod_Date";
            col5.ReadOnly = true;
            col5.Visible = false;

            col6.HeaderText = "Mod_User_id";
            col6.Name = "Mod_User_id";
            col6.ReadOnly = true;
            col6.Visible = false;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col1, col2, col3, col4, col5, col6 });

            DataGridViewImageColumn img_delete = new DataGridViewImageColumn();
            dataGridView1.Columns.Add(img_delete);
            img_delete.HeaderText = "Delete";
            img_delete.Name = "Delete";
            img_delete.Image = FruPak.PF.Global.Properties.Resources.delete;
            img_delete.ReadOnly = true;
            img_delete.Visible = bol_Write_access;

            DataGridViewImageColumn img_edit = new DataGridViewImageColumn();
            dataGridView1.Columns.Add(img_edit);
            img_edit.HeaderText = "Edit";
            img_edit.Name = "Edit";
            img_edit.Image = FruPak.PF.Global.Properties.Resources.edit;
            img_edit.ReadOnly = true;

        }
        private void populate_datagridview()
        {
            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();

            DataSet ds_Get_Menu_Info = FruPak.PF.Data.AccessLayer.CM_Fruit_Variety.Get_Info();
            DataRow dr_Get_Menu_Info;
            for (int i = 0; i < Convert.ToInt32(ds_Get_Menu_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Menu_Info = ds_Get_Menu_Info.Tables[0].Rows[i];

                dataGridView1.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_User_Id = new DataGridViewTextBoxCell();
                DGVC_User_Id.Value = dr_Get_Menu_Info["Variety_Id"].ToString();
                dataGridView1.Rows[i].Cells[0] = DGVC_User_Id;

                DataGridViewCell DGVC_Menu = new DataGridViewTextBoxCell();
                DGVC_Menu.Value = dr_Get_Menu_Info["Fruit"].ToString();
                dataGridView1.Rows[i].Cells[1] = DGVC_Menu;

                DataGridViewCell DGVC_Name = new DataGridViewTextBoxCell();
                DGVC_Name.Value = dr_Get_Menu_Info["Variety"].ToString();
                dataGridView1.Rows[i].Cells[2] = DGVC_Name;

                DataGridViewCell DGVC_Description = new DataGridViewTextBoxCell();
                DGVC_Description.Value = dr_Get_Menu_Info["Description"].ToString();
                dataGridView1.Rows[i].Cells[3] = DGVC_Description;

                DataGridViewCell DGVC_Mod_date = new DataGridViewTextBoxCell();
                DGVC_Mod_date.Value = dr_Get_Menu_Info["Mod_date"].ToString();
                dataGridView1.Rows[i].Cells[4] = DGVC_Mod_date;

                DataGridViewCell DGVC_Mod_User_Id = new DataGridViewTextBoxCell();
                DGVC_Mod_User_Id.Value = dr_Get_Menu_Info["Mod_User_Id"].ToString();
                dataGridView1.Rows[i].Cells[5] = DGVC_Mod_User_Id;

                //dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dataGridView1.AutoResizeColumn(0, DataGridViewAutoSizeColumnMode.AllCells);
                dataGridView1.AutoResizeColumn(1, DataGridViewAutoSizeColumnMode.AllCells);
                dataGridView1.AutoResizeColumn(2, DataGridViewAutoSizeColumnMode.AllCells);
                dataGridView1.AutoResizeColumn(6, DataGridViewAutoSizeColumnMode.AllCells);
                dataGridView1.AutoResizeColumn(7, DataGridViewAutoSizeColumnMode.AllCells);

                ds_Get_Menu_Info.Dispose();
            }
        }
        private void SizeAllColumns(Object sender, EventArgs e)
        {
            //dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataGridView1.AutoResizeColumn(0, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(1, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(2, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(6, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(7, DataGridViewAutoSizeColumnMode.AllCells);


        }
        private void populdate_combobox()
        {
            DataSet ds_get_info = FruPak.PF.Data.AccessLayer.CM_Fruit_Type.Get_Info();

            cmb_Fruit.DataSource = ds_get_info.Tables[0];
            cmb_Fruit.DisplayMember = "Code";
            cmb_Fruit.ValueMember = "FruitType_Id";

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult DLR_Message = new DialogResult();
            int int_result = 0;

            if (e.ColumnIndex == 6)
            {
                string str_msg;

                str_msg = "You are about to delete " + Environment.NewLine;
                str_msg = str_msg + "Variety " + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + Environment.NewLine;
                str_msg = str_msg + "Do you want to continue?";
                DLR_Message = MessageBox.Show(str_msg, "Common - Variety Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (DLR_Message == DialogResult.Yes)
                {
                    int_result = FruPak.PF.Data.AccessLayer.CM_Fruit_Variety.Delete(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                }

                if (int_result > 0)
                {
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    lbl_message.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + " has been deleted";
                }
                else
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + " failed to delete";
                }
                populate_datagridview();
            }
            else if (e.ColumnIndex == 7)
            {
                int_variety_Id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());

                cmb_Fruit.SelectedIndex = cmb_Fruit.FindString(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                txt_code.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txt_Description.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                btn_Add.Text = "Update";
            }

        }
        private void btn_Add_Click(object sender, EventArgs e)
        {
            DialogResult DLR_MessageBox = new DialogResult();
            string str_msg = "";
            int int_result = 0;

            if (txt_code.TextLength == 0)
            {
                str_msg = str_msg + "Invalid Variety: Please enter a Variety" + Environment.NewLine;
            }
            else if ((sender as Button).Text == "Add")
            {
                DataSet ds_Get_Menu_Info = FruPak.PF.Data.AccessLayer.CM_Fruit_Variety.Get_Info(txt_code.Text);
                if (Convert.ToInt32(ds_Get_Menu_Info.Tables[0].Rows.Count.ToString()) > 0)
                {
                    str_msg = str_msg + "Invalid Variety: This Variety already exists. Please choose a different Variety Name" + Environment.NewLine;
                }
            }
            if (txt_Description.TextLength == 0)
            {
                str_msg = str_msg + "Invalid Description: Please enter a Description" + Environment.NewLine;
            }

            if (str_msg.Length > 0)
            {
                DLR_MessageBox = MessageBox.Show(str_msg, "Common - Fruit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (DLR_MessageBox != DialogResult.OK)
            {
                switch ((sender as Button).Text)
                {
                    case "Add":
                        int_result = FruPak.PF.Data.AccessLayer.CM_Fruit_Variety.Insert(FruPak.PF.Common.Code.General.int_max_user_id("CM_Fruit_Variety"), Convert.ToInt32(cmb_Fruit.SelectedValue.ToString()), txt_code.Text, txt_Description.Text, int_Current_User_Id);
                        break;
                    case "Update":
                        int_result = FruPak.PF.Data.AccessLayer.CM_Fruit_Variety.Update(int_variety_Id, Convert.ToInt32(cmb_Fruit.SelectedValue.ToString()), txt_code.Text, txt_Description.Text, int_Current_User_Id);
                        break;
                }
            }

            if (int_result > 0)
            {
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                lbl_message.Text = txt_code.Text + " has been saved";
                Reset();
            }
            else
            {
                lbl_message.ForeColor = System.Drawing.Color.Red;
                lbl_message.Text = txt_code.Text + " has NOT been saved";
            }

            populate_datagridview();
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            txt_code.ResetText();
            txt_Description.ResetText();
            btn_Add.Text = "Add";
        }
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
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
        private void Fruit_Variety_KeyDown(object sender, KeyEventArgs e)
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
