using NLog;
using System;
using System.Data;
using System.Windows.Forms;

namespace PF.Accounts
{
    public partial class Sales_Rates : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static string str_table = "";
        private static int int_Current_User_Id = 0;
        private static bool bol_write_access;
        private static int int_CustSalesRate_Id = 0;

        public Sales_Rates(string str_type, int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();

            str_table = str_type;
            int_Current_User_Id = int_C_User_id;
            //restrict access
            bol_write_access = bol_w_a;
            btn_Add.Enabled = bol_w_a;

            switch (str_table)
            {
                case "PF_A_Customer_Sales_Rates":
                    lbl_price.Text = "Price:";
                    this.Text += " (Sales Price)";
                    break;

                case "PF_A_Customer_Sales_Discount":
                    lbl_price.Text = "Discount:";
                    this.Text += " (Sales Discount)";
                    break;
            }

            //check if testing or not

            //if (PF.Global.Global.bol_Testing == true)
            //{
            //    this.Text = "FP Process Factory - " + this.Text + " - Test Environment";
            //}
            //else
            //{
            //    this.Text = "FP Process Factory - " + this.Text;
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
                else if (c.GetType() == typeof(PF.Utils.UserControls.Customer))
                {
                    PF.Utils.UserControls.Customer cust = (PF.Utils.UserControls.Customer)c;
                    cust.CustomerChanged += new EventHandler(this.CustomerControl_CustomerChanged);
                }
            }

