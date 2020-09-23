using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var persons = new Person[3];

            var count = 0;
            while (count < persons.Length)
            {
                try
                {
                    var person = new Person();
                    Console.Write($"Enter name {count}: ");
                    person.Name = Console.ReadLine();
                    Console.Write($"Enter age {count}: ");
                    person.Age = int.Parse(Console.ReadLine());
                    persons[count++] = person;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Try again!");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
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
