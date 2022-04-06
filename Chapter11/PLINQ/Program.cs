using static System.Console;
using System.Diagnostics;
using PLINQ;

static int Fib(int i) =>
    i switch
    {
        1 => 0,
        2 => 1,
        _ => Fib(i - 1) + Fib(i - 2)
    };

Stopwatch s = new();
WriteLine("Ready?");
ReadLine();
s.Start();
int max = 45;
IEnumerable<int> nums = Enumerable.Range(1, max);
WriteLine("Processing...");
//int[] fibNums = nums.Select(n => Fib(n)).ToArray();
int[] fibNums = nums.AsParallel().Select(n => Fib(n)).OrderBy(n => n).ToArray();
s.Stop();
WriteLine(s.ElapsedMilliseconds);
WriteLine(String.Join(", ", fibNums));



var t = nums.FilterMoreFour();
