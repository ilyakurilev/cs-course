using System;

namespace ConsoleApp1
{
	partial class Pet
	{
		public int Age() =>
			(int)((DateTimeOffset.Now - Date).TotalDays / ((365 * 4 + 366) / 5.0));

		public string Desctripiton =>
			$"Name {Name}, Age: {Age()}, Sex: {Sex} , Kind: {Kind}";

		public string ShortDescription =>
			$"Name: {Name}, Age: {Age()}";

		public void WriteProperties(bool showFullDescription = false) =>
			Console.WriteLine(showFullDescription ? Desctripiton : ShortDescription);


		public void UpdateProperties(string name, DateTimeOffset date)
		{
			Name = name;
			Date = date;
		}

		public void UpdateProperties(string name, char sex)
		{
			Name = name;
			Sex = sex;
		}
	}
}
