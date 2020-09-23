using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var persons = new Person[3];

            for (var i = 0; i < persons.Length; i++)
            {
                try
                {
                    Console.Write($"Enter name {i}: ");
                    var name = Console.ReadLine();
                    Console.Write($"Enter age {i}: ");
                    var age = int.Parse(Console.ReadLine());
                    persons[i] = new Person()
                    {
                        Name = name,
                        Age = age
                    };
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;
                    Console.WriteLine("Try again!");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;
                    Console.WriteLine("Try again!");
                }
            }


            foreach (var person in persons)
            {
                Console.WriteLine(person.Information);
            }

            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}
