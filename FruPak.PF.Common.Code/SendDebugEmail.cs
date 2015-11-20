using NLog;
using System;
using System.Windows.Forms;

namespace PF.Common.Code
{
    public partial class SendDebugEmail : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public SendDebugEmail()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxBugReport_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxBugReport.Text != string.Empty)
            {
                if (this.textBoxBugReport.Text.Length > 20)
                {
                    this.buttonOk.Enabled = true;
                }
                else
                {
                    this.buttonOk.Enabled = false;
                }
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            logger.Log(LogLevel.Info, "Calling: SendDebugInfo");

            // Hide the form so that it does not appear in the screenshot
            this.Hide();

            // Make sure this form is out of the way
            System.Threading.Thread.Sleep(500);

            // Do the capture
            DebugCaptureScreen.CaptureScreen();

            // Show the form again
            this.Show();

            // Fire off the bug report
            PF.Common.Code.EmailDebugInfo.SendDebugInfo(this.textBoxBugReport.Text);
            
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            this.textBoxBugReport.Text = string.Empty;
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            //this.textBoxBugReport.Text = "Testing from Bruce. Please ignore. " + DateTime.Now + "\r\n\r\n";

            //this.textBoxBugReport.Text += "I’m here to kick ass and chew bubblegum and I’m all out of bubblegum\r\n\r\nThey Live (1998)\r\nhttp://www.imdb.com/title/tt0096256/\r\n\r\n";

            //this.textBoxBugReport.Text += "http://sploid.gizmodo.com/watch-this-insanely-fast-drone-fly-at-impossible-speeds-1701006766";
        }
    }
}