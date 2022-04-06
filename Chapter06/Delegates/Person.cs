namespace Delegates;
public class Person : IComparable<Person>
{
    private int AngelLevel = 3;
    public string Name { get; }
    public EventHandler? Shout;

    public delegate void MyMethod(Person o, string param);
    public MyMethod? Shout2;

    public Person(string name)
    {
        Name = name;
    }

    public void Poke()
    {
        AngelLevel++;
        if(AngelLevel > 3){
            if(Shout != null){
                Shout(this, EventArgs.Empty);
            }
        }
    }

    public void Pole(string str){
        if(Shout2 != null){
            Shout2(this, "test msg");
        }
    }

    public int CompareTo(Person? other)
    {
        throw new NotImplementedException();
    }
}