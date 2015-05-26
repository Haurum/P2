using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections;

namespace OrienteeringTracker
{
    public class Coordinate
    {
        public Coordinate(float x, float y, DateTime t, double UTME, double UTMN)
        {
            _pixelPoint = new PointF(x, y);
            UTMEast = UTME;
            UTMNorth = UTMN;
            Time = t;
        }

		// Indeholder tid, og koordinater som pixler og UTM
        private PointF _pixelPoint { get; set; }
        public PointF pixelPoint
        {
            get
            {
                return _pixelPoint;
            }
        }
        public DateTime Time { get; set; }

        public double UTMEast { get; set; }
        public double UTMNorth { get; set; }
    }
}
