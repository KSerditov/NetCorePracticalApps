using System.Diagnostics;

Trace.Listeners.Add(new TextWriterTraceListener(@"C:\temp\log.txt"));
Trace.AutoFlush = true;

Console.WriteLine("Hello, World!");
double a = 4.5;
double b = 2.5;
Debug.WriteLine($"{a} + {b} = ");
Trace.WriteLine(Add(a, b));

static double Add(double a, double b){
    return a * b;
}