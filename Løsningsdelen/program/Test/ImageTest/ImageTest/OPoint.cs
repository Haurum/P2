using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ImageTest
{
    class OPoint
    {
        public OPoint(PointF Point, DateTime Time) : this(Point.X, Point.Y, Time) { }
        public OPoint(float pixX, float pixY, DateTime Time)
        {
            point = new PointF(pixX, pixY);
            time = Time;
        }
        public PointF point { get; set; }
        public DateTime time { get; set; }
    }
}
