using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
//Aliases
using U = TimeLibrary.Utilities;


namespace TimeLibrary
{
    public struct Time : IEquatable<Time>, IComparable<Time>
    {
        /// <summary>
        /// This property always returns the value and is a representation of Hours in Time.
        /// </summary>
        public readonly byte Hours { get;}
        public readonly byte Minutes { get;}
        public readonly byte Seconds { get;}

        #region Constructors
        /*
        public Time(byte hours = 00, byte minutes = 00, byte seconds = 00)
        {
            U.ExceptionHandler(hours, minutes, seconds);
            
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }
        */

        public Time(int hours = 00, int minutes = 00, int seconds = 00)
        {
            byte convertHour = (byte)hours;
            byte convertMinutes = (byte)minutes;
            byte convertSeconds = (byte)seconds;

            U.ExceptionHandler(convertHour, convertMinutes, convertSeconds);

            Hours = convertHour;
            Minutes = convertMinutes;
            Seconds = convertSeconds;
        }

        public Time(string strTime)
        {
            string[] timeArray = strTime.Split(":");
            
            bool uncheckedHour = byte.TryParse(timeArray[0], out byte resultHours);
            bool uncheckedMinutes = byte.TryParse(timeArray[1], out byte resultMinutes);
            bool uncheckedSeconds = byte.TryParse(timeArray[2], out byte resultSeconds);

            U.ExceptionHandler(resultHours, resultMinutes, resultSeconds);

            if (uncheckedHour && uncheckedMinutes && uncheckedSeconds is true)
            {
                Hours = resultHours;
                Minutes = resultMinutes;
                Seconds = resultSeconds;
            }
            else
                throw new FormatException("Nie udało się przekonwertować wprowadzonych danych na wartość czasową. " +
                    "Upewnij się, że wprowadzasz dane w formacie 00:00:00.");
            
        }

        
        #endregion

        public override string ToString()
        {
            DateTime time = new DateTime(1, 1, 1, Hours, Minutes, Seconds);
            return time.ToString("HH:mm:ss");

            //return $"{Hours}:{Minutes}:{Seconds}"; 
        }

        #region Implementacja IEquatable<Time>

        public bool Equals(Time other)
        {
            /*
            if (ReferenceEquals(this, other))
                return true;
            */
            
            return
                (Hours == other.Hours &&
                 Minutes == other.Minutes &&
                 Seconds == other.Seconds);
        }

        public override bool Equals(object? obj)
        {
            return obj is Time ? Equals((Time)obj) : false;
        }

        public override int GetHashCode()
        {
            int aConvert = (int)Hours;
            int bConvert = (int)Minutes;
            int cConvert = (int)Seconds;

            return (aConvert, bConvert, cConvert).GetHashCode();
        }
        #endregion

        #region Implementacja IComparable<Time>
        public int CompareTo(Time other)
        {
            if (this.Equals(other)) return 0;

            if (this.Hours != other.Hours)
                return this.Hours.CompareTo(other.Hours);

            if (this.Minutes != other.Minutes)
                return this.Minutes.CompareTo(other.Minutes);

            return this.Seconds.CompareTo(other.Seconds);
        }
        #endregion

        #region Arithmetics
        Time Plus(TimePeriod period)
        {
            TimeSpan t = TimeSpan.FromSeconds(period.Seconds);

            int totalSeconds = this.Seconds + t.Seconds;
            int totalMinutes = this.Minutes + t.Minutes + (totalSeconds / 60);
            int totalHours = this.Hours + t.Hours + (totalMinutes / 60);

            Time result = new Time(totalHours % 24, totalMinutes % 60, totalSeconds % 60);
            return result;
        }

        static Time Plus(Time t1, TimePeriod period)
        {
            return t1.Plus(period);
        }

       Time Minus(TimePeriod period)
        {
            TimeSpan t = TimeSpan.FromSeconds(period.Seconds);

            int totalSeconds = this.Seconds - t.Seconds;
            int totalMinutes = this.Minutes - t.Minutes;
            int totalHours = this.Hours - t.Hours;

            if (totalSeconds < 0)
            {
                totalSeconds += 60;
                totalMinutes--;
            }

            if (totalMinutes < 0)
            {
                totalMinutes += 60;
                totalHours++;
            }

            totalHours = (totalHours % 24 + 24) % 24;

            byte parsedSeconds = (byte)(totalSeconds);
            byte parsedMinutes = (byte)(totalMinutes);
            byte parsedHours = (byte)(totalHours);

            Time result = new Time(parsedHours, parsedMinutes, parsedSeconds);
            return result;

        }

        static Time Minus(Time t1, TimePeriod period)
        {
            return t1.Minus(period);
        }

        #endregion

        #region Operators
        public static bool operator ==(Time t1, Time t2) => t1.Equals(t2);
        public static bool operator !=(Time t1, Time t2) => !(t1 == t2);
        public static bool operator <(Time t1, Time t2) => t1.CompareTo(t2) < 0;
        public static bool operator >(Time t1, Time t2) => t1.CompareTo(t2) > 0;
        public static bool operator <=(Time t1, Time t2) => t1.CompareTo(t2) <= 0;
        public static bool operator >=(Time t1, Time t2) => t1.CompareTo(t2) >= 0;
        public static Time operator +(Time t1, TimePeriod period) => t1.Plus(period);
        public static Time operator -(Time t1, TimePeriod period) => t1.Minus(period);

        #endregion

    }
}
