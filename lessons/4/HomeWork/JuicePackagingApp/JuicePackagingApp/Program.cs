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
            
            //each elemet of the array corresponds to the number of containers of a sertain size from enum Bottle
            //from larger volume to smaller
            int[] numberOfBottles = new int[bottles.Length];

            for (int i = bottles.Length - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    numberOfBottles[bottles.Length - i - 1] = (int)Math.Round(litersOfJuice / (int)bottles[i], MidpointRounding.ToPositiveInfinity);
                    if (numberOfBottles[bottles.Length - i - 1] > 0)
                    {
                        bottlesFlags |= 1 << i;
                    }
                }
                else
                {
                    numberOfBottles[bottles.Length - i - 1] = (int)litersOfJuice / (int)bottles[i];
                    if (numberOfBottles[bottles.Length - i - 1] > 0)
                    {
                        bottlesFlags |= 1 << i;
                        litersOfJuice %= (int)bottles[i];
                    }
                }
            }

            Console.WriteLine("You need the following containers:");
            for (int i = 0; i < bottles.Length; i++)
            {
                if ((bottlesFlags & 1 << bottles.Length - i - 1) == 1 << bottles.Length - i - 1)
                {
                    Console.WriteLine("{0,4:d} l: {1}pcs.", bottles[bottles.Length - 1 - i], numberOfBottles[i]);
                }
            }

        }
    }
}
