using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NLog;
using System.Threading;

namespace FruPak.PF.StockControl
{
    static class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();     

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            #region ThreadException and UnhandledException Handlers - Register Handlers - Phantom 9/12/2014
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            #endregion

            Application.Run(new Stock_Used(8, true));
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
