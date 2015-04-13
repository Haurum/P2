using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrienteeringTracker
{
    class Route
    {
        public string RunnerName { get; set; }
        public DateTime Date { get; set; }

        public List<Coordinate> Coords { get; set; }

        public List<ControlPoint> ToVisit { get; set; }

        public List<ControlPoint> Visited { get; set; }
    }
}
