using PR5_3var;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR5_3var
{
    interface IAreaCalculable
    {
        double CalculateArea();
    }

    interface IDrawable
    {
        void Draw();
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
            Console.WriteLine($"Имя: {Name}, Площадь: {CalculateArea()}");
        }

        public abstract double CalculateArea();
    }

    class Circle : Shape, IDrawable
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

        public void Draw()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            int r = (int)Radius;
            int diameter = 2 * r + 1;

            for (int y = 0; y < diameter; y++)
            {
                for (int x = 0; x < diameter; x++)
                {
                    double dx = x - r;
                    double dy = y - r;
                    double distance = Math.Sqrt(dx * dx + dy * dy);

                    if (Math.Abs(distance - r) < 0.8) // Точки на окружности
                    {
                        Console.Write("*");
                    }
                    else if (distance < r) // Точки внутри круга
                    {
                        Console.Write(".");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }

            Console.ResetColor();
        }
    }

    class Rectangle : Shape, IDrawable
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
            Console.WriteLine("Scale() без параметров: ширина +10, высота +10");
        }

        public void Scale(double factor)
        {
            Width = Width * factor;
            Height = Height * factor;
            Console.WriteLine($"Scale({factor})");
        }

        public void Scale(double widthFactor, double heightFactor)
        {
            Width = Width * widthFactor;
            Height = Height * heightFactor;
            Console.WriteLine($"Scale({widthFactor}, {heightFactor})");
        }

        public void Draw()
        {
            switch (Color.ToLower())
            {
                case "красный":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "синий":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "зеленый":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "желтый":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            int w = (int)Width;
            int h = (int)Height;

            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if (i == 0 || i == h - 1 || j == 0 || j == w - 1)
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }

            Console.ResetColor();
        }
    }

    class TextShape : Shape, IDrawable
    {
        public string Text;
        private ConsoleColor textColor = ConsoleColor.White;
        private ConsoleColor backgroundColor = ConsoleColor.Black;

        public TextShape(string name, string text) : base(name)
        {
            Text = text;
        }

        public override double CalculateArea()
        {
            return Text.Length;
        }

        public void Draw()
        {
            Console.ForegroundColor = textColor;
            Console.BackgroundColor = backgroundColor;

            int width = Text.Length + 4;

            // Верхняя граница
            for (int i = 0; i < width; i++)
                Console.Write("*");
            Console.WriteLine();

            // Строка с текстом по бокам
            Console.Write("* ");
            Console.Write(Text);
            Console.Write(" *");
            Console.WriteLine();

            // Нижняя граница
            for (int i = 0; i < width; i++)
                Console.Write("*");

            Console.ResetColor();
            Console.WriteLine();
        }

        // Перегруженные методы SetStyle
        public void SetStyle(ConsoleColor newTextColor)
        {
            textColor = newTextColor;
            Console.WriteLine($"Установлен цвет текста: {newTextColor}");
        }

        public void SetStyle(ConsoleColor newTextColor, ConsoleColor newBackgroundColor)
        {
            textColor = newTextColor;
            backgroundColor = newBackgroundColor;
            Console.WriteLine($"Установлен цвет текста: {newTextColor}, цвет фона: {newBackgroundColor}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            List<Shape> shapes = new List<Shape>();

            Circle circle = new Circle("Круг_1", 5);
            shapes.Add(circle);

            Rectangle rectangle = new Rectangle("Прямоугольник_1", 15, 7, "Синий");
            shapes.Add(rectangle);

            TextShape textShape = new TextShape("Текст_1", "Привет, мир!");
            shapes.Add(textShape);

            Console.WriteLine("=== ДЕМОНСТРАЦИЯ ФИГУР ===\n");
            foreach (Shape shape in shapes)
            {
                shape.DisplayInfo();

                if (shape is IDrawable drawable)
                {
                    Console.WriteLine("Отрисовка:");
                    drawable.Draw();
                }

                Console.WriteLine();
            }

            Console.WriteLine("=== ДЕМОНСТРАЦИЯ SETSTYLE ===\n");

            TextShape myText = new TextShape("Стильный текст", "Hello world");
            myText.DisplayInfo();
            myText.Draw();

            Console.WriteLine("\nПосле SetStyle:");
            myText.SetStyle(ConsoleColor.Yellow);
            myText.Draw();

            Console.WriteLine("\nПосле SetStyle:");
            myText.SetStyle(ConsoleColor.Green, ConsoleColor.White);
            myText.Draw();

            Console.WriteLine("\n=== ДЕМОНСТРАЦИЯ SCALE ===\n");

            Console.WriteLine("После Scale:");
            rectangle.Scale(1.5);
            rectangle.DisplayInfo();
            rectangle.Draw();
        }
    }
}