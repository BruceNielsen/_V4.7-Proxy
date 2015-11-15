using NLog;
using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FruPak.PF.Temp
{
    public partial class Submission : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static bool bol_write_access;
        private static int int_Current_User_Id = 0;

        public Submission(int int_C_User_id, bool bol_w_a)
        {
            InitializeComponent();
            int_Current_User_Id = int_C_User_id;

            //restrict access
            bol_write_access = bol_w_a;
            btn_Add.Enabled = bol_w_a;
            //check if testing or not
            Console.WriteLine(FruPak.PF.Global.Global.bol_Testing);

            grower1.bol_test = FruPak.PF.Global.Global.bol_Testing;
            //if (FruPak.PF.Global.Global.bol_Testing == true)
            //{
            //    this.Text = "FruPak Process Factory - " + this.Text + " - Test Environment";
            //}
            //else
            //{
            //    this.Text = "FruPak Process Factory";
            //}

            populate_combobox();
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

            #endregion Log any interesting events from the UI to the CSV log file
        }

        private void populate_combobox()
        {
            DataSet ds_Get_info;
            ds_Get_info = FruPak.PF.Data.AccessLayer.CM_Trader.Get_Info();
            cmb_Trader.DataSource = ds_Get_info.Tables[0];
            cmb_Trader.DisplayMember = "Combined";
            cmb_Trader.ValueMember = "Trader_Id";
            cmb_Trader.Text = null;
            ds_Get_info.Dispose();
        }

        private void grower1_GrowerChanged(object sender, EventArgs e)
        {
            // when the grower changes we need to requery and reload the blocks available
            populate_block_combo(grower1.Grower_Id);
        }

        private void populate_block_combo(int grower_id)
        {
            if (grower_id == 0)
                cmb_Block.DataSource = null;
            else
            {
                DataSet ds_get_info;
                ds_get_info = FruPak.PF.Data.AccessLayer.CM_Block.Get_Info(grower_id);
                cmb_Block.DisplayMember = "Code";
                cmb_Block.ValueMember = "Block_Id";
                cmb_Block.DataSource = ds_get_info.Tables[0];
                if (ds_get_info.Tables[0].Rows.Count == 1)
                    cmb_Block.SelectedIndex = 0;
                ds_get_info.Dispose();
            }
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
            var col8 = new DataGridViewTextBoxColumn();
            var col9 = new DataGridViewTextBoxColumn();
            var col10 = new DataGridViewTextBoxColumn();
            var col11 = new DataGridViewTextBoxColumn();

            col0.HeaderText = "Sub ID";
            col0.Name = "Submission_Id";
            col0.ReadOnly = true;
            col0.Visible = true;

            col1.HeaderText = "Trader Id";
            col1.Name = "Trader_Id";
            col1.ReadOnly = true;
            col1.Visible = false;

            col2.HeaderText = "Trader";
            col2.Name = "Trader";
            col2.ReadOnly = true;

            col3.HeaderText = "Grower Id";
            col3.Name = "Grower_Id";
            col3.ReadOnly = true;
            col3.Visible = false;

            col4.HeaderText = "Grower";
            col4.Name = "Grower";
            col4.ReadOnly = true;

            col5.HeaderText = "Block_Id";
            col5.Name = "Block_Id";
            col5.ReadOnly = true;
            col5.Visible = true;

            col6.HeaderText = "Block";
            col6.Name = "Block";
            col6.ReadOnly = true;

            col7.HeaderText = "Sub Date";
            col7.Name = "Sub_Date";
            col7.ReadOnly = true;

            col8.HeaderText = "Harvest Date";
            col8.Name = "Harvest_Date";
            col8.ReadOnly = true;

            col9.HeaderText = "Pick Num";
            col9.Name = "Pick_Num";
            col9.ReadOnly = true;

            col10.HeaderText = "Comments";
            col10.Name = "Comments";
            col10.Width = 180;
            col10.ReadOnly = true;

            col11.HeaderText = "Grower Ref";
            col11.Name = "Grower_Reference";
            col11.ReadOnly = true;
            col11.Visible = false;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2, col3, col4, col5, col6, col7, col8, col9, col10, col11 });

            DataGridViewButtonColumn btn_Reprint = new DataGridViewButtonColumn();
            btn_Reprint.Text = "Print";
            btn_Reprint.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(btn_Reprint);

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

            DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.Temp_submission.Get_Info_Translated();
            DataRow dr_Get_Info;

            for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                dataGridView1.Rows.Add(new DataGridViewRow());

                DataGridViewCell DGVC_Cell0 = new DataGridViewTextBoxCell();
                DGVC_Cell0.Value = dr_Get_Info["Submission_Id"].ToString();
                dataGridView1.Rows[i].Cells["Submission_Id"] = DGVC_Cell0;

                DataGridViewCell DGVC_Cell1 = new DataGridViewTextBoxCell();
                DGVC_Cell1.Value = dr_Get_Info["Trader_Id"].ToString();
                dataGridView1.Rows[i].Cells[1] = DGVC_Cell1;

                DataGridViewCell DGVC_Cell2 = new DataGridViewTextBoxCell();
                DGVC_Cell2.Value = dr_Get_Info["T_Code"].ToString() + " - " + dr_Get_Info["T_Description"].ToString();
                dataGridView1.Rows[i].Cells["Trader"] = DGVC_Cell2;

                DataGridViewCell DGVC_Cell3 = new DataGridViewTextBoxCell();
                DGVC_Cell3.Value = dr_Get_Info["Grower_Id"].ToString();
                dataGridView1.Rows[i].Cells["Grower_Id"] = DGVC_Cell3;

                DataGridViewCell DGVC_Cell4 = new DataGridViewTextBoxCell();
                DGVC_Cell4.Value = dr_Get_Info["G_Rpin"].ToString();
                dataGridView1.Rows[i].Cells["Grower"] = DGVC_Cell4;

                DataGridViewCell DGVC_Cell5 = new DataGridViewTextBoxCell();
                DGVC_Cell5.Value = dr_Get_Info["Block_Id"].ToString();
                dataGridView1.Rows[i].Cells["Block_Id"] = DGVC_Cell5;

                DataGridViewCell DGVC_Cell6 = new DataGridViewTextBoxCell();
                DGVC_Cell6.Value = dr_Get_Info["B_Code"].ToString() + " - " + dr_Get_Info["B_Description"].ToString();
                dataGridView1.Rows[i].Cells["Block"] = DGVC_Cell6;

                DataGridViewCell DGVC_Cell7 = new DataGridViewTextBoxCell();
                DGVC_Cell7.Value = dr_Get_Info["Sub_Date"].ToString();
                dataGridView1.Rows[i].Cells["Sub_Date"] = DGVC_Cell7;

                DataGridViewCell DGVC_Cell8 = new DataGridViewTextBoxCell();
                DGVC_Cell8.Value = dr_Get_Info["Harvest_Date"].ToString();
                dataGridView1.Rows[i].Cells["Harvest_Date"] = DGVC_Cell8;

                DataGridViewCell DGVC_Cell9 = new DataGridViewTextBoxCell();
                DGVC_Cell9.Value = dr_Get_Info["Pick_Num"].ToString();
                dataGridView1.Rows[i].Cells["Pick_Num"] = DGVC_Cell9;

                DataGridViewCell DGVC_Cell10 = new DataGridViewTextBoxCell();
                DGVC_Cell10.Value = dr_Get_Info["Comments"].ToString();
                dataGridView1.Rows[i].Cells["Comments"] = DGVC_Cell10;

                DataGridViewCell DGVC_Cell11 = new DataGridViewTextBoxCell();
                DGVC_Cell11.Value = dr_Get_Info["Grower_Reference"].ToString();
                dataGridView1.Rows[i].Cells["Grower_Reference"] = DGVC_Cell11;
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
            dataGridView1.AutoResizeColumn(7, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(8, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(9, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(11, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(12, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(13, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(14, DataGridViewAutoSizeColumnMode.AllCells);
        }

        private int int_new_Sub_Id = 0;

        private void btn_Add_Click(object sender, EventArgs e)
        {
            DialogResult DLR_message = new DialogResult();
            string str_msg = "";
            int int_result = 0;

            if (cmb_Trader.SelectedIndex == -1)
            {
                str_msg = str_msg + "Invalid Trader. Please select a valid Trader from the drop down list." + Environment.NewLine;
            }
            if (grower1.Grower_Id == 0)
            {
                str_msg = str_msg + "Invalid Grower. PLease select a valid Grower from the drop down list." + Environment.NewLine;
            }
            if (cmb_Block.SelectedIndex == -1)
            {
                str_msg = str_msg + "Invalid Block. Please select a valid Block from the drop down list." + Environment.NewLine;
            }
            if (txt_Grower_Ref.TextLength == 0)
            {
                str_msg = str_msg + "Invalid grower Reference. Please enter a valid Grower Reference." + Environment.NewLine;
            }
            if (nud_Pick_Num.Value == 0)
            {
                str_msg = str_msg + "Invalid Pick Num. PLease enter a valid Pick Num." + Environment.NewLine;
            }
            if (str_msg.Length > 0)
            {
                DLR_message = MessageBox.Show(str_msg, "Process Factory - GateHouse(Submission)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //*******************************************************************************************
            switch (btn_Add.Text)
            {
                case "&Add":
                    int_new_Sub_Id = FruPak.PF.Common.Code.General.int_max_user_id("GH_Submission");
                    break;
            }

            bool bol_bins_Add = true;
            if (DLR_message != System.Windows.Forms.DialogResult.OK)
            {
                switch (btn_Add.Text)
                {
                    case "&Add":
                        int_result = FruPak.PF.Data.AccessLayer.GH_Submission.Insert(int_new_Sub_Id, Convert.ToInt32(cmb_Trader.SelectedValue.ToString()), grower1.Grower_Id,
                            Convert.ToInt32(cmb_Block.SelectedValue.ToString()), txt_Grower_Ref.Text, dtp_Sub_Date.Value.ToString("yyyy/MM/dd"), dtp_Harvest_Date.Value.ToString("yyyy/MM/dd"), Convert.ToInt32(nud_Pick_Num.Value.ToString()),
                            txt_Comments.Text, int_Current_User_Id);
                        bol_bins_Add = true;
                        break;

                    case "&Update":
                        int_result = FruPak.PF.Data.AccessLayer.GH_Submission.Update(int_new_Sub_Id, Convert.ToInt32(cmb_Trader.SelectedValue.ToString()), grower1.Grower_Id,
                            Convert.ToInt32(cmb_Block.SelectedValue.ToString()), txt_Grower_Ref.Text, dtp_Sub_Date.Value.ToString("yyyy/MM/dd"), dtp_Harvest_Date.Value.ToString("yyyy/MM/dd"), Convert.ToInt32(nud_Pick_Num.Value.ToString()),
                            txt_Comments.Text, int_Current_User_Id);
                        bol_bins_Add = false;
                        break;
                }
                if (int_result > 0)
                {
                    DialogResult dlr_frm = new System.Windows.Forms.DialogResult();

                    Form frm = new Submission_Bins(int_new_Sub_Id, Convert.ToInt32(cmb_Trader.SelectedValue.ToString()), int_Current_User_Id, bol_write_access, bol_bins_Add);
                    dlr_frm = frm.ShowDialog();

                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    if (Submission_Bins.Created_Bins > 0)
                    {
                        lbl_message.Text = "Submission has been Saved and " + Convert.ToString(Submission_Bins.Created_Bins) + " Bins have been added";
                    }
                    else
                    {
                        lbl_message.Text = "Submission has been saved and NO Bins have to created";
                    }
                    populate_DataGridView1();
                    Reset();
                }
                else
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = "Submission has NOT been saved";
                }
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            cmb_Trader.Text = null;
            cmb_Block.Text = null;
            grower1.Orchardist_Id = 0;
            dtp_Sub_Date.Value = DateTime.Now;
            dtp_Harvest_Date.Value = DateTime.Now;
            nud_Pick_Num.Value = 0;
            txt_Grower_Ref.ResetText();
            txt_Comments.ResetText();
            ckb_Print_all.Checked = false;
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            int int_cmb_Trader = 0;

            if (cmb_Trader.SelectedIndex != -1)
            {
                int_cmb_Trader = Convert.ToInt32(cmb_Trader.SelectedValue.ToString());
            }

            populate_combobox();
            cmb_Trader.SelectedValue = int_cmb_Trader;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult DLR_Message = new System.Windows.Forms.DialogResult();
            string str_msg = "";
            int int_result = 0;

            //Print

            #region ------------- Print -------------

            if (e.ColumnIndex == 12)
            {
                string Data = "";
                Data = "S:" + dataGridView1.CurrentRow.Cells["Submission_Id"].Value.ToString() + ":" + Convert.ToBoolean(ckb_Print_all.Checked);

                string curent_printer = System.Printing.LocalPrintServer.GetDefaultPrintQueue().FullName.ToString();
                string str_req_Printer = FruPak.PF.Common.Code.General.Get_Single_System_Code("PR-Bincrd");
                foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                {
                    try
                    {
                        if (printer.IndexOf(str_req_Printer) > 0)
                        {
                            FruPak.PF.PrintLayer.Word.Printer = printer;
                            myPrinters.SetDefaultPrinter(FruPak.PF.PrintLayer.Word.Printer);
                        }
                    }
                    catch
                    {
                        myPrinters.SetDefaultPrinter(curent_printer);
                    }
                }

                FruPak.PF.PrintLayer.Bin_Card.Print(Data, true);
                myPrinters.SetDefaultPrinter(curent_printer);
            }

            #endregion ------------- Print -------------

            //Delete

            #region ------------- Delete -------------

            else if (e.ColumnIndex == 13)
            {
                str_msg = "You are about to delete Submission Number " + dataGridView1.Rows[e.RowIndex].Cells["Submission_Id"].Value.ToString() + Environment.NewLine;
                str_msg = str_msg + "Do you want to continue?";
                DLR_Message = MessageBox.Show(str_msg, "Process Factory - GateHouse(Submission)", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (DLR_Message == DialogResult.Yes)
                {
                    int_result = FruPak.PF.Data.AccessLayer.CM_Bins.Delete_Submission(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Submission_Id"].Value.ToString()));
                    int_result = FruPak.PF.Data.AccessLayer.GH_Submission.Delete(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Submission_Id"].Value.ToString()));
                }
                if (int_result > 0)
                {
                    lbl_message.ForeColor = System.Drawing.Color.Blue;
                    lbl_message.Text = "Submission Number " + dataGridView1.Rows[e.RowIndex].Cells["Submission_Id"].Value.ToString() + " has been deleted";
                    populate_DataGridView1();
                }
                else
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = "Submission Number " + dataGridView1.Rows[e.RowIndex].Cells["Submission_Id"].Value.ToString() + " failed to delete";
                }
            }

            #endregion ------------- Delete -------------

            //Edit

            #region ------------- Edit -------------

            else if (e.ColumnIndex == 14)
            {
                int_new_Sub_Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Submission_Id"].Value.ToString());
                cmb_Trader.SelectedValue = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Trader_Id"].Value.ToString());

                //get orchardist Id
                DataSet ds = FruPak.PF.Data.AccessLayer.CM_Grower.Get_Orchardist(Convert.ToInt32(dataGridView1.CurrentRow.Cells["Grower_Id"].Value.ToString()));
                DataRow dr;

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dr = ds.Tables[0].Rows[i];
                    grower1.Orchardist_Id = Convert.ToInt32(dr["Orchardist_Id"].ToString());
                }

                ds.Dispose();

                grower1.Grower_Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Grower_Id"].Value.ToString());

                cmb_Block.SelectedValue = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Block_Id"].Value.ToString());

                dtp_Sub_Date.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["Sub_Date"].Value.ToString());
                dtp_Harvest_Date.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["Harvest_Date"].Value.ToString());
                nud_Pick_Num.Value = Convert.ToDecimal(dataGridView1.CurrentRow.Cells["Pick_Num"].Value.ToString());
                txt_Comments.Text = dataGridView1.CurrentRow.Cells["Comments"].Value.ToString();
                txt_Grower_Ref.Text = dataGridView1.CurrentRow.Cells["Grower_Reference"].Value.ToString();

                btn_Add.Text = "&Update";
            }

            #endregion ------------- Edit -------------
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
        private void Submission_KeyDown(object sender, KeyEventArgs e)
        {
            // Note that the KeyPreview property must be set to true for this to work
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        #endregion Methods to log UI events to the CSV file. BN 29/01/2015
    }

    public static class myPrinters
    {
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Name);
    }
}