using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var writer = new FileWriterWithProgress();

            writer.WritingPerformed += (sender, e) => Console.WriteLine(e.Percentage);
            writer.WritingCompleted += (sender, e) => Console.WriteLine("Completed");

            var data = new byte[100];
            new Random().NextBytes(data);
            writer.WriteBytes("log.txt", data, 0.1f);
        }
    }
}
