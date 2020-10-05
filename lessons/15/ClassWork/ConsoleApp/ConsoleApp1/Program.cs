using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var circle = new Circle(5);

            Console.WriteLine($"Length: {circle.Calculate(CircleLength)}");
            Console.WriteLine($"Square: {circle.Calculate(CircleArea)}");
        }

        static double CircleLength(double radius) =>
             2 * Math.PI * radius;


        static double CircleArea(double radius) =>
            Math.PI * Math.Pow(radius, 2);
        
    }
}
