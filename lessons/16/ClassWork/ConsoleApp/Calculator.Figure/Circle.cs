using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Figure
{
    public class Circle
    {
        private readonly double _radius;

        public Circle(int radius) =>
            _radius = radius;

        public double Calculate(Func<double, double> func) =>
            func(_radius);
    }
}
