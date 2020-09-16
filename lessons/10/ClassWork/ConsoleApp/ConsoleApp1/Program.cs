using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Pet pet1 = new Pet();
            pet1.Kind = "Cat";
            pet1.Age = 10;
            pet1.Sex = 'm';
            pet1.Name = "Barsik";
            Console.WriteLine(pet1.Description);

            var pet2 = new Pet()
            {
                Name = "Dog",
                Kind = "dog",
                Age = 5,
                Sex = 'f'
            };
            Console.WriteLine(pet2.Description);

        }
    }
}
