using System;

namespace ConsoleApp1
{
	class Helicopter : FlyingMachine, IHelicopter
	{
		public byte BladesCount { get; private set; }

		public Helicopter(int maxHeight, byte bladesCount) :
			base(maxHeight)
		{
			BladesCount = bladesCount;
			Console.WriteLine("It's a helicopter, welcome aboard!");
		}

		public override void WriteAllProperties()
		{
			Console.WriteLine("Properties of Helicopter:");
			Console.WriteLine($"BladesCount:\t\t{BladesCount}");
			Console.WriteLine($"CurrentHeight:\t\t{CurrentHeight}");
			Console.WriteLine($"MaxHeight:\t\t{MaxHeight}");
		}

		public override void WriteAllProperties2()
		{
			Console.WriteLine("Properties of Helicopter:");
			Console.WriteLine($"BladesCount:\t\t{BladesCount}");
			base.WriteAllProperties2();
		}
	}
}
