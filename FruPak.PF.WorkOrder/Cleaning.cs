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
    public partial class Cleaning : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();     

        private static bool bol_write_access;
        private static int int_Current_User_Id = 0;
        private static int int_CleanAreaCmp_Id = 0;
        private static int int_cmb_Staff_SelectedValue = 0;
        private static string str_dtp_start_Time_ValueChanged = "";
        private static string str_dtp_finish_Time_ValueChanged = "";
        private static string str_txt_comments_TextChanged = "";
        private static int int_selected_CleaningArea_Id = 1;
        private static string str_btn_Base_text = "Add";

        public Cleaning(int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();
            int_Current_User_Id = int_C_User_id;

            //restrict access
            bol_write_access = bol_w_a;
            //check if testing or not


            //if (FruPak.PF.Global.Global.bol_Testing == true)
            //{
            //    this.Text = "FruPak Process Factory - " + this.Text + " - Test Environment";
            //}
            //else
            //{
            //    this.Text = "FruPak Process Factory";
            //}
            AddTabPages();
            update_checked_Status();
            tabpage_visibility();

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
        private void AddTabPages()
        {
            
            DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Cleaning_Area.Get_Info();
            DataRow dr_Get_Info;

            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                TabPage new_tab_page = new TabPage(dr_Get_Info["Code"].ToString());
                new_tab_page.Name = dr_Get_Info["Code"].ToString();
                new_tab_page.Tag = dr_Get_Info["CleanArea_Id"].ToString();
 
                tbc_Cleaning.TabPages.Add(new_tab_page);                

                //Staff
                Label lbl_staff = new Label();
                lbl_staff.Name = "lbl_staff";
                lbl_staff.Text = "Completed By:";
                lbl_staff.Size = new System.Drawing.Size(80, 13);
                lbl_staff.Location = new System.Drawing.Point(12, 15);
                tbc_Cleaning.TabPages[dr_Get_Info["Code"].ToString()].Controls.Add(lbl_staff);

                ComboBox cmb_Staff = new ComboBox();
                cmb_Staff.Name = "cmb_Staff";
                cmb_Staff.Size = new System.Drawing.Size(121, 21);
                cmb_Staff.Location = new System.Drawing.Point(90, 12);
                cmb_Staff.DataSource = null;

                DataSet ds_Get_Info2;
                ds_Get_Info2 = FruPak.PF.Data.AccessLayer.PF_Staff.Get_Info();
                cmb_Staff.DataSource = ds_Get_Info2.Tables[0];
                cmb_Staff.DisplayMember = "Name";
                cmb_Staff.ValueMember = "Staff_Id";
                cmb_Staff.SelectedValueChanged += new EventHandler(cmb_Staff_SelectedValueChanged); 
                cmb_Staff.Text = null;
                ds_Get_Info2.Dispose();
                tbc_Cleaning.TabPages[dr_Get_Info["Code"].ToString()].Controls.Add(cmb_Staff);               

                //start time 
                Label lbl_start = new Label();
                lbl_start.Name = "lbl_start";
                lbl_start.Text = "Start:";
                lbl_start.Size = new System.Drawing.Size(35, 13);
                lbl_start.Location = new System.Drawing.Point(230, 15);
                tbc_Cleaning.TabPages[dr_Get_Info["Code"].ToString()].Controls.Add(lbl_start);

                DateTimePicker dtp_start_Time = new DateTimePicker();
                dtp_start_Time.Name = "dtp_start_Time";
                dtp_start_Time.Format = DateTimePickerFormat.Time;
                dtp_start_Time.ShowUpDown = true;
                dtp_start_Time.Value = DateTime.Now;
                dtp_start_Time.Size = new System.Drawing.Size(100, 20);
                dtp_start_Time.Location = new System.Drawing.Point(265, 12);
                dtp_start_Time.ValueChanged += new EventHandler(dtp_start_Time_ValueChanged); 
                tbc_Cleaning.TabPages[dr_Get_Info["Code"].ToString()].Controls.Add(dtp_start_Time);

                //finish time 
                Label lbl_finish = new Label();
                lbl_finish.Name = "lbl_finish";
                lbl_finish.Text = "Finish:";
                lbl_finish.Size = new System.Drawing.Size(40, 13);
                lbl_finish.Location = new System.Drawing.Point(400, 15);
                tbc_Cleaning.TabPages[dr_Get_Info["Code"].ToString()].Controls.Add(lbl_finish);

                DateTimePicker dtp_finish_Time = new DateTimePicker();
                dtp_finish_Time.Name = "dtp_finish_Time";
                dtp_finish_Time.Format = DateTimePickerFormat.Time;
                dtp_finish_Time.ShowUpDown = true;
                dtp_finish_Time.Value = DateTime.Now;
                dtp_finish_Time.Size = new System.Drawing.Size(100, 20);
                dtp_finish_Time.Location = new System.Drawing.Point(440, 12);
                dtp_finish_Time.ValueChanged += new EventHandler(dtp_finish_Time_ValueChanged); 
                tbc_Cleaning.TabPages[dr_Get_Info["Code"].ToString()].Controls.Add(dtp_finish_Time);

                Button btn_Base = new Button();
                btn_Base.Name = "btn_Base";
                btn_Base.Text = "Create";
                btn_Base.Size = new System.Drawing.Size(75, 20);
                btn_Base.UseVisualStyleBackColor = true;
                btn_Base.Location = new System.Drawing.Point(600, 12);
                btn_Base.Click += new System.EventHandler(btn_Base_Click);
                tbc_Cleaning.TabPages[dr_Get_Info["Code"].ToString()].Controls.Add(btn_Base);

                //add checkboxes
                int y = 50;
                int x = 10;
                int count = 0;
                ds_Get_Info2 = FruPak.PF.Data.AccessLayer.PF_Clean_Area_Area_Parts_Relationship.Get_Info_Area_Translated(Convert.ToInt32(dr_Get_Info["CleanArea_Id"].ToString()));
                DataRow dr_Get_Info2;

                for (int icb = 0; icb < Convert.ToInt32(ds_Get_Info2.Tables[0].Rows.Count.ToString()); icb++)
                {
                    count++;
                    dr_Get_Info2 = ds_Get_Info2.Tables[0].Rows[icb];
                    CheckBox current = new CheckBox();
                    current.Location = new Point(x, y);
                    current.Tag = dr_Get_Info2["CleanAreaParts_Id"].ToString();
                    current.Name = dr_Get_Info2["Code"].ToString();
                    current.Text = dr_Get_Info2["Description"].ToString();

                    current.Width = 175;
                    current.CheckedChanged += new EventHandler(check_CheckedChanged);
                    tbc_Cleaning.TabPages[dr_Get_Info["Code"].ToString()].Controls.Add(current);

                    y += 25;
                    if (count == 5)
                    {                       
                        x += 200;
                        y = 50;
                        count = 0;
                    }
                }
                ds_Get_Info2.Dispose();

                //Comments Box
                Label lbl_comments = new Label();
                lbl_comments.Name = "lbl_comments";
                lbl_comments.Text = "Comments:";
                lbl_comments.Size = new System.Drawing.Size(60, 13);
                lbl_comments.Location = new System.Drawing.Point(12, 182);
                tbc_Cleaning.TabPages[dr_Get_Info["Code"].ToString()].Controls.Add(lbl_comments);

                TextBox txt_comments = new TextBox();
                txt_comments.Name = "txt_comments";
                txt_comments.Multiline = true;
                txt_comments.Size = new System.Drawing.Size(600, 40);
                txt_comments.Location = new System.Drawing.Point(70, 180);
                txt_comments.TextChanged += new EventHandler(txt_comments_TextChanged);
                tbc_Cleaning.TabPages[dr_Get_Info["Code"].ToString()].Controls.Add(txt_comments);

                //add image to tab
                tab_image_load();

            }
            ds_Get_Info.Dispose();
        }

        private void tab_image_load()
        {
            DataSet ds_Get_Info2;
            DataRow dr_Get_Info2;
            //add image to tab
            ds_Get_Info2 = FruPak.PF.Data.AccessLayer.PF_Work_Order.Get_Cleaning(dtp_Completed.Value.ToString("yyyy/MM/dd"));


            foreach (TabPage tbp in tbc_Cleaning.TabPages)
            {
                tbp.ImageIndex = -1;
            }
            for (int i2 = 0; i2 < Convert.ToInt32(ds_Get_Info2.Tables[0].Rows.Count.ToString()); i2++)
            {
                dr_Get_Info2 = ds_Get_Info2.Tables[0].Rows[i2];

                foreach (TabPage tbp in tbc_Cleaning.TabPages)
                {
                    try
                    {
                        if (Convert.ToInt32(dr_Get_Info2["CleanArea_Id"].ToString()) == Convert.ToInt32(tbp.Tag.ToString()))
                        {
                            tbp.ImageIndex = 2;
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Log(LogLevel.Debug, ex.Message);
                    }                    

                    //check that all checkboxes have been ticked
                    Load_Tab_Images();
                }
            }
            ds_Get_Info2.Dispose();
        }
        private void btn_Base_Click(Object sender, EventArgs e)
        {
            DialogResult DLR_MessageBox = new DialogResult();
            string str_msg = "";
            int int_result = 0;

            if (int_cmb_Staff_SelectedValue == 0)
            {
                str_msg = str_msg + "Invalid Staff Member. Please select a vaild Staff Memeber from the Dropdown List" + Environment.NewLine;
            }
            if (str_dtp_start_Time_ValueChanged == "")
            {
                str_dtp_start_Time_ValueChanged = DateTime.Now.ToString("HH:mm");
            }
            if (str_dtp_finish_Time_ValueChanged == "")
            {
                str_dtp_finish_Time_ValueChanged = DateTime.Now.ToString("HH:mm");
            }

            if (str_msg.Length > 0)
            {
                DLR_MessageBox = MessageBox.Show(str_msg, "Cleaning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (DLR_MessageBox != System.Windows.Forms.DialogResult.OK)
            {
                switch (str_btn_Base_text)
                {
                    case "Add":
                        int_result = FruPak.PF.Data.AccessLayer.PF_Cleaning_Area_Completed.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Cleaning_Area_Completed"), dtp_Completed.Value.ToString("yyyy/MM/dd"), Convert.ToInt32(tbc_Cleaning.SelectedTab.Tag), int_cmb_Staff_SelectedValue, str_dtp_start_Time_ValueChanged, str_dtp_finish_Time_ValueChanged, str_txt_comments_TextChanged, int_Current_User_Id);
                        break;
                    case "Update":
                        int_result = FruPak.PF.Data.AccessLayer.PF_Cleaning_Area_Completed.Update(int_CleanAreaCmp_Id, dtp_Completed.Value.ToString("yyyy/MM/dd"), int_cmb_Staff_SelectedValue, str_dtp_start_Time_ValueChanged, str_dtp_finish_Time_ValueChanged, str_txt_comments_TextChanged, int_Current_User_Id);
                        break;
                }
                
            }
            update_checked_Status();
            tabpage_visibility();
        }        
        private void cmb_Staff_SelectedValueChanged(Object sender, EventArgs e)
        {
            try
            {
                if ((sender as ComboBox).SelectedValue != null)
                {
                    int_cmb_Staff_SelectedValue = Convert.ToInt32((sender as ComboBox).SelectedValue.ToString());
                }
                else
                {
                    // Phantom 15/12/2014
                    logger.Log(LogLevel.Warn, "Avoided error due to ComboBox Staff SelectedValue being null");
                }
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message);
            }
        }
        private void dtp_start_Time_ValueChanged(Object sender, EventArgs e)
        {
            str_dtp_start_Time_ValueChanged = (sender as DateTimePicker).Value.ToString("HH:mm");            
        }
        private void dtp_finish_Time_ValueChanged(Object sender, EventArgs e)
        {
            str_dtp_finish_Time_ValueChanged = (sender as DateTimePicker).Value.ToString("HH:mm");
        }
        private void txt_comments_TextChanged(Object sender, EventArgs e)
        {
            str_txt_comments_TextChanged = (sender as TextBox).Text.ToString();
        }
        private void check_CheckedChanged(Object sender, EventArgs e)
        {
                DataSet ds_Get_Info;
                DataRow dr_Get_Info;
                ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Cleaning_Area_Completed.Get_Info(int_selected_CleaningArea_Id, dtp_Completed.Value.ToString("yyyy/MM/dd"));

                if (Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()) > 0)
                {
                   
                    for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
                    {
                        dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                        int_CleanAreaCmp_Id = Convert.ToInt32(dr_Get_Info["CleanAreaCmp_Id"].ToString());                       
                    }
                }
                ds_Get_Info.Dispose();
                ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Cleaning_Area_Parts_Completed.Get_Info(int_CleanAreaCmp_Id, Convert.ToInt32((sender as CheckBox).Tag.ToString()), dtp_Completed.Value.ToString("yyyy-MM-dd"));

                if (Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()) > 0)
                {

                    for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
                    {
                        dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                        FruPak.PF.Data.AccessLayer.PF_Cleaning_Area_Parts_Completed.Delete(Convert.ToInt32(dr_Get_Info["CleanAreaPartCmp_Id"].ToString()));
                    }
                    

                }
                else
                {
                    if ((sender as CheckBox).Checked == true && bol_djb == false)
                    {
                        FruPak.PF.Data.AccessLayer.PF_Cleaning_Area_Parts_Completed.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Cleaning_Area_Parts_Completed"), int_CleanAreaCmp_Id, Convert.ToInt32((sender as CheckBox).Tag.ToString()), int_Current_User_Id);
                    }
                }
            //check that all checkboxes have been ticked
            Load_Tab_Images();
        }

        private void Load_Tab_Images()
        {
            //check that all checkboxes have been ticked
            int int_ckb_count = 0;
            foreach (Control control in tbc_Cleaning.TabPages[tbc_Cleaning.SelectedTab.Name].Controls)
            {
                if (control.GetType().ToString() == "System.Windows.Forms.CheckBox")
                {
                    switch ((control as CheckBox).Checked)
                    {
                        case true:
                            int_ckb_count = int_ckb_count + 0;
                            break;
                        case false:
                            int_ckb_count = int_ckb_count + 1;
                            break;
                    }
                }
            }
            int int_Current_image_index = tbc_Cleaning.TabPages[tbc_Cleaning.SelectedTab.Name].ImageIndex;
            if (int_ckb_count == 0)
            {
                tbc_Cleaning.TabPages[tbc_Cleaning.SelectedTab.Name].ImageIndex = 0;
            }
            else
            {
                if (int_Current_image_index > -1)
                {
                    tbc_Cleaning.TabPages[tbc_Cleaning.SelectedTab.Name].ImageIndex = 2;
                }
            }
            //first tab does NOT require an image
            tbc_Cleaning.TabPages[0].ImageIndex = -1;
        }
        private void tbc_Cleaning_SelectedIndexChanged(object sender, EventArgs e)
        {
            int_selected_CleaningArea_Id = Convert.ToInt32(tbc_Cleaning.SelectedTab.Tag);
            update_checked_Status();
            tabpage_visibility();
        }        
        private void tabpage_visibility()
        {
            try
            {
                DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Cleaning_Area_Completed.Get_Info(int_selected_CleaningArea_Id, dtp_Completed.Value.ToString("yyyy/MM/dd"));
                
                if (Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()) > 0)
                {
                    DataRow dr_Get_Info;
                    for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
                    {
                        dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                        int_CleanAreaCmp_Id = Convert.ToInt32(dr_Get_Info["CleanAreaCmp_Id"].ToString());

                        foreach (Control control in tbc_Cleaning.TabPages[tbc_Cleaning.SelectedTab.Name].Controls)
                        {
                            switch (control.Name.ToString())
                            {
                                case "btn_Base":
                                    control.Text = "Update";
                                    str_btn_Base_text = "Update";
                                    break;
                                case "cmb_Staff":
                                    DataSet ds_Get_Info2 = FruPak.PF.Data.AccessLayer.PF_Staff.Get_Info(Convert.ToInt32(dr_Get_Info["Staff_Id"].ToString()));
                                    DataRow dr_Get_Info2;
                                    for (int i2 = 0; i2 < Convert.ToInt32(ds_Get_Info2.Tables[0].Rows.Count.ToString()); i2++)
                                    {
                                        dr_Get_Info2 = ds_Get_Info2.Tables[0].Rows[i2];
                                        control.Text = dr_Get_Info2["Name"].ToString();
                                    }
                                    ds_Get_Info2.Dispose();                                    
                                    break;
                                case "dtp_start_Time":
                                    control.Text = dr_Get_Info["Start_Time"].ToString();
                                    break;
                                case "dtp_finish_Time":
                                    control.Text = dr_Get_Info["Finish_Time"].ToString();
                                    break;
                                case "txt_comments":
                                    control.Text = dr_Get_Info["Comments"].ToString();
                                    break;
                                default:
                                    control.Visible = true;
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    foreach (Control control in tbc_Cleaning.TabPages[tbc_Cleaning.SelectedTab.Name].Controls)
                    {
                        switch (control.Name.ToString())
                        {
                            case "btn_Base":
                                control.Text = "Add";
                                str_btn_Base_text = "Add";
                                control.Visible = true;
                                break;
                            case "lbl_staff":                           
                            case "lbl_start":
                            case "dtp_start_Time":
                            case "lbl_finish":
                            case "dtp_finish_Time":
                            case "lbl_comments":
                            case "pic_Cleaner":
                                control.Visible = true;
                                break;
                            case "txt_comments":
                                control.Text = null;
                                break;
                            case "cmb_Staff":
                                control.Text = null;
                                (control as ComboBox).Text = null;
                                (control as ComboBox).SelectedIndex = -1;
                                (control as ComboBox).SelectedItem = -1;
                                (control as ComboBox).SelectedValue = -1;
                                (control as ComboBox).SelectedText = null;
                                break;
                            default:
                                control.Visible = false;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message);
            }
        }
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dtp_Completed_ValueChanged(object sender, EventArgs e)
        {
            update_checked_Status();
            tabpage_visibility();
            tab_image_load();
        }
        private static bool bol_djb = false;
        private void update_checked_Status()
        {
            foreach (Control control in tbc_Cleaning.TabPages[tbc_Cleaning.SelectedTab.Name].Controls)
            {

                if (control.GetType().ToString() == "System.Windows.Forms.CheckBox")
                {

                    DataSet ds_Get_Info3 = FruPak.PF.Data.AccessLayer.PF_Cleaning_Area_Parts_Completed.Get_Info(Convert.ToInt32(tbc_Cleaning.SelectedTab.Tag.ToString()), Convert.ToInt32(control.Tag.ToString()), dtp_Completed.Value.Date.ToString("yyyy-MM-dd"));
                    if (Convert.ToInt32(ds_Get_Info3.Tables[0].Rows.Count.ToString()) > 0)
                    {
                        DataRow dr_Get_Info3;
                        for (int i3 = 0; i3 < Convert.ToInt32(ds_Get_Info3.Tables[0].Rows.Count.ToString()); i3++)
                        {
                            dr_Get_Info3 = ds_Get_Info3.Tables[0].Rows[i3];
                            bol_djb = true;
                            (control as CheckBox).Checked = true;
                            
                        }
                    }
                    else
                    {
                        bol_djb = false;
                        (control as CheckBox).Checked = false;
                        
                    }
                    ds_Get_Info3.Dispose();
                }
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
        private void Cleaning_KeyDown(object sender, KeyEventArgs e)
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
