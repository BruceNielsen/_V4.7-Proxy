using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using NLog;

namespace FruPak.PF.Dispatch
{
    public partial class Shipping_Order : Form
    {
        // Phantom 11/12/2014
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static bool bol_write_access;        
        private static int int_Current_User_Id = 0;
        private int int_order_id;
        private static DialogResult DLR_MessageBox = new DialogResult();

        public Shipping_Order(int int_C_User_id, bool bol_w_a)
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
            AddColumnsProgrammatically();            

            //set up outlook locations
            FruPak.PF.Data.Outlook.Outlook.Folder_Name = FruPak.PF.Common.Code.Outlook.SetUp_Location("PF-Contact");
            FruPak.PF.Data.Outlook.Outlook.Folder_Name_Location = FruPak.PF.Data.Outlook.Outlook.Folder_Name_Location;

            populate_DataGridView1();
            populate_combobox();
            AddColumnsProgrammatically2();
            populate_DataGridView2();

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

        private void populate_combobox()
        {
            DataSet ds_Get_Info;

            //Destination
            ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Cities.Get_Info();
            cmb_Destination.DataSource = ds_Get_Info.Tables[0];
            cmb_Destination.DisplayMember = "Description";
            cmb_Destination.ValueMember = "City_Id";
            cmb_Destination.Text = null;
            ds_Get_Info.Dispose();

            //Truck
            ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Truck.Get_Info();            
            cmb_Truck.DataSource = ds_Get_Info.Tables[0];
            cmb_Truck.DisplayMember = "Description";
            cmb_Truck.ValueMember = "Truck_Id";
            cmb_Truck.Text = null;
            ds_Get_Info.Dispose();

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

            col0.HeaderText = "Order";
            col0.Name = "Id";
            col0.ReadOnly = true;
            col0.Visible = true;

            col1.HeaderText = "Load Date";
            col1.Name = "LoadDate";
            col1.ReadOnly = true;

            col2.HeaderText = "Customer_Id";
            col2.Name = "Customer_Id";
            col2.ReadOnly = true;
            col2.Visible = false;

            col3.HeaderText = "Customer";
            col3.Name = "Customer";
            col3.ReadOnly = true;
            col3.Visible = true;

            col4.HeaderText = "Customer Order";
            col4.Name = "Customer_Order";
            col4.ReadOnly = true;
            col4.Visible = true;

            col5.HeaderText = "Truck_Id";
            col5.Name = "Truck_Id";
            col5.ReadOnly = true;
            col5.Visible = false;

            col6.HeaderText = "Truck";
            col6.Name = "Truck";
            col6.ReadOnly = true;
            col6.Visible = true;

            col7.HeaderText = "Destination_Id";
            col7.Name = "Destination_Id";
            col7.ReadOnly = true;
            col7.Visible = false;

            col8.HeaderText = "Destination";
            col8.Name = "Destination";
            col8.ReadOnly = true;
            col8.Visible = true;

            col9.HeaderText = "Freight Docket";
            col9.Name = "Freight_Docket";
            col9.ReadOnly = true;
            col9.Visible = true;

            col10.HeaderText = "Comments";
            col10.Name = "Comments";
            col10.Width = 200;
            col10.ReadOnly = true;
            col10.Visible = true;
            

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2, col3, col4, col5, col6, col7, col8, col9, col10 });

