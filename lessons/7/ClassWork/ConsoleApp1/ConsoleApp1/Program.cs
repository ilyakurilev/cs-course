using System;
using System.Net.NetworkInformation;
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


            /*var sb = new StringBuilder(text);
            var count = 0;
            for (var i = 0; i < sb.Length;)
            {
                if (char.IsWhiteSpace(sb[i]))
                {
                    sb.Remove(i, 1);
                }
                else
                {
                    while (char.IsLetter(sb[i]))
                    {
                        if (count == 1)
                        {
                            sb[i] = char.ToUpper(sb[i]);
                        }
                        i++;
                    }
                    count++;
                }
            }
            Console.WriteLine(sb);*/
            var sb = new StringBuilder(text);
            Trim(sb);
            RemoveExtraSpaces(sb);
            SecondWordToUpper(sb);
            Console.WriteLine(sb);
        }

        static void Trim(StringBuilder sb)
        {
            while (char.IsWhiteSpace(sb[0]))
            {
                sb.Remove(0, 1);
            }

            while (char.IsWhiteSpace(sb[sb.Length - 1]))
            {
                sb.Remove(sb.Length - 1, 1);
            }
        }

        static void RemoveExtraSpaces(StringBuilder sb)
        {
            for (var i = 0; i < sb.Length; i++)
            {
                if (char.IsWhiteSpace(sb[i]))
                {
                    while (char.IsWhiteSpace(sb[i]))
                    {
                        sb.Remove(i, 1);
                    }
                    sb.Insert(i, ' ');
                }
            }
        }

        static void SecondWordToUpper(StringBuilder sb)
        {
            var countWords = 0;
            for (var i = 0; i < sb.Length;)
            {
                if (countWords > 1)
                {
                    break;
                }
                
                while (char.IsLetter(sb[i]))
                {
                    if (countWords == 1)
                    {
                        sb[i] = char.ToUpper(sb[i]);
                    }
                    i++;
                }
                i++;
                countWords++;
            }
            var i = 0;
            while (countWords <= 1 && i < sb.Length)
            {
                while (char.IsLetter(sb[i]))
                {
                    if (countWords == 1)
                    {
                        sb[i] = char.ToUpper(sb[i]);
                    }
                    i++;
                }
                i++;
                countWords++;
            }
        }

    }
}
