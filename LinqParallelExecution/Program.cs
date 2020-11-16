using System;
using System.Linq;
using System.Threading;

namespace LinqParallelExecution
{
    class Program
    {
        static void Main(string[] args)
        {
            //ParallelLinqExample();
            CancellationAndExceptionInParallelLinq();

        }

        public static void ParallelLinqExample()
        {
            int count = 50;
            var items = Enumerable.Range(1, count).ToArray();
            var results = new int[count];

            items.AsParallel().ForAll(x => {
                int newValue = x * x * x;
                results[x - 1] = newValue;
            });

            foreach(var item in results)
                Console.Write($" {item}\t");


            var cubes = items.AsParallel().AsOrdered().Select(x => x * x * x); // if want to result in ordered fashion

            Console.WriteLine();
            var arr = cubes.ToArray();
            foreach (var item in cubes)
                Console.Write($" {item}\t");
        }

        public static void CancellationAndExceptionInParallelLinq()
        {
            var cts = new CancellationTokenSource();

            var items = ParallelEnumerable.Range(1, 20);

            var results = items.WithCancellation(cts.Token).Select(i => 
            {
                double result = Math.Log10(i);
                return result;
                
            });

            try
            {
                foreach(var c in results)
                {
                    if (c > 1)
                        cts.Cancel();

                    Console.WriteLine(c);
                }
                    
            }
            catch(AggregateException ae)
            {
                ae.Handle(e => {
                    Console.WriteLine("Handled");
                    return true;
                });
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine("cancel the operation");
            }
        }
    }
}
