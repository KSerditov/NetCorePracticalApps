// See https://aka.ms/new-console-template for more information
//#error version
using System.Reflection;

System.Data.DataSet ds;
HttpClient client;

Assembly? assembly = Assembly.GetEntryAssembly();
if (assembly == null) return;
foreach (AssemblyName an in assembly.GetReferencedAssemblies()){
    Assembly a = Assembly.Load(an);
    int MethodCount = 0;
    foreach(TypeInfo ti in a.DefinedTypes){
        MethodCount += ti.GetMethods().Count();
    }
    Console.WriteLine(String.Format("{0} {1} {2} {3} {4}", an.FullName, "Methods:", MethodCount, "Types:", a.DefinedTypes.Count()));
    
}
/// <summary>
/// 
/// </summary>
static void Test(){

}

Console.WriteLine(sizeof(int));
Console.WriteLine(sizeof(double));
Console.WriteLine(sizeof(decimal));