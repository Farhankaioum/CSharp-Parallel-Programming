using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace ParallelLoop
{
    public class ThredLocalStorage
    {
        private Random random = new Random();

        public static void AddNumber()
        {
            // add numbers from 1 to 100

            int sum = 0;
            Parallel.For(1, 1001, x =>
            {
                Console.Write($"[{x}] t={Task.CurrentId}\t");
                Interlocked.Add(ref sum, x); // concurrent access to sum from all these threads is inefficient
                                             // all tasks can add up their respective values, then add sum to total sum
            });
            Console.WriteLine($"\nSum of 1..100 = {sum}");

            sum = 0;
            Parallel.For(1, 1001,
              () => 0, // initialize local state, show code completion for next arg
              (x, state, tls) =>
              {
                //Console.WriteLine($"{Task.CurrentId}");
            tls += x;
                  Console.WriteLine($"Task {Task.CurrentId} has sum {tls}");
                  return tls;
              },
              partialSum =>
              {
                  Console.WriteLine($"Partial value of task {Task.CurrentId} is {partialSum}");
                  Interlocked.Add(ref sum, partialSum);
              }
            );
            Console.WriteLine($"Sum of 1..100 = {sum}");
        }
    }
}
