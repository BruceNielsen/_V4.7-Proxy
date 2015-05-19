using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FruPak.PF.Global
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

        private static bool _bol_Testing = true;
        public static bool bol_Testing
        {
            get {return _bol_Testing;}
            set { _bol_Testing = value; }
        }

        private static bool _bol_Timer_Msg = true;
        public static bool bol_Timer_Msg
        {
            get { return _bol_Timer_Msg; }
            set { _bol_Timer_Msg = value; }
        }

        /// <summary>
        /// Clean the comment field of any illegal SQL characters (just ' for now)
        /// </summary>
        /// <param name="Comment">String value to be cleaned</param>
        /// <returns>Cleaned string value</returns>
        public static string CleanSqlComment (string Comment)
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

    }
}
