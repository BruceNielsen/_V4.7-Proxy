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
    public partial class PF_Staff : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();     

        private static int int_Current_User_Id = 0;
        //private static int int_DVG_Row_id = 0;
        private static bool bol_write_access;
        public PF_Staff(int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();
            //restrict access
            bol_write_access = bol_w_a;
            btn_Add.Enabled = bol_w_a;
            int_Current_User_Id = int_C_User_id;
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
        private void AddColumnsProgrammatically()
        {
            var col0 = new DataGridViewTextBoxColumn();
            var col1 = new DataGridViewTextBoxColumn();
            var col2 = new DataGridViewTextBoxColumn();
            var col3 = new DataGridViewTextBoxColumn();
            var col4 = new DataGridViewTextBoxColumn();
            var col5 = new DataGridViewTextBoxColumn();
            var col6 = new DataGridViewTextBoxColumn();
            

            col0.HeaderText = "Id";
            col0.Name = "Id";
            col0.ReadOnly = true;
            col0.Visible = false;

            col1.HeaderText = "Frist Name";
            col1.Name = "First_Name";
            col1.ReadOnly = true;

            col2.HeaderText = "Last Name";
            col2.Name = "Last_Name";
            col2.ReadOnly = true;

            col3.HeaderText = "Employee Number";
            col3.Name = "Employee_Num";
            col3.Width = 60;
            col3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col3.ReadOnly = true;

            col4.HeaderText = "Employee Type";
            col4.Name = "Emp_Type";
            col4.Width = 60;
            col4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col4.ReadOnly = true;

            col5.HeaderText = "Hourly Rate";
            col5.Name = "Hourly_Rate";
            col5.ReadOnly = true;

            col6.HeaderText = "Active";
            col6.Name = "Active_Ind";
            col6.ReadOnly = true;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2, col3, col4, col5, col6});

            DataGridViewImageColumn img_delete = new DataGridViewImageColumn();
            dataGridView1.Columns.Add(img_delete);
            img_delete.HeaderText = "Delete";
            img_delete.Name = "Delete";
            img_delete.Image = FruPak.PF.Global.Properties.Resources.delete;
            img_delete.ReadOnly = true;
            img_delete.Visible = bol_write_access;

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

            DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Staff.Get_Info_ALL();
            DataRow dr_Get_Info;
            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                dataGridView1.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_Cell0 = new DataGridViewTextBoxCell();
                DGVC_Cell0.Value = dr_Get_Info["Staff_Id"].ToString();
                dataGridView1.Rows[i].Cells[0] = DGVC_Cell0;

                DataGridViewCell DGVC_Cell1 = new DataGridViewTextBoxCell();
                DGVC_Cell1.Value = dr_Get_Info["First_Name"].ToString();
                dataGridView1.Rows[i].Cells[1] = DGVC_Cell1;

                DataGridViewCell DGVC_Cell2 = new DataGridViewTextBoxCell();
                DGVC_Cell2.Value = dr_Get_Info["Last_Name"].ToString();
                dataGridView1.Rows[i].Cells[2] = DGVC_Cell2;

                DataGridViewCell DGVC_Cell3 = new DataGridViewTextBoxCell();
                DGVC_Cell3.Value = dr_Get_Info["Emp_Number"].ToString();
                dataGridView1.Rows[i].Cells[3] = DGVC_Cell3;

                DataGridViewCell DGVC_Cell4 = new DataGridViewTextBoxCell();
                DGVC_Cell4.Value = dr_Get_Info["Emp_Type"].ToString();
                dataGridView1.Rows[i].Cells[4] = DGVC_Cell4;

                DataGridViewCell DGVC_Cell5 = new DataGridViewTextBoxCell();
                DGVC_Cell5.Value = dr_Get_Info["Hourly_Rate"].ToString();
                dataGridView1.Rows[i].Cells[5] = DGVC_Cell5;

                DataGridViewCell DGVC_Cell6 = new DataGridViewTextBoxCell();
                DGVC_Cell6.Value = dr_Get_Info["PF_Active_Ind"].ToString();
                dataGridView1.Rows[i].Cells[6] = DGVC_Cell6;

                Column_Size();
            }
            ds_Get_Info.Dispose();
        }
        private void SizeAllColumns(Object sender, EventArgs e)
        {
            Column_Size();
        }
        private void Column_Size()
        {
           // dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataGridView1.AutoResizeColumn(0, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(1, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(2, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(5, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(6, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(7, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(8, DataGridViewAutoSizeColumnMode.AllCells);
        }
        private void btn_Add_Click(object sender, EventArgs e)
        {
            Add_btn();
        }
        private void Add_btn()
        {
            DialogResult DLR_MessageBox = new DialogResult();
            string str_msg = "";
            int int_result = 0;

            if (txt_First_Name.TextLength == 0)
            {
                str_msg = str_msg + "Invalid First Name. Please enter a valid First Name." + Environment.NewLine;
            }
            if (txt_Employee_Num.TextLength == 0)
            {
                str_msg = str_msg + "Invalid Enployee Number. Please enter a valid Employee Number." + Environment.NewLine;
            }
            else
            {
                try
                {
                    Convert.ToInt32(txt_Employee_Num.Text);
                    switch (btn_Add.Text)
                    {
                        case "Add":
                            DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Staff.Check_Employee_Num(Convert.ToInt32(txt_Employee_Num.Text));

                            if (ds_Get_Info.Tables[0].Rows.Count > 0)
                            {
                                str_msg = str_msg + "Invalid Enployee Number. Employee Number has already been used. Please Re-enter with a valid Employee Number" + Environment.NewLine;
                            }
                            break;
                    }
                }
                catch
                {
                    str_msg = str_msg + "Invalid Enployee Number. Employee Number must only contain numerics. Please Re-enter with a valid Employee Number" + Environment.NewLine;
                }
            }
            if (cmb_Emp_Type.Text.Length == 0)
            {
                str_msg = str_msg + "Invalid Employee Type. Please select an Employee Type from the Dropdown list." + Environment.NewLine;
            }
            if (txt_hourly_Rate.TextLength == 0)
            {
                str_msg = str_msg + "Invalid Hourly Rate. Please enter a valid Hourly Rate." + Environment.NewLine;
            }
            else
            {
                try
                {
                    Convert.ToDecimal(txt_hourly_Rate.Text);
                }
                catch
                {
                    str_msg = str_msg + "Invalid Hourly Rate. Hourly Rates must be numeric. Please Re-enter a valid Hourly Rate" + Environment.NewLine;
                }
            }

            if (str_msg.Length > 0)
            {
                DLR_MessageBox = MessageBox.Show(str_msg, "Maintenance - Staff", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (DLR_MessageBox != DialogResult.OK)
            {

                switch (btn_Add.Text)
                {
                    case "Add":
                        int_result = FruPak.PF.Data.AccessLayer.PF_Staff.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Staff"), txt_First_Name.Text, txt_Last_Name.Text, Convert.ToInt32(txt_Employee_Num.Text), Convert.ToDecimal(txt_hourly_Rate.Text), Convert.ToBoolean(ckb_Active.Checked), cmb_Emp_Type.Text.Substring(0, 1), int_Current_User_Id);
                        break;
                    case "Update":
                        int_result = FruPak.PF.Data.AccessLayer.PF_Staff.Update(int_staff_id, txt_First_Name.Text, txt_Last_Name.Text, Convert.ToInt32(txt_Employee_Num.Text), Convert.ToDecimal(txt_hourly_Rate.Text), Convert.ToBoolean(ckb_Active.Checked), cmb_Emp_Type.Text.Substring(0, 1), int_Current_User_Id);
                        break;
                }
            }
            if (int_result > 0)
            {
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                lbl_message.Text = "Staff Member has been saved";
                Reset();
            }
            else
            {
                lbl_message.ForeColor = System.Drawing.Color.Red;
                lbl_message.Text = "Staff Member has NOT been saved";
            }
            populate_datagridview();
        }
        private void btn_reset_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void Reset()
        {
            txt_First_Name.ResetText();
            txt_Last_Name.ResetText();
            txt_Employee_Num.ResetText();
            txt_hourly_Rate.ResetText();
            cmb_Emp_Type.Text = null;
            ckb_Active.Checked = true;
        }
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private static int int_staff_id = 0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Delete
            if (e.ColumnIndex == 7)
            {
                FruPak.PF.Data.AccessLayer.PF_Staff.Update_Active(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()), false, int_Current_User_Id);
                populate_datagridview();
            }
            //Edit
            else if (e.ColumnIndex == 8)
            {
                int_staff_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                txt_First_Name.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txt_Last_Name.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txt_Employee_Num.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txt_hourly_Rate.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                ckb_Active.Checked = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());

                if (dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString() == "P")
                {
                    cmb_Emp_Type.Text = "Permanent";
                }
                else
                {
                    cmb_Emp_Type.Text = "Seasonal";
                }
                btn_Add.Text = "Update";
            }
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
        private void PF_Staff_KeyDown(object sender, KeyEventArgs e)
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
