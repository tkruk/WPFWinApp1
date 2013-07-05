using System;
using Experiments.Other;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestExperiments.Other
{
    [TestClass]
    public class UnitTestUnixTime
    {
        [TestMethod]
        public void TestNow()
        {
            var rtnValue = UnixTime.Now;
            Console.WriteLine("Current time: " + rtnValue.ToString());

            Assert.IsNotNull(rtnValue);
        }

        [TestMethod]
        public void TestConvertFromUnixToUTCDateTime()
        {
            var rtnValue = UnixTime.ToDateTime(UnixTime.Now);
            Console.WriteLine("Converted value from unix to date time: " + rtnValue.ToString());

            Assert.IsNotNull(rtnValue);
        }

        [TestMethod]
        public void TestConvertFromUTCDateTimeToUnix()
        {
            var rtnValue = UnixTime.FromDateTime(DateTime.UtcNow);
            Console.WriteLine("Converted value from date time to unix: " + rtnValue.ToString());

            Assert.IsNotNull(rtnValue);
        }

        [TestMethod]
        public void TestFromUTCDateTimeToUnixException()
        {
            var message = string.Empty;

            try
            {
                var localDateTime = new DateTime(10000, DateTimeKind.Local);
                UnixTime.FromDateTime(localDateTime);
            }
            catch (ArgumentException ex)
            {
                message = ex.Message;
            }

            Assert.AreEqual("Input date time must be in UTC!", message);
        }
    }
}