            #endregion Log any interesting events from the UI to the CSV log file
        }

        public void populate_combobox()
        {
            DataSet ds_Get_Info = PF.Data.AccessLayer.CM_Material.Get_For_Combo_For_Customer();
            cmb_Material.DataSource = ds_Get_Info.Tables[0];
            cmb_Material.DisplayMember = "Combined";
            cmb_Material.ValueMember = "Material_Id";
            cmb_Material.Text = null;
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

            col5.HeaderText = "Value";
            col5.Name = "Value";
            col5.ReadOnly = true;

            col6.HeaderText = "Active";
            col6.Name = "Active";
            col6.ReadOnly = true;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2, col3, col4, col5, col6 });

            DataGridViewImageColumn img_delete = new DataGridViewImageColumn();
            dataGridView1.Columns.Add(img_delete);
            img_delete.HeaderText = "Delete";
            img_delete.Name = "Delete";
            img_delete.Image = PF.Global.Properties.Resources.delete;
            img_delete.ReadOnly = true;
            img_delete.Visible = bol_write_access;

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

            DataSet ds_Get_Info = null;
            switch (str_table)
            {
                case "PF_A_Customer_Sales_Rates":
                    ds_Get_Info = PF.Data.AccessLayer.PF_A_Customer_Sales_Rates.Get_Info_Translated();
                    break;

                case "PF_A_Customer_Sales_Discount":
                    ds_Get_Info = PF.Data.AccessLayer.PF_A_Customer_Sales_Discount.Get_Info_Translated();
                    break;
            }

            DataRow dr_Get_Info;

            for (int i = 0; i < ds_Get_Info.Tables[0].Rows.Count; i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                dataGridView1.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_Cell0 = new DataGridViewTextBoxCell();
                switch (str_table)
                {
                    case "PF_A_Customer_Sales_Rates":
                        DGVC_Cell0.Value = dr_Get_Info["CustSalesRate_Id"].ToString();
                        break;

                    case "PF_A_Customer_Sales_Discount":
                        DGVC_Cell0.Value = dr_Get_Info["CustSalesDisc_Id"].ToString();
                        break;
                }
                dataGridView1.Rows[i].Cells["Id"] = DGVC_Cell0;

                DataGridViewCell DGVC_Cell1 = new DataGridViewTextBoxCell();
                DGVC_Cell1.Value = dr_Get_Info["Customer_Id"].ToString();
                dataGridView1.Rows[i].Cells["Customer_Id"] = DGVC_Cell1;

                DataGridViewCell DGVC_Cell2 = new DataGridViewTextBoxCell();
                DGVC_Cell2.Value = dr_Get_Info["Customer"].ToString();
                dataGridView1.Rows[i].Cells["Customer"] = DGVC_Cell2;

                DataGridViewCell DGVC_Cell3 = new DataGridViewTextBoxCell();
                DGVC_Cell3.Value = dr_Get_Info["Material_Id"].ToString();
                dataGridView1.Rows[i].Cells["Material_Id"] = DGVC_Cell3;

                DataGridViewCell DGVC_Cell4 = new DataGridViewTextBoxCell();
                DGVC_Cell4.Value = dr_Get_Info["Combined"].ToString();
                dataGridView1.Rows[i].Cells["Material"] = DGVC_Cell4;

                DataGridViewCell DGVC_Cell5 = new DataGridViewTextBoxCell();
                DGVC_Cell5.Value = dr_Get_Info["Value"].ToString();
                dataGridView1.Rows[i].Cells["Value"] = DGVC_Cell5;

                DataGridViewCell DGVC_Cell6 = new DataGridViewTextBoxCell();
                DGVC_Cell6.Value = dr_Get_Info["PF_Active_Ind"].ToString();
                dataGridView1.Rows[i].Cells["Active"] = DGVC_Cell6;

                Column_resize();
                ds_Get_Info.Dispose();
            }
        }

        private void SizeAllColumns(Object sender, EventArgs e)
        {
            Column_resize();
        }

        private void Column_resize()
        {
            //dataGridView1.AutoResizeColumn(0, DataGridViewAutoSizeColumnMode.AllCells);
            //dataGridView1.AutoResizeColumn(1, DataGridViewAutoSizeColumnMode.AllCells);
            //dataGridView1.AutoResizeColumn(2, DataGridViewAutoSizeColumnMode.AllCells);
            //dataGridView1.AutoResizeColumn(3, DataGridViewAutoSizeColumnMode.AllCells);

            //dataGridView1.AutoResizeColumn(5, DataGridViewAutoSizeColumnMode.AllCells);
            //dataGridView1.AutoResizeColumn(6, DataGridViewAutoSizeColumnMode.AllCells);
            //dataGridView1.AutoResizeColumn(7, DataGridViewAutoSizeColumnMode.AllCells);
            //dataGridView1.AutoResizeColumn(8, DataGridViewAutoSizeColumnMode.AllCells);

            dataGridView1.AutoResizeColumn(8, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(7, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(6, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(5, DataGridViewAutoSizeColumnMode.AllCells);

            dataGridView1.AutoResizeColumn(4, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(3, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(2, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(1, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(0, DataGridViewAutoSizeColumnMode.AllCells);
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Add_btn();
            Cursor.Current = Cursors.Default;

        }

        private void Add_btn()
        {
            DialogResult DLR_MessageBox = new DialogResult();
            string str_msg = "";
            int int_result = 0;

            labelMissingRatesMaterial.Text = "";    // BN - 4/12/2015

            if (customer1.Customer_Id <= 0)
            {
                str_msg = str_msg + "Invalid Customer. Please select a customer from the dropdown list." + Environment.NewLine;
            }
            if (cmb_Material.SelectedIndex == -1)
            {
                str_msg = str_msg + "Invalid Product. Please select a product from the dropdown list." + Environment.NewLine;
            }
            if (txt_Price.TextLength == 0)
            {
                switch (str_table)
                {
                    case "PF_A_Customer_Sales_Rates":
                        str_msg = str_msg + "Invalid Price. Please enter a valid price." + Environment.NewLine;
                        break;

                    case "PF_A_Customer_Sales_Discount":
                        str_msg = str_msg + "Invalid Discount. Please enter a valid Discount." + Environment.NewLine;
                        break;
                }
            }
            else
            {
                try
                {
                    Convert.ToDecimal(txt_Price.Text);
                }
                catch
                {
                    switch (str_table)
                    {
                        case "PF_A_Customer_Sales_Rates":
                            str_msg = str_msg + "Invalid Price. A price must be numeric. Please Re-enter a valid Price." + Environment.NewLine;
                            break;

                        case "PF_A_Customer_Sales_Discount":
                            str_msg = str_msg + "Invalid Discount. A Discount must be numeric. Please Re-enter a valid Discount." + Environment.NewLine;
                            break;
                    }
                }
            }
            if (str_msg.Length > 0)
            {
                DLR_MessageBox = MessageBox.Show(str_msg, "Process Factory - Accounting(Sale Price/Disc)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (DLR_MessageBox != System.Windows.Forms.DialogResult.OK)
            {
                switch (btn_Add.Text)
                {
                    case "&Add":
                        switch (str_table)
                        {
                            case "PF_A_Customer_Sales_Rates":
                                int_result = PF.Data.AccessLayer.PF_A_Customer_Sales_Rates.Insert(PF.Common.Code.General.int_max_user_id("PF_A_Customer_Sales_Rates"), Convert.ToInt32(cmb_Material.SelectedValue.ToString()),
                                                                                                      customer1.Customer_Id, Convert.ToDecimal(txt_Price.Text), Convert.ToBoolean(ckb_Active.Checked), int_Current_User_Id);
                                break;

                            case "PF_A_Customer_Sales_Discount":
                                int_result = PF.Data.AccessLayer.PF_A_Customer_Sales_Discount.Insert(PF.Common.Code.General.int_max_user_id("PF_A_Customer_Sales_Discount"), Convert.ToInt32(cmb_Material.SelectedValue.ToString()),
                                                                                                     customer1.Customer_Id, Convert.ToDecimal(txt_Price.Text), Convert.ToBoolean(ckb_Active.Checked), int_Current_User_Id);
                                break;
                        }
                        break;

                    case "&Update":
                        switch (str_table)
                        {
                            case "PF_A_Customer_Sales_Rates":
                                int_result = PF.Data.AccessLayer.PF_A_Customer_Sales_Rates.Update(int_CustSalesRate_Id, Convert.ToInt32(cmb_Material.SelectedValue.ToString()),
                                                                                              customer1.Customer_Id, Convert.ToDecimal(txt_Price.Text), Convert.ToBoolean(ckb_Active.Checked), int_Current_User_Id);
                                break;

                            case "PF_A_Customer_Sales_Discount":
                                int_result = PF.Data.AccessLayer.PF_A_Customer_Sales_Discount.Update(int_CustSalesRate_Id, Convert.ToInt32(cmb_Material.SelectedValue.ToString()),
                                                                                              customer1.Customer_Id, Convert.ToDecimal(txt_Price.Text), Convert.ToBoolean(ckb_Active.Checked), int_Current_User_Id);
                                break;
                        }
                        break;
                }
            }
            if (int_result > 0)
            {
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                lbl_message.Text = "Value has been saved";

                Reset();
            }
            else
            {
                lbl_message.ForeColor = System.Drawing.Color.Red;
                lbl_message.Text = "Value has NOT been saved";
            }
            populate_datagridview();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Delete
            if (e.ColumnIndex == 7)
            {
                PF.Data.AccessLayer.PF_A_Customer_Sales_Rates.Update_Active(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString()), false, int_Current_User_Id);
                populate_datagridview();

                int int_result = PF.Common.Code.General.Delete_Record(str_table, dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(),
                                    dataGridView1.Rows[e.RowIndex].Cells["Customer"].Value.ToString() + " - " + dataGridView1.Rows[e.RowIndex].Cells["Material"].Value.ToString());

                if (int_result >= 0)
                {
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    lbl_message.Text = dataGridView1.Rows[e.RowIndex].Cells["Customer"].Value.ToString() + " - " + dataGridView1.Rows[e.RowIndex].Cells["Material"].Value.ToString() + " has been deleted";
                }
                else
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = dataGridView1.Rows[e.RowIndex].Cells["Customer"].Value.ToString() + " - " + dataGridView1.Rows[e.RowIndex].Cells["Material"].Value.ToString() + " failed to delete";
                }
                populate_datagridview();
            }
            //Edit
            else if (e.ColumnIndex == 8)
            {
                int_CustSalesRate_Id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString());
                customer1.Customer_Id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Customer_Id"].Value.ToString());
                cmb_Material.SelectedValue = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Material_Id"].Value.ToString());
                txt_Price.Text = dataGridView1.Rows[e.RowIndex].Cells["Value"].Value.ToString();
                ckb_Active.Checked = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells["Active"].Value.ToString());
                btn_Add.Text = "&Update";
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            customer1.Customer_Id = 0;
            cmb_Material.Text = null;
            txt_Price.ResetText();
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
            PF.Utils.UserControls.Customer cust = (PF.Utils.UserControls.Customer)sender;
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
        private void Sales_Rates_KeyDown(object sender, KeyEventArgs e)
        {
            // Note that the KeyPreview property must be set to true for this to work
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        #endregion Methods to log UI events to the CSV file. BN 29/01/2015

        private void button1_Click(object sender, EventArgs e)
        {
            throw new ArgumentException("The parameter was invalid");
        }

        public void buttonPasteMissingRateInfo_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            labelMissingRatesMaterial.Text = "Searching...";

            string searchString = Clipboard.GetText();
            bool found = false;
            int counter = 0;

            foreach (DataRowView drv in cmb_Material.Items)
            {
                DataRow dr = drv.Row;
                Console.WriteLine(dr[1].ToString());
                if (dr[1].ToString().Contains(searchString))
                {
                    found = true;
                    break;
                }
                counter++;
            }

            if(found == true)
            {
                cmb_Material.SelectedIndex = counter;
                labelMissingRatesMaterial.Text = "Product located. Now select the customer and enter a price.";
                //customer1.; // Can't set focus
            }

            Cursor.Current = Cursors.Default;

        }

    }
}