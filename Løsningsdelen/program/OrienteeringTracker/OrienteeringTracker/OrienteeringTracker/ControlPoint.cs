﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrienteeringTracker
{
    public class ControlPoint
    {
        public int Radius { get; set; }
        public int Number { get; set; }
        public Coordinate Cord { get; set; }

        public void ReadControlPoint(string line, int nr)
        {
            string[] coordinatesString = line.Split(';');
            this.Cord = new Coordinate(float.Parse(coordinatesString[2], System.Globalization.CultureInfo.InvariantCulture), float.Parse(coordinatesString[3], System.Globalization.CultureInfo.InvariantCulture), DateTime.Now, float.Parse(coordinatesString[0], System.Globalization.CultureInfo.InvariantCulture), float.Parse(coordinatesString[1], System.Globalization.CultureInfo.InvariantCulture));
            this.Radius = 10; 
            this.Number = nr;
	    }
    }
}
