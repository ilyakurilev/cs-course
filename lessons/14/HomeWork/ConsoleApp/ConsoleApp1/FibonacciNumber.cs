using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class FibonacciNumber : IEnumerable<int>
    {
        private class Enumerator : IEnumerator<int>
        {
            private FibonacciNumber _fibonacciNumber;
            private int _prev;
            private int _current;
            private int _count;

            public Enumerator(FibonacciNumber fibonacciNumber)
            {
                _fibonacciNumber = fibonacciNumber;
                Reset();
            }

            public bool MoveNext() =>
                ++_count < _fibonacciNumber._number;

            public int Current
            {
                get
                {
                    var temp = _current;
                    _current = _prev;
                    _prev = temp + _current;
                    return _current;
                }
            }

            object IEnumerator.Current
            {
                get => Current;
            }

            public void Reset()
            {
                _count = -1;
                _current = 1;
                _prev = 0;
            }

            public void Dispose()
            {
                if (_fibonacciNumber == null)
                {
                    return;
                }

                _fibonacciNumber = null;
            }
        }

        private readonly int _number;
  
        public FibonacciNumber(int number)
        {
            _number = number;
        }

        public IEnumerator<int> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
