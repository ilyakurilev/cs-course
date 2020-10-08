using System;

namespace ConsoleApp1
{
    class MultipleLogWriter : ILogWriter
    {
        public ILogWriter[] LogWriters { get; private set; }

        public MultipleLogWriter(params ILogWriter[] logWriters)
        {
            LogWriters = logWriters ?? throw new ArgumentNullException($"\"{nameof(logWriters)}\" cannot be null");
        }

        public void LogInfo(string message)
        {
            foreach (var writer in LogWriters)
            {
                writer.LogInfo(message);
            }
        }

        public void LogWarning(string message)
        {
            foreach (var writer in LogWriters)
            {
                writer.LogWarning(message);
            }
        }

        public void LogError(string message)
        {
            foreach (var writer in LogWriters)
            {
                writer.LogError(message);
            }
        }
    }
}
