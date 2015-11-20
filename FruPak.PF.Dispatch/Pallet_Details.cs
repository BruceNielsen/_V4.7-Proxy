using NLog;
using System;
using System.Data;
using System.Windows.Forms;

namespace PF.Dispatch
{
    public partial class Pallet_Details : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static bool bol_write_access;
        private static int int_Current_User_Id = 0;
        private int int_order_id;

        public Pallet_Details(int order_num, int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();
            int_Current_User_Id = int_C_User_id;
            int_order_id = order_num;

            //restrict access
            bol_write_access = bol_w_a;

            //check if testing or not

            //if (PF.Global.Global.bol_Testing == true)
            //{
            //    this.Text = "FP Process Factory - " + this.Text + " - Test Environment";
            //}
            //else
            //{
            //    this.Text = "FP Process Factory";
            //}

            txt_order_num.Text = Convert.ToString(int_order_id);
            AddColumnsProgrammatically();
            populate_DataGridView1();
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

            col0.HeaderText = "Pallet";
            col0.Name = "Pallet_Id";
            col0.ReadOnly = true;

            col1.HeaderText = "Barcode";
            col1.Name = "Barcode";
            col1.ReadOnly = true;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col0, col1 });

            DataGridViewImageColumn img_delete = new DataGridViewImageColumn();
            dataGridView1.Columns.Add(img_delete);
            img_delete.HeaderText = "Remove From Order";
            img_delete.Name = "Remove";
            img_delete.Image = PF.Global.Properties.Resources.delete;
            img_delete.ReadOnly = true;
        }

        private void AddColumnsProgrammatically2()
        {
            var col0 = new DataGridViewTextBoxColumn();
            var col1 = new DataGridViewTextBoxColumn();
            var col2 = new DataGridViewTextBoxColumn();
            var col3 = new DataGridViewTextBoxColumn();
            var col3a = new DataGridViewTextBoxColumn();
            var col4 = new DataGridViewTextBoxColumn();
            var col5 = new DataGridViewTextBoxColumn();

            col0.HeaderText = "Pallet";
            col0.Name = "Pallet_Id";
            col0.ReadOnly = true;
            col0.Visible = false;

            col1.HeaderText = "Product Id";
            col1.Name = "Product_Id";
            col1.ReadOnly = true;
            col1.Visible = false;

            col2.HeaderText = "Product";
            col2.Name = "Product";
            col2.ReadOnly = true;

            col3.HeaderText = "Fruit";
            col3.Name = "Fruit";
            col3.ReadOnly = true;

            col3a.HeaderText = "Variety";
            col3a.Name = "Variety";
            col3a.ReadOnly = true;

            col4.HeaderText = "Batch Num";
            col4.Name = "Batch_Num";
            col4.ReadOnly = true;

            col5.HeaderText = "Quantity";
            col5.Name = "Quantity";
            col5.ReadOnly = true;

            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2, col3, col3a, col4, col5 });
        }

        private void populate_DataGridView1()
        {
            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();

            DataSet ds_Get_Info = PF.Data.AccessLayer.PF_Pallet.Get_Info_distinct_order(int_order_id);
            DataRow dr_Get_Info;

            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                dataGridView1.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_Cell0 = new DataGridViewTextBoxCell();
                DGVC_Cell0.Value = dr_Get_Info["Pallet_Id"].ToString();
                dataGridView1.Rows[i].Cells["Pallet_Id"] = DGVC_Cell0;

                DataGridViewCell DGVC_Cell1 = new DataGridViewTextBoxCell();
                DGVC_Cell1.Value = dr_Get_Info["Barcode"].ToString();
                dataGridView1.Rows[i].Cells["Barcode"] = DGVC_Cell1;
            }
            Column_Size();
            ds_Get_Info.Dispose();
        }

        private void populate_DataGridView2(int int_Pallet_ID)
        {
            dataGridView2.Refresh();
            dataGridView2.Rows.Clear();

            DataSet ds_Get_Info = PF.Data.AccessLayer.PF_Pallet.Get_Info_for_Order_Pallet(int_order_id, int_Pallet_ID);
            DataRow dr_Get_Info;

            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                dataGridView2.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_Cell0 = new DataGridViewTextBoxCell();
                DGVC_Cell0.Value = dr_Get_Info["Pallet_Id"].ToString();
                dataGridView2.Rows[i].Cells["Pallet_Id"] = DGVC_Cell0;

                DataGridViewCell DGVC_Cell1 = new DataGridViewTextBoxCell();
                DGVC_Cell1.Value = dr_Get_Info["Product_Id"].ToString();
                dataGridView2.Rows[i].Cells["Product_Id"] = DGVC_Cell1;

                DataGridViewCell DGVC_Cell2 = new DataGridViewTextBoxCell();
                DGVC_Cell2.Value = dr_Get_Info["Product"].ToString();
                dataGridView2.Rows[i].Cells["Product"] = DGVC_Cell2;

                DataGridViewCell DGVC_Cell3 = new DataGridViewTextBoxCell();
                DGVC_Cell3.Value = dr_Get_Info["FT_Description"].ToString();
                dataGridView2.Rows[i].Cells["Fruit"] = DGVC_Cell3;

                DataGridViewCell DGVC_Cell3a = new DataGridViewTextBoxCell();
                DGVC_Cell3a.Value = dr_Get_Info["FV_Description"].ToString();
                dataGridView2.Rows[i].Cells["Variety"] = DGVC_Cell3a;

                DataGridViewCell DGVC_Cell4 = new DataGridViewTextBoxCell();
                DGVC_Cell4.Value = dr_Get_Info["Batch_Num"].ToString();
                dataGridView2.Rows[i].Cells["Batch_Num"] = DGVC_Cell4;

                DataGridViewCell DGVC_Cell5 = new DataGridViewTextBoxCell();
                DGVC_Cell5.Value = dr_Get_Info["Quantity"].ToString();
                dataGridView2.Rows[i].Cells["Quantity"] = DGVC_Cell5;
            }
            Column_Size();
            ds_Get_Info.Dispose();
        }

        private void SizeAllColumns(Object sender, EventArgs e)
        {
            Column_Size();
        }

        private void Column_Size()
        {
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataGridView2.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                populate_DataGridView2(Convert.ToInt32(dataGridView1.CurrentRow.Cells["Pallet_Id"].Value.ToString()));
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult DLR_Message = new System.Windows.Forms.DialogResult();
            string str_msg = "";
            int int_result = 0;

            //Remove
            if (e.ColumnIndex == 2)
            {
                if (e.RowIndex != -1)   // BN 21/01/2015
                {
                    str_msg = "You are about to Remove Pallet Number " + dataGridView1.Rows[e.RowIndex].Cells["Pallet_Id"].Value.ToString() + Environment.NewLine;
                    str_msg = str_msg + "Do you want to continue?";
                    DLR_Message = MessageBox.Show(str_msg, "Process Factory - Disptach(Orders)", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (DLR_Message == DialogResult.Yes)
                    {
                        int_result = PF.Data.AccessLayer.PF_Pallet.Update_remove_from_Order(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Pallet_Id"].Value.ToString()), int_Current_User_Id);
                    }

                    if (int_result > 0)
                    {
                        lbl_message.ForeColor = System.Drawing.Color.Blue;
                        lbl_message.Text = "Pallet has been removed from the Order";
                    }
                    else
                    {
                        lbl_message.ForeColor = System.Drawing.Color.Red;
                        lbl_message.Text = "Pallet failed to be removed from the Order";
                    }
                    populate_DataGridView1();
                    try
                    {
                        populate_DataGridView2(Convert.ToInt32(dataGridView1.Rows[0].Cells["Pallet_Id"].Value.ToString()));
                    }
                    catch (Exception ex)
                    {
                        logger.Log(LogLevel.Debug, ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Nothing to remove", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    logger.Log(LogLevel.Info, LogCode("dataGridView1_CellClick: Nothing to remove"));
                }
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Log Code

        /// <summary>
        /// Formats the output to the debug log so that the actual log contents stand out amongst the machine-generated stuff.
        /// Refers to machine-generated content, not calling specifics.
        /// </summary>
        /// <param name="input">The code function that we want to log.</param>
        /// <returns>A formatted (Bracketed) string</returns>
        public string LogCode(string input)
        {
            string Output = "___[ " + input + " ]___";
            return Output;
        }

        #endregion Log Code

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
        private void Pallet_Details_KeyDown(object sender, KeyEventArgs e)
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