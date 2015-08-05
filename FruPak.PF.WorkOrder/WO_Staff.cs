using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;

namespace FruPak.PF.WorkOrder
{
    public partial class WO_Staff : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static bool bol_write_access;
        private static int int_Current_User_Id = 0;
        private static int int_Work_Order_Id = 0;
        private static int int_DVG_Row_id = 0;

        public WO_Staff(int int_wo_Id, int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();

            int_Current_User_Id = int_C_User_id;
            int_Work_Order_Id = int_wo_Id;
            //check if testing or not

            //if (FruPak.PF.Global.Global.bol_Testing == true)
            //{
            //    this.Text = "FruPak Process Factory - " + this.Text + " - Test Environment";
            //}
            //else
            //{
            //    this.Text = "FruPak Process Factory";
            //}
            //populate Work Order Display
            woDisplay1.Work_Order_Id = int_wo_Id;
            woDisplay1.populate();

            //restrict access
            bol_write_access = bol_w_a;
            btn_Add.Enabled = bol_w_a;

            AddColumnsProgrammatically();
            populate_combobox();
            populate_datagridview();

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
        private void populate_datagridview()
        {
            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();

            DataSet ds_Get_Info;
            DataRow dr_Get_Info;

            ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Work_Order_Staff_Relationship.Get_Info_Translated(int_Work_Order_Id);

            if (Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()) > 0)
            {
                for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
                {
                    int i_rows = Convert.ToInt32(dataGridView1.Rows.Count.ToString());
                    dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                    dataGridView1.Rows.Add(new DataGridViewRow());

                    DataGridViewCell DGVC_Id = new DataGridViewTextBoxCell();
                    DGVC_Id.Value = dr_Get_Info["WOStf_Relat_Id"].ToString();
                    dataGridView1.Rows[i_rows].Cells[0] = DGVC_Id;

                    DataGridViewCell DGVC_Staff_Id = new DataGridViewTextBoxCell();
                    DGVC_Staff_Id.Value = dr_Get_Info["Staff_Id"].ToString();
                    dataGridView1.Rows[i_rows].Cells[1] = DGVC_Staff_Id;

                    DataGridViewCell DGVC_Name = new DataGridViewTextBoxCell();
                    DGVC_Name.Value = dr_Get_Info["Name"].ToString();
                    dataGridView1.Rows[i_rows].Cells[2] = DGVC_Name;

                    DataGridViewCell DGVC_Process_Hours = new DataGridViewTextBoxCell();
                    DGVC_Process_Hours.Value = dr_Get_Info["Process_Hours"].ToString();
                    dataGridView1.Rows[i_rows].Cells[3] = DGVC_Process_Hours;

                    DataGridViewCell DGVC_Extra_Hours = new DataGridViewTextBoxCell();
                    DGVC_Extra_Hours.Value = dr_Get_Info["Extra_Hours"].ToString();
                    dataGridView1.Rows[i_rows].Cells[4] = DGVC_Extra_Hours;
                }
                ds_Get_Info.Dispose();
            }
            else
            {
                DataSet ds_Get_Reltn = FruPak.PF.Data.AccessLayer.PF_Staff.Get_Info_Perm_Staff();
                DataRow dr_Get_Reltn;
                for (int i = 0; i < Convert.ToInt32(ds_Get_Reltn.Tables[0].Rows.Count.ToString()); i++)
                {
                    dr_Get_Reltn = ds_Get_Reltn.Tables[0].Rows[i];
                    FruPak.PF.Data.AccessLayer.PF_Work_Order_Staff_Relationship.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Work_Order_Staff_Relationship"), int_Work_Order_Id, Convert.ToInt32(dr_Get_Reltn["Staff_Id"].ToString()), true, 0, int_Current_User_Id);
                }
                ds_Get_Reltn.Dispose();
                populate_datagridview();
            }
            ds_Get_Info.Dispose();
        }
        private void AddColumnsProgrammatically()
        {
            var col0 = new DataGridViewTextBoxColumn();
            var col1 = new DataGridViewTextBoxColumn();
            var col2 = new DataGridViewTextBoxColumn();
            var col3 = new DataGridViewTextBoxColumn();
            var col4 = new DataGridViewTextBoxColumn();

            col0.HeaderText = "ID";
            col0.Name = "WOStf_Relat_Id";
            col0.ReadOnly = true;
            col0.Visible = false;

            col1.HeaderText = "Staff_Id";
            col1.Name = "Staff_Id";
            col1.ReadOnly = true;
            col1.Visible = false;

            col2.HeaderText = "Staff Member";
            col2.Name = "StaffMember";
            col2.ReadOnly = true;
            col2.Visible = true;

            col3.HeaderText = "Process Hours";
            col3.Name = "ProcessHours";
            col3.ReadOnly = true;

            col4.HeaderText = "Extra Hours";
            col4.Name = "ExtraHours";
            col4.ReadOnly = true;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2, col3, col4 });

            DataGridViewImageColumn img_delete = new DataGridViewImageColumn();
            dataGridView1.Columns.Add(img_delete);
            img_delete.HeaderText = "Remove";
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
        private void SizeAllColumns(Object sender, EventArgs e)
        {
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }
        private void populate_combobox()
        {
            cmb_Staff.DataSource = null;

            DataSet ds_Get_Info;
            ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Staff.Get_Info();
            cmb_Staff.DataSource = ds_Get_Info.Tables[0];
            cmb_Staff.DisplayMember = "Name" ;
            cmb_Staff.ValueMember = "Staff_Id";
            cmb_Staff.Text = null;
            ds_Get_Info.Dispose();

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult DLR_Message = new DialogResult();
            int int_result = 0;


            //Delete
            if (e.ColumnIndex == 5)
            {
                string str_msg;

                str_msg = "You are about to remove ";
                str_msg = str_msg + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + " From the Work Order Staff List " + Environment.NewLine;
                str_msg = str_msg + "Do you want to continue?";
                DLR_Message = MessageBox.Show(str_msg, "Process Factory - Work Order(Staff) - Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (DLR_Message == DialogResult.Yes)
                {
                    int_result = FruPak.PF.Data.AccessLayer.PF_Work_Order_Staff_Relationship.Delete(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                }

                if (int_result > 0)
                {
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    lbl_message.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + " has been remove from this Work Order";
                }
                else
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + " failed to be removed from this Work Order";
                }
                populate_datagridview();
            }
            //Edit
            else if (e.ColumnIndex == 6)
            {
                int_DVG_Row_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                cmb_Staff.SelectedValue = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());

                ckb_Process_Hours.Checked = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                nud_Extra_Hours.Value = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                btn_Add.Text = "&Update";
            }

        }
        private void btn_Add_Click(object sender, EventArgs e)
        {
            Add_btn();
        }
        private void Add_btn()
        {
            DialogResult DLR_MessageBox = new DialogResult();
            DialogResult DLR_MessageBox2 = new DialogResult();
            DialogResult DLR_MessageBox3 = new DialogResult();

            string str_msg = "";
            int int_result = 0;

            if (cmb_Staff.SelectedValue == null && cmb_Staff.Text.ToString().Length == 0)
            {
                str_msg = str_msg + "Invalid Staff Member: Please select a Staff Member from the DropDown List." + Environment.NewLine;
                str_msg = str_msg + "Or Enter a New Staff Menbers Name." + Environment.NewLine;
            }
            if (str_msg.Length > 0)
            {
                DLR_MessageBox = MessageBox.Show(str_msg, "Process Factory - Work Order(Staff)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (DLR_MessageBox != DialogResult.OK)
            {
                if (btn_Add.Text == "&Add")
                {
                    if (cmb_Staff.SelectedValue != null)
                    {
                        int_result = FruPak.PF.Data.AccessLayer.PF_Work_Order_Staff_Relationship.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Work_Order_Staff_Relationship"), int_Work_Order_Id, Convert.ToInt32(cmb_Staff.SelectedValue.ToString()), Convert.ToBoolean(ckb_Process_Hours.Checked), Convert.ToDecimal(nud_Extra_Hours.Value), int_Current_User_Id);
                        lbl_message.Text = "Staff Member has been added to the Work Order";
                    }
                    else
                    {
                        string str_first_name = "";
                        string str_last_name = "";
                        int int_emp_num = 0;
                        try
                        {
                            str_first_name = cmb_Staff.Text.ToString().Substring(0, cmb_Staff.Text.ToString().IndexOf(" "));
                            str_last_name = cmb_Staff.Text.ToString().Substring(cmb_Staff.Text.ToString().IndexOf(" ") + 1);
                        }
                        catch
                        {
                            str_first_name = cmb_Staff.Text.ToString();
                        }

                        try
                        {
                            int_emp_num = Convert.ToInt32(txt_Emp_Num.Text);
                        }
                        catch
                        {
                            string str_msg1 = "You have not supplied an Employee Number." + Environment.NewLine;
                            str_msg1 = str_msg1 + "Press Yes to Continue without." + Environment.NewLine;
                            str_msg1 = str_msg1 + "or" + Environment.NewLine;
                            str_msg1 = str_msg1 + "Press No to stop and an input an Employee Number." + Environment.NewLine;
                            DLR_MessageBox2 = MessageBox.Show(str_msg1, "Process Factory - Work Order(Staff)", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
                        }

                        try
                        {
                            Convert.ToDecimal(txt_hourly_rate.Text);
                        }
                        catch
                        {
                            string str_msg1 = "You have not entered a valid Hourly Rate. Please Re-enter a valid numeric value.";
                            DLR_MessageBox3 = MessageBox.Show(str_msg1, "Process Factory - Work Order(Staff)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        string str_emp_type = "";

                        if (rb_Perm_staff.Checked == true)
                        {
                            str_emp_type = "P";
                        }
                        else if (rb_Seasonal_Staff.Checked == true)
                        {
                            str_emp_type = "S";
                        }
                        else if (rb_Casual_Staff.Checked == true)
                        {
                            str_emp_type = "C";
                        }

                        int int_new_Staff_Id = FruPak.PF.Common.Code.General.int_max_user_id("PF_Staff");
                        int_emp_num = int_new_Staff_Id;

                        if (DLR_MessageBox3 != DialogResult.OK)
                        {
                            if (DLR_MessageBox2 == DialogResult.Yes)
                            {
                                int_result = FruPak.PF.Data.AccessLayer.PF_Staff.Insert(int_new_Staff_Id, str_first_name, str_last_name, int_new_Staff_Id, Convert.ToDecimal(txt_hourly_rate.Text), true, str_emp_type, int_Current_User_Id);
                                int_result = FruPak.PF.Data.AccessLayer.PF_Work_Order_Staff_Relationship.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Work_Order_Staff_Relationship"), int_Work_Order_Id, int_new_Staff_Id, Convert.ToBoolean(ckb_Process_Hours.Checked), Convert.ToDecimal(nud_Extra_Hours.Value), int_Current_User_Id);
                                lbl_message.Text = "Staff Member has been added to the Work Order";
                                populate_combobox();

                            }
                            else if (DLR_MessageBox2 != DialogResult.Yes && DLR_MessageBox2 != DialogResult.No)
                            {
                                int_result = FruPak.PF.Data.AccessLayer.PF_Staff.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Staff"), str_first_name, str_last_name, int_emp_num, Convert.ToDecimal(txt_hourly_rate.Text), true, str_emp_type, int_Current_User_Id);
                                int_result = FruPak.PF.Data.AccessLayer.PF_Work_Order_Staff_Relationship.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Work_Order_Staff_Relationship"), int_Work_Order_Id, int_new_Staff_Id, Convert.ToBoolean(ckb_Process_Hours.Checked), Convert.ToDecimal(nud_Extra_Hours.Value), int_Current_User_Id);
                                lbl_message.Text = "Staff Member has been added to the Work Order";
                                populate_combobox();
                            }
                        }
                    }
                }
                else
                {
                    int_result = FruPak.PF.Data.AccessLayer.PF_Work_Order_Staff_Relationship.Update(int_DVG_Row_id, int_Work_Order_Id, Convert.ToInt32(cmb_Staff.SelectedValue.ToString()), Convert.ToBoolean(ckb_Process_Hours.Checked), Convert.ToDecimal(nud_Extra_Hours.Value), int_Current_User_Id);
                    lbl_message.Text = "Staff Member has been update on the Work Order";
                }
            }
            if (int_result > 0)
            {
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                populate_datagridview();
                Reset();
            }
            else
            {
                lbl_message.Text = "Staff Member failed to be added";
                lbl_message.ForeColor = System.Drawing.Color.Red;
            }
        }
        private void Reset()
        {
            cmb_Staff.Text = null;
            ckb_Process_Hours.Checked = false;
            nud_Extra_Hours.Value = 0;
            txt_Emp_Num.ResetText();
            txt_hourly_rate.ResetText();
            grb_Employ.Visible = false;
            btn_Add.Text = "&Add";
        }
        private void cmb_Staff_TextUpdate(object sender, EventArgs e)
        {
            if ((sender as ComboBox).Text.ToString().Length > 0)
            {
                grb_Employ.Visible = true;
            }
            else
            {
                grb_Employ.Visible = false;
                txt_Emp_Num.ResetText();
                txt_hourly_rate.ResetText();
            }
        }
        private void btn_reset_Click(object sender, EventArgs e)
        {
            Reset();
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
        private void WO_Staff_KeyDown(object sender, KeyEventArgs e)
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
