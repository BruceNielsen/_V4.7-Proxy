using NLog;
using System;
using System.Data;
using System.Windows.Forms;

namespace FruPak.PF.Accounts
{
    public partial class Intended_Orders : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static int int_Current_User_Id = 0;
        private static bool bol_write_access;

        public Intended_Orders(int int_C_User_id, bool bol_w_a)
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
            //    this.Text = "FruPak Process Factory - " + this.Text;
            //}

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
            DataSet ds = null;
            ds = FruPak.PF.Data.AccessLayer.CM_Fruit_Type.Get_Info();
            cmb_Fruit_Type.DataSource = ds.Tables[0];
            cmb_Fruit_Type.DisplayMember = "Description";
            cmb_Fruit_Type.ValueMember = "FruitType_Id";
            cmb_Fruit_Type.Text = null;
            ds.Dispose();

            cmb_Material_Num.DataSource = null;
            ds = FruPak.PF.Data.AccessLayer.CM_Material.Get_For_Combo_For_Customer();
            cmb_Material_Num.DataSource = ds.Tables[0];
            cmb_Material_Num.DisplayMember = "Combined";
            cmb_Material_Num.ValueMember = "Material_Id";
            cmb_Material_Num.Text = null;
            ds.Dispose();
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
            var col8 = new DataGridViewTextBoxColumn();
            var col9 = new DataGridViewTextBoxColumn();
            var col10 = new DataGridViewTextBoxColumn();

            col0.HeaderText = "Id";
            col0.Name = "Id";
            col0.ReadOnly = true;
            col0.Visible = false;

            col1.HeaderText = "Customer Id";
            col1.Name = "Customer_Id";
            col1.ReadOnly = true;
            col1.Visible = false;

            col2.HeaderText = "Customer";
            col2.Name = "Customer";
            col2.ReadOnly = true;

            col3.HeaderText = "Material Id";
            col3.Name = "Material_Id";
            col3.ReadOnly = true;
            col3.Visible = false;

            col4.HeaderText = "Material";
            col4.Name = "Material";
            col4.ReadOnly = true;
            col4.Width = 200;

            col5.HeaderText = "Quantity";
            col5.Name = "Quantity";
            col5.ReadOnly = true;

            col6.HeaderText = "Price";
            col6.Name = "Price";
            col6.ReadOnly = true;

            col10.HeaderText = "Sold";
            col10.Name = "Sold";
            col10.ReadOnly = true;

            col7.HeaderText = "Order";
            col7.Name = "custoemr_Order";
            col7.ReadOnly = true;

            col8.HeaderText = "Comments";
            col8.Name = "Comments";
            col8.Width = 200;
            col8.ReadOnly = true;

