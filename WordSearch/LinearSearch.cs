using System.Collections.Concurrent;

namespace WordSearch
{
    public class LinearSearch : ISearchAlgorithm
    {
        public static int StartsWith(string pattern, string[] list, string[] result)
        {
            int j = 0;

            for(int i = 0; i < list.Length; i++)
            {
                if(list[i].StartsWith(pattern, StringComparison.Ordinal))
                {
                    result[j] = list[i];
                    j++;
                }
            }
            return j;
        }

        public static int ParallelStartsWith(string pattern, string[] list, string[] result)
        {
            object _lock = new object();
            int j = 0;

            Parallel.For(0, list.Length, (i) =>
            {
                if (list[i].StartsWith(pattern, StringComparison.Ordinal))
                {
                    lock(_lock)
                    {
                        result[j] = list[i];
                        j++;

                    }
                }
            });

            return j;
        }

        public static int ParallelStartsWithConrurrentQueue(string pattern, IReadOnlyList<string> list, string[] result)
        {
            ConcurrentQueue<string> resultQueue = new ConcurrentQueue<string> { };

            Parallel.For(0, list.Count, (i) =>
            {
                string entry = list[i];
                if (entry.StartsWith(pattern, StringComparison.Ordinal))
                {
                    resultQueue.Enqueue(entry);
                }
            });
            resultQueue.CopyTo(result, 0);

            return resultQueue.Count;
        }

        public int Search(string pattern, string[] list, string[] result)
        {
            return ParallelStartsWith(pattern, list, result);
        }
    }
}
