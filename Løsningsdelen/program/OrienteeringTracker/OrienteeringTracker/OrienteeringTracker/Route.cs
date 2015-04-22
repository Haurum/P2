using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrienteeringTracker
{
    public class Route
    {
        public Route()
        {
            Coords = new List<Coordinate>();
            Visited = new List<ControlPointTime>();
        }

        public string RunnerName { get; set; }
        public DateTime Date { get; set; }
        public System.Drawing.Color RouteColor { get; set; }


        public List<Coordinate> Coords { get; set; }


        public List<ControlPointTime> Visited { get; set; }
    }
}
