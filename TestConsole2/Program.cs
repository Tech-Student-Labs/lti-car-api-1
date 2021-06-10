using System;

namespace TestConsole2
{
    class Program
    {
        static void Main(string[] args)
        {
            var period = new DateTimeOffset(new DateTime());
            Console.WriteLine(period.Date.Hour);
        }
    }
}
