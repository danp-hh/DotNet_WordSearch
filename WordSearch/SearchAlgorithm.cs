using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch
{
    public abstract class SearchAlgorithm
    {
        public abstract void Search(string pattern, string[] list, string[] result);
    }
}
