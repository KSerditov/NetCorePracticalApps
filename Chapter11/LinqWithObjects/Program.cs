List<Exception> exceptions = new()
{
    new ArgumentException(),
    new SystemException(),
    new NullReferenceException(),
    new ApplicationException(),
    new IndexOutOfRangeException()
};

var exptns = exceptions.OfType<SystemException>();
//var exptns = exceptions.Where(e => e.GetType() == typeof(SystemException));
foreach (var ex in exptns)
{
    Console.WriteLine(ex);
}

string[] s1 = new[] { "Alex", "John", "Alex2", "Andrey", "Pavel", "Alex" };
string[] s2 = new[] { "Alex" };
string[] s3 = new[] { "Anna", "Alexey", "Jack" };

var s1distinct = s1.Distinct();
Console.WriteLine(String.Join(",", s1distinct.ToArray()));

var s1distinctsub = s1.DistinctBy(s => s.Substring(0, 2));
Console.WriteLine(String.Join(",", s1distinctsub.ToArray()));

var o2 = s1.Union(s2);
Console.WriteLine(String.Join(",", o2.ToArray()));

var o3 = s1.Concat(s2);
Console.WriteLine(String.Join(",", o3.ToArray()));

var o4 = s1.Intersect(s2);
Console.WriteLine(String.Join(",", o4.ToArray()));

//Except
//Zip (matches in pairs: s1[0]-s2[0], etc.)
/*
string[] names = new string[] { "Alex", "John", "Michael", "Toby", "Jack" };

var query1 = names.Where(s => s.Contains("a"));

var query2 = from name in names where name.EndsWith("x") select name;

foreach(var a in query1){
    Console.WriteLine(a);
}

foreach(var a in query2){
    Console.WriteLine(a);
}

bool MoreThanThree(string s){
    return s.Length > 3;
}

var q = names.Where(MoreThanThree);
*/