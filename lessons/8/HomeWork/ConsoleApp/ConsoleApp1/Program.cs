using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var s1 = "()";
            var s2 = "[]()";
            var s3 = "[[]()]";
            var s4 = "([([])])()[]";
            var s5 = "(";
            var s6 = "[][)";
            var s7 = "[(])";
            var s8 = "(()[]]";

            Console.WriteLine(ValidateBarackets(s1));
            Console.WriteLine(ValidateBarackets(s2));
            Console.WriteLine(ValidateBarackets(s3));
            Console.WriteLine(ValidateBarackets(s4));
            Console.WriteLine(ValidateBarackets(s5));
            Console.WriteLine(ValidateBarackets(s6));
            Console.WriteLine(ValidateBarackets(s7));
            Console.WriteLine(ValidateBarackets(s8));
        }

        public static bool ValidateBarackets(string text)
        {

            if (text == null)
            {
                return false;
            }

            Stack<char> brackets = new Stack<char>();
            bool isCorrect = true;
            for (var i = 0; i < text.Length && isCorrect; i++)
            {
                switch (text[i])
                {
                    case '(':
                    case '{':
                    case '[':
                        brackets.Push(text[i]);
                        break;
                    case ')':
                        if (brackets.Pop() != '(')
                        {
                            isCorrect = false;
                        }
                        break;
                    case '}':
                        if (brackets.Pop() != '{')
                        {
                            isCorrect = false;
                        }
                        break;
                    case ']':
                        if (brackets.Pop() != '[')
                        {
                            isCorrect = false;
                        }
                        break;
                }
            }

            return isCorrect && brackets.Count == 0;
        }
    }
}
