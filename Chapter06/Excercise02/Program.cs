namespace Excercise02
{
    class Program
    {
        static void Main(string[] args)
        {
            Shape s1 = new Rectangle(2, 3);
            Console.WriteLine($"{s1.Area}");
            Shape s2 = new Square(2);
            Console.WriteLine($"{s2.Area}");
            Shape s3 = new Circle(2);
            Console.WriteLine($"{s3.Area}");
        }
    }

    public abstract class Shape
    {
        protected Shape()
        {

        }
        protected double _height;
        protected double _width;
        public double Height { get => _height; }
        public double Width { get => _width; }
        public virtual double Area { get; }
    }

    public class Rectangle : Shape
    {
        public Rectangle(double height, double width)
        {
            _height = height;
            _width = width;
        }
        public override double Area => _height * _width;
    }

    public class Square : Shape
    {
        public Square(double height)
        {
            _height = height;
            _width = height;
        }
        public override double Area => _height * _height;
    }
    public class Circle : Shape
    {
        public Circle(double height)
        {
            _height = height;
            _width = height;
        }
        public override double Area => Math.PI * _height * _height;
    }

}