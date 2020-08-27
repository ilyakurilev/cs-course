using System;

namespace JuicePackagingApp
{

    [Flags]
    enum Bottle
    {
        None = 0,   
        OneLiter = 0x1,
        FiveLiters = 0x2,
        TwentyLiters = 0x4
    }

    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("How much juice (in liters) you need to pack?");
            Console.Write(">");
            double litersOfJuice = Double.Parse(Console.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);

            Bottle bottles = Bottle.None;

            int twentyLiters = (int)(litersOfJuice / 20);
            if (twentyLiters > 0)
            {
                bottles |= Bottle.TwentyLiters;
                litersOfJuice %= 20;
            }

            int fiweLiters = (int)(litersOfJuice / 5);
            if (fiweLiters > 0)
            {
                bottles |= Bottle.FiveLiters;
                litersOfJuice %= 5;
            }

            int oneLiters = (int)Math.Round(litersOfJuice, MidpointRounding.ToPositiveInfinity);
            if (oneLiters > 0)
            {
                bottles |= Bottle.OneLiter;
            }

            Console.WriteLine("You need the following containers:");
            if ((bottles & Bottle.TwentyLiters) == Bottle.TwentyLiters)
            {
                Console.WriteLine("20 l: {0}pcs.", twentyLiters);
            }
            if ((bottles & Bottle.FiveLiters) == Bottle.FiveLiters)
            {
                Console.WriteLine("5 l: {0}pcs.", fiweLiters);
            }
            if ((bottles & Bottle.OneLiter) == Bottle.OneLiter)
            {
                Console.WriteLine("1 l: {0}pcs.", oneLiters);
            }

        }
    }
}
