using System;
using System.Data;
using System.Windows.Forms;

namespace FruPak.PF.Menu
{
    public partial class Fun_Messages : Form
    {
        public Fun_Messages()
        {
            InitializeComponent();

            AddColumnsProgrammatically();
            populate_datagridview();
            populate_combox();
        }

        private void populate_combox()
        {
            DataSet ds = FruPak.PF.Data.AccessLayer.SC_User.Get_For_Combo();

            DataRow toinsert = ds.Tables[0].NewRow();

            toinsert[0] = 0;
            toinsert[9] = "Everyone";

            ds.Tables[0].Rows.InsertAt(toinsert, 0);

            cmb_User.DataSource = ds.Tables[0];
            cmb_User.DisplayMember = "Full_Name";
            cmb_User.ValueMember = "User_Id";
            cmb_User.Text = null;
            ds.Dispose();

            cmb_Repeat_Type.Items.AddRange(new string[] { "One Off", "Annual", "Months", "Days" });
        }

        private void AddColumnsProgrammatically()
        {
            var col0 = new DataGridViewTextBoxColumn();
            var col1 = new DataGridViewTextBoxColumn();
            var col1a = new DataGridViewTextBoxColumn();
            var col2 = new DataGridViewTextBoxColumn();
            var col2a = new DataGridViewTextBoxColumn();
            var col3 = new DataGridViewTextBoxColumn();
            var col4 = new DataGridViewTextBoxColumn();

            col0.HeaderText = "Id";
            col0.Name = "Id";
            col0.ReadOnly = true;
            col0.Visible = false;

            col1.HeaderText = "User";
            col1.Name = "User_Id";
            col1.ReadOnly = true;
            col1.Visible = false;

            col1a.HeaderText = "Name";
            col1a.Name = "Name";
            col1a.ReadOnly = true;

            col2.HeaderText = "Message";
            col2.Name = "Message";
            //col2.Width = 300;
            col2.ReadOnly = true;

            col2a.HeaderText = "Countdown";
            col2a.Name = "countdown_ind";
            col2a.ReadOnly = true;
            col2a.Visible = false;

            col3.HeaderText = "End Date";
            col3.Name = "End_Date";
            col3.ReadOnly = true;

            col4.HeaderText = "Repeat";
            col4.Name = "Repeat_Ind";
            col4.ReadOnly = true;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col1a, col2, col2a, col3, col4 });

            DataGridViewImageColumn img_delete = new DataGridViewImageColumn();
            dataGridView1.Columns.Add(img_delete);
            img_delete.HeaderText = "Delete";
            img_delete.Name = "Delete";
            img_delete.Image = FruPak.PF.Global.Properties.Resources.delete;
            img_delete.ReadOnly = true;

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

            DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.SY_Message.Get_Info_Translated();
            DataRow dr_Get_Info;
            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                dataGridView1.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_Cell0 = new DataGridViewTextBoxCell();
                DGVC_Cell0.Value = dr_Get_Info["Message_Id"].ToString();
                dataGridView1.Rows[i].Cells["Id"] = DGVC_Cell0;

                DataGridViewCell DGVC_Cell1 = new DataGridViewTextBoxCell();
                DGVC_Cell1.Value = dr_Get_Info["User_Id"].ToString();
                dataGridView1.Rows[i].Cells["User_Id"] = DGVC_Cell1;

                DataGridViewCell DGVC_Cell1a = new DataGridViewTextBoxCell();
                DGVC_Cell1a.Value = dr_Get_Info["Name"].ToString();
                dataGridView1.Rows[i].Cells["Name"] = DGVC_Cell1a;

                DataGridViewCell DGVC_Cell2 = new DataGridViewTextBoxCell();
                DGVC_Cell2.Value = dr_Get_Info["Message"].ToString().Replace('%', ' ').Trim();
                dataGridView1.Rows[i].Cells["Message"] = DGVC_Cell2;

                DataGridViewCell DGVC_Cell2a = new DataGridViewTextBoxCell();
                DGVC_Cell2a.Value = dr_Get_Info["Message"].ToString().IndexOf('%') + "," + dr_Get_Info["Message"].ToString().Length;
                dataGridView1.Rows[i].Cells["countdown_ind"] = DGVC_Cell2a;

                DataGridViewCell DGVC_Cell3 = new DataGridViewTextBoxCell();
                DGVC_Cell3.Value = dr_Get_Info["End_Date"].ToString();
                dataGridView1.Rows[i].Cells["End_Date"] = DGVC_Cell3;

