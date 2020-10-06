using System;

namespace ConsoleApp1
{
    class LogWriterFactory
    {
        private static LogWriterFactory _instance;
        public static LogWriterFactory Instance =>
            _instance ??= new LogWriterFactory();

        private LogWriterFactory()
        {

        }

        public ILogWriter GetLogWriter<T>(object parameters = null)
            where T : ILogWriter
        {
            if (typeof(T) == typeof(ConsoleLogWriter))
            {
                return new ConsoleLogWriter();
            }


            if (typeof(T) == typeof(FileLogWriter))
            {
                return parameters is string fileName ?
                    new FileLogWriter(fileName) :
                    parameters == null ?
                    new FileLogWriter() :
                    throw new ArgumentException($"Parameter \"{nameof(parameters)}\" must be of type \"{typeof(string)}\"");
            }


            if (typeof(T) == typeof(MultipleLogWriter))
            {
                return parameters is ILogWriter[] logWriters ?
                    new MultipleLogWriter(logWriters) : 
                    throw new ArgumentException($"Parameter \"{nameof(parameters)}\" must be of type \"{typeof(ILogWriter[])}\"");
            }


            throw new NotSupportedException($"Type parameter \"{typeof(T)}\" is not supported");
        }
    }
}
