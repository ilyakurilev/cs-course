using System;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			var cat = new Pet("Cat", "cat", 'm', DateTimeOffset.Parse("17.05.2015"));

			Console.WriteLine(cat.Desctripiton);
			cat.UpdateProperties("Dog", 'f');

			Console.WriteLine(cat.Desctripiton);
			cat.UpdateProperties("Mouse", DateTimeOffset.Parse("17.05.2019"));

			Console.WriteLine(cat.Desctripiton);

			cat.WriteProperties();
			cat.WriteProperties(showFullDescription: true);
		}
	}
}
