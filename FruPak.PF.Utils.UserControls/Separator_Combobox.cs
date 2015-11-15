/****************************************************************************************************************
(C) Copyright 2007 Zuoliu Ding.  All Rights Reserved.
SeparatorComboBox:	Implementation class
Created by:			05/15/2004, Zuoliu Ding
Note:				For a Combo box with Separators
****************************************************************************************************************/

using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FruPak.PF.Utils.UserControls
{
    public class SeparatorComboBox : ComboBox
    {
        #region Constructor

        public SeparatorComboBox()
        {
            DrawMode = DrawMode.OwnerDrawVariable;
            _separatorStyle = DashStyle.Solid;
            _separators = new ArrayList();

            _separatorStyle = DashStyle.Solid;
            _separatorColor = Color.Black;
            _separatorMargin = 1;
            _separatorWidth = 1;
            _autoAdjustItemHeight = false;
        }

        #endregion Constructor

        #region Medthods

        public void AddString(string s)
        {
            Items.Add(s);
        }

        public void AddStringWithSeparator(string s)
        {
            Items.Add(s);
            _separators.Add(s);
        }

        public void SetSeparator(int pos)
        {
            _separators.Add(pos);
        }

        #endregion Medthods

        #region Properties

        [Description("Gets or sets the Separator Style"), Category("Separator")]
        public DashStyle SeparatorStyle
        {
            get { return _separatorStyle; }
            set { _separatorStyle = value; }
        }

        [Description("Gets or sets the Separator Color"), Category("Separator")]
        public Color SeparatorColor
        {
            get { return _separatorColor; }
            set { _separatorColor = value; }
        }

        [Description("Gets or sets the Separator Width"), Category("Separator")]
        public int SeparatorWidth
        {
            get { return _separatorWidth; }
            set { _separatorWidth = value; }
        }

        [Description("Gets or sets the Separator Margin"), Category("Separator")]
        public int SeparatorMargin
        {
            get { return _separatorMargin; }
            set { _separatorMargin = value; }
        }

        [Description("Gets or sets Auto Adjust Item Height"), Category("Separator")]
        public bool AutoAdjustItemHeight
        {
            get { return _autoAdjustItemHeight; }
            set { _autoAdjustItemHeight = value; }
        }

        #endregion Properties

        #region Overrides

        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            if (_autoAdjustItemHeight)
                e.ItemHeight += _separatorWidth;

            base.OnMeasureItem(e);
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (-1 == e.Index) return;

            bool sep = false;
            object o;
            for (int i = 0; !sep && i < _separators.Count; i++)
            {
                o = _separators[i];

                if (o is string)
                {
                    if ((string)this.Items[e.Index] == o as string)
                        sep = true;
                }
                else
                {
                    int pos = (int)o;
                    if (pos < 0) pos += Items.Count;

                    if (e.Index == pos) sep = true;
                }
            }

            e.DrawBackground();
            Graphics g = e.Graphics;
            int y = e.Bounds.Location.Y + _separatorWidth - 1;

            if (sep)
            {
                Pen pen = new Pen(_separatorColor, _separatorWidth);
                pen.DashStyle = _separatorStyle;

                g.DrawLine(pen, e.Bounds.Location.X + _separatorMargin, y, e.Bounds.Location.X + e.Bounds.Width - _separatorMargin, y);
                y++;
            }

            Brush br = DrawItemState.Selected == (DrawItemState.Selected & e.State) ? SystemBrushes.HighlightText : new SolidBrush(e.ForeColor);
            g.DrawString((string)Items[e.Index], e.Font, br, e.Bounds.Left, y + 1);
            //			e.DrawFocusRectangle();

            base.OnDrawItem(e);
        }

        #endregion Overrides

        #region Data members

        private ArrayList _separators;
        private DashStyle _separatorStyle;
        private Color _separatorColor;
        private int _separatorWidth;
        private int _separatorMargin;
        private bool _autoAdjustItemHeight;

        #endregion Data members
    }
}