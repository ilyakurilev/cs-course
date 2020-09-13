using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите строку из нескольких слов: ");
            string[] words;
            while (true)
            {

                Console.Write("> ");
                var line = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(line))
                {
                    Console.WriteLine("Вы ввели пустую строку, попробуйте ещё раз:");
                }
                else
                {
                    words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (words.Length < 2)
                    {
                        Console.WriteLine("Слишком мало слов, попробуйте ещё раз:");
                    }
                    else
                    {
                        break;
                    }
                }
            }


            var numberWordsA = 0;
            foreach (var word in words)
            {
                if (char.ToLower(word[0]) == 'a' || char.ToLower(word[0]) == 'а')
                {
                    numberWordsA++;
                }
            }

            Console.WriteLine($"Количество слов, начинающихся с буквы \'A\': {numberWordsA}");
            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey(true);
        }
    }
}
