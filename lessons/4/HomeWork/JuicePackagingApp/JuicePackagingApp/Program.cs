using System;

namespace JuicePackagingApp
{

    //[Flags]
    enum Bottle
    {
        OneLiter = 1,
        FiveLiters = 5,
        TwentyLiters = 20,
    }


    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("How much juice (in liters) you need to pack?");
            Console.Write("> ");
            double litersOfJuice = Double.Parse(Console.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);

            int bottlesFlags = 0;
            Bottle[] bottles = (Bottle[])Enum.GetValues(typeof(Bottle));
            int[] numberOfBottles = new int[bottles.Length];

            for (int i = bottles.Length - 1; i > 0; i--)
            {
                numberOfBottles[i] = (int)litersOfJuice / (int)bottles[i];
                if (numberOfBottles[i] > 0)
                {
                    bottlesFlags |= 1 << i;
                    litersOfJuice %= (int)bottles[i];
                }
            }

            numberOfBottles[0] = (int)Math.Round(litersOfJuice / (int)bottles[0], MidpointRounding.ToPositiveInfinity);
            if (numberOfBottles[0] > 0)
            {
                bottlesFlags |= 1;
            }


            Console.WriteLine("You need the following containers:");
            for (int i = bottles.Length - 1; i >= 0; i--)
            {
                if ((bottlesFlags & 1 << i) == 1 << i)
                {
                    Console.WriteLine("{0,4:d} l: {1}pcs.", bottles[i], numberOfBottles[i]);
                }
            }

        }
    }
}
