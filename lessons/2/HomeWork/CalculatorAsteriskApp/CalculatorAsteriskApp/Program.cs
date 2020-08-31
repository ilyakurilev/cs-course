using System;

namespace CalculatorAsteriskApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Calculator App *****\n");

            Console.Write("Enter the first integer: ");
            int firstNumber = System.Int32.Parse(Console.ReadLine());

            Console.Write("Enter the second integer: ");
            int secondNumber = System.Int32.Parse(Console.ReadLine());

            Console.Write("Enter operation(+, -, *, /, %, ^): ");
            char operation = System.Char.Parse(Console.ReadLine());

            int result = 0;

            if (operation == '+')
            {
                result = firstNumber + secondNumber;
            }
            else if (operation == '-')
            {
                result = firstNumber - secondNumber;
            }
            else if (operation == '*')
            {
                result = firstNumber * secondNumber;
            }
            else if (operation == '/')
            {
                result = firstNumber / secondNumber;
            }
            else if (operation == '%')
            {
                result = firstNumber % secondNumber;
            }
            else if (operation == '^')
            {
                result = (int)Math.Pow(firstNumber, secondNumber);
            }
            else
            {
                Console.WriteLine("\nWrong operation: \"{0}\"", operation);
                return;
            }

            Console.WriteLine();
            Console.WriteLine("********* Result *********\n");
            Console.WriteLine("{0} {1} {2} = {3}", firstNumber, operation, secondNumber, result);

        }
    }
}
