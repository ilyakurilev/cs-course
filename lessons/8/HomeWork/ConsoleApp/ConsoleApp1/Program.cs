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
            var s9 = "]";

            Console.WriteLine(ValidateBarackets(s1));
            Console.WriteLine(ValidateBarackets(s2));
            Console.WriteLine(ValidateBarackets(s3));
            Console.WriteLine(ValidateBarackets(s4));
            Console.WriteLine(ValidateBarackets(s5));
            Console.WriteLine(ValidateBarackets(s6));
            Console.WriteLine(ValidateBarackets(s7));
            Console.WriteLine(ValidateBarackets(s8));
            Console.WriteLine(ValidateBarackets(s9));
        }

        public static bool ValidateBarackets(string text)
        {
            if (text == null)
            {
                return false;
            }

            var bracketsDictionary = new Dictionary<char, char>
            {
                ['('] = ')',
                ['{'] = '}',
                ['['] = ']',
            };

            var reverseBracketsDictionary = new Dictionary<char, char>();
            foreach (var pair in bracketsDictionary)
            {
                reverseBracketsDictionary.Add(pair.Value, pair.Key);
            }

            Stack<char> brackets = new Stack<char>();
            bool isCorrect = true;
            for (var i = 0; i < text.Length && isCorrect; i++)
            {
                if (bracketsDictionary.ContainsKey(text[i]))
                {
                    brackets.Push(text[i]);
                }
                else if (reverseBracketsDictionary.ContainsKey(text[i]))
                {
                    isCorrect = brackets.TryPop(out var br) && br == reverseBracketsDictionary[text[i]];
                    if (!isCorrect)
                    {
                        break;
                    }
                }     
            }

            return isCorrect && brackets.Count == 0;
        }
    }
}
