using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLibrary
{
    public class Utilities
    {
        public static void ExceptionHandler(byte h, byte m, byte s)
        {
            if ((h < 0 || h > 23) || (m < 0 || m > 59) || (s < 0 || s > 59))
                throw new ArgumentOutOfRangeException("Podane wartości czasowe są niepoprawne!");
        }

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

        public static long Converter(Time t1)
        {
            return (t1.Hours * 3600) + (t1.Minutes * 60) + t1.Seconds;
        }

    }
}
