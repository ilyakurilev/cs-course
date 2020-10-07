using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Operation
{
    public class CircleOperation
    {
        public static double Square(double radius) =>
            Math.PI * radius * radius;

        public static double Length(double radius) =>
            2 * Math.PI * radius;

        public static double Diameter(double radius) =>
            2 * radius;
    }
}
