using NLog;
using System;
using System.Drawing;
using System.Windows.Forms;

// Most of the logging here is not needed, but whatever.

namespace FruPak.PF.WorkOrder
{
    public partial class Tablet_WO_Labels : WO_Labels
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Parameterless constructor to avoid designer error - Phantom 12/12/2014
        /// </summary>
        public Tablet_WO_Labels()
        {
        }

        public Tablet_WO_Labels(int int_wo_Id, int int_C_User_id, bool bol_w_a) : base(int_wo_Id, int_C_User_id, bol_w_a)
        {
            InitializeComponent();

            btn_Add.Enabled = true;
            bol_bypass = true;

            //hide controls from inherited form that are not required.
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;

            //move controls to there new location and size.

            this.Size = new System.Drawing.Size(730, 400);

            lbl_output.Font = new Font(lbl_output.Font.FontFamily, 12);

            cmb_Material.Location = new Point(130, 73);
            cmb_Material.Font = new Font(lbl_output.Font.FontFamily, 12);

            lbl_Pallet_Type.Location = new Point(29, 108);
            lbl_Pallet_Type.Font = new Font(lbl_output.Font.FontFamily, 12);

            cmb_Pallet_Type.Location = new Point(130, 105);
            cmb_Pallet_Type.Font = new Font(cmb_Pallet_Type.Font.FontFamily, 12);

            ckb_AutoPrint.Location = new Point(415, 108);
            ckb_AutoPrint.Font = new Font(ckb_AutoPrint.Font.FontFamily, 12);

            ckb_Auto_Reset.Location = new Point(545, 108);
            ckb_Auto_Reset.Font = new Font(ckb_Auto_Reset.Font.FontFamily, 12);

            lbl_Location.Location = new Point(29, 140);
            lbl_Location.Font = new Font(lbl_Location.Font.FontFamily, 12);

            cmb_Location.Location = new Point(130, 136);
            cmb_Location.Font = new Font(cmb_Location.Font.FontFamily, 12);

            ckb_dup.Location = new Point(415, 140);
            ckb_dup.Font = new Font(ckb_dup.Font.FontFamily, 12);

            ckb_plc_on_order.Location = new Point(545, 140);
            ckb_plc_on_order.Font = new Font(ckb_plc_on_order.Font.FontFamily, 12);

            lbl_batch.Location = new Point(150, 180);
            lbl_qunatity.Location = new Point(195, 180);
            lbl_total.Location = new Point(282, 180);

            lbl_order.Location = new Point(390, 180);
            lbl_order.Font = new Font(lbl_order.Font.FontFamily, 12);
            cmb_Orders.Location = new Point(500, 180);
            cmb_Orders.Font = new Font(cmb_Orders.Font.FontFamily, 12);

            rb_EOR1.Location = new Point(29, 200);
            rb_EOR1.Font = new Font(rb_EOR1.Font.FontFamily, 12);

            cmb_Batch1.Location = new Point(150, 200);
            cmb_Batch1.Font = new Font(cmb_Batch1.Font.FontFamily, 12);

            nud_Quantity1.Location = new Point(195, 200);
            nud_Quantity1.Font = new Font(nud_Quantity1.Font.FontFamily, 12);

            txt_Pallet_Total.Location = new Point(282, 200);

            rb_EOR2.Location = new Point(29, 230);
            rb_EOR2.Font = new Font(rb_EOR2.Font.FontFamily, 12);

            cmb_Batch2.Location = new Point(150, 230);
            cmb_Batch2.Font = new Font(cmb_Batch2.Font.FontFamily, 12);

            nud_Quantity2.Location = new Point(195, 230);
            nud_Quantity2.Font = new Font(nud_Quantity2.Font.FontFamily, 12);

            rb_EOR3.Location = new Point(29, 260);
            rb_EOR3.Font = new Font(rb_EOR3.Font.FontFamily, 12);

            cmb_Batch3.Location = new Point(150, 260);
            cmb_Batch3.Font = new Font(cmb_Batch3.Font.FontFamily, 12);

            nud_Quantity3.Location = new Point(195, 260);
            nud_Quantity3.Font = new Font(nud_Quantity2.Font.FontFamily, 12);

            lbl_weight.Location = new Point(271, 245);

            nud_EOR_Weight.Location = new Point(271, 260);
            nud_EOR_Weight.Font = new Font(nud_EOR_Weight.Font.FontFamily, 12);

            btn_Add.Location = new Point(29, 300);
            btn_Add.Font = new Font(btn_Add.Font.FontFamily, 12);
            btn_Add.Size = new System.Drawing.Size(75, 46);

            btn_reset.Location = new Point(110, 300);
            btn_reset.Font = new Font(btn_reset.Font.FontFamily, 12);
            btn_reset.Size = new System.Drawing.Size(75, 46);

            btn_Close.Location = new Point(190, 300);
            btn_Close.Font = new Font(btn_Close.Font.FontFamily, 12);
            btn_Close.Size = new System.Drawing.Size(75, 46);

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
        private void Tablet_WO_Labels_KeyDown(object sender, KeyEventArgs e)
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