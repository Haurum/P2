using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace OrienteeringTracker
{
    class Coordinate
    {
        public Coordinate(float x, float y, DateTime t)
        {
            _p = new PointF(x, y);
            Time = t;
        }

        private PointF _p { get; set; }
        public PointF p
        {
            get
            {
                return _p;
            }
        }
        public DateTime Time { get; set; }
        
    }
}
