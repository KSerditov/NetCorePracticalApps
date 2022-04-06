namespace Excercise02
{
    class Program
    {
        static void Main()
        {
            List<Shape> shapes = new()
            {
                new Circle { Color = "Red", Radius = 2.5 },
                new Rectangle { Color = "Black", Height = 20, Width = 10 },
                new Circle { Color = "Green", Radius = 8 },
                new Circle { Color = "Purple", Radius = 12.3 },
                new Rectangle { Color = "Yellow", Height = 20, Width = 10 },
                new Rectangle { Color = "White", Height = 45, Width = 18 }
            };

            XmlSerializer xs = new(shapes.GetType());

            using (FileStream fs = File.Create("test.xml"))
            {

                xs.Serialize(fs, shapes);
            }

            List<Shape> restoredShapes;

            using (Stream reader = new FileStream("test.xml", FileMode.Open))
            {
                restoredShapes = (List<Shape>)xs.Deserialize(reader);
            }
        }
    }
}