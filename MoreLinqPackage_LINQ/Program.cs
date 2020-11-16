using System;
using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace MoreLinqPackage_LINQ
{
    public class MoreLinqDemo
    {
        public MoreLinqDemo()
        {
            //BatchDemo();
            //InterleaveDemo();
            //PermDemo();
            SplitDemo();
        }

        private void SplitDemo()
        {
            var rand = new Random();
            var numbers = Enumerable.Range(1, 100).Select(_ => rand.Next(10));

            var split = numbers.Split(5);
            foreach (var group in split)
                Console.WriteLine($"{group.Count()} elements :" +
                  string.Join(", ", group));
        }

        private void PermDemo()
        {
            char[] letters = "draw".ToCharArray();

            foreach (var item in letters.Permutations())
            {
                Console.WriteLine(new string(item.ToArray()));
            }
        }

        private void InterleaveDemo()
        {
            var rand = new Random();
            var wholeNumbers = Enumerable.Range(1, 10).Select(_ => (double)rand.Next(10));
            var fractNumbers = Enumerable.Range(1, 10).Select(_ => rand.NextDouble());

            foreach (var d in wholeNumbers.Interleave(fractNumbers)) // executing 2 work together
            {
                Console.Write(d);
                Console.Write("\t");
            }
            Console.WriteLine();
        }

        private void BatchDemo() // Batch/Group a numbers based on size such as 100 numbers, make group, in each group contain 10 number, total group = 10
        {
            var numbers = Enumerable.Range(1, 100);
            foreach (var batch in numbers.Batch(10))
            {
                Console.WriteLine("Got a batch!");
                foreach (var i in batch)
                    Console.Write($"{i}\t");
                Console.WriteLine();
            }
        }

    }
        class Program
        {
            static void Main(string[] args)
            {
                new MoreLinqDemo();
            }
        }
    }

