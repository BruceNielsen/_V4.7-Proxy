using NLog;
using System;
using System.Data;
using System.Windows.Forms;

namespace PF.Utils.Scanning
{
    public partial class Pallet_Transfer : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static bool bol_write_access;
        private static int int_Current_User_Id = 0;

        public Pallet_Transfer(int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();
            int_Current_User_Id = int_C_User_id;

            //if (PF.Global.Global.bol_Testing == true)
            //{
            //    this.Text = "FP Process Factory - " + this.Text + " - Test Environment";
            //}
            //else
            //{
            //    this.Text = "FP Process Factory";
            //}

            //restrict access
            bol_write_access = bol_w_a;
            btn_Add.Enabled = bol_w_a;
            AddColumnsProgrammatically();
            //for testing
            barcode_frm.Set_Label = "From:";
            barcode_To.Set_Label = "To:";

            // BN 22/01/2015 Disabled this after discussion with Jim
            //barcode_frm.Get_Barcode = "00394210096400000011";
            barcode_frm.BarcodeValue = string.Empty;

            populate_dataGridView1();

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

            col0.HeaderText = "Pallet_Id";
            col0.Name = "Pallet_Id";
            col0.ReadOnly = false;
            col0.Visible = false;

            col1.HeaderText = "Date";
            col1.Name = "Date";
            col1.ReadOnly = false;
            col1.Visible = true;

            col2.HeaderText = "Batch";
            col2.Name = "Batch";
            col2.ReadOnly = true;

            col3.HeaderText = "Qty";
            col3.Name = "Qty";
            col3.ReadOnly = true;

            col4.HeaderText = "# Reqd";
            col4.Name = "Required";
            col4.ReadOnly = false;

            col5.HeaderText = "Batch_Num";
            col5.Name = "Batch_Num";
            col5.ReadOnly = true;
            col5.Visible = false;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2, col3, col4, col5 });
        }

        private void populate_dataGridView1()
        {
            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();

            DataSet ds_Get_Info;
            DataRow dr_Get_Info;
            if (barcode_frm.BarcodeValue.Length > 0)
            {
                ds_Get_Info = PF.Data.AccessLayer.PF_Pallet.Get_Info_for_Barcode(barcode_frm.BarcodeValue.ToString());
                for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
                {
                    dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                    dataGridView1.Rows.Add(new DataGridViewRow());

                    DataGridViewCell DGVC_Id = new DataGridViewTextBoxCell();
                    DGVC_Id.Value = dr_Get_Info["Pallet_Id"].ToString();
                    dataGridView1.Rows[i].Cells[0] = DGVC_Id;

                    DataGridViewCell DGVC_date = new DataGridViewTextBoxCell();
                    DGVC_date.Value = dr_Get_Info["Batch_Num"].ToString().Substring(6, 2) + "/" + dr_Get_Info["Batch_Num"].ToString().Substring(4, 2) + "/" + dr_Get_Info["Batch_Num"].ToString().Substring(0, 4);
                    dataGridView1.Rows[i].Cells[1] = DGVC_date;

                    DataGridViewCell DGVC_Batch = new DataGridViewTextBoxCell();
                    DGVC_Batch.Value = dr_Get_Info["Batch_Num"].ToString().Substring(8);
                    dataGridView1.Rows[i].Cells[2] = DGVC_Batch;

                    DataGridViewCell DGVC_Quantity = new DataGridViewTextBoxCell();
                    DGVC_Quantity.Value = dr_Get_Info["Quantity"].ToString();
                    dataGridView1.Rows[i].Cells[3] = DGVC_Quantity;

                    DataGridViewCell DGVC_col5 = new DataGridViewTextBoxCell();
                    DGVC_col5.Value = dr_Get_Info["Batch_Num"].ToString();
                    dataGridView1.Rows[i].Cells[5] = DGVC_col5;
                }
                Column_Size();
            }
        }

        private void SizeAllColumns(Object sender, EventArgs e)
        {
            Column_Size();
        }

        private void Column_Size()
        {
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = 40;
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int int_new_value = 0;
            if (e.ColumnIndex == 4)
            {
                try
                {
                    int_new_value = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue.ToString());
                }
                catch (Exception ex)
                {
                    logger.Log(LogLevel.Debug, ex.Message);
                }

                if (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString()) < int_new_value)
                {
                    lbl_message.Text = "Not Enough left in this Batch on this Pallet";
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lbl_message.Text = "";
                }
            }
            int_new_value = 0;
            for (int i = 0; i < Convert.ToInt32(dataGridView1.Rows.Count.ToString()); i++)
            {
                try
                {
                    int_new_value = int_new_value + Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value.ToString());
                }
                catch
                {
                    int_new_value = int_new_value + 0;
                }
            }
            txt_total.Text = Convert.ToString(int_new_value);
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            string str_msg = "";
            //  int int_req_Amount = 0;
            int int_result = -1;

            if (barcode_frm.BarcodeValue.Length == 0)
            {
                str_msg = str_msg + "Invalid From Pallet. Please scan/enter a valid barcode.";
                lbl_message.Text = str_msg;
                lbl_message.ForeColor = System.Drawing.Color.Red;
            }
            if (barcode_To.BarcodeValue.Length == 0)
            {
                str_msg = str_msg + "Invalid To Pallet. Please scan/enter a valid barcode.";
                lbl_message.Text = str_msg;
                lbl_message.ForeColor = System.Drawing.Color.Red;
            }
            if (barcode_frm.BarcodeValue.ToString() == barcode_To.BarcodeValue.ToString())
            {
                str_msg = str_msg + "Invalid Barcodes. You can NOT Transfer to the same Pallet .";
                lbl_message.Text = str_msg;
                lbl_message.ForeColor = System.Drawing.Color.Red;
            }

            if (str_msg.Length == 0)
            {
                int int_reject = 0;
                for (int i = 0; i < Convert.ToInt32(dataGridView1.Rows.Count.ToString()); i++)
                {
                    try
                    {
                        Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value.ToString());
                        DataSet ds_Get_Info = PF.Data.AccessLayer.PF_Pallet.Get_Details_For_Barcode_BatchNum(barcode_frm.BarcodeValue.ToString(), Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value.ToString()), Convert.ToInt32(dataGridView1.Rows[i].Cells["Qty"].Value.ToString()));
                        DataRow dr_Get_Info;

                        //use the pallet linked to the second barcode that has been scanned
                        DataSet ds_Get_Info2 = PF.Data.AccessLayer.PF_Pallet.Get_Info_for_Barcode(barcode_To.BarcodeValue.ToString());
                        DataRow dr_Get_Info2 = null;    // Added null to get rid of 'Unassigned variable' warning - BN 9/9/2015

                        int int_Pallet_Id = 0;
                        for (int i2 = 0; i2 < ds_Get_Info2.Tables[0].Rows.Count; i2++)
                        {
                            dr_Get_Info2 = ds_Get_Info2.Tables[0].Rows[i2];
                            int_Pallet_Id = Convert.ToInt32(dr_Get_Info2["Pallet_Id"].ToString());
                        }
                        //ds_Get_Info2.Dispose(); // Don't dispose just yet; needed below - BN

                        // ---------------------------------------------------------------------------------
                        // As per Sel's request, this is a check to disllow transfers between pallets
                        // where the Material_Id's don't match.
                        // BN - 09/09/2015
                        try
                        {
                            dr_Get_Info = ds_Get_Info.Tables[0].Rows[0];    // Set to first row
                            var fromPallet_Material_Id = dr_Get_Info["Material_Id"].ToString();
                            var toPallet_Material_Id = dr_Get_Info2["Material_Id"].ToString();
                            if (fromPallet_Material_Id != toPallet_Material_Id)
                            {
                                int_result = -2;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            logger.Log(LogLevel.Debug, "Error - Material_Id: " + ex.Message);
                        }

                        ds_Get_Info2.Dispose(); // Now dispose.
                        // ---------------------------------------------------------------------------------
                        if (int_result != -2)
                        {
                            for (int j = 0; j < ds_Get_Info.Tables[0].Rows.Count; j++)
                            {
                                dr_Get_Info = ds_Get_Info.Tables[0].Rows[j];
                                int_result = PF.Data.AccessLayer.PF_Pallet_Details.Insert(PF.Common.Code.General.int_max_user_id("PF_Pallet_Details"), int_Pallet_Id, Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value.ToString()),
                                                                                                Convert.ToInt32(dr_Get_Info["Material_Id"].ToString()), Convert.ToInt32(dr_Get_Info["Work_Order_Id"].ToString()), Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value.ToString()), int_Current_User_Id);
                                if (int_result >= 0)
                                {
                                    int_result = PF.Data.AccessLayer.PF_Pallet_Details.Update_Quantity(Convert.ToInt32(dr_Get_Info["PalletDetails_Id"].ToString()), Convert.ToDecimal(dr_Get_Info["Quantity"].ToString()) - Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value.ToString()), int_Current_User_Id);
                                    DataSet ds = PF.Data.AccessLayer.PF_Pallet_Details.Get_Pallet_Quantity(barcode_frm.BarcodeValue);
                                    DataRow dr;
                                    int int_total_quantity = 99;    // BN 21/01/2015 - What was he trying to achieve here?
                                    for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                                    {
                                        dr = ds.Tables[0].Rows[k];
                                        int_total_quantity = Convert.ToInt32(dr["Total_Quantity"].ToString());
                                    }
                                    ds.Dispose();
                                    if (int_total_quantity == 0)
                                    {
                                        int_result = int_result + PF.Data.AccessLayer.PF_Pallet.Update_Active_status(int_Pallet_Id, false, int_Current_User_Id);
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                        int_reject++;
                        logger.Log(LogLevel.Warn, "int_reject: " + int_reject.ToString());
                    }
                }
                if (int_result >= 0)
                {
                    lbl_message.Text = "Pallet has been Transferred";
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                }
                else if (int_reject == dataGridView1.Rows.Count)
                {
                    lbl_message.Text = "Number required has not been completed";
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                }
                else if (int_result == -1)
                {
                    lbl_message.Text = "Pallet has NOT been Transferred";
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                }
                else // int_result == -2 - - BN 9/9/2015
                {
                    lbl_message.Text = "Failed. Material_Id's do not match.";
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                }
            }
            Reset();
        }

        private void Reset()
        {
            barcode_frm.BarcodeValue = "";
            barcode_To.BarcodeValue = "";
            txt_total.ResetText();
        }

        private void txt_Pallet_From_TextChanged(object sender, EventArgs e)
        {
            if (barcode_frm.BarcodeValue != null || barcode_frm.BarcodeValue != "")
            {
                populate_dataGridView1();
            }
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
        private void Pallet_Transfer_KeyDown(object sender, KeyEventArgs e)
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