using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruPak.PF.Events
{
    public class EventClass
    {

    }
    public class Global_EventArgs : System.EventArgs
    {
        // add local member variables to hold text
        private string _Message;

        // class constructor
        public Global_EventArgs(string string_Message)
        {
            this._Message = string_Message;
        }

        // Properties - Viewable by each listener
        public string Message
        {
            get
            {
                return _Message;
            }
        }
    }

    public class TextEventArgs : EventArgs
    {
        // Private field exposed by the Text property
        private string _text;

        public TextEventArgs(string TextMessage)
        {
            this._text = TextMessage;
        }

        // Read only property.
        public string Text
        {
            get { return _text; }
        }
    }
}
