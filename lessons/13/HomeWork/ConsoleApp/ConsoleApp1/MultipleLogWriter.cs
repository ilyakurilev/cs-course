namespace ConsoleApp1
{
    class MultipleLogWriter : AbstractLogWriter
    {
        public ILogWriter[] LogWriters { get; private set; }


        public MultipleLogWriter(params ILogWriter[] logWriters)
        {
            LogWriters = logWriters;
        }


        protected override void Write(string message, LogType logType)
        {
            switch (logType)
            {
                case LogType.Info:
                    foreach (var logWriter in LogWriters)
                    {
                        logWriter.LogInfo(message);
                    }
                    break;
                case LogType.Warning:
                    foreach (var logWriter in LogWriters)
                    {
                        logWriter.LogWarning(message);
                    }
                    break;
                case LogType.Error:
                    foreach (var logWriter in LogWriters)
                    {
                        logWriter.LogError(message);
                    }
                    break;
            }
        }
    }
}
