using System;
using System.Linq;
using Experiments.Other;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestExperiments.Other
{
    [TestClass]
    public class UnitTestGenerics
    {
        [TestMethod]
        public void TestGenericList()
        {
            GenericList<int> list = new GenericList<int>();

            for (int x = 0; x < 10; x++)
            {
                list.AddHead(x);
            }

            foreach (int i in list)
            {
                Console.WriteLine(i + " ");
            }

            Assert.AreEqual(10, list.Count);
        }
    }
}
