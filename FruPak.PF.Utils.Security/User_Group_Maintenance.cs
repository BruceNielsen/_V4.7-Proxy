using NLog;
using System;
using System.Data;
using System.Windows.Forms;

namespace PF.Utils.Security
{
    /*Description
    -----------------
    User Group Class.
     *
     * This class is a form used to Add, Update, and Delete USer Groups

    Date        Author     Desc
    -------------------------------------------------------------------------------------------------------------------------------------------------
    01/09/2013  Dave       Creation
    */

    public partial class User_Group_Maintenance : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static int int_Current_User_Id = 0;
        private static bool bol_Write_access;

        public User_Group_Maintenance(int int_C_User_Id, bool bol_w_a)
        {
            int_Current_User_Id = int_C_User_Id;
            InitializeComponent();

            // restrict write access
            bol_Write_access = bol_w_a;
            btn_Add.Enabled = bol_w_a;
            btn_Update_Relationships.Enabled = bol_w_a;

            AddColumnsProgrammatically();
            populate_datagridview();
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

            col1.HeaderText = "User_Id";
            col1.Name = "User_Id";
            col1.ReadOnly = true;
            col1.Visible = false;

            col2.HeaderText = "Group Name";
            col2.Name = "Name";
            col2.ReadOnly = true;

            col3.HeaderText = "Description";
            col3.Name = "Description";
            col3.Width = 350;
            col3.ReadOnly = true;

            col4.HeaderText = "Mod_Date";
            col4.Name = "Mod_Date";
            col4.ReadOnly = true;
            col4.Visible = false;

            col5.HeaderText = "Mod_User_id";
            col5.Name = "Mod_User_id";
            col5.ReadOnly = true;
            col5.Visible = false;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col1, col2, col3, col4, col5 });

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

            DataSet ds_Get_UserGroup_Info = PF.Data.AccessLayer.SC_User_Groups.Get_Info();
            DataRow dr_Get_UserGroup_Info;
            for (int i = 0; i < Convert.ToInt32(ds_Get_UserGroup_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_UserGroup_Info = ds_Get_UserGroup_Info.Tables[0].Rows[i];

                dataGridView1.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_User_Id = new DataGridViewTextBoxCell();
                DGVC_User_Id.Value = dr_Get_UserGroup_Info["UserGroup_Id"].ToString();
                dataGridView1.Rows[i].Cells[0] = DGVC_User_Id;

                DataGridViewCell DGVC_Name = new DataGridViewTextBoxCell();
                DGVC_Name.Value = dr_Get_UserGroup_Info["Name"].ToString();
                dataGridView1.Rows[i].Cells[1] = DGVC_Name;

                DataGridViewCell DGVC_Description = new DataGridViewTextBoxCell();
                DGVC_Description.Value = dr_Get_UserGroup_Info["Description"].ToString();
                dataGridView1.Rows[i].Cells[2] = DGVC_Description;

                DataGridViewCell DGVC_Mod_date = new DataGridViewTextBoxCell();
                DGVC_Mod_date.Value = dr_Get_UserGroup_Info["Mod_date"].ToString();
                dataGridView1.Rows[i].Cells[3] = DGVC_Mod_date;

                DataGridViewCell DGVC_Mod_User_Id = new DataGridViewTextBoxCell();
                DGVC_Mod_User_Id.Value = dr_Get_UserGroup_Info["Mod_User_Id"].ToString();
                dataGridView1.Rows[i].Cells[4] = DGVC_Mod_User_Id;

                //dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dataGridView1.AutoResizeColumn(0, DataGridViewAutoSizeColumnMode.AllCells);
                dataGridView1.AutoResizeColumn(1, DataGridViewAutoSizeColumnMode.AllCells);
                dataGridView1.AutoResizeColumn(5, DataGridViewAutoSizeColumnMode.AllCells);
                dataGridView1.AutoResizeColumn(6, DataGridViewAutoSizeColumnMode.AllCells);

                ds_Get_UserGroup_Info.Dispose();
            }
        }

        private void SizeAllColumns(Object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'moneyDataSet.List_TS_Work' table. You can move, or remove it, as needed.

            //dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataGridView1.AutoResizeColumn(0, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(1, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(5, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(6, DataGridViewAutoSizeColumnMode.AllCells);
        }

        private void populate_check_boxList()
        {
            DataSet ds_Get_User_Info = PF.Data.AccessLayer.SC_User.Get_Info();
            DataRow dr_Get_User_Info;

            for (int i = 0; i < Convert.ToInt32(ds_Get_User_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_User_Info = ds_Get_User_Info.Tables[0].Rows[i];
                checkedListBox1.Items.Add(dr_Get_User_Info["User_Id"].ToString() + " " + dr_Get_User_Info["First_Name"].ToString() + " " + dr_Get_User_Info["Last_Name"].ToString());
            }
            ds_Get_User_Info.Dispose();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            DialogResult DLR_MessageBox = new DialogResult();
            string str_msg = "";
            int int_result = 0;
            if (txt_Group_Name.TextLength == 0)
            {
                str_msg = str_msg + "Invalid Group Name: Please enter a Group Name" + Environment.NewLine;
            }
            else if ((sender as Button).Text == "&Add")
            {
                DataSet ds_Get_UserGroup_Info = PF.Data.AccessLayer.SC_User_Groups.Get_Info(txt_Group_Name.Text);
                if (Convert.ToInt32(ds_Get_UserGroup_Info.Tables[0].Rows.Count.ToString()) > 0)
                {
                    str_msg = str_msg + "Invalid Group: A Group by this name already exists. Please choose a different Group Name" + Environment.NewLine;
                }
            }
            if (txt_Group_Description.TextLength == 0)
            {
                str_msg = str_msg + "Invalid Description: Please enter a Description" + Environment.NewLine;
            }

            if (str_msg.Length > 0)
            {
                DLR_MessageBox = MessageBox.Show(str_msg, "Security - User Groups", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (DLR_MessageBox != DialogResult.OK)
            {
                switch ((sender as Button).Text)
                {
                    case "&Add":
                        int_result = PF.Data.AccessLayer.SC_User_Groups.Insert(PF.Common.Code.General.int_max_user_id("SC_User_Groups"), txt_Group_Name.Text, txt_Group_Description.Text, int_Current_User_Id);
                        break;

                    case "&Update":
                        int_result = PF.Data.AccessLayer.SC_User_Groups.Update(int_UserGroup_id, txt_Group_Name.Text, txt_Group_Description.Text, int_Current_User_Id);
                        break;
                }
            }

            if (int_result > 0)
            {
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                lbl_message.Text = txt_Group_Name.Text + " has been saved";
                Reset();
            }
            else
            {
                lbl_message.ForeColor = System.Drawing.Color.Red;
                lbl_message.Text = txt_Group_Name.Text + " has NOT to has been saved";
            }

            populate_datagridview();
        }

        private void Reset()
        {
            txt_Group_Name.ResetText();
            txt_Group_Description.ResetText();
            btn_Add.Text = "&Add";
            btn_Add.Visible = true;
            btn_Update_Relationships.Visible = false;
            checkedListBox1.Visible = false;
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private static int int_UserGroup_id = 0;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult DLR_Message = new DialogResult();
            int int_result = 0;

            if (e.ColumnIndex == 5)
            {
                string str_msg;

                str_msg = "You are about to delete " + Environment.NewLine;
                str_msg = str_msg + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + Environment.NewLine;
                str_msg = str_msg + "Do you want to continue?";
                DLR_Message = MessageBox.Show(str_msg, "Security - User Group Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (DLR_Message == DialogResult.Yes)
                {
                    int_result = PF.Data.AccessLayer.SC_User_Group_Relationship.Delete_User_From_Group(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                    int_result = PF.Data.AccessLayer.SC_Menu_Group_Relationship.Delete_UserGroup_From_Menu(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                    int_result = PF.Data.AccessLayer.SC_User_Groups.Delete(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                }

                if (int_result > 0)
                {
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    lbl_message.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + " has been deleted";
                }
                else
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + " failed to delete";
                }
                populate_datagridview();
            }
            else if (e.ColumnIndex == 6)
            {
                int_UserGroup_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                txt_Group_Name.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txt_Group_Description.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                btn_Add.Text = "&Update";
                update_checkList();
                btn_Update_Relationships.Visible = true;
                checkedListBox1.Visible = true;
            }
        }

        private void update_checkList()
        {
            DataSet ds_Get_Info = PF.Data.AccessLayer.SC_User_Group_Relationship.Get_Info_By_Group(int_UserGroup_id);
            DataRow dr_Get_Info;

            for (int i = 0; i < Convert.ToInt32(checkedListBox1.Items.Count.ToString()); i++)
            {
                string item = checkedListBox1.Items[i].ToString();
                int int_User_Id = Convert.ToInt32(item.Substring(0, item.IndexOf(' ')));
                checkedListBox1.SetItemCheckState(i, CheckState.Unchecked);
                for (int j = 0; j < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); j++)
                {
                    dr_Get_Info = ds_Get_Info.Tables[0].Rows[j];

                    if (Convert.ToInt32(item.Substring(0, item.IndexOf(' '))) == Convert.ToInt32(dr_Get_Info["User_Id"].ToString()))
                    {
                        checkedListBox1.SetItemCheckState(i, CheckState.Checked);
                    }
                }
            }
            ds_Get_Info.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PF.Data.AccessLayer.SC_User_Group_Relationship.Delete_User_From_Group(int_UserGroup_id);
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                int int_User_Id = Convert.ToInt32(checkedListBox1.Items[i].ToString().Substring(0, checkedListBox1.Items[i].ToString().IndexOf(' ')));
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

        private void checkedListBox1_Click(object sender, EventArgs e)
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
        private void User_Group_Maintenance_KeyDown(object sender, KeyEventArgs e)
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