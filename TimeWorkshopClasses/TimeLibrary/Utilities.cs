﻿using System;
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
        public static void ExceptionHandler(byte h, byte m, byte s)
        {
            if ((h < 0 || h > 23) || (m < 0 || m > 59) || (s < 0 || s > 59))
                throw new ArgumentOutOfRangeException("Podane wartości czasowe są niepoprawne!");
        }

        /// <summary>
        /// Method that checks if values are not out of range.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="s"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void ExceptionHandler(byte m, byte s)
        {
            if ((m < 0 || m > 59) || (s < 0 || s > 59))
                throw new ArgumentOutOfRangeException("Podane wartości czasowe są niepoprawne!");
        }

        public static void ExceptionHandler(Time t1)
        {
            byte h = t1.Hours;
            byte m = t1.Minutes;
            byte s = t1.Seconds;

            if ((h < 0 || h > 23) || (m < 0 || m > 59) || (s < 0 || s > 59))
                throw new ArgumentOutOfRangeException("Podane wartości czasowe są niepoprawne!");

        }

        public static long Converter(byte hours, byte minutes, byte seconds)
        {
            return (hours * 3600) + (minutes * 60) + seconds;
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
