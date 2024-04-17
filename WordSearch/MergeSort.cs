using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch
{
    public class MergeSort
    {

    }

    public class LinkedCharList
    {
        public readonly char Value;
        public readonly int Depth;
        public readonly Dictionary<char, LinkedCharList> Children;

        public LinkedCharList(char value, int depth)
        {
            Value = value;
            Depth = depth;
            Children = new Dictionary<char, LinkedCharList>();
        }
    }
}