            col9.HeaderText = "Active";
            col9.Name = "Active";
            col9.ReadOnly = true;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2, col3, col4, col5, col6, col10, col7, col8, col9 });

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

            DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Orders_Intended.Get_info_translated();
            DataRow dr_Get_Info;

            for (int i = 0; i < ds_Get_Info.Tables[0].Rows.Count; i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                dataGridView1.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_Cell0 = new DataGridViewTextBoxCell();
                DGVC_Cell0.Value = dr_Get_Info["OrderIntent_Id"].ToString();
                dataGridView1.Rows[i].Cells["Id"] = DGVC_Cell0;

                DataGridViewCell DGVC_Cell1 = new DataGridViewTextBoxCell();
                DGVC_Cell1.Value = dr_Get_Info["Customer_Id"].ToString();
                dataGridView1.Rows[i].Cells["Customer_Id"] = DGVC_Cell1;

                DataGridViewCell DGVC_Cell2 = new DataGridViewTextBoxCell();
                DGVC_Cell2.Value = dr_Get_Info["Name"].ToString();
                dataGridView1.Rows[i].Cells["Customer"] = DGVC_Cell2;

                DataGridViewCell DGVC_Cell3 = new DataGridViewTextBoxCell();
                DGVC_Cell3.Value = dr_Get_Info["Material_Id"].ToString();
                dataGridView1.Rows[i].Cells["Material_Id"] = DGVC_Cell3;

                DataGridViewCell DGVC_Cell4 = new DataGridViewTextBoxCell();
                DGVC_Cell4.Value = dr_Get_Info["Material"].ToString();
                dataGridView1.Rows[i].Cells["Material"] = DGVC_Cell4;

                DataGridViewCell DGVC_Cell5 = new DataGridViewTextBoxCell();
                DGVC_Cell5.Value = dr_Get_Info["Quantity"].ToString();
                dataGridView1.Rows[i].Cells["Quantity"] = DGVC_Cell5;

                DataGridViewCell DGVC_Cell6 = new DataGridViewTextBoxCell();
                DGVC_Cell6.Value = dr_Get_Info["Unit_Price"].ToString();
                dataGridView1.Rows[i].Cells["Price"] = DGVC_Cell6;

                DataGridViewCell DGVC_Cell7 = new DataGridViewTextBoxCell();
                DGVC_Cell7.Value = dr_Get_Info["Customer_Order"].ToString();
                dataGridView1.Rows[i].Cells["custoemr_Order"] = DGVC_Cell7;

                DataGridViewCell DGVC_Cell8 = new DataGridViewTextBoxCell();
                DGVC_Cell8.Value = dr_Get_Info["Comments"].ToString();
                dataGridView1.Rows[i].Cells["Comments"] = DGVC_Cell8;

                DataGridViewCell DGVC_Cell9 = new DataGridViewTextBoxCell();
                DGVC_Cell9.Value = dr_Get_Info["PF_Active_Ind"].ToString();
                dataGridView1.Rows[i].Cells["Active"] = DGVC_Cell9;

                DataGridViewCell DGVC_Cell10 = new DataGridViewTextBoxCell();
                DGVC_Cell10.Value = dr_Get_Info["Total_Sold"].ToString();
                dataGridView1.Rows[i].Cells["Sold"] = DGVC_Cell10;

                Column_resize();
                ds_Get_Info.Dispose();
            }
        }

        private void Column_resize()
        {
            dataGridView1.AutoResizeColumn(0, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(1, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(2, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(3, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(5, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(6, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(7, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(9, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(10, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(11, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(12, DataGridViewAutoSizeColumnMode.AllCells);
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            customer1.Customer_Id = 0;
            cmb_Material_Num.Text = null;
            nud_Quantity.Value = 0;
            nud_Unit_Price.Value = 0;
            txt_Comments.ResetText();
            txt_Customer_Order.ResetText();
            ckb_Active.Checked = true;
            btn_Add.Text = "&Add";
        }

        private void Intended_Orders_Load(object sender, EventArgs e)
        {
            populate_combobox();
            AddColumnsProgrammatically();
            Column_resize();
            populate_datagridview();
        }

        private void cmb_Fruit_Type_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cmb_Material_Num.DataSource = null;
            DataSet ds = FruPak.PF.Data.AccessLayer.CM_Material.Get_For_Combo_by_fruit(Convert.ToInt32(cmb_Fruit_Type.SelectedValue.ToString()));
            cmb_Material_Num.DataSource = ds.Tables[0];
            cmb_Material_Num.DisplayMember = "Combined";
            cmb_Material_Num.ValueMember = "Material_Id";
            cmb_Material_Num.Text = null;
            ds.Dispose();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            string str_msg = "";
            DialogResult DLR_Message = new DialogResult();
            int int_result = 0;

            if (customer1.Customer_Id == 0)
            {
                str_msg = str_msg + "Invalid Customer. PLease select a valid Customer from the dropdown list." + Environment.NewLine;
            }
            if (cmb_Material_Num.SelectedIndex == -1)
            {
                str_msg = str_msg + "Invalid Material Number. Please select a valid Material Number from the dropdown list." + Environment.NewLine;
            }
            if (nud_Quantity.Value == 0)
            {
                str_msg = str_msg + "Invalid Quantity. Please enter a valid number of Kilo's." + Environment.NewLine;
            }
            if (nud_Unit_Price.Value == 0)
            {
                str_msg = str_msg + "Invalid Unit Price. Please enter a valid unit price per Kilo." + Environment.NewLine;
            }
            if (str_msg.Length > 0)
            {
                DLR_Message = MessageBox.Show(str_msg, "Process Factory - Intended Orders", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (DLR_Message != System.Windows.Forms.DialogResult.OK)
            {
                switch (btn_Add.Text)
                {
                    case "&Add":
                        int_result = FruPak.PF.Data.AccessLayer.PF_Orders_Intended.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Orders_Intended"), customer1.Customer_Id,
                                                                                          Convert.ToInt32(cmb_Material_Num.SelectedValue.ToString()), nud_Quantity.Value, nud_Unit_Price.Value,
                                                                                          txt_Comments.Text, txt_Customer_Order.Text, ckb_Active.Checked, int_Current_User_Id);
                        break;

                    case "&Update":
                        int_result = FruPak.PF.Data.AccessLayer.PF_Orders_Intended.Update(int_dgv_Id, customer1.Customer_Id, Convert.ToInt32(cmb_Material_Num.SelectedValue.ToString()), nud_Quantity.Value, nud_Unit_Price.Value,
                                                                                          txt_Comments.Text, txt_Customer_Order.Text, ckb_Active.Checked, int_Current_User_Id);
                        break;
                }
            }
            if (int_result > 0)
            {
                lbl_message.Text = "Order for " + customer1.Customer_Name + " has been loaded/updated";
                lbl_message.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                lbl_message.Text = "Order for " + customer1.Customer_Name + " Failed to be loaded/updated";
                lbl_message.ForeColor = System.Drawing.Color.Red;
            }
            populate_datagridview();
        }

        private static int int_dgv_Id = 0;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int int_result = 0;

            #region ----------- Delete ---------

            if (e.ColumnIndex == 11)
            {
                int_result = FruPak.PF.Common.Code.General.Delete_Record("PF_Orders_Intended", dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells["Customer"].Value.ToString());

                if (int_result > 0)
                {
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    lbl_message.Text = "Intended Order has been deleted";
                }
                else
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = "Intended Order failed to delete";
                }
                populate_datagridview();
            }

            #endregion ----------- Delete ---------

            #region -------- edit -------

            else if (e.ColumnIndex == 12)
            {
                int_dgv_Id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString());
                customer1.Customer_Id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Customer_Id"].Value.ToString());
                cmb_Material_Num.SelectedValue = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Material_Id"].Value.ToString());

                //get fruit type ID
                DataSet ds = FruPak.PF.Data.AccessLayer.CM_Material.Get_Info(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Material_Id"].Value.ToString()));
                DataRow dr;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dr = ds.Tables[0].Rows[i];
                    cmb_Fruit_Type.SelectedValue = Convert.ToInt32(dr["FruitType_Id"].ToString());
                }
                ds.Dispose();

                nud_Quantity.Value = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["Quantity"].Value.ToString());
                nud_Unit_Price.Value = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["Price"].Value.ToString());
                txt_Comments.Text = dataGridView1.Rows[e.RowIndex].Cells["Comments"].Value.ToString();
                txt_Customer_Order.Text = dataGridView1.Rows[e.RowIndex].Cells["custoemr_Order"].Value.ToString();
                ckb_Active.Checked = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells["Active"].Value.ToString());
                btn_Add.Text = "&Update";
            }

            #endregion -------- edit -------
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
        private void Intended_Orders_KeyDown(object sender, KeyEventArgs e)
        {
            // Note that the KeyPreview property must be set to true for this to work
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        #endregion Methods to log UI events to the CSV file. BN 29/01/2015

        private void Intended_Orders_FormClosing(object sender, FormClosingEventArgs e)
        {
            logger.Log(LogLevel.Info, "---------------------[ FruPak.PF.Accounts.Intended_Orders Form Closing]--- :" + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToLongTimeString());
        }
    }
}