using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Experiments.Other;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestExperiments.Other
{
    [TestClass]
    public class UnitTestSampleClass
    {
        [TestMethod]
        public void TestingStaticFields()
        {
            var sampleClass = new SampleClass();

            Assert.AreEqual(0, SampleClass.X);
            Assert.AreEqual(3, SampleClass.Y);
        }
    }
}
