using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;

namespace FruPak.PF.Utils.Security
{
    /*Description
    -----------------
    Menu Panel Class.
        *
        * This class is a form used to Add, Update, and Delete Menu Panel access.
        * A Menu panel is a control group on a Menu Tab
        * The actual Menu Panel still needs to be coded on to the Tab.

    Date        Author     Desc
    -------------------------------------------------------------------------------------------------------------------------------------------------
    01/09/2013  Dave       Creation
    */
    public partial class Menu_Panel_Maintenance : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static int int_SubMenu_id = 0;
        private static int int_Current_User_Id = 0;
        private static bool bol_Write_access;

        public Menu_Panel_Maintenance(int int_C_User_Id, bool bol_w_a)
        {
            int_Current_User_Id = int_C_User_Id;
            InitializeComponent();

            // restrict write access
            bol_Write_access = bol_w_a;
            btn_Add.Enabled = bol_w_a;
            btn_Update_Relationships.Enabled = bol_w_a;

            AddColumnsProgrammatically();
            populate_datagridview();
            populate_datagridview2();
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

                //else if (c.GetType() == typeof(FruPak.PF.Utils.UserControls.Customer))
                //{
                //    FruPak.PF.Utils.UserControls.Customer cust = (FruPak.PF.Utils.UserControls.Customer)c;
                //    cust.CustomerChanged += new EventHandler(this.CustomerControl_CustomerChanged);
                //}
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

            col1.HeaderText = "Menu_Id";
            col1.Name = "Menu_Id";
            col1.ReadOnly = true;
            col1.Visible = false;

            col2.HeaderText = "Menu Name";
            col2.Name = "MName";
            col2.ReadOnly = true;

            col3.HeaderText = "SubMenu Name";
            col3.Name = "SubMName";
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


            var col1a = new DataGridViewTextBoxColumn();
            col1a.HeaderText = "User Group Name";
            col1a.Name = "UserGroupName";
            col1a.ReadOnly = true;

            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { col1a });

            DataGridViewCheckBoxColumn chb_1 = new DataGridViewCheckBoxColumn();
            dataGridView2.Columns.Add(chb_1);
            chb_1.HeaderText = "Select";
            chb_1.Name = "chb_UGN";

