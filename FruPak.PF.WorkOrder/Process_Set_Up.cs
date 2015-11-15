using NLog;
using System;
using System.Data;
using System.Windows.Forms;

namespace FruPak.PF.WorkOrder
{
    public partial class Process_Set_Up : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static bool bol_write_access;
        private static int int_Current_User_Id = 0;

        public Process_Set_Up(int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();
            int_Current_User_Id = int_C_User_id;

            //restrict access
            bol_write_access = bol_w_a;
            btn_Add.Enabled = bol_w_a;

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
            AddColumnsProgrammatically2();
            string str_join = "LEFT OUTER JOIN dbo.PF_Process_Setup_Work_Order_Relationship PSWOR ON PSWOR.Work_Order_Id = WO.Work_Order_Id";
            string str_where = " WHERE PSWOR.Processsetup_Id IS NULL";
            populate_datagridview2(str_join, str_where);

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

            #endregion Log any interesting events from the UI to the CSV log file
        }

        private void populate_combobox()
        {
            DataSet ds = FruPak.PF.Data.AccessLayer.PF_Staff.Get_Info();
            cmb_Staff.DataSource = ds.Tables[0];
            cmb_Staff.DisplayMember = "Name";
            cmb_Staff.ValueMember = "Staff_Id";
            cmb_Staff.Text = null;
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

            col0.HeaderText = "ID";
            col0.Name = "Processsetup_Id";
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

            col3.HeaderText = "Start Date";
            col3.Name = "Start_Date";
            col3.ReadOnly = true;

            col4.HeaderText = "Start Time";
            col4.Name = "Start_Time";
            col4.ReadOnly = true;

            col5.HeaderText = "Finish Date";
            col5.Name = "Finish_Date";
            col5.ReadOnly = true;

            col6.HeaderText = "Finish Time";
            col6.Name = "Finish_Time";
            col6.ReadOnly = true;

            col7.HeaderText = "Comments";
            col7.Name = "Comments";
            col7.Width = 200;
            col7.ReadOnly = true;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2, col3, col4, col5, col6, col7 });

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

        private void AddColumnsProgrammatically2()
        {
            var col0 = new DataGridViewTextBoxColumn();
            var col1 = new DataGridViewTextBoxColumn();
            var col2 = new DataGridViewTextBoxColumn();
            var col3 = new DataGridViewTextBoxColumn();
            var col4 = new DataGridViewTextBoxColumn();
            var col5 = new DataGridViewTextBoxColumn();
            var col6 = new DataGridViewTextBoxColumn();
            var col7 = new DataGridViewTextBoxColumn();

            col0.HeaderText = "Work Order";
            col0.Name = "Work_Order_Id";
            col0.ReadOnly = true;

            col1.HeaderText = "Date";
            col1.Name = "Process_Date";
            col1.ReadOnly = true;

            col2.HeaderText = "Product_Id";
            col2.Name = "Product_Id";
            col2.ReadOnly = true;
            col2.Visible = false;

            col3.HeaderText = "Product";
            col3.Name = "Product";
            col3.ReadOnly = true;

            col4.HeaderText = "FruitType_Id";
            col4.Name = "FruitType_Id";
            col4.ReadOnly = true;
            col4.Visible = false;

            col5.HeaderText = "FruityType";
            col5.Name = "FruityType";
            col5.ReadOnly = true;

            col6.HeaderText = "Variety_Id";
            col6.Name = "Variety_Id";
            col6.ReadOnly = true;
            col6.Visible = false;

            col7.HeaderText = "FruitVariety";
            col7.Name = "FruitVariety";
            col7.ReadOnly = true;

            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2, col3, col4, col5, col6, col7 });

            DataGridViewCheckBoxColumn chk_select = new DataGridViewCheckBoxColumn();
            dataGridView2.Columns.Add(chk_select);
            chk_select.HeaderText = "Select";
            chk_select.Name = "Select";
            chk_select.ReadOnly = false;
        }

        private void populate_datagridview()
        {
            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();

            DataSet ds_Get_Info;
            DataRow dr_Get_Info;

            ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Process_Setup.Get_Info_Translated();

            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                int i_rows = Convert.ToInt32(dataGridView1.Rows.Count.ToString());
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                dataGridView1.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_Cell0 = new DataGridViewTextBoxCell();
                DGVC_Cell0.Value = dr_Get_Info["Processsetup_Id"].ToString();
                dataGridView1.Rows[i_rows].Cells["Processsetup_Id"] = DGVC_Cell0;

                DataGridViewCell DGVC_Cell1 = new DataGridViewTextBoxCell();
                DGVC_Cell1.Value = dr_Get_Info["Staff_Id"].ToString();
                dataGridView1.Rows[i_rows].Cells["Staff_Id"] = DGVC_Cell1;

                DataGridViewCell DGVC_Cell2 = new DataGridViewTextBoxCell();
                DGVC_Cell2.Value = dr_Get_Info["Name"].ToString();
                dataGridView1.Rows[i_rows].Cells["StaffMember"] = DGVC_Cell2;

                DateTime dt = new DateTime();
                dt = Convert.ToDateTime(dr_Get_Info["Start_Date"].ToString());
                DataGridViewCell DGVC_Cell3 = new DataGridViewTextBoxCell();
                DGVC_Cell3.Value = dt.Day.ToString() + "/" + dt.Month.ToString() + "/" + dt.Year.ToString();
                dataGridView1.Rows[i_rows].Cells["Start_Date"] = DGVC_Cell3;

                dt = Convert.ToDateTime(dr_Get_Info["Start_Time"].ToString());
                DataGridViewCell DGVC_Cell4 = new DataGridViewTextBoxCell();
                DGVC_Cell4.Value = dt.Hour.ToString() + ":" + dt.Minute.ToString() + ":" + dt.Second.ToString();
                dataGridView1.Rows[i_rows].Cells["Start_Time"] = DGVC_Cell4;

                dt = Convert.ToDateTime(dr_Get_Info["Finish_Date"].ToString());
                DataGridViewCell DGVC_Cell5 = new DataGridViewTextBoxCell();
                DGVC_Cell5.Value = dt.Day.ToString() + "/" + dt.Month.ToString() + "/" + dt.Year.ToString();
                dataGridView1.Rows[i_rows].Cells["Finish_Date"] = DGVC_Cell5;

                dt = Convert.ToDateTime(dr_Get_Info["Finish_Time"].ToString());
                DataGridViewCell DGVC_Cell6 = new DataGridViewTextBoxCell();
                DGVC_Cell6.Value = dt.Hour.ToString() + ":" + dt.Minute.ToString() + ":" + dt.Second.ToString();
                dataGridView1.Rows[i_rows].Cells["Finish_Time"] = DGVC_Cell6;

                DataGridViewCell DGVC_Cell7 = new DataGridViewTextBoxCell();
                DGVC_Cell7.Value = dr_Get_Info["Comments"].ToString();
                dataGridView1.Rows[i_rows].Cells["Comments"] = DGVC_Cell7;
            }
            ds_Get_Info.Dispose();
            ColumnSize();
        }

        private void populate_datagridview2(string str_join, string str_where)
        {
            dataGridView2.Refresh();
            dataGridView2.Rows.Clear();

            DataSet ds = FruPak.PF.Data.AccessLayer.PF_Work_Order.Get_WO_For_SetUP(str_join, str_where);
            DataRow dr;

            for (int i = 0; i < Convert.ToInt32(ds.Tables[0].Rows.Count.ToString()); i++)
            {
                dr = ds.Tables[0].Rows[i];

                int i_rows = Convert.ToInt32(dataGridView2.Rows.Count.ToString());
                dataGridView2.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_Cell0 = new DataGridViewTextBoxCell();
                DGVC_Cell0.Value = dr["Work_Order_Id"].ToString();
                dataGridView2.Rows[i_rows].Cells["Work_Order_Id"] = DGVC_Cell0;

                DataGridViewCell DGVC_Cell1 = new DataGridViewTextBoxCell();
                DGVC_Cell1.Value = dr["Process_Date"].ToString();
                dataGridView2.Rows[i_rows].Cells["Process_Date"] = DGVC_Cell1;

                DataGridViewCell DGVC_Cell2 = new DataGridViewTextBoxCell();
                DGVC_Cell2.Value = dr["Product_Id"].ToString();
                dataGridView2.Rows[i_rows].Cells["Product_Id"] = DGVC_Cell2;

                DataGridViewCell DGVC_Cell3 = new DataGridViewTextBoxCell();
                DGVC_Cell3.Value = dr["Product"].ToString();
                dataGridView2.Rows[i_rows].Cells["Product"] = DGVC_Cell3;

                DataGridViewCell DGVC_Cell4 = new DataGridViewTextBoxCell();
                DGVC_Cell4.Value = dr["FruitType_Id"].ToString();
                dataGridView2.Rows[i_rows].Cells["FruitType_Id"] = DGVC_Cell4;

                DataGridViewCell DGVC_Cell5 = new DataGridViewTextBoxCell();
                DGVC_Cell5.Value = dr["FruityType"].ToString();
                dataGridView2.Rows[i_rows].Cells["FruityType"] = DGVC_Cell5;

                DataGridViewCell DGVC_Cell6 = new DataGridViewTextBoxCell();
                DGVC_Cell6.Value = dr["Variety_Id"].ToString();
                dataGridView2.Rows[i_rows].Cells["Variety_Id"] = DGVC_Cell6;

                DataGridViewCell DGVC_Cell7 = new DataGridViewTextBoxCell();
                DGVC_Cell7.Value = dr["FruitVariety"].ToString();
                dataGridView2.Rows[i_rows].Cells["FruitVariety"] = DGVC_Cell7;

                DataSet ds_distinct = FruPak.PF.Data.AccessLayer.PF_Process_Setup_Work_Order_Relationship.Get_Selected(Convert.ToInt32(dr["Work_Order_Id"].ToString()));

                if (ds_distinct.Tables[0].Rows.Count > 0)
                {
                    DataGridViewCell DGVC_Cell8 = new DataGridViewCheckBoxCell();
                    DGVC_Cell8.Value = true;
                    dataGridView2.Rows[i_rows].Cells["Select"] = DGVC_Cell8;
                }
            }
            ds.Dispose();
            ColumnSize();
        }

        private void SizeAllColumns(Object sender, EventArgs e)
        {
            ColumnSize();
        }

        private void ColumnSize()
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

            dataGridView2.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            DialogResult DLR_Message = new System.Windows.Forms.DialogResult();
            int int_result = 0;

            string str_msg = "";
            //no date or time load
            if (dtp_start_date.Value.Date == dtp_finish_date.Value.Date && dtp_start_time.Value.ToLocalTime() == dtp_finish_time.Value.ToLocalTime())
            {
                str_msg = str_msg + "Invalid date and time. Please enter valid Start and Finsih dates and times." + Environment.NewLine;
            }
            // Finish date must be after start date
            else if (dtp_start_date.Value.Date > dtp_finish_date.Value.Date)
            {
                str_msg = str_msg + "Invalid Start and Finsh Dates. You can not finsh before you start. " + Environment.NewLine;
            }
            // start and finsh on same day, finish time must be after start time
            else if (dtp_start_date.Value.Date == dtp_finish_date.Value.Date)
            {
                if (((dtp_start_time.Value.Hour * 360) + (dtp_start_time.Value.Minute * 60) + dtp_start_time.Value.Second) > ((dtp_finish_time.Value.Hour * 360) + (dtp_finish_time.Value.Minute * 60) + dtp_finish_time.Value.Second))
                {
                    str_msg = str_msg + "Invalid Start and Finsh Times. You can not finsh before you start. " + Environment.NewLine;
                }
            }

            if (str_msg.Length > 0)
            {
                DLR_Message = MessageBox.Show(str_msg, "Process Factory - Setup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (DLR_Message != System.Windows.Forms.DialogResult.OK)
            {
                switch (btn_Add.Text)
                {
                    case "&Add":
                        int_result = FruPak.PF.Data.AccessLayer.PF_Process_Setup.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Process_Setup"), Convert.ToInt32(cmb_Staff.SelectedValue.ToString()), dtp_start_date.Value.ToString("yyyy/MM/dd"),
                                                                                    dtp_start_time.Value.Hour.ToString() + ":" + dtp_start_time.Value.Minute.ToString() + ":" + dtp_start_time.Value.Second.ToString(), dtp_finish_date.Value.ToString("yyyy/MM/dd"),
                                                                                    dtp_finish_time.Value.Hour.ToString() + ":" + dtp_finish_time.Value.Minute.ToString() + ":" + dtp_finish_time.Value.Second.ToString(), txt_comments.Text, int_Current_User_Id);
                        break;

                    case "&Update":
                        int_result = FruPak.PF.Data.AccessLayer.PF_Process_Setup.Update(int_dvg_Cell0, Convert.ToInt32(cmb_Staff.SelectedValue.ToString()), dtp_start_date.Value.ToString("yyyy/MM/dd"),
                                                                                    dtp_start_time.Value.Hour.ToString() + ":" + dtp_start_time.Value.Minute.ToString() + ":" + dtp_start_time.Value.Second.ToString(), dtp_finish_date.Value.ToString("yyyy/MM/dd"),
                                                                                    dtp_finish_time.Value.Hour.ToString() + ":" + dtp_finish_time.Value.Minute.ToString() + ":" + dtp_finish_time.Value.Second.ToString(), txt_comments.Text, int_Current_User_Id);

                        break;
                }
            }
            if (int_result > 0)
            {
                switch (btn_Add.Text)
                {
                    case "&Add":
                        lbl_message.Text = "Staff Time has been added";
                        break;

                    case "&Update":
                        lbl_message.Text = "Staff Time has been updated";
                        break;
                }

                lbl_message.ForeColor = System.Drawing.Color.Blue;
                populate_datagridview();
                Reset();
            }
            else
            {
                lbl_message.Text = "Staff Time failed to be added/update";
                lbl_message.ForeColor = System.Drawing.Color.Red;
            }
        }

        private int int_dvg_Cell0 = 0;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult DLR_Message = new DialogResult();
            int int_result = 0;

            //Delete
            if (e.ColumnIndex == 8)
            {
                string str_msg;

                str_msg = "You are about to remove ";
                str_msg = str_msg + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + " From the Setup  List " + Environment.NewLine;
                str_msg = str_msg + "Do you want to continue?";
                DLR_Message = MessageBox.Show(str_msg, "Process Factory - Process SetUp(Staff) - Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (DLR_Message == DialogResult.Yes)
                {
                    int_result = FruPak.PF.Data.AccessLayer.PF_Process_Setup.Delete(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Processsetup_Id"].Value.ToString()));
                }

                if (int_result > 0)
                {
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    lbl_message.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + " has been remove from this Process Setup";
                }
                else
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + " failed to be removed from this Process Setup";
                }
                populate_datagridview();
            }
            //Edit
            else if (e.ColumnIndex == 9)
            {
                int_dvg_Cell0 = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Processsetup_Id"].Value.ToString());
                cmb_Staff.SelectedValue = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Processsetup_Id"].Value.ToString());
                dtp_start_date.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["Start_Date"].Value.ToString());
                dtp_start_time.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["Start_Time"].Value.ToString());
                dtp_finish_date.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["Finish_Date"].Value.ToString());
                dtp_finish_time.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["Finish_Time"].Value.ToString());
                txt_comments.Text = dataGridView1.CurrentRow.Cells["Comments"].Value.ToString();
                btn_Add.Text = "&Update";
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            cmb_Staff.Text = null;
            dtp_start_date.Value = DateTime.Now;
            dtp_start_time.Value = DateTime.Now;
            dtp_finish_date.Value = DateTime.Now;
            dtp_finish_time.Value = DateTime.Now;
            txt_comments.ResetText();
            btn_Add.Text = "&Add";
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                if (Convert.ToBoolean(dataGridView2.CurrentCell.Value) == true)
                {
                    dataGridView2.CurrentCell.Value = false;
                }
                else
                {
                    dataGridView2.CurrentCell.Value = true;
                }
            }
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            int int_result = 0;

            for (int i2 = 0; i2 < dataGridView2.Rows.Count; i2++)
            {
                if (Convert.ToBoolean(dataGridView2.Rows[i2].Cells["Select"].Value) == true)
                {
                    FruPak.PF.Data.AccessLayer.PF_Process_Setup_Work_Order_Relationship.Delete(Convert.ToInt32(dataGridView1.CurrentRow.Cells["Processsetup_Id"].Value.ToString()), Convert.ToInt32(dataGridView2.Rows[i2].Cells["Work_Order_Id"].Value.ToString()));
                    int_result =
                    int_result + FruPak.PF.Data.AccessLayer.PF_Process_Setup_Work_Order_Relationship.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Process_Setup_Work_Order_Relationship"),
                        Convert.ToInt32(dataGridView1.CurrentRow.Cells["Processsetup_Id"].Value.ToString()), Convert.ToInt32(dataGridView2.Rows[i2].Cells["Work_Order_Id"].Value.ToString()), int_Current_User_Id);
                }
                else
                {
                    int_result =
                    int_result + FruPak.PF.Data.AccessLayer.PF_Process_Setup_Work_Order_Relationship.Delete(Convert.ToInt32(dataGridView1.CurrentRow.Cells["Processsetup_Id"].Value.ToString()), Convert.ToInt32(dataGridView2.Rows[i2].Cells["Work_Order_Id"].Value.ToString()));
                }
            }

            if (int_result > 0)
            {
                lbl_message.Text = "Work Orders have been Updated";
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                string str_join = "LEFT OUTER JOIN dbo.PF_Process_Setup_Work_Order_Relationship PSWOR ON PSWOR.Work_Order_Id = WO.Work_Order_Id";
                string str_where = " WHERE PSWOR.Processsetup_Id IS NULL and WO.Fruit_Type_Id <> 1 ";
                populate_datagridview2(str_join, str_where);
            }
            else
            {
                lbl_message.Text = "Work Orders failed to Updated";
                lbl_message.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            FruPak.PF.Data.AccessLayer.PF_Process_Setup.Update_Active(Convert.ToBoolean(false), int_Current_User_Id);
            populate_datagridview();

            string str_join = "INNER JOIN dbo.PF_Process_Setup_Work_Order_Relationship PSWOR ON PSWOR.Work_Order_Id <> WO.Work_Order_Id";
            string str_where = "WHERE WO.Fruit_Type_Id <> 1 ";
            populate_datagridview2(str_join, str_where);
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

        #endregion Decorate String

        /// <summary>
        /// Close the form with the Esc key (Sel request 11-02-2015 BN)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Process_Set_Up_KeyDown(object sender, KeyEventArgs e)
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