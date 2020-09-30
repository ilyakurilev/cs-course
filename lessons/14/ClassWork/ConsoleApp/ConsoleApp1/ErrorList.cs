using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class ErrorList : IDisposable, IEnumerable<string>
    {
        private List<string> _errors;

        public string Category { get; }
        
        public ErrorList(string category)
        {
            Category = category;
            _errors = new List<string>();
        }

        public void Add(string errorMessage)
        {
            _errors.Add(errorMessage);
        }

        public void Dispose()
        {
            if (_errors == null)
            {
                return;
            }

            _errors.Clear();
            _errors = null;
        }

        public IEnumerator<string> GetEnumerator() =>
            _errors.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            _errors.GetEnumerator();
    }
}
