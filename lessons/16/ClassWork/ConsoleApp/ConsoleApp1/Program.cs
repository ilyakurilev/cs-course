using System;
using Calculator.Figure;
using Calculator.Operation;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var circle = new Circle(5);

            Console.WriteLine($"Length: {circle.Calculate(CircleOperation.Length)}");
            Console.WriteLine($"Square: {circle.Calculate(CircleOperation.Square)}");
            Console.WriteLine($"Diameter: {circle.Calculate(CircleOperation.Diameter)}");
        }
    }
}
