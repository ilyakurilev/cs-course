using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] availableOperations = { '+', '-', '*', '/' };

            double firstNumber = ReadNumber("Enter the first number: ");
            double secondNumber = ReadNumber("Enter the second number: ");
            char operation = ReadOperation("Enter the operation: ", availableOperations);


            double? result = operation switch
            {
                '+' => firstNumber + secondNumber,
                '-' => firstNumber - secondNumber,
                '*' => firstNumber * secondNumber,
                '/' => firstNumber / secondNumber,
                _ => null
            };

            if (result != null)
            {
                Console.WriteLine($"{firstNumber} {operation} {secondNumber} = {result}");
            }
            else
            {
                Console.WriteLine("Incorrect operation : {0}", operation);
            }

        }

        static double ReadNumber(string prompt)
        {
            for (; ; )
            {
                Console.Write(prompt);
                bool isParse = double.TryParse(Console.ReadLine(), out double number);
                if (isParse)
                {
                    return number;
                }
                else
                {
                    Console.WriteLine("Incorrect input, try again.");
                }
            }
        }

        static char ReadOperation(string prompt, char[] availableOperations)
        {
            for (; ; )
            {
                Console.Write(prompt);
                bool isParse = char.TryParse(Console.ReadLine(), out char operation);
                if (isParse)
                {
                    for (int i = 0; i < availableOperations.Length; i++)
                    {
                        if (operation == availableOperations[i])
                        {
                            return operation;
                        }
                    }
                    Console.WriteLine("Incorrect operation, try again.");
                }
            }
        }
    }
}
