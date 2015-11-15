using System;
using System.Windows.Forms;

namespace FruPak.PF.Menu
{
    public partial class InputMaterialNumber : Form
    {
        public event EventHandler<TextEventArgs> NewTextChanged;

        // Private field to hold the value that is passed around.
        // Exposed by the NewText property and passed in the NewTextChanged event.
        private string newText;

        // Experimental
        //public event EventHandler<GeneralPurposeObjectEventArgs> NewGeneralPurposeObjectChanged;
        //private string newGeneralPurposeObject;

        /// <summary>
        /// Constructor
        /// </summary>
        public InputMaterialNumber()
        {
            InitializeComponent();
            textBoxMaterialNumber.TextChanged += new EventHandler(textBoxMaterialNumber_TextChanged);
            //textBoxMaterialNumber.TextChanged += new EventHandler(textBoxMaterialNumber_TextChanged);
        }

        // textBox.TextChanged handler
        private void textBoxMaterialNumber_TextChanged(object sender, EventArgs e)
        {
            NewText = textBoxMaterialNumber.Text;
        }

        public string NewText
        {
            get { return newText; }
            /* Setting this property will raise the event if the value is different.
             * As this property has a public getter you can access it and raise the
             * event from any reference to this class. in this example it is set from
             * the textBox.TextChanged handler above. The setter can be marked as
             * as private if required. */
            set
            {
                if (newText != value)
                {
                    newText = value;
                    OnNewTextChanged(new TextEventArgs(newText));
                }
            }
        }

        // Standard event raising pattern.
        protected virtual void OnNewTextChanged(TextEventArgs e)
        {
            // Create a copy of the event to work with
            EventHandler<TextEventArgs> eh = NewTextChanged;
            /* If there are no subscribers, eh will be null so we need to check
             * to avoid a NullReferenceException. */
            if (eh != null)
                eh(this, e);
        }

        #region Button Handlers

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.textBoxMaterialNumber.Text = string.Empty;
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion Button Handlers
    }
}