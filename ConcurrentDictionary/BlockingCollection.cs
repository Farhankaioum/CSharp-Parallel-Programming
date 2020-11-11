using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrentCollection
{
    public class BlockingCollection
    {
        // BlockingCollection uses for if you want to control your collection, like want to add into your collection at a time
        // not more than 10 values, you can do it using BlockingCollection with /* bounded */ capacity.
        // BlockingCollection uses produces consumer pattern, BlockingCollection not itself a collection, it's a wrapper collection
        // Uderlying other collection
        static BlockingCollection<int> messages = new BlockingCollection<int>(
                            new ConcurrentBag<int>(), 10 /* bounded */
        );

        public static CancellationTokenSource cts = new CancellationTokenSource();

        public static void ProduceAndConsume()
        {
            var producer = Task.Factory.StartNew(RunProducer);
            var consumer = Task.Factory.StartNew(RunConsumer);

            try
            {
                Task.WaitAll(new[] { producer, consumer }, cts.Token);
            }
            catch (AggregateException ae)
            {
                ae.Handle(e => true);
            }
        }

        private static Random random = new Random();

        private static void RunConsumer()
        {
            foreach (var item in messages.GetConsumingEnumerable()) // GetConsumingEnumerable special method, when it's call, it check if
            {                                                       // the Enumerable/Collection is not empty, then it work
                cts.Token.ThrowIfCancellationRequested();
                Console.WriteLine($"-{item}");
                Thread.Sleep(random.Next(1000));
            }
        }

        private static void RunProducer()
        {
            while (true)
            {
                cts.Token.ThrowIfCancellationRequested();
                int i = random.Next(100);
                messages.Add(i);
                Console.WriteLine($"+{i}\t");
                Thread.Sleep(random.Next(1000));
            }
        }
    }
}
