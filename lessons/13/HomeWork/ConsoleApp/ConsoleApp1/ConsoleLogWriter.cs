using System;

namespace ConsoleApp1
{
    class ConsoleLogWriter : AbstractLogWriter
    {
        protected override void Write(string message, LogType logType)
        {
            var formattedMessage = FormatMessage(message, logType);
            switch (logType)
            {
                case LogType.Info:
                    WriteConsole(formattedMessage);
                    break;
                case LogType.Warning:
                    WriteConsole(formattedMessage, ConsoleColor.Yellow);
                    break;
                case LogType.Error:
                    WriteConsole(formattedMessage, ConsoleColor.Red);
                    break;
            }
        }

        private void WriteConsole(string message, ConsoleColor color = ConsoleColor.White)
        {
            var defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = defaultColor;
        }
    }
}
