using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using TimeLibrary;

namespace TimePeriodUnitTests
{
    [TestClass]
    public class TimePeriodGenerals
    {
        [TestMethod, TestCategory("General")]
        public void TimePeriodProperties_Type()
        {
            TimePeriod t = new TimePeriod();

            Assert.IsInstanceOfType(t.Seconds, typeof(long));
        }
    }

    // ==========================================================================================

    [TestClass]
    public class TimePeriodConstructors
    {
        private static byte defaultTime = 00;

        private void AssertTimePeriod(TimePeriod t, long expectedSeconds)
        {
            Assert.AreEqual(expectedSeconds, t.Seconds);
        }

        // ==========================================================================================

        #region Constructor tests ================================
        [TestMethod, TestCategory("Constructors")]
        public void TimePeriodConstructor_Default()
        {
            TimePeriod t = new TimePeriod();

            AssertTimePeriod(t, defaultTime);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(10, 36000)]
        public void TimePeriodConstructor_1param(int hour, long expectedSeconds)
        {
            TimePeriod t = new TimePeriod(hour);

            AssertTimePeriod(t, expectedSeconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(10, 10, 36600)]
        public void TimePeriodConstructor_2param(int hour, int minute, long expectedSeconds)
        {
            TimePeriod t = new TimePeriod(hour, minute);

            AssertTimePeriod(t, expectedSeconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(10, 10, 10, 36610)]
        public void TimePeriodConstructor_3param(int hour, int minute, int second, long expectedSeconds)
        {
            TimePeriod t = new TimePeriod(hour, minute, second);

            AssertTimePeriod(t, expectedSeconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("10:10:10", 36610)]
        public void TimePeriodConstructor_String(string stringRepresentation, long expectedSeconds)
        {
            TimePeriod t = new TimePeriod(stringRepresentation);

            AssertTimePeriod(t, expectedSeconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(10, 10, 10, 11, 10, 10, 3600)]
        public void TimePeriodConstructor_Time(int hour1, int minute1, int second1, int hour2, int minute2, int second2, long expectedSeconds)
        {
            Time t1 = new Time(hour1, minute1, second1);
            Time t2 = new Time(hour2, minute2, second2);

            TimePeriod t = new TimePeriod(t1, t2);

            AssertTimePeriod(t, expectedSeconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(3600, 3600)]
        public void TimePeriodConstructor_Long(long seconds, long expectedSeconds)
        {
            TimePeriod t = new TimePeriod(seconds);

            AssertTimePeriod(t, expectedSeconds);
        }
        #endregion
    }

    [TestClass]
    public class TimePeriodStringRepresentation
    {
        #region ToString tests ===================================
        [DataTestMethod, TestCategory("ToString()")]
        [DataRow(10, 10, 10, "10:10:10")]
        [DataRow(23, 17, 25, "23:17:25")]
        [DataRow(7, 12, 9, "07:12:09")]
        [DataRow(30, 30, 40, "30:30:40")]
        [DataRow(129, 30, 50, "129:30:50")]
        public void TimePeriodToString_Complementary(int hour, int minute, int second, string stringConstructorExpected)
        {
            TimePeriod t1 = new TimePeriod(hour, minute, second);
            string tRepresentation = t1.ToString();

            TimePeriod t2 = new TimePeriod(tRepresentation);

            Assert.AreEqual(tRepresentation, stringConstructorExpected);
            Assert.AreEqual(t1, t2);
        }

        //Optional Test that can be used if format h:m:s is acceptable.
        [DataTestMethod, TestCategory("ToString()")]
        [DataRow("10:10:10", "10:10:10")]
        [DataRow("7:10:7", "07:10:07")]
        [DataRow("05:6:20", "05:06:20")]
        public void TimePeriodToString_FromStringFormat(string stringConstructor, string stringConstructorExpected)
        {
            TimePeriod t1 = new TimePeriod(stringConstructor);
            string t1Representation = t1.ToString();

            Assert.AreEqual(t1Representation, stringConstructorExpected);
        }


        #endregion
    }

    [TestClass]
    public class TimePeriodOperators
    {
        #region Operators (Equals, CompareTo) ================================
        [DataTestMethod, TestCategory("Operators")]
        [DataRow(10, 10, 10, 10, 10, 10, true)]
        [DataRow(21, 03, 04, 21, 03, 04, true)]
        [DataRow(30, 30, 20, 30, 30, 20, true)]
        [DataRow(10, 10, 20, 10, 10, 10, false)]
        [DataRow(07, 07, 07, 07, 07, 08, false)]
        [DataRow(50, 30, 40, 50, 30, 30, false)]
        public void TimePeriodOperators_Equal(int hour1, int minute1, int second1, int hour2, int minute2, int second2, bool expectedResult)
        {
            TimePeriod t1 = new TimePeriod(hour1, minute1, second1);
            TimePeriod t2 = new TimePeriod(hour2, minute2, second2);

            Assert.AreEqual(expectedResult, t1 == t2);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(10, 10, 10, 10, 10, 10, false)]
        [DataRow(21, 03, 04, 21, 03, 04, false)]
        [DataRow(30, 30, 20, 30, 30, 20, false)]
        [DataRow(10, 10, 20, 10, 10, 10, true)]
        [DataRow(07, 07, 07, 07, 07, 08, true)]
        [DataRow(50, 30, 40, 50, 30, 30, true)]
        public void TimePeriodOperators_NotEqual(int hour1, int minute1, int second1, int hour2, int minute2, int second2, bool expectedResult)
        {
            TimePeriod t1 = new TimePeriod(hour1, minute1, second1);
            TimePeriod t2 = new TimePeriod(hour2, minute2, second2);

            Assert.AreEqual(expectedResult, t1 != t2);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(10, 10, 10, 11, 10, 10, true)]
        [DataRow(40, 30, 30, 50, 30, 30, true)]
        [DataRow(15, 20, 20, 15, 20, 20, false)]
        [DataRow(30, 30, 30, 30, 30, 30, false)]
        [DataRow(18, 10, 10, 15, 20, 20, false)]
        [DataRow(40, 10, 10, 30, 10, 10, false)]
        public void TimePeriodOperators_Smaller(int hour1, int minute1, int second1, int hour2, int minute2, int second2, bool expectedResult)
        {
            TimePeriod t1 = new TimePeriod(hour1, minute1, second1);
            TimePeriod t2 = new TimePeriod(hour2, minute2, second2);

            Assert.AreEqual(expectedResult, t1 < t2);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(10, 10, 10, 11, 10, 10, false)]
        [DataRow(40, 30, 30, 50, 30, 30, false)]
        [DataRow(15, 20, 20, 15, 20, 20, false)]
        [DataRow(30, 30, 30, 30, 30, 30, false)]
        [DataRow(18, 10, 10, 15, 20, 20, true)]
        [DataRow(40, 10, 10, 30, 10, 10, true)]
        public void TimePeriodOperators_Bigger(int hour1, int minute1, int second1, int hour2, int minute2, int second2, bool expectedResult)
        {
            TimePeriod t1 = new TimePeriod(hour1, minute1, second1);
            TimePeriod t2 = new TimePeriod(hour2, minute2, second2);

            Assert.AreEqual(expectedResult, t1 > t2);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(10, 10, 10, 11, 10, 10, true)]
        [DataRow(40, 30, 30, 50, 30, 30, true)]
        [DataRow(15, 20, 20, 15, 20, 20, true)]
        [DataRow(30, 30, 30, 30, 30, 30, true)]
        [DataRow(18, 10, 10, 15, 20, 20, false)]
        [DataRow(40, 10, 10, 30, 10, 10, false)]
        public void TimePeriodOperators_SmallerOrEqual(int hour1, int minute1, int second1, int hour2, int minute2, int second2, bool expectedResult)
        {
            TimePeriod t1 = new TimePeriod(hour1, minute1, second1);
            TimePeriod t2 = new TimePeriod(hour2, minute2, second2);

            Assert.AreEqual(expectedResult, t1 <= t2);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(10, 10, 10, 11, 10, 10, false)]
        [DataRow(40, 30, 30, 50, 30, 30, false)]
        [DataRow(15, 20, 20, 15, 20, 20, true)]
        [DataRow(30, 30, 30, 30, 30, 30, true)]
        [DataRow(18, 10, 10, 15, 20, 20, true)]
        [DataRow(40, 10, 10, 30, 10, 10, true)]
        public void TimePeriodOperators_BiggerOrEqual(int hour1, int minute1, int second1, int hour2, int minute2, int second2, bool expectedResult)
        {
            TimePeriod t1 = new TimePeriod(hour1, minute1, second1);
            TimePeriod t2 = new TimePeriod(hour2, minute2, second2);

            Assert.AreEqual(expectedResult, t1 >= t2);
        }
        #endregion
    }

    [TestClass]
    public class TimePeriodArithmetics
    {
        #region Arithmetics (Plus, Minus, Multiply + their operators) ======================

        [DataTestMethod, TestCategory("Arithmetics")]
        [DataRow(10, 10, 10, 10, 10, 10, 20, 20, 20)]
        [DataRow(07, 30, 30, 11, 30, 30, 19, 01, 00)]
        [DataRow(30, 10, 10, 20, 10, 10, 50, 20, 20)]
        [DataRow(25, 00, 00, 23, 00, 00, 48, 00, 00)]
        [DataRow(70, 30, 40, 30, 30, 40, 101, 01, 20)]
        [DataRow(10, 30, 40, 60, 30, 50, 71, 01, 30)]
        public void TimePeriodArithmetics_Plus(int hour1, int minute1, int second1, int hour2, int minute2, int second2, int expectedHour, int expectedMinute, int expectedSecond)
        {
            TimePeriod t1 = new TimePeriod(hour1, minute1, second1);
            TimePeriod t2 = new TimePeriod(hour2, minute2, second2);
            TimePeriod expectedTime = new TimePeriod(expectedHour, expectedMinute, expectedSecond);

            //Non-Static Method 'Plus' is the original method, others are using it in their implementations. If test fails there's a huge possibility that problem comes from original method.
            Assert.AreEqual(expectedTime, t1.Plus(t2));
            Assert.AreEqual(expectedTime, TimePeriod.Plus(t1, t2));
            Assert.AreEqual(expectedTime, t1 + t2);
        }

        [DataTestMethod, TestCategory("Arithmetics")]
        [DataRow(10, 10, 10, 10, 10, 10, 00, 00, 00)]
        [DataRow(30, 10, 10, 20, 10, 10, 10, 00, 00)]
        [DataRow(11, 40, 30, 11, 30, 40, 00, 09, 50)]
        [DataRow(20, 00, 00, 11, 30, 30, 08, 29, 30)]
        [DataRow(129, 50, 50, 100, 40, 40, 29, 10, 10)]
        [DataRow(100, 00, 00, 50, 00, 00, 50, 00, 00)]
        public void TimePeriodArithmetics_Minus(int hour1, int minute1, int second1, int hour2, int minute2, int second2, int expectedHour, int expectedMinute, int expectedSecond)
        {
            TimePeriod t1 = new TimePeriod(hour1, minute1, second1);
            TimePeriod t2 = new TimePeriod(hour2, minute2, second2);
            TimePeriod expectedTime = new TimePeriod(expectedHour, expectedMinute, expectedSecond);

            //Non-Static Method 'Minus' is the original method, others are using it in their implementations. If test fails there's a huge possibility that problem comes from original method.
            Assert.AreEqual(expectedTime, t1.Minus(t2));
            Assert.AreEqual(expectedTime, TimePeriod.Minus(t1, t2));
            Assert.AreEqual(expectedTime, t1 - t2);
        }

        [DataTestMethod, TestCategory("Arithmetics")]
        [DataRow(50, 00, 00, 2, 100, 00, 00)]
        [DataRow(10, 10, 10, 2, 20, 20, 20)]
        [DataRow(05, 30, 20, 3, 16, 31, 00)]
        [DataRow(07, 10, 10, 20, 143, 23, 20)]
        [DataRow(07, 06, 08, 3, 21, 18, 24)]
        [DataRow(50, 06, 06, 1, 50, 06, 06)]
        public void TimePeriodArithmetics_Multiply(int hour1, int minute1, int second1, int multiplicator, int expectedHour, int expectedMinute, int expectedSecond)
        {
            TimePeriod t1 = new TimePeriod(hour1, minute1, second1);
            TimePeriod expectedTime = new TimePeriod(expectedHour, expectedMinute, expectedSecond);

            //Non-Static Method 'Multiply' is the original method, others are using it in their implementations. If test fails there's a huge possibility that problem comes from original method.
            Assert.AreEqual(expectedTime, t1.Multiply(multiplicator));
            Assert.AreEqual(expectedTime, TimePeriod.Multiply(t1, multiplicator));
            Assert.AreEqual(expectedTime, t1 * multiplicator);
        }
        #endregion
    }

    [TestClass]
    public class TimePeriodExceptions
    {
        #region Exceptions tests
        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1, 1)]
        [DataRow(1, -1, 1)]
        [DataRow(1, 1, -1)]
        [DataRow(-1, -1, 1)]
        [DataRow(-1, 1, -1)]
        [DataRow(1, -1, -1)]
        [DataRow(-1, -1, -1)]
        [DataRow(1, 60, 1)]
        [DataRow(1, 1, 60)]
        [DataRow(1, 60, 60)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TimePeriodConstructors_ArgumentOutOfRangeException(int hour, int minute, int second)
        {
            TimePeriod t = new TimePeriod(hour, minute, second);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(0.01, 0.1, 1)]
        [DataRow(0.1, 0.01, 1)]
        [DataRow(0.1, 0.1, 0.01)]
        [DataRow(0.5, 0.7, 0.5)]
        [ExpectedException(typeof(ArgumentException))]
        public void TimePeriodConstructors_ArgumentException(int hour, int minute, int second)
        {
            TimePeriod t = new TimePeriod(hour, minute, second);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("-1:1:1")]
        [DataRow("1:-1:1")]
        [DataRow("1:1:-1")]
        [DataRow("-1:-1:1")]
        [DataRow("-1:1:-1")]
        [DataRow("1:-1:-1")]
        [DataRow("-1:-1:-1")]
        [DataRow("1:60:1")]
        [DataRow("1:1:60")]
        [DataRow("1:60:60")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TimePeriodConstructors_String_ArgumentOutOfRangeException(string stringRepresentation)
        {
            TimePeriod t = new TimePeriod(stringRepresentation);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("0.01:0.1:1")]
        [DataRow("0.1:0.01:1")]
        [DataRow("0.1:0.1:0.01")]
        [DataRow("0.5:0.7:0.5")]
        [DataRow("Kasia:10:20")]
        [DataRow("Janusz:Marian:30")]
        [ExpectedException(typeof(ArgumentException))]
        public void TimePeriodConstructors_String_ArgumentException(string stringRepresentation)
        {
            TimePeriod t = new TimePeriod(stringRepresentation);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("Mariola10:10")]
        [DataRow("10,10,10")]
        [DataRow("JanuszTomaszKasia")]
        [DataRow(":10:10:10:")]
        [DataRow("10;10:10")]
        [DataRow("20:07::15")]
        [ExpectedException(typeof(FormatException))]
        public void TimePeriodConstructors_String_FormatException(string stringRepresentation)
        {
            TimePeriod t = new TimePeriod(stringRepresentation);
        }

        [DataTestMethod, TestCategory("Arithmetics")]
        [DataRow(10, 10, 10, 11, 10, 10)]
        [DataRow(30, 30, 30, 50, 00, 00)]
        [DataRow(100, 00, 00, 200, 00, 00)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TimePeriodArithmetics_Minus_ArgumentOutOfRangeException(int hour1, int minute1, int second1, int hour2, int minute2, int second2)
        {
            TimePeriod t1 = new TimePeriod(hour1, minute1, second1);
            TimePeriod t2 = new TimePeriod(hour2, minute2, second2);

            TimePeriod result = t1.Minus(t2);
        }

        #endregion
    }
}

