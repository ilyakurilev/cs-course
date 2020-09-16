using System;


namespace ConsoleApp1
{
    enum TypeAnimal
    {
        Cat,
        Dog,
        Mouse
    }


    class Pet
    {
        private string kind;
        private string name;
        private char sex;
        private int age;

        public string Kind
        {
            get { return kind; }
            set
            {
                kind = Enum.Parse(typeof(TypeAnimal), value, true).ToString();
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value ?? throw new ArgumentException("Name propertie can not be null");
            }
        }

        public char Sex
        {
            get { return sex; }
            set
            {
                if (char.ToLower(value) != 'm' && char.ToLower(value) != 'f')
                {
                    throw new ArgumentException();
                }
                sex = value;
            }
        }

        public int Age
        {
            get { return age; }
            set
            {
                if (value < 0 || value > 25)
                {
                    throw new ArgumentException("");
                }
                age = value;
            }
        }

        public string Description
        {
            get { return $"{Name} is {Kind} ({Sex}) {Age} age"; }
        }
    }
}
