using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using TimeLibrary;

namespace TimeUnitTests
{

    [TestClass]
    public static class InitializeCulture
    {
        [AssemblyInitialize]
        public static void SetEnglishCultureOnAllUnitTest(TestContext context)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
        }
    }

    // =======================================

    [TestClass]
    public class UnitTestsTimeConstructors
    {
        private static byte defaultTime = 00;

        private void AssertTime(Time t, int expectedHour, int expectedMinute, int expectedSecond)
        {
            Assert.AreEqual(expectedHour, t.Hours);
            Assert.AreEqual(expectedMinute, t.Minutes);
            Assert.AreEqual(expectedSecond, t.Seconds);
        }


        #region Constructor tests ================================

        [TestMethod, TestCategory("Constructors")]
        public void TimeConstructor_Default()
        {
            Time t = new Time();

            Assert.AreEqual(defaultTime, t.Hours);
            Assert.AreEqual(defaultTime, t.Minutes);
            Assert.AreEqual(defaultTime, t.Seconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(10, 10, 00, 00)]
        [DataRow(20, 20, 00, 00)]
        public void TimeConstructor_1param(int hour, int expectedHour, int expectedMinute, int expectedSecond)
        {
            Time t = new Time(hour);

            AssertTime(t, expectedHour, expectedMinute, expectedSecond);

        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(10, 10, 10, 10, 00)]
        [DataRow(20, 17, 20, 17, 00)]
        public void TimeConstructor_2params(int hour, int minute, int expectedHour, int expectedMinute, int expectedSecond)
        {
            Time t = new Time(hour, minute);

            AssertTime(t, expectedHour, expectedMinute, expectedSecond);

        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(10, 10, 10, 10, 10, 10)]
        [DataRow(20, 17, 31, 20, 17, 31)]
        public void TimeConstructor_3params(int hour, int minute, int second, int expectedHour, int expectedMinute, int expectedSecond)
        {
            Time t = new Time(hour, minute, second);

            AssertTime(t, expectedHour, expectedMinute, expectedSecond);

        }






        #endregion


    }




}





