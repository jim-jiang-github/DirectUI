using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUITriangle
    {
        private Point p1;
        private Point p2;
        private Point p3;

        public Point P1
        {
            get { return p1; }
            set { p1 = value; }
        }
        public Point P2
        {
            get { return p2; }
            set { p2 = value; }
        }
        public Point P3
        {
            get { return p3; }
            set { p3 = value; }
        }
        public void Offset(int x, int y)
        {
            p1.X += x;
            p1.Y += y;
            p2.X += x;
            p2.Y += y;
            p2.X += x;
            p2.Y += y;
        }
        public DUITriangle(Point p1, Point p2, Point p3)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
        }
        public DUITriangle(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            this.p1 = new Point(x1, y1);
            this.p2 = new Point(x2, y2);
            this.p3 = new Point(x3, y3);
        }
        public DUITriangle()
        {
        }
    }
}
