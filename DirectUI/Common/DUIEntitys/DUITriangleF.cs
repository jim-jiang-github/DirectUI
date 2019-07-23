using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUITriangleF
    {
        private PointF p1;
        private PointF p2;
        private PointF p3;

        public PointF P1
        {
            get { return p1; }
            set { p1 = value; }
        }
        public PointF P2
        {
            get { return p2; }
            set { p2 = value; }
        }
        public PointF P3
        {
            get { return p3; }
            set { p3 = value; }
        }
        public void Offset(float x, float y)
        {
            p1.X += x;
            p1.Y += y;
            p2.X += x;
            p2.Y += y;
            p3.X += x;
            p3.Y += y;
        }
        public DUITriangleF(PointF p1, PointF p2, PointF p3)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
        }
        public DUITriangleF(float x1, float y1, float x2, float y2, float x3, float y3)
        {
            this.p1 = new PointF(x1, y1);
            this.p2 = new PointF(x2, y2);
            this.p3 = new PointF(x3, y3);
        }
        public DUITriangleF()
        {
        }
    }
}
