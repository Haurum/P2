using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Drawing;

namespace OrienteeringTracker.Test
{
    [TestFixture]
    class ControlPointTest
    {
        [TestCase]
        public void ReadControlPoints_LineAndNr_PopulatedCP()
        {
            ControlPoint cp = new ControlPoint();
            string line = "539446.2;6249967;200;1056";
            int nr = 1;
            cp.ReadControlPoint(line, nr);

            
            PointF point = new PointF(200,1056);
            Assert.AreEqual(cp.Cord.pixelPoint, point);
        }
    }
}
