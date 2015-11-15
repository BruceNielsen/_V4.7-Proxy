using NLog;
using System;
using System.Data;
using System.Windows.Forms;

namespace FruPak.PF.Utils.Common
{
    public partial class Grower : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static bool bol_write_access;
        private static int int_Current_User_Id = 0;
        private static int int_Grower_Id = 0;

        public Grower(int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();
            int_Current_User_Id = int_C_User_id;

            //setup Outlook location

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
            AddColumnsProgrammatically();
            populate_datagridview();
            populate_Combobox();

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
            var col1 = new DataGridViewTextBoxColumn();
            var col2 = new DataGridViewTextBoxColumn();
            var col3 = new DataGridViewTextBoxColumn();
            var col3a = new DataGridViewTextBoxColumn();
            var col4 = new DataGridViewTextBoxColumn();
            var col5 = new DataGridViewTextBoxColumn();
            var col6 = new DataGridViewTextBoxColumn();
            var col7 = new DataGridViewTextBoxColumn();
            var col8 = new DataGridViewTextBoxColumn();
            var col9 = new DataGridViewTextBoxColumn();

            col1.HeaderText = "Id";
            col1.Name = "Id";
            col1.ReadOnly = true;
            col1.Visible = false;

            col2.HeaderText = "RPIN";
            col2.Name = "Rpin";
            col2.ReadOnly = true;

            col3a.HeaderText = "Orchardist_Id";
            col3a.Name = "Orchardist_Id";
            col3a.ReadOnly = true;
            col3a.Visible = false;

            col3.HeaderText = "Orchardist";
            col3.Name = "Description";
            col3.ReadOnly = true;

            col4.HeaderText = "Global Gap";
            col4.Name = "Global_Gap_Number";
            col4.Width = 100;
            col4.ReadOnly = true;

            col5.HeaderText = "GST";
            col5.Name = "GST";
            col5.ReadOnly = true;

            col6.HeaderText = "Active";
            col6.Name = "Active_Ind";
            col6.ReadOnly = true;

            col7.HeaderText = "Outlook";
            col7.Name = "Customer_Id";
            col7.ReadOnly = true;
            col7.Visible = false;

            col8.HeaderText = "Mod_Date";
            col8.Name = "Mod_Date";
            col8.ReadOnly = true;
            col8.Visible = false;

            col9.HeaderText = "Mod_User_id";
            col9.Name = "Mod_User_id";
            col9.ReadOnly = true;
            col9.Visible = false;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col1, col2, col3a, col3, col4, col5, col6, col7, col8, col9 });

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

            DataGridViewButtonColumn btn_block1 = new DataGridViewButtonColumn();
            btn_block1.Text = "Block";
            btn_block1.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(btn_block1);
        }

        private void populate_datagridview()
        {
            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();

            DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Grower.Get_Info();
            DataRow dr_Get_Info;
            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                dataGridView1.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_Id = new DataGridViewTextBoxCell();
                DGVC_Id.Value = dr_Get_Info["Grower_Id"].ToString();
                dataGridView1.Rows[i].Cells[0] = DGVC_Id;

                DataGridViewCell DGVC_RPIN = new DataGridViewTextBoxCell();
                DGVC_RPIN.Value = dr_Get_Info["RPIN"].ToString();
                dataGridView1.Rows[i].Cells[1] = DGVC_RPIN;

                DataGridViewCell DGVC_Orchardist_Id = new DataGridViewTextBoxCell();
                DGVC_Orchardist_Id.Value = dr_Get_Info["Orchardist_Id"].ToString();
                dataGridView1.Rows[i].Cells[2] = DGVC_Orchardist_Id;
                DataGridViewCell DGVC_Orchardist = new DataGridViewTextBoxCell();
                DGVC_Orchardist.Value = dr_Get_Info["Description"].ToString();
                dataGridView1.Rows[i].Cells[3] = DGVC_Orchardist;

                DataGridViewCell DGVC_GAP = new DataGridViewTextBoxCell();
                DGVC_GAP.Value = dr_Get_Info["Global_Gap_Number"].ToString();
                dataGridView1.Rows[i].Cells[4] = DGVC_GAP;

                DataGridViewCell DGVC_GST = new DataGridViewTextBoxCell();
                DGVC_GST.Value = dr_Get_Info["GST"].ToString();
                dataGridView1.Rows[i].Cells[5] = DGVC_GST;

                DataGridViewCell DGVC_Active = new DataGridViewTextBoxCell();
                DGVC_Active.Value = dr_Get_Info["PF_Active_Ind"].ToString();
                dataGridView1.Rows[i].Cells[6] = DGVC_Active;

                DataGridViewCell DGVC_Outlook = new DataGridViewTextBoxCell();
                DGVC_Outlook.Value = dr_Get_Info["Customer_Id"].ToString();
                dataGridView1.Rows[i].Cells[7] = DGVC_Outlook;

                DataGridViewCell DGVC_Mod_date = new DataGridViewTextBoxCell();
                DGVC_Mod_date.Value = dr_Get_Info["Mod_date"].ToString();
                dataGridView1.Rows[i].Cells[8] = DGVC_Mod_date;

                DataGridViewCell DGVC_Mod_User_Id = new DataGridViewTextBoxCell();
                DGVC_Mod_User_Id.Value = dr_Get_Info["Mod_User_Id"].ToString();
                dataGridView1.Rows[i].Cells[9] = DGVC_Mod_User_Id;

                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                ds_Get_Info.Dispose();
            }
        }

        private void SizeAllColumns(Object sender, EventArgs e)
        {
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private static DataSet ds_EX_Details;
        private static DataSet ds_CM_Orchardist;

        private void populate_Combobox()
        {
            ds_EX_Details = FruPak.PF.Data.AccessLayer.EX_Pipfruit_Orchard.Get_Distinct_RPINS();
            cmb_Rpin.DataSource = ds_EX_Details.Tables[0];
            cmb_Rpin.DisplayMember = "RPIN";
            cmb_Rpin.ValueMember = "RPIN";
            cmb_Rpin.Text = null;

            ds_CM_Orchardist = FruPak.PF.Data.AccessLayer.CM_Orchardist.Get_Info();
            cmb_Orchardist.DataSource = ds_CM_Orchardist.Tables[0];
            cmb_Orchardist.DisplayMember = "Description";
            cmb_Orchardist.ValueMember = "Orchardist_Id";
            cmb_Orchardist.Text = null;

            if (cmb_Rpin.SelectedValue != null)
            {
                Read_PipFruit(cmb_Rpin.SelectedValue.ToString());
            }
        }

        private void cmb_Rpin_SelectedValueChanged(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedValue != null)
            {
                Read_PipFruit((sender as ComboBox).SelectedValue.ToString());
            }
        }

        private void Read_PipFruit(string str_rpin)
        {
            DataRow dr_EX_Details;
            for (int i = 0; i < Convert.ToInt32(ds_EX_Details.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_EX_Details = ds_EX_Details.Tables[0].Rows[i];
                if (dr_EX_Details["RPIN"].ToString() == str_rpin)
                {
                    txt_Global_Gap.Text = dr_EX_Details["Gap"].ToString();
                    cmb_Orchardist.SelectedIndex = cmb_Orchardist.FindString(dr_EX_Details["Orchardist"].ToString());
                }
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Add_btn();
        }

        private void Add_btn()
        {
            DialogResult DLR_MessageBox = new DialogResult();
            string str_msg = "";
            string str_rpin = "";
            int int_result = 0;

            if (btn_Add.Text == "&Add")
            {
                if (cmb_Rpin.SelectedValue == null && cmb_Rpin.Text == "")
                {
                    str_msg = str_msg + "Invalid RPIN: Either Select a valid RPIN or Enter one. Please select another RPIN" + Environment.NewLine;
                }
                else if (cmb_Rpin.SelectedValue != null)
                {
                    str_rpin = cmb_Rpin.SelectedValue.ToString();
                }
                else if (cmb_Rpin.Text != "")
                {
                    str_rpin = cmb_Rpin.Text;
                }

                DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Grower.Get_Info(str_rpin);

                if (Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()) > 0)
                {
                    str_msg = str_msg + "Invalid RPIN: This RPIN has already been allocated to a Grower. Please select another RPIN" + Environment.NewLine;
                }
            }

            if (cmb_Orchardist.SelectedValue == null)
            {
                str_msg = str_msg + "Invalid Orchardist: Please select a valid Orchardist from the drop down list" + Environment.NewLine;
            }

            if (txt_Global_Gap.TextLength == 0)
            {
                str_msg = str_msg + "Invalid Global GAP: Please enter a valid Global GAP Number" + Environment.NewLine;
            }
            else
            {
                try
                {
                    Convert.ToDecimal(txt_Global_Gap.Text);
                }
                catch
                {
                    str_msg = str_msg + "Invalid Global GAP: Global GAP Number needs to be numeric only" + Environment.NewLine;
                }
            }
            if (txt_GST.TextLength == 0)
            {
                str_msg = str_msg + "Invalid GST: Please enter a valid GST Number" + Environment.NewLine;
            }
            else
            {
                try
                {
                    Convert.ToDecimal(txt_GST.Text);
                }
                catch
                {
                    str_msg = str_msg + "Invalid Global GAP: GST Number needs to be numeric only" + Environment.NewLine;
                }
            }

            if (customer1.Customer_Id <= 0)
            {
                str_msg = str_msg + "Invalid Customer Link.  Please select a valid link from the drop down list." + Environment.NewLine;
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

                        int_Grower_Id = FruPak.PF.Common.Code.General.int_max_user_id("CM_Grower");
                        int_result = FruPak.PF.Data.AccessLayer.CM_Grower.Insert(int_Grower_Id, str_rpin, Convert.ToInt32(cmb_Orchardist.SelectedValue.ToString()),
                                                                              Convert.ToDecimal(txt_Global_Gap.Text), Convert.ToDecimal(txt_GST.Text), ckb_Active.Checked, customer1.Customer_Id, int_Current_User_Id);
                        //CREATE BLOCK FOR Grower
                        if (cmb_Rpin.SelectedValue != null)
                        {
                            DataSet ds_Get_Blocks = FruPak.PF.Data.AccessLayer.EX_Pipfruit_Orchard.Get_Blocks(cmb_Rpin.SelectedValue.ToString());
                            DataRow dr_Get_Blocks;
                            for (int i = 0; i < Convert.ToInt32(ds_Get_Blocks.Tables[0].Rows.Count.ToString()); i++)
                            {
                                dr_Get_Blocks = ds_Get_Blocks.Tables[0].Rows[i];

                                int int_Block_Id = FruPak.PF.Common.Code.General.int_max_user_id("CM_Block");
                                int_result = FruPak.PF.Data.AccessLayer.CM_Block.Insert(int_Block_Id, int_Grower_Id, dr_Get_Blocks["Prodsite_code"].ToString() + dr_Get_Blocks["Mgmtarea_code"].ToString(), null, true, int_Current_User_Id);
                                //Create Variety for each Block for Grower

                                DataSet ds_Get_Variety = FruPak.PF.Data.AccessLayer.EX_Pipfruit_Orchard.Get_Variety(cmb_Rpin.SelectedValue.ToString(), dr_Get_Blocks["Prodsite_code"].ToString(), dr_Get_Blocks["Mgmtarea_code"].ToString());
                                DataRow dr_Get_Variety;
                                for (int j = 0; j < Convert.ToInt32(ds_Get_Variety.Tables[0].Rows.Count.ToString()); j++)
                                {
                                    dr_Get_Variety = ds_Get_Variety.Tables[0].Rows[j];
                                    int_result = FruPak.PF.Data.AccessLayer.CM_Block_Variety_Relationship.Insert(FruPak.PF.Common.Code.General.int_max_user_id("CM_Block_Variety_Relationship"), int_Block_Id, Convert.ToInt32(dr_Get_Variety["Variety_Id"].ToString()), int_Current_User_Id);
                                }
                            }

                            ds_Get_Blocks.Dispose();
                        }
                        lbl_message.Text = str_rpin + " has been added";
                        break;

                    case "&Update":

                        if (cmb_Rpin.SelectedValue == null)
                        {
                            str_rpin = cmb_Rpin.Text;
                        }
                        else
                        {
                            str_rpin = cmb_Rpin.SelectedValue.ToString();
                        }

                        int_result = FruPak.PF.Data.AccessLayer.CM_Grower.Update(int_Grower_Id, str_rpin, Convert.ToInt32(cmb_Orchardist.SelectedValue.ToString()),
                                                                              Convert.ToDecimal(txt_Global_Gap.Text), Convert.ToDecimal(txt_GST.Text), ckb_Active.Checked, int_Current_User_Id, customer1.Customer_Id);
                        lbl_message.Text = str_rpin + " has been updated";
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
                lbl_message.Text = str_rpin + " failed to be Added / Updated";
            }
            populate_datagridview();
            Reset();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult DLR_Message = new DialogResult();
            int int_result = 0;

            //Delete
            if (e.ColumnIndex == 10)
            {
                string str_msg;
                DataSet ds = null;
                ds = FruPak.PF.Data.AccessLayer.PF_Work_Order.Check_Grower(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    str_msg = "The chosen Grower can NOT be deleted." + Environment.NewLine;
                    str_msg = str_msg + "You can edit the Grower and make in-active." + Environment.NewLine;
                    DLR_Message = MessageBox.Show(str_msg, "Process Factory - Common (Grower Deletion)", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                ds.Dispose();

                if (DLR_Message != System.Windows.Forms.DialogResult.OK)
                {
                    str_msg = "You are about to delete " + Environment.NewLine;
                    str_msg = str_msg + "Grower " + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + Environment.NewLine;
                    str_msg = str_msg + "Do you want to continue?";
                    DLR_Message = MessageBox.Show(str_msg, "Common - Grower Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (DLR_Message == DialogResult.Yes)
                    {
                        //delete block variety link
                        ds = FruPak.PF.Data.AccessLayer.CM_Block.Get_Info(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                        DataRow dr;
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            dr = ds.Tables[0].Rows[i];
                            int_result = int_result + FruPak.PF.Data.AccessLayer.CM_Block_Variety_Relationship.Delete_For_Block(Convert.ToInt32(dr["Block_Id"].ToString()));
                        }
                        ds.Dispose();

                        //delete blocks linked to a grower
                        int_result = int_result + FruPak.PF.Data.AccessLayer.CM_Block.Delete_All_for_Grower(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                        //delete the grower
                        int_result = int_result + FruPak.PF.Data.AccessLayer.CM_Grower.Delete(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                    }

                    if (int_result > 0)
                    {
                        lbl_message.ForeColor = System.Drawing.Color.Blue;
                        lbl_message.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + " has been deleted";
                    }
                    else
                    {
                        lbl_message.ForeColor = System.Drawing.Color.Red;
                        lbl_message.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + " failed to delete";
                    }
                    populate_datagridview();
                }
            }
            //Edit
            else if (e.ColumnIndex == 11)
            {
                Cursor = Cursors.WaitCursor;
                int_Grower_Id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                cmb_Rpin.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                cmb_Rpin.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                cmb_Orchardist.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();

                txt_Global_Gap.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                txt_GST.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                ckb_Active.Checked = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());
                try
                {
                    customer1.Customer_Id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString());
                }
                catch
                {
                    customer1.Customer_Id = 0;
                }

                btn_Add.Text = "Update";
                Cursor = Cursors.Default;
            }
            //block Button
            else if (e.ColumnIndex == 12)
            {
                Form block = new Grower_Block(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()), int_Current_User_Id, bol_write_access);
                block.ShowDialog();
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            txt_Global_Gap.ResetText();
            txt_GST.ResetText();
            ckb_Active.Checked = true;
            btn_Add.Text = "&Add";
            customer1.Customer_Id = 0;
            cmb_Orchardist.Text = null;
            cmb_Rpin.Text = null;
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

        private void cmb_Orchardist_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DataSet ds = FruPak.PF.Data.AccessLayer.CM_Orchardist.Get_Info(Convert.ToInt32((sender as ComboBox).SelectedValue.ToString()));
            DataRow dr;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dr = ds.Tables[0].Rows[i];
                try
                {
                    customer1.Customer_Id = Convert.ToInt32(dr["Customer_Id"].ToString());
                }
                catch
                {
                    customer1.Customer_Id = 0;
                }
            }
            ds.Dispose();
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
        private void Grower_KeyDown(object sender, KeyEventArgs e)
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