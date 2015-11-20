using NLog;
using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PF.Utils.Scanning
{
    public partial class Create_Small_Rejects : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static int int_Current_User_Id = 0;

        public Create_Small_Rejects(int int_C_User_id)
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

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            int int_Current_WO_Id = Get_Current_Work_Order_Id();

            for (int i = 0; i < Convert.ToInt32(nud_required.Value); i++)
            {
                int int_Bin_Id = PF.Common.Code.General.int_max_user_id("CM_Bins");

                PF.Common.Code.Barcode.Barcode_Trader = Get_Trader_Id(int_Current_WO_Id);
                PF.Common.Code.Barcode.Barcode_Create(int_Bin_Id);
                string str_barcode = PF.Common.Code.Barcode.Barcode_Num.ToString();

                PF.Data.AccessLayer.CM_Bins.InsertWorkOrder(int_Bin_Id, int_Current_WO_Id, str_barcode, 456, 1, 4, int_Current_User_Id);
                Print(int_Bin_Id, nud_required.Value);
            }
        }

        /// <summary>
        /// Get the current Work Order being processed
        /// </summary>
        /// <returns></returns>
        private int Get_Current_Work_Order_Id()
        {
            int int_WO_Id = 0;

            DataSet ds = PF.Data.AccessLayer.PF_Work_Order.Get_Current();
            DataRow dr;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dr = ds.Tables[0].Rows[i];
                int_WO_Id = Convert.ToInt32(dr["Work_Order_Id"].ToString());
            }
            ds.Dispose();
            return int_WO_Id;
        }

        /// <summary>
        /// Gets the Trader ID from the current Work Order.
        /// </summary>
        /// <returns></returns>
        private int Get_Trader_Id(int Current_WO_Id)
        {
            int int_Trader_Id = 0;
            DataSet ds = PF.Data.AccessLayer.PF_Work_Order.Get_Info(Current_WO_Id);
            DataRow dr;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dr = ds.Tables[0].Rows[i];
                int_Trader_Id = Convert.ToInt32(dr["Trader_Id"].ToString());
            }
            ds.Dispose();
            return int_Trader_Id;
        }

        private void Print(int int_Bin_Id, decimal total_required)
        {
            string Data = "W:";         //Bin or Submission indicator
            Data = Data + int_Bin_Id;
            Data = Data + ":True";      //Reprint
            Data = Data + ":" + total_required;

            string curent_printer = System.Printing.LocalPrintServer.GetDefaultPrintQueue().FullName.ToString();
            string str_req_Printer = PF.Common.Code.General.Get_Single_System_Code("PR-Bincrd");
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                try
                {
                    if (printer.IndexOf(str_req_Printer) > 0)
                    {
                        PF.PrintLayer.Word.Printer = printer;
                        myPrinters.SetDefaultPrinter(PF.PrintLayer.Word.Printer);
                    }
                }
                catch (Exception ex)
                {
                    logger.Log(LogLevel.Debug, ex.Message);
                    myPrinters.SetDefaultPrinter(curent_printer);
                }
            }

            try
            {
                PF.PrintLayer.Bin_Card.Print(Data, true);
            }
            catch
            {
                PF.Data.AccessLayer.SY_PrintRequest.Insert(PF.Common.Code.General.int_max_user_id("SY_PrintRequest"), "Bincard", "PF-BinCard", "", Data, int_Current_User_Id);
            }
            myPrinters.SetDefaultPrinter(curent_printer);
        }

        private void btn_reprint_Click(object sender, EventArgs e)
        {
            DataSet ds = PF.Data.AccessLayer.CM_Bins.Get_unused_Rejects(Get_Current_Work_Order_Id());
            DataRow dr;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dr = ds.Tables[0].Rows[i];
                Print(Convert.ToInt32(dr["Bins_Id"].ToString()), Convert.ToDecimal(ds.Tables[0].Rows.Count));
            }
            ds.Dispose();
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
        private void Create_Small_Rejects_KeyDown(object sender, KeyEventArgs e)
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