using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FruPak.PF.WorkOrder
{
    class Marquee_Label : Label
    {
        private int CurrentPosition { get; set; }
        private Timer Timer { get; set; }

        public Marquee_Label()
        {
            UseCompatibleTextRendering = true;
            Timer = new Timer();
            Timer.Interval = 25;
            Timer.Tick += new EventHandler(Timer_Tick);
            Timer.Start();
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            //scrolls right to left
            if ((CurrentPosition * -1) > Width)
                CurrentPosition = +Width;
            else
                CurrentPosition -= 2;

            //scrolls left to right
            //if (CurrentPosition > Width)
            //    CurrentPosition = -Width;
            //else
            //    CurrentPosition += 2;

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TranslateTransform((float)CurrentPosition, 0);
            base.OnPaint(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Timer != null)
                    Timer.Dispose();
            }
            Timer = null;
        }
    }
}
