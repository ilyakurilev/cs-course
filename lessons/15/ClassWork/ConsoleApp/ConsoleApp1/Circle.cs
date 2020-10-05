using System;

namespace ConsoleApp1
{
    public delegate double CircleDelegate(double a);

    public sealed class Circle
    {
        private readonly double _radius;

        public Circle(double radius) =>
            _radius = radius;
        

        public double Calculate(Func<double, double> operation) =>
             operation(_radius);

    }
}
