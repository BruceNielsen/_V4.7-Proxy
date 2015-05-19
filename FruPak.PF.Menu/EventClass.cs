using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruPak.PF.Menu
{
    //public class EventClass
    //{

    //}
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

    //public class ObjectEventArgs : EventArgs
    //{
    //    // Private field exposed by the Object property
    //    private object _object;

    //    public ObjectEventArgs(object ObjectMessage)
    //    {
    //        this._object = ObjectMessage;
    //    }

    //    // Read only property.
    //    public object MenuObject
    //    {
    //        get { return _object; }
    //    }
    //}

    public class GeneralPurposeObjectEventArgs : EventArgs
    {
        // Private fields exposed by the GeneralPurpose properties
        
        private string _generalPurposeDescription;  // Might be handy
        private object _generalPurposeObject;       // Object for casting later
        private string _generalPurposeText;         // Name, probably

        public GeneralPurposeObjectEventArgs(string GP_Description, object GP_Object, string GP_Text)
        {
            this._generalPurposeDescription = GP_Description;
            this._generalPurposeObject = GP_Object;
            this._generalPurposeText = GP_Text;
        }

        // Read only properties.
        public string GP_Description
        {
            get { return _generalPurposeDescription; }
        }        
        
        public object GP_Object
        {
            get { return _generalPurposeObject; }
        }        
        
        public string GP_Text
        {
            get { return _generalPurposeText; }
        }        
        

    }
}
