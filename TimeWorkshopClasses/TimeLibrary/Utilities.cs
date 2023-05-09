using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLibrary
{
    public class Utilities
    {
        /// <summary>
        /// Method that checks if values are not out of range.
        /// </summary>
        /// <param name="h"></param>
        /// <param name="m"></param>
        /// <param name="s"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void TimeExceptionHandler(int h, int m, int s)
        {
            if ((h < 0 || h > 23) || (m < 0 || m > 59) || (s < 0 || s > 59))
                throw new ArgumentOutOfRangeException("Entered Time values are incorrect!");
                
        }

        /// <summary>
        /// Method that checks if values are not out of range.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="s"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void TimePeriodExceptionHandler(int h, int m, int s)
        {
            if (h < 0 || (m < 0 || m > 59) || (s < 0 || s > 59))
                throw new ArgumentOutOfRangeException("Entered Time values are incorrect!");
        }

        /// <summary>
        /// Method that converts Time to Seconds.
        /// </summary>
        /// <param name="t1"></param>
        /// <returns>Number of Seconds</returns>
        public static long Converter(Time t1)
        {
            return (t1.Hours * 3600) + (t1.Minutes * 60) + t1.Seconds;
        }

    }
}
