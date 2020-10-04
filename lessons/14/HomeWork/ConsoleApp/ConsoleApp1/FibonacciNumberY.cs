using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class FibonacciNumberY : IEnumerable<int>
    {
        private readonly int _number;

        public FibonacciNumberY(int number)
        {
            _number = number;
        }

        public IEnumerator<int> GetEnumerator()
        {
            var _current = 1;
            var _prev = 0;
            for (int i = 0; i < _number; i++)
            {

                var temp = _current;
                _current = _prev;
                _prev = temp + _current;
                yield return _current;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
