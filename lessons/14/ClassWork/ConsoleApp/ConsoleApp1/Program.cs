using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var errors = new ErrorList("Warning"))
            {
                errors.Add("fist warning");
                errors.Add("second warning");
                errors.Add("third warning");

                foreach (var error in errors)
                {
                    Console.WriteLine($"{errors.Category}: {error}");
                }
            }
        }
    }
}
