using System;
using System.Collections.Generic;
using System.Linq;

namespace GettingStartLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            //ImpelmentingIEnumerable.Print();

            // generation operations using Linq and Enumerable
            Console.WriteLine(Enumerable.Empty<int>());

            Console.WriteLine(Enumerable.Repeat("hello", 3));

            Console.WriteLine(Enumerable.Range(1, 10));
            Console.WriteLine(Enumerable.Range('a', 'z' - 'a').Select(c => (char)c));
            Console.WriteLine(Enumerable.Range(1, 10).Select(i => new string('x', i)));

            
            Console.WriteLine();

            var values = Enumerable.Range(1, 10).Select(i => {
              
                return i;
            });

            foreach (var item in values)
            {
                Console.WriteLine(item);
            }
        }
    }
}
