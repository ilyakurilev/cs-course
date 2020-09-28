using System;

namespace ConsoleApp1
{
	class Plane : FlyingMachine, IPlane
	{
		public byte EnginesCount { get; private set; }

		public Plane(int maxHeight, byte enginesCount) : 
			base(maxHeight)
		{
			EnginesCount = enginesCount;
			Console.WriteLine("It's a plane, welcome aboard!");
		}

		public override void WriteAllProperties()
		{
			Console.WriteLine("Properties of Plane:");
			Console.WriteLine($"EnginesCount:\t\t{EnginesCount}");
			Console.WriteLine($"CurrentHeight:\t\t{CurrentHeight}");
			Console.WriteLine($"MaxHeight:\t\t{MaxHeight}");
		}

		public override void WriteAllProperties2()
		{
			Console.WriteLine("Properties of Plane:");
			Console.WriteLine($"EnginesCount:\t\t{EnginesCount}");
			base.WriteAllProperties2();
		}
	}
}
