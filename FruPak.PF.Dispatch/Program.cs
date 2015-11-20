using NLog;
using System;
using System.Threading;
using System.Windows.Forms;

namespace PF.Dispatch
{
    internal static class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            #region ThreadException and UnhandledException Handlers - Register Handlers - Phantom 9/12/2014

            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            #endregion ThreadException and UnhandledException Handlers - Register Handlers - Phantom 9/12/2014

            //  Application.Run(new ShippingOrder(8, true));
            Application.Run(new Shipping_Search(1, true));
        }

        #region Application_ThreadException - Handler Method - Phantom 9/12/2014

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            //MessageBox.Show(e.Exception.Message, "Unhandled Thread Exception");
            // here you can log the exception ...
            logger.Log(LogLevel.Debug, e.Exception.Message);
        }

        #endregion Application_ThreadException - Handler Method - Phantom 9/12/2014

        #region CurrentDomain_UnhandledException - Handler Method - Phantom 9/12/2014

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //MessageBox.Show((e.ExceptionObject as Exception).Message, "Unhandled UI Exception");
            // here you can log the exception ...
            logger.Log(LogLevel.Debug, (e.ExceptionObject as Exception).Message);
        }

        #endregion CurrentDomain_UnhandledException - Handler Method - Phantom 9/12/2014
    }
}