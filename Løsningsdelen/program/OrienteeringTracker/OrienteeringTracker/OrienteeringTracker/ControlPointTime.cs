using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrienteeringTracker
{
    public class ControlPointTime : ControlPoint
    {
        public int Second { get; set; }
        public double Dist { get; set; }

        public void ControlPointChecker(ControlPoint cp, Runner r)
        {
            List<ControlPointTime> distList = new List<ControlPointTime>();
            ControlPointTime cpt = new ControlPointTime();
            double doubleDist = 0;
            foreach (Coordinate coord in r.Coords)
            {
                doubleDist = Helper.CalcSingleLength(coord.pixelPoint.X, coord.pixelPoint.Y, cp.Cord.pixelPoint.X, cp.Cord.pixelPoint.Y);
                if (doubleDist < 25)
                {
                    for (int i = r.Coords.IndexOf(coord); i < r.Coords.Count; i++)
                    {
                        if (Helper.CalcSingleLength(r.Coords[i].pixelPoint.X, r.Coords[i].pixelPoint.Y, cp.Cord.pixelPoint.X, cp.Cord.pixelPoint.Y) > 25)
                        {
                            ControlPointTime thisCpt = distList.OrderBy(distance => distance.Dist).First();
                            this.Cord = thisCpt.Cord;
                            this.Dist = thisCpt.Dist;
                            this.Number = thisCpt.Number;
                            this.Second = thisCpt.Second;
                            this.Radius = thisCpt.Radius;
                            return;
                        }
                        cpt = new ControlPointTime();
                        cpt.Cord = cp.Cord;
                        cpt.Number = cp.Number;
                        cpt.Second = r.Coords.IndexOf(coord);
                        cpt.Dist = Helper.CalcSingleLength(r.Coords[i].pixelPoint.X, r.Coords[i].pixelPoint.Y, cp.Cord.pixelPoint.X, cp.Cord.pixelPoint.Y);
                        cpt.Second = i;
                        distList.Add(cpt);
                    }
                }
            }
            this.Cord = null;
            this.Dist = 0;
            this.Number = 0;
            this.Second = 0;
            this.Radius = 0;
            return;

        }
    }
}
