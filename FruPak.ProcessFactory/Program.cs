using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using NLog;

namespace FruPak.ProcessFactory
{
    static class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            //set to production
            FruPak.PF.Global.Global.bol_Testing = false;

            
            //FruPak.PF.Global.Configuration.App.Configuration.Initialize();
            //FruPak.PF.Global.Configuration.App.Configuration.Write();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            #region ThreadException and UnhandledException Handlers - Register Handlers - Phantom 9/12/2014
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            #endregion



            if (args.Count() > 0)
            {
                Application.Run(new Install());
            }
            else
            {
                DateTime dt = new DateTime();

                // Added 09-01-2014 BN
                double copyDelayInMinutes = Install.CopyDelayInMinutes;

                //dt = Install.Get_Last_execution(Install.get_MAC()).AddHours(1);
                dt = Install.Get_Last_execution(Install.get_MAC()).AddMinutes(copyDelayInMinutes);

                try
                {
                    if (dt.ToUniversalTime() < DateTime.Now.ToUniversalTime())
                    {
                        Application.Run(new Install());
                    }
                    Application.Run(new FruPak.PF.Menu.Main_Menu());
                }
                catch (Exception ex)
                {
                    logger.Log(LogLevel.Debug, ex.Message);
                }
            }

        }

        #region Application_ThreadException - Handler Method - Phantom 9/12/2014

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            //MessageBox.Show(e.Exception.Message, "Unhandled Thread Exception");
            // here you can log the exception ...
            logger.Log(LogLevel.Debug, e.Exception.Message);

        }
        #endregion

        #region CurrentDomain_UnhandledException - Handler Method - Phantom 9/12/2014
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //MessageBox.Show((e.ExceptionObject as Exception).Message, "Unhandled UI Exception");
            // here you can log the exception ...
            logger.Log(LogLevel.Debug, (e.ExceptionObject as Exception).Message);
        }
        #endregion

    }
}
