using System;

namespace PF.Global
{
    // I want this as a global function
    // string surname = this.surnameTb.Text.Replace("'", "''");

    public class Global
    {
        private static bool _Phantom_Dev_Mode = true;

        public static bool Phantom_Dev_Mode
        {
            get { return _Phantom_Dev_Mode; }
            set { _Phantom_Dev_Mode = value; }
        }

        private static bool _bool_Testing = true;

        public static bool bool_Testing
        {
            get { return _bool_Testing; }
            set { _bool_Testing = value; }
        }

        private static bool _bool_Timer_Msg = true;

        public static bool bool_Timer_Msg
        {
            get { return _bool_Timer_Msg; }
            set { _bool_Timer_Msg = value; }
        }

        /// <summary>
        /// For final email testing - BN 3/12/2015
        /// </summary>
        private static bool _bool_DisableNetworkVerificationForEmailTesting = false;
        public static bool bool_DisableNetworkVerificationForEmailTesting
        {
            get { return _bool_DisableNetworkVerificationForEmailTesting; }
            set { _bool_DisableNetworkVerificationForEmailTesting = value; }
        }

        private static bool _bool_AutomatedTesting = false;
        public static bool bool_AutomatedTesting
        {
            get { return _bool_AutomatedTesting; }
            set { _bool_AutomatedTesting = value; }
        }

        /// <summary>
        /// Clean the comment field of any illegal SQL characters (just ' for now)
        /// </summary>
        /// <param name="Comment">String value to be cleaned</param>
        /// <returns>Cleaned string value</returns>
        public static string CleanSqlComment(string Comment)
        {
            string CleanedSqlComment = string.Empty;
            try
            {
                CleanedSqlComment = Comment.Replace("'", "''");

                // Strip off any trailing \r\n (carriage return + line feed
                CleanedSqlComment = CleanedSqlComment.TrimEnd('\r', '\n');

                return CleanedSqlComment;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
        }

        private static string _LogonName;

        public static string LogonName
        {
            get { return _LogonName; }
            set { _LogonName = value; }
        }
    }
}