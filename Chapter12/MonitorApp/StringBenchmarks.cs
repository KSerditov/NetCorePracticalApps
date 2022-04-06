using System.Text;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Attributes;

namespace MonitorApp
{
    public class StringBenchmarks
    {
        int[] numbers;
        public StringBenchmarks()
        {
            numbers = Enumerable.Range(1, 20).ToArray();
        }

        [Benchmark(Baseline = true)]
        public string ConcatenationTest()
        {
            string s = String.Empty;
            for (int i = 0; i < numbers.Length; i++)
            {
                s += numbers[i] + ", ";
            }
            return s;
        }

        [Benchmark]
        public string StringBuilderTest()
        {
            StringBuilder sb = new();
            for (int i = 0; i < numbers.Length; i++)
            {
                sb.Append(numbers[i]);
                sb.Append(", ");
            }
            return sb.ToString();
        }
    }
}