using NLog;
using System;
using System.Data;
using System.Windows.Forms;

namespace PF.Utils.Common
{
    /*Description
    -----------------
    Maintenance2 Class.
     * In order to keep things simple and as standard as possible. It was decided where possible to a standard table structure would be used.
     * This structure is: _id, Code, Description, Outlook_Key, Active_Ind, Mod_date, Mod_User_Id
     *
     * This class is a form used to Add, Update, and Delete general Common items, where the tables have followed the standard structure.
     *
     * This class is used where Outlook is also needed to be accessed

    Date        Author     Desc
    -------------------------------------------------------------------------------------------------------------------------------------------------
    01/09/2013  Dave       Creation
    */

    public partial class Common_Maintenance2 : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static string str_table = "";
        private static bool bol_write_access;
        private static int int_DVG_Row_id = 0;
        private static int int_Current_User_Id = 0;

        public Common_Maintenance2(string str_type, int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();
            int_Current_User_Id = int_C_User_id;
            str_table = str_type;
            //check if testing or not

            //if (PF.Global.Global.bol_Testing == true)
            //{
            //    this.Text = "FP Process Factory - " + this.Text + " - Test Environment";
            //}
            //else
            //{
            //    this.Text = "FP Process Factory";
            //}

            //setup Outlook location

            // Experimental Change 23-03-2015
            // PF.Data.Outlook.Outlook.Folder_Name = PF.Common.Code.Outlook.SetUp_Location("OutLook%");
            PF.Data.Outlook.Outlook.Folder_Name = PF.Common.Code.Outlook.SetUp_Location("OutLook");

            //restrict access
            bol_write_access = bol_w_a;
            btn_Add.Enabled = bol_w_a;

            switch (str_table)
            {
                case "CM_Trader":
                    lbl_barcode.Visible = true;
                    txt_barcode.Visible = true;
                    break;

                default:
                    lbl_barcode.Visible = false;
                    txt_barcode.Visible = false;
                    break;
            }
            AddColumnsProgrammatically();
            populate_datagridview();
            populate_outlook_Combo();

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

            col1.HeaderText = "Code";
            col1.Name = "Code";
            col1.ReadOnly = true;

            col2.HeaderText = "Description";
            col2.Name = "Description";
            col2.Width = 350;
            col2.ReadOnly = true;

            col3.HeaderText = "Active";
            col3.Name = "Active_Ind";
            col3.ReadOnly = true;

            col4.HeaderText = "Customer";
            col4.Name = "Customer_Id";
            col4.ReadOnly = true;
            col4.Visible = false;

            col5.HeaderText = "Barcode";
            col5.Name = "Barcode_Num";
            col5.ReadOnly = true;
            switch (str_table)
            {
                case "CM_Trader":
                    col5.Visible = true;
                    break;

                default:
                    col5.Visible = false;
                    break;
            }

            col6.HeaderText = "Mod_Date";
            col6.Name = "Mod_Date";
            col6.ReadOnly = true;
            col6.Visible = false;

            col7.HeaderText = "Mod_User_id";
            col7.Name = "Mod_User_id";
            col7.ReadOnly = true;
            col7.Visible = false;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2, col3, col4, col5, col6, col7 });

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

        private static DataSet ds_Get_Info;

        private void populate_datagridview()
        {
            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();

            DataRow dr_Get_Info;

            switch (str_table)
            {
                case "CM_Trader":
                    ds_Get_Info = PF.Data.AccessLayer.CM_Trader.Get_Info();
                    break;

                case "CM_Orchardist":
                    ds_Get_Info = PF.Data.AccessLayer.CM_Orchardist.Get_Info();
                    break;
            }

            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                dataGridView1.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_Id = new DataGridViewTextBoxCell();
                switch (str_table)
                {
                    case "CM_Trader":
                        DGVC_Id.Value = dr_Get_Info["Trader_Id"].ToString();
                        break;

                    case "CM_Orchardist":
                        DGVC_Id.Value = dr_Get_Info["Orchardist_Id"].ToString();
                        break;
                }
                dataGridView1.Rows[i].Cells[0] = DGVC_Id;

                DataGridViewCell DGVC_Code = new DataGridViewTextBoxCell();
                DGVC_Code.Value = dr_Get_Info["Code"].ToString();
                dataGridView1.Rows[i].Cells[1] = DGVC_Code;

                DataGridViewCell DGVC_Description = new DataGridViewTextBoxCell();
                DGVC_Description.Value = dr_Get_Info["Description"].ToString();
                dataGridView1.Rows[i].Cells[2] = DGVC_Description;

                DataGridViewCell DGVC_Active = new DataGridViewTextBoxCell();
                DGVC_Active.Value = dr_Get_Info["PF_Active_Ind"].ToString();
                dataGridView1.Rows[i].Cells[3] = DGVC_Active;

                DataGridViewCell DGVC_Outlook = new DataGridViewTextBoxCell();
                DGVC_Outlook.Value = dr_Get_Info["Customer_Id"].ToString();
                dataGridView1.Rows[i].Cells[4] = DGVC_Outlook;

                switch (str_table)
                {
                    case "CM_Trader":
                        DataGridViewCell DGVC_Barcode_Num = new DataGridViewTextBoxCell();
                        DGVC_Barcode_Num.Value = dr_Get_Info["Barcode_Num"].ToString();
                        dataGridView1.Rows[i].Cells[5] = DGVC_Barcode_Num;
                        break;
                }

                DataGridViewCell DGVC_Mod_date = new DataGridViewTextBoxCell();
                DGVC_Mod_date.Value = dr_Get_Info["Mod_Date"].ToString();
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

        private void populate_outlook_Combo()
        {
            Cursor = Cursors.WaitCursor;
            cmb_Outlook.DataSource = null;

            DataSet ds_Contacts = PF.Data.AccessLayer.PF_Customer.Get_Info();
            cmb_Outlook.DataSource = ds_Contacts.Tables[0];
            cmb_Outlook.DisplayMember = "Name";
            cmb_Outlook.ValueMember = "Customer_Id";
            cmb_Outlook.Text = null;
            Cursor = Cursors.Default;
        }

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
                    case "CM_Trader":
                        ds_validate = PF.Data.AccessLayer.CM_Trader.Get_Info(txt_code.Text);
                        break;

                    case "CM_Orchardist":
                        ds_validate = PF.Data.AccessLayer.CM_Orchardist.Get_Info(txt_code.Text);
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
            switch (str_table)
            {
                case "CM_Trader":
                    try
                    {
                        if (txt_barcode.TextLength > 0)
                        {
                            Convert.ToInt32(txt_barcode.Text);
                        }
                    }
                    catch
                    {
                        str_msg = str_msg + "Invalid Barcode: The barcode needs to be a numeric. Please re-enter" + Environment.NewLine;
                    }
                    break;
            }
            return str_msg;
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            // This can take quite a while, so give some visual feedback - BN 24-06-2015

            // Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor;

            Add_btn();

            // Set cursor as default arrow
            Cursor.Current = Cursors.Default;
        }

        private void Add_btn()
        {
            DialogResult DLR_MessageBox = new DialogResult();
            string str_msg = "";
            int int_result = 0;

            str_msg = validate(str_table, btn_Add.Text);

            if (cmb_Outlook.SelectedIndex == -1)
            {
                str_msg = str_msg + "Invalid Customer. Please select a valid Customer from the drop down list." + Environment.NewLine;
            }

            if (str_msg.Length > 0)
            {
                DLR_MessageBox = MessageBox.Show(str_msg, "Common", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (DLR_MessageBox != DialogResult.OK)
            {
                switch (btn_Add.Text)
                {
                    case "&Add":
                        switch (str_table)
                        {
                            case "CM_Trader":
                                int_result = PF.Data.AccessLayer.CM_Trader.Insert(PF.Common.Code.General.int_max_user_id("CM_Trader"), txt_code.Text, txt_Description.Text, Convert.ToInt32(cmb_Outlook.SelectedValue.ToString()), txt_barcode.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;

                            case "CM_Orchardist":
                                int_result = PF.Data.AccessLayer.CM_Orchardist.Insert(PF.Common.Code.General.int_max_user_id("CM_Orchardist"), txt_code.Text, txt_Description.Text, Convert.ToInt32(cmb_Outlook.SelectedValue.ToString()), ckb_Active.Checked, int_Current_User_Id);
                                break;
                        }
                        break;

                    case "&Update":
                        switch (str_table)
                        {
                            case "CM_Trader":
                                int_result = PF.Data.AccessLayer.CM_Trader.Update(int_DVG_Row_id, txt_code.Text, txt_Description.Text, Convert.ToInt32(cmb_Outlook.SelectedValue.ToString()), txt_barcode.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;

                            case "CM_Orchardist":
                                int_result = PF.Data.AccessLayer.CM_Orchardist.Update(int_DVG_Row_id, txt_code.Text, txt_Description.Text, Convert.ToInt32(cmb_Outlook.SelectedValue.ToString()), ckb_Active.Checked, int_Current_User_Id);
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
            Reset();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult DLR_Message = new DialogResult();
            int int_result = 0;

            //Delete
            if (e.ColumnIndex == 8)
            {
                string str_msg;

                DataSet ds = null;

                switch (str_table)
                {
                    case "CM_Trader":
                        ds = PF.Data.AccessLayer.CM_Trader.Check_Trader(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            str_msg = "The chosen Trader can NOT be deleted." + Environment.NewLine;
                            str_msg = str_msg + "You can edit the Trader and make in-active." + Environment.NewLine;
                            DLR_Message = MessageBox.Show(str_msg, "Process Factory - Common (Trader Deletion)", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        ds.Dispose();
                        break;

                    case "CM_Orchardist":
                        ds = PF.Data.AccessLayer.CM_Orchardist.Check_Orchardist(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            str_msg = "The chosen Orchardist can NOT be deleted." + Environment.NewLine;
                            str_msg = str_msg + "You can edit the Orchardist and make in-active." + Environment.NewLine;
                            DLR_Message = MessageBox.Show(str_msg, "Process Factory - Common (Orchardist Deletion)", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        ds.Dispose();
                        break;
                }

                if (DLR_Message != System.Windows.Forms.DialogResult.OK)
                {
                    str_msg = "You are about to delete " + Environment.NewLine;
                    str_msg = str_msg + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + Environment.NewLine;
                    str_msg = str_msg + "Do you want to continue?";

                    switch (str_table)
                    {
                        case "CM_Trader":
                            DLR_Message = MessageBox.Show(str_msg, "Process Factory - Common (Trader Deletion)", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            break;

                        case "CM_Orchardist":
                            DLR_Message = MessageBox.Show(str_msg, "Process Factory - Common (Orchardist Deletion)", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            break;
                    }

                    if (DLR_Message == DialogResult.Yes)
                    {
                        switch (str_table)
                        {
                            case "CM_Trader":
                                int_result = PF.Data.AccessLayer.CM_Trader.Delete(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                                break;

                            case "CM_Orchardist":
                                int_result = PF.Data.AccessLayer.CM_Orchardist.Delete(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
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
            }
            //Edit
            else if (e.ColumnIndex == 9)
            {
                int_DVG_Row_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                txt_code.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txt_Description.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                try
                {
                    cmb_Outlook.SelectedValue = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                }
                catch
                {
                    cmb_Outlook.Text = null;
                }
                switch (str_table)
                {
                    case "CM_Trader":
                        txt_barcode.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                        break;
                }

                ckb_Active.Checked = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                btn_Add.Text = "&Update";
            }
            //outlook View
            else if (e.ColumnIndex == 10)
            {
                int int_return_Code = 0;
                int_return_Code = PF.Data.Outlook.Contacts.Contact_Display(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());

                if (int_return_Code > 0)
                {
                    MessageBox.Show("The Outlook Entry could not be found. You will need to select a New Outlook link for this person", "Common - Outlook", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
            txt_barcode.ResetText();
            cmb_Outlook.Text = null;
            btn_Add.Text = "&Add";
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
        private void Common_Maintenance2_KeyDown(object sender, KeyEventArgs e)
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