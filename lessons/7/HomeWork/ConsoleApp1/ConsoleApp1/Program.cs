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
                try
                {
                    Console.Write("> ");
                    words = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (words.Length < 2)
                    {
                        Console.WriteLine("Слишком мало слов, попробуйте ещё раз:");
                        continue;
                    }
                    break;
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("Вы ввели null строку, попробуйте ещё раз:");
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
