using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DirectUI
{
    public sealed class DUICursors
    {
        private static Cursor rotateCursor = null;
        static DUICursors()
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DirectUI.Cursors.rotateCursor.cur"))
            using (Bitmap cursor = (Bitmap)Image.FromStream(stream))
            {
                rotateCursor = new Cursor(cursor.GetHicon());
            }
        }
        public static Cursor Rotate
        {
            get
            {
                return rotateCursor;
            }
        }
        public static Cursor Skew
        {
            get
            {
                return Cursors.Help;
            }
        }
    }
}
