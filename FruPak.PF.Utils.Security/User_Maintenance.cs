using NLog;
using System;
using System.Data;
using System.Windows.Forms;

namespace PF.Utils.Security
{
    /*Description
    -----------------
    User Maintenance Class.
     * This class is a form used to Add, Update, and Delete users

    Date        Author     Desc
    -------------------------------------------------------------------------------------------------------------------------------------------------
    01/09/2013  Dave       Creation
    */

    public partial class User_Maintenance : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static int int_Current_User_Id = 0;
        private static bool bol_Write_access;

        public User_Maintenance(int int_C_User_Id, bool bol_w_a)
        {
            int_Current_User_Id = int_C_User_Id;

            InitializeComponent();

            // restrict write access
            bol_Write_access = bol_w_a;
            btn_Update.Enabled = bol_w_a;
            btn_Add_Members.Enabled = bol_w_a;

            AddColumnsProgrammatically();
            populate_datagridview();
            populate_check_boxList();

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

                //else if (c.GetType() == typeof(PF.Utils.UserControls.Customer))
                //{
                //    PF.Utils.UserControls.Customer cust = (PF.Utils.UserControls.Customer)c;
                //    cust.CustomerChanged += new EventHandler(this.CustomerControl_CustomerChanged);
                //}
            }

