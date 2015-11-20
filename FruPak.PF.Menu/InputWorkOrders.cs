using System;
using System.Windows.Forms;

namespace PF.Menu
{
    public partial class InputWorkOrder : Form
    {
        //public event EventHandler<ObjectEventArgs> NewObjectChanged;
        // Private field to hold the value that is passed around.
        // Exposed by the NewObject property and passed in the NewObjectChanged event.
        //private object newObject;

        //public event EventHandler<GeneralPurposeObjectEventArgs> GP_Changed;
        //private string gp_Desc;
        //private object gp_Object;
        //private string gp_Text;

        /// <summary>
        /// Constructor
        /// </summary>
        public InputWorkOrder()
        {
            InitializeComponent();

            // Have to change this to object
            //this.comboBoxDate.TextChanged += new EventHandler(comboBoxDate_ObjectChanged);
            //this.comboBoxDate. += new EventHandler(comboBoxDate_ObjectChanged);

            //textBoxMaterialNumber.TextChanged += new EventHandler(textBoxMaterialNumber_ObjectChanged);
        }

        // textBox.TextChanged handler
        private void comboBoxDate_ObjectChanged(object sender, EventArgs e)
        {
            //NewObject = comboBoxDate.Text;
            //NewObject = null;
            //NewObject = comboBoxDate;
        }

        //public object NewObject
        //{
        //    get { return newObject; }
        //    /* Setting this property will raise the event if the value is different.
        //     * As this property has a public getter you can access it and raise the
        //     * event from any reference to this class. in this example it is set from
        //     * the textBox.TextChanged handler above. The setter can be marked as
        //     * as private if required. */
        //    set
        //    {
        //        if (newObject != value)
        //        {
        //            newObject = value;
        //            OnNewObjectChanged(new ObjectEventArgs(newObject));
        //        }
        //    }
        //}

        //// Standard event raising pattern.
        //protected virtual void OnNewObjectChanged(ObjectEventArgs e)
        //{
        //    // Create a copy of the event to work with
        //    EventHandler<ObjectEventArgs> eh = NewObjectChanged;
        //    /* If there are no subscribers, eh will be null so we need to check
        //     * to avoid a NullReferenceException. */
        //    if (eh != null)
        //        eh(this, e);
        //}

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            //this.textBoxMaterialNumber.Text = string.Empty;
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If you just have this as an argument, it refers to the form
            comboBoxDate_ObjectChanged(this.comboBoxDate, EventArgs.Empty);
        }

        private void comboBoxDate_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBoxDate_ObjectChanged(this.comboBoxDate, EventArgs.Empty);
        }
    }
}