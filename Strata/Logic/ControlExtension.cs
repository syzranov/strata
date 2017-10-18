﻿using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Strata
{
    public static class ControlExtension
    {
        // TKey is control to drag, TValue is a flag used while dragging
        private static readonly Dictionary<Control, bool> draggables =
            new Dictionary<Control, bool>();

        private static Size mouseOffset;

        /// <summary>
        ///     Enabling/disabling dragging for control
        /// </summary>
        public static void Draggable(this Control control, bool Enable)
        {
            if (Enable)
            {
                // enable drag feature
                if (draggables.ContainsKey(control))
                {
                    // return if control is already draggable
                    return;
                }
                // 'false' - initial state is 'not dragging'
                draggables.Add(control, false);

                // assign required event handlersnnn
                control.MouseDown += control_MouseDown;
                control.MouseUp += control_MouseUp;
                control.MouseMove += control_MouseMove;
            }
            else
            {
                // disable drag feature
                if (!draggables.ContainsKey(control))
                {
                    // return if control is not draggable
                    return;
                }
                // remove event handlers
                control.MouseDown -= control_MouseDown;
                control.MouseUp -= control_MouseUp;
                control.MouseMove -= control_MouseMove;
                draggables.Remove(control);
            }
        }

        private static void control_MouseDown(object sender, MouseEventArgs e)
        {
            mouseOffset = new Size(e.Location);
            // turning on dragging
            draggables[(Control) sender] = true;
        }

        private static void control_MouseUp(object sender, MouseEventArgs e)
        {
            // turning off dragging
            draggables[(Control) sender] = false;

            Form form = ((Control) sender).FindForm();
            if (form != null)
            {
                form.Refresh();
            }
        }

        private static void control_MouseMove(object sender, MouseEventArgs e)
        {
            // only if dragging is turned on
            if (draggables[(Control) sender])
            {
                // calculations of control's new position
                Point newLocationOffset = e.Location - mouseOffset;
                ((Control) sender).Left += newLocationOffset.X;
                ((Control) sender).Top += newLocationOffset.Y;
            }
        }
    }
}