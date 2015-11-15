using NLog;
using System;
using System.Data;
using System.Windows.Forms;

namespace FruPak.PF.Utils.Common
{
    public partial class Grower_Block : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static bool bol_write_access;
        private static int int_Current_User_Id = 0;
        private static int int_Grower_Id = 0;
        private static int int_Block_Id = 0;

        public Grower_Block(int int_Grow_Id, int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();

            int_Current_User_Id = int_C_User_id;
            int_Grower_Id = int_Grow_Id;
            txt_rpin.Text = FruPak.PF.Data.AccessLayer.CM_Grower.Get_RPIN(int_Grower_Id);
            //check if testing or not

            //if (FruPak.PF.Global.Global.bol_Testing == true)
            //{
            //    this.Text = "FruPak Process Factory - " + this.Text + " - Test Environment";
            //}
            //else
            //{
            //    this.Text = "FruPak Process Factory";
            //}
            //restrict access
            bol_write_access = bol_w_a;
            btn_Add.Enabled = bol_w_a;

            AddColumnsProgrammatically();
            populate_datagridview1(int_Grower_Id);

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

            col1.HeaderText = "Grower";
            col1.Name = "Grower_Id";
            col1.ReadOnly = true;
            col1.Visible = false;

            col2.HeaderText = "RPIN";
            col2.Name = "RPIN";
            col2.ReadOnly = true;

            col3.HeaderText = "Block";
            col3.Name = "Block";
            col3.ReadOnly = true;

            col4.HeaderText = "Description";
            col4.Name = "Description";
            //col4.Width = 100;
            col4.ReadOnly = true;

            col5.HeaderText = "Active";
            col5.Name = "Active_Ind";
            col5.ReadOnly = true;

            col6.HeaderText = "Mod_Date";
            col6.Name = "Mod_Date";
            col6.ReadOnly = true;
            col6.Visible = false;

            col7.HeaderText = "Mod_User_id";
            col7.Name = "Mod_User_id";
            col7.ReadOnly = true;
            col7.Visible = false;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2, col3, col4, col5, col6, col7 });

            DataGridViewImageColumn img_edit = new DataGridViewImageColumn();
            dataGridView1.Columns.Add(img_edit);
            img_edit.HeaderText = "Edit";
            img_edit.Name = "Edit";
            img_edit.Image = FruPak.PF.Global.Properties.Resources.edit;
            img_edit.ReadOnly = true;

            DataGridViewButtonColumn btn_variety = new DataGridViewButtonColumn();
            btn_variety.Text = "Variety";
            btn_variety.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(btn_variety);
        }

        private void populate_datagridview1(int int_Grower_Id)
        {
            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();

            DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Block.Get_Info(int_Grower_Id);
            DataRow dr_Get_Info;
            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                dataGridView1.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_Id = new DataGridViewTextBoxCell();
                DGVC_Id.Value = dr_Get_Info["Block_Id"].ToString();
                dataGridView1.Rows[i].Cells[0] = DGVC_Id;

                DataGridViewCell DGVC_Grower_Id = new DataGridViewTextBoxCell();
                DGVC_Grower_Id.Value = dr_Get_Info["Grower_Id"].ToString();
                dataGridView1.Rows[i].Cells[1] = DGVC_Grower_Id;

                DataGridViewCell DGVC_RPIN = new DataGridViewTextBoxCell();
                DGVC_RPIN.Value = dr_Get_Info["RPIN"].ToString();
                dataGridView1.Rows[i].Cells[2] = DGVC_RPIN;

                DataGridViewCell DGVC_Code = new DataGridViewTextBoxCell();
                DGVC_Code.Value = dr_Get_Info["Code"].ToString();
                dataGridView1.Rows[i].Cells[3] = DGVC_Code;

                DataGridViewCell DGVC_Description = new DataGridViewTextBoxCell();
                DGVC_Description.Value = dr_Get_Info["Description"].ToString();
                dataGridView1.Rows[i].Cells[4] = DGVC_Description;

                DataGridViewCell DGVC_Active = new DataGridViewTextBoxCell();
                DGVC_Active.Value = dr_Get_Info["PF_Active_Ind"].ToString();
                dataGridView1.Rows[i].Cells[5] = DGVC_Active;

                DataGridViewCell DGVC_Mod_date = new DataGridViewTextBoxCell();
                DGVC_Mod_date.Value = dr_Get_Info["Mod_date"].ToString();
                dataGridView1.Rows[i].Cells[6] = DGVC_Mod_date;

                DataGridViewCell DGVC_Mod_User_Id = new DataGridViewTextBoxCell();
                DGVC_Mod_User_Id.Value = dr_Get_Info["Mod_User_Id"].ToString();
                dataGridView1.Rows[i].Cells[7] = DGVC_Mod_User_Id;

                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                ds_Get_Info.Dispose();
            }
        }

        private void SizeAllColumns(Object sender, EventArgs e)
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

            if (btn_Add.Text == "&Add")
            {
                if (txt_code.TextLength == 0)
                {
                    str_msg = str_msg + "Invalid Code: Please enter a valid Block" + Environment.NewLine;
                }
            }
            if (str_msg.Length > 0)
            {
                DLR_MessageBox = MessageBox.Show(str_msg, "Common - Grower", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (DLR_MessageBox != DialogResult.OK)
            {
                switch (btn_Add.Text)
                {
                    case "&Add":
                        if (txt_code.TextLength > 0)
                        {
                            int_result = FruPak.PF.Data.AccessLayer.CM_Block.Insert(FruPak.PF.Common.Code.General.int_max_user_id("CM_Block"), int_Grower_Id, txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                            lbl_message.Text = "Block " + txt_code.Text + " has been added";
                        }
                        break;

                    case "&Update":
                        int_result = FruPak.PF.Data.AccessLayer.CM_Block.Update(int_Block_Id, int_Grower_Id, txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                        lbl_message.Text = "Block " + txt_code.Text + " has been updated";
                        break;
                }
            }
            if (int_result > 0)
            {
                lbl_message.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                lbl_message.ForeColor = System.Drawing.Color.Red;
                lbl_message.Text = "Block " + txt_code.Text + " failed to be Added / Updated";
            }
            populate_datagridview1(int_Grower_Id);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Edit
            if (e.ColumnIndex == 8)
            {
                int_Block_Id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                txt_code.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txt_Description.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                ckb_Active.Checked = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
                btn_Add.Text = "&Update";
            }
            //block Button
            else if (e.ColumnIndex == 9)
            {
                Form block = new Grower_Block_Variety(int_Grower_Id, Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()), int_Current_User_Id, bol_write_access);
                block.ShowDialog();
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            txt_code.ResetText();
            txt_Description.ResetText();
            btn_Add.Text = "&Add";
            ckb_Active.Checked = true;
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
        private void Grower_Block_KeyDown(object sender, KeyEventArgs e)
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