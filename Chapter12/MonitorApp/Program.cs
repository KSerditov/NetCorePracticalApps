using MonitorLib;
using static System.Console;
using BenchmarkDotNet.Running;
using MonitorApp;

BenchmarkRunner.Run<StringBenchmarks>();

/* WriteLine("Processing started...");
Recorder.Start();
int[] x = Enumerable.Range(1, 10_000).ToArray();

Thread.Sleep(new Random().Next(5, 10) * 1000);
Recorder.Stop(); */