            DataGridViewButtonColumn btn_Reprint = new DataGridViewButtonColumn();
            btn_Reprint.HeaderText = "Print";
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
        private void AddColumnsProgrammatically2()
        {
            var col0 = new DataGridViewTextBoxColumn();
            var col1 = new DataGridViewTextBoxColumn();
            var col2 = new DataGridViewTextBoxColumn();

            col0.HeaderText = "Id";
            col0.Name = "Id";
            col0.ReadOnly = true;
            col0.Visible = false;

            col1.HeaderText = "Description";
            col1.Name = "Description";
            col1.ReadOnly = true;

            col2.HeaderText = "Number Required";
            col2.Name = "Number";
            col2.ReadOnly = false;
            col2.Visible = true;

            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2 });
        }
        private void SizeAllColumns(Object sender, EventArgs e)
        {
            SizeColumns();
        }
        private void SizeColumns()
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
            dataGridView2.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }
        private void populate_DataGridView2()
        {
            dataGridView2.Refresh();
            dataGridView2.Rows.Clear();

            DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Pallet_Type.Get_Info();
            DataRow dr_Get_Info;

            if (ds_Get_Info != null)    // BN 29/01/2015

            {
                for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
                {
                    dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                    dataGridView2.Rows.Add(new DataGridViewRow());

                    DataGridViewCell DGVC_Order_ID = new DataGridViewTextBoxCell();
                    DGVC_Order_ID.Value = dr_Get_Info["PalletType_Id"].ToString();
                    dataGridView2.Rows[i].Cells[0] = DGVC_Order_ID;

                    DataGridViewCell DGVC_Description = new DataGridViewTextBoxCell();
                    DGVC_Description.Value = dr_Get_Info["Combined"].ToString();
                    dataGridView2.Rows[i].Cells[1] = DGVC_Description;
                }
                dataGridView2.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                ds_Get_Info.Dispose();
                SizeColumns();
            }
            else
            {
                // BN 29/01/2015
                logger.Log(LogLevel.Info, "populate_DataGridView2: Crash avoided: Dataset was null.");
            }
        }
        private void populate_DataGridView1()
        {
            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();

            DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Orders.Get_Order_Info(true);
            DataRow dr_Get_Info;

            if (ds_Get_Info != null)    // BN 29/01/2015
            {

                for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
                {
                    dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                    dataGridView1.Rows.Add(new DataGridViewRow());

                    DataGridViewCell DGVC_Order_ID = new DataGridViewTextBoxCell();
                    DGVC_Order_ID.Value = dr_Get_Info["Order_Id"].ToString();
                    dataGridView1.Rows[i].Cells[0] = DGVC_Order_ID;

                    DataGridViewCell DGVC_Load_Date = new DataGridViewTextBoxCell();
                    DGVC_Load_Date.Value = dr_Get_Info["Load_Date"].ToString();
                    dataGridView1.Rows[i].Cells[1] = DGVC_Load_Date;

                    DataGridViewCell DGVC_Customer_Id = new DataGridViewTextBoxCell();
                    DGVC_Customer_Id.Value = dr_Get_Info["Customer_Id"].ToString();
                    dataGridView1.Rows[i].Cells[2] = DGVC_Customer_Id;

                    DataGridViewCell DGVC_Customer = new DataGridViewTextBoxCell();
                    DGVC_Customer.Value = dr_Get_Info["Customer"].ToString();
                    dataGridView1.Rows[i].Cells[3] = DGVC_Customer;

                    DataGridViewCell DGVC_Customer_Order = new DataGridViewTextBoxCell();
                    DGVC_Customer_Order.Value = dr_Get_Info["Customer_Order"].ToString();
                    dataGridView1.Rows[i].Cells[4] = DGVC_Customer_Order;

                    DataGridViewCell DGVC_Truck_Id = new DataGridViewTextBoxCell();
                    DGVC_Truck_Id.Value = dr_Get_Info["Truck_Id"].ToString();
                    dataGridView1.Rows[i].Cells[5] = DGVC_Truck_Id;

                    DataGridViewCell DGVC_Truck = new DataGridViewTextBoxCell();
                    DGVC_Truck.Value = dr_Get_Info["Truck"].ToString();
                    dataGridView1.Rows[i].Cells[6] = DGVC_Truck;

                    DataGridViewCell DGVC_Destination_Id = new DataGridViewTextBoxCell();
                    DGVC_Destination_Id.Value = dr_Get_Info["City_Id"].ToString();
                    dataGridView1.Rows[i].Cells[7] = DGVC_Destination_Id;

                    DataGridViewCell DGVC_Destination = new DataGridViewTextBoxCell();
                    DGVC_Destination.Value = dr_Get_Info["City"].ToString();
                    dataGridView1.Rows[i].Cells[8] = DGVC_Destination;

                    DataGridViewCell DGVC_Freight_Docket = new DataGridViewTextBoxCell();
                    DGVC_Freight_Docket.Value = dr_Get_Info["Freight_Docket"].ToString();
                    dataGridView1.Rows[i].Cells[9] = DGVC_Freight_Docket;

                    DataGridViewCell DGVC_Comments = new DataGridViewTextBoxCell();
                    DGVC_Comments.Value = dr_Get_Info["Comments"].ToString();
                    dataGridView1.Rows[i].Cells[10] = DGVC_Comments;
                }
                // Disabled BN 21/01/2015
                //dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                ds_Get_Info.Dispose();
                Overdue_Orders();

                // Disabled BN 21/01/2015
                //SizeColumns();
            }
            else
            {
                // BN 29/01/2015
                logger.Log(LogLevel.Info, "populate_DataGridView2: Crash avoided: Dataset was null.");
            }

        }
        private void Overdue_Orders()
        {
            System.Windows.Forms.DataGridViewCellStyle boldStyle = new System.Windows.Forms.DataGridViewCellStyle();

            boldStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);

            DateTime dt_current = new DateTime();
            dt_current = DateTime.Now;
            int int_dt_current = Convert.ToInt32(dt_current.ToString("yyyyMMdd"));


            DataSet ds = FruPak.PF.Data.AccessLayer.PF_Orders.Get_Overdue_Orders();
            DataRow dr;

            if (ds != null)    // BN 29/01/2015
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dr = ds.Tables[0].Rows[i];

                    DateTime dt_order = new DateTime();
                    dt_order = Convert.ToDateTime(dr["Load_Date"].ToString());

                    int int_dt_order = Convert.ToInt32(dt_order.ToString("yyyyMMdd"));



                    //do for each row
                    DataGridViewRow dr2;
                    for (int j = 0; j < dataGridView1.Rows.Count; j++)
                    {
                        dr2 = dataGridView1.Rows[j];

                        if (int_dt_order < int_dt_current)
                        {
                            if (dr2.Cells[0].Value.ToString() == dr["Order_Id"].ToString())
                            {
                                dr2.DefaultCellStyle = boldStyle;
                                //count the number of cells
                                for (int k = 0; k < dr2.Cells.Count; k++)
                                {
                                    dataGridView1[k, j].Style.BackColor = Color.LightCoral;
                                }
                            }
                        }
                        else
                        {
                            if (dr2.Cells[0].Value.ToString() == dr["Order_Id"].ToString())
                            {
                                dr2.DefaultCellStyle = boldStyle;
                                //count the number of cells
                                for (int k = 0; k < dr2.Cells.Count; k++)
                                {
                                    dataGridView1[k, j].Style.BackColor = Color.LightGreen;
                                }
                            }
                        }
                    }
                }
                ds.Dispose();
            }
            else
            {
                // BN 29/01/2015
                logger.Log(LogLevel.Info, "Overdue_Orders: Crash avoided: Dataset was null.");
            }
        }
        private string Validate_Input()
        {
            string str_msg = "";
            if (customer1.Customer_Id <= 0)
            {
                str_msg = str_msg + "Invalid Customer.\r\nPlease Select a valid Customer from the dropdown List.\r\n" + Environment.NewLine;
            }
            if (txt_Customer_Ref.TextLength == 0)
            {
                str_msg = str_msg + "Invalid Customer Reference.\r\nPlease Enter a valid Customer Order Reference.\r\n" + Environment.NewLine;
            }
            if (cmb_Truck.SelectedValue == null)
            {
                str_msg = str_msg + "Invalid Truck.\r\nPlease Select a valid Truck from the dropdown List.\r\n" + Environment.NewLine;
            }
            if (cmb_Destination.SelectedValue == null)
            {
                str_msg = str_msg + "Invalid Destination.\r\nPlease Select a valid Destination from the dropdown List.\r\n" + Environment.NewLine;
            }
            //if (txt_Freight_Docket.TextLength == 0)
            //{
            //    str_msg = str_msg + "Invalid Freight Docket. Please Enter a valid Freight Docket." + Environment.NewLine;
            //}
            return str_msg;
        }       
        private void btn_Add_Click(object sender, EventArgs e)
        {
            DLR_MessageBox = System.Windows.Forms.DialogResult.None;

            string str_msg = "";
            int int_result = 0;

            str_msg = str_msg + Validate_Input();

            if (str_msg.Length > 0)
            {
                DLR_MessageBox = MessageBox.Show(str_msg, "Process Factory - Dispatch(Order)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (DLR_MessageBox != DialogResult.OK)
            {
                switch (btn_Add.Text)
                {
                    case "&Add":
                        
                        int_order_id = FruPak.PF.Common.Code.General.int_max_user_id("PF_Orders");
                        int_result = FruPak.PF.Data.AccessLayer.PF_Orders.Insert(int_order_id, Convert.ToInt32(cmb_Truck.SelectedValue.ToString()), Convert.ToInt32(cmb_Destination.SelectedValue.ToString()), dtp_Load_Date.Value.ToString("yyyy/MM/dd"), txt_Customer_Ref.Text, txt_Freight_Docket.Text, customer1.Customer_Id, txt_Comments.Text, true, ckb_Hold_For_Payment.Checked, int_Current_User_Id);

                        for (int i = 0; i < Convert.ToInt32(dataGridView2.Rows.Count.ToString()); i++)
                        {
                            if (dataGridView2.Rows[i].Cells[2].EditedFormattedValue.ToString() != "")
                            {
                                FruPak.PF.Data.AccessLayer.PF_Orders_Pallets.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Orders_Pallets"), int_order_id, Convert.ToInt32(dataGridView2.Rows[i].Cells[0].Value.ToString()), Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].EditedFormattedValue.ToString()),int_Current_User_Id);
                            }
                        }
                        break;
                    case "&Update":
                        int_result = FruPak.PF.Data.AccessLayer.PF_Orders.Update(int_order_id, Convert.ToInt32(cmb_Truck.SelectedValue.ToString()), Convert.ToInt32(cmb_Destination.SelectedValue.ToString()), dtp_Load_Date.Value.ToString("yyyy/MM/dd"), txt_Customer_Ref.Text, txt_Freight_Docket.Text, customer1.Customer_Id, txt_Comments.Text, true, ckb_Hold_For_Payment.Checked, int_Current_User_Id);
                        FruPak.PF.Data.AccessLayer.PF_Orders_Pallets.Delete(int_order_id);
                        for (int i = 0; i < Convert.ToInt32(dataGridView2.Rows.Count.ToString()); i++)
                        {
                            if (dataGridView2.Rows[i].Cells[2].EditedFormattedValue.ToString() != "")
                            {
                                FruPak.PF.Data.AccessLayer.PF_Orders_Pallets.Insert(FruPak.PF.Common.Code.General.int_max_user_id("PF_Orders_Pallets"), int_order_id, Convert.ToInt32(dataGridView2.Rows[i].Cells[0].Value.ToString()), Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].EditedFormattedValue.ToString()), int_Current_User_Id);
                            }
                        }
                        break;
                }
                Form frm_staff = new Shipping_Staff(int_order_id, int_Current_User_Id, bol_write_access);
                frm_staff.ShowDialog();                
            }

            if (int_result > 0)
            {
                lbl_message.ForeColor = System.Drawing.Color.Blue;
                lbl_message.Text = "Order has been saved";
                populate_DataGridView1();
                Reset();
            }
            else
            {
                lbl_message.ForeColor = System.Drawing.Color.Red;
                lbl_message.Text = "Order has NOT been saved";
            }
        }
        private List<string> lst_filenames = new List<string>();
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult DLR_Message = new DialogResult();
           
            string str_msg ="";

            //print
            #region ------------- Print -------------
            if (e.ColumnIndex == 11)
            {
                DataSet ds_Get_Info;
                DataRow dr_Get_Info;

                //variables
                DateTime Load_Date = new DateTime();
                string str_Order_Num = "";
                string str_Freight_Docket = "";
                string str_delivery_name = "";
                int int_Loop_count = 0;

                #region ------------- defaults -------------
                if (e.RowIndex != -1)    // Stop a crash when clicking on the Print header - BN 20/01/2015
                {

                    ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Orders.Get_Info_incl_Outlook(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));

                    for (int i = 0; i <= Convert.ToInt32(Math.Floor(Convert.ToDecimal(ds_Get_Info.Tables[0].Rows.Count.ToString()) / 10)); i++)
                    {
                        int_Loop_count = Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString());

                        dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];

                        Load_Date = Convert.ToDateTime(dr_Get_Info["Load_Date"].ToString());
                        str_Order_Num = dr_Get_Info["Customer_Order"].ToString();
                        str_Freight_Docket = dr_Get_Info["Freight_Docket"].ToString();

                        FruPak.PF.Data.Outlook.Contacts.Contact_Details(dr_Get_Info["Outlook_Key"].ToString());

                        if (FruPak.PF.Data.Outlook.Contacts.Contact_Business_Address.ToString() == "")
                        {
                            str_msg = str_msg + "No Address loaded in OutLook for this Customer. Please load an Address and try again." + Environment.NewLine;
                        }
                        if (FruPak.PF.Data.Outlook.Contacts.Contact_Company_Name == "")
                        {
                            if (FruPak.PF.Data.Outlook.Contacts.Contact_First_Name == "" && FruPak.PF.Data.Outlook.Contacts.Contact_Last_Name == "")
                            {
                                str_msg = str_msg + "No Names (Company, First, Last) are loaded in Outlook for this Customer. Please load a Name and try again." + Environment.NewLine;
                            }
                            else
                            {
                                str_delivery_name = FruPak.PF.Data.Outlook.Contacts.Contact_First_Name + " " + FruPak.PF.Data.Outlook.Contacts.Contact_Last_Name;
                            }
                        }
                        else
                        {
                            str_delivery_name = FruPak.PF.Data.Outlook.Contacts.Contact_Company_Name;
                        }

                        if (str_msg.Length > 0)
                        {
                            DLR_Message = MessageBox.Show(str_msg, "Process Factory - Dispatch(Order - Print)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            logger.Log(LogLevel.Info, DecorateString("dataGridView1_CellClick", str_msg, "(Defaults)"));

                        }

                    }
                    ds_Get_Info.Dispose();
                }
                #endregion
                #region ------------- Pallet Card -------------
                if (DLR_Message != DialogResult.OK)
                {
                    if (e.RowIndex != -1)    // Stop a crash when clicking on the Print header - BN 20/01/2015
                    {

                        string str_barcode = "";
                        bool bol_print = false;
                        //Gets the pallet details for the order
                        ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Pallet.Get_Info_for_Order(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString()));
                        for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
                        {
                            dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                            str_barcode = dr_Get_Info["Barcode"].ToString();
                            bol_print = Convert.ToBoolean(dr_Get_Info["PCardPrinted_Ind"].ToString());

                            if (bol_print == false)
                            {
                                Cursor = Cursors.WaitCursor;
                                //set printer
                                FruPak.PF.PrintLayer.Word.Printer = FruPak.PF.Common.Code.General.Get_Printer("A4");
                                FruPak.PF.PrintLayer.Word.FileName = "PC" + str_barcode;
                                FruPak.PF.PrintLayer.Pallet_Card.Print(str_barcode, true, int_Current_User_Id);
                                FruPak.PF.Data.AccessLayer.PF_Pallet.Update_For_PCard(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString()), true, int_Current_User_Id);
                                Cursor = Cursors.Default;
                            }
                        }
                        ds_Get_Info.Dispose();
                    }
                }
                #endregion
                #region ------------- Certificate of Analysis -------------
                if (DLR_Message != DialogResult.OK)
                {
                    if (e.RowIndex != -1)    // Stop a crash when clicking on the Print header - BN 20/01/2015
                    {

                        string Data = "";

                        Data = Data + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                        Data = Data + ":" + str_delivery_name;
                        Data = Data + ":" + FruPak.PF.Data.Outlook.Contacts.Contact_Business_Address.ToString();
                        Data = Data + ":Print";

                        Cursor = Cursors.WaitCursor;
                        //set printer
                        FruPak.PF.PrintLayer.Word.Printer = FruPak.PF.Common.Code.General.Get_Printer("A4");
                        FruPak.PF.PrintLayer.Word.FileName = "COA" + dataGridView1.CurrentRow.Cells[0].Value.ToString();
                        FruPak.PF.PrintLayer.Certificate_Of_Analysis.Print(Data, true);
                        Cursor = Cursors.Default;
                    }
                }
                #endregion
                #region ------------- print Packing Slip -------------
                if (DLR_Message != DialogResult.OK)
                {
                    if (e.RowIndex != -1)    // Stop a crash when clicking on the Print header - BN 20/01/2015
                    {

                        string Data = "";
                        Data = Data + Convert.ToString(int_Loop_count);
                        Data = Data + ":" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                        Data = Data + ":" + str_delivery_name;
                        Data = Data + ":" + FruPak.PF.Data.Outlook.Contacts.Contact_Business_Address.ToString();
                        Data = Data + ":Print"; // Added 12/08/2015 BN - Was crashing due to index out of array bounds

                        Cursor = Cursors.WaitCursor;
                        //set printer
                        FruPak.PF.PrintLayer.Word.Printer = FruPak.PF.Common.Code.General.Get_Printer("A4");
                        FruPak.PF.PrintLayer.Word.FileName = "PS" + dataGridView1.CurrentRow.Cells[0].Value.ToString();
                        FruPak.PF.PrintLayer.Packing_Slip.Print(Data, true);
                        Cursor = Cursors.Default;

                    }
                }
                #endregion
                #region ------------- Print Sticky Address Label -------------
                //if (DLR_Message != DialogResult.OK)
                //{
                //    string Data = "";
                //    Data = Data + str_delivery_name;
                //    Data = Data + ":" + FruPak.PF.Data.Outlook.Contacts.Contact_Business_Address.ToString();
                //    Cursor = Cursors.WaitCursor;
                //    //set printer
                //    FruPak.PF.PrintLayer.Word.Printer = FruPak.PF.Common.Code.General.Get_Printer("Custom");
                //    FruPak.PF.PrintLayer.Word.FileName = "SL" + dataGridView1.CurrentRow.Cells[0].Value.ToString();
                //    FruPak.PF.PrintLayer.StickyAddressLabel.Print(Data, true);
                //    Cursor = Cursors.Default;
                //}
                #endregion
                #region ------------- Cleanup -------------
                if (e.RowIndex != -1)    // Stop a crash when clicking on the Print header - BN 20/01/2015
                {

                    FruPak.PF.Common.Code.General.Delete_After_Printing("", "*" + dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString() + "*");
                    FruPak.PF.Common.Code.General.Delete_After_Printing("", "COA" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() + "*");
                    FruPak.PF.Common.Code.General.Delete_After_Printing("", "PS" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() + "*");
                    FruPak.PF.Common.Code.General.Delete_After_Printing("", "SL" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() + "*");

                    foreach (string filename in lst_filenames)
                    {
                        File.Delete(filename);
                    }
                }
                #endregion
            }
            #endregion
            #region ------------- Delete -------------
            else if (e.ColumnIndex == 12)
            {
                if (e.RowIndex != -1)    // Stop a crash when clicking on the Delete header - BN 20/01/2015
                {

                    int int_result = FruPak.PF.Common.Code.General.Delete_Record("PF_Orders", dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString(), " Order Number " + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());

                    if (int_result > 0)
                    {
                        lbl_message.ForeColor = System.Drawing.Color.Blue;
                        lbl_message.Text = "Order " + dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString() + " has been deleted";
                    }
                    else
                    {
                        lbl_message.ForeColor = System.Drawing.Color.Red;
                        lbl_message.Text = "Order " + dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString() + " failed to delete";
                    }
                    populate_DataGridView1();
                }
            }
            #endregion
            #region ------------- Edit -------------
            else if (e.ColumnIndex == 13)
            {
                if (e.RowIndex != -1)    // Stop a crash when clicking on the Edit header - BN 20/01/2015
                {
                    int_order_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                    dtp_Load_Date.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                    customer1.Customer_Id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                    txt_Customer_Ref.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    cmb_Truck.SelectedValue = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
                    cmb_Destination.SelectedValue = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString());
                    txt_Freight_Docket.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                    txt_Comments.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                    btn_Add.Text = "&Update";
                    populate_DataGridView2();
                    DataSet ds_Get_Info = FruPak.PF.Data.AccessLayer.PF_Orders_Pallets.Get_Info_for_Order(int_order_id);
                    DataRow dr_Get_Info;
                    for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
                    {
                        dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                        for (int j = 0; j < Convert.ToInt32(dataGridView2.Rows.Count.ToString()); j++)
                        {
                            if (Convert.ToInt32(dr_Get_Info["PalletType_Id"].ToString()) == Convert.ToInt32(dataGridView2.Rows[j].Cells[0].Value.ToString()))
                            {
                                dataGridView2.Rows[j].Cells[2].Value = dr_Get_Info["Number"].ToString();
                            }
                        }
                    }
                    ds_Get_Info.Dispose();

                    Form frm = new Pallet_Details(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString()), int_Current_User_Id, bol_write_access);
                    frm.ShowDialog();
                }
            }
            #endregion
        }
        private void dataGridView2_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                try
                {
                    if (dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue.ToString() != "")
                    {
                        Convert.ToDecimal(dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue.ToString());

                        // BN 20/01/2015 log both the description and value here
                        logger.Log(LogLevel.Info, DecorateString("dataGridView2_CellLeave",
                            dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex -1].EditedFormattedValue.ToString() + ": " +
                            dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue.ToString(),
                            "(CellLeave)"));
                    }
                }
                catch
                {
                    MessageBox.Show("Invalid number of Pallets/Bins. You can only enter numerics in this field.", "Process Factory - Disptach(Orders)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btn_reset_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void Reset()
        {
            customer1.Customer_Id = 0;
            cmb_Destination.Text = null;
            cmb_Truck.Text = null;
            txt_Comments.ResetText();
            txt_Customer_Ref.ResetText();
            txt_Freight_Docket.ResetText();
            btn_Add.Text = "&Add";
            populate_DataGridView2();
        }
        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            string str_cmb_cust = "";
            int int_cmb_dest = 0;
            int int_cmb_Truck = 0;

            if (customer1.Customer_Id <= 0)
            {
                str_cmb_cust = customer1.Customer_Id.ToString();
            }
            if (cmb_Destination.SelectedValue != null)
            {
                int_cmb_dest = Convert.ToInt32(cmb_Destination.SelectedValue.ToString());
            }
            if (cmb_Truck.SelectedValue != null)
            {
                int_cmb_Truck = Convert.ToInt32(cmb_Truck.SelectedValue.ToString());
            }
            populate_combobox();

            customer1.Customer_Id = Convert.ToInt32(str_cmb_cust);
            cmb_Destination.SelectedValue = int_cmb_dest;
            cmb_Truck.SelectedValue = int_cmb_Truck;
        }
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Obsoleted code 29/01/2015 BN
        //private void customer1_CustomerChanged(object sender, EventArgs e)
        //{
        //    // BN 20/01/2015
        //    if(customer1.Customer_Name != null && customer1.Customer_Name != string.Empty)
        //    {
        //        logger.Log(LogLevel.Info, DecorateString("customer1_CustomerChanged", customer1.Customer_Name, "(Changed)"));

        //    }
        //}
        //#region Decorate String
        ///// <summary>
        ///// Formats the output to the debug log so that the relevant bits stand out amongst the machine-generated stuff.
        ///// </summary>
        ///// <param name="name"></param>
        ///// <param name="input"></param>
        ///// <param name="action"></param>
        ///// <returns></returns>
        //private string DecorateString(string name, string input, string action)
        //{
        //    // This code is intentionally duplicated from Main_Menu.cs, as it's faster
        //    // to find the faulty code - This Class/Form is a known major problem area.

        //    string output = string.Empty;

        //    output = intro + name + openPad + input + closePad + action + outro;
        //    return output;
        //}
        //#endregion

        //private void dtp_Load_Date_ValueChanged(object sender, EventArgs e)
        //{
        //    if(dtp_Load_Date.Value != null)
        //    {
        //        logger.Log(LogLevel.Info, DecorateString("dtp_Load_Date_ValueChanged", dtp_Load_Date.Value.ToShortDateString(), "(Changed)"));
        //    }
        //}

        //private void cmb_Truck_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    if(cmb_Truck.Text != null && cmb_Truck.Text != string.Empty)
        //    {
        //        logger.Log(LogLevel.Info, DecorateString("cmb_Truck_SelectedValueChanged", cmb_Truck.Text, "(Changed)"));
        //    }
        //}

        //private void cmb_Destination_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    if (cmb_Destination.Text != null && cmb_Destination.Text != string.Empty)
        //    {
        //        logger.Log(LogLevel.Info, DecorateString("cmb_Destination_SelectedValueChanged", cmb_Destination.Text, "(Changed)"));
        //    }
        //}

        //private void txt_Customer_Ref_Leave(object sender, EventArgs e)
        //{
        //    if (txt_Customer_Ref.Text != null && txt_Customer_Ref.Text != string.Empty)
        //    {
        //        logger.Log(LogLevel.Info, DecorateString("txt_Customer_Ref_Leave", txt_Customer_Ref.Text, "(Leave)"));
        //    }
        //}

        //private void txt_Comments_Leave(object sender, EventArgs e)
        //{
        //    if (txt_Comments.Text != null && txt_Comments.Text != string.Empty)
        //    {
        //        logger.Log(LogLevel.Info, DecorateString("txt_Comments_Leave", txt_Comments.Text, "(Leave)"));
        //    }
        //}

        //private void txt_Freight_Docket_Leave(object sender, EventArgs e)
        //{
        //    if (txt_Freight_Docket.Text != null && txt_Freight_Docket.Text != string.Empty)
        //    {
        //        logger.Log(LogLevel.Info, DecorateString("txt_Freight_Docket_Leave", txt_Freight_Docket.Text, "(Leave)"));
        //    }
        //}

        //private void ckb_Hold_For_Payment_CheckedChanged(object sender, EventArgs e)
        //{
        //    logger.Log(LogLevel.Info, DecorateString("ckb_Hold_For_Payment_CheckedChanged", ckb_Hold_For_Payment.Checked.ToString(), "(Changed)"));
        //}
        #endregion

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
        private void Shipping_Order_KeyDown(object sender, KeyEventArgs e)
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
