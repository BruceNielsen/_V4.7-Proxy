﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace FruPak.PF.Utils.Server
{
    public partial class Print_Server : Form
    {
        Timer Print_Timer;
        public Print_Server()
        {
            InitializeComponent();

            //set the environment as this is a background process and is not set anywhere else
            FruPak.PF.Global.Global.bol_Testing = false;

            //if (FruPak.PF.Global.Global.bol_Testing == true)
            //{
            //    this.Text = "FruPak(Server) - Test Environment";
            //}
            //else
            //{
            //    this.Text = "FruPak(Server)";
            //}

            Print_Timer = new Timer();
            Print_Timer.Interval = 1000; // 1 second
            Print_Timer.Start();
            Print_Timer.Tick += new EventHandler(Timer_Tick);
        }

        public void Timer_Tick(object sender, EventArgs eArgs)
        {
            label1.Text = DateTime.Now.ToString();
            Get_Print_Request();
        }

        private void Get_Print_Request()
        {
            DataSet ds = null;
            DataRow dr;
            ds = FruPak.PF.Data.AccessLayer.SY_PrintRequest.Get_Info();      

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ds.Tables[0];

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                int Return_code = 99;
                dr = ds.Tables[0].Rows[i];
                string current_printer = System.Printing.LocalPrintServer.GetDefaultPrintQueue().FullName.ToString();
                try
                {
                    FruPak.PF.PrintLayer.Word.Printer = dr["Printer_Name"].ToString();
                    myPrinters.SetDefaultPrinter(dr["Printer_Name"].ToString());
                }
                catch
                {
                    myPrinters.SetDefaultPrinter(current_printer);
                }
                switch (dr["Template_Name"].ToString())
                {
                    case "PF-Palletcard":
                        Return_code = FruPak.PF.PrintLayer.Pallet_Card.Print(dr["Data"].ToString(), Convert.ToInt32(dr["Mod_User_Id"].ToString()));
                        break;
                    case "PF-BinCard":
                        Return_code = FruPak.PF.PrintLayer.Bin_Card.Print(dr["Data"].ToString(), true);                            
                        break;
                    case "PF-WPC":
                        Return_code = FruPak.PF.PrintLayer.WPC_Card.Print(dr["Data"].ToString(), Convert.ToInt32(dr["Mod_User_Id"].ToString()));
                        break;
                }

                myPrinters.SetDefaultPrinter(current_printer);                       

                if (Return_code == 0)
                {
                    FruPak.PF.Data.AccessLayer.SY_PrintRequest.Update(Convert.ToInt32(dr["PrintRequest_Id"].ToString()), true, 0);
                }
            }
            ds.Dispose();
        }

        private void rdb_prod_test_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_prod.Checked == true)
            {
                FruPak.PF.Global.Global.bol_Testing = false;
            }
            else if (rdb_Test.Checked == true)
            {
                FruPak.PF.Global.Global.bol_Testing = true;
            }

            //if (FruPak.PF.Global.Global.bol_Testing == true)
            //{
            //    this.Text = "FruPak(Server) - Test Environment";
            //}
            //else
            //{
            //    this.Text = "FruPak(Server)";
            //}
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            FruPak.PF.Data.AccessLayer.SY_PrintRequest.Delete_All();
        }

        /// <summary>
        /// Close the form with the Esc key (Sel request 11-02-2015 BN)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Print_Server_KeyDown(object sender, KeyEventArgs e)
        {
            // Note that the KeyPreview property must be set to true for this to work
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
    public static class myPrinters
    {
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Name);
    }
}