            DataGridViewCheckBoxColumn chb_2 = new DataGridViewCheckBoxColumn();
            dataGridView2.Columns.Add(chb_2);
            chb_2.HeaderText = "Write Access";
            chb_2.Name = "chb_WA";

        }
        private void populate_datagridview()
        {
            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();

            DataSet ds_Get_Menu_Info = FruPak.PF.Data.AccessLayer.SC_Menu_Panel.Get_Info();
            DataRow dr_Get_Menu_Info;
            for (int i = 0; i < Convert.ToInt32(ds_Get_Menu_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Menu_Info = ds_Get_Menu_Info.Tables[0].Rows[i];

                dataGridView1.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_User_Id = new DataGridViewTextBoxCell();
                DGVC_User_Id.Value = dr_Get_Menu_Info["MenuPan_Id"].ToString();
                dataGridView1.Rows[i].Cells[0] = DGVC_User_Id;

                DataGridViewCell DGVC_Menu = new DataGridViewTextBoxCell();
                DGVC_Menu.Value = dr_Get_Menu_Info["Menu"].ToString();
                dataGridView1.Rows[i].Cells[1] = DGVC_Menu;

                DataGridViewCell DGVC_Name = new DataGridViewTextBoxCell();
                DGVC_Name.Value = dr_Get_Menu_Info["Name"].ToString();
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
        private void populate_datagridview2()
        {

            DataSet ds_Get_UserGroup_Info = FruPak.PF.Data.AccessLayer.SC_User_Groups.Get_Info();
            DataRow dr_Get_UserGroup_Info;

            for (int i = 0; i < Convert.ToInt32(ds_Get_UserGroup_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_UserGroup_Info = ds_Get_UserGroup_Info.Tables[0].Rows[i];
                dataGridView2.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_User_Id = new DataGridViewTextBoxCell();
                DGVC_User_Id.Value = dr_Get_UserGroup_Info["UserGroup_Id"].ToString() + " " + dr_Get_UserGroup_Info["Name"].ToString();
                dataGridView2.Rows[i].Cells[0] = DGVC_User_Id;
            }
            dataGridView2.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            ds_Get_UserGroup_Info.Dispose();
        }
        private void SizeAllColumns(Object sender, EventArgs e)
        {
            //dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataGridView1.AutoResizeColumn(0, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(1, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(2, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(6, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(7, DataGridViewAutoSizeColumnMode.AllCells);

            dataGridView2.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }
        private void populdate_combobox()
        {
            DataSet ds_get_info = FruPak.PF.Data.AccessLayer.SC_Menu.Get_Info();

            cmb_Menu.DataSource = ds_get_info.Tables[0];
            cmb_Menu.DisplayMember = "Name";
            cmb_Menu.ValueMember = "Menu_Id";

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult DLR_Message = new DialogResult();
            int int_result = 0;

            if (e.ColumnIndex == 6)
            {
                string str_msg;

                str_msg = "You are about to delete " + Environment.NewLine;
                str_msg = str_msg + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + Environment.NewLine + " from the " + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + " Menu Tab " + Environment.NewLine;
                str_msg = str_msg + "Do you want to continue?";
                DLR_Message = MessageBox.Show(str_msg, "Security - User Group Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (DLR_Message == DialogResult.Yes)
                {
                    int_result = FruPak.PF.Data.AccessLayer.SC_Menu_Panel_Group_Relationship.Delete(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                    int_result = FruPak.PF.Data.AccessLayer.SC_Menu_Panel.Delete(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                }

                if (int_result > 0)
                {
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    lbl_message.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + " has been deleted from the " +dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + " Menu Tab ";
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
                int_SubMenu_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());

                cmb_Menu.SelectedIndex = cmb_Menu.FindString(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                txt_SubMenu_Name.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txt_Menu_Description.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                btn_Add.Text = "&Update";
                dataGridView2.Visible = true;
                update_checkList();
                btn_Add.Visible = true;
                btn_Update_Relationships.Visible = true;

            }

        }

        private void update_checkList()
        {
            DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.SC_Menu_Panel_Group_Relationship.Get_Info(int_SubMenu_id);
            DataRow dr_Get_Info;

            for (int i = 0; i < Convert.ToInt32(dataGridView2.RowCount.ToString()); i++)
            {
                dataGridView2.Rows[i].Cells[1].Value = false;
                dataGridView2.Rows[i].Cells[2].Value = false;

                for (int j = 0; j < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); j++)
                {
                    dr_Get_Info = ds_Get_Info.Tables[0].Rows[j];

                    if (Convert.ToInt32(dataGridView2.Rows[i].Cells[0].Value.ToString().Substring(0, dataGridView2.Rows[i].Cells[0].Value.ToString().IndexOf(' '))) == Convert.ToInt32(dr_Get_Info["UserGroup_Id"].ToString()))
                    {
                        dataGridView2.Rows[i].Cells[1].Value = true;
                        dataGridView2.Rows[i].Cells[2].Value = Convert.ToBoolean(dr_Get_Info["Write_Access"].ToString());
                    }
                }
            }

            ds_Get_Info.Dispose();
        }
        private void btn_Add_Click(object sender, EventArgs e)
        {
            DialogResult DLR_MessageBox = new DialogResult();
            string str_msg = "";
            int int_result = 0;

            if (txt_SubMenu_Name.TextLength == 0)
            {
                str_msg = str_msg + "Invalid SubMenu Name: Please enter a SubMenu Name" + Environment.NewLine;
            }
            else if ((sender as Button).Text == "&Add")
            {
                DataSet ds_Get_Menu_Info = FruPak.PF.Data.AccessLayer.SC_Menu_Panel.Get_Info(Convert.ToInt32(cmb_Menu.SelectedValue.ToString()),txt_SubMenu_Name.Text);
                if (Convert.ToInt32(ds_Get_Menu_Info.Tables[0].Rows.Count.ToString()) > 0)
                {
                    str_msg = str_msg + "Invalid SubMenu: A SubMenu by this name already exists for this Main Menu Item. Please choose a different SubMenu Name or a diffent Main Menu" + Environment.NewLine;
                }
            }
            if (txt_Menu_Description.TextLength == 0)
            {
                str_msg = str_msg + "Invalid Description: Please enter a Description" + Environment.NewLine;
            }

            if (str_msg.Length > 0)
            {
                DLR_MessageBox = MessageBox.Show(str_msg, "Security - SubMenu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (DLR_MessageBox != DialogResult.OK)
            {
                switch ((sender as Button).Text)
                {
                    case "&Add":
                        int_result = FruPak.PF.Data.AccessLayer.SC_Menu_Panel.Insert(FruPak.PF.Common.Code.General.int_max_user_id("SC_Menu_Panel"), Convert.ToInt32(cmb_Menu.SelectedValue.ToString()), txt_SubMenu_Name.Text, txt_Menu_Description.Text, int_Current_User_Id);
                        break;
                    case "&Update":
                        int_result = FruPak.PF.Data.AccessLayer.SC_Menu_Panel.Update(int_SubMenu_id, Convert.ToInt32(cmb_Menu.SelectedValue.ToString()), txt_SubMenu_Name.Text, txt_Menu_Description.Text, int_Current_User_Id);
                        break;
                }
            }

            if (int_result > 0)
            {
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                lbl_message.Text = txt_SubMenu_Name.Text + " has been saved";
                Reset();
            }
            else
            {
                lbl_message.ForeColor = System.Drawing.Color.Red;
                lbl_message.Text = txt_SubMenu_Name.Text + " has NOT been saved";
            }

            populate_datagridview();
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            Reset();
        }


        private void Reset()
        {
            txt_SubMenu_Name.ResetText();
            txt_Menu_Description.ResetText();
            dataGridView2.Visible = false;
            btn_Add.Visible = true;
            btn_Update_Relationships.Visible = false;
            btn_Add.Text = "&Add";

        }
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Add_Members_Click(object sender, EventArgs e)
        {
            FruPak.PF.Data.AccessLayer.SC_Menu_Panel_Group_Relationship.Delete(int_SubMenu_id);

            for (int i = 0; i < Convert.ToInt32(dataGridView2.RowCount.ToString()); i++)
            {
                bool bol_selected = false;
                bool bol_write_access = false;

                try
                {
                    bol_selected = Convert.ToBoolean(dataGridView2.Rows[i].Cells[1].Value.ToString());
                }
                catch (Exception ex)
                {
                    logger.Log(LogLevel.Debug, ex.Message);
                }
                try
                {
                    bol_write_access = Convert.ToBoolean(dataGridView2.Rows[i].Cells[2].Value.ToString());
                }
                catch (Exception ex)
                {
                    logger.Log(LogLevel.Debug, ex.Message);
                }

                int int_UserGroup_id = Convert.ToInt32(dataGridView2.Rows[i].Cells[0].Value.ToString().Substring(0, dataGridView2.Rows[i].Cells[0].Value.ToString().IndexOf(' ')));

                if (bol_selected == true)
                {
                    FruPak.PF.Data.AccessLayer.SC_Menu_Panel_Group_Relationship.Insert(FruPak.PF.Common.Code.General.int_max_user_id("SC_Menu_Panel_Group_Relationship"), int_UserGroup_id, int_SubMenu_id, bol_write_access, int_Current_User_Id);
                }
            }
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            btn_Add.Visible = false;
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
        //private void CustomerControl_CustomerChanged(object sender, EventArgs e)
        //{
        //    FruPak.PF.Utils.UserControls.Customer cust = (FruPak.PF.Utils.UserControls.Customer)sender;
        //    logger.Log(LogLevel.Info, DecorateString(cust.Name, cust.Customer_Name, "TextChanged"));
        //}

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
        private void Menu_Panel_Maintenance_KeyDown(object sender, KeyEventArgs e)
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
