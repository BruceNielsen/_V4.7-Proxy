using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FruPak.PF.Accounts
{
    public class HistoryUpdateEventArgs : System.EventArgs
    {
        // add local member variables to hold text
        private string _HistoryMessage;

        // class constructor
        public HistoryUpdateEventArgs(string sHistory)
        {
            this._HistoryMessage = sHistory;
        }

        // Properties - Viewable by each listener
        public string History
        {
            get
            {
                return _HistoryMessage;
            }
        }
    }
}
