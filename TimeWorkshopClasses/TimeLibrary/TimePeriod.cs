using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Aliases
using U = TimeLibrary.Utilities;

namespace TimeLibrary
{
    public struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        /// <summary>
        /// This property always returns a value and is a representation of Seconds in Time Period.
        /// </summary>
        public readonly long Seconds { get; }

        #region Constructors
        /// <summary>
        /// TimePeriod Constructor that takes 3 ints and creates element of type 'TimePeriod'.
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public TimePeriod(int hours = 00, int minutes = 00, int seconds = 00)
        {
            byte convertHour = (byte)hours;
            byte convertMinutes = (byte)minutes;
            byte convertSeconds = (byte)seconds;

            U.ExceptionHandler(convertHour, convertMinutes, convertSeconds);

            Seconds = (convertHour * 3600) + (convertMinutes * 60) + convertSeconds;
        }

        /// <summary>
        /// TimePeriod Constructor that takes 2 'Time' elements and base on difference between them creates element of type 'TimePeriod'.
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public TimePeriod(Time t1, Time t2)
        {
            long fullDay = 86400;
            long firstInSeconds = U.Converter(t1);
            long secondInSeconds = U.Converter(t2);

            if (t1 > t2)
                Seconds = fullDay - firstInSeconds + secondInSeconds;
            else if (t2 > t1)
                Seconds = secondInSeconds - firstInSeconds;
            else
                Seconds = 00;
        }

        /// <summary>
        /// TimePeriod Constructor that takes string and creates element of type 'TimePeriod'.
        /// </summary>
        /// <param name="strTime"></param>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public TimePeriod(string strTime)
        {
            string[] timeArray = strTime.Split(":");

            bool uncheckedHour = byte.TryParse(timeArray[0], out byte resultHours);
            bool uncheckedMinutes = byte.TryParse(timeArray[1], out byte resultMinutes);
            bool uncheckedSeconds = byte.TryParse(timeArray[2], out byte resultSeconds);

            U.ExceptionHandler(resultMinutes, resultSeconds);

            if (uncheckedHour && uncheckedMinutes && uncheckedSeconds is true)
            {
                Seconds = (resultHours * 3600) + (resultMinutes * 60) + resultSeconds;
            }
            else
                throw new FormatException("Nie udało się przekonwertować wprowadzonych danych na wartość czasową. " +
                    "Upewnij się, że wprowadzasz dane w formacie 00:00:00.");
        }

        /// <summary>
        /// TimePeriod Constructor that takes long value representing number of seconds and creates element of type 'TimePeriod'.
        /// </summary>
        /// <param name="inSeconds"></param>
        public TimePeriod(long inSeconds)
        {
            Seconds = inSeconds;
        }
        #endregion

        public override string ToString()
        {
            long hours = Seconds / 3600;
            long minutes = (Seconds % 3600) / 60;
            long seconds = Seconds % 60;

            DateTime time = new DateTime(1, 1, 1, (int)hours, (int)minutes, (int)seconds);

            return time.ToString("HH:mm:ss");

            //return time.ToString("hh\\:mm\\:ss");
        }

        #region Implementacja IEquatable<TimePeriod>
        public bool Equals(TimePeriod other)
        {
            return
                (Seconds == other.Seconds);
        }

        public override bool Equals(object? obj)
        {
            return obj is TimePeriod ? Equals((TimePeriod)obj) : false;
        }
        
        public override int GetHashCode() => Seconds.GetHashCode();
        #endregion

        #region Implementacja IComparable<TimePeriod>
        public int CompareTo(TimePeriod other)
        {
            if (this.Equals(other))
                return 0;
            else
                return this.Seconds.CompareTo(other.Seconds);
        }
        #endregion

        //TODO - Dokumentacja XML
        #region Arithmetics
        TimePeriod Plus(TimePeriod t1)
        {
            TimePeriod result = new TimePeriod(this.Seconds + t1.Seconds);
            return result;
        }

        static TimePeriod Plus(TimePeriod t1, TimePeriod t2)
        {
            return t1.Plus(t2);
        }

        TimePeriod Minus(TimePeriod t1)
        {
            if (this.Seconds < t1.Seconds)
            {
                throw new ArgumentException("Odjęcie tego odcinka czasowego spowoduje wynik minusowy, co nie jest dopuszczalne!");
            }
            else
            {
                TimePeriod result = new TimePeriod(this.Seconds - t1.Seconds);
                return result;
            }
        }

        static TimePeriod Minus(TimePeriod t1, TimePeriod t2)
        {
            return t1.Minus(t2);
        }

        #endregion

        //TODO - Dokumentacja XML
        #region Basic Operators
        public static bool operator ==(TimePeriod t1, TimePeriod t2) => t1.Equals(t2);
        public static bool operator !=(TimePeriod t1, TimePeriod t2) => !(t1 == t2);
        public static bool operator <(TimePeriod t1, TimePeriod t2) => t1.CompareTo(t2) < 0;
        public static bool operator >(TimePeriod t1, TimePeriod t2) => t1.CompareTo(t2) > 0;
        public static bool operator <=(TimePeriod t1, TimePeriod t2) => t1.CompareTo(t2) <= 0;
        public static bool operator >=(TimePeriod t1, TimePeriod t2) => t1.CompareTo(t2) >= 0;
        public static TimePeriod operator +(TimePeriod t1, TimePeriod t2) => t1.Plus(t2);
        public static TimePeriod operator -(TimePeriod t1, TimePeriod t2) => t1.Minus(t2);

        #endregion


    




    }
}