                DataGridViewCell DGVC_Cell4 = new DataGridViewTextBoxCell();
                DGVC_Cell4.Value = dr_Get_Info["Repeat_ind"].ToString();
                dataGridView1.Rows[i].Cells["Repeat_ind"] = DGVC_Cell4;

                Column_Size();
                ds_Get_Info.Dispose();
            }
        }

        private void Fun_Messages_Load(object sender, EventArgs e)
        {
            Column_Size();
        }

        private void Column_Size()
        {
            //dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataGridView1.AutoResizeColumn(0, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(1, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(2, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(3, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(4, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(5, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(6, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(7, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(8, DataGridViewAutoSizeColumnMode.AllCells);
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmb_Repeat_Type_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cmb_Repeat_Type_SelectionChange(cmb_Repeat_Type.SelectedItem.ToString().Substring(0, 1).ToUpper());
        }

        private void cmb_Repeat_Type_SelectionChange(string Repeat_type)
        {
            switch (Repeat_type)
            {
                case "O":
                case "A":
                    cmb_Repeat_interval.Items.Clear();
                    cmb_Repeat_interval.Visible = false;
                    break;

                case "M":
                    cmb_Repeat_interval.Items.Clear();
                    for (int i = 1; i < 13; i++)
                    {
                        cmb_Repeat_interval.Items.Add(i);
                    }
                    cmb_Repeat_interval.Visible = true;
                    break;

                case "D":
                    cmb_Repeat_interval.Items.Clear();
                    for (int i = 1; i < 32; i++)
                    {
                        cmb_Repeat_interval.Items.Add(i);
                    }
                    cmb_Repeat_interval.Visible = true;
                    break;
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            cmb_User.Text = null;
            cmb_Repeat_Type.Text = null;
            cmb_Repeat_interval.Visible = false;
            txt_Display_Message.ResetText();
            rdb_Message_End.Checked = false;
            rdb_Message_Excl.Checked = false;
            rdb_Message_Start.Checked = false;
            btn_Add.Text = "&Add";
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            string str_msg = "";
            DialogResult DLR_Message = new DialogResult();

            if (txt_Display_Message.TextLength == 0)
            {
                str_msg = str_msg + "Invalid Message. Please enter a valid message." + Environment.NewLine;
            }
            int int_User_Id = 0;
            if (cmb_User.SelectedIndex == -1)
            {
                int_User_Id = 0;
            }
            else
            {
                int_User_Id = Convert.ToInt32(cmb_User.SelectedValue.ToString());
            }

            string str_Display = "";
            if (rdb_Message_Start.Checked == true)
            {
                str_Display = "% " + txt_Display_Message.Text;
            }
            else if (rdb_Message_End.Checked == true)
            {
                str_Display = txt_Display_Message.Text + " %";
            }
            else
            {
                str_Display = txt_Display_Message.Text;
            }

            string str_End_Date = dtp_End_Date.Value.Year.ToString() + "/" + dtp_End_Date.Value.Month.ToString() + "/" + dtp_End_Date.Value.Day.ToString() + " " +
                                  dtp_End_Time.Value.Hour.ToString() + ":" + dtp_End_Time.Value.Minute.ToString() + ":" + dtp_End_Time.Value.Second.ToString();

            string str_repeat = "";

            if (cmb_Repeat_Type.SelectedIndex == -1)
            {
                str_msg = str_msg + "Invalid Repeat Interval. Please enter a Interval from dropdown box." + Environment.NewLine;
            }
            else
            {
                switch (cmb_Repeat_Type.SelectedItem.ToString().Substring(0, 1).ToUpper())
                {
                    case "A":
                        str_repeat = "A";
                        break;

                    case "M":
                        str_repeat = "M";
                        if (cmb_Repeat_interval.SelectedIndex == -1)
                        {
                            str_msg = str_msg = "Invalid Number of Months to repeat in. Please select a valid number from the dropdown list." + Environment.NewLine;
                        }
                        else
                        {
                            str_repeat = str_repeat + cmb_Repeat_interval.SelectedItem.ToString();
                        }
                        break;

                    case "D":
                        str_repeat = "D";
                        if (cmb_Repeat_interval.SelectedIndex == -1)
                        {
                            str_msg = str_msg = "Invalid Number of Days to repeat in. Please select a valid number from the dropdown list." + Environment.NewLine;
                        }
                        else
                        {
                            str_repeat = str_repeat + cmb_Repeat_interval.SelectedItem.ToString();
                        }
                        break;

                    default:
                        str_repeat = null;
                        break;
                }
            }
            if (str_msg.Length > 0)
            {
                DLR_Message = MessageBox.Show(str_msg, "Process Factory", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            int int_result = 0;
            if (DLR_Message != System.Windows.Forms.DialogResult.OK)
            {
                switch (btn_Add.Text)
                {
                    case "&Add":
                        int_result = int_result + FruPak.PF.Data.AccessLayer.SY_Message.Insert(FruPak.PF.Common.Code.General.int_max_user_id("SY_Message"), int_User_Id, str_Display, str_End_Date, str_repeat);
                        break;

                    case "&Update":
                        int_result = int_result + FruPak.PF.Data.AccessLayer.SY_Message.Update(int_Message_Id, int_User_Id, str_Display, str_End_Date, str_repeat);
                        break;
                }
            }
            if (int_result > 0)
            {
                lbl_message.Text = "Item has been added/Updated.";
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                populate_datagridview();
                Reset();
            }
        }

        private int int_Message_Id = 0;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult DLR_Message = new DialogResult();
            int int_result = 0;

            #region ---------- DELETE -------------

            if (e.ColumnIndex == 7)
            {
                string str_msg;

                str_msg = "You are about to delete " + Environment.NewLine;
                str_msg = str_msg + dataGridView1.Rows[e.RowIndex].Cells["Message"].Value.ToString() + Environment.NewLine;
                str_msg = str_msg + "Do you want to continue?";
                DLR_Message = MessageBox.Show(str_msg, "Process Factory - Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (DLR_Message == DialogResult.Yes)
                {
                    int_result = int_result + FruPak.PF.Data.AccessLayer.SY_Message.Delete(Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value.ToString()));
                }
                if (int_result > 0)
                {
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    lbl_message.Text = "Message has been deleted";
                }
                else
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = "Message failed to delete";
                }
                populate_datagridview();
            }

            #endregion ---------- DELETE -------------

            #region ---------- EDIT -------------

            if (e.ColumnIndex == 8)
            {
                int_Message_Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value.ToString());
                cmb_User.SelectedValue = Convert.ToInt32(dataGridView1.CurrentRow.Cells["User_Id"].Value.ToString());
                txt_Display_Message.Text = dataGridView1.CurrentRow.Cells["Message"].Value.ToString().Replace('%', ' ');
                dtp_End_Date.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["End_Date"].Value.ToString());
                dtp_End_Time.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["End_Date"].Value.ToString());
                try
                {
                    cmb_Repeat_Type_SelectionChange(dataGridView1.CurrentRow.Cells["Repeat_Ind"].Value.ToString().Substring(0, 1).ToUpper());
                    switch (dataGridView1.CurrentRow.Cells["Repeat_Ind"].Value.ToString().Substring(0, 1).ToUpper())
                    {
                        case "O":
                            cmb_Repeat_Type.SelectedItem = "One Off";
                            break;

                        case "A":
                            cmb_Repeat_Type.SelectedItem = "Annual";
                            break;

                        case "M":
                            cmb_Repeat_Type.SelectedItem = "Months";
                            cmb_Repeat_interval.SelectedItem = dataGridView1.CurrentRow.Cells["Repeat_Ind"].Value.ToString().Substring(1);
                            break;

                        case "D":
                            cmb_Repeat_Type.SelectedItem = "Days";
                            cmb_Repeat_interval.SelectedItem = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Repeat_Ind"].Value.ToString().Substring(1));
                            break;
                    }
                }
                catch
                {
                    cmb_Repeat_Type_SelectionChange("O");
                    cmb_Repeat_Type.SelectedItem = "One Off";
                }
                try
                {
                    string[] DataParts = dataGridView1.CurrentRow.Cells["countdown_ind"].Value.ToString().Split(',');

                    if (Convert.ToInt32(DataParts[0]) == 0)
                    {
                        rdb_Message_Start.Checked = true;
                    }
                    else if (Convert.ToInt32(DataParts[0]) == Convert.ToInt32(DataParts[1]) - 1)
                    {
                        rdb_Message_End.Checked = true;
                    }
                    else
                    {
                        rdb_Message_Excl.Checked = true;
                    }
                }
                catch
                {
                    rdb_Message_Excl.Checked = true;
                }
                btn_Add.Text = "&Update";
            }

            #endregion ---------- EDIT -------------
        }

        /// <summary>
        /// Close the form with the Esc key (Sel request 11-02-2015 BN)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Fun_Messages_KeyDown(object sender, KeyEventArgs e)
        {
            // Note that the KeyPreview property must be set to true for this to work
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}