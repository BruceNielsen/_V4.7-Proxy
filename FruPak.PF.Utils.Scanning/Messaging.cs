using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.Utils.Scanning
{
    /// <summary>
    /// Built 21-22/11/2015 BN (Over the weekend) to add communication between forms
    /// </summary>
    public class MessageUpdateEventArgs : System.EventArgs
    {
        // add local member variables to hold text
        private string mHeader;
        private string mMessage;

        // Class constructor
        public MessageUpdateEventArgs(string sHeader, string sMessage)
        {
            this.mHeader = sHeader;
            this.mMessage = sMessage;
        }

        // Properties - Viewable by each listener
        public string Header
        {
            get
            {
                return mHeader;
            }
        }
        public string Message
        {
            get
            {
                return mMessage;
            }
        }
    }
}
