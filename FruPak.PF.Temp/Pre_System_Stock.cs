using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;

namespace FruPak.PF.Temp
{
    public partial class Pre_System_Stock : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();     

        private static bool bol_write_access;
        private static int int_Current_User_Id = 0;
        public Pre_System_Stock(int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();

            int_Current_User_Id = int_C_User_id;

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

            materialNumber1.selectedMaterial = 0;
            materialNumber1.populate();
            AddColumnsProgrammatically();
            populate_DataGridView1();

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

            col0.HeaderText = "PrePalletStock_ID";
            col0.Name = "PrePalletStock_ID";
            col0.ReadOnly = true;
            col0.Visible = true;

            col1.HeaderText = "Material_Id";
            col1.Name = "Material_Id";
            col1.ReadOnly = true;
            col1.Visible = false;

            col2.HeaderText = "Material Num";
            col2.Name = "Material_Num";
            col2.ReadOnly = true;

            col3.HeaderText = "Material Description";
            col3.Name = "Material_desc";
            col3.ReadOnly = true;
            

            col4.HeaderText = "Quantity";
            col4.Name = "Quantity";
            col4.ReadOnly = true;


            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2, col3, col4 });

            DataGridViewImageColumn img_delete = new DataGridViewImageColumn();
            dataGridView1.Columns.Add(img_delete);
            img_delete.HeaderText = "Delete";
            img_delete.Name = "Delete";
            img_delete.Image = FruPak.PF.Global.Properties.Resources.delete;
            img_delete.ReadOnly = true;

            DataGridViewImageColumn img_edit = new DataGridViewImageColumn();
            dataGridView1.Columns.Add(img_edit);
            img_edit.HeaderText = "Edit";
            img_edit.Name = "Edit";
            img_edit.Image = FruPak.PF.Global.Properties.Resources.edit;
            img_edit.ReadOnly = true;

        }
        private void populate_DataGridView1()
        {
            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();

            DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_PreSystem_Pallet_Stock.Get_Info_Translated();
            DataRow dr_Get_Info;


            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                dataGridView1.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_Cell0 = new DataGridViewTextBoxCell();
                DGVC_Cell0.Value = dr_Get_Info["PrePalletStock_ID"].ToString();
                dataGridView1.Rows[i].Cells["PrePalletStock_ID"] = DGVC_Cell0;

                DataGridViewCell DGVC_Cell1 = new DataGridViewTextBoxCell();
                DGVC_Cell1.Value = dr_Get_Info["Material_Id"].ToString();
                dataGridView1.Rows[i].Cells["Material_Id"] = DGVC_Cell1;

                DataGridViewCell DGVC_Cell2 = new DataGridViewTextBoxCell();
                DGVC_Cell2.Value = dr_Get_Info["Material_Num"].ToString();
                dataGridView1.Rows[i].Cells["Material_Num"] = DGVC_Cell2;

                DataGridViewCell DGVC_Cell3 = new DataGridViewTextBoxCell();
                DGVC_Cell3.Value = dr_Get_Info["Description"].ToString();
                dataGridView1.Rows[i].Cells["Material_desc"] = DGVC_Cell3;

                DataGridViewCell DGVC_Cell4 = new DataGridViewTextBoxCell();
                DGVC_Cell4.Value = dr_Get_Info["Quantity"].ToString();
                dataGridView1.Rows[i].Cells["Quantity"] = DGVC_Cell4;
            }
            ColumnSize();
            ds_Get_Info.Dispose();
        }
        private void SizeAllColumns(Object sender, EventArgs e)
        {
            ColumnSize();
        }
        private void ColumnSize()
        {
            dataGridView1.AutoResizeColumn(0, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(1, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(2, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(3, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(4, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(5, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(6, DataGridViewAutoSizeColumnMode.AllCells);
        }
        private void btn_Add_Click(object sender, EventArgs e)
        {
            DialogResult DLR_message = new DialogResult();
            String str_msg = "";
            int int_result = 0;

            if (materialNumber1.Material_Number == 0)
            {
                str_msg = str_msg + "Invalid Material Number. Please Select a valid Material Number." + Environment.NewLine;
            }
            if (nud_quantity.Value == 0)
            {
                str_msg = str_msg + "Invalid Quantity. PLease enter a valid quantity." + Environment.NewLine;
            }

            if (str_msg.Length > 0)
            {
                DLR_message = MessageBox.Show(str_msg, "Process Factory - Temp (Presystem Stock Load)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (DLR_message != System.Windows.Forms.DialogResult.OK)
            {
                switch (btn_Add.Text)
                {
                    case "Add":
                        int_result = FruPak.PF.Data.AccessLayer.PF_PreSystem_Pallet_Stock.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_PreSystem_Pallet_Stock"), materialNumber1.Material_Number, nud_quantity.Value, int_Current_User_Id);
                        break;
                    case "Update":
                        int_result = FruPak.PF.Data.AccessLayer.PF_PreSystem_Pallet_Stock.Update(int_new_Id, materialNumber1.Material_Number, nud_quantity.Value, int_Current_User_Id);
                        break;
                }
                
            }
            if (int_result > 0)
            {
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                lbl_message.Text = "Pre stock has been saved";
                populate_DataGridView1();
                Reset();
            }
            else
            {
                lbl_message.ForeColor = System.Drawing.Color.Red;
                lbl_message.Text = "Pre stock has NOT been saved";
            }
        }

        private static int int_new_Id = 0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult DLR_Message = new System.Windows.Forms.DialogResult();
            string str_msg = "";
            int int_result = 0;
            //Delete
            #region ------------- Delete -------------
            if (e.ColumnIndex == 5)
            {
                str_msg = "You are about to delete Pre Stock Number for Material Number " + dataGridView1.Rows[e.RowIndex].Cells["Material_Num"].Value.ToString() + Environment.NewLine;
                str_msg = str_msg + "Do you want to continue?";
                DLR_Message = MessageBox.Show(str_msg, "Process Factory - Temp(Pre System Stock)", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (DLR_Message == DialogResult.Yes)
                {
                    int_result = FruPak.PF.Data.AccessLayer.PF_PreSystem_Pallet_Stock.Delete(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["PrePalletStock_ID"].Value.ToString()));
                }
                if (int_result > 0)
                {
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    lbl_message.Text = "Pre System Stock has been deleted";
                    populate_DataGridView1();
                }
                else
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = "Pre System Stock  failed to delete";
                }
            }
            #endregion
            //Edit
            #region ------------- Edit -------------
            else if (e.ColumnIndex == 6)
            {
                int_new_Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["PrePalletStock_ID"].Value.ToString());
                materialNumber1.setMaterial(Convert.ToInt32(dataGridView1.CurrentRow.Cells["Material_Id"].Value.ToString()));
                nud_quantity.Value = Convert.ToDecimal(dataGridView1.CurrentRow.Cells["Quantity"].Value.ToString());

                btn_Add.Text = "Update";
            }
            #endregion
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
            materialNumber1.Reset();
            nud_quantity.Value = 0;
            btn_Add.Text = "Add";
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
        private void Pre_System_Stock_KeyDown(object sender, KeyEventArgs e)
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
