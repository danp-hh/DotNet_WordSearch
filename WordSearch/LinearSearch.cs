using System;
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

        public static void StartsWith(string pattern, string[] list, string[] result)
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
        }

        public static void ParallelStartsWith(string pattern, string[] list, string[] result)
        {
            int j = 0;
            object _lockl = new object();

            Parallel.For(0, list.Length, (i) =>
            {
                if (list[i].StartsWith(pattern, StringComparison.Ordinal))
                {
                    lock(_lockl) 
                    {
                        result[j] = list[i];
                        j++;
                    }
                }
            });
        }

        public override void Search(string pattern, string[] list, string[] result)
        {
            ParallelStartsWith(pattern, list, result);
        }
    }
}
