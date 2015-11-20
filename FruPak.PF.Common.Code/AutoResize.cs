using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PF.Common.Code
{
    public class AutoResize
    {
        private List<System.Drawing.Rectangle> _arr_control_storage = new List<System.Drawing.Rectangle>();
        private bool showRowHeader = false;

        public AutoResize(Form _form_)
        {
            form = _form_;                  // The calling form
            _formSize = _form_.ClientSize;  // Save initial form size
            _fontsize = _form_.Font.Size;   // Font size
        }

        private float _fontsize { get; set; }

        private System.Drawing.SizeF _formSize { get; set; }

        private Form form { get; set; }

        public void _get_initial_size()                     // Get initial size
        {
            var _controls = _get_all_controls(form);        // Call the enumerator
            foreach (Control control in _controls)          // Loop through the controls
            {
                _arr_control_storage.Add(control.Bounds);   // Saves control bounds/dimension

                // If you have a datagridview
                if (control.GetType() == typeof(DataGridView))
                    _dgv_Column_Adjust(((DataGridView)control), showRowHeader);
            }
        }

        public void _resize() // Set the resize
        {
            double _form_ratio_width = (double)form.ClientSize.Width / (double)_formSize.Width;      // Ratio could be greater or less than 1
            double _form_ratio_height = (double)form.ClientSize.Height / (double)_formSize.Height;  // This one too
            var _controls = _get_all_controls(form);                                                //reenumerate the control collection
            int _pos = -1;                                                                          //do not change this value unless you know what you are doing

            // Do some math calculations
            foreach (Control control in _controls)
            {
                _pos += 1; // Increment by 1;
                System.Drawing.Size _controlSize = new System.Drawing.Size((int)(_arr_control_storage[_pos].Width * _form_ratio_width),
                    (int)(_arr_control_storage[_pos].Height * _form_ratio_height)); // Use for sizing

                System.Drawing.Point _controlposition = new System.Drawing.Point((int)
                (_arr_control_storage[_pos].X * _form_ratio_width), (int)(_arr_control_storage[_pos].Y * _form_ratio_height)); // Use for location

                // Set bounds
                control.Bounds = new System.Drawing.Rectangle(_controlposition, _controlSize); //Put together

                // Assuming you have a datagridview inside a form()
                // If you want to show the row header, replace the false statement of
                // showRowHeader on top/public declaration to true;
                if (control.GetType() == typeof(DataGridView))
                    _dgv_Column_Adjust(((DataGridView)control), showRowHeader);

                //Font AutoSize
                control.Font = new System.Drawing.Font(form.Font.FontFamily,
                 (float)(((Convert.ToDouble(_fontsize) * _form_ratio_width) / 2) +
                  ((Convert.ToDouble(_fontsize) * _form_ratio_height) / 2)));
            }
        }

        /// <summary>
        /// If you have a Datagridview and want to resize the column based upon its dimensions.
        /// </summary>
        /// <param name="dgv">The DGV.</param>
        /// <param name="_showRowHeader">if set to <c>true</c> [_show row header].</param>
        private void _dgv_Column_Adjust(DataGridView dgv, bool _showRowHeader)
        {
            int intRowHeader = 0;
            const int Hscrollbarwidth = 5;
            if (_showRowHeader)
                intRowHeader = dgv.RowHeadersWidth;
            else
                dgv.RowHeadersVisible = false;

            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                if (dgv.Dock == DockStyle.Fill) // In case the datagridview is docked
                    dgv.Columns[i].Width = ((dgv.Width - intRowHeader) / dgv.ColumnCount);
                else
                    dgv.Columns[i].Width = ((dgv.Width - intRowHeader - Hscrollbarwidth) / dgv.ColumnCount);
            }
        }

        private static IEnumerable<Control> _get_all_controls(Control c)
        {
            return c.Controls.Cast<Control>().SelectMany(item =>
                _get_all_controls(item)).Concat(c.Controls.Cast<Control>()).Where(control =>
                control.Name != string.Empty);
        }
    }
}