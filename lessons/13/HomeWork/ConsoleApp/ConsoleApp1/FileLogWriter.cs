using System.IO;

namespace ConsoleApp1
{
    class FileLogWriter : AbstractLogWriter
    {
        private readonly string _fileName;

        public FileLogWriter(string path = "log.txt")
        {
            _fileName = path;
            File.WriteAllText(_fileName, "");
        }

        protected override void Write(string message, LogType logType)
        {
            var formattedMessage = FormatMessage(message, logType) + "\n";
            File.AppendAllText(_fileName, formattedMessage);
        }
    }
}
