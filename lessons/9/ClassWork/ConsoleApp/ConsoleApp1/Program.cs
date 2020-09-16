using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = 10;
            var array = new int[n];
            var rand = new Random();
            for (var i = 0; i < n; i++)
            {
                array[i] = rand.Next(10);
            }

            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        var temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }
    }
}
