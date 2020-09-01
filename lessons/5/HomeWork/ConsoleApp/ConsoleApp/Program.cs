using System;

namespace ConsoleApp
{

    enum Shape : byte
    {
        Circle = 1,
        Triangle,
        Rectangle
    }

    class Program
    {
        static void Main(string[] args)
        {
            Shape shape = ReadShape();

            double[] paramsArray = ReadParamsOfShape(shape);

            double area = CalculateArea(shape, paramsArray);
            double perimeter = CalculatePerimeter(shape, paramsArray);

            Console.WriteLine();
            Console.WriteLine($"Area of {shape} = {Math.Round(area, 2, MidpointRounding.AwayFromZero)}");
            Console.WriteLine($"Perimeter of {shape} = {Math.Round(perimeter, 2, MidpointRounding.AwayFromZero)}");
        }

        static Shape ReadShape()
        {
            Console.WriteLine("Enter shape type(1 circle, 2 triangle(equilateral), 3 rectangle:");
            for (; ; )
            {
                Console.Write("> ");
                bool success = Enum.TryParse(Console.ReadLine(), true, out Shape shape);
                if (success && Enum.IsDefined(typeof(Shape), shape))
                {
                    return shape;
                }
                Console.WriteLine("Incorrect input, try again.");
            }
        }

        static double[] ReadParamsOfShape(Shape shape)
        {
            string[] prompt = shape switch
            {
                Shape.Circle => new string[] { "Enter the diameter of circle: " },
                Shape.Triangle => new string[] { "Enter the side of triangle: " },
                Shape.Rectangle => new string[] { "Enter the width of rectangle: ", "Enter the height of rectangle: " },
                _ => throw new ArgumentOutOfRangeException($"{shape} is undefined shape!")
            };

            double[] paramsArray = new double[prompt.Length];

            for (int i = 0; i < paramsArray.Length; i++)
            {
                for (; ; )
                {
                    Console.WriteLine(prompt[i]);
                    Console.Write("> ");
                    System.Globalization.NumberStyles style = System.Globalization.NumberStyles.AllowDecimalPoint;
                    bool success = double.TryParse(Console.ReadLine(), style, System.Globalization.CultureInfo.InvariantCulture, out double param);
                    if (success && param > 0)
                    {
                        paramsArray[i] = param;
                        break;
                    }

                    Console.WriteLine("Incorrect input, try again.");
                }
            }
            return paramsArray;
        }

        static double CalculateArea(Shape shape, double[] paramsArray)
        {
            return shape switch
            {
                Shape.Circle => CircleSurfaceArea(paramsArray[0]),
                Shape.Triangle => TriangleSurfaceArea(paramsArray[0]),
                Shape.Rectangle => RectangleSurfaceArea(paramsArray[0], paramsArray[1]),
                _ => throw new ArgumentOutOfRangeException($"{shape} is undefined shape!")
            };
        }

        static double CalculatePerimeter(Shape shape, double[] paramsArray)
        {
            return shape switch
            {
                Shape.Circle => CirclePerimeterLength(paramsArray[0]),
                Shape.Triangle => TrianglePerimeterLength(paramsArray[0]),
                Shape.Rectangle => RectanglePerimeterLength(paramsArray[0], paramsArray[1]),
                _ => throw new ArgumentOutOfRangeException($"{shape} is undefined shape!")
            };
        }

        static double CircleSurfaceArea(double diameter)
        {
            return 1.0 / 4.0 * Math.PI * Math.Pow(diameter, 2);
        }

        static double TriangleSurfaceArea(double side)
        {
            return Math.Pow(side, 2) * Math.Sqrt(3) / 4;
        }

        static double RectangleSurfaceArea(double width, double height)
        {
            return width * height;
        }

        static double CirclePerimeterLength(double diameter)
        {
            return Math.PI * diameter;
        }

        static double TrianglePerimeterLength(double side)
        {
            return side * 3;
        }

        static double RectanglePerimeterLength(double width, double height)
        {
            return (width + height) * 2;
        }
    }
}
