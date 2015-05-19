using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;

namespace FruPak.PF.Dispatch
{
    public partial class Shipping_Staff : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();     

        private static bool bol_write_access;
        private static int int_Current_User_Id = 0;
        private static int int_order_id;
        private static int int_OStf_Relat_Id = 0;
        public Shipping_Staff(int int_Order, int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();
            int_Current_User_Id = int_C_User_id;
            int_order_id = int_Order;

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
            populate_combobox();
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
        private void populate_combobox()
        {
            cmb_Staff.DataSource = null;

            DataSet ds_Get_Info;
            ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Staff.Get_Info();
            cmb_Staff.DataSource = ds_Get_Info.Tables[0];
            cmb_Staff.DisplayMember = "Name";
            cmb_Staff.ValueMember = "Staff_Id";
            cmb_Staff.Text = null;
            ds_Get_Info.Dispose();

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

            col0.HeaderText = "ID";
            col0.Name = "OStf_Relat_Id";
            col0.ReadOnly = true;
            col0.Visible = false;

            col1.HeaderText = "Order_Id";
            col1.Name = "Order_Id";
            col1.ReadOnly = true;
            col1.Visible = false;

            col2.HeaderText = "Staff_Id";
            col2.Name = "Staff_Id";
            col2.ReadOnly = true;
            col2.Visible = false;

            col3.HeaderText = "Staff Member";
            col3.Name = "StaffMember";
            col3.ReadOnly = true;
            col3.Visible = true;

            col4.HeaderText = "Start";
            col4.Name = "Start";
            col4.ReadOnly = true;

            col5.HeaderText = "Finish";
            col5.Name = "Finish";
            col5.ReadOnly = true;

            col6.HeaderText = "Work Type";
            col6.Name = "Work_Type";
            col6.ReadOnly = true;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2, col3, col4, col5, col6 });

     
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
            SizeColumns();
        }
        private void populate_datagridview()
        {
            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();

            DataSet ds_Get_Info;
            DataRow dr_Get_Info;

            ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Orders_Staff_Relationship.Get_Info_Translated(int_order_id);

            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                int i_rows = Convert.ToInt32(dataGridView1.Rows.Count.ToString());
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                dataGridView1.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_Col0 = new DataGridViewTextBoxCell();
                DGVC_Col0.Value = dr_Get_Info["OStf_Relat_Id"].ToString();
                dataGridView1.Rows[i_rows].Cells[0] = DGVC_Col0;

                DataGridViewCell DGVC_Col1 = new DataGridViewTextBoxCell();
                DGVC_Col1.Value = dr_Get_Info["Order_Id"].ToString();
                dataGridView1.Rows[i_rows].Cells[1] = DGVC_Col1;

                DataGridViewCell DGVC_Col2 = new DataGridViewTextBoxCell();
                DGVC_Col2.Value = dr_Get_Info["Staff_Id"].ToString();
                dataGridView1.Rows[i_rows].Cells[2] = DGVC_Col2;

                DataGridViewCell DGVC_Col3 = new DataGridViewTextBoxCell();
                DGVC_Col3.Value = dr_Get_Info["First_Name"].ToString() + " " + dr_Get_Info["Last_Name"].ToString();
                dataGridView1.Rows[i_rows].Cells[3] = DGVC_Col3;

                DataGridViewCell DGVC_Col4 = new DataGridViewTextBoxCell();
                DGVC_Col4.Value = dr_Get_Info["Start_Time"].ToString().Substring(0, 8);
                dataGridView1.Rows[i_rows].Cells[4] = DGVC_Col4;

                DataGridViewCell DGVC_Col5 = new DataGridViewTextBoxCell();
                DGVC_Col5.Value = dr_Get_Info["Finish_Time"].ToString().Substring(0, 8); 
                dataGridView1.Rows[i_rows].Cells[5] = DGVC_Col5;

                DataGridViewCell DGVC_Col6 = new DataGridViewTextBoxCell();
                switch (dr_Get_Info["Work_Type"].ToString())
                {
                    case "P":
                        DGVC_Col6.Value = "Prep Order";
                        
                        break;
                    case "D":
                        DGVC_Col6.Value = "Deliver Order";
                        break;
                }
                DGVC_Col6.Tag = dr_Get_Info["Work_Type"].ToString();
                dataGridView1.Rows[i_rows].Cells["Work_Type"] = DGVC_Col6;
            }
            ds_Get_Info.Dispose();
        }
        private void SizeColumns()
        {
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }
        private void btn_Add_Click(object sender, EventArgs e)
        {
            Add_btn();
        }
        private void Add_btn()
        {
            string str_msg = "";
            int int_result = 0;
            string str_work_type = "";
            DialogResult dlr_Message = new DialogResult();

            if (cmb_Staff.SelectedIndex == -1)
            {
                str_msg = str_msg + "Invalid Staff Member. YOU must selected a valid staff member from the dropdown list." + Environment.NewLine;
            }
            if (dtp_start.Value == dtp_finish.Value)
            {
                str_msg = str_msg + "Invalid Times. Start and Finsih Times need to be different. Please put in the correct start and finish times." + Environment.NewLine;
            }
            else if (dtp_start.Value > dtp_finish.Value)
            {
                str_msg = str_msg + "Invalid Times. Start and Finsih Times need to be different. Please put in the correct start and finish times." + Environment.NewLine;
            }
            if (rb_Pre_Order.Checked == true)
            {
                str_work_type = "P"; 
            }
            else if (rb_Deliver.Checked == true)
            {
                str_work_type = "D";
            }
            else
            {
                str_work_type = "";
            }
            if (str_msg.Length > 0)
            {
                dlr_Message = MessageBox.Show(str_msg, "Process Factory - Dispatch(Staff)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (dlr_Message != System.Windows.Forms.DialogResult.OK)
            {
                switch (btn_Add.Text)
                {
                    case "Add":
                        int_result = FruPak.PF.Data.AccessLayer.PF_Orders_Staff_Relationship.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Orders_Staff_Relationship"), int_order_id,
                            Convert.ToInt32(cmb_Staff.SelectedValue.ToString()), dtp_start.Value.ToString("HH:mm:ss"), dtp_finish.Value.ToString("HH:mm:ss"),str_work_type, int_Current_User_Id);
                        break;
                    case "Update":
                        int_result = FruPak.PF.Data.AccessLayer.PF_Orders_Staff_Relationship.Update(int_OStf_Relat_Id, Convert.ToInt32(cmb_Staff.SelectedValue.ToString()),
                            dtp_start.Value.ToString("HH:mm:ss"), dtp_finish.Value.ToString("HH:mm:ss"),str_work_type, int_Current_User_Id);
                        break;
                }
            }

            if (int_result > 0)
            {
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                lbl_message.Text = "Staff member has been assigned to the Order.";
            }
            else
            {
                lbl_message.ForeColor = System.Drawing.Color.Red;
                lbl_message.Text = "Staff Member failed to be assigned to the Order.";
            }
            populate_datagridview();
        }
        private void btn_reset_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void Reset()
        {
            cmb_Staff.Text = null;
            dtp_start.Value = DateTime.Now;
            dtp_finish.Value = dtp_start.Value;
            btn_Add.Text = "Add";
        }
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string str_msg = "";
            DialogResult DLR_Message = new DialogResult();
            int int_result = 0;
            //Remove
            if (e.ColumnIndex == 7)
            {
                str_msg = "You are about to remove " + dataGridView1.Rows[e.RowIndex].Cells["StaffMember"].Value.ToString() + Environment.NewLine;
                str_msg = str_msg + "Do you want to continue?";
                DLR_Message = MessageBox.Show(str_msg, "Process Factory - Disptach(Orders (Staff))", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (DLR_Message == DialogResult.Yes)
                {
                    int_result = FruPak.PF.Data.AccessLayer.PF_Orders_Staff_Relationship.Delete(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["OStf_Relat_Id"].Value.ToString()),
                                                                                             Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Staff_Id"].Value.ToString()));
                }

                if (int_result > 0)
                {
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    lbl_message.Text = "Staff Member has been removed from the Order.";
                }
                else
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = "Staff Member failed to be removed from the Order.";
                }
                populate_datagridview();
            }
            //Edit
            else if (e.ColumnIndex == 8)
            {
                int_OStf_Relat_Id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["OStf_Relat_Id"].Value.ToString());
                cmb_Staff.SelectedValue = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Staff_Id"].Value.ToString());
                dtp_start.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["Start"].Value.ToString());
                dtp_finish.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["Finish"].Value.ToString());
                switch (dataGridView1.Rows[e.RowIndex].Cells["Work_Type"].Tag.ToString())
                {
                    case "P":
                        rb_Pre_Order.Checked = true;
                        break;
                    case "D":
                        rb_Deliver.Checked = true;
                        break;
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
        private void Shipping_Staff_KeyDown(object sender, KeyEventArgs e)
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
