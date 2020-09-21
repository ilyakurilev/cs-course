using System;


namespace ConsoleApp1
{
	partial class Pet
	{
		public string Name;
		public string Kind;
		public char Sex;
		public DateTimeOffset Date;

		public Pet(string name, string kind, char sex, DateTimeOffset date)
		{
			Name = name;
			Kind = kind;
			Sex = sex;
			Date = date;
		}
	}
}
