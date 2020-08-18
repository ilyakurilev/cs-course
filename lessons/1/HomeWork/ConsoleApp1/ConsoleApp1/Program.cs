using System;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Thread.Sleep(5000);
            Console.WriteLine("Hello, {0}!", name);
            Thread.Sleep(5000);
            Console.WriteLine("Bye, {0}!", name);
            Console.ReadKey();
        }
    }
}
