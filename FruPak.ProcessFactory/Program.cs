using NLog;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace FruPak.ProcessFactory
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
            try
            {
                //set to production
                FruPak.PF.Global.Global.bol_Testing = false;

                //FruPak.PF.Global.Configuration.App.Configuration.Initialize();
                //FruPak.PF.Global.Configuration.App.Configuration.Write();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                #region ThreadException and UnhandledException Handlers - Register Handlers - Phantom 9/12/2014

                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

                Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

                // Add the event handler for handling UI thread exceptions to the event.
                //Application.ThreadException += new ThreadExceptionEventHandler(Form1_UIThreadException);

                //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                // Add the event handler for handling non-UI thread exceptions to the event. 
                AppDomain.CurrentDomain.UnhandledException +=
                    new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                // -----------------------------------------------[ Testing Exceptions only - not part of original design ]----------------------
                Application.Run(new FruPak.PF.Menu.Main_Menu());
                // -----------------------------------------------[ Testing Exceptions only - not part of original design ]----------------------

                #endregion ThreadException and UnhandledException Handlers - Register Handlers - Phantom 9/12/2014

                // -------------------------------------[ This is the original code ]------------------------------------------------------------
                //if (args.Count() > 0)
                //{
                //    Application.Run(new Install());
                //}
                //else
                //{
                //    DateTime dt = new DateTime();

                //    // Added 09-01-2014 BN
                //    double copyDelayInMinutes = Install.CopyDelayInMinutes;

                //    //dt = Install.Get_Last_execution(Install.get_MAC()).AddHours(1);
                //    dt = Install.Get_Last_execution(Install.get_MAC()).AddMinutes(copyDelayInMinutes);

                //    try
                //    {
                //        if (dt.ToUniversalTime() < DateTime.Now.ToUniversalTime())
                //        {
                //            Application.Run(new Install());
                //        }
                //        Application.Run(new FruPak.PF.Menu.Main_Menu());
                //    }
                //    catch (Exception ex)
                //    {
                //        logger.Log(LogLevel.Debug, ex.Message);
                //    }
                //}
                // -------------------------------------[ This is the original code ]------------------------------------------------------------
            }
            catch (Exception ex)
            {
                // Doesn't work here. Not important.
                PF.Common.Code.DebugStacktrace.StackTrace(ex);
            }
        }

        #region Application_ThreadException - Handler Method - Phantom 9/12/2014

        /// <summary>
        /// Handles any UnHandled Exceptions - 20-10-2015
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            StackTrace st = new StackTrace(e.Exception, true);
            StackFrame[] frames = st.GetFrames();
            string displaytrace = string.Empty; // Could just use string displaytrace = "";
            string trace = string.Empty;        // but I like = string.Empty.

            // Iterate over the frames extracting the information you need
            // Could use frames[0] indexing to pull the info off the first frame, but why bother - this works.
            foreach (StackFrame frame in frames)
            {
                // -----------------------------------------------------------------------------------------------------------------------------------------------
                // Horrific example from MSDN
                // Console.WriteLine("{0}:{1}({2},{3})", frame.GetFileName(), frame.GetMethod().Name, frame.GetFileLineNumber(), frame.GetFileColumnNumber());
                // -----------------------------------------------------------------------------------------------------------------------------------------------

                // ------- My Code -------
                // Note the elegant usage of Carriage Return + NewLine (\r\n) - Could also use Environment.NewLine
                // Also note the usage of tab stops to get everything to line up visually (\t)

                // --- For MessageBox/Email Display ---
                displaytrace += frame.GetFileName() + "\r\n\r\nMethod:\t" +
                    frame.GetMethod().Name + "\r\nLine:\t" +
                    frame.GetFileLineNumber() + "\r\nColumn:\t" +
                    frame.GetFileColumnNumber();

                // --- For Console and CSV Logging ---
                // All this needs to go in one contiguous line so it fits nicely into a single cell in Excel (log.csv)
                // So, everything is appended into one single string (trace)
                trace += frame.GetFileName() + ", Method: " +
                    frame.GetMethod().Name + ", Line: " +
                    frame.GetFileLineNumber() + ", Column" +
                    frame.GetFileColumnNumber();

                break;  // Too much info - only want the first frame off the stack - the rest is crap - Exit the loop
            }

            // Log the exception message
            logger.Log(LogLevel.Debug, "App_ThreadException - " + e.Exception.Message);

            // If there is an InnerException, log that as well
            if (e.Exception.InnerException != null)
            {
                logger.Log(LogLevel.Debug, "App_ThreadException - Inner - " + e.Exception.InnerException.ToString());
            }

            // Grab everything possible about the error and log it to the CSV file
            logger.Log(LogLevel.Debug, trace);  

            // Add a division marker (of sorts) here
            //logger.Log(LogLevel.Info, "Calling: SendDebugInfo");

            //// Now get the debug email ready by creating a new debug form called sde.
            //FruPak.PF.Common.Code.SendDebugEmail sde = new PF.Common.Code.SendDebugEmail();

            //// Auto-populate the error - Add a blank line ("\r\n\r\n") as a prefix so they can type in any extra info 
            //// (It'll never happen)
            //sde.textBoxBugReport.Text = "\r\n\r\n" + e.Exception.Message + "\r\n\r\n" + displaytrace;
            
            //// Deselect all text and move the caret to the first position in the text box
            //// If anyone cares enough to type out an explanation, they can do so here without wiping out the error trace details.
            //sde.textBoxBugReport.Select(0, 0);  

            //sde.Show();
            //sde.TopMost = true; // 21/10/2015 BN
            //// At this point they can choose to send or not, but the error has already been logged out to the CSV no matter what.
        }

        #endregion Application_ThreadException - Handler Method - Phantom 9/12/2014

        #region CurrentDomain_UnhandledException - Handler Method - Phantom 9/12/2014

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show((e.ExceptionObject as Exception).Message, "Bruce - Main Menu: Unhandled UI Exception");
            // here you can log the exception ...
            logger.Log(LogLevel.Debug, (e.ExceptionObject as Exception).Message);
        }

        #endregion CurrentDomain_UnhandledException - Handler Method - Phantom 9/12/2014



        // Handle the UI exceptions by showing a dialog box, and asking the user whether
        // or not they wish to abort execution.
        private static void Form1_UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            DialogResult result = DialogResult.Cancel;
            try
            {
                result = ShowThreadExceptionDialog("Windows Forms Error", t.Exception);
            }
            catch
            {
                try
                {
                    MessageBox.Show("Fatal Windows Forms Error",
                        "Fatal Windows Forms Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }

            // Exits the program when the user clicks Abort.
            if (result == DialogResult.Abort)
                Application.Exit();
        }

        // Handle the UI exceptions by showing a dialog box, and asking the user whether
        // or not they wish to abort execution.
        // NOTE: This exception cannot be kept from terminating the application - it can only 
        // log the event, and inform the user about it. 
        //private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        //{
        //    try
        //    {
        //        Exception ex = (Exception)e.ExceptionObject;
        //        string errorMsg = "An application error occurred. Please contact the adminstrator " +
        //            "with the following information:\n\n";

        //        // Since we can't prevent the app from terminating, log this to the event log.
        //        if (!EventLog.SourceExists("ThreadException"))
        //        {
        //            EventLog.CreateEventSource("ThreadException", "Application");
        //        }

        //        // Create an EventLog instance and assign its source.
        //        EventLog myLog = new EventLog();
        //        myLog.Source = "ThreadException";
        //        myLog.WriteEntry(errorMsg + ex.Message + "\n\nStack Trace:\n" + ex.StackTrace);
        //    }
        //    catch (Exception exc)
        //    {
        //        try
        //        {
        //            MessageBox.Show("Fatal Non-UI Error",
        //                "Fatal Non-UI Error. Could not write the error to the event log. Reason: "
        //                + exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //        }
        //        finally
        //        {
        //            Application.Exit();
        //        }
        //    }
        //}

        // Creates the error message and displays it.
        private static DialogResult ShowThreadExceptionDialog(string title, Exception e)
        {
            string errorMsg = "An application error occurred. Please contact the adminstrator " +
                "with the following information:\n\n";
            errorMsg = errorMsg + e.Message + "\n\nStack Trace:\n" + e.StackTrace;
            return MessageBox.Show(errorMsg, title, MessageBoxButtons.AbortRetryIgnore,
                MessageBoxIcon.Stop);
        }
    }
}