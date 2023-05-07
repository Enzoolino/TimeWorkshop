using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeLibrary;

namespace TimeProgram
{
    class Program
    {
        public static void Main(string[] args)
        {
            /*
            //Time - classic int constructor + string Representation.

            Console.WriteLine("What would you like to input ?\n");
            Console.WriteLine("1 = Time");
            Console.WriteLine("2 = TimePeriod");
            var valueType = Console.ReadLine();

            switch(valueType)
            {
            //Time
                case "1":
                    Console.Clear();
                    Time result = new Time();

                    Console.WriteLine("Please enter the value of Hours in Time.");
                    bool hourCheck = int.TryParse(Console.ReadLine(), out int hour);
                    Console.Clear();

                    Console.WriteLine("Please enter the value of Minutes in Time.");
                    bool minuteCheck = int.TryParse(Console.ReadLine(), out int minute);
                    Console.Clear();

                    Console.WriteLine("Please enter the value of Seconds in Time.");
                    bool secondCheck = int.TryParse(Console.ReadLine(), out int second);
                    Console.Clear();

                    if (hourCheck || minuteCheck || secondCheck)
                    {
                        result = new Time(hour, minute, second);
                    }
                    else
                        throw new ArgumentException("Your input is not a natural number!");


                    Console.WriteLine($"You can see your time here: {result}\n");
                    Console.WriteLine("What operations would you like to perform ?");
                    Console.WriteLine("1 = Check Equality (==)");
                    Console.WriteLine("2 = Check Inequality (!=)");
                    Console.WriteLine("3 = Compare to another Time (<, >, <=, >=)");
                    Console.WriteLine("4 = Add TimePeriod to Time (+)");
                    Console.WriteLine("5 = Subtract TimePeriod value from Time (-)");
                    Console.WriteLine("6 = Multiply Time by given number (*)");

                    var operationType = Console.ReadLine();

                    switch(operationType)
                    {
                        case "1":

                        case "2":


                    }


                    //TimePeriod
                    case "2":


            }
        }
        
        */
        }
    }
}
