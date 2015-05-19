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
    public partial class WO_Tests : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();     

        private static bool bol_write_access;
        private static int int_Current_User_Id = 0;
        private static int int_Work_Order_Id = 0;

        public WO_Tests(int int_wo_Id, int int_C_User_id, bool bol_w_a)
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

            //populate Batch Num Display
            batchNum1.Work_Order_Id = int_wo_Id;
            batchNum1.populate();

            //restrict access
            bol_write_access = bol_w_a;
            btn_Add.Enabled = bol_w_a;

            populate_Combobox();
            AddColumnsProgrammatically();
            populate_PreOP();
            populate_datagrid();

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
        public void populate_PreOP()
        {
            DataSet ds_Get_Info;
            ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Batch_Test.Get_Info(int_Work_Order_Id);

            if (Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()) == 0)
            {
                ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Product_Tests_Relationship.Get_Info(woDisplay1.Product_Id, "P");
                DataRow dr_Get_Info;

                for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
                {
                    dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                    FruPak.PF.Data.AccessLayer.PF_Batch_Test.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Batch_Test"), 0, dtp_start.Value.ToString("HH:mm:ss"),
                                                            int_Work_Order_Id, Convert.ToInt32(dr_Get_Info["Test_Id"].ToString()), false, null, null, int_Current_User_Id);
                }
                ds_Get_Info.Dispose();
            }
        }
        public void populate_Combobox()
        {
            DataSet ds_Get_Info;
            //Test List
            ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Product_Tests_Relationship.Get_Info(woDisplay1.Product_Id);
            cmb_Test.DataSource = ds_Get_Info.Tables[0];
            cmb_Test.DisplayMember = "Test";
            cmb_Test.ValueMember = "Test_Id";
            cmb_Test.Text = null;
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

            col0.HeaderText = "Batch_Id";
            col0.Name = "Batch_Id";
            col0.ReadOnly = true;
            col0.Visible = false;

            col1.HeaderText = "Batch";
            col1.Name = "Batch_Num";
            col1.ReadOnly = true;
            col1.Visible = true;

            col2.HeaderText = "Time";
            col2.Name = "Time";
            col2.ReadOnly = true;

            col3.HeaderText = "Test_Id";
            col3.Name = "Test_Id";
            col3.ReadOnly = true;
            col3.Visible = false;

            col4.HeaderText = "Test";
            col4.Name = "Test";
            col4.ReadOnly = true;

            col5.HeaderText = "Passed";
            col5.Name = "Test_Passed";
            col5.ReadOnly = true;

            col6.HeaderText = "Result";
            col6.Name = "Result";
            col6.ReadOnly = false;

            col7.HeaderText = "Comments";
            col7.Name = "Comments";
            col7.ReadOnly = false;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2, col3, col4, col5, col6, col7 });

            DataGridViewImageColumn img_delete = new DataGridViewImageColumn();
            dataGridView1.Columns.Add(img_delete);
            img_delete.HeaderText = "Delete";
            img_delete.Name = "Delete";
            img_delete.Image = FruPak.PF.Global.Properties.Resources.delete;
            img_delete.ReadOnly = true;
        }
        private void populate_datagrid()
        {
            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();

            DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Batch_Test.Get_Info(int_Work_Order_Id);
            DataRow dr_Get_Info;
            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                int i_rows = Convert.ToInt32(dataGridView1.Rows.Count.ToString());
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                dataGridView1.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_Id = new DataGridViewTextBoxCell();
                DGVC_Id.Value = dr_Get_Info["BatchTest_Id"].ToString();
                dataGridView1.Rows[i_rows].Cells[0] = DGVC_Id;

                DataGridViewCell DGVC_Batch_Num = new DataGridViewTextBoxCell();
                DGVC_Batch_Num.Value = dr_Get_Info["Batch_Num"].ToString();
                dataGridView1.Rows[i_rows].Cells[1] = DGVC_Batch_Num;

                DataGridViewCell DGVC_Time = new DataGridViewTextBoxCell();
                DGVC_Time.Value = Convert.ToDateTime(dr_Get_Info["Time"].ToString()).ToString("HH:mm:ss");
                dataGridView1.Rows[i_rows].Cells[2] = DGVC_Time;

                DataGridViewCell DGVC_Test_Id = new DataGridViewTextBoxCell();
                DGVC_Test_Id.Value = dr_Get_Info["Test_Id"].ToString();
                dataGridView1.Rows[i_rows].Cells[3] = DGVC_Test_Id;

                DataSet ds_Get_Info2 = FruPak.PF.Data.AccessLayer.PF_Tests.Get_Info(Convert.ToInt32(dr_Get_Info["Test_Id"].ToString()));
                DataRow dr_Get_Info2;
                for (int j = 0; j < Convert.ToInt32(ds_Get_Info2.Tables[0].Rows.Count.ToString()); j++)
                {
                    dr_Get_Info2 = ds_Get_Info2.Tables[0].Rows[j];
                    DataGridViewCell DGVC_Test = new DataGridViewTextBoxCell();
                    DGVC_Test.Value = dr_Get_Info2["Code"].ToString();
                    dataGridView1.Rows[i_rows].Cells[4] = DGVC_Test;

                }
                ds_Get_Info2.Dispose();

                DataGridViewCell DGVC_Test_Passed = new DataGridViewTextBoxCell();
                DGVC_Test_Passed.Value = dr_Get_Info["Test_Passed"].ToString();
                dataGridView1.Rows[i_rows].Cells[5] = DGVC_Test_Passed;

                DataGridViewCell DGVC_Result = new DataGridViewTextBoxCell();
                DGVC_Result.Value = dr_Get_Info["Results"].ToString();
                dataGridView1.Rows[i_rows].Cells[6] = DGVC_Result;

                DataGridViewCell DGVC_Comments = new DataGridViewTextBoxCell();
                DGVC_Comments.Value = dr_Get_Info["Comments"].ToString();
                dataGridView1.Rows[i_rows].Cells[7] = DGVC_Comments;
            }
            ds_Get_Info.Dispose();
            ColumSize();
        }
        private void SizeAllColumns(Object sender, EventArgs e)
        {
            ColumSize();
        }
        private void ColumSize()
        {
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
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

            if (batchNum1.Batch_Num == 0)
            {
                str_msg = str_msg + "Invalid Batch Number. Please select the current Batch number from the dropdown list." + Environment.NewLine;
            }
            if (str_msg.Length != 0)
            {
                DLR_MessageBox = MessageBox.Show(str_msg, "Process Factory - Work Order(Tests)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (DLR_MessageBox != DialogResult.OK)
            {
                string str_batch = "";
                str_batch = woDisplay1.Process_Date.Substring(0, 4) + woDisplay1.Process_Date.Substring(5, 2) + woDisplay1.Process_Date.Substring(8, 2) + batchNum1.Batch_Num.ToString();

                if (cmb_Test.SelectedValue == null)
                {
                    DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Product_Tests_Relationship.Get_Info(woDisplay1.Product_Id, "B");
                    DataRow dr_Get_Info;

                    for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
                    {
                        dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                        int_result = FruPak.PF.Data.AccessLayer.PF_Batch_Test.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Batch_Test"), Convert.ToInt32(str_batch), dtp_start.Value.ToString("HH:mm:ss"),
                                                                             int_Work_Order_Id, Convert.ToInt32(dr_Get_Info["Test_Id"].ToString()), false, null, null, int_Current_User_Id);
                    }
                }
                else
                {
                    int_result = FruPak.PF.Data.AccessLayer.PF_Batch_Test.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Batch_Test"), Convert.ToInt32(str_batch), dtp_start.Value.ToString("HH:mm:ss"),
                                                                         int_Work_Order_Id, Convert.ToInt32(cmb_Test.SelectedValue.ToString()), false, null, null, int_Current_User_Id);

                }
                populate_datagrid();
            }
            if (int_result > 0)
            {
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                lbl_message.Text = "Tests have been Added to the Work Order";
                Reset();
            }
            else
            {
                lbl_message.ForeColor = System.Drawing.Color.Red;
                lbl_message.Text = "Tests has NOT been saved to the Work Order";
            }

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult DLR_Message = new DialogResult();
            int int_result = 0;


            //Pass/Fail 
            if (e.ColumnIndex == 5)
            {
                switch(Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString()))
                {
                    case true:
                        FruPak.PF.Data.AccessLayer.PF_Batch_Test.Update(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()), false, 
                                                                dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString(),
                                                                dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString());
                        break;
                    case false:
                        FruPak.PF.Data.AccessLayer.PF_Batch_Test.Update(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()), true,
                                                                dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString(),
                                                                dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString());
                        break;
                    default:
                        FruPak.PF.Data.AccessLayer.PF_Batch_Test.Update(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()), false,
                                                                dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString(),
                                                                dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString());
                        break;
                }
               
            }
            //Delete
            else if (e.ColumnIndex == 8)
            {
                string str_msg;

                str_msg = "You are about to remove the ";
                str_msg = str_msg + dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                str_msg = str_msg + " Test from Batch ";
                str_msg = str_msg + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + Environment.NewLine;
                str_msg = str_msg + "Do you want to continue?";
                DLR_Message = MessageBox.Show(str_msg, "Process Factory - Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (DLR_Message == DialogResult.Yes)
                {
                    int_result = FruPak.PF.Data.AccessLayer.PF_Batch_Test.Delete(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                }

                if (int_result > 0)
                {
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    lbl_message.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString() + " has been Removed from the Work Order";
                }
                else
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + " failed to be removed";
                }

            }
            if (e.ColumnIndex == 5 || e.ColumnIndex == 8)
            {
                populate_datagrid();
            }
        }
        private void btn_reset_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void Reset()
        {
        }
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6 )
            {
                FruPak.PF.Data.AccessLayer.PF_Batch_Test.Update(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()),
                                                        Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString()),
                                                        dataGridView1.Rows[e.RowIndex].Cells[6].EditedFormattedValue.ToString(),
                                                        dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString());
            }
            else if (e.ColumnIndex == 7)
            {
                FruPak.PF.Data.AccessLayer.PF_Batch_Test.Update(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()),
                                                        Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString()),
                                                        dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString(),
                                                        dataGridView1.Rows[e.RowIndex].Cells[7].EditedFormattedValue.ToString());
            }
        }
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
            {
                foreach (DataGridViewCell Cell in (sender as DataGridView).SelectedCells)
                {
                    Cell.Value = "";
                }
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
        private void WO_Tests_KeyDown(object sender, KeyEventArgs e)
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
