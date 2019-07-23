using DirectUI.Common;
using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DirectUI.Share
{
    public class LayoutEngine
    {
        public static void DoLayout(DirectUI.Common.DUILayoutEventArgs e)
        {
            if (e.AffectedControl == null || !e.AffectedControl.CanLayout) { return; }
            e.AffectedControl.DUIControls.OfType<DUIControl>().ToList().ForEach(d =>
            {
                LayoutEngine.DoLayout(new Common.DUILayoutEventArgs(d, "DoLayout"));
            });
            e.AffectedControl.DoLayout(e);
            //e.AffectedControl.Invalidate();
        }
        public static void DoLayoutDock(DirectUI.Common.DUILayoutEventArgs e, SizeF oldClientSize, bool invalidate = true)
        {
            if (e.AffectedControl == null || !e.AffectedControl.CanLayout) { return; }
            float l = 0;
            float t = 0;
            float r = e.AffectedControl.ClientSize.Width;
            float b = e.AffectedControl.ClientSize.Height;
            bool needInvalidate = false;
            e.AffectedControl.DUIControls.OfType<DUIControl>().ToList().ForEach(d =>
            {
                RectangleF lastBounds = d.Bounds;
                switch (d.Dock)
                {
                    case DockStyle.Bottom:
                        d.SetBounds(l, b - d.Height, r - l, d.Height, d.CenterX, d.CenterY, d.Rotate, d.SkewX, d.SkewY, d.ScaleX, d.ScaleY, DUIBoundsSpecified.All, false);
                        if (!d.Visible) { break; }
                        needInvalidate |= d.Bounds == lastBounds;
                        b -= d.Height;
                        break;
                    case DockStyle.Fill:
                        d.SetBounds(l, t, r - l, b - t, d.CenterX, d.CenterY, d.Rotate, d.SkewX, d.SkewY, d.ScaleX, d.ScaleY, DUIBoundsSpecified.All, false);
                        needInvalidate |= d.Bounds == lastBounds;
                        break;
                    case DockStyle.Left:
                        d.SetBounds(l, t, d.Width, b - t, d.CenterX, d.CenterY, d.Rotate, d.SkewX, d.SkewY, d.ScaleX, d.ScaleY, DUIBoundsSpecified.All, false);
                        if (!d.Visible) { break; }
                        needInvalidate |= d.Bounds == lastBounds;
                        l += d.Width;
                        break;
                    case DockStyle.None:
                        if (((d.Anchor & AnchorStyles.Left) == AnchorStyles.Left) && ((d.Anchor & AnchorStyles.Right) == AnchorStyles.Right))
                        {
                            d.SetBounds(d.Location.X, d.Location.Y, d.Right + e.AffectedControl.ClientSize.Width - oldClientSize.Width - d.Location.X, d.Height, d.CenterX, d.CenterY, d.Rotate, d.SkewX, d.SkewY, d.ScaleX, d.ScaleY, DUIBoundsSpecified.All, false);
                        }
                        if (!((d.Anchor & AnchorStyles.Left) == AnchorStyles.Left) && !((d.Anchor & AnchorStyles.Right) == AnchorStyles.Right))
                        {
                            d.SetBounds(d.Location.X + e.AffectedControl.ClientSize.Width / 2 - oldClientSize.Width / 2, d.Location.Y, d.Width, d.Height, d.CenterX, d.CenterY, d.Rotate, d.SkewX, d.SkewY, d.ScaleX, d.ScaleY, DUIBoundsSpecified.All, false);
                        }
                        if (!((d.Anchor & AnchorStyles.Left) == AnchorStyles.Left) && ((d.Anchor & AnchorStyles.Right) == AnchorStyles.Right))
                        {
                            d.SetBounds(d.Location.X + e.AffectedControl.ClientSize.Width - oldClientSize.Width, d.Location.Y, d.Width, d.Height, d.CenterX, d.CenterY, d.Rotate, d.SkewX, d.SkewY, d.ScaleX, d.ScaleY, DUIBoundsSpecified.All, false);
                        }
                        if (((d.Anchor & AnchorStyles.Left) == AnchorStyles.Left) && !((d.Anchor & AnchorStyles.Right) == AnchorStyles.Right))
                        {

                        }
                        if (((d.Anchor & AnchorStyles.Top) == AnchorStyles.Top) && ((d.Anchor & AnchorStyles.Bottom) == AnchorStyles.Bottom))
                        {
                            d.SetBounds(d.Location.X, d.Location.Y, d.Width, d.Bottom + e.AffectedControl.ClientSize.Height - oldClientSize.Height - d.Location.Y, d.CenterX, d.CenterY, d.Rotate, d.SkewX, d.SkewY, d.ScaleX, d.ScaleY, DUIBoundsSpecified.All, false);
                        }
                        if (!((d.Anchor & AnchorStyles.Top) == AnchorStyles.Top) && !((d.Anchor & AnchorStyles.Bottom) == AnchorStyles.Bottom))
                        {
                            d.SetBounds(d.Location.X, d.Location.Y + e.AffectedControl.ClientSize.Height / 2 - oldClientSize.Height / 2, d.Width, d.Height, d.CenterX, d.CenterY, d.Rotate, d.SkewX, d.SkewY, d.ScaleX, d.ScaleY, DUIBoundsSpecified.All, false);
                        }
                        if (!((d.Anchor & AnchorStyles.Top) == AnchorStyles.Top) && ((d.Anchor & AnchorStyles.Bottom) == AnchorStyles.Bottom))
                        {
                            d.SetBounds(d.Location.X, d.Location.Y + e.AffectedControl.ClientSize.Height - oldClientSize.Height, d.Width, d.Height, d.CenterX, d.CenterY, d.Rotate, d.SkewX, d.SkewY, d.ScaleX, d.ScaleY, DUIBoundsSpecified.All, false);
                        }
                        if (((d.Anchor & AnchorStyles.Top) == AnchorStyles.Top) && !((d.Anchor & AnchorStyles.Bottom) == AnchorStyles.Bottom))
                        {

                        }
                        needInvalidate |= d.Bounds == lastBounds;
                        break;
                    case DockStyle.Right:
                        d.SetBounds(r - d.Width, t, d.Width, b - t, d.CenterX, d.CenterY, d.Rotate, d.SkewX, d.SkewY, d.ScaleX, d.ScaleY, DUIBoundsSpecified.All, false);
                        if (!d.Visible) { break; }
                        needInvalidate |= d.Bounds == lastBounds;
                        r -= d.Width;
                        break;
                    case DockStyle.Top:
                        d.SetBounds(l, t, r - l, d.Height, d.CenterX, d.CenterY, d.Rotate, d.SkewX, d.SkewY, d.ScaleX, d.ScaleY, DUIBoundsSpecified.All, false);
                        if (!d.Visible) { break; }
                        needInvalidate |= d.Bounds == lastBounds;
                        t += d.Height;
                        break;
                }
            });
            if (needInvalidate && invalidate)
            {
                e.AffectedControl.Invalidate();
            }
        }
        public static void DoLayoutDock(DirectUI.Common.DUILayoutEventArgs e, bool invalidate = true)
        {
            if (e.AffectedControl == null || !e.AffectedControl.CanLayout) { return; }
            float l = 0;
            float t = 0;
            float r = e.AffectedControl.ClientSize.Width;
            float b = e.AffectedControl.ClientSize.Height;
            bool needInvalidate = false;
            e.AffectedControl.DUIControls.OfType<DUIControl>().ToList().ForEach(d =>
            {
                RectangleF lastBounds = d.Bounds;
                switch (d.Dock)
                {
                    case DockStyle.Bottom:
                        d.SetBounds(l, b - d.Height, r - l, d.Height, d.CenterX, d.CenterY, d.Rotate, d.SkewX, d.SkewY, d.ScaleX, d.ScaleY, DUIBoundsSpecified.All, false);
                        if (!d.Visible) { break; }
                        needInvalidate |= d.Bounds == lastBounds;
                        b -= d.Height;
                        break;
                    case DockStyle.Fill:
                        d.SetBounds(l, t, r - l, b - t, d.CenterX, d.CenterY, d.Rotate, d.SkewX, d.SkewY, d.ScaleX, d.ScaleY, DUIBoundsSpecified.All, false);
                        needInvalidate |= d.Bounds == lastBounds;
                        break;
                    case DockStyle.Left:
                        d.SetBounds(l, t, d.Width, b - t, d.CenterX, d.CenterY, d.Rotate, d.SkewX, d.SkewY, d.ScaleX, d.ScaleY, DUIBoundsSpecified.All, false);
                        if (!d.Visible) { break; }
                        needInvalidate |= d.Bounds == lastBounds;
                        l += d.Width;
                        break;
                    case DockStyle.None:
                        break;
                    case DockStyle.Right:
                        d.SetBounds(r - d.Width, t, d.Width, b - t, d.CenterX, d.CenterY, d.Rotate, d.SkewX, d.SkewY, d.ScaleX, d.ScaleY, DUIBoundsSpecified.All, false);
                        if (!d.Visible) { break; }
                        needInvalidate |= d.Bounds == lastBounds;
                        r -= d.Width;
                        break;
                    case DockStyle.Top:
                        d.SetBounds(l, t, r - l, d.Height, d.CenterX, d.CenterY, d.Rotate, d.SkewX, d.SkewY, d.ScaleX, d.ScaleY, DUIBoundsSpecified.All, false);
                        if (!d.Visible) { break; }
                        needInvalidate |= d.Bounds == lastBounds;
                        t += d.Height;
                        break;
                }
            });
            if (needInvalidate && invalidate)
            {
                e.AffectedControl.Invalidate();
            }
        }
    }
}
