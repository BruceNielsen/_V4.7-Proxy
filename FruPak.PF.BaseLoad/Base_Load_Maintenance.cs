using NLog;
using System;
using System.Data;
using System.Windows.Forms;

namespace PF.BaseLoad
{
    public partial class Base_Load_Maintenance : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static int int_Current_User_Id = 0;
        private static bool bol_write_access;

        public Base_Load_Maintenance(int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();
            int_Current_User_Id = int_C_User_id;
            //restrict access
            bol_write_access = bol_w_a;
            btn_Add.Enabled = bol_w_a;
            //check if testing or not

            //if (PF.Global.Global.bol_Testing == true)
            //{
            //    this.Text = "FP Process Factory - " + this.Text + "Test Environment";
            //}
            //else
            //{
            //    this.Text = "FP Process Factory";
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

        private void populate_combobox()
        {
            DataSet ds = PF.Data.AccessLayer.PF_Work_Order.Get_Combo();
            cmb_Work_Order.DataSource = ds.Tables[0];
            cmb_Work_Order.DisplayMember = "Work_Order_Id";
            cmb_Work_Order.ValueMember = "Work_Order_Id";
            cmb_Work_Order.Text = null;
            ds.Dispose();
        }

        private void AddColumnsProgrammatically()
        {
            var col0 = new DataGridViewTextBoxColumn();
            var col1 = new DataGridViewTextBoxColumn();
            var col2 = new DataGridViewTextBoxColumn();
            var col3 = new DataGridViewTextBoxColumn();

            col0.HeaderText = "Id";
            col0.Name = "BaseLoad_Id";
            col0.ReadOnly = true;
            col0.Visible = false;

            col1.HeaderText = "Work Order ID";
            col1.Name = "Work_Order_Id";
            col1.ReadOnly = true;

            col2.HeaderText = "Description";
            col2.Name = "Description";
            col2.Width = 200;
            col2.ReadOnly = true;

            col3.HeaderText = "Active";
            col3.Name = "PF_Active_Ind";
            col3.ReadOnly = true;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2, col3 });

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

            DataSet ds_Get_Info = PF.Data.AccessLayer.PF_BaseLoad.Get_Info();
            DataRow dr_Get_Info;
            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                dataGridView1.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_Cell0 = new DataGridViewTextBoxCell();
                DGVC_Cell0.Value = dr_Get_Info["BaseLoad_Id"].ToString();
                dataGridView1.Rows[i].Cells["BaseLoad_Id"] = DGVC_Cell0;

                DataGridViewCell DGVC_Cell1 = new DataGridViewTextBoxCell();
                DGVC_Cell1.Value = dr_Get_Info["Work_Order_Id"].ToString();
                dataGridView1.Rows[i].Cells["Work_Order_Id"] = DGVC_Cell1;

                DataGridViewCell DGVC_Cell2 = new DataGridViewTextBoxCell();
                DGVC_Cell2.Value = dr_Get_Info["Description"].ToString();
                dataGridView1.Rows[i].Cells["Description"] = DGVC_Cell2;

                DataGridViewCell DGVC_Cell3 = new DataGridViewTextBoxCell();
                DGVC_Cell3.Value = dr_Get_Info["PF_Active_Ind"].ToString();
                dataGridView1.Rows[i].Cells["PF_Active_Ind"] = DGVC_Cell3;

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
            dataGridView1.AutoResizeColumn(3, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(4, DataGridViewAutoSizeColumnMode.AllCells);
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
            cmb_Work_Order.Text = null;
            txt_Description.ResetText();
            btn_Add.Text = "&Add";
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            int int_result = 0;
            string str_msg = "";
            DialogResult DLR_Message = new DialogResult();

            if (cmb_Work_Order.SelectedIndex == -1)
            {
                str_msg = str_msg + "Invalid Work Order. Please select a valid Work Order from the drop down list." + Environment.NewLine;
            }
            if (txt_Description.TextLength == 0)
            {
                str_msg = str_msg + "Invalid Description. Please enter a description for this item." + Environment.NewLine;
            }
            if (str_msg.Length > 0)
            {
                DLR_Message = MessageBox.Show(str_msg, "Process Factory - Baseload(Maintenace).", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (DLR_Message != System.Windows.Forms.DialogResult.OK)
            {
                switch (btn_Add.Text)
                {
                    case "&Add":
                        int_result = PF.Data.AccessLayer.PF_BaseLoad.Insert(PF.Common.Code.General.int_max_user_id("PF_BaseLoad"), Convert.ToInt32(cmb_Work_Order.SelectedValue.ToString()), txt_Description.Text, Convert.ToBoolean(ckb_Active.Checked), int_Current_User_Id);
                        break;

                    case "&Update":
                        int_result = PF.Data.AccessLayer.PF_BaseLoad.Update(int_dvg_id, Convert.ToInt32(cmb_Work_Order.SelectedValue.ToString()), txt_Description.Text, Convert.ToBoolean(ckb_Active.Checked), int_Current_User_Id);
                        break;
                }
            }
            if (int_result > 0)
            {
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                lbl_message.Text = "Item has been saved";
                Reset();
                populate_datagridview();
            }
            else
            {
                lbl_message.ForeColor = System.Drawing.Color.Red;
                lbl_message.Text = "Item was NOT been saved";
            }
        }

        private int int_dvg_id = 0;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                int_dvg_id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["BaseLoad_Id"].Value.ToString());
                cmb_Work_Order.SelectedValue = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Work_Order_Id"].Value.ToString());
                txt_Description.Text = dataGridView1.CurrentRow.Cells["Description"].Value.ToString();
                ckb_Active.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells["PF_Active_Ind"].Value.ToString());
                btn_Add.Text = "&Update";
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
        private void Base_Load_Maintenance_KeyDown(object sender, KeyEventArgs e)
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