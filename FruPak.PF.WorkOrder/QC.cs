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
    public partial class QC : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static bool bol_write_access;
        private static int int_Current_User_Id = 0;
        private static int int_Work_Order_Id = 0;
        public QC(int int_wo_Id, int int_C_User_id, bool bol_w_a)
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
            populate_RPIN();
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
        private void populate_RPIN()
        {
            DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Work_Order.Get_Info(int_Work_Order_Id);
            DataRow dr_Get_Info;
            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                txt_grower.Text = dr_Get_Info["Grower"].ToString();
            }
            ds_Get_Info.Dispose();
        }
        private void populate_combobox()
        {
            cmb_Defect.SeparatorColor = Color.DarkBlue;
            cmb_Defect.SeparatorWidth = 2;
            cmb_Defect.AutoAdjustItemHeight = true;
            DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Defect_Class.Get_Info();
            DataRow dr_Get_Info;

            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i ++ )
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                cmb_Defect.AddStringWithSeparator(dr_Get_Info["Code"].ToString().PadLeft(40));

                DataSet ds_Get_Info2 = FruPak.PF.Data.AccessLayer.CM_Defect.Get_Info_BY_Defect_Class(dr_Get_Info["Code"].ToString());
                DataRow dr_Get_Info2;
                for (int i2 = 0; i2 < Convert.ToInt32(ds_Get_Info2.Tables[0].Rows.Count.ToString()); i2++)
                {
                    dr_Get_Info2 = ds_Get_Info2.Tables[0].Rows[i2];
                    cmb_Defect.AddString(dr_Get_Info2["Defect_Id"].ToString() + " - " + dr_Get_Info2["Code"].ToString() + " - " + dr_Get_Info2["Description"].ToString());
                }
                ds_Get_Info2.Dispose();
            }
            ds_Get_Info.Dispose();

            cmb_Defect.SelectedIndex = 0;
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

            col0.HeaderText = "Id";
            col0.Name = "Id";
            col0.ReadOnly = true;
            col0.Visible = false;

            col1.HeaderText = "Work Order";
            col1.Name = "Work_Order_Id";
            col1.ReadOnly = true;

            col2.HeaderText = "Date";
            col2.Name = "Work_Order_Date";
            col2.ReadOnly = true;

            col3.HeaderText = "Rpin";
            col3.Name = "Rpin";
            col3.ReadOnly = true;

            col4.HeaderText = "Defect_ID";
            col4.Name = "Defect_ID";
            col4.ReadOnly = true;
            col4.Visible = false;

            col5.HeaderText = "Defect";
            col5.Name = "Defect";
            col5.ReadOnly = true;

            col6.HeaderText = "Num Found";
            col6.Name = "Num_found";
            col6.ReadOnly = true;

            col7.HeaderText = "Comments";
            col7.Name = "Comments";
            col7.Width = 200;
            col7.ReadOnly = true;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2, col3, col4, col5, col6, col7 });

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

            Column_Size();
        }
        private void SizeAllColumns(Object sender, EventArgs e)
        {
            Column_Size();
        }
        private void Column_Size()
        {
            // TODO: This line of code loads data into the 'moneyDataSet.List_TS_Work' table. You can move, or remove it, as needed.
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
        private void populate_datagridview()
        {
            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();

            DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Defect_Results.Get_Info_Translated(int_Work_Order_Id);
            DataRow dr_Get_Info;
            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                dataGridView1.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_User_Id = new DataGridViewTextBoxCell();
                DGVC_User_Id.Value = dr_Get_Info["DefectResult_Id"].ToString();
                dataGridView1.Rows[i].Cells[0] = DGVC_User_Id;


                DataGridViewCell DGVC_WO_Id = new DataGridViewTextBoxCell();
                DGVC_WO_Id.Value = dr_Get_Info["Work_Order_Id"].ToString();
                dataGridView1.Rows[i].Cells[1] = DGVC_WO_Id;

                DataGridViewCell DGVC_Process_Date = new DataGridViewTextBoxCell();
                DGVC_Process_Date.Value = dr_Get_Info["Process_Date"].ToString();
                dataGridView1.Rows[i].Cells[2] = DGVC_Process_Date;

                DataGridViewCell DGVC_Rpin = new DataGridViewTextBoxCell();
                DGVC_Rpin.Value = dr_Get_Info["Rpin"].ToString();
                dataGridView1.Rows[i].Cells[3] = DGVC_Rpin;

                DataGridViewCell DGVC_Defect_ID = new DataGridViewTextBoxCell();
                DGVC_Defect_ID.Value = dr_Get_Info["Defect_ID"].ToString();
                dataGridView1.Rows[i].Cells[4] = DGVC_Defect_ID;

                DataGridViewCell DGVC_D_Combined = new DataGridViewTextBoxCell();
                DGVC_D_Combined.Value = dr_Get_Info["Code"].ToString() + " - " +dr_Get_Info["Description"].ToString();
                dataGridView1.Rows[i].Cells[5] = DGVC_D_Combined;

                DataGridViewCell DGVC_num_found = new DataGridViewTextBoxCell();
                DGVC_num_found.Value = dr_Get_Info["num_found"].ToString();
                dataGridView1.Rows[i].Cells[6] = DGVC_num_found;

                DataGridViewCell DGVC_Comments = new DataGridViewTextBoxCell();
                DGVC_Comments.Value = dr_Get_Info["Comments"].ToString();
                dataGridView1.Rows[i].Cells[7] = DGVC_Comments;

                Column_Size();
                ds_Get_Info.Dispose();
            }
        }
        private void btn_Add_Click(object sender, EventArgs e)
        {
            DialogResult DLR_MessageBox = new DialogResult();
            string str_msg = "";
            int int_result = 0;
            int int_Defect_Id = 0;
            if (cmb_Defect.SelectedIndex == 0)
            {
                str_msg = str_msg + "Invalid Defect. Please select a valid Defect from the DropDown List" + Environment.NewLine;
            }
            else
            {
                try
                {
                    int_Defect_Id = Convert.ToInt32(cmb_Defect.SelectedItem.ToString().Substring(0, cmb_Defect.SelectedItem.ToString().IndexOf('-')));
                }
                catch
                {
                    str_msg = str_msg + "Invalid Defect. Please select a valid Defect from the DropDown List" + Environment.NewLine;
                }
            }
            if (nud_num_Found.Value == 0)
            {
                str_msg = str_msg + "Invalid Number found. Please enter a number of defects found" + Environment.NewLine;
            }

            if (str_msg.Length > 0)
            {
                DLR_MessageBox = MessageBox.Show(str_msg, "Process Factory - QC", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (DLR_MessageBox != DialogResult.OK)
            {
                switch (btn_Add.Text)
                {
                    case "&Add":
                        int_result = FruPak.PF.Data.AccessLayer.PF_Defect_Results.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Defect_Results"), int_Work_Order_Id, int_Defect_Id, Convert.ToDecimal(nud_num_Found.Value.ToString()), txt_comments.Text, int_Current_User_Id);
                        break;
                    case "&Update":
                        int_result = FruPak.PF.Data.AccessLayer.PF_Defect_Results.Update(int_DVG_Row_id, int_Work_Order_Id, int_Defect_Id, Convert.ToDecimal(nud_num_Found.Value.ToString()), txt_comments.Text, int_Current_User_Id);
                        break;
                }
            }
            if (int_result > 0)
            {
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                lbl_message.Text = "Defect has been saved";
                Reset();
            }
            else
            {
                lbl_message.ForeColor = System.Drawing.Color.Red;
                lbl_message.Text = "Defect was NOT been saved";
            }
            populate_datagridview();
        }
        private static int int_DVG_Row_id = 0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult DLR_MessageBox = new DialogResult();
            int int_result = 0;

            //delete
            if (e.ColumnIndex == 8)
            {
                string str_msg;

                str_msg = "You are about to Delete Defect  ";
                str_msg = str_msg + dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                str_msg = str_msg + " from Work Order ";
                str_msg = str_msg + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + Environment.NewLine;
                str_msg = str_msg + "Do you want to continue?";
                DLR_MessageBox = MessageBox.Show(str_msg, "Process Factory - Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (DLR_MessageBox == DialogResult.Yes)
                {
                    int_result = FruPak.PF.Data.AccessLayer.PF_Defect_Results.Delete(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                }

                if (int_result > 0)
                {
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    lbl_message.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString() + " has been Removed from the Work Order";
                }
                else
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString() + " failed to be removed from the Work Order";
                }
                populate_datagridview();
            }
            //edit
            else if (e.ColumnIndex == 9)
            {
                int_DVG_Row_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                cmb_Defect.SelectedItem = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString() + " - " + dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                nud_num_Found.Value = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());
                txt_comments.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                btn_Add.Text = "&Update";
            }

        }
        private void btn_reset_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void Reset()
        {
            nud_num_Found.Value = 0;
            cmb_Defect.SelectedIndex = 0;
            txt_comments.ResetText();
            btn_Add.Text = "&Add";
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
        private void QC_KeyDown(object sender, KeyEventArgs e)
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
