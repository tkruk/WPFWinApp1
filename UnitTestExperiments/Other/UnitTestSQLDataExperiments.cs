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
            int count = 50000;
            var dataExperiments = new SQLDataExperiments();

            // Before running the test we want to clear all data from the table.
            dataExperiments.DeleteAllAccountData();

            watch.Start();
            dataExperiments.InsertMultipleAccounts(count);
            watch.Stop();

            Debug.WriteLine("Measured time: " + watch.Elapsed.TotalMilliseconds + " ms.");

            var rtnVal = dataExperiments.CountRecordsInAccountTable();

            Assert.AreEqual(count, rtnVal);

            dataExperiments.DeleteAllAccountData();
            rtnVal = dataExperiments.CountRecordsInAccountTable();

            Assert.AreEqual(0, rtnVal);
        }
    }
}
