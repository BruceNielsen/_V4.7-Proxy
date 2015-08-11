using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Data;
using FruPak.PF.CustomSettings;
using NLog;

namespace FruPak.PF.Utils.Scanning
{
    public partial class Reprint_Palletcards : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();     

        // Phantom 17/12/2014 Declare application settings object
        // Added  = new PhantomCustomSettings (); 10/01/2015 to suppress a 'never assigned to' warning
        private static PhantomCustomSettings Settings = new PhantomCustomSettings();

        private List<string> lst_filenames = new List<string>();
        private static int int_Current_User_Id = 0;
        public Reprint_Palletcards(int int_C_User_id)
        {
            InitializeComponent();
            int_Current_User_Id = int_C_User_id;
            barcode1.BarcodeValue = "00394210096400000011";

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

        public static string PrintMessage
        {
            get;
            set;
        }
        private void btn_Print_Click(object sender, EventArgs e)
        {
            int int_result = 9;
            if (barcode1.BarcodeValue.Length > 0)
            {
                FruPak.PF.Common.Code.General.Get_Printer("A4");
                if (rbn_PalletCard.Checked == true)
                {
                    try
                    {
                        int_result = FruPak.PF.PrintLayer.Pallet_Card.Print(barcode1.BarcodeValue.ToString(), true, int_Current_User_Id);
                    }
                    catch
                    {
                        string Data = "";
                        Data = Data + barcode1.BarcodeValue.ToString();
                        Data = Data + ":True";

                        //FruPak.PF.PrintLayer.Word.Printer = "Brother HL-2040 series";
                        FruPak.PF.PrintLayer.Word.Printer = Settings.Printer_Name;  // 16/06/2015 Fixed - Jim worked out there was some hardcoded strings

                        // Phantom 18/12/2014
                        //FruPak.PF.PrintLayer.Word.Printer = Settings.Printer_Name;    // Reverted 06-03-2015

                        FruPak.PF.PrintLayer.Word.Server_Print(Data, int_Current_User_Id);
                    }
                }
                else
                {
                    DataSet ds = null;
                    DataRow dr;
                    ds = FruPak.PF.Data.AccessLayer.PF_Work_Order.Get_Current();
                    int int_current_WO = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dr = ds.Tables[0].Rows[i];
                        int_current_WO = Convert.ToInt32(dr["Work_Order_Id"].ToString());
                    }
                    ds.Dispose();

                    // New trap for SystemOverflowException: Convert.ToInt32(barcode1.Get_Barcode.ToString() - BN 11/08/2015
                    try
                    {
                        ds = FruPak.PF.Data.AccessLayer.PF_Pallet.Get_Pallet_barocde(int_current_WO, Convert.ToInt32(barcode1.BarcodeValue.ToString()));
                        string str_Barcode = "";
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            dr = ds.Tables[0].Rows[i];
                            str_Barcode = dr["Barcode"].ToString();
                        }
                        ds.Dispose();

                        if (str_Barcode.Length > 0)
                        {
                            try
                            {
                                int_result = FruPak.PF.PrintLayer.WPC_Card.Print(str_Barcode, true, int_Current_User_Id);
                            }
                            catch
                            {
                                string Data = "";
                                Data = Data + barcode1.BarcodeValue.ToString();
                                Data = Data + ":True";

                                //FruPak.PF.PrintLayer.Word.Printer = "Brother HL-2040 series";
                                FruPak.PF.PrintLayer.Word.Printer = Settings.Printer_Name;  // 16/06/2015 Fixed - Jim worked out there was some hardcoded strings

                                // Phantom 18/12/2014
                                //FruPak.PF.PrintLayer.Word.Printer = Settings.Printer_Name;   // Reverted 06-03-2015

                                FruPak.PF.PrintLayer.Word.Server_Print(Data, int_Current_User_Id);
                            }
                        }
                        else
                        {
                            int_result = 3; // Added this 11/08/2015 BN
                                            //lbl_message.Text = "Invalid Bin Number";
                        }
                    }
                    catch (Exception ex)
                    {
                        int_result = 3; // Added this 11/08/2015 BN
                        MessageBox.Show(ex.Message, "Error converting number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                if (int_result == 0)
                {
                    lbl_message.Text = PrintMessage = "Pallet Card has been printed";
                    //Clean Up 
                    string filePath = Settings.Path_Printer_Temp;

                    // BN 12-01-2015
                    //lst_filenames.AddRange(System.IO.Directory.GetFiles(@"\\FRUPAK-SBS\PublicDocs\FruPak\Server\Temp", "*" + barcode1.Get_Barcode.ToString() + "*", System.IO.SearchOption.TopDirectoryOnly));
                    lst_filenames.AddRange(System.IO.Directory.GetFiles(filePath, "*" + barcode1.BarcodeValue.ToString() + "*", System.IO.SearchOption.TopDirectoryOnly));

                    foreach (string filename in lst_filenames)
                    {
                        File.Delete(filename);
                    }
                }
                else if (int_result == 3)
                {
                    lbl_message.Text = PrintMessage = "Invalid Bin Number"; // BN
                }
                else
                {
                    lbl_message.Text = PrintMessage = "Pallet Card could not be printed";
                }
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
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
        private void Reprint_Palletcards_KeyDown(object sender, KeyEventArgs e)
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
