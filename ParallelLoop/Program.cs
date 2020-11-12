using System;

namespace ParallelLoop
{
    class Program
    {
        static void Main(string[] args)
        {
            Loops.Loop();
            ParallelLoops_BreakingAndStopping.BreakingAndStopping();
            ThredLocalStorage.AddNumber();
            PartitioningDemo.BenchmarkRun();
        }
    }
}
