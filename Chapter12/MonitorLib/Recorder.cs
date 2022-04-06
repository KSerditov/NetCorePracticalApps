using System.Diagnostics;
using static System.Console;

namespace MonitorLib;
public static class Recorder
{
    private static Stopwatch timer = new();
    private static long bytesPhysicalBefore = 0;
    private static long bytesVirtualBefore = 0;

    public static void Start()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
        bytesPhysicalBefore = Process.GetCurrentProcess().WorkingSet64;
        bytesVirtualBefore = Process.GetCurrentProcess().VirtualMemorySize64;
        timer.Restart();
        WriteLine(Process.GetCurrentProcess().WorkingSet64);
    }

    public static void Stop()
    {
        timer.Stop();
        long bytesPhysicalAfter = Process.GetCurrentProcess().WorkingSet64;
        long bytesVirtualAfter = Process.GetCurrentProcess().VirtualMemorySize64;
        WriteLine("{0} physical bytes used", bytesPhysicalAfter - bytesPhysicalBefore);
        WriteLine("{0} virtual bytes used", bytesVirtualAfter - bytesVirtualBefore);
        WriteLine("{0} timespan elapsed", timer.Elapsed);
        WriteLine("{0} total ms elapsed", timer.ElapsedMilliseconds);
    }
}
