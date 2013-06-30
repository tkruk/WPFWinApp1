using System;
using Experiments.DynamicObjectSample;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestExperiments.Experiments
{
    [TestClass]
    public class ExposedObjectSimpleTests
    {
        [TestMethod]
        public void TestAccessingListPrivateMethod()
        {
            var objInternals = new AccessObjectInternals();

            var rtnVal = objInternals.Experiment1();

            Assert.AreEqual(true, rtnVal);
        }

        [TestMethod]
        public void TestCallingPrivateStaticMethod()
        {
            var objInternals = new AccessObjectInternals();

            var rtnVal = objInternals.Experiment2();

            Assert.AreEqual(false, rtnVal);
        }
    }
}
