using System;

namespace ConsoleApp1
{
	interface IFlyingObject
	{
		int MaxHeight { get; }
		int CurrentHeight { get; }
		void TakeUpper(int delta);
		void TakeLower(int delta);
	}
}
