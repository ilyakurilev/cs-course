using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Console.Write("Enter the age from 1 to 100: ");
            int age = System.Int32.Parse(Console.ReadLine());
           
            
            if (age > 200)
            {
                throw new Exception("Number is greather than 200!");         
            }



            if (age < 1 || age > 100)
            {
                Console.WriteLine("Error input!");
            }
            if (age % 10 == 1 && age / 10 != 1)
            {
                Console.WriteLine("{0} год", age);
            }
            else if ((age % 10 <= 4) && age / 10 != 1)
            {
                Console.WriteLine("{0} года", age);
            }
            else
            {
                Console.WriteLine("{0} лет", age);
            }*/
            double firstNumber;
            double secondNumber;
            char operation;

            while (true) {
                try
                {
                    Console.Write("Enter the first number: ");
                    firstNumber = System.Double.Parse(Console.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);
                    Console.Write("Enter the second number: ");
                    secondNumber = System.Double.Parse(Console.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);
                    Console.Write("Enter the operation (+, -, * /): ");
                    operation = System.Char.Parse(Console.ReadLine());
                    break;
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("Incorrect input, try again");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Incorrect input, try again");
                }
                catch (DivideByZeroException)
                {
                    Console.WriteLine("Incorrect input, try again");
                }
                catch
                {
                    Console.WriteLine("Incorrect input, try again");
                }
            }

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
    }
}
