using System;

namespace ConsoleApp1
{
    class Person
    {
        private string _name;
        private int _age;

        public string Name
        {
            get => _name; 
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name must not be empty");
                }
                _name = value;
            }
        }

        public int Age
        {
            get => _age;
            set
            {
                if (value < 0 || value > 125)
                {
                    throw new ArgumentException("Age must be between 0 and 125");
                }
                _age = value;
            }
        }

        public int AgeInFourYears
        {
            get => _age + 4;
        }

        public string Information
        {
            get => $"Name: {Name}, age in 4 years: {AgeInFourYears}.";
        }

    }
}
