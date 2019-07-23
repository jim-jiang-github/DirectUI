using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUIQuadrangle
    {
        private PointF plt;
        private PointF prt;
        private PointF plb;
        private PointF prb;
        private bool convexQuadrilateral = true;
        public DUIQuadrangle(PointF plt, PointF prt, PointF plb, PointF prb)
        {
            this.plt = plt;
            this.plt = prt;
            this.plt = plb;
            this.plt = prb;
            float t1 = (plb.X - plt.X) * (prt.Y - plt.Y) - (plb.Y - plt.Y) * (prt.X - plt.X);
            float t2 = (plt.X - prt.X) * (prb.Y - prt.Y) - (plt.Y - prt.Y) * (prb.X - prt.X);
            float t3 = (prt.X - prb.X) * (plb.Y - prb.Y) - (prt.Y - prb.Y) * (plb.X - prb.X);
            float t4 = (prb.X - plb.X) * (plt.Y - plb.Y) - (prb.Y - plb.Y) * (plt.X - plb.X);
            if (t1 * t2 * t3 * t4 < 0)
            {
                convexQuadrilateral = true;
            }
        }
    }
}