            #endregion Log any interesting events from the UI to the CSV log file
        }

        private void AddColumnsProgrammatically()
        {
            var col1 = new DataGridViewTextBoxColumn();
            var col2 = new DataGridViewTextBoxColumn();
            var col3 = new DataGridViewTextBoxColumn();
            var col4 = new DataGridViewTextBoxColumn();
            var col5 = new DataGridViewTextBoxColumn();
            var col6 = new DataGridViewTextBoxColumn();
            var col7 = new DataGridViewTextBoxColumn();
            var col8 = new DataGridViewTextBoxColumn();
            var col9 = new DataGridViewTextBoxColumn();

            col1.HeaderText = "User_Id";
            col1.Name = "User_Id";
            col1.ReadOnly = true;
            col1.Visible = false;

            col2.HeaderText = "UserId";
            col2.Name = "Logon";
            col2.ReadOnly = true;

            col3.HeaderText = "First Name";
            col3.Name = "First_Name";
            col3.ReadOnly = true;

            col4.HeaderText = "Last Name";
            col4.Name = "Last_Name";
            col4.ReadOnly = true;

            col5.HeaderText = "Password";
            col5.Name = "Password";
            col5.ReadOnly = true;
            col5.Visible = false;

            col6.HeaderText = "Active";
            col6.Name = "Active";
            col6.ReadOnly = true;

            col7.HeaderText = "Mod_Date";
            col7.Name = "Mod_Date";
            col7.ReadOnly = true;
            col7.Visible = false;

            col8.HeaderText = "Mod_User_id";
            col8.Name = "Mod_User_id";
            col8.ReadOnly = true;
            col8.Visible = false;

            col9.HeaderText = "Test";
            col9.Name = "Allow_Test";
            col9.ReadOnly = true;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col1, col2, col3, col4, col5, col6, col7, col8, col9 });

            DataGridViewImageColumn img_delete = new DataGridViewImageColumn();
            dataGridView1.Columns.Add(img_delete);
            img_delete.HeaderText = "Delete";
            img_delete.Name = "Delete";
            img_delete.Image = PF.Global.Properties.Resources.delete;
            img_delete.ReadOnly = true;
            img_delete.Visible = bol_Write_access;

            DataGridViewImageColumn img_edit = new DataGridViewImageColumn();
            dataGridView1.Columns.Add(img_edit);
            img_edit.HeaderText = "Edit";
            img_edit.Name = "Edit";
            img_edit.Image = PF.Global.Properties.Resources.edit;
            img_edit.ReadOnly = true;
        }

        private void populate_datagridview()
        {
            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();

            DataSet ds_Get_User_Info = PF.Data.AccessLayer.SC_User.Get_Info();
            DataRow dr_Get_User_Info;
            for (int i = 0; i < Convert.ToInt32(ds_Get_User_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_User_Info = ds_Get_User_Info.Tables[0].Rows[i];

                dataGridView1.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_User_Id = new DataGridViewTextBoxCell();
                DGVC_User_Id.Value = dr_Get_User_Info["User_Id"].ToString();
                dataGridView1.Rows[i].Cells["User_Id"] = DGVC_User_Id;

                DataGridViewCell DGVC_Logon = new DataGridViewTextBoxCell();
                DGVC_Logon.Value = dr_Get_User_Info["Logon"].ToString();
                dataGridView1.Rows[i].Cells[1] = DGVC_Logon;

                DataGridViewCell DGVC_First_Name = new DataGridViewTextBoxCell();
                DGVC_First_Name.Value = dr_Get_User_Info["First_Name"].ToString();
                dataGridView1.Rows[i].Cells[2] = DGVC_First_Name;

                DataGridViewCell DGVC_Last_Name = new DataGridViewTextBoxCell();
                DGVC_Last_Name.Value = dr_Get_User_Info["Last_Name"].ToString();
                dataGridView1.Rows[i].Cells[3] = DGVC_Last_Name;

                DataGridViewCell DGVC_Password = new DataGridViewTextBoxCell();
                DGVC_Password.Value = dr_Get_User_Info["Password"].ToString();
                dataGridView1.Rows[i].Cells[4] = DGVC_Password;

                DataGridViewCell DGVC_Active = new DataGridViewTextBoxCell();
                DGVC_Active.Value = dr_Get_User_Info["Active_Ind"].ToString();
                dataGridView1.Rows[i].Cells[5] = DGVC_Active;

                DataGridViewCell DGVC_Mod_date = new DataGridViewTextBoxCell();
                DGVC_Mod_date.Value = dr_Get_User_Info["Mod_date"].ToString();
                dataGridView1.Rows[i].Cells[6] = DGVC_Mod_date;

                DataGridViewCell DGVC_Mod_User_Id = new DataGridViewTextBoxCell();
                DGVC_Mod_User_Id.Value = dr_Get_User_Info["Mod_User_Id"].ToString();
                dataGridView1.Rows[i].Cells[7] = DGVC_Mod_User_Id;

                DataGridViewCell DGVC_Col9 = new DataGridViewTextBoxCell();
                DGVC_Col9.Value = dr_Get_User_Info["Allow_Test"].ToString();
                dataGridView1.Rows[i].Cells["Allow_Test"] = DGVC_Col9;

                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
        }

        /// <summary>
        /// Sizes all the columns to fit the loaded Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SizeAllColumns(Object sender, EventArgs e)
        {
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void populate_check_boxList()
        {
            DataSet ds_Get_User_Info = PF.Data.AccessLayer.SC_User_Groups.Get_Info();
            DataRow dr_Get_User_Info;

            for (int i = 0; i < Convert.ToInt32(ds_Get_User_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_User_Info = ds_Get_User_Info.Tables[0].Rows[i];
                checkedListBox1.Items.Add(dr_Get_User_Info["UserGroup_Id"].ToString() + " " + dr_Get_User_Info["Name"].ToString());
            }
        }

        public static string str_logon = "";
        public static int int_User_Id = 0;
        public static bool bol_edit = false;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int int_result = 0;
            DialogResult DLR_Message = new DialogResult();

            //deletes a user
            if (e.ColumnIndex == 9)
            {
                string str_msg;

                str_msg = "You are about to delete " + Environment.NewLine;
                str_msg = str_msg + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + " " + dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString() + Environment.NewLine;
                str_msg = str_msg + "Do you want to continue?";
                DLR_Message = MessageBox.Show(str_msg, "Security - User Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (DLR_Message == DialogResult.Yes)
                {
                    int_result = PF.Data.AccessLayer.SC_User_Group_Relationship.Delete_User(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["User_Id"].Value.ToString()));
                    int_result = PF.Data.AccessLayer.SC_User.Delete(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["User_Id"].Value.ToString()));
                }

                if (int_result > 0)
                {
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    lbl_message.Text = dataGridView1.Rows[e.RowIndex].Cells["First_Name"].Value.ToString() + " " + dataGridView1.Rows[e.RowIndex].Cells["Last_Name"].Value.ToString() + " has been deleted";
                }
                else
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = dataGridView1.Rows[e.RowIndex].Cells["First_Name"].Value.ToString() + " " + dataGridView1.Rows[e.RowIndex].Cells["Last_Name"].Value.ToString() + " failed to delete";
                }
                populate_datagridview();
            }
            else if (e.ColumnIndex == 10)
            {
                bol_edit = true;
                int_User_Id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["User_Id"].Value.ToString());
                txt_First_Name.Text = dataGridView1.Rows[e.RowIndex].Cells["First_Name"].Value.ToString();
                txt_Last_Name.Text = dataGridView1.Rows[e.RowIndex].Cells["Last_Name"].Value.ToString();
                txt_Logon.Text = dataGridView1.Rows[e.RowIndex].Cells["Logon"].Value.ToString();

                txt_Password.Text = dataGridView1.Rows[e.RowIndex].Cells["Password"].Value.ToString();

                str_logon = txt_Logon.Text;
                ckb_Active.Checked = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells["Active"].Value);
                try
                {
                    ckb_allow_test.Checked = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells["Allow_Test"].Value);
                }
                catch
                {
                    ckb_allow_test.Checked = false;
                }
                btn_Update.Text = "Update";

                update_checkList();
                checkedListBox1.Visible = true;
                btn_Add_Members.Visible = true;
            }
        }

        private void update_checkList()
        {
            DataSet ds_Get_Info = PF.Data.AccessLayer.SC_User_Group_Relationship.Get_Info_By_User(int_User_Id);
            DataRow dr_Get_Info;

            for (int i = 0; i < Convert.ToInt32(checkedListBox1.Items.Count.ToString()); i++)
            {
                string item = checkedListBox1.Items[i].ToString();

                checkedListBox1.SetItemCheckState(i, CheckState.Unchecked);
                for (int j = 0; j < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); j++)
                {
                    dr_Get_Info = ds_Get_Info.Tables[0].Rows[j];

                    if (Convert.ToInt32(item.Substring(0, item.IndexOf(' '))) == Convert.ToInt32(dr_Get_Info["UserGroup_Id"].ToString()))
                    {
                        checkedListBox1.SetItemCheckState(i, CheckState.Checked);
                    }
                }
            }
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            string str_msg = "";
            DialogResult DLR_MessageBox = new DialogResult();
            int int_update = 0;

            if ((sender as Button).Text == "&Add")
            {
                str_msg = General.User_Check(txt_First_Name.Text, txt_Last_Name.Text, txt_Logon.Text, true, txt_Password.Text);
                int_update = 1;
            }
            else if ((sender as Button).Text == "&Update")
            {
                if (txt_Password.TextLength == 0)
                {
                    if (str_logon == txt_Logon.Text)
                    {
                        str_msg = General.User_Check(txt_First_Name.Text, txt_Last_Name.Text);
                        int_update = 2;
                    }
                    else
                    {
                        str_msg = General.User_Check(txt_First_Name.Text, txt_Last_Name.Text, txt_Logon.Text, true);
                        int_update = 3;
                    }
                }
                else
                {
                    if (str_logon == txt_Logon.Text)
                    {
                        str_msg = General.User_Check(txt_First_Name.Text, txt_Last_Name.Text, txt_Logon.Text, false, txt_Password.Text);
                        int_update = 4;
                    }
                    else
                    {
                        str_msg = General.User_Check(txt_First_Name.Text, txt_Last_Name.Text, txt_Logon.Text, true, txt_Password.Text);
                        int_update = 5;
                    }
                }
            }

            if (str_msg.Length > 0)
            {
                DLR_MessageBox = MessageBox.Show(str_msg, "Security - User Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (DLR_MessageBox == DialogResult.None)
            {
                int int_result = 0;

                switch (int_update)
                {
                    case 1:
                        int_User_Id = PF.Common.Code.General.int_max_user_id("SC_User");
                        int_result = PF.Data.AccessLayer.SC_User.Insert(int_User_Id, txt_Logon.Text, txt_First_Name.Text, txt_Last_Name.Text, General.Password_Encryption(txt_Password.Text), ckb_Active.Checked, ckb_allow_test.Checked, int_Current_User_Id);
                        break;

                    case 2:
                        int_result = PF.Data.AccessLayer.SC_User.Update(int_User_Id, txt_First_Name.Text, txt_Last_Name.Text, ckb_Active.Checked, ckb_allow_test.Checked, int_Current_User_Id);
                        break;

                    case 3:
                        int_result = PF.Data.AccessLayer.SC_User.Update(int_User_Id, txt_First_Name.Text, txt_Last_Name.Text, txt_Logon.Text, ckb_Active.Checked, ckb_allow_test.Checked, int_Current_User_Id);
                        break;

                    case 4:
                        int_result = PF.Data.AccessLayer.SC_User.Update(int_User_Id, txt_First_Name.Text, txt_Last_Name.Text, ckb_Active.Checked, ckb_allow_test.Checked, int_Current_User_Id);
                        int_result = int_result + PF.Data.AccessLayer.SC_User.Update_User_Password(int_User_Id, General.Password_Encryption(txt_Password.Text), int_Current_User_Id);
                        break;

                    case 5:
                        int_result = PF.Data.AccessLayer.SC_User.Update(int_User_Id, txt_First_Name.Text, txt_Last_Name.Text, txt_Logon.Text, ckb_Active.Checked, ckb_allow_test.Checked, int_Current_User_Id);
                        int_result = int_result + PF.Data.AccessLayer.SC_User.Update_User_Password(int_User_Id, General.Password_Encryption(txt_Password.Text), int_Current_User_Id);
                        break;
                }

                switch ((sender as Button).Text)
                {
                    case "&Add":
                        PF.Common.Code.General.write_log(int_User_Id, "Usr - Add", int_Current_User_Id);
                        break;

                    case "&Update":
                        PF.Common.Code.General.write_log(int_User_Id, "Usr - Updated", int_Current_User_Id);
                        break;
                }

                if (int_result > 0)
                {
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    lbl_message.Text = txt_First_Name.Text + " " + txt_Last_Name.Text + " has been saved";
                    Reset();
                }
                else
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = txt_First_Name.Text + " " + txt_Last_Name.Text + " has NOT to has been saved";
                }
                populate_datagridview();
            }
        }

        private void ckb_Show_Password_Click(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked == true)
            {
                txt_Password.PasswordChar = '\0';
            }
            else
            {
                txt_Password.PasswordChar = '*';
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            txt_First_Name.ResetText();
            txt_Last_Name.ResetText();
            txt_Logon.ResetText();
            txt_Password.ResetText();
            ckb_Active.Checked = true;
            ckb_Show_Password.Checked = false;
            btn_Update.Text = "&Add";
            checkedListBox1.Visible = false;
            btn_Add_Members.Visible = false;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Uses the characters of the first name as the default logonID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_First_Name_TextChanged(object sender, EventArgs e)
        {
            if (bol_edit == false)
            {
                txt_Logon.Text = (sender as TextBox).Text;
            }
        }

        private string str_Last_Name_Char = "";

        private void txt_Last_Name_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).TextLength > 0)
            {
                str_Last_Name_Char = (sender as TextBox).Text.Substring(0, 1);
            }
            else
            {
                str_Last_Name_Char = "";
            }

            if (bol_edit == false)
            {
                txt_Logon.Text = txt_First_Name.Text + str_Last_Name_Char;
            }
        }

        private void btn_Add_Members_Click(object sender, EventArgs e)
        {
            PF.Data.AccessLayer.SC_User_Group_Relationship.Delete_User(int_User_Id);
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                int int_UserGroup_id = Convert.ToInt32(checkedListBox1.Items[i].ToString().Substring(0, checkedListBox1.Items[i].ToString().IndexOf(' ')));
                if (checkedListBox1.GetItemCheckState(i) == CheckState.Checked)
                {
                    PF.Data.AccessLayer.SC_User_Group_Relationship.Insert(PF.Common.Code.General.int_max_user_id("SC_User_Group_Relationship"), int_User_Id, int_UserGroup_id, int_Current_User_Id);
                }
                else
                {
                    PF.Data.AccessLayer.SC_User_Group_Relationship.Delete_User_From_Group(int_User_Id, int_UserGroup_id);
                }
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

        //private void CustomerControl_CustomerChanged(object sender, EventArgs e)
        //{
        //    PF.Utils.UserControls.Customer cust = (PF.Utils.UserControls.Customer)sender;
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

        #endregion Decorate String

        /// <summary>
        /// Close the form with the Esc key (Sel request 11-02-2015 BN)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void User_Maintenance_KeyDown(object sender, KeyEventArgs e)
        {
            // Note that the KeyPreview property must be set to true for this to work
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        #endregion Methods to log UI events to the CSV file. BN 29/01/2015
    }
}