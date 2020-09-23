using System;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			var documents = new BaseDocument[]
			{
				new BaseDocument("Some document", "0000", DateTimeOffset.UtcNow.AddYears(-1)),
				new Passport("1234", DateTimeOffset.UtcNow.AddYears(-20), "Russia", "Ivan"),
				new BaseDocument("Some document", "1111", DateTimeOffset.UtcNow.AddYears(-2)),
				new Passport("8545", DateTimeOffset.UtcNow.AddYears(-25), "Russia", "Oleg")
			};

			foreach (var document in documents)
			{
				if (document is Passport passport)
				{
					passport.ChangeIssueDate(DateTimeOffset.UtcNow);
				}
				document.WriteToConsole();
			}
		}
	}
}
