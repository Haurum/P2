﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrienteeringTracker
{
    class RunnerData
    {

        public RunnerData()
        {
            speed = Helper.CalcSpeedMinsPrKm(distance, time);
        }

        public string name { get; set; }
        public int pos { get; set; }
        public int time { get; set; }
        public double diff { get; set; }
        public double distance { get; set; }
        public double speed { get; set; }
    }
}
