using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace OrienteeringTracker.Test
{
    [TestFixture]
    public class HelperTest
    {
        [TestCase]
        public void CalcSingleLength_TwoPoints_Return5()
        {
            double dist = Helper.CalcSingleLength(-2, 1, 1, 5);
            Assert.AreEqual(dist, 5);
        }
   
        [TestCase]
        public void ConvertLatLongtoUTM_LatLong_UTM()
        {
            double easting;
            double northing;
            string zone;
            Helper.ConvertLatLongtoUTM(57.121332, 9.953613, out northing, out easting, out zone);

            Assert.AreEqual(Math.Round(easting, 2), Math.Round(557740.21, 2));
            Assert.AreEqual(Math.Round(northing, 2), Math.Round(6331295.72, 2));
            Assert.AreEqual(zone, "32V");
        }
    }
}
