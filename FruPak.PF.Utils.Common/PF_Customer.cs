using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;

namespace FruPak.PF.Utils.Common
{
    public partial class PF_Customer : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static int int_Current_User_Id = 0;
        private static bool bol_write_access;

        /// <summary>
        /// Occasionally getting: A first chance exception of type 'System.TypeInitializationException' occurred in FruPak.PF.Utils.Common.dll
        /// </summary>
        /// <param name="int_C_User_id"></param>
        /// <param name="bol_w_a"></param>
        public PF_Customer(int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();
            int_Current_User_Id = int_C_User_id;

            //check if testing or not

            //if (FruPak.PF.Global.Global.bol_Testing == true)
            //{
            //    this.Text = "FruPak Process Factory - " + this.Text + " - Test Environment";
            //}
            //else
            //{
            //    this.Text = "FruPak Process Factory";
            //}
            //setup Outlook location

            try
            {
                // Experimental Change 23-03-2015
                //FruPak.PF.Data.Outlook.Outlook.Folder_Name = FruPak.PF.Common.Code.Outlook.SetUp_Location("OutLook%");
                FruPak.PF.Data.Outlook.Outlook.Folder_Name = FruPak.PF.Common.Code.Outlook.SetUp_Location("OutLook");

                if (FruPak.PF.Data.Outlook.Outlook.Folder_Name == string.Empty)
                {
                    Console.WriteLine("FruPak.PF.Data.Outlook.Outlook.Folder_Name == string.Empty");
                    logger.Log(LogLevel.Debug, "FruPak.PF.Data.Outlook.Outlook.Folder_Name == string.Empty");
                }
                //restrict access
                bol_write_access = bol_w_a;
                btn_Add.Enabled = bol_w_a;

                AddColumnsProgrammatically();
                populate_datagridview();
                populate_outlook_Combo();
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, "PF_Customer: " + ex.Message);
                MessageBox.Show("Outlook link just failed.\r\nIs Outlook running?\r\nNetwork error?", "Outlook Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cursor = Cursors.Default;

            }

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
        private void AddColumnsProgrammatically()
        {
            var col0 = new DataGridViewTextBoxColumn();
            var col1 = new DataGridViewTextBoxColumn();
            var col2 = new DataGridViewTextBoxColumn();
            var col3 = new DataGridViewTextBoxColumn();
            var col4 = new DataGridViewTextBoxColumn();
            var col5 = new DataGridViewTextBoxColumn();

            col0.HeaderText = "Id";
            col0.Name = "Id";
            col0.ReadOnly = true;
            col0.Visible = false;

            col1.HeaderText = "Name";
            col1.Name = "Name";
            col1.ReadOnly = true;
            col1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            col2.HeaderText = "Outlook Key";
            col2.Name = "Outlook_Key";
            col2.Width = 600;
            col2.ReadOnly = true;

            col3.HeaderText = "Active";
            col3.Name = "PF_Active_Ind";
            col3.ReadOnly = true;
            col3.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            col4.HeaderText = "Mod_Date";
            col4.Name = "Mod_Date";
            col4.ReadOnly = true;
            col4.Visible = false;

            col5.HeaderText = "Mod_User_id";
            col5.Name = "Mod_User_id";
            col5.ReadOnly = true;
            col5.Visible = false;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2, col3, col4, col5 });

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
        private void SizeAllColumns(Object sender, EventArgs e)
        {
            Column_Size();
        }
        private void Column_Size()
        {
            dataGridView1.AutoResizeColumn(0, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(1, DataGridViewAutoSizeColumnMode.AllCells);
            //dataGridView1.AutoResizeColumn(2, DataGridViewAutoSizeColumnMode.AllCells); // Added BN
            dataGridView1.AutoResizeColumn(3, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(4, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(5, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(6, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(7, DataGridViewAutoSizeColumnMode.AllCells);
        }
        private void populate_datagridview()
        {
            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();

            DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Customer.Get_Info();
            DataRow dr_Get_Info;

            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                dataGridView1.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_Id = new DataGridViewTextBoxCell();
                DGVC_Id.Value = dr_Get_Info["Customer_Id"].ToString();
                dataGridView1.Rows[i].Cells["Id"] = DGVC_Id;

                DataGridViewCell DGVC_Code = new DataGridViewTextBoxCell();
                DGVC_Code.Value = dr_Get_Info["Name"].ToString();
                dataGridView1.Rows[i].Cells["Name"] = DGVC_Code;

                DataGridViewCell DGVC_Description = new DataGridViewTextBoxCell();
                DGVC_Description.Value = dr_Get_Info["Outlook_Key"].ToString();
                dataGridView1.Rows[i].Cells["Outlook_Key"] = DGVC_Description;

                DataGridViewCell DGVC_Active = new DataGridViewTextBoxCell();
                DGVC_Active.Value = dr_Get_Info["PF_Active_Ind"].ToString();
                dataGridView1.Rows[i].Cells["PF_Active_Ind"] = DGVC_Active;

                DataGridViewCell DGVC_Mod_date = new DataGridViewTextBoxCell();
                DGVC_Mod_date.Value = dr_Get_Info["Mod_Date"].ToString();
                dataGridView1.Rows[i].Cells["Mod_Date"] = DGVC_Mod_date;

                DataGridViewCell DGVC_Mod_User_Id = new DataGridViewTextBoxCell();
                DGVC_Mod_User_Id.Value = dr_Get_Info["Mod_User_Id"].ToString();
                dataGridView1.Rows[i].Cells["Mod_User_id"] = DGVC_Mod_User_Id;

                Column_Size();
                ds_Get_Info.Dispose();
            }
        }
        private void populate_outlook_Combo()
        {
            Console.WriteLine("populate_outlook_Combo - Begin");
            Cursor = Cursors.WaitCursor;
            cmb_Outlook.DataSource = null;

            // Folder_Name_Location is returned in a COM object (BN 22-04-2015)
            if (FruPak.PF.Data.Outlook.Outlook.Folder_Name_Location == null)
            {
                Console.WriteLine("FruPak.PF.Data.Outlook.Outlook.Folder_Name_Location == null");
                logger.Log(LogLevel.Debug, "FruPak.PF.Data.Outlook.Outlook.Folder_Name_Location == null");
            }

            try
            {
                FruPak.PF.Data.Outlook.Outlook.Folder_Name_Location = FruPak.PF.Data.Outlook.Outlook.Folder_Name_Location;

                // Contact_List_All returns a DataSet with the following fields:
                // combined, company, FirstName, LastName, JobTitle, Entry_Id.
                // ---------------------------------------------------------------------- (BN 22-04-2015)
                DataSet ds_Contacts = FruPak.PF.Data.Outlook.Contacts.Contact_List_All();
                cmb_Outlook.DataSource = ds_Contacts.Tables[0];
                cmb_Outlook.DisplayMember = "Combined";
                cmb_Outlook.ValueMember = "Entry_Id";
                cmb_Outlook.Text = null;
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, "populate_outlook_Combo: " + ex.Message);
                MessageBox.Show("populate_outlook_Combo just failed.\r\nIs Outlook running?", "Outlook Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cursor = Cursors.Default;
            }

            Cursor = Cursors.Default;
            Console.WriteLine("populate_outlook_Combo - End");
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
            txt_name.ResetText();
            cmb_Outlook.Text = null;
            btn_Add.Text = "&Add";
        }
        private void btn_Add_Click(object sender, EventArgs e)
        {
            DialogResult DLR_MessageBox = new DialogResult();
            string str_msg = "";
            int int_result = 0;

            if (cmb_Outlook.SelectedIndex == -1)
            {
                str_msg = str_msg + "Invalid Outlook Link. PLease select a valid Outlook link from the Drop down list." + Environment.NewLine;
            }

            if (txt_name.TextLength == 0)
            {
                str_msg = str_msg + "Invalid Name. Please enter a valid name." + Environment.NewLine;
            }

            if (str_msg.Length > 0)
            {
                DLR_MessageBox = MessageBox.Show(str_msg, "Process Factory - Customers", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (DLR_MessageBox != System.Windows.Forms.DialogResult.OK)
            {
                switch (btn_Add.Text)
                {
                    case "&Add":
                        int_result = FruPak.PF.Data.AccessLayer.PF_Customer.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Customer"), txt_name.Text, cmb_Outlook.SelectedValue.ToString(), ckb_Active.Checked, int_Current_User_Id);
                        break;
                    case "&Update":
                        int_result = FruPak.PF.Data.AccessLayer.PF_Customer.Update(int_DVG_Row_id, txt_name.Text, cmb_Outlook.SelectedValue.ToString(), ckb_Active.Checked, int_Current_User_Id);
                        break;
                }

            }
            if (int_result > 0)
            {
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                lbl_message.Text = txt_name.Text + " has been saved";
                Reset();
            }
            else
            {
                lbl_message.ForeColor = System.Drawing.Color.Red;
                lbl_message.Text = txt_name.Text + " has NOT been saved";
            }
            populate_datagridview();
            Reset();
        }

        private void cmb_Outlook_SelectionChangeCommitted(object sender, EventArgs e)
        {
            // BN 21/01/2015 - Just discovered if you accidentally click on the wrong entry in the drop-down list,
            // you'll cause a crash.
            //
            FruPak.PF.Data.Outlook.Contacts.Contact_Details(cmb_Outlook.SelectedValue.ToString());

            // BN 5/8/2015 - The dude who originally wrote this was too lazy to
            // check for nulls, and preferred to catch exceptions instead. Lazy
            // git.

            // Exceptions are expensive to catch in terms of processor time.
            if (FruPak.PF.Data.Outlook.Contacts.Contact_Company_Name != "" &&
                FruPak.PF.Data.Outlook.Contacts.Contact_Company_Name != null)
            {
                txt_name.Text = FruPak.PF.Data.Outlook.Contacts.Contact_Company_Name.ToString();
            }
            else
            {
                txt_name.Text = FruPak.PF.Data.Outlook.Contacts.Contact_First_Name.ToString() + " " + FruPak.PF.Data.Outlook.Contacts.Contact_Last_Name.ToString();
            }

            //try
            //{
            //    txt_name.Text = FruPak.PF.Data.Outlook.Contacts.Contact_Company_Name.ToString();
            //}
            //catch
            //{
            //    try
            //    {
            //        txt_name.Text = FruPak.PF.Data.Outlook.Contacts.Contact_First_Name.ToString() + " " + FruPak.PF.Data.Outlook.Contacts.Contact_Last_Name.ToString();
            //    }
            //    catch (Exception ex)
            //    {
            //        logger.Log(LogLevel.Debug, ex.Message);
            //    }
            //}

        }
        private int int_DVG_Row_id = 0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult DLR_Message = new DialogResult();
            int int_result = 0;

            //Delete
            if (e.ColumnIndex == 6)
            {
                string str_msg;

                str_msg = "You are about to delete " + Environment.NewLine;
                str_msg = str_msg + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + Environment.NewLine;
                str_msg = str_msg + "Do you want to continue?";
                DLR_Message = MessageBox.Show(str_msg, "Customer - Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (DLR_Message == DialogResult.Yes)
                {
                    int_result = FruPak.PF.Data.AccessLayer.PF_Customer.Delete(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
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
            else if (e.ColumnIndex == 7)    // Edit - Additions: 22-04-2015 BN
            {
                int_DVG_Row_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());

                // Here we have the name of the customer selected from the DataGrid (Original code)
                txt_name.Text = dataGridView1.Rows[e.RowIndex].Cells["Name"].Value.ToString();

                // ----------------------------------------------------------------------------------[ Addition 1 begin ]---
                // Add a convenience search to the cmb_Outlook ComboBox to find the first (if any)
                // occurrence of the selected customer's name
                cmb_Outlook.DroppedDown = true;

                int nameIndex = cmb_Outlook.FindString(txt_name.Text, 0); // Doesn't have to be an exact match
                cmb_Outlook.SelectedIndex = nameIndex;
                // ----------------------------------------------------------------------------------[ Addition 1 end   ]---

                // Original code
                cmb_Outlook.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells["Outlook_Key"].Value.ToString();

                // ----------------------------------------------------------------------------------[ Addition 2 begin ]---
                // I eventually got it to work by placing the SelectedText call AFTER the SelectedValue call.
                // It took a good few hours to figure out why it wasn't working. BN 22-04-2015
                cmb_Outlook.SelectedText = dataGridView1.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                // ----------------------------------------------------------------------------------[ Addition 2 end   ]---

                ckb_Active.Checked = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells["PF_Active_Ind"].Value.ToString());
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
        private void PF_Customer_KeyDown(object sender, KeyEventArgs e)
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
