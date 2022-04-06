Delegates.Person p1 = new("Jack");
Delegates.Person p2 = new("Mary");

Console.WriteLine(p1.Name);
Console.WriteLine(p2.Name);

p2.Shout = ObjectName_Shout;
p2.Poke();

static void ObjectName_Shout(object? sender, EventArgs e){
    if(sender != null){
        Delegates.Person p = (Delegates.Person)sender;
        Console.WriteLine($"{p.Name} shouted!");
    }

}