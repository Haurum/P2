using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrienteeringTracker
{
    class PlayControl
    {

        public PlayControl()
        {
            Initialize();
        }

        public int StartingPoint { get; set; }
        public int Tempo { get; set; }

        public List<Route> TheRoute { get; set; }

        private void Initialize()
        {
            Tempo = 200;
            StartingPoint = 0;
        }
    }
}
