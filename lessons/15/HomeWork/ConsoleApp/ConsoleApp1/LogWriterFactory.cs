using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class LogWriterFactory
    {
        private readonly IDictionary<Type, Func<object, ILogWriter>> _logers;

        private static LogWriterFactory _instance;
        public static LogWriterFactory Instance =>
            _instance ??= new LogWriterFactory();

        private LogWriterFactory()
        {
            _logers = new Dictionary<Type, Func<object, ILogWriter>>
            {
                //[typeof(ConsoleLogWriter)] = (parameters) => new ConsoleLogWriter(),
                [typeof(FileLogWriter)] = (parameters) => new FileLogWriter(parameters as string),
                [typeof(MultipleLogWriter)] = (parameters) => new MultipleLogWriter(parameters as ILogWriter[])
            };
        }

        public ILogWriter GetLogWriter<T>(object parameters)
            where T : ILogWriter
        {
            if (_logers.TryGetValue(typeof(T), out var ctor))
            {
                return ctor(parameters);
            }

            throw new NotSupportedException($"Type parameter \"{typeof(T)}\" is not supported");
        }

        public void RegisterLoger<T>(Func<object, ILogWriter> ctor)
            where T : ILogWriter
        {
            _logers.Add(typeof(T), ctor);
        }
    }
}
