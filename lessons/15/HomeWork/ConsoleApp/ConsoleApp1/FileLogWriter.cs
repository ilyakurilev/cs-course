using System;
using System.IO;

namespace ConsoleApp1
{
    class FileLogWriter : AbstractLogWriter
    {
        private readonly string _fileName;

        public FileLogWriter(string path = "log.txt")
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException($"\"{nameof(path)}\" cannot be null or empty");
            }
            _fileName = path;
        }

        protected override void Write(string message, LogType logType)
        {
            var formattedMessage = FormatMessage(message, logType) + "\n";
            File.AppendAllText(_fileName, formattedMessage);
        }
    }
}
