using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrienteeringTracker
{
    public class RunnerData
    {
        public bool reached { get; set; }
        public string name { get; set; }
        public int pos { get; set; }
        public TimeSpan time { get; set; }
        public TimeSpan diff { get; set; }
        public double distance { get; set; }
        public double speed { get; set; }
    }
}
