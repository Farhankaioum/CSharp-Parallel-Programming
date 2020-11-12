using System;
using System.CodeDom;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelLoop
{
    public class ParallelLoops_BreakingAndStopping
    {
        public static void Demo()
        {
            var cts = new CancellationTokenSource();

            var po = new ParallelOptions { CancellationToken = cts.Token };
            ParallelLoopResult result = Parallel.For(0, 20, po, (int x, ParallelLoopState state) =>
            {
                Console.Write($"{x}[{Task.CurrentId}]\t");
                if (x == 10)
                {
                    cts.Cancel();
                    //throw new Exception(); // execution stops on exception
                    //state.Stop(); // stop execution as soon as possible
                    //state.Break(); // request that loop stop execution of iterations beyond current iteration asap
                }
                if (state.IsExceptional)
                    Console.Write($"EX[{Task.CurrentId}]\t");

                // state.LowestBreakIteration, ShouldExitCurrentIteration
            });

            Console.WriteLine();
            Console.WriteLine($"Was loop completed? {result.IsCompleted}"); // uncomment break
            if (result.LowestBreakIteration.HasValue)
                Console.WriteLine($"Lowest break iteration: {result.LowestBreakIteration}");
        }
        public static void BreakingAndStopping()
        {
            try
            {
                Demo();
            }
            catch (OperationCanceledException) { }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine(e.Message);
                    return true;
                });
            }
        }
    }
}
