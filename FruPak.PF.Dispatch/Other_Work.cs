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
    public partial class Other_Work : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();     

        private static bool bol_write_access;
        private static int int_Current_User_Id = 0;
        private static int int_Other_Work_Id;
        public Other_Work(int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();
            int_Current_User_Id = int_C_User_id;

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

            //setup custom date time format
            dtp_Start.Format = DateTimePickerFormat.Custom;
            dtp_Start.CustomFormat = "dd-MMM-yyyy  HH:mm:ss";
            dtp_Finish.Format = DateTimePickerFormat.Custom;
            dtp_Finish.CustomFormat = "dd-MMM-yyyy  HH:mm:ss";


            populate_combobox();
            AddColumnsProgrammatically();
            populate_DataGridView1();

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
            DataSet ds_Get_Info;

            //Work Type
            ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Other_Work_Types.Get_Info();
            cmb_Work_Type.DataSource = ds_Get_Info.Tables[0];
            cmb_Work_Type.DisplayMember = "Combined";
            cmb_Work_Type.ValueMember = "Other_Work_Types_Id";
            cmb_Work_Type.Text = null;
            ds_Get_Info.Dispose();

            //Destination
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
            var col7 = new DataGridViewTextBoxColumn();

            col0.HeaderText = "Other_Work_Id";
            col0.Name = "Other_Work_Id";
            col0.ReadOnly = true;
            col0.Visible = false;

            col1.HeaderText = "Other_Work_Types_Id";
            col1.Name = "Other_Work_Types_Id";
            col1.ReadOnly = true;
            col1.Visible = false;

            col2.HeaderText = "Other Work Types";
            col2.Name = "Other_Work_Types";
            col2.ReadOnly = true;

            col3.HeaderText = "Staff_Id";
            col3.Name = "Staff_Id";
            col3.ReadOnly = true;
            col3.Visible = false;

            col4.HeaderText = "Staff";
            col4.Name = "Staff";
            col4.ReadOnly = true;
            
            col5.HeaderText = "Start Date/Time";
            col5.Name = "Start_DateTime";
            col5.ReadOnly = true;

            col6.HeaderText = "Finish Date/Time";
            col6.Name = "Finish_DateTime";
            col6.ReadOnly = true;

            col7.HeaderText = "Description";
            col7.Name = "Description";
            col7.ReadOnly = true;
            col7.Width = 200;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2, col3, col4, col5, col6, col7 });

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
        private void SizeAllColumns(Object sender, EventArgs e)
        {
            columnSize();
        }
        private void columnSize()
        {
            dataGridView1.AutoResizeColumn(0, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(1, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(2, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(3, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(4, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(5, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(6, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(8, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(9, DataGridViewAutoSizeColumnMode.AllCells);
        }
        private void populate_DataGridView1()
        {
            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();

            DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Other_Work.Get_Info_Translated();
            DataRow dr_Get_Info;

            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                dataGridView1.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_Cell0 = new DataGridViewTextBoxCell();
                DGVC_Cell0.Value = dr_Get_Info["Other_Work_Id"].ToString();
                dataGridView1.Rows[i].Cells["Other_Work_Id"] = DGVC_Cell0;

                DataGridViewCell DGVC_Cell1 = new DataGridViewTextBoxCell();
                DGVC_Cell1.Value = dr_Get_Info["Other_Work_Types_Id"].ToString();
                dataGridView1.Rows[i].Cells["Other_Work_Types_Id"] = DGVC_Cell1;

                DataGridViewCell DGVC_Cell2 = new DataGridViewTextBoxCell();
                DGVC_Cell2.Value = dr_Get_Info["OWT_Code"].ToString();
                dataGridView1.Rows[i].Cells["Other_Work_Types"] = DGVC_Cell2;

                DataGridViewCell DGVC_Cell3 = new DataGridViewTextBoxCell();
                DGVC_Cell3.Value = dr_Get_Info["Staff_Id"].ToString();
                dataGridView1.Rows[i].Cells["Staff_Id"] = DGVC_Cell3;

                DataGridViewCell DGVC_Cell4 = new DataGridViewTextBoxCell();
                DGVC_Cell4.Value = dr_Get_Info["Name"].ToString();
                dataGridView1.Rows[i].Cells["Staff"] = DGVC_Cell4;

                DataGridViewCell DGVC_Cell5 = new DataGridViewTextBoxCell();
                DGVC_Cell5.Value = dr_Get_Info["Start_DateTime"].ToString();
                dataGridView1.Rows[i].Cells["Start_DateTime"] = DGVC_Cell5;

                DataGridViewCell DGVC_Cell6 = new DataGridViewTextBoxCell();
                DGVC_Cell6.Value = dr_Get_Info["Finish_DateTime"].ToString();
                dataGridView1.Rows[i].Cells["Finish_DateTime"] = DGVC_Cell6;

                DataGridViewCell DGVC_Cell7 = new DataGridViewTextBoxCell();
                DGVC_Cell7.Value = dr_Get_Info["Description"].ToString();
                dataGridView1.Rows[i].Cells["Description"] = DGVC_Cell7;

            }
            columnSize();
            ds_Get_Info.Dispose();
        }
        private void btn_Add_Click(object sender, EventArgs e)
        {
            DialogResult DLR_message = new DialogResult();
            String str_msg = "";
            int int_result = 0;

            if (cmb_Work_Type.SelectedIndex == -1)
            {
                str_msg = str_msg + "Invalid Work Type. Please select a valid Work Type from the drop down list." + Environment.NewLine;
            }
            if (cmb_Staff.SelectedIndex == -1)
            {
                str_msg = str_msg + "Invalid Staff Memeber. Please select a valid Staff Member from the drop down list." + Environment.NewLine;
            }
            //start date time  equals finish date time
            if (dtp_Start.Value.ToShortDateString() == dtp_Finish.Value.ToShortDateString() && dtp_Start.Value.ToShortTimeString() == dtp_Finish.Value.ToShortTimeString())
            {
                str_msg = str_msg + "Invalid Date Time. Start Date and Time must be different from the Finish Date and Time." + Environment.NewLine;
            }
            //start date = finish date, start time is after finish time
            else if (dtp_Start.Value.ToShortDateString() == dtp_Finish.Value.ToShortDateString() && Convert.ToDateTime(dtp_Start.Value.ToShortTimeString()) > Convert.ToDateTime(dtp_Finish.Value.ToShortTimeString()))
            {
                str_msg = str_msg + "Invalid Date Time. You can not finish before you start." + Environment.NewLine;
            }
            //start date > finish date
            else if (Convert.ToDateTime(dtp_Start.Value.ToShortDateString()) > Convert.ToDateTime(dtp_Finish.Value.ToShortDateString()) )
            {
                str_msg = str_msg + "Invalid Date Time. You can not finish before you start." + Environment.NewLine;
            }

            if (str_msg.Length > 0)
            {
                DLR_message = MessageBox.Show(str_msg, "Process Factory - Other Work", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (DLR_message != System.Windows.Forms.DialogResult.OK)
            {
                switch (btn_Add.Text)
                {
                    case "Add":
                        int_result = FruPak.PF.Data.AccessLayer.PF_Other_Work.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Other_Work"), Convert.ToInt32(cmb_Work_Type.SelectedValue.ToString()), Convert.ToInt32(cmb_Staff.SelectedValue.ToString()),
                            dtp_Start.Value.ToShortDateString() + " " + dtp_Start.Value.Hour.ToString() +":"+ dtp_Start.Value.Minute +":" + dtp_Start.Value.Second.ToString(),
                            dtp_Finish.Value.ToShortDateString() + " " + dtp_Finish.Value.Hour.ToString() + ":" + dtp_Finish.Value.Minute.ToString() + ":" + dtp_Finish.Value.Second.ToString(),
                            txt_Description.Text, int_Current_User_Id);
                        break;
                    case "Update":
                        int_result = FruPak.PF.Data.AccessLayer.PF_Other_Work.Update(int_Other_Work_Id, Convert.ToInt32(cmb_Work_Type.SelectedValue.ToString()), Convert.ToInt32(cmb_Staff.SelectedValue.ToString()),
                            dtp_Start.Value.ToShortDateString() + " " + dtp_Start.Value.Hour.ToString() + ":" + dtp_Start.Value.Minute + ":" + dtp_Start.Value.Second.ToString(),
                            dtp_Finish.Value.ToShortDateString() + " " + dtp_Finish.Value.Hour.ToString() + ":" + dtp_Finish.Value.Minute.ToString() + ":" + dtp_Finish.Value.Second.ToString(),
                            txt_Description.Text, int_Current_User_Id);
                        break;
                }
            }
            if (int_result > 0)
            {
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                lbl_message.Text = "Other Work has been saved";
                populate_DataGridView1();
                Reset();
            }
            else
            {
                lbl_message.ForeColor = System.Drawing.Color.Red;
                lbl_message.Text = "Other Work has NOT been saved";
            }
        }
        private void btn_reset_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void Reset()
        {
            dtp_Start.Value = DateTime.Now;
            dtp_Finish.Value = DateTime.Now;
            cmb_Staff.Text = null;
            cmb_Work_Type.Text = null;
            txt_Description.ResetText();
        }
        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            int int_Work_type_Id = 0;
            int int_Staff_Id = 0;

            if (cmb_Work_Type.SelectedValue != null)
            {
                int_Work_type_Id = Convert.ToInt32(cmb_Work_Type.SelectedValue.ToString());
            }
            if (cmb_Staff.SelectedValue != null)
            {
                int_Staff_Id = Convert.ToInt32(cmb_Staff.SelectedValue.ToString());
            }
            populate_combobox();

            cmb_Work_Type.SelectedValue = int_Work_type_Id;
            cmb_Staff.SelectedValue = int_Staff_Id;
        }
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lbl_message.ResetText();

            //Delete
            if (e.ColumnIndex == 8)
            {
                int int_result = FruPak.PF.Common.Code.General.Delete_Record("PF_Other_Work", dataGridView1.Rows[e.RowIndex].Cells["Other_Work_Id"].Value.ToString(), 
                                                                        " Other Work entry for " + dataGridView1.Rows[e.RowIndex].Cells["Staff"].Value.ToString());
                if (int_result > 0)
                {
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    lbl_message.Text = "Entry has been Deleted";
                }
                else
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = "Entry failed to delete";
                }
                populate_DataGridView1();
            }
            else if (e.ColumnIndex == 9)
            {
                int_Other_Work_Id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Other_Work_Id"].Value.ToString());
                cmb_Work_Type.SelectedValue = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Other_Work_Types_Id"].Value.ToString());
                cmb_Staff.SelectedValue = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Staff_Id"].Value.ToString());
                dtp_Start.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["Start_DateTime"].Value.ToString());
                dtp_Finish.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["Finish_DateTime"].Value.ToString());
                txt_Description.Text = dataGridView1.Rows[e.RowIndex].Cells["Description"].Value.ToString();
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
        private void Other_Work_KeyDown(object sender, KeyEventArgs e)
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
