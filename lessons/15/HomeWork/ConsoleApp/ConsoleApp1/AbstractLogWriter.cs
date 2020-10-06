using System;

namespace ConsoleApp1
{
    enum LogType : byte
    {
        Error,
        Info,
        Warning
    }

    abstract class AbstractLogWriter : ILogWriter
    {
        private readonly string _format = "yyyy-MM-dd HH:mm:sszz";

        public void LogError(string message)
        {
            Write(message, LogType.Error);
        }

        public void LogInfo(string message)
        {
            Write(message, LogType.Info);
        }

        public void LogWarning(string message)
        {
            Write(message, LogType.Warning);
        }

        protected string FormatMessage(string message, LogType logType)
        {
            return $"{DateTimeOffset.UtcNow.ToString(_format)}\t{logType}\t{message}";
        }

        protected abstract void Write(string message, LogType logType);
    }
}
