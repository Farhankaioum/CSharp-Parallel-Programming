using ConcurrentCollection;
using System;
using System.Threading.Tasks;

namespace ConcurrentCollections
{
    public class ConcurrentDictionaryDemo
    {
        static void Main(string[] args)
        {
            //ConcurrentDictionary.DictionaryP();
            //ConcurrentQueue.QueueP();
            //ConcurrentStack.StackP();
            //ConcurrentBag.BagP();

            // blocking collection
            Task.Factory.StartNew(BlockingCollection.ProduceAndConsume, BlockingCollection.cts.Token);
            Console.ReadKey();
            BlockingCollection.cts.Cancel();
        }
    }
}