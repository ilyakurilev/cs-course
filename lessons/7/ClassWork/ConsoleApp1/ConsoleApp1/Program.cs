using System;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "     lorem     ipsum    dolor    sit   amet  ";
            /*var array = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            array[1] = array[1].ToUpper();
            var result = string.Join(' ', array);
            Console.WriteLine(result);

            string temp = text.TrimEnd();
            string result2 = temp.Substring(0, temp.LastIndexOf(' ') + 1).TrimEnd();
            Console.WriteLine(result2);*/
            var sb = new StringBuilder(text);
        }
    }
}
