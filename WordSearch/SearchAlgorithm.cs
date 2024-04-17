﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch
{
    public interface ISearchAlgorithm
    {
        public abstract int Search(string pattern, string[] list, string[] result);
    }
}
