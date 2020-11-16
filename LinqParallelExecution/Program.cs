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
            //CancellationAndExceptionInParallelLinq();
            MergeOptionInParallelLinqExecution();

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

        public static void MergeOptionInParallelLinqExecution()
        {
            var numbers = Enumerable.Range(1, 20).ToArray() ;

            var results = numbers
                .AsParallel()
                .WithMergeOptions(ParallelMergeOptions.FullyBuffered)// if you want to product result first then consume, consume means print or show the result
                .Select(i =>
                {
                    double result = Math.Log10(i);
                    Console.Write($"P: {result}\t");
                    return result;

                });

            foreach (var c in results)
                Console.Write($"C: {c}\t");
        }

         public static void CustomAggregationForParallelLinq()
        {
            var normalSum = Enumerable.Range(1, 1000)
                .Aggregate(0, (i, acc) => i + acc);

            var sum = ParallelEnumerable.Range(1, 1000)
                .Aggregate(
                    0, // seed value
                    (partialSum, i) => partialSum += i, // An accumulator function to be invoked on each element in a partition.
                    (total, subtotal) => total += subtotal, // An accumulator function to be invoked on the yielded accumulator result from
                                                            //     each partition.
                    i => i); // A function to transform the final accumulator value into the result value.

            Console.WriteLine(sum);
        }
    }
}
