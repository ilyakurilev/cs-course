using System;

namespace ConsoleApp1
{
	class BaseDocument
	{
        public string Title { get; set; }
        public string Number { get; set; }
        public DateTimeOffset IssueDate { get; set; }

        public BaseDocument(string title, string number, DateTimeOffset issueDate)
		{
            Title = title;
            Number = number;
            IssueDate = issueDate;
		}

        public virtual string Description =>
            $"Title: {Title}, Number: {Number}, IssueDate: {IssueDate}";

        public override string ToString() =>
            Description;

        public void WriteToConsole()
		{
			Console.WriteLine(Description);
		}
	}
}
