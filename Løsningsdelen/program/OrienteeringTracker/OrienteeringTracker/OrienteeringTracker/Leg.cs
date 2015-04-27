using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrienteeringTracker
{
    class Leg
    {
        public List<RunnerData> Runners = new List<RunnerData>();
        public string Name { get; set; }
    }
}
