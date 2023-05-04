using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class TimeGenerals
    {
        [TestMethod, TestCategory("General")]
        public void TimeProperties_Type()
        {
            Time t = new Time();

            Assert.IsInstanceOfType(t.Hours, typeof(byte));
            Assert.IsInstanceOfType(t.Minutes, typeof(byte));
            Assert.IsInstanceOfType(t.Seconds, typeof(byte));
        }
    }

    [TestClass]
    public class TimeConstructors
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
        [DataRow(5, 5, 00, 00)]
        public void TimeConstructor_1param(int hour, int expectedHour, int expectedMinute, int expectedSecond)
        {
            Time t = new Time(hour);

            AssertTime(t, expectedHour, expectedMinute, expectedSecond);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(10, 10, 10, 10, 00)]
        [DataRow(20, 17, 20, 17, 00)]
        [DataRow(5, 6, 5, 6, 00)]
        public void TimeConstructor_2params(int hour, int minute, int expectedHour, int expectedMinute, int expectedSecond)
        {
            Time t = new Time(hour, minute);

            AssertTime(t, expectedHour, expectedMinute, expectedSecond);

        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(10, 10, 10, 10, 10, 10)]
        [DataRow(20, 17, 31, 20, 17, 31)]
        [DataRow(5, 6, 18, 5, 6, 18)]
        public void TimeConstructor_3params(int hour, int minute, int second, int expectedHour, int expectedMinute, int expectedSecond)
        {
            Time t = new Time(hour, minute, second);

            AssertTime(t, expectedHour, expectedMinute, expectedSecond);

        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("10:10:10", 10, 10, 10)]
        [DataRow("09:07:05", 09, 07, 05)]
        [DataRow("9:7:5", 09, 07, 05)]
        public void TimeConstructor_String(string stringRepresentation, int expectedHour, int expectedMinute, int expectedSecond)
        {
            Time t = new Time(stringRepresentation);

            AssertTime(t, expectedHour, expectedMinute, expectedSecond);
        }
        #endregion



        #region ToString tests ===================================
        [DataTestMethod, TestCategory("ToString()")]
        [DataRow(10, 10, 10, "10:10:10")]
        [DataRow(23, 17, 25, "23:17:25")]
        [DataRow(7, 12, 9, "07:12:09")]
        public void TimeToString_Complementary(int hour, int minute, int second, string stringConstructorExpected)
        {
            Time t1 = new Time(hour, minute, second);
            string tRepresentation = t1.ToString();

            Time t2 = new Time(tRepresentation);

            Assert.AreEqual(tRepresentation, stringConstructorExpected);
            Assert.AreEqual(t1, t2);
        }

        //Optional Test that can be used if format h:m:s is acceptable.
        [DataTestMethod, TestCategory("ToString()")]
        [DataRow("10:10:10", "10:10:10")]
        [DataRow("7:6:5", "07:06:05")]
        [DataRow("10:7:3", "10:07:03")]
        public void TimeToString_FromStringFormat(string stringConstructor, string stringConstructorExpected)
        {
            Time t1 = new Time(stringConstructor);
            string t1Representation = t1.ToString();
           
            Assert.AreEqual(t1Representation, stringConstructorExpected);
        }
        #endregion

    }

    //TODO - Testy związane z TimePeriod
    [TestClass]
    public class TimePeriodGenerals
    {

    }

    [TestClass]
    public class TimePeriodConstructors
    {

    }




}





