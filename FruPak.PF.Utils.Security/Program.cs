using NLog;
using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace PF.Utils.Security
{
    internal static class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            string str_user_id = "";

            if (args.Count() > 0)
            {
                str_user_id = args[0].ToString();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            #region ThreadException and UnhandledException Handlers - Register Handlers - Phantom 9/12/2014

            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            #endregion ThreadException and UnhandledException Handlers - Register Handlers - Phantom 9/12/2014

            Application.Run(new Logon("DavidB", "Computer"));
            Application.Run(new User_Maintenance(1, true));
            //Application.Run(new User_Group_Maintenance(1, true));

            //Application.Run(new Menu_Maintenance(1,true));
            //Application.Run(new Menu_Panel_Maintenance(1, true));
            //Application.Run(new Maintenance("SC_Log_Action", 1, true));
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