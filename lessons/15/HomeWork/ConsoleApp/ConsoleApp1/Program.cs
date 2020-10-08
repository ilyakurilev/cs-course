using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            LogWriterFactory.Instance.RegisterLoger<ConsoleLogWriter>(parameters => new ConsoleLogWriter());

            var consoleLogWriter = LogWriterFactory.Instance.GetLogWriter<ConsoleLogWriter>(null);
            var fileLogWriter = LogWriterFactory.Instance.GetLogWriter<FileLogWriter>("log.txt");
            var multipleLogWriter = LogWriterFactory.Instance.GetLogWriter<MultipleLogWriter>(new ILogWriter[] { consoleLogWriter, fileLogWriter });
   
            multipleLogWriter.LogError("Some error!");
            multipleLogWriter.LogInfo("Some info!");
            multipleLogWriter.LogWarning("Some warning!");
        }
    }
}
