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

    // ==========================================================================================

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

        // ==========================================================================================

        #region Constructor tests ================================

        [TestMethod, TestCategory("Constructors")]
        public void TimeConstructor_Default()
        {
            Time t = new Time();

            AssertTime(t, defaultTime, defaultTime, defaultTime);
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
    }

    [TestClass]
    public class TimeStringRepresentation
    {
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

    [TestClass]
    public class TimeOperators
    {
        #region Operators (Equals, CompareTo) ================================

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(10, 10, 10, 10, 10, 10, true)]
        [DataRow(21, 03, 04, 21, 03, 04, true)]
        [DataRow(19, 30, 30, 20, 30, 30, false)]
        [DataRow(07, 07, 07, 07, 08, 07, false)]
        public void TimeOperators_Equal(int hour1, int minute1, int second1, int hour2, int minute2, int second2, bool expectedResult)
        {
            Time t1 = new Time(hour1, minute1, second1);
            Time t2 = new Time(hour2, minute2, second2);

            Assert.AreEqual(expectedResult, t1 == t2);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(10, 10, 10, 10, 10, 10, false)]
        [DataRow(21, 03, 04, 21, 03, 04, false)]
        [DataRow(19, 30, 30, 20, 30, 30, true)]
        [DataRow(07, 07, 07, 07, 08, 07, true)]
        public void TimeOperators_NotEqual(int hour1, int minute1, int second1, int hour2, int minute2, int second2, bool expectedResult)
        {
            Time t1 = new Time(hour1, minute1, second1);
            Time t2 = new Time(hour2, minute2, second2);

            Assert.AreEqual(expectedResult, t1 != t2);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(09, 09, 09, 10, 10, 10, true)]
        [DataRow(10, 10, 10, 11, 10, 10, true)]
        [DataRow(05, 05, 05, 05, 05, 05, false)]
        [DataRow(18, 30, 30, 15, 30, 25, false)]
        public void TimeOperators_Smaller(int hour1, int minute1, int second1, int hour2, int minute2, int second2, bool expectedResult)
        {
            Time t1 = new Time(hour1, minute1, second1);
            Time t2 = new Time(hour2, minute2, second2);

            Assert.AreEqual(expectedResult, t1 < t2);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(09, 09, 09, 10, 10, 10, false)]
        [DataRow(10, 10, 10, 11, 10, 10, false)]
        [DataRow(05, 05, 05, 05, 05, 05, false)]
        [DataRow(18, 30, 30, 15, 30, 25, true)]
        public void TimeOperators_Bigger(int hour1, int minute1, int second1, int hour2, int minute2, int second2, bool expectedResult)
        {
            Time t1 = new Time(hour1, minute1, second1);
            Time t2 = new Time(hour2, minute2, second2);

            Assert.AreEqual(expectedResult, t1 > t2);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(09, 09, 09, 10, 10, 10, true)]
        [DataRow(10, 10, 10, 11, 10, 10, true)]
        [DataRow(05, 05, 05, 05, 05, 05, true)]
        [DataRow(18, 30, 30, 15, 30, 25, false)]
        public void TimeOperators_SmallerOrEqual(int hour1, int minute1, int second1, int hour2, int minute2, int second2, bool expectedResult)
        {
            Time t1 = new Time(hour1, minute1, second1);
            Time t2 = new Time(hour2, minute2, second2);

            Assert.AreEqual(expectedResult, t1 <= t2);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(09, 09, 09, 10, 10, 10, false)]
        [DataRow(10, 10, 10, 11, 10, 10, false)]
        [DataRow(05, 05, 05, 05, 05, 05, true)]
        [DataRow(18, 30, 30, 15, 30, 25, true)]
        public void TimeOperators_BiggerOrEqual(int hour1, int minute1, int second1, int hour2, int minute2, int second2, bool expectedResult)
        {
            Time t1 = new Time(hour1, minute1, second1);
            Time t2 = new Time(hour2, minute2, second2);

            Assert.AreEqual(expectedResult, t1 >= t2);
        }
        #endregion
    }

    [TestClass]
    public class TimeArithmetics
    {
        #region Arithmetics (Plus, Minus, Multiply + their operators) ======================

        [DataTestMethod, TestCategory("Arithemtics")]
        [DataRow(10, 10, 10, 10, 10, 10, 20, 20, 20)]
        [DataRow(07, 30, 30, 11, 30, 30, 19, 01, 00)]
        [DataRow(11, 40, 30, 11, 30, 40, 23, 11, 10)]
        [DataRow(20, 10, 10, 06, 10, 10, 02, 20, 20)] //Next Day
        [DataRow(10, 30, 40, 30, 30, 40, 17, 01, 20)] //Next Day
        [DataRow(10, 30, 40, 60, 30, 40, 23, 01, 20)] //2 days forward
        public void TimeArithmetics_Plus(int hour1, int minute1, int second1, int hour2, int minute2, int second2, int expectedHour, int expectedMinute, int expectedSecond)
        {
            Time t1 = new Time(hour1, minute1, second1);
            TimePeriod t2 = new TimePeriod(hour2, minute2, second2);
            Time expectedTime = new Time(expectedHour, expectedMinute, expectedSecond);

            //Non-Static Method 'Plus' is the original method, others are using it in their implementations. If test fails there's a huge possibility that problem comes from original method.
            Assert.AreEqual(expectedTime, t1.Plus(t2));
            Assert.AreEqual(expectedTime, Time.Plus(t1, t2));
            Assert.AreEqual(expectedTime, t1 + t2);
        }

        [DataTestMethod, TestCategory("Arithmetics")]
        [DataRow(10, 10, 10, 10, 10, 10, 00, 00, 00)]
        [DataRow(20, 00, 00, 11, 30, 30, 08, 29, 30)]
        [DataRow(11, 40, 30, 11, 30, 40, 00, 09, 50)]
        [DataRow(07, 30, 30, 11, 30, 30, 20, 00, 00)] //Previous day
        [DataRow(14, 30, 40, 30, 30, 40, 08, 00, 00)] //Previous day
        [DataRow(14, 30, 40, 60, 30, 40, 02, 00, 00)] //2 days backward
        public void TimeArithmetics_Minus(int hour1, int minute1, int second1, int hour2, int minute2, int second2, int expectedHour, int expectedMinute, int expectedSecond)
        {
            Time t1 = new Time(hour1, minute1, second1);
            TimePeriod t2 = new TimePeriod(hour2, minute2, second2);
            Time expectedTime = new Time(expectedHour, expectedMinute, expectedSecond);

            //Non-Static Method 'Minus' is the original method, others are using it in their implementations. If test fails there's a huge possibility that problem comes from original method.
            Assert.AreEqual(expectedTime, t1.Minus(t2));
            Assert.AreEqual(expectedTime, Time.Minus(t1, t2));
            Assert.AreEqual(expectedTime, t1 - t2);
        }

        [DataTestMethod, TestCategory("Arithmetics")]
        [DataRow(10, 10, 10, 2, 20, 20, 20)]
        [DataRow(20, 00, 00, 2, 16, 00, 00)]
        [DataRow(05, 30, 20, 3, 16, 31, 00)]
        [DataRow(07, 07, 07, 3, 21, 21, 21)]
        [DataRow(01, 30, 30, 6, 09, 03, 00)]
        [DataRow(02, 01, 01, 10, 20, 10, 10)]
        public void TimeArithmetics_Multiply(int hour1, int minute1, int second1, int multiplicator, int expectedHour, int expectedMinute, int expectedSecond)
        {
            Time t1 = new Time(hour1, minute1, second1);
            Time expectedTime = new Time(expectedHour, expectedMinute, expectedSecond);

            //Non-Static Method 'Multiply' is the original method, others are using it in their implementations. If test fails there's a huge possibility that problem comes from original method.
            Assert.AreEqual(expectedTime, t1.Multiply(multiplicator));
            Assert.AreEqual(expectedTime, Time.Multiply(t1, multiplicator));
            Assert.AreEqual(expectedTime, t1 * multiplicator);
        }
        #endregion
    }
}












