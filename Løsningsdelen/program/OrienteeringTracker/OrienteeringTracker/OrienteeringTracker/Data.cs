using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrienteeringTracker
{
    class Data
    {
        public Data(MainForm mf)
        {
            mainForm = mf;
        }
        MainForm mainForm;

        public List<Runner> Runners = new List<Runner>();
        public List<ControlPoint> ControlPoints = new List<ControlPoint>();
        List<Leg> Legs = new List<Leg>();
        Leg MainLeg = new Leg();

        public void Load()
        {
             
        }

        private void LoadRunners()
        {

        }

        private void LoadControlPoints()
        {

        }

        private void LoadLegs()
        {

        }
    }
}