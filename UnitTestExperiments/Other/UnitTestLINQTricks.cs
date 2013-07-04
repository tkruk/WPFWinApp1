using System;
using System.Linq;
using Experiments.Other;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestExperiments.Other
{
    [TestClass]
    public class UnitTestLINQTricks
    {
        [TestMethod]
        public void TestArrayInitMethod()
        {
            var tricks = new PlayingWithLINQ();
            var rtnValue = tricks.ArrayInit(10);

            Assert.IsNotNull(rtnValue);
        }

        [TestMethod]
        public void TestArrayConcat()
        {
            var tricks = new PlayingWithLINQ();
            var rtnval = tricks.LoopMultiArraysInOneLoop(10);

            Assert.AreEqual(30, rtnval);
        }

        [TestMethod]
        public void TestArrayUnion()
        {
            var tricks = new PlayingWithLINQ();
            var rtnval = tricks.LoopMultiArraysInOneLoopOnlyUniqueValues(10);

            Assert.AreEqual(21, rtnval);
        }

        [TestMethod]
        public void TestRandomSequence()
        {
            var tricks = new PlayingWithLINQ();
            var rtnVal = tricks.GenerateRandomSequence(10);

            Assert.AreEqual(10, rtnVal.Distinct().Count());
        }

        [TestMethod]
        public void TestGenerteString()
        {
            var tricks = new PlayingWithLINQ();
            var rtnVal = tricks.GenerateString(10);

            Assert.AreEqual(10, rtnVal.Length);
        }

        [TestMethod]
        public void TestExtensionMethods()
        {
            var rtnVal = 1.intTo(10);

            rtnVal.DisplayAll();

            Assert.AreEqual(10, rtnVal.Count());
        }

        [TestMethod]
        public void TestExtensionMethodsPartTwo()
        {
            var rtnVal = 1.intTo(30).Where(i => i % 2 == 0);
            rtnVal.DisplayAll();

            Assert.AreEqual(15, rtnVal.Count());
        }

        [TestMethod]
        public void TestExtensionMehodsWithFibonacciSequence()
        {
            var rtnVal = ExtensionMethods.FibonacciSequence.Take(10);
            rtnVal.DisplayAll();

            Assert.AreEqual(10, rtnVal.Count());
            Assert.AreEqual(1, rtnVal.ToArray()[1]);
            Assert.AreEqual(1, rtnVal.ToArray()[2]);

        }
    }
}