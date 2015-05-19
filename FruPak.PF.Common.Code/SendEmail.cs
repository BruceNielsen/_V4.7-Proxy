using System;
using System.Net.Mail;
using System.Collections.Generic;
using NLog;
using FruPak.PF.CustomSettings;
using System.Windows.Forms;
using System.IO;

namespace FruPak.PF.Common.Code
{
    public class SendEmail
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static PhantomCustomSettings Settings;

        public static string Subject
        {
            get;
            set;
        }
        public static bool IsBodyHtml
        {
            get;
            set;
        }
        public static string message
        {
            get;
            set;
        }
        public static List<string> attachment
        {
            get;
            set;
        }
        public static List<string> Recipient
        {
            get;
            set;
        }
        public static List<string> BCC_Email_Address
        {
            get;
            set;
        }
        public static string From_Email_Address
        {
            get;
            set;
        }
        public static string Network_UserId
        {
            get;
            set;
        }
        public static string Network_Password
        {
            get;
            set;
        }
        public static string send_mail()
        {
            string str_return = "";
            try
            {
                MailMessage oMsg = new MailMessage();


                foreach (string recipient in Recipient)
                {
                    oMsg.To.Add(recipient);
                }

                if (BCC_Email_Address.Count > 0)
                {
                    foreach (string bcc in BCC_Email_Address)
                    {
                        oMsg.Bcc.Add(bcc);
                    }
                }

                oMsg.From = new MailAddress(From_Email_Address);
                oMsg.Subject = Subject;
                oMsg.Body = message;
                oMsg.IsBodyHtml = IsBodyHtml;

                if (attachment.Count > 0 )
                {
                    foreach (string str_attachment in attachment)
                    {
                        try
                        {
                            Attachment mailAttachment = new Attachment(str_attachment);
                            oMsg.Attachments.Add(mailAttachment);
                        }
                        catch (Exception ex)
                        {
                            logger.Log(LogLevel.Debug, ex.Message);
                        }
                    }
                    attachment.Clear();
                }
                SmtpClient smtp = new SmtpClient();
                
                // Am pretty sure I'm not really needing to do al the load thingy, but until I refactor it all...

                #region Load CustomSettings
                #region Get "Application.StartupPath" folder
                string path = Application.StartupPath;
                #endregion

                // Initialize settings
                Settings = new PhantomCustomSettings();
                Settings.SettingsPath = Path.Combine(path, "FruPak.Phantom.config");
                Settings.EncryptionKey = "phantomKey";

                if (!File.Exists(Settings.SettingsPath))
                {
                    Settings.Save();
                    logger.Log(LogLevel.Info, LogCodeStatic("Default Phantom Settings file created (Sendmail)"));
                }
                // Load settings - Normally in Form_Load
                Settings.Load();

                logger.Log(LogLevel.Info, LogCodeStatic("Settings file opened."));

                #endregion


                //smtp.Host = "FRUPAK-SBS.frupak.local";
                smtp.Host = Settings.Path_SMTP_Host;
                smtp.Credentials = new System.Net.NetworkCredential(Network_UserId, Network_Password);

                smtp.Send(oMsg);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Trace, ex.Message);
                logger.Log(LogLevel.Trace, ex.InnerException);

                MessageBox.Show("Exception: " + 
                    ex.Message.ToString() + 
                    "\r\nInner Exception: " + 
                    ex.InnerException.ToString(), "Email Problem");

                str_return = ex.Message.ToString();
            }                    

            return str_return;
        }
        public static string Send_Debug_Email(string Message)
        {
            string str_return = "";
            try
            {
                MailMessage oMsg = new MailMessage();


                foreach (string recipient in Recipient)
                {
                    oMsg.To.Add(recipient);
                }

                if (BCC_Email_Address.Count > 0)
                {
                    foreach (string bcc in BCC_Email_Address)
                    {
                        oMsg.Bcc.Add(bcc);
                    }
                }

                oMsg.From = new MailAddress(From_Email_Address);
                oMsg.Subject = Subject;
                oMsg.Body = message;
                oMsg.IsBodyHtml = IsBodyHtml;

                if (attachment.Count > 0)
                {
                    foreach (string str_attachment in attachment)
                    {
                        try
                        {
                            Attachment mailAttachment = new Attachment(str_attachment);
                            oMsg.Attachments.Add(mailAttachment);
                        }
                        catch (Exception ex)
                        {
                            logger.Log(LogLevel.Debug, ex.Message);
                        }
                    }
                    attachment.Clear();
                }
                SmtpClient smtp = new SmtpClient();

                // Am pretty sure I'm not really needing to do al the load thingy, but until I refactor it all...

                #region Load CustomSettings
                #region Get "Application.StartupPath" folder
                string path = Application.StartupPath;
                #endregion

                // Initialize settings
                Settings = new PhantomCustomSettings();
                Settings.SettingsPath = Path.Combine(path, "FruPak.Phantom.config");
                Settings.EncryptionKey = "phantomKey";

                if (!File.Exists(Settings.SettingsPath))
                {
                    Settings.Save();
                    logger.Log(LogLevel.Info, LogCodeStatic("Default Phantom Settings file created (Sendmail)"));
                }
                // Load settings - Normally in Form_Load
                Settings.Load();

                logger.Log(LogLevel.Info, LogCodeStatic("Settings file opened."));

                #endregion


                //smtp.Host = "FRUPAK-SBS.frupak.local";
                smtp.Host = Settings.Path_SMTP_Host;
                smtp.Credentials = new System.Net.NetworkCredential(Network_UserId, Network_Password);

                smtp.Send(oMsg);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Trace, ex.Message);
                str_return = ex.Message.ToString();
            }

            return str_return;
        }

        #region Log Code Static
        /// <summary>
        /// Formats the output to the debug log so that the actual log contents stand out amongst the machine-generated stuff. 
        /// Refers to machine-generated content, not calling specifics.
        /// </summary>
        /// <param name="input">The code function that we want to log.</param>
        /// <returns>A formatted (Bracketed) string</returns>
        public static string LogCodeStatic(string input)
        {
            string Output = "___[ " + input + " ]___";
            return Output;
        }
        #endregion

    }
}
