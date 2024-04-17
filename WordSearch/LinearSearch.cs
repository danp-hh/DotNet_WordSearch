using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch
{
    public class LinearSearch : SearchAlgorithm
    {
        public static void Equals(string pattern, string[] list, string[] result)
        {
            int j = 0;

            for(int i  = 0; i < list.Length; i++) 
            {
                if (String.CompareOrdinal(list[i], pattern) == 0) 
                {
                    result[j] = list[i];
                    j++;
                }
            }

        }

        public static void ParallelEquals(string pattern, string[] list, string[] result) 
        {
            int j = 0;
            object _lock = new object();

            Parallel.For(0, list.Length, (i) =>
            {
                if (String.CompareOrdinal(list[i], pattern) == 0)
                {
                    lock (_lock)
                    {
                        result[j] = list[i];
                        j++;

                    }
                }
            });

        }

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

        public static int ParallelStartsWith(string pattern, IReadOnlyList<string> list, string[] result)
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
            result = resultQueue.ToArray();
            return resultQueue.Count;
        }

        public override int Search(string pattern, string[] list, string[] result)
        {
            return ParallelStartsWith(pattern, list, result);
        }
    }
}
