using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the number of home work(1 or 2):");
            while (true)
            {
                Console.Write("> ");
                if (int.TryParse(Console.ReadLine(), out var numberHomeWork)) {
                    switch(numberHomeWork)
                    {
                        case 1:
                            HomeWork1();
                            break;
                        case 2:
                            HomeWork2();
                            break;
                        default:
                            Console.WriteLine("Invalid value entered! Try again:");
                            continue;
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid value entered! Try again:");
                }
            }
        }

        static void HomeWork1()
        {
            Console.WriteLine("Enter a positive natural number no more than 2 billion:");

            var input = 0;
            while (true)
            {
                Console.Write("> ");
                try
                {
                    input = int.Parse(Console.ReadLine());
                    if (input > 2_000_000_000)
                    {
                        throw new OverflowException();
                    }
                    if (input <= 0)
                    {
                        Console.WriteLine("Invalid value entered! Try again:");
                        continue;
                    }
                    break;
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Error {ex.GetType()}! Try again:");
                }
                catch (OverflowException ex)
                {
                    Console.WriteLine($"Error {ex.GetType()}! Try again:");
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine($"Error {ex.GetType()}! Try again:");
                }
            }
            Console.Write($"Number {input} contains the following number of even numbers: {CountEvenNumbers(input)}");
        }

        static int CountEvenNumbers(int input)
        {
            var count = 0;
            while (input > 0)
            {
                int lastDigit = input % 10;
                if (lastDigit % 2 == 0)
                {
                    count++;
                }
                input /= 10;
            }
            return count;
        }

        static void HomeWork2()
        {
            var initialPayment = 0.0m;
            var desiredAmount = 0.0m;
            var dailyPercent = 0.0m;

            initialPayment = Read("Enter the amount of the initial payment in rubles:");
            while (true)
            {
                
                dailyPercent = Read("Enter daily income percentage as a decimal(1% = 0.01):");
                if (dailyPercent > 1)
                {
                    Console.WriteLine("Invalid value entered, percentage must be between 0 and 1! Try again:");
                    continue;
                }
                break;
            }
            while (true)
            {
                desiredAmount = Read("Enter the desired amount of savings in rubles:");
                if (desiredAmount < initialPayment)
                {
                    Console.WriteLine("Invalid value entered, the desired amount must be greater than the initial payment! Try again:");
                    continue;
                }
                break;
            }

            var currentBalance = initialPayment;
            var countDays = 0;

            while (currentBalance < desiredAmount)
            {
                currentBalance += currentBalance * dailyPercent;
                countDays++;
            }

            Console.WriteLine($"The required number of days to accumulate the desired amount: {countDays}");
        }

        static decimal Read(string prompt)
        {
            var input = 0.0m;

            Console.WriteLine(prompt);

            while (true)
            {
                Console.Write("> ");
                try
                {
                    input = decimal.Parse(Console.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);
                    if (input <= 0)
                    {
                        Console.WriteLine("Invalid value entered! Try again:");
                        continue;
                    }

                    break;
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Error {ex.GetType()}! Try again:");
                }
                catch (OverflowException ex)
                {
                    Console.WriteLine($"Error {ex.GetType()}! Try again:");
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine($"Error {ex.GetType()}! Try again:");
                }
            }

            return input;
        }
    }
}
