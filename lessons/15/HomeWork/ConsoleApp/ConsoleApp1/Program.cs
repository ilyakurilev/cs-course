
namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var consoleLogWriter = LogWriterFactory.Instance.GetLogWriter<ConsoleLogWriter>();
            var fileLogWriter = LogWriterFactory.Instance.GetLogWriter<FileLogWriter>();
            var multipleLogWriter = LogWriterFactory.Instance.GetLogWriter<MultipleLogWriter>(new ILogWriter[] { consoleLogWriter, fileLogWriter });


            multipleLogWriter.LogError("Some error!");
            multipleLogWriter.LogInfo("Some info!");
            multipleLogWriter.LogWarning("Some warning!");
        }
    }
}
