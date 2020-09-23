using System;

namespace ConsoleApp1
{
	class Passport : BaseDocument
	{
		public string Country { get; set; }
		public string PersonName { get; set; }

		public Passport(string number, DateTimeOffset issueDate, string country, string personName) : 
			base("Passport", number, issueDate)
		{
			Country = country;
			PersonName = personName;
		}

		public override string Description =>
			$"Title: {Title}, Number: {Number}, IssueDate: {IssueDate}, Country: {Country}, PersonName: {PersonName}";

		public void ChangeIssueDate(DateTimeOffset newIssueDate) =>
			IssueDate = newIssueDate;
	}
}
