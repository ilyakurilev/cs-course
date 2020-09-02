using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the length of array: ");
            Console.Write("> ");
            var success = int.TryParse(Console.ReadLine(), out var length);
            if (success)
            {
                var array = new string[length];
                for (var i = 0; i < length; i++)
                {
                    Console.Write("> " );
                    array[i] = Console.ReadLine();
                }

                Console.WriteLine("\n***************************\n");
                foreach (var a in array)
                {
                    Console.WriteLine(a);
                }
            }

            
        }
    }
}
