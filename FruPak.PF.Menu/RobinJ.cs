using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;
using System.Diagnostics;

namespace FruPak.PF.Menu
{
    public partial class RobinJ : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();     

        public static DateTime startTime;
        public static string str_Name;
        Timer Clock;
        public RobinJ(string Name_in, string startTime_in)
        {
            startTime = Convert.ToDateTime(startTime_in);
            str_Name = Name_in;
            InitializeComponent();
            Clock = new Timer();
            Clock.Interval = 1000;
            Clock.Start();
            Clock.Tick += new EventHandler(Timer_Tick);
        }
        
        public string GetTime()
        {
            string TimeInString = "";
            int hour = DateTime.Now.Hour;
            int min = DateTime.Now.Minute;
            int sec = DateTime.Now.Second;

            TimeInString = (hour < 10) ? "0" + hour.ToString() : hour.ToString();
            TimeInString += ":" + ((min < 10) ? "0" + min.ToString() : min.ToString());
            TimeInString += ":" + ((sec < 10) ? "0" + sec.ToString() : sec.ToString());
            return TimeInString;
        }

        [DebuggerStepThrough]
        public void Timer_Tick(object sender, EventArgs eArgs)
        {
            lbl_month.Text = "";
            lbl_month.Text = str_Name;
            if (sender == Clock) 
            {
               
                double dbl_month = Math.Floor(((startTime - DateTime.Now).TotalDays / 365.25 * 12));
                double dbl_days = Math.Floor((startTime - DateTime.Now).TotalDays - (dbl_month / 12 * 365.25));
                try
                {
                    if (dbl_month > 0)
                    {
                        lbl_month.Text = Convert.ToString(dbl_month) + " Months ";
                    }
                    if (dbl_days > 0)
                    {
                        lbl_month.Text = lbl_month.Text + Convert.ToString(dbl_days) + " Days "; 
                    }
                    lbl_month.Text = lbl_month.Text + ((startTime - DateTime.Now)).ToString("hh\\:mm\\:ss");
                    lbl_month.Text = lbl_month.Text + " and Counting Down";
                }
                catch (Exception ex)
                {
                    logger.Log(LogLevel.Debug, ex.Message);
                }
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RobinJ_Shown(object sender, EventArgs e)
        {
            // BN 21/12/2014
            // I really don't care how long it is until your retirement
            // and this form just gets in the way unnecessarily.
            this.Close();
        }

        /// <summary>
        /// Close the form with the Esc key (Sel request 11-02-2015 BN)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RobinJ_KeyDown(object sender, KeyEventArgs e)
        {
            // Note that the KeyPreview property must be set to true for this to work
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        } 
    }
}  
