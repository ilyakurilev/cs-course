using System;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			var helicopter = new Helicopter(100, 4);
			var plane = new Plane(150, 6);

			helicopter.WriteAllProperties();
			Console.WriteLine();
			plane.WriteAllProperties();
		}
	}
}
