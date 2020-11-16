using System;
using System.Linq;

namespace LinqParallelExecution
{
    class Program
    {
        static void Main(string[] args)
        {
            ParallelLinqExample();

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
    }
}
