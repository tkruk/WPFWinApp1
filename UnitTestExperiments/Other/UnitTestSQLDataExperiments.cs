using System;
using System.Diagnostics;
using Experiments.Other;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestExperiments.Other
{
    [TestClass]
    public class UnitTestSQLDataExperiments
    {
        [TestMethod]
        public void TestSimpleInsert()
        {
            var dataExperiments = new SQLDataExperiments();

            var rtnVal = dataExperiments.ExecuteSimpleInsertAccount();

            Assert.AreEqual(1, rtnVal);

            rtnVal = dataExperiments.ExecuteDeleteAccount();

            Assert.AreEqual(1, rtnVal);
        }

        [TestMethod]
        public void TestMultipleInsertsTraceWithStopWatch()
        {
            Stopwatch watch = new Stopwatch();
            int count = 100000;
            var dataExperiments = new SQLDataExperiments();

            watch.Start();
            dataExperiments.InsertMultipleAccounts(count);
            watch.Stop();

            Console.WriteLine("Measured time: " + watch.Elapsed.TotalMilliseconds + " ms.");

            var rtnVal = dataExperiments.CountRecordsInAccountTable();

            Assert.AreEqual(count, rtnVal);

            dataExperiments.DeleteAllAccountData();
            rtnVal = dataExperiments.CountRecordsInAccountTable();

            Assert.AreEqual(0, rtnVal);
        }
    }
}
