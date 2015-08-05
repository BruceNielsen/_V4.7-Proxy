#region Imports (9)

using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

#endregion Imports (9)

namespace FruPak.PF.Utils.Common
{
    public partial class Common_Maintenance : Form
    {
        #region Members of Common_Maintenance (11)
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static string str_table = "";
        private static int int_Current_User_Id = 0;
        private static int int_DVG_Row_id = 0;
        private static bool bol_write_access;
        private static DataSet ds_Get_Info;
        ///  <summary>
        ///  validates the text fields on length
        ///  </summary>
        ///  <returns></returns>
        private DataSet ds_validate;
        private string openPad = " --- [ ";
        private string closePad = " ] --- ";
        private string intro = "--->   { ";
        private string outro = " }   <---";

        #endregion Members of Common_Maintenance (11)

        #region Constructors of Common_Maintenance (1)

        public Common_Maintenance(string str_type, int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();
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
            str_table = str_type;
            int_Current_User_Id = int_C_User_id;
            this.Text = str_type + " Maintenance";
            AddColumnsProgrammatically();
            populate_datagridview();
            switch (str_table)
            {
                case "CM_Pack_Type":
                    lbl_tare_weight.Visible = true;
                    nud_Tare_Weight.Visible = true;
                    btn_Update_Relationship.Visible = true;
                    break;
                default:
                    lbl_tare_weight.Visible = false;
                    nud_Tare_Weight.Visible = false;
                    btn_Update_Relationship.Visible = false;
                    break;

            }

            AddColumnsProgrammatically2();

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

        #endregion Constructors of Common_Maintenance (1)

        #region Methods of Common_Maintenance (25)

        private void Add_btn()
        {
            DialogResult DLR_MessageBox = new DialogResult();
            string str_msg = "";
            int int_result = 0;

            str_msg = validate(str_table, btn_Add.Text);
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
                            case "CM_Brand":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Brand.Insert(FruPak.PF.Common.Code.General.int_max_user_id("CM_Brand"), txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "CM_Cities":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Cities.Insert(FruPak.PF.Common.Code.General.int_max_user_id("CM_Cities"), txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "CM_Count":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Count.Insert(FruPak.PF.Common.Code.General.int_max_user_id("CM_Count"), txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "CM_Defect":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Defect.Insert(FruPak.PF.Common.Code.General.int_max_user_id("CM_Defect"), txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "CM_Defect_Class":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Defect_Class.Insert(FruPak.PF.Common.Code.General.int_max_user_id("CM_Defect_Class"), txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "CM_ESP":
                                int_result = FruPak.PF.Data.AccessLayer.CM_ESP.Insert(FruPak.PF.Common.Code.General.int_max_user_id("CM_ESP"), txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "CM_Fruit_Type":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Fruit_Type.Insert(FruPak.PF.Common.Code.General.int_max_user_id("CM_Fruit_Type"), txt_code.Text, txt_Description.Text, int_Current_User_Id);
                                break;
                            case "CM_Grade":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Grade.Insert(FruPak.PF.Common.Code.General.int_max_user_id("CM_Grade"), txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "CM_Growing_Method":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Growing_Method.Insert(FruPak.PF.Common.Code.General.int_max_user_id("CM_Growing_Method"), txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "CM_Market_Attribute":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Market_Attribute.Insert(FruPak.PF.Common.Code.General.int_max_user_id("CM_Market_Attribute"), txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "CM_Location":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Location.Insert(FruPak.PF.Common.Code.General.int_max_user_id("CM_Location"), txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "CM_Pack_Type":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Pack_Type.Insert(FruPak.PF.Common.Code.General.int_max_user_id("CM_Pack_Type"), txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "CM_Pallet_Type":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Pallet_Type.Insert(FruPak.PF.Common.Code.General.int_max_user_id("CM_Pallet_Type"), txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "CM_Product_Group":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Product_Group.Insert(FruPak.PF.Common.Code.General.int_max_user_id("CM_Product_Group"), txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "CM_Size":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Size.Insert(FruPak.PF.Common.Code.General.int_max_user_id("CM_Size"), txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "CM_Storage":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Storage.Insert(FruPak.PF.Common.Code.General.int_max_user_id("CM_Storage"), txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "CM_Treatment":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Treatment.Insert(FruPak.PF.Common.Code.General.int_max_user_id("CM_Treatment"), txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "CM_Truck":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Truck.Insert(FruPak.PF.Common.Code.General.int_max_user_id("CM_Truck"), txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "PF_Other_Work_Types":
                                int_result = FruPak.PF.Data.AccessLayer.PF_Other_Work_Types.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Other_Work_Types"), txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                        }
                        break;
                    case "&Update":
                        switch (str_table)
                        {
                            case "CM_Brand":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Brand.Update(int_DVG_Row_id, txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "CM_Cities":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Cities.Update(int_DVG_Row_id, txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "CM_Count":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Count.Update(int_DVG_Row_id, txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "CM_Defect":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Defect.Update(int_DVG_Row_id, txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "CM_Defect_Class":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Defect_Class.Update(int_DVG_Row_id, txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "CM_ESP":
                                int_result = FruPak.PF.Data.AccessLayer.CM_ESP.Update(int_DVG_Row_id, txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "CM_Fruit_Type":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Fruit_Type.Update(int_DVG_Row_id, txt_code.Text, txt_Description.Text, int_Current_User_Id);
                                break;
                            case "CM_Grade":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Grade.Update(int_DVG_Row_id, txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "CM_Growing_Method":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Growing_Method.Update(int_DVG_Row_id, txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "CM_Market_Attribute":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Market_Attribute.Update(int_DVG_Row_id, txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "CM_Location":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Location.Update(int_DVG_Row_id, txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "CM_Pack_Type":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Pack_Type.Update(int_DVG_Row_id, txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "CM_Pallet_Type":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Pallet_Type.Update(int_DVG_Row_id, txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "CM_Product_Group":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Product_Group.Update(int_DVG_Row_id, txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "CM_Size":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Size.Update(int_DVG_Row_id, txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "CM_Storage":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Storage.Update(int_DVG_Row_id, txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "CM_Treatment":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Treatment.Update(int_DVG_Row_id, txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "CM_Truck":
                                int_result = FruPak.PF.Data.AccessLayer.CM_Truck.Update(int_DVG_Row_id, txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
                                break;
                            case "PF_Other_Work_Types":
                                int_result = FruPak.PF.Data.AccessLayer.PF_Other_Work_Types.Update(int_DVG_Row_id, txt_code.Text, txt_Description.Text, ckb_Active.Checked, int_Current_User_Id);
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

        private void AddColumnsProgrammatically()
        {
            var col1 = new DataGridViewTextBoxColumn();
            var col2 = new DataGridViewTextBoxColumn();
            var col3 = new DataGridViewTextBoxColumn();
            var col4 = new DataGridViewTextBoxColumn();
            var col5 = new DataGridViewTextBoxColumn();
            var col6 = new DataGridViewTextBoxColumn();

            col1.HeaderText = "Id";
            col1.Name = "Id";
            col1.ReadOnly = true;
            col1.Visible = false;

            col2.HeaderText = "Code";
            col2.Name = "Code";
            col2.ReadOnly = true;

            col3.HeaderText = "Description";
            col3.Name = "Description";
            col3.Width = 300;
            col3.ReadOnly = true;

            col4.HeaderText = "Active";
            col4.Name = "Active";
            col4.ReadOnly = true;

            col5.HeaderText = "Mod_Date";
            col5.Name = "Mod_Date";
            col5.ReadOnly = true;
            col5.Visible = false;

            col6.HeaderText = "Mod_User_id";
            col6.Name = "Mod_User_id";
            col6.ReadOnly = true;
            col6.Visible = false;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col1, col2, col3, col4, col5, col6 });

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

        private void AddColumnsProgrammatically2()
        {
            var col0 = new DataGridViewTextBoxColumn();
            var col1 = new DataGridViewTextBoxColumn();
            var col2 = new DataGridViewTextBoxColumn();
            var col3 = new DataGridViewTextBoxColumn();


            col0.HeaderText = "Id";
            col0.Name = "PackType_Weight_Id";
            col0.ReadOnly = true;
            col0.Visible = false;

            col1.HeaderText = "PackType_Id";
            col1.Name = "PackType_Id";
            col1.ReadOnly = true;
            col1.Visible = false;

            col2.HeaderText = "Weight_Tare";
            col2.Name = "Weight_Tare";

            col3.HeaderText = "PF_Active_Ind";
            col3.Name = "PF_Active_Ind";
            col3.ReadOnly = true;

            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2, col3 });
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            //? This can take quite a while, so give some visual feedback - BN 24-06-2015

            //-----------------------------------------
            // Demonstrate new commenting styles
            //! Dark Green
            //? Red
            //x Grey + Strikethrough for some reason
            //TODO Brown
            //!? Purple (WTF is going on-type comments)
            //-----------------------------------------
            // SharpComments (//x //- //+ //++ //! //!+ //!++ //? //?+ //?++ // TODO: // HACK: // UNDONE:)
            //!+ Now it works
            //!++ Now it works
            //x Well






            // Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor;

            Add_btn();

            // Set cursor as default arrow
            Cursor.Current = Cursors.Default;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btn_Update_Relationship_Click(object sender, EventArgs e)
        {
            DialogResult DLR_Message = new DialogResult();
            switch (str_table)
            {
                case "CM_Defect":
                    FruPak.PF.Data.AccessLayer.CM_Defect_Defect_Class_Relationship.Delete_Defect_From_Class(int_DVG_Row_id);
                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    {
                        int int_Product_Id = Convert.ToInt32(checkedListBox1.Items[i].ToString().Substring(0, checkedListBox1.Items[i].ToString().IndexOf(' ')));
                        if (checkedListBox1.GetItemCheckState(i) == CheckState.Checked)
                        {
                            FruPak.PF.Data.AccessLayer.CM_Defect_Defect_Class_Relationship.Insert(FruPak.PF.Common.Code.General.int_max_user_id("CM_Defect_Defect_Class_Relationship"), int_DVG_Row_id, int_Product_Id, int_Current_User_Id);
                        }
                        else
                        {
                            FruPak.PF.Data.AccessLayer.CM_Defect_Defect_Class_Relationship.Delete_Defect_From_Class(int_DVG_Row_id, int_Product_Id);
                        }
                    }
                    break;
                case "CM_Defect_Class":
                    FruPak.PF.Data.AccessLayer.CM_Defect_Defect_Class_Relationship.Delete_Class_From_Defect(int_DVG_Row_id);
                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    {
                        int int_Product_Id = Convert.ToInt32(checkedListBox1.Items[i].ToString().Substring(0, checkedListBox1.Items[i].ToString().IndexOf(' ')));
                        if (checkedListBox1.GetItemCheckState(i) == CheckState.Checked)
                        {
                            FruPak.PF.Data.AccessLayer.CM_Defect_Defect_Class_Relationship.Insert(FruPak.PF.Common.Code.General.int_max_user_id("CM_Defect_Defect_Class_Relationship"), int_Product_Id, int_DVG_Row_id, int_Current_User_Id);
                        }
                        else
                        {
                            FruPak.PF.Data.AccessLayer.CM_Defect_Defect_Class_Relationship.Delete_Defect_From_Class(int_Product_Id, int_DVG_Row_id);
                        }
                    }
                    break;
                case "CM_Product_Product_Group_Relationship":
                    FruPak.PF.Data.AccessLayer.CM_Product_Product_Group_Relationship.Delete_Product_From_Group(int_DVG_Row_id);
                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    {
                        int int_Product_Id = Convert.ToInt32(checkedListBox1.Items[i].ToString().Substring(0, checkedListBox1.Items[i].ToString().IndexOf(' ')));
                        if (checkedListBox1.GetItemCheckState(i) == CheckState.Checked)
                        {
                            FruPak.PF.Data.AccessLayer.CM_Product_Product_Group_Relationship.Insert(FruPak.PF.Common.Code.General.int_max_user_id("CM_Product_Product_Group_Relationship"), int_Product_Id, int_DVG_Row_id, int_Current_User_Id);
                        }
                        else
                        {
                            FruPak.PF.Data.AccessLayer.CM_Product_Product_Group_Relationship.Delete_Product_From_Group(int_Product_Id, int_DVG_Row_id);
                        }
                    }
                    break;
                case "CM_Pack_Type":
                    if (nud_Tare_Weight.Visible == true)
                    {
                        if (nud_Tare_Weight.Value == 0)
                        {
                            DLR_Message = MessageBox.Show("Invalid Tare Weight. PLease Enter a value greater and zero.", "Process Factory - Common", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            FruPak.PF.Data.AccessLayer.CM_Pack_Type_Weight.Insert(FruPak.PF.Common.Code.General.int_max_user_id("CM_Pack_Type_Weight"), Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value.ToString()), nud_Tare_Weight.Value, true, int_Current_User_Id);
                        }
                    }
                    else
                    {
                        if (Convert.ToDecimal(dataGridView2.CurrentRow.Cells["Weight_Tare"].Value.ToString()) == 0)
                        {
                            DLR_Message = MessageBox.Show("Invalid Tare Weight. PLease Enter a value greater and zero.", "Process Factory - Common", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            FruPak.PF.Data.AccessLayer.CM_Pack_Type_Weight.Update(Convert.ToInt32(dataGridView2.CurrentRow.Cells["PackType_Weight_Id"].Value.ToString()), Convert.ToInt32(dataGridView2.CurrentRow.Cells["PackType_Id"].Value.ToString()),
                                Convert.ToDecimal(dataGridView2.CurrentRow.Cells["Weight_Tare"].Value.ToString()), Convert.ToBoolean(dataGridView2.CurrentRow.Cells["PF_Active_Ind"].Value.ToString()), int_Current_User_Id);
                        }

                    }
                    break;
            }
        }

        /// <summary>
        /// Close the form with the Esc key (Sel request 11-02-2015 BN)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Common_Maintenance_KeyDown(object sender, KeyEventArgs e)
        {
            // Note that the KeyPreview property must be set to true for this to work
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void Control_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            logger.Log(LogLevel.Info, DecorateString(cb.Name, cb.Checked.ToString(), "CheckedChanged"));
        }

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

        private void Control_NudValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            logger.Log(LogLevel.Info, DecorateString(nud.Name, nud.Text, "NudValueChanged"));
        }

        private void Control_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            logger.Log(LogLevel.Info, DecorateString(cb.Name, cb.Text, "SelectedValueChanged"));
        }

        private void Control_Validated(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            logger.Log(LogLevel.Info, DecorateString(t.Name, t.Text, "Validated"));
        }

        private void Control_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;
            logger.Log(LogLevel.Info, DecorateString(dtp.Name, dtp.Text, "ValueChanged"));
        }

        private void CustomerControl_CustomerChanged(object sender, EventArgs e)
        {
            FruPak.PF.Utils.UserControls.Customer cust = (FruPak.PF.Utils.UserControls.Customer)sender;
            logger.Log(LogLevel.Info, DecorateString(cust.Name, cust.Customer_Name, "TextChanged"));
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int int_result = 0;

            //Delete
            if (e.ColumnIndex == 6)
            {
                int_result = FruPak.PF.Common.Code.General.Delete_Record(str_table, dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(),
                                                                         dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());

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
            //Edit
            else if (e.ColumnIndex == 7)
            {
                int_DVG_Row_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                txt_code.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txt_Description.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                ckb_Active.Checked = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                btn_Add.Text = "&Update";
                switch (str_table)
                {
                    case "CM_Defect":
                        populate_check_boxList();
                        checkedListBox1.Visible = true;
                        btn_Update_Relationship.Visible = true;
                        btn_Update_Relationship.Enabled = bol_write_access;
                        break;
                    case "CM_Defect_Class":
                        populate_check_boxList();
                        checkedListBox1.Visible = true;
                        btn_Update_Relationship.Visible = true;
                        btn_Update_Relationship.Enabled = bol_write_access;
                        break;
                    case "CM_Product_Group":
                        populate_check_boxList();
                        checkedListBox1.Visible = true;
                        btn_Update_Relationship.Visible = true;
                        btn_Update_Relationship.Enabled = bol_write_access;
                        break;
                    default:
                        checkedListBox1.Visible = false;
                        btn_Update_Relationship.Visible = false;
                        btn_Update_Relationship.Enabled = bol_write_access;
                        break;
                    case "CM_Pack_Type":
                        populate_dataGridView2();
                        btn_Update_Relationship.Visible = true;
                        btn_Update_Relationship.Text = "Update Tare Weight";
                        break;
                }
            }
        }

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

        private void Enter_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13)
            {
                Add_btn();
            }
        }

        private void KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void populate_check_boxList()
        {
            DataRow dr_Get_Info;
            checkedListBox1.Items.Clear();

            //get list of products
            switch (str_table)
            {
                case "CM_Defect":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Defect_Class.Get_Info();
                    break;
                case "CM_Defect_Class":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Defect.Get_Info();
                    break;
                case "CM_Product_Group":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Product.Get_Info();
                    break;
            }


            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                switch (str_table)
                {
                    case "CM_Defect":
                        checkedListBox1.Items.Add(dr_Get_Info["DefectClass_Id"].ToString() + " " + dr_Get_Info["Code"].ToString() + " " + dr_Get_Info["Description"].ToString());
                        break;
                    case "CM_Defect_Class":
                        checkedListBox1.Items.Add(dr_Get_Info["Defect_Id"].ToString() + " " + dr_Get_Info["Code"].ToString() + " " + dr_Get_Info["Description"].ToString());
                        break;
                    case "CM_Product_Group":
                        checkedListBox1.Items.Add(dr_Get_Info["Product_Id"].ToString() + " " + dr_Get_Info["Code"].ToString() + " " + dr_Get_Info["Description"].ToString());
                        break;
                }

            }
            ds_Get_Info.Dispose();

            //turn checks on or off according to current relationship data
            switch (str_table)
            {
                case "CM_Defect":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Defect_Defect_Class_Relationship.Get_Info_By_Defect(int_DVG_Row_id);
                    break;
                case "CM_Defect_Class":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Defect_Defect_Class_Relationship.Get_Info_By_DefectClass(int_DVG_Row_id);
                    break;
                case "CM_Product_Group":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Product_Product_Group_Relationship.Get_Info_By_Group(int_DVG_Row_id);
                    break;
            }

            for (int i = 0; i < Convert.ToInt32(checkedListBox1.Items.Count.ToString()); i++)
            {
                string item = checkedListBox1.Items[i].ToString();
                int int_User_Id = Convert.ToInt32(item.Substring(0, item.IndexOf(' ')));
                checkedListBox1.SetItemCheckState(i, CheckState.Unchecked);
                for (int j = 0; j < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); j++)
                {
                    dr_Get_Info = ds_Get_Info.Tables[0].Rows[j];

                    int int_clb_id = 0;
                    switch (str_table)
                    {
                        case "CM_Defect":
                            int_clb_id = Convert.ToInt32(dr_Get_Info["DefectClass_Id"].ToString());
                            break;
                        case "CM_Defect_Class":
                            int_clb_id = Convert.ToInt32(dr_Get_Info["Defect_Id"].ToString());
                            break;
                        case "CM_Product_Group":
                            int_clb_id = Convert.ToInt32(dr_Get_Info["Product_Id"].ToString());
                            break;
                    }


                    if (Convert.ToInt32(item.Substring(0, item.IndexOf(' '))) == int_clb_id)
                    {
                        checkedListBox1.SetItemCheckState(i, CheckState.Checked);
                    }
                }
            }
            ds_Get_Info.Dispose();

        }

        private void populate_datagridview()
        {
            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();

            switch (str_table)
            {
                case "CM_Brand":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Brand.Get_Info();
                    break;
                case "CM_Cities":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Cities.Get_Info();
                    break;
                case "CM_Count":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Count.Get_Info();
                    break;
                case "CM_Defect":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Defect.Get_Info();
                    break;
                case "CM_Defect_Class":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Defect_Class.Get_Info();
                    break;
                case "CM_ESP":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_ESP.Get_Info();
                    break;
                case "CM_Fruit_Type":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Fruit_Type.Get_Info();
                    break;
                case "CM_Grade":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Grade.Get_Info();
                    break;
                case "CM_Growing_Method":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Growing_Method.Get_Info();
                    break;
                case "CM_Location":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Location.Get_Info();
                    break;
                case "CM_Market_Attribute":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Market_Attribute.Get_Info();
                    break;
                case "CM_Pack_Type":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Pack_Type.Get_Info();
                    break;
                case "CM_Pallet_Type":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Pallet_Type.Get_Info();
                    break;
                case "CM_Product_Group":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Product_Group.Get_Info();
                    break;
                case "CM_Size":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Size.Get_Info();
                    break;
                case "CM_Storage":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Storage.Get_Info();
                    break;
                case "CM_Treatment":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Treatment.Get_Info();
                    break;
                case "CM_Truck":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Truck.Get_Info();
                    break;
                case "PF_Other_Work_Types":
                    ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Other_Work_Types.Get_Info();
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
                    case "CM_Brand":
                        DGVC_User_Id.Value = dr_Get_Info["Brand_Id"].ToString();
                        break;
                    case "CM_Cities":
                        DGVC_User_Id.Value = dr_Get_Info["City_Id"].ToString();
                        break;
                    case "CM_Count":
                        DGVC_User_Id.Value = dr_Get_Info["Count_Id"].ToString();
                        break;
                    case "CM_Defect":
                        DGVC_User_Id.Value = dr_Get_Info["Defect_Id"].ToString();
                        break;
                    case "CM_Defect_Class":
                        DGVC_User_Id.Value = dr_Get_Info["DefectClass_Id"].ToString();
                        break;
                    case "CM_ESP":
                        DGVC_User_Id.Value = dr_Get_Info["ESP_Id"].ToString();
                        break;
                    case "CM_Fruit_Type":
                        DGVC_User_Id.Value = dr_Get_Info["FruitType_Id"].ToString();
                        break;
                    case "CM_Grade":
                        DGVC_User_Id.Value = dr_Get_Info["Grade_Id"].ToString();
                        break;
                    case "CM_Growing_Method":
                        DGVC_User_Id.Value = dr_Get_Info["Growing_Method_Id"].ToString();
                        break;
                    case "CM_Location":
                        DGVC_User_Id.Value = dr_Get_Info["Location_Id"].ToString();
                        break;
                    case "CM_Market_Attribute":
                        DGVC_User_Id.Value = dr_Get_Info["Market_Attribute_Id"].ToString();
                        break;
                    case "CM_Pack_Type":
                        DGVC_User_Id.Value = dr_Get_Info["PackType_Id"].ToString();
                        break;
                    case "CM_Pallet_Type":
                        DGVC_User_Id.Value = dr_Get_Info["PalletType_id"].ToString();
                        break;
                    case "CM_Product_Group":
                        DGVC_User_Id.Value = dr_Get_Info["ProductGroup_Id"].ToString();
                        break;
                    case "CM_Size":
                        DGVC_User_Id.Value = dr_Get_Info["Size_Id"].ToString();
                        break;
                    case "CM_Storage":
                        DGVC_User_Id.Value = dr_Get_Info["Storage_Id"].ToString();
                        break;
                    case "CM_Treatment":
                        DGVC_User_Id.Value = dr_Get_Info["Treatment_Id"].ToString();
                        break;
                    case "CM_Truck":
                        DGVC_User_Id.Value = dr_Get_Info["Truck_Id"].ToString();
                        break;
                    case "PF_Other_Work_Types":
                        DGVC_User_Id.Value = dr_Get_Info["Other_Work_Types_Id"].ToString();
                        break;
                }

                dataGridView1.Rows[i].Cells[0] = DGVC_User_Id;

                DataGridViewCell DGVC_Name = new DataGridViewTextBoxCell();
                DGVC_Name.Value = dr_Get_Info["Code"].ToString();
                dataGridView1.Rows[i].Cells[1] = DGVC_Name;

                DataGridViewCell DGVC_Description = new DataGridViewTextBoxCell();
                DGVC_Description.Value = dr_Get_Info["Description"].ToString();
                dataGridView1.Rows[i].Cells[2] = DGVC_Description;

                try
                {
                    dr_Get_Info["PF_Active_Ind"].ToString();
                    DataGridViewCell DGVC_Active = new DataGridViewTextBoxCell();
                    DGVC_Active.Value = dr_Get_Info["PF_Active_Ind"].ToString();
                    dataGridView1.Rows[i].Cells[3] = DGVC_Active;
                }
                catch
                {
                    ckb_Active.Visible = false;
                    dataGridView1.Columns[3].Visible = false;
                }

                DataGridViewCell DGVC_Mod_date = new DataGridViewTextBoxCell();
                DGVC_Mod_date.Value = dr_Get_Info["Mod_date"].ToString();
                dataGridView1.Rows[i].Cells[4] = DGVC_Mod_date;

                DataGridViewCell DGVC_Mod_User_Id = new DataGridViewTextBoxCell();
                DGVC_Mod_User_Id.Value = dr_Get_Info["Mod_User_Id"].ToString();
                dataGridView1.Rows[i].Cells[5] = DGVC_Mod_User_Id;

                //dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dataGridView1.AutoResizeColumn(0, DataGridViewAutoSizeColumnMode.AllCells);
                dataGridView1.AutoResizeColumn(1, DataGridViewAutoSizeColumnMode.AllCells);
                dataGridView1.AutoResizeColumn(3, DataGridViewAutoSizeColumnMode.AllCells);
                dataGridView1.AutoResizeColumn(6, DataGridViewAutoSizeColumnMode.AllCells);
                dataGridView1.AutoResizeColumn(7, DataGridViewAutoSizeColumnMode.AllCells);

                ds_Get_Info.Dispose();
            }
        }

        private void populate_dataGridView2()
        {
            dataGridView2.Refresh();
            dataGridView2.Rows.Clear();

            DataSet ds = FruPak.PF.Data.AccessLayer.CM_Pack_Type_Weight.Get_Info(Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value.ToString()));
            DataRow dr;

            if (ds.Tables[0].Rows.Count == 0)
            {
                dataGridView2.Visible = false;
                lbl_tare_weight.Visible = true;
                nud_Tare_Weight.Visible = true;
                nud_Tare_Weight.Value = 0;
            }
            else
            {
                lbl_tare_weight.Visible = false;
                nud_Tare_Weight.Visible = false;
                dataGridView2.Visible = true;
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dr = ds.Tables[0].Rows[i];

                dataGridView2.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_Cell0 = new DataGridViewTextBoxCell();
                DGVC_Cell0.Value = dr["PackType_Weight_Id"].ToString();
                dataGridView2.Rows[i].Cells["PackType_Weight_Id"] = DGVC_Cell0;

                DataGridViewCell DGVC_Cell1 = new DataGridViewTextBoxCell();
                DGVC_Cell1.Value = dr["PackType_Id"].ToString();
                dataGridView2.Rows[i].Cells["PackType_Id"] = DGVC_Cell1;

                DataGridViewCell DGVC_Cell2 = new DataGridViewTextBoxCell();
                DGVC_Cell2.Value = dr["Weight_Tare"].ToString();
                dataGridView2.Rows[i].Cells["Weight_Tare"] = DGVC_Cell2;

                DataGridViewCell DGVC_Cell3 = new DataGridViewTextBoxCell();
                DGVC_Cell3.Value = dr["PF_Active_Ind"].ToString();
                dataGridView2.Rows[i].Cells["PF_Active_Ind"] = DGVC_Cell3;
            }

            ds.Dispose();
        }

        private void Reset()
        {
            txt_code.ResetText();
            txt_Description.ResetText();
            btn_Add.Text = "&Add";
            checkedListBox1.Visible = false;
            dataGridView2.Visible = false;
            btn_Update_Relationship.Enabled = bol_write_access;
            switch (str_table)
            {
                case "CM_Pack_Type":
                    lbl_tare_weight.Visible = true;
                    nud_Tare_Weight.Visible = true;
                    btn_Update_Relationship.Visible = true;
                    break;
                default:
                    lbl_tare_weight.Visible = false;
                    nud_Tare_Weight.Visible = false;
                    btn_Update_Relationship.Visible = false;
                    break;

            }
        }

        private void SizeAllColumns(Object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'moneyDataSet.List_TS_Work' table. You can move, or remove it, as needed.
            dataGridView1.AutoResizeColumn(0, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(1, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(3, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(6, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(7, DataGridViewAutoSizeColumnMode.AllCells);

        }

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
                    case "CM_Brand":
                        ds_validate = FruPak.PF.Data.AccessLayer.CM_Brand.Get_Info(txt_code.Text);
                        break;
                    case "CM_Cities":
                        ds_validate = FruPak.PF.Data.AccessLayer.CM_Cities.Get_Info(txt_code.Text);
                        break;
                    case "CM_Count":
                        ds_validate = FruPak.PF.Data.AccessLayer.CM_Count.Get_Info(txt_code.Text);
                        break;
                    case "CM_Defect":
                        ds_validate = FruPak.PF.Data.AccessLayer.CM_Defect.Get_Info(txt_code.Text);
                        break;
                    case "CM_Defect_Class":
                        ds_validate = FruPak.PF.Data.AccessLayer.CM_Defect_Class.Get_Info(txt_code.Text);
                        break;
                    case "CM_ESP":
                        ds_validate = FruPak.PF.Data.AccessLayer.CM_ESP.Get_Info(txt_code.Text);
                        break;
                    case "CM_Fruit_Type":
                        ds_validate = FruPak.PF.Data.AccessLayer.CM_Fruit_Type.Get_Info(txt_code.Text);
                        break;
                    case "CM_Grade":
                        ds_validate = FruPak.PF.Data.AccessLayer.CM_Grade.Get_Info(txt_code.Text);
                        break;
                    case "CM_Growing_Method":
                        ds_validate = FruPak.PF.Data.AccessLayer.CM_Growing_Method.Get_Info(txt_code.Text);
                        break;
                    case "CM_Location":
                        ds_validate = FruPak.PF.Data.AccessLayer.CM_Location.Get_Info(txt_code.Text);
                        break;
                    case "CM_Market_Attribute":
                        ds_validate = FruPak.PF.Data.AccessLayer.CM_Market_Attribute.Get_Info(txt_code.Text);
                        break;
                    case "CM_Pack_Type":
                        ds_validate = FruPak.PF.Data.AccessLayer.CM_Pack_Type.Get_Info(txt_code.Text);
                        break;
                    case "CM_Pallet_Type":
                        ds_validate = FruPak.PF.Data.AccessLayer.CM_Pallet_Type.Get_Info(txt_code.Text);
                        break;
                    case "CM_Product_Group":
                        ds_validate = FruPak.PF.Data.AccessLayer.CM_Product_Group.Get_Info(txt_code.Text);
                        break;
                    case "CM_Size":
                        ds_validate = FruPak.PF.Data.AccessLayer.CM_Size.Get_Info(txt_code.Text);
                        break;
                    case "CM_Storage":
                        ds_validate = FruPak.PF.Data.AccessLayer.CM_Storage.Get_Info(txt_code.Text);
                        break;
                    case "CM_Treatment":
                        ds_validate = FruPak.PF.Data.AccessLayer.CM_Treatment.Get_Info(txt_code.Text);
                        break;
                    case "CM_Truck":
                        ds_validate = FruPak.PF.Data.AccessLayer.CM_Truck.Get_Info(txt_code.Text);
                        break;
                    case "PF_Other_Work_Types":
                        ds_validate = FruPak.PF.Data.AccessLayer.PF_Other_Work_Types.Get_Info(txt_code.Text);
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

        #endregion Methods of Common_Maintenance (25)
    }
}
