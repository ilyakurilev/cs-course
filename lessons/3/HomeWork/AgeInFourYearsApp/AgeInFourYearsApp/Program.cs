using System;

namespace AgeInFourYearsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfPerson = 3;

            string[] names = new string[numberOfPerson];
            int[] ages = new int[numberOfPerson];

            for (int i = 0; i < numberOfPerson; i++)
            {
                Console.Write("Enter the name: ");
                names[i] = Console.ReadLine();
                Console.Write($"Enter {names[i]}'s age: ");
                ages[i] = System.Int32.Parse(Console.ReadLine());
            }

            Console.WriteLine("\n*************************************\n");

            for (int i = 0; i < numberOfPerson; i++)
            {
                Console.WriteLine($"Name: {names[i]}, age in 4 years: {ages[i] + 4}");
            }

            Console.WriteLine("\nPress any key...");
            Console.ReadKey(true);
        }
    }
}
