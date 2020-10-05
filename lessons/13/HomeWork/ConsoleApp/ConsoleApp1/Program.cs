using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var consoleLogWriter = new ConsoleLogWriter();
            var fileLogWriter = new FileLogWriter();
            var multipleLogWriter = new MultipleLogWriter(consoleLogWriter, fileLogWriter);

            multipleLogWriter.LogError("Some error");
            multipleLogWriter.LogWarning("Some warning");
            multipleLogWriter.LogInfo("Some info");
        }
    }
}
