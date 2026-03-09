using System;
using System.Collections.Generic;

namespace GeometryDash
{
    interface IAreaCalculable
    {
        double CalculateArea();
    }

    abstract class Shape : IAreaCalculable
    {
        public string Name;

        public Shape(string name)
        {
            Name = name;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Имя: {Name}, площадь: {CalculateArea()}");
        }

        public abstract double CalculateArea();
    }

    class Circle : Shape
    {
        public double Radius;

        public Circle(string name, double radius) : base(name)
        {
            Radius = radius;
        }

        public override double CalculateArea()
        {
            return 3.14 * Radius * Radius;
        }
    }

    class Rectangle : Shape
    {
        public double Width;
        public double Height;
        public string Color;

        public Rectangle(string name, double width, double height, string color) : base(name)
        {
            Width = width;
            Height = height;
            Color = color;
        }

        public override double CalculateArea()
        {
            return Width * Height;
        }

        public void Scale()
        {
            Width = Width + 10;
            Height = Height + 10;
        }

        public void Scale(double factor)
        {
            Width = Width * factor;
            Height = Height * factor;
        }

        public void Scale(double widthFactor, double heightFactor)
        {
            Width = Width * widthFactor;
            Height = Height * heightFactor;
        }
    }

    class Program
    {
        static void Main()
        {
            List<IAreaCalculable> figures = new List<IAreaCalculable>();

            Circle circle = new Circle("Круг_1", 5);
            figures.Add(circle);

            Rectangle rectangle = new Rectangle("Прямоугольник_1", 4, 6, "Черный");
            figures.Add(rectangle);

            foreach (IAreaCalculable figure in figures)
            {
                Console.WriteLine($"Площадь: {figure.CalculateArea()}");
            }

            rectangle.Scale();
            Console.WriteLine($"Площадь прямоугольника после Scale(): {rectangle.CalculateArea()}");

            rectangle.Scale(2);
            Console.WriteLine($"Площадь прямоугольника после Scale(2): {rectangle.CalculateArea()}");

            rectangle.Scale(1.5, 2.5);
            Console.WriteLine($"Площадь прямоугольника после Scale(1.5, 2,5): {rectangle.CalculateArea()}");
        }
    }
}