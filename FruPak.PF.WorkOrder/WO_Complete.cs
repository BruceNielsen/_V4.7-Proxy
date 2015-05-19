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
    public partial class WO_Complete : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();     

        private static bool bol_write_access;
        private static int int_Current_User_Id = 0;
        public WO_Complete(int int_C_User_id, bool bol_w_a)
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
            AddColumnsProgrammatically();
            populate_dataGridView();

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
            //DataGridView 1
            #region ------------- DataGridView 1 -------------
            var col10 = new DataGridViewTextBoxColumn();
            var col11 = new DataGridViewTextBoxColumn();
            var col12 = new DataGridViewButtonColumn();

            col10.HeaderText = "Work_Order_Id";
            col10.Name = "Work_Order_Id";
            col10.ReadOnly = true;
            col10.Visible = true;

            col11.HeaderText = "Process_Date";
            col11.Name = "Process_Date";
            col11.ReadOnly = true;
            col11.Visible = true;

            col12.Name = "btn_Complete";            
            col12.Visible = true;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col10, col11, col12 });

            #endregion
        }
        private void populate_dataGridView()
        {
            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();

            DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Work_Order.Get_Info_Exclude_Current(FruPak.PF.Common.Code.General.Get_Season());
            DataRow dr_Get_Info;
            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                dataGridView1.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_Cell10 = new DataGridViewTextBoxCell();
                DGVC_Cell10.Value = dr_Get_Info["Work_Order_Id"].ToString();
                dataGridView1.Rows[i].Cells["Work_Order_Id"] = DGVC_Cell10;

                DataGridViewCell DGVC_Cell11 = new DataGridViewTextBoxCell();
                DGVC_Cell11.Value = dr_Get_Info["Process_Date"].ToString();
                dataGridView1.Rows[i].Cells["Process_Date"] = DGVC_Cell11;

                DataGridViewButtonCell btn_Reprint = new DataGridViewButtonCell();

                if (dr_Get_Info["Completed_Date"].ToString() != "")
                {                    
                    btn_Reprint.Value = "Un-Complete";
                }
                else
                {
                    btn_Reprint.Value = "Complete";
                }
                dataGridView1.Rows[i].Cells["btn_Complete"] = btn_Reprint;
            }
            Column_resize();
            ds_Get_Info.Dispose();
        }
        private void SizeAllColumns(Object sender, EventArgs e)
        {
            Column_resize();
        }
        private void Column_resize()
        {
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int int_result = -1;
            if (e.ColumnIndex == 2)
            {
                if (dataGridView1.CurrentRow.Cells[2].Value.ToString() == "Complete")
                {
                    int_result = FruPak.PF.Data.AccessLayer.PF_Work_Order.Update_Completed(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString()), int_Current_User_Id);
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    lbl_message.Text = "Work Order: " + Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value.ToString()) + " has been Completed";
                }
                else
                {
                    int_result = FruPak.PF.Data.AccessLayer.PF_Work_Order.Update_UnCompleted(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString()), int_Current_User_Id);
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    lbl_message.Text = "Work Order: " + Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value.ToString()) + " has been Un-Completed";
                }
            }
            populate_dataGridView();
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
        private void WO_Complete_KeyDown(object sender, KeyEventArgs e)
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
