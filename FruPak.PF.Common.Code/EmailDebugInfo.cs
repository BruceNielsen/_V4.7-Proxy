using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;

namespace FruPak.PF.Common.Code
{
    /// <summary>
    /// Bruce Nielsen 2015
    /// </summary>
    public static class EmailDebugInfo
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();     

        private static string savePath = Application.StartupPath;
        private static void CaptureScreen()
        {

            Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }
                bitmap.Save(savePath + "\\logs\\Screenshot.jpg", ImageFormat.Jpeg);
            }
        }

        public static void SendDebugInfo(string ReasonMessage)
        {
            try
            {

                CaptureScreen();

                FruPak.PF.Common.Code.SendEmail.Recipient = new List<string>();
                FruPak.PF.Common.Code.SendEmail.Recipient.Add("processerrors@frupak.co.nz");

                FruPak.PF.Common.Code.SendEmail.BCC_Email_Address = new List<string>();
                //FruPak.PF.Common.Code.SendEmail.BCC_Email_Address.Add(string.Empty);

                FruPak.PF.Common.Code.SendEmail.attachment = new List<string>();
                FruPak.PF.Common.Code.SendEmail.attachment.Add(savePath + "\\logs\\Screenshot.jpg");
                FruPak.PF.Common.Code.SendEmail.attachment.Add(savePath + "\\logs\\log.csv");

                FruPak.PF.Common.Code.SendEmail.Subject = "Debug Info: " + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString();
                FruPak.PF.Common.Code.SendEmail.message = "For your consideration...";
                //FruPak.PF.Common.Code.SendEmail.message = ReasonMessage;
                FruPak.PF.Common.Code.SendEmail.From_Email_Address = "FruPak.PF." + Environment.UserName + "@frupak.co.nz";
                FruPak.PF.Common.Code.SendEmail.Network_UserId = "services";
                FruPak.PF.Common.Code.SendEmail.Network_Password = "Pentium3";

                // Send the email
                string result = FruPak.PF.Common.Code.SendEmail.send_mail();


                if (result.Length == 0)
                {
                    string outputText = string.Empty;

                    outputText =
                        outputText + "Recipient:\t\t" + "processerrors@frupak.co.nz" + Environment.NewLine +
                        outputText + "Attachments:\t" + "Screenshot.jpg, log.csv" + Environment.NewLine +
                        outputText + "Subject:\t\t" + "Debug Info: " + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString() + Environment.NewLine +
                        outputText + "Message:\t\t" + "For your consideration..." + Environment.NewLine +
                        outputText + "From (Logon):\t" + "FruPak.PF (" + Environment.UserName + ")" + Environment.NewLine +
                        outputText + "UserId:\t\t" + "***" + Environment.NewLine +
                        outputText + "Password:\t\t" + "***" + Environment.NewLine;

                    MessageBox.Show(outputText, "Email Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    logger.Log(LogLevel.Trace, result);
                    MessageBox.Show(result, "Oops...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Trace, ex.Message);
                MessageBox.Show(ex.Message);
            }
        }
    }
}
