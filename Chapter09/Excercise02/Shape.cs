namespace Excercise02
{
    [XmlInclude(typeof(Rectangle))]
    [XmlInclude(typeof(Circle))]
    public abstract class Shape
    {
        [XmlAttribute("color")]
        public string Color { get; set; }
    }

    public class Rectangle : Shape
    {
        [XmlAttribute("height")]
        public double Height { get; set; }
        [XmlAttribute("width")]
        public double Width { get; set; }
    }

    public class Circle : Shape
    {
        [XmlAttribute("radius")]
        public double Radius { get; set; }
    }
}