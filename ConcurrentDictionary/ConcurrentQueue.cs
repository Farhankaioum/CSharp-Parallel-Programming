using System;
using System.Collections.Concurrent;

namespace ConcurrentCollection
{
    public class ConcurrentQueue
    {
        public static void QueueP()
        {
            var q = new ConcurrentQueue<int>();
            q.Enqueue(1);
            q.Enqueue(2);

            // Queue: 2 1 <- front

            int result;
            //int last = q.Dequeue();
            if (q.TryDequeue(out result))
            {
                Console.WriteLine($"Removed element {result}");
            }

            // Queue: 2

            //int peeked = q.Peek();
            if (q.TryPeek(out result))
            {
                Console.WriteLine($"Last element is {result}");
            }
        }
    }
}
