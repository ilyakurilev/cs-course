using System;
using System.Text;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите непустую строку:");
            string line;
            while (true)
            {
                try
                {
                    Console.Write("> ");
                    line = Console.ReadLine();
                    if (line.Trim() == "")
                    {
                        Console.WriteLine("Вы ввели пустую строку, попробуйте ещё раз: ");
                        continue;
                    }
                    break;
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("Вы ввели null строку, попробуйте ещё раз:");
                }
            }

            var sb = new StringBuilder();
            for (int i = line.Length - 1; i >= 0; i--)
            {
                sb.Append(char.ToLower(line[i]));
            }

            Console.Write("> ");
            Console.WriteLine(sb);
            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey(true);
        }
    }
}
