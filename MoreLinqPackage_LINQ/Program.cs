using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            // new MoreLinqDemo();

            // MoreLinq Example
            var numbers = Enumerable.Range(1, 10);
            var numbers2 = Enumerable.Range(1, 10);
            var names = new List<string>();
            names.Add("One");
            names.Add("two");
            names.Add("three");
            names.Add("four");
            names.Add("five");
            var names2 = names;
            names2.Add("six");

            var aggregate = numbers.Aggregate(1, (a, b) => a + b);
            var aggregateRight = numbers.AggregateRight(1, (a, b) => a + b); // Aggregate/Calculate value from right side
            //Console.WriteLine(aggregateRight);

            var assert = numbers.Assert(n => n > 0); // every element should be fulfill the given condition, otherwise given error
            var assertCount = names.AssertCount(5); // if you don't know which value inside the elements, but you know total value, then it's help you to find all value

            var atleast = numbers.AtLeast(5); // check atleast a number elements contains this collection
            var atMost = numbers.AtMost(10); // check list of element atmost given value, means less than or equal to given value

            var backsert = numbers.Backsert(new List<int> { 10,21,33}, 2); // insert list of value in specific position

            var batch = numbers.Batch(2); // making group from listof element, given specified number

            var cartesian = numbers.Cartesian(numbers2, (a, b) =>  a + b); // cross multiplication 

            var choose = numbers.Choose(c => (c > 2, c)); // choose a item based on the condition

            var compareCount = numbers.CompareCount(numbers2); // compare two list, return an integer, check the first list has,
                                                               //  fewer, the same or more elements than the second sequence.
                                                               // fewer => -1, same => 0, more => 1

            numbers.Consume(); // Completely consumes the given sequence. This method uses immediate execution, 
                               // and doesn't store any data during execution

            var countBetween = names.CountBetween(1, 10); // count element based on min and max range
            var countBy = numbers.CountBy(c => c > 5); // return KeyValuePair<bool, int> => return how much element fulfill this condition and how much not fulfill.
            var countDown = numbers.CountDown(0, (a, b) => a + b);

            var distinctBy = numbers.DistinctBy(p => p > 2);

            var endsWith = names.EndsWith(names2); // end of the first sequence is equivalent to the second sequence


            Console.WriteLine(endsWith);
            
        }
    }

  
}

