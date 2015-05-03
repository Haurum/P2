using System;
using NUnit.Framework;
using OrienteeringTracker;
using System.Collections.Generic;

namespace OrienteeringTracker.Test
{
    [TestFixture]
    public class HelperTest
    {
        [Test]
        public void Test_GetPosAndDiff()
        {
            List<RunnerData> rd = new List<RunnerData>();
            List<Runner> r = new List<Runner>();
            Assert.AreEqual(Helper.GetPosAndDiff(rd, r), null);
        }
    }
}
