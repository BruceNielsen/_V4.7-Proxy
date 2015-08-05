using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;

namespace FruPak.PF.Utils.Security
{
    /*Description
    -----------------
    Maintenance Class.
     * In order to keep things simple and as standard as possible. It was decided where possible to a standard table structure would be used.
     * This structure is: _id, Code, Description, Mod_date, Mod_User_Id
     *
     * This class is a form used to Add, Update, and Delete general Security items, where the tables have foloowed the stanard structure.

    Date        Author     Desc
    -------------------------------------------------------------------------------------------------------------------------------------------------
    01/09/2013  Dave       Creation
    */
    public partial class Security_Maintenance : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static string str_table = "";
        private static int int_Current_User_Id = 0;
        private static int int_DVG_Row_id = 0;
        private static bool bol_write_access;
        public Security_Maintenance(string str_type, int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();
            //restrict access
            bol_write_access = bol_w_a;
            btn_Add.Enabled = bol_w_a;

            str_table = str_type;
            int_Current_User_Id = int_C_User_id;
            this.Text = str_type + " Maintenance";
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

                //else if (c.GetType() == typeof(FruPak.PF.Utils.UserControls.Customer))
                //{
                //    FruPak.PF.Utils.UserControls.Customer cust = (FruPak.PF.Utils.UserControls.Customer)c;
                //    cust.CustomerChanged += new EventHandler(this.CustomerControl_CustomerChanged);
                //}
            }
            #endregion

        }
        private void AddColumnsProgrammatically()
        {
            var col1 = new DataGridViewTextBoxColumn();
            var col2 = new DataGridViewTextBoxColumn();
            var col3 = new DataGridViewTextBoxColumn();
            var col4 = new DataGridViewTextBoxColumn();
            var col5 = new DataGridViewTextBoxColumn();

            col1.HeaderText = "Id";
            col1.Name = "Id";
            col1.ReadOnly = true;
            col1.Visible = false;

            col2.HeaderText = "Code";
            col2.Name = "Code";
            col2.ReadOnly = true;

            col3.HeaderText = "Description";
            col3.Name = "Description";
            col3.Width = 350;
            col3.ReadOnly = true;

            col4.HeaderText = "Mod_Date";
            col4.Name = "Mod_Date";
            col4.ReadOnly = true;
            col4.Visible = false;

            col5.HeaderText = "Mod_User_id";
            col5.Name = "Mod_User_id";
            col5.ReadOnly = true;
            col5.Visible = false;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col1, col2, col3, col4, col5 });

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
        private static DataSet ds_Get_Info;
        private void populate_datagridview()
        {
            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();

            switch (str_table)
            {
                case "SC_Log_Action":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.SC_Log_Action.Get_Info();
                    break;
            }


            DataRow dr_Get_Info;
            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                dataGridView1.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_User_Id = new DataGridViewTextBoxCell();
                switch (str_table)
                {
                    case "SC_Log_Action":
                        DGVC_User_Id.Value = dr_Get_Info["Action_Id"].ToString();
                        break;
                }

                dataGridView1.Rows[i].Cells[0] = DGVC_User_Id;

                DataGridViewCell DGVC_Name = new DataGridViewTextBoxCell();
                DGVC_Name.Value = dr_Get_Info["Code"].ToString();
                dataGridView1.Rows[i].Cells[1] = DGVC_Name;

                DataGridViewCell DGVC_Description = new DataGridViewTextBoxCell();
                DGVC_Description.Value = dr_Get_Info["Description"].ToString();
                dataGridView1.Rows[i].Cells[2] = DGVC_Description;

                DataGridViewCell DGVC_Mod_date = new DataGridViewTextBoxCell();
                DGVC_Mod_date.Value = dr_Get_Info["Mod_date"].ToString();
                dataGridView1.Rows[i].Cells[3] = DGVC_Mod_date;

                DataGridViewCell DGVC_Mod_User_Id = new DataGridViewTextBoxCell();
                DGVC_Mod_User_Id.Value = dr_Get_Info["Mod_User_Id"].ToString();
                dataGridView1.Rows[i].Cells[4] = DGVC_Mod_User_Id;

                //dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dataGridView1.AutoResizeColumn(0, DataGridViewAutoSizeColumnMode.AllCells);
                dataGridView1.AutoResizeColumn(1, DataGridViewAutoSizeColumnMode.AllCells);
                dataGridView1.AutoResizeColumn(5, DataGridViewAutoSizeColumnMode.AllCells);
                dataGridView1.AutoResizeColumn(6, DataGridViewAutoSizeColumnMode.AllCells);

                ds_Get_Info.Dispose();
            }
        }
        private void SizeAllColumns(Object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'moneyDataSet.List_TS_Work' table. You can move, or remove it, as needed.
            dataGridView1.AutoResizeColumn(0, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(1, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(5, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(6, DataGridViewAutoSizeColumnMode.AllCells);
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult DLR_Message = new DialogResult();
            int int_result = 0;

            if (e.ColumnIndex == 5)
            {
                string str_msg;

                str_msg = "You are about to delete " + Environment.NewLine;
                str_msg = str_msg + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + Environment.NewLine;
                str_msg = str_msg + "Do you want to continue?";
                DLR_Message = MessageBox.Show(str_msg, "Security - Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (DLR_Message == DialogResult.Yes)
                {
                    switch (str_table)
                    {
                        case "SC_Log_Action":
                            int_result = FruPak.PF.Data.AccessLayer.SC_Log_Action.Delete(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                            break;

                    }
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
            else if (e.ColumnIndex == 6)
            {
                int_DVG_Row_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                txt_code.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txt_Description.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                btn_Add.Text = "&Update";
            }

        }
        private void btn_Add_Click(object sender, EventArgs e)
        {
            DialogResult DLR_MessageBox = new DialogResult();
            string str_msg = "";
            int int_result = 0;

            str_msg = validate(str_table, (sender as Button).Text);
            if (str_msg.Length > 0)
            {
               DLR_MessageBox = MessageBox.Show(str_msg, "Common",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (DLR_MessageBox != DialogResult.OK)
            {
                switch ((sender as Button).Text)
                {
                    case "&Add":
                        switch (str_table)
                        {
                            case "SC_Log_Action":
                                int_result = FruPak.PF.Data.AccessLayer.SC_Log_Action.Insert(FruPak.PF.Common.Code.General.int_max_user_id("SC_Log_Action"), txt_code.Text, txt_Description.Text, int_Current_User_Id);
                                break;

                        }
                        break;
                    case "&Update":
                        switch (str_table)
                        {
                            case "SC_Log_Action":
                                int_result = FruPak.PF.Data.AccessLayer.SC_Log_Action.Update(int_DVG_Row_id, txt_code.Text, txt_Description.Text, int_Current_User_Id);
                                break;
                        }
                        break;
                }
            }
            if (int_result > 0)
            {
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                lbl_message.Text = txt_code.Text + " has been saved";
                Reset();
            }
            else
            {
                lbl_message.ForeColor = System.Drawing.Color.Red;
                lbl_message.Text = txt_code.Text + " has NOT been saved";
            }
            populate_datagridview();
        }
        /// <summary>
        /// validates the text fields on length
        /// </summary>
        /// <returns></returns>
        ///
        private DataSet ds_validate;
        private string validate(string str_table, string str_btnText)
        {
            string str_msg = "";

            if (txt_code.TextLength == 0)
            {
                str_msg = str_msg + "Invalid Code: Please Enter a valid Code" + Environment.NewLine;
            }
            else if (str_btnText == "Add")
            {
                switch (str_table)
                {
                    case "SC_Log_Action":
                        ds_validate = FruPak.PF.Data.AccessLayer.SC_Log_Action.Get_Info(txt_code.Text);
                        break;

                }

               if (Convert.ToInt32(ds_validate.Tables[0].Rows.Count.ToString()) > 0)
               {
                   str_msg = str_msg + "Invalid Code: This Code already exists. Please choose a different Code" + Environment.NewLine;
               }
               ds_validate.Dispose();
            }
            if (txt_Description.TextLength == 0)
            {
                str_msg = str_msg + "Invalid Description: Please Enter a Description";
            }
            return str_msg;
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
        //private void CustomerControl_CustomerChanged(object sender, EventArgs e)
        //{
        //    FruPak.PF.Utils.UserControls.Customer cust = (FruPak.PF.Utils.UserControls.Customer)sender;
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
        #endregion

        /// <summary>
        /// Close the form with the Esc key (Sel request 11-02-2015 BN)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Security_Maintenance_KeyDown(object sender, KeyEventArgs e)
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
