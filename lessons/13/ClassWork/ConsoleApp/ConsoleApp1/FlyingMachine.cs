using System;

namespace ConsoleApp1
{
	abstract class FlyingMachine : IFlyingObject, IPropertiesWriter
	{
		public int MaxHeight { get; private set; }
		public int CurrentHeight { get; private set; }

		public FlyingMachine(int maxHeight)
		{
			MaxHeight = maxHeight;
		}

		public void TakeUpper(int delta)
		{
			if (delta <= 0)
			{
				throw new ArgumentOutOfRangeException("delta must be greater than 0");
			}

			if (CurrentHeight + delta > MaxHeight)
			{
				CurrentHeight = MaxHeight;
			}
			else
			{
				CurrentHeight += delta;
			}
		}

		public void TakeLower(int delta)
		{
			if (delta <= 0)
			{
				throw new ArgumentOutOfRangeException("delta must be greater than 0");
			}

			if (CurrentHeight - delta > 0)
			{
				CurrentHeight -= delta;
			}
			else if (CurrentHeight - delta == 0)
			{
				CurrentHeight = 0;
			}
			else
			{
				throw new InvalidOperationException("Crash!");
			}
		}

		public abstract void WriteAllProperties();

		public virtual void WriteAllProperties2()
		{
			Console.WriteLine($"CurrentHeight:\t\t{CurrentHeight}");
			Console.WriteLine($"MaxHeight:\t\t{MaxHeight}");
		}
	}
}
