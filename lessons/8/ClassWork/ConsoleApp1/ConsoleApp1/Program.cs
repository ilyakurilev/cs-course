using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*var capitals = new Dictionary<string, string>
            {
                ["Россия"] = "Москва",
                ["Япония"] = "Токио",
                ["Германия"] = "Берлин",
                ["Китай"] = "Пекин",
                ["Мексика"] = "Мехико",
            };

            while (true)
            {
                var count = new Random().Next(capitals.Count);
                var i = 0;
                var answer = "";
                foreach (var capital in capitals)
                {
                    if (i == count)
                    {
                        Console.Write($"Введите столицу {capital.Key}: ");
                        answer = Console.ReadLine();
                        if (answer.Equals(capital.Value, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("Поздравляю, это верный ответ!");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Это неверный ответ!");
                            return;
                        }
                    }
                    i++;
                }
            }*/

            /*var queue = new Queue<double>();
            Console.WriteLine("Введите число:");
            while (true)
            {
                Console.Write("> ");
                var line = Console.ReadLine();
                if (line.Equals("run", StringComparison.OrdinalIgnoreCase))
                {
                    while (queue.Count > 0)
                    {
                        var q = queue.Dequeue();
                        Console.WriteLine($"Квадратный корень {q} = {Math.Sqrt(q)}");
                    }
                    continue;
                }
                if (line.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Число оставшихся задач {queue.Count}");
                    break;
                }
                var number = double.Parse(line, System.Globalization.CultureInfo.InvariantCulture);
                queue.Enqueue(number);
            }*/

            var stack = new Stack<string>();
            Console.WriteLine("Введите команду: ");
            while (true)
            {
                Console.Write("> ");
                var line = Console.ReadLine();
                switch (line)
                {
                    case "wash":
                        stack.Push("Тарелка");             
                        break;
                    case "dry":
                        if (!stack.TryPop(out var plate))
                        {
                            Console.WriteLine("Стопка тарелок пуста!");
                        }

                        break;
                    case "exit":
                        Console.WriteLine($"Тарелок в стопке: {stack.Count}");
                        return;
                    default:
                        Console.WriteLine("Неверная команда!");
                        break;
                }
                Console.WriteLine($"Тарелок в стопке: {stack.Count}");
            }
        }
    }
}
