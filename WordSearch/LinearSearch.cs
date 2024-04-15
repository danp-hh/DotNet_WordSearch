using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch
{
    public class LinearSearch
    {
        public static void SearchEquals(string pattern, string[] list, string[] result)
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

        public static void ParallelSearchEquals(string pattern, string[] list, string[] result) 
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
    }
}
