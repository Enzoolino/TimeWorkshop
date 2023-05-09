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
        #region Properties
        /// <summary>
        /// This property always returns a value and is a representation of Seconds in Time Period.
        /// </summary>
        public readonly long Seconds { get; }
        #endregion

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
            U.TimePeriodExceptionHandler(hours, minutes, seconds);

            /*
            byte convertHour = (byte)hours;
            byte convertMinutes = (byte)minutes;
            byte convertSeconds = (byte)seconds;
            */

            Seconds = (hours * 3600) + (minutes * 60) + seconds;
        }

        /// <summary>
        /// TimePeriod Constructor that takes two 'Time' elements and based on difference between t2 and t1, creates element of type 'TimePeriod'.
        /// </summary>
        /// <remarks>If t2 is smaller than t1, it counts as tomorrow 'Time'.</remarks>
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
        /// <remarks>Format must be "00:00:00".</remarks>
        /// <param name="strTime"></param>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public TimePeriod(string strTime)
        {
            string[] timeArray = strTime.Split(":");

            if (timeArray.Length == 3)
            {
                for (int i = 0; i < timeArray.Length; i++)
                {
                    bool checkTime = int.TryParse(timeArray[i], out int result);

                    if (!checkTime)
                    {
                        throw new ArgumentException("Converting input into 'TimePeriod' value failed." +
                            "You can only input Natural numbers.");
                    }
                }

                U.TimePeriodExceptionHandler(int.Parse(timeArray[0]), int.Parse(timeArray[1]), int.Parse(timeArray[2]));

                int resultHours = int.Parse(timeArray[0]);
                int resultMinutes = int.Parse(timeArray[1]);
                int resultSeconds = int.Parse(timeArray[2]);

               Seconds = (resultHours * 3600) + (resultMinutes * 60) + resultSeconds;
                
            }
            else
                throw new FormatException("Converting input into 'TimePeriod' value failed." +
                    "Make sure value you are trying to convert is in format 00:00:00.");
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
            TimeSpan timeSpan = TimeSpan.FromSeconds(Seconds);
            string formattedTimePeriod = string.Format("{0:D2}:{1:D2}:{2:D2}", (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds);
            return formattedTimePeriod;
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

        #region Arithmetics
        /// <summary>
        /// Adds two elements of type 'TimePeriod' together.
        /// </summary>
        /// <param name="t1"></param>
        /// <returns>Element of type 'TimePeriod'</returns>
        public TimePeriod Plus(TimePeriod t1)
        {
            TimePeriod result = new TimePeriod(this.Seconds + t1.Seconds);
            return result;
        }

        /// <summary>
        /// Adds two elements of type 'TimePeriod' together.
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns>Element of type 'TimePeriod'</returns>
        public static TimePeriod Plus(TimePeriod t1, TimePeriod t2)
        {
            return t1.Plus(t2);
        }

        /// <summary>
        /// Subtracts element of type 'TimePeriod' from another.
        /// </summary>
        /// <remarks>Result can't be negative.</remarks>
        /// <param name="t1"></param>
        /// <returns>Element of type 'TimePeriod'</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public TimePeriod Minus(TimePeriod t1)
        {
            if (this.Seconds < t1.Seconds)
            {
                throw new ArgumentOutOfRangeException("Odjęcie tego odcinka czasowego spowoduje wynik minusowy, co nie jest dopuszczalne!");
            }
            else
            {
                TimePeriod result = new TimePeriod(this.Seconds - t1.Seconds);
                return result;
            }
        }

        /// <summary>
        /// Subtracts element of type 'TimePeriod' from another.
        /// </summary>
        /// <remarks>Result can't be negative.</remarks>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns>Element of type 'TimePeriod'</returns>
        public static TimePeriod Minus(TimePeriod t1, TimePeriod t2)
        {
            return t1.Minus(t2);
        }

        /// <summary>
        /// Multiplies element of type 'TimePeriod' by given number.
        /// </summary>
        /// <param name="multiplicator"></param>
        /// <returns>Element of type 'TimePeriod'</returns>
        public TimePeriod Multiply(int multiplicator)
        {
            TimePeriod result = new TimePeriod(this.Seconds * multiplicator);
            return result;
        }

        /// <summary>
        /// Multiplies element of type 'TimePeriod' by given number.
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="multiplicator"></param>
        /// <returns>Element of type 'TimePeriod'</returns>
        public static TimePeriod Multiply(TimePeriod t1, int multiplicator)
        {
            return t1.Multiply(multiplicator);
        }

        #endregion

        #region Operators
        /// <summary>
        /// Checks if 'TimePeriod' value is equal to another.
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns>Bool value</returns>
        public static bool operator ==(TimePeriod t1, TimePeriod t2) => t1.Equals(t2);

        /// <summary>
        /// Checks if 'TimePeriod' value is not equal to another.
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns>Bool value</returns>
        public static bool operator !=(TimePeriod t1, TimePeriod t2) => !(t1 == t2);

        /// <summary>
        /// Check if 'TimePeriod' value is smaller than another.
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns>Bool value</returns>
        public static bool operator <(TimePeriod t1, TimePeriod t2) => t1.CompareTo(t2) < 0;

        /// <summary>
        /// Checks if 'TimePeriod' value is bigger than another.
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public static bool operator >(TimePeriod t1, TimePeriod t2) => t1.CompareTo(t2) > 0;

        /// <summary>
        /// Checks if 'TimePeriod' value is smaller or equal to another.
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns>Bool value</returns>
        public static bool operator <=(TimePeriod t1, TimePeriod t2) => t1.CompareTo(t2) <= 0;

        /// <summary>
        /// Checks if 'TimePeriod' value is bigger or equal to another.
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns>Bool value</returns>
        public static bool operator >=(TimePeriod t1, TimePeriod t2) => t1.CompareTo(t2) >= 0;

        /// <summary>
        /// Adds two elements of type 'TimePeriod' together.
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns>Element of type 'TimePeriod'</returns>
        public static TimePeriod operator +(TimePeriod t1, TimePeriod t2) => t1.Plus(t2);

        /// <summary>
        /// Subtracts element of type 'TimePeriod' from another.
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns>Element of type 'TimePeriod'</returns>
        public static TimePeriod operator -(TimePeriod t1, TimePeriod t2) => t1.Minus(t2);

        /// <summary>
        /// Multiplies element of type 'TimePeriod' by given number.
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="multiplicator"></param>
        /// <returns>Element of type 'TimePeriod'</returns>
        public static TimePeriod operator *(TimePeriod t1, int multiplicator) => t1.Multiply(multiplicator);
        #endregion
    }
}
