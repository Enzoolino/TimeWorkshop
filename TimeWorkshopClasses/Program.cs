using TimeLibrary;


namespace TimeWorkshop
{
    class Program
    {
        public static void Main(string[] args)
        {
            Time Janusz = new Time(20, 10, 10);
            Time Marian = new Time(21, 10, 10);
            Time Kazimierz = new Time(22, 10, 10);
            Time Kazimierzg = new Time(10, 10, 10);
            Time Marek = new Time("10:10:10");

            TimePeriod Marysia = new TimePeriod(Janusz, Marian);
            TimePeriod Kasia = new TimePeriod(Janusz, Kazimierz);
            TimePeriod Marysiag = new TimePeriod(Janusz, Kazimierzg);
            TimePeriod Bigie = new TimePeriod(3, 10, 10);
            TimePeriod Baba = new TimePeriod("03:10:10");
            
            Console.WriteLine(Bigie == Baba);
        }
    }
}

//1. Poprawić formatowanie tekstu dla TimePeriod
//2. Dopisać dokumentacje kodu w XML !!
//3. Zrobić testy jednostkowe !!



