using NLog;
using System;
using System.Data;
using System.Windows.Forms;

namespace FruPak.PF.Menu.Tablet
{
    public partial class Tablet_Menu : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static int int_User_id = 0;

        public Tablet_Menu()
        {
            InitializeComponent();

            bool bol_temp_environ = FruPak.PF.Global.Global.bol_Testing;

            var result = FruPak.PF.Utils.Security.Logon.Execute(Environment.UserName, "Scanner");

            if (result.Result == System.Windows.Forms.DialogResult.None)
            {
                FruPak.PF.Global.Global.bol_Testing = bol_temp_environ;
            }

            //if (FruPak.PF.Global.Global.bol_Testing == true)
            //{
            //    this.Text = "FruPak - Test Environment";
            //}
            //else
            //{
            //    this.Text = "FruPak";
            //}

            if (result.Result == DialogResult.Cancel)
            {
                this.Close();
            }
            int_User_id = Convert.ToInt32(result.Field1.ToString());

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

                //else if (c.GetType() == typeof(FruPak.PF.Utils.UserControls.Customer))
                //{
                //    FruPak.PF.Utils.UserControls.Customer cust = (FruPak.PF.Utils.UserControls.Customer)c;
                //    cust.CustomerChanged += new EventHandler(this.CustomerControl_CustomerChanged);
                //}
            }

            #endregion Log any interesting events from the UI to the CSV log file
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form frm = new FruPak.PF.Utils.Scanning.Bin_Tipping(int_User_id, true);
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form frm = new FruPak.PF.Utils.Scanning.Tipping_Onto_Order(int_User_id, true);
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form frm = new FruPak.PF.Utils.Scanning.Pallet_Transfer(int_User_id, true);
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form frm = new FruPak.PF.Utils.Scanning.Reprint_Palletcards(int_User_id);
            //frm.TopMost = true; // 21/10/2015 BN
            frm.ShowDialog();
            lbl_message.Text = FruPak.PF.Utils.Scanning.Reprint_Palletcards.PrintMessage;
            //   frm.Close();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form frm = new FruPak.PF.Utils.Scanning.Repack(int_User_id, true);
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form frm = new FruPak.PF.Utils.Scanning.Bin_Tare_Weights(int_User_id, true);
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form frm = new FruPak.PF.Utils.Scanning.Pallet_Weight(int_User_id, true);
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form frm = new FruPak.PF.Utils.Scanning.GDI_Scanning(int_User_id, true);
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form frm = new FruPak.PF.Utils.Scanning.Create_Small_Rejects(int_User_id);
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form frm = new FruPak.PF.Utils.Scanning.Reject_Bins_Weights(int_User_id);
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DataSet ds = FruPak.PF.Data.AccessLayer.PF_Work_Order.Get_Current();
            DataRow dr;
            int int_current_wo = 0;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dr = ds.Tables[0].Rows[i];
                int_current_wo = Convert.ToInt32(dr["Work_Order_Id"].ToString());
            }
            Form frm = new FruPak.PF.WorkOrder.Tablet_WO_Labels(int_current_wo, int_User_id, true);
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Form frm = new FruPak.PF.Utils.Scanning.Wet_Dry(int_User_id);
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Form frm = new FruPak.PF.Utils.Scanning.Bin_To_Order(int_User_id);
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
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

        //private void CustomerControl_CustomerChanged(object sender, EventArgs e)
        //{
        //    FruPak.PF.Utils.UserControls.Customer cust = (FruPak.PF.Utils.UserControls.Customer)sender;
        //    logger.Log(LogLevel.Info, DecorateString(cust.Name, cust.Customer_Name, "TextChanged"));
        //}

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
        private void Tablet_Menu_KeyDown(object sender, KeyEventArgs e)
        {
            // Note that the KeyPreview property must be set to true for this to work
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        #endregion Methods to log UI events to the CSV file. BN 29/01/2015

        private void buttonCombinedWeights_Click(object sender, EventArgs e)
        {
            Form frm = new FruPak.PF.Utils.Scanning.Bin_Tipping_Combined(int_User_id, true);
            //frm.TopMost = true; // 21/10/2015 BN
            frm.Show();
        }
    }
}