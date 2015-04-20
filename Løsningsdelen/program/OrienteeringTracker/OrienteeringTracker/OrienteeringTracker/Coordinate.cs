using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace OrienteeringTracker
{
    public class Coordinate
    {
        public Coordinate(float x, float y, DateTime t, float UTME, float UTMN)
        {
            _pixelPoint = new PointF(x, y);
            _UTMPoint = new PointF(UTME, UTMN);
            Time = t;
        }

        private PointF _pixelPoint { get; set; }
        public PointF pixelPoint
        {
            get
            {
                return _pixelPoint;
            }
        }
        public DateTime Time { get; set; }

        private PointF _UTMPoint { get; set; }
        public PointF UTMPoint
        {
            get
            {
                return _UTMPoint;
            }
        }
        
    }
}
