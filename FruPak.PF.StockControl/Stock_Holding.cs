using NLog;
using System;
using System.Data;
using System.Windows.Forms;

namespace PF.StockControl
{
    public partial class Stock_Holding : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static int int_Current_User_Id = 0;
        private static bool bol_write_access;
        private static int int_DVG_Row_id = 0;

        public Stock_Holding(int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();
            //restrict access
            bol_write_access = bol_w_a;
            btn_Add.Enabled = bol_w_a;
            //check if testing or not

            //if (PF.Global.Global.bol_Testing == true)
            //{
            //    this.Text = "FP Process Factory - " + this.Text + " - Test Environment";
            //}
            //else
            //{
            //    this.Text = "FP Process Factory";
            //}

            int_Current_User_Id = int_C_User_id;

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

                //else if (c.GetType() == typeof(PF.Utils.UserControls.Customer))
                //{
                //    PF.Utils.UserControls.Customer cust = (PF.Utils.UserControls.Customer)c;
                //    cust.CustomerChanged += new EventHandler(this.CustomerControl_CustomerChanged);
                //}
            }

            #endregion Log any interesting events from the UI to the CSV log file
        }

        private void populate_combobox()
        {
            DataSet ds_get_info = PF.Data.AccessLayer.PF_Stock_Item.Get_Info();
            cmb_stock_Item.DataSource = ds_get_info.Tables[0];
            cmb_stock_Item.DisplayMember = "Combined";
            cmb_stock_Item.ValueMember = "Stock_Item_Id";
            cmb_stock_Item.Text = null;
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

            col0.HeaderText = "Id";
            col0.Name = "Id";
            col0.ReadOnly = true;
            col0.Visible = false;

            col1.HeaderText = "Stock_Item_Id";
            col1.Name = "Stock_Item_Id";
            col1.ReadOnly = true;
            col1.Visible = false;

            col2.HeaderText = "Stock_Item";
            col2.Name = "Stock_Item";
            col2.ReadOnly = true;

            col3.HeaderText = "Season";
            col3.Name = "Season";
            col3.ReadOnly = true;

            col4.HeaderText = "Invoice Number";
            col4.Name = "Invoice";
            col4.ReadOnly = true;

            col5.HeaderText = "Arrival Date";
            col5.Name = "Arrival_Date";
            col5.ReadOnly = true;

            col6.HeaderText = "Quanity";
            col6.Name = "Quantity";
            col6.ReadOnly = true;

            col7.HeaderText = "Mod_Date";
            col7.Name = "Mod_Date";
            col7.ReadOnly = true;
            col7.Visible = false;

            col8.HeaderText = "Mod_User_id";
            col8.Name = "Mod_User_id";
            col8.ReadOnly = true;
            col8.Visible = false;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2, col3, col4, col5, col6, col7, col8 });

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
            DataSet ds_Get_Info = null;
            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();

            ds_Get_Info = PF.Data.AccessLayer.PF_Stock_Holding.Get_Info_Translated();

            DataRow dr_Get_Info;
            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                dataGridView1.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_User_Id = new DataGridViewTextBoxCell();
                DGVC_User_Id.Value = dr_Get_Info["Stock_Hold_Id"].ToString();
                dataGridView1.Rows[i].Cells["Id"] = DGVC_User_Id;

                DataGridViewCell DGVC_Cell2 = new DataGridViewTextBoxCell();
                DGVC_Cell2.Value = dr_Get_Info["Stock_Item_Id"].ToString();
                dataGridView1.Rows[i].Cells["Stock_Item_Id"] = DGVC_Cell2;

                DataGridViewCell DGVC_Cell3 = new DataGridViewTextBoxCell();
                DGVC_Cell3.Value = dr_Get_Info["SI_Code"].ToString() + " - " + dr_Get_Info["SI_Description"].ToString();
                dataGridView1.Rows[i].Cells["Stock_Item"] = DGVC_Cell3;

                DataGridViewCell DGVC_Cell4 = new DataGridViewTextBoxCell();
                DGVC_Cell4.Value = dr_Get_Info["Season"].ToString();
                dataGridView1.Rows[i].Cells["Season"] = DGVC_Cell4;

                DataGridViewCell DGVC_Cell5 = new DataGridViewTextBoxCell();
                DGVC_Cell5.Value = dr_Get_Info["Invoice_Num"].ToString();
                dataGridView1.Rows[i].Cells["Invoice"] = DGVC_Cell5;

                DataGridViewCell DGVC_Cell6 = new DataGridViewTextBoxCell();
                DGVC_Cell6.Value = dr_Get_Info["Arrival_Date"].ToString();
                dataGridView1.Rows[i].Cells["Arrival_Date"] = DGVC_Cell6;

                DataGridViewCell DGVC_Cell7 = new DataGridViewTextBoxCell();
                DGVC_Cell7.Value = dr_Get_Info["Quantity"].ToString();
                dataGridView1.Rows[i].Cells["Quantity"] = DGVC_Cell7;

                DataGridViewCell DGVC_Cell8 = new DataGridViewTextBoxCell();
                DGVC_Cell8.Value = dr_Get_Info["Mod_date"].ToString();
                dataGridView1.Rows[i].Cells["Mod_Date"] = DGVC_Cell8;

                DataGridViewCell DGVC_Cell9 = new DataGridViewTextBoxCell();
                DGVC_Cell9.Value = dr_Get_Info["Mod_User_Id"].ToString();
                dataGridView1.Rows[i].Cells["Mod_User_id"] = DGVC_Cell9;

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
            // TODO: This line of code loads data into the 'moneyDataSet.List_TS_Work' table. You can move, or remove it, as needed.
            dataGridView1.AutoResizeColumn(0, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(1, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(2, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(3, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(4, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(5, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(6, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(7, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(8, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(9, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(10, DataGridViewAutoSizeColumnMode.AllCells);
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            nud_quantity.Value = 0;
            txt_Inv_Num.ResetText();
            cmb_stock_Item.Text = null;
            btn_Add.Text = "&Add";
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            DialogResult DLR_MessageBox = new DialogResult();
            string str_msg = "";
            int int_result = 0;

            if (cmb_stock_Item.SelectedIndex == -1)
            {
                str_msg = str_msg + "Invalid Consumable Item. PLease select a valid item from the drop down list." + Environment.NewLine;
            }
            if (nud_quantity.Value == 0)
            {
                str_msg = str_msg + "Invalid Value. Please enter a valid Quantity." + Environment.NewLine;
            }

            if (str_msg.Length > 0)
            {
                DLR_MessageBox = MessageBox.Show(str_msg, "Process Factory - Consumable(Holding)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (DLR_MessageBox != DialogResult.OK)
            {
                switch (btn_Add.Text)
                {
                    case "&Add":
                        int_result = PF.Data.AccessLayer.PF_Stock_Holding.Insert(PF.Common.Code.General.int_max_user_id("PF_Stock_Holding"), Convert.ToInt32(cmb_stock_Item.SelectedValue.ToString()), PF.Common.Code.General.Get_Season(),
                                        txt_Inv_Num.Text, dtp_Arrival_Date.Value.Year.ToString() + "/" + dtp_Arrival_Date.Value.Month.ToString() + "/" + dtp_Arrival_Date.Value.Day.ToString(), Convert.ToInt32(nud_quantity.Value), int_Current_User_Id);
                        if (int_result > 0)
                        {
                            PF.Data.AccessLayer.PF_Stock_Item.Update_Email(Convert.ToInt32(cmb_stock_Item.SelectedValue.ToString()), false, int_Current_User_Id);
                        }
                        lbl_message.Text = "Consumable Item has been added.";
                        break;

                    case "&Update":
                        int_result = PF.Data.AccessLayer.PF_Stock_Holding.Update(int_DVG_Row_id, Convert.ToInt32(cmb_stock_Item.SelectedValue.ToString()),
                                        txt_Inv_Num.Text, dtp_Arrival_Date.Value.Year.ToString() + "/" + dtp_Arrival_Date.Value.Month.ToString() + "/" + dtp_Arrival_Date.Value.Day.ToString(), Convert.ToInt32(nud_quantity.Value), int_Current_User_Id);
                        lbl_message.Text = "Consumable Item has been Updated.";
                        break;
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
                lbl_message.Text = "Consumable Item failed to be added/Updated.";
                lbl_message.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Delete
            if (e.ColumnIndex == 9)
            {
                int int_result = PF.Common.Code.General.Delete_Record("PF_Stock_Holding", dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells["Stock_Item"].Value.ToString());
                if (int_result > 0)
                {
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    lbl_message.Text = dataGridView1.Rows[e.RowIndex].Cells["Stock_Item"].Value.ToString() + " has been deleted";
                }
                else
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = dataGridView1.Rows[e.RowIndex].Cells["Stock_Item"].Value.ToString() + " failed to delete";
                }
                populate_datagridview();
            }
            //Edit
            else if (e.ColumnIndex == 10)
            {
                int_DVG_Row_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString());
                cmb_stock_Item.Text = dataGridView1.Rows[e.RowIndex].Cells["Stock_Item"].Value.ToString();
                dtp_Arrival_Date.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["Arrival_Date"].Value.ToString());
                txt_Inv_Num.Text = dataGridView1.Rows[e.RowIndex].Cells["Invoice"].Value.ToString();
                nud_quantity.Value = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["Quantity"].Value.ToString());
                btn_Add.Text = "&Update";
            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            int int_cmb_stock_Item = 0;
            if (cmb_stock_Item.SelectedValue != null)
            {
                int_cmb_stock_Item = Convert.ToInt32(cmb_stock_Item.SelectedValue.ToString());
                populate_combobox();
                cmb_stock_Item.SelectedValue = int_cmb_stock_Item;
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

        //private void CustomerControl_CustomerChanged(object sender, EventArgs e)
        //{
        //    PF.Utils.UserControls.Customer cust = (PF.Utils.UserControls.Customer)sender;
        //    logger.Log(LogLevel.Info, DecorateString(cust.Name, cust.Customer_Name, "TextChanged"));
        //}

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
        private void Stock_Holding_KeyDown(object sender, KeyEventArgs e)
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